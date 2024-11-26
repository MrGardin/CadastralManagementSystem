using CadastralManagement.Data;
using CadastralManagement.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CadastralManagement.Controllers
{
    public class AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : Controller
    {
        // GET: Account/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IdentityUser user = new() { UserName = model.Name };

            var result = userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult(); // нет синхронного метода Create, поэтому используем Awaiter, чтобы получить результат

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description); // добавляем ошибки, которые появятся через asp-validation-summary
                }

                return View(model);
            }

            userManager.AddToRoleAsync(user, UsersInitializer.CLIENT_ROLE).Wait();
            signInManager.SignInAsync(user, false).Wait(); // нет синхронного метода SignIn, поэтому используем Wait, чтобы дождаться выполнения функции

            return RedirectToAction("Index", "CadastralObjects");
        }

        // GET: Account/Login
        public IActionResult Login(string returnUrl = "~/CadastralObjects")
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        // POST: Account/Register
        [HttpPost]
        public IActionResult Login(LoginModel model, string returnUrl = "~/CadastralObjects")
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = signInManager.PasswordSignInAsync(
                model.Name,
                model.Password,
                model.RememberMe, // сохранить входные данные в куки, если стоит соответствующая галочка
                false // не блокировать пользователя при сбое входа
            )
                .GetAwaiter().GetResult(); // нет синхронного метода PasswordSignIn, поэтому используем Awaiter, чтобы получить результат

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");

                return View(model);
            }

            return RedirectToLocal(returnUrl);
        }

        // GET: Account/Login
        public IActionResult Logout()
        {
            return View();
        }

        // POST: Account/Logout
        [Authorize]
        [HttpPost]
        [ActionName("Logout")]
        public IActionResult LogoutConfirmed()
        {
            signInManager.SignOutAsync().Wait(); // выходим из аккаунта и чистим входные данные в куки
            return RedirectToAction("Index", "CadastralObjects");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "CadastralObjects");
            }
        }
    }

    /* настройка сообщений об ошибках валидации из Identity */
    public class UserValidationErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Description = $"Пользователь {userName} уже существует" }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Description = $"Пароль должен состоять не менее чем из {length} символов" }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Description = "Пароль должен содержать хотя бы один спец. символ" }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Description = "Пароль должен содержать хотя бы одну цифру" }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Description = "Пароль должен содержать хотя бы один символ в нижнем регистре" }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Description = "Пароль должен содержать хотя бы один символ в вернем регистре" }; }
    }
}