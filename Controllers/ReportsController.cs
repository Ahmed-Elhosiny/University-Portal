using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University_Portal.Models;
using System.Text;

namespace University_Portal.Controllers
{
    public class ReportsController : Controller
    {
        private readonly UniversityMvcContext _context;

        public ReportsController(UniversityMvcContext context)
        {
            _context = context;
        }

        // Reports Index Page
        public IActionResult Index()
        {
            return View();
        }

        // Export Students to CSV
        public async Task<IActionResult> ExportStudentsCSV()
        {
            var students = await _context.Students
                .Include(s => s.Course)
                .OrderBy(s => s.Name)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("ID,Name,Age,Level,Course,Image");

            foreach (var student in students)
            {
                csv.AppendLine($"{student.Id},{student.Name},{student.Age},{student.Level},{student.Course.Name},{student.Image}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"Students_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
        }

        // Export Courses to CSV
        public async Task<IActionResult> ExportCoursesCSV()
        {
            var courses = await _context.Courses
                .Include(c => c.Students)
                .OrderBy(c => c.Name)
                .ToListAsync();

            var csv = new StringBuilder();
            csv.AppendLine("ID,Course Name,Credits,Enrolled Students");

            foreach (var course in courses)
            {
                csv.AppendLine($"{course.Id},{course.Name},{course.Credits},{course.Students.Count}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", $"Courses_Export_{DateTime.Now:yyyyMMdd_HHmmss}.csv");
        }

        // Generate Summary Report
        public async Task<IActionResult> SummaryReport()
        {
            var totalStudents = await _context.Students.CountAsync();
            var totalCourses = await _context.Courses.CountAsync();
            
            var courseStats = await _context.Courses
                .Include(c => c.Students)
                .Select(c => new
                {
                    CourseName = c.Name,
                    Credits = c.Credits,
                    EnrolledStudents = c.Students.Count
                })
                .ToListAsync();

            var levelStats = await _context.Students
                .GroupBy(s => s.Level)
                .Select(g => new
                {
                    Level = g.Key,
                    Count = g.Count()
                })
                .OrderBy(l => l.Level)
                .ToListAsync();

            var report = new StringBuilder();
            report.AppendLine("=== UNIVERSITY PORTAL - SUMMARY REPORT ===");
            report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine();
            report.AppendLine("OVERVIEW");
            report.AppendLine("--------");
            report.AppendLine($"Total Students: {totalStudents}");
            report.AppendLine($"Total Courses: {totalCourses}");
            report.AppendLine($"Average Students per Course: {(totalCourses > 0 ? (double)totalStudents / totalCourses : 0):F2}");
            report.AppendLine();
            
            report.AppendLine("COURSE ENROLLMENT");
            report.AppendLine("-----------------");
            foreach (var course in courseStats)
            {
                report.AppendLine($"{course.CourseName} ({course.Credits} credits): {course.EnrolledStudents} students");
            }
            report.AppendLine();
            
            report.AppendLine("STUDENTS BY LEVEL");
            report.AppendLine("-----------------");
            foreach (var level in levelStats)
            {
                report.AppendLine($"Level {level.Level}: {level.Count} students");
            }

            var bytes = Encoding.UTF8.GetBytes(report.ToString());
            return File(bytes, "text/plain", $"Summary_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        }

        // Student Details Report
        public async Task<IActionResult> StudentDetailsReport()
        {
            var students = await _context.Students
                .Include(s => s.Course)
                .OrderBy(s => s.Course.Name)
                .ThenBy(s => s.Name)
                .ToListAsync();

            var report = new StringBuilder();
            report.AppendLine("=== STUDENT DETAILS REPORT ===");
            report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"Total Students: {students.Count}");
            report.AppendLine();

            var groupedByCourse = students.GroupBy(s => s.Course.Name);
            foreach (var group in groupedByCourse)
            {
                report.AppendLine($"\n{group.Key.ToUpper()}");
                report.AppendLine(new string('-', group.Key.Length));
                foreach (var student in group)
                {
                    report.AppendLine($"  • {student.Name} (ID: {student.Id}) - Age: {student.Age}, Level: {student.Level}");
                }
            }

            var bytes = Encoding.UTF8.GetBytes(report.ToString());
            return File(bytes, "text/plain", $"Student_Details_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        }

        // Course Analytics Report
        public async Task<IActionResult> CourseAnalyticsReport()
        {
            var courses = await _context.Courses
                .Include(c => c.Students)
                .OrderByDescending(c => c.Students.Count)
                .ToListAsync();

            var report = new StringBuilder();
            report.AppendLine("=== COURSE ANALYTICS REPORT ===");
            report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"Total Courses: {courses.Count}");
            report.AppendLine();

            report.AppendLine("ENROLLMENT STATISTICS");
            report.AppendLine("--------------------");
            foreach (var course in courses)
            {
                var avgAge = course.Students.Any() ? course.Students.Average(s => s.Age) : 0;
                var avgLevel = course.Students.Any() ? course.Students.Average(s => s.Level) : 0;
                
                report.AppendLine($"\n{course.Name}");
                report.AppendLine($"  Credits: {course.Credits}");
                report.AppendLine($"  Enrolled Students: {course.Students.Count}");
                report.AppendLine($"  Average Age: {avgAge:F1}");
                report.AppendLine($"  Average Level: {avgLevel:F1}");
            }

            var bytes = Encoding.UTF8.GetBytes(report.ToString());
            return File(bytes, "text/plain", $"Course_Analytics_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
        }
    }
}
