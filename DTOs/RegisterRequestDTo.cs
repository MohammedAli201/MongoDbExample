using System.ComponentModel.DataAnnotations;

namespace MongoDbExample.DTOs
{
    public class RegisterRequestDTo
    {

        // [Required]
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        // [Required]
        public string Password { get; set; } = string.Empty;
        // [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Password do not match")]
        // public string ConfirmPassword { get; set; } = string.Empty;
    }
}