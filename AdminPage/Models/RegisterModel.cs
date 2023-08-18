using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminPage.Models
{
    public class RegisterModel
    {
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Required(ErrorMessage = "Enter an Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repeat password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
//идентификатор, именем, мылом, датой регистрации, датой последнего логина, статусом