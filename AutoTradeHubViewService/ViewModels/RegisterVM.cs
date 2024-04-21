using System.ComponentModel.DataAnnotations;

namespace AutoTradeHubViewService.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Введите электронную почту!")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Подтвердите пароль!")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string? ConfirmPassword { get; set; }
    }
}
