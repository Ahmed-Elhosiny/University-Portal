using University_Portal.Models;


namespace University_Portal.ViewModels
{
    public class SearchViewModel
    {
        public string? SearchTerm { get; set; }
        public int? SelectedCourseId { get; set; }
        public int? MinLevel { get; set; }
        public int? MaxLevel { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string SortBy { get; set; } = "Name";
        public string SortOrder { get; set; } = "asc";

        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();

        public int TotalCount { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}