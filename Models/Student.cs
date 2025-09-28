using Microsoft.AspNetCore.Mvc;

namespace University_Portal.Models
{

    [ModelMetadataType(typeof(Validations.StudentBuddyClass))]
    public partial class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string? Image { get; set; } = "default.png";

        public int Level { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; } = null!;
    }

}
