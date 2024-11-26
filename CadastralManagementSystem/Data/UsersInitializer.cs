using Microsoft.AspNetCore.Identity;

namespace CadastralManagement.Data
{
    public class UsersInitializer
    {
        public const string CLIENT_ROLE = "Client";
        public const string ADMIN_ROLE = "Administrator";

        //Класс админа и гостя

        public static void Initialize(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            const string adminName = "admin";
            const string adminPassword = "qwerY1!";

            CreateRoleIfNotExist(roleManager, CLIENT_ROLE);
            CreateRoleIfNotExist(roleManager, ADMIN_ROLE);

            CreateUserIfNotExist(userManager, adminName, adminPassword, ADMIN_ROLE);
        }

        private static void CreateRoleIfNotExist(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult() == null) // для Awaiter и Wait см. комментарии в AccountController
            {
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
            }
        }

        private static void CreateUserIfNotExist(UserManager<IdentityUser> userManager, string userName, string userPassword, string userRole)
        {
            if (userManager.FindByNameAsync(userName).GetAwaiter().GetResult() != null) // для Awaiter и Wait см. комментарии в AccountController
            {
                return;
            }

            IdentityUser user = new IdentityUser { UserName = userName };

            var result = userManager.CreateAsync(user, userPassword).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, userRole).Wait();
            }
        }
    }
}