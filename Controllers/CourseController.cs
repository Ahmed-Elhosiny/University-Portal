using  University_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace University_Portal.Controllers
{
    public class CourseController : Controller
    {
        private UniversityMvcContext context;
        public CourseController(UniversityMvcContext _context)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var courses = context.Courses.ToList();
            foreach (var item in courses)
            {
                int students = context.Students.Where(s => s.CourseId == item.Id).Count();
                ViewData[item.Id.ToString()] = students;
            }
            return View(courses);
        }
        public async Task<IActionResult> CourseSearch(string searchTerm = "", string sortBy = "Name", string sortOrder = "asc")
        {
            var coursesQuery = context.Courses.Include(c => c.Students).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                coursesQuery = coursesQuery.Where(c => c.Name.Contains(searchTerm));
            }

            coursesQuery = sortBy?.ToLower() switch
            {
                "credits" => sortOrder == "desc" ? coursesQuery.OrderByDescending(c => c.Credits) : coursesQuery.OrderBy(c => c.Credits),
                "students" => sortOrder == "desc" ? coursesQuery.OrderByDescending(c => c.Students.Count) : coursesQuery.OrderBy(c => c.Students.Count),
                _ => sortOrder == "desc" ? coursesQuery.OrderByDescending(c => c.Name) : coursesQuery.OrderBy(c => c.Name)
            };

            var courses = await coursesQuery.ToListAsync();

            ViewBag.SearchTerm = searchTerm;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;

            return View("Index", courses);
        }


        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Courses.Add(course);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("DatabaseError", "Something Went Wrong");
                    return View(course);
                }
            }
            return View(course);
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await context.Courses.FindAsync(id);
            if (course == null)
            {
                TempData["ErrorMessage"] = "Course not found.";
                return RedirectToAction("Index");
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingCourse = await context.Courses.FindAsync(course.Id);
                    if (existingCourse == null)
                    {
                        TempData["ErrorMessage"] = "Course not found.";
                        return RedirectToAction("Index");
                    }

                    existingCourse.Name = course.Name;
                    existingCourse.Credits = course.Credits;

                    await context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Course '{course.Name}' updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the course.");
                    return View(course);
                }
            }
            return View(course);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var course = await context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                TempData["ErrorMessage"] = "Course not found.";
                return RedirectToAction("Index");
            }

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course = await context.Courses
                    .Include(c => c.Students)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction("Index");
                }

                // Check if course has enrolled students
                if (course.Students.Any())
                {
                    TempData["ErrorMessage"] = $"Cannot delete '{course.Name}' because it has {course.Students.Count} enrolled student(s). Please transfer or remove students first.";
                    return RedirectToAction("Index");
                }

                context.Courses.Remove(course);
                await context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Course '{course.Name}' deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the course.";
            }

            return RedirectToAction("Index");
        }

    }
}
