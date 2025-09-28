using Microsoft.AspNetCore.Mvc;


namespace University_Portal.Models
{

    [ModelMetadataType(typeof(Validations.CourseBuddyClass))]
    public partial class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Credits { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}