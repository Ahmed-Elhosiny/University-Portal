using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University_Portal.Models;
using University_Portal.ViewModels;

namespace University_Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly UniversityMvcContext _context;

        public HomeController(UniversityMvcContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = new DashboardViewModel();

            try
            {
                // Basic Statistics
                dashboard.TotalStudents = await _context.Students.CountAsync();
                dashboard.TotalCourses = await _context.Courses.CountAsync();
                dashboard.TotalEnrollments = dashboard.TotalStudents; // Since each student has one course

                if (dashboard.TotalCourses > 0)
                {
                    dashboard.AverageStudentsPerCourse = Math.Round((double)dashboard.TotalStudents / dashboard.TotalCourses, 1);
                }

                // Course Statistics
                dashboard.CourseStats = await _context.Courses
                    .Include(c => c.Students)
                    .Select(c => new CourseEnrollmentStats
                    {
                        CourseName = c.Name,
                        StudentCount = c.Students.Count,
                        Credits = c.Credits
                    })
                    .OrderByDescending(c => c.StudentCount)
                    .Take(5)
                    .ToListAsync();

                // Level Distribution
                dashboard.LevelStats = await _context.Students
                    .GroupBy(s => s.Level)
                    .Select(g => new LevelDistribution
                    {
                        Level = g.Key,
                        StudentCount = g.Count(),
                        Percentage = dashboard.TotalStudents > 0 ? Math.Round((double)g.Count() / dashboard.TotalStudents * 100, 1) : 0
                    })
                    .OrderBy(l => l.Level)
                    .ToListAsync();

                // Most Popular Course
                var mostPopular = dashboard.CourseStats.FirstOrDefault();
                if (mostPopular != null)
                {
                    dashboard.MostPopularCourse = mostPopular.CourseName;
                    dashboard.MostPopularCourseCount = mostPopular.StudentCount;
                }

                // Recent Activities (Mock data - you can enhance this with actual activity tracking)
                dashboard.RecentActivities = GenerateRecentActivities();

                // Monthly stats (mock data for demo)
                dashboard.StudentsThisMonth = Math.Min(dashboard.TotalStudents, Random.Shared.Next(1, 10));
                dashboard.CoursesThisMonth = Math.Min(dashboard.TotalCourses, Random.Shared.Next(1, 3));
            }
            catch (Exception ex)
            {
                // Log error and show empty dashboard
                ViewBag.Error = "Unable to load dashboard data. Please check your database connection.";
            }

            return View(dashboard);
        }

        private List<RecentActivity> GenerateRecentActivities()
        {
            var activities = new List<RecentActivity>
            {
                new RecentActivity
                {
                    ActivityType = "Student Added",
                    Description = "New student registered to the system",
                    Timestamp = DateTime.Now.AddHours(-2),
                    Icon = "fas fa-user-plus",
                    Color = "success"
                },
                new RecentActivity
                {
                    ActivityType = "Course Updated",
                    Description = "Course information was modified",
                    Timestamp = DateTime.Now.AddHours(-5),
                    Icon = "fas fa-edit",
                    Color = "warning"
                },
                new RecentActivity
                {
                    ActivityType = "System Backup",
                    Description = "Automatic database backup completed",
                    Timestamp = DateTime.Now.AddDays(-1),
                    Icon = "fas fa-database",
                    Color = "info"
                },
                new RecentActivity
                {
                    ActivityType = "Report Generated",
                    Description = "Monthly enrollment report created",
                    Timestamp = DateTime.Now.AddDays(-2),
                    Icon = "fas fa-chart-bar",
                    Color = "primary"
                }
            };

            return activities.OrderByDescending(a => a.Timestamp).Take(5).ToList();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}