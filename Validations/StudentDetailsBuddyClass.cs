using System.ComponentModel.DataAnnotations;

namespace University_Portal.Validations
{
    public class StudentDetailsBuddyClass
    {
        [Display(Name = "Student ID")]
        public int SId { get; set; }
        [Display(Name = "Student Name")]
        public string? SName { get; set; }
        [Display(Name = "Student Level")]
        public int SLevel { get; set; }

        [Display(Name = "Course Name")]
        public string? CName { get; set; }

        [Display(Name = "Course Credits")]
        public int CCredits { get; set; }
    }
}
