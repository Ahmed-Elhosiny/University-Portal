// Validations/ImageValidationAttribute.cs
using System.ComponentModel.DataAnnotations;

namespace University_Portal.Validations
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        private readonly string[] _validExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB

        public override bool IsValid(object? value)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    ErrorMessage = "File size cannot exceed 5MB.";
                    return false;
                }

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_validExtensions.Contains(extension))
                {
                    ErrorMessage = "Only JPG, PNG, and GIF files are allowed.";
                    return false;
                }
            }

            return true;
        }
    }
}