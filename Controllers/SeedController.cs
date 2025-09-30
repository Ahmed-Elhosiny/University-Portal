using Microsoft.AspNetCore.Mvc;
using University_Portal.Data;
using University_Portal.Models;

namespace University_Portal.Controllers
{
    public class SeedController : Controller
    {
        private readonly UniversityMvcContext _context;

        public SeedController(UniversityMvcContext context)
        {
            _context = context;
        }

        // GET: /Seed/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Seed/SeedDatabase
        [HttpPost]
        public async Task<IActionResult> SeedDatabase()
        {
            try
            {
                var seeder = new DatabaseSeeder(_context);
                await seeder.SeedAsync();
                TempData["SuccessMessage"] = "Database seeded successfully with sample data!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error seeding database: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // POST: /Seed/ClearData
        [HttpPost]
        public async Task<IActionResult> ClearData()
        {
            try
            {
                var seeder = new DatabaseSeeder(_context);
                await seeder.ClearAllDataAsync();
                TempData["SuccessMessage"] = "All data cleared successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error clearing data: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // POST: /Seed/ResetDatabase
        [HttpPost]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                var seeder = new DatabaseSeeder(_context);
                await seeder.ResetAndReseedAsync();
                TempData["SuccessMessage"] = "Database reset and reseeded successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error resetting database: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}