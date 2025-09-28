// Validations/CourseBuddyClass.cs
using System.ComponentModel.DataAnnotations;

namespace University_Portal.Validations
{
    public class CourseBuddyClass
    {
        [Display(Name = "Course ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Course name must be between 3 and 100 characters")]
        [Display(Name = "Course Name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Credits are required")]
        [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6")]
        [Display(Name = "Credit Hours")]
        public int Credits { get; set; }
    }
}