using System.ComponentModel.DataAnnotations;

namespace BookAddressProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "اسم المستخدم")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "غير متشابهين")]
        [Display(Name = "Enter the password again")]
        public string? ConfirmPassword { get; set; }
    }
}
