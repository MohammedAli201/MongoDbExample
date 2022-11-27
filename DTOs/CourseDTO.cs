using System.ComponentModel.DataAnnotations;

namespace MongoDbExample.DTOs
{
    public class CourseDTO
    {
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required")]
        public string Code { get; set; } = string.Empty;
    }
}