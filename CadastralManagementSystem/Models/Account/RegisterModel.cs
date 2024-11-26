using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CadastralManagement.Models.Account
{
    public class RegisterModel : LoginModel
    {
        [DisplayName("Подтверждение пароля")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public required string PasswordConfirm { get; set; }
    }
}