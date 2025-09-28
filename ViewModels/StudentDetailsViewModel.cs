using Microsoft.AspNetCore.Mvc;

namespace University_Portal.ViewModels
{

    [ModelMetadataType(typeof(Validations.StudentDetailsBuddyClass))]
    public class StudentDetailsViewModel
    {
        public int SId { get; set; }
        public string? SName { get; set; }
        public int SAge { get; set; }
        public int SLevel { get; set; }
        public string? SImage { get; set; }
        public string? CName { get; set; }
        public int CCredits { get; set; }
        
    }
}
