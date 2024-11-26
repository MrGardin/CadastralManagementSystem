using CadastralManagement.Controllers;
using CadastralManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/* Регистрация и настройка сервисов */

builder.Services.AddDbContext<CadastralManagementContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultSqliteConnection")) // берём настройки из appsettings.json
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>() // настройка типов пользователей и ролей
    .AddEntityFrameworkStores<CadastralManagementContext>() // храним пользователей в базе данных
    .AddErrorDescriber<UserValidationErrorDescriber>(); // настройка сообщений об ошибках валидации из Identity

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options => // настройка базовых сообщений об ошибках валидации
    {
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => $"Некорректное значение поля\"{y}\".");
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => $"Поле пустое");
    });

/* Настройка веб-приложения */

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Ho/Error");
}

app.UseStaticFiles(); // по умолчанию статические файлы обслуживаются только в корневом веб-каталоге wwwroot

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute( // стандартная маршрутизация через имена контроллеров и действия
    name: "default",
    pattern: "{controller=CadastralObjects}/{action=Index}/{id?}"
);

/* Инициализация начальных ролей и пользователей */
// https://metanit.com/sharp/aspnet5/16.12.php

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    UsersInitializer.Initialize(roleManager, userManager);
}

/* Запуск */

app.Run();