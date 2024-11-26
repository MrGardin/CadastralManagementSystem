using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CadastralManagement.Models.Account
{
    public class AccountModel
    {
        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        public required string Name { get; set; }

        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле \"{0}\" обязательно")]
        public required string Password { get; set; }
    }
}
