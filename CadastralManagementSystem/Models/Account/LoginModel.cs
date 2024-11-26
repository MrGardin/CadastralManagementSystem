using System.ComponentModel;

namespace CadastralManagement.Models.Account
{
    public class LoginModel : AccountModel
    {
        [DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}