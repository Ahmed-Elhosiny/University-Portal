using University_Portal.Models;
using Microsoft.EntityFrameworkCore;

namespace University_Portal.Data
{
    public class DatabaseSeeder
    {
        private readonly UniversityMvcContext _context;

        public DatabaseSeeder(UniversityMvcContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            // Check if database is already seeded
            if (await _context.Courses.AnyAsync() || await _context.Students.AnyAsync())
            {
                return; // Database already has data
            }

            // Seed Courses first
            await SeedCourses();

            // Then seed Students
            await SeedStudents();
        }

        private async Task SeedCourses()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Name = "Computer Science",
                    Credits = 4
                },
                new Course
                {
                    Name = "Business Administration",
                    Credits = 3
                },
                new Course
                {
                    Name = "Mechanical Engineering",
                    Credits = 5
                },
                new Course
                {
                    Name = "Medicine",
                    Credits = 6
                },
                new Course
                {
                    Name = "Civil Engineering",
                    Credits = 5
                },
                new Course
                {
                    Name = "Psychology",
                    Credits = 3
                },
                new Course
                {
                    Name = "Electrical Engineering",
                    Credits = 4
                },
                new Course
                {
                    Name = "Architecture",
                    Credits = 4
                },
                new Course
                {
                    Name = "Pharmacy",
                    Credits = 5
                },
                new Course
                {
                    Name = "Law",
                    Credits = 4
                },
                new Course
                {
                    Name = "Economics",
                    Credits = 3
                },
                new Course
                {
                    Name = "Graphic Design",
                    Credits = 3
                },
                new Course
                {
                    Name = "Data Science",
                    Credits = 4
                },
                new Course
                {
                    Name = "Marketing",
                    Credits = 3
                },
                new Course
                {
                    Name = "Biology",
                    Credits = 4
                }
            };

            _context.Courses.AddRange(courses);
            await _context.SaveChangesAsync();
        }

        private async Task SeedStudents()
        {
            // Get all courses
            var courses = await _context.Courses.ToListAsync();
            if (!courses.Any()) return;

            var random = new Random();

            // Realistic student names (diverse international names)
            var firstNames = new[]
            {
                "Ahmed", "Sarah", "Mohamed", "Fatima", "Omar", "Layla", "Youssef", "Noor",
                "Hassan", "Amira", "Ali", "Zainab", "Khaled", "Salma", "Adam", "Mariam",
                "Ibrahim", "Yasmin", "Mahmoud", "Hana", "Tariq", "Leila", "Rami", "Dina",
                "Karim", "Sofia", "Fadi", "Maya", "Walid", "Nadia", "Sami", "Rana",
                "Basel", "Lina", "Hamza", "Jana", "Adel", "Reem", "Tarek", "Hiba",
                "Majed", "Noura", "Rashid", "Dalia", "Saeed", "Mona", "Nabil", "Aisha",
                "Fahad", "Samira", "Ziad", "Laila", "Jamal", "Rania", "Fares", "Sana"
            };

            var lastNames = new[]
            {
                "Abdullah", "Hassan", "Ali", "Ibrahim", "Mahmoud", "Ahmed", "Mohamed",
                "Khalil", "Saleh", "Omar", "Youssef", "Mansour", "Rashid", "Salem",
                "Nasser", "Farid", "Hamdan", "Sabri", "Kamal", "Fouad", "Samir",
                "Mustafa", "Adnan", "Tariq", "Waleed", "Nasr", "Bakr", "Haddad"
            };

            var students = new List<Student>();

            // Generate 50 diverse students
            for (int i = 0; i < 50; i++)
            {
                var firstName = firstNames[random.Next(firstNames.Length)];
                var lastName = lastNames[random.Next(lastNames.Length)];
                var fullName = $"{firstName} {lastName}";

                // Ensure unique names
                while (students.Any(s => s.Name == fullName))
                {
                    firstName = firstNames[random.Next(firstNames.Length)];
                    lastName = lastNames[random.Next(lastNames.Length)];
                    fullName = $"{firstName} {lastName}";
                }

                var student = new Student
                {
                    Name = fullName,
                    Age = random.Next(18, 35), // Ages between 18-34
                    Level = random.Next(1, 6), // Levels 1-5
                    CourseId = courses[random.Next(courses.Count)].Id,
                    Image = "default.png" // We'll use default images for demo
                };

                students.Add(student);
            }

            _context.Students.AddRange(students);
            await _context.SaveChangesAsync();
        }

        // Method to clear all data (useful for testing)
        public async Task ClearAllDataAsync()
        {
            _context.Students.RemoveRange(_context.Students);
            _context.Courses.RemoveRange(_context.Courses);
            await _context.SaveChangesAsync();
        }

        // Method to reset and reseed (useful for demos)
        public async Task ResetAndReseedAsync()
        {
            await ClearAllDataAsync();
            await SeedAsync();
        }
    }
}