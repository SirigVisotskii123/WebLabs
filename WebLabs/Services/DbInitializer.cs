using Microsoft.AspNetCore.Identity;
using WebLabs.DAL.Data;
using WebLabs.DAL.Entities;

namespace WebLabs.Services
{
    public class DbInitializer
    {
        public static async Task SetupDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = (ApplicationDbContext?)scope.ServiceProvider.GetService(typeof(ApplicationDbContext));
            var userManager = (UserManager<ApplicationUser>?)scope.ServiceProvider.GetService(typeof(UserManager<ApplicationUser>));
            var roleManager = (RoleManager<IdentityRole>?)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));

            await Seed(context, userManager, roleManager);

        }
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // создать БД, если она еще не создана
            context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }
            // проверка наличия пользователей
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }
        }
    }
}
