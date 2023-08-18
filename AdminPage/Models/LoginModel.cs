using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter an Email")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Display(Description = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        [Display(Description = "Password")]
        public string Password { get; set; }
    }
}