using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using University_Portal.Models;

namespace University_Portal.Validations
{
    public class StudentBuddyClass
    {

        [Display(Name = "Student ID")]
        public int Id { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Should be between (3,50) characters")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(10, 75)]
        public int Age { get; set; }

 

        [Required]
        [Range(1, 5)]
        public int Level { get; set; }

        [Display(Name = "Course ID")]
        public int CourseId { get; set; }

        [ValidateNever]
        public virtual Course Course { get; set; } = null!;
    }
}
