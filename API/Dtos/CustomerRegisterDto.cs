using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long. Up to 32 characters max.")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }
}