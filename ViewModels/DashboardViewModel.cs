namespace University_Portal.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalCourses { get; set; }
        public int TotalEnrollments { get; set; }
        public double AverageStudentsPerCourse { get; set; }

        public List<CourseEnrollmentStats> CourseStats { get; set; } = new();
        public List<LevelDistribution> LevelStats { get; set; } = new();
        public List<RecentActivity> RecentActivities { get; set; } = new();

        public int StudentsThisMonth { get; set; }
        public int CoursesThisMonth { get; set; }
        public string MostPopularCourse { get; set; } = "";
        public int MostPopularCourseCount { get; set; }
    }

    public class CourseEnrollmentStats
    {
        public string CourseName { get; set; } = "";
        public int StudentCount { get; set; }
        public int Credits { get; set; }
    }

    public class LevelDistribution
    {
        public int Level { get; set; }
        public int StudentCount { get; set; }
        public double Percentage { get; set; }
    }

    public class RecentActivity
    {
        public string ActivityType { get; set; } = ""; // "Student Added", "Course Created", etc.
        public string Description { get; set; } = "";
        public DateTime Timestamp { get; set; }
        public string Icon { get; set; } = "";
        public string Color { get; set; } = "";
    }
}