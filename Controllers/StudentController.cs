
using University_Portal.Models;
using University_Portal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace University_Portal.Controllers
{
    public class StudentController(UniversityMvcContext _context) : Controller
    {
        private readonly UniversityMvcContext context = _context;

        public async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            // Get all courses for filter dropdown
            var courses = await context.Courses.ToListAsync();
            searchModel.Courses = courses;

            // Start with all students query
            var studentsQuery = context.Students.Include(s => s.Course).AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchModel.SearchTerm))
            {
                studentsQuery = studentsQuery.Where(s =>
                    s.Name.Contains(searchModel.SearchTerm) ||
                    s.Course.Name.Contains(searchModel.SearchTerm));
            }

            if (searchModel.SelectedCourseId.HasValue)
            {
                studentsQuery = studentsQuery.Where(s => s.CourseId == searchModel.SelectedCourseId.Value);
            }

            if (searchModel.MinLevel.HasValue)
            {
                studentsQuery = studentsQuery.Where(s => s.Level >= searchModel.MinLevel.Value);
            }

            if (searchModel.MaxLevel.HasValue)
            {
                studentsQuery = studentsQuery.Where(s => s.Level <= searchModel.MaxLevel.Value);
            }

            if (searchModel.MinAge.HasValue)
            {
                studentsQuery = studentsQuery.Where(s => s.Age >= searchModel.MinAge.Value);
            }

            if (searchModel.MaxAge.HasValue)
            {
                studentsQuery = studentsQuery.Where(s => s.Age <= searchModel.MaxAge.Value);
            }

            // Apply sorting
            studentsQuery = searchModel.SortBy?.ToLower() switch
            {
                "age" => searchModel.SortOrder == "desc" ? studentsQuery.OrderByDescending(s => s.Age) : studentsQuery.OrderBy(s => s.Age),
                "level" => searchModel.SortOrder == "desc" ? studentsQuery.OrderByDescending(s => s.Level) : studentsQuery.OrderBy(s => s.Level),
                "course" => searchModel.SortOrder == "desc" ? studentsQuery.OrderByDescending(s => s.Course.Name) : studentsQuery.OrderBy(s => s.Course.Name),
                _ => searchModel.SortOrder == "desc" ? studentsQuery.OrderByDescending(s => s.Name) : studentsQuery.OrderBy(s => s.Name)
            };

            // Get total count for pagination
            searchModel.TotalCount = await studentsQuery.CountAsync();

            // Apply pagination
            var students = await studentsQuery
                .Skip((searchModel.CurrentPage - 1) * searchModel.PageSize)
                .Take(searchModel.PageSize)
                .ToListAsync();

            searchModel.Students = students;

            return View(searchModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Course"] = new SelectList(context.Courses.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Student student, IFormFile? imageFile)
        {
            // Remove Course from ModelState since it's not needed for validation
            ModelState.Remove("Course");
            ModelState.Remove("Image"); // Add this line to remove Image validation

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Create Images directory if it doesn't exist
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Create unique filename
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        student.Image = uniqueFileName;
                    }
                    else
                    {
                        student.Image = "default.png";
                    }

                    context.Students.Add(student);
                    await context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Student '{student.Name}' has been added successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the actual error for debugging
                    System.Diagnostics.Debug.WriteLine($"Error adding student: {ex.Message}");
                    ModelState.AddModelError("", $"An error occurred while saving the student: {ex.Message}");
                    ViewData["Course"] = new SelectList(context.Courses.ToList(), "Id", "Name");
                    return View(student);
                }
            }

            ViewData["Course"] = new SelectList(context.Courses.ToList(), "Id", "Name");
            return View(student);
        }


        // Improved Details action with better error handling
        public async Task<IActionResult> Details(int id)
        {
            var student = await context.Students
                .Include(s => s.Course) // Use Include for better performance
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToAction("Index");
            }

            var studentDetails = new StudentDetailsViewModel
            {
                SId = student.Id,
                SName = student.Name,
                SAge = student.Age,
                SLevel = student.Level,
                SImage = student.Image,
                CName = student.Course.Name,
                CCredits = student.Course.Credits
            };

            return View(studentDetails);
        }
        public IActionResult Update(int id)
        {
            // Questions: Can I send the full model from the Index View instead of retrieving it here again from the database ? 
            var toUpdate = context.Students.FirstOrDefault(s => s.Id == id);
            if (toUpdate == null) return BadRequest();

            ViewData["Course"] = new SelectList(context.Courses.ToList(), "Id", "Name");
            return View(toUpdate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Student student, IFormFile? imageFile)
        {
            ModelState.Remove("Course");
            ModelState.Remove("Image"); // Add this line

            if (ModelState.IsValid)
            {
                try
                {
                    var existingStudent = await context.Students.FindAsync(student.Id);
                    if (existingStudent == null)
                    {
                        TempData["ErrorMessage"] = "Student not found.";
                        return RedirectToAction("Index");
                    }

                    // Handle image upload
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Delete old image if not default
                        if (!string.IsNullOrEmpty(existingStudent.Image) &&
                            existingStudent.Image != "default.png" &&
                            System.IO.File.Exists(Path.Combine(uploadsFolder, existingStudent.Image)))
                        {
                            try
                            {
                                System.IO.File.Delete(Path.Combine(uploadsFolder, existingStudent.Image));
                            }
                            catch
                            {
                                // Ignore file deletion errors - file might be in use
                            }
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        existingStudent.Image = uniqueFileName;
                    }

                    // Update other properties
                    existingStudent.Name = student.Name;
                    existingStudent.Age = student.Age;
                    existingStudent.Level = student.Level;
                    existingStudent.CourseId = student.CourseId;

                    await context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student updated successfully!";
                    return RedirectToAction("Details", new { id = student.Id });
                }
                catch (Exception ex)
                {
                    // Log the actual error for debugging
                    System.Diagnostics.Debug.WriteLine($"Error updating student: {ex.Message}");
                    ModelState.AddModelError("", $"An error occurred while updating the student: {ex.Message}");
                    ViewData["Course"] = new SelectList(context.Courses.ToList(), "Id", "Name", student.CourseId);
                    return View(student);
                }
            }

            ViewData["Course"] = new SelectList(context.Courses.ToList(), "Id", "Name", student.CourseId);
            return View(student);
        }
        public IActionResult Delete(int id)
        {

            var toDelete = context.Students.FirstOrDefault(s => s.Id == id);
            if (toDelete == null) return BadRequest();

            try
            {
                context.Students.Remove(toDelete);
                context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("DatabaseError", "Something Went Wrong");
            }

            return RedirectToAction("Index");
        }

    }
}
