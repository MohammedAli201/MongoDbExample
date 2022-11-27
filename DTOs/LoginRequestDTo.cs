using System.ComponentModel.DataAnnotations;

namespace MongoDbExample.DTOs
{
    public class LoginRequestDTo
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password must be a string and is required"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}