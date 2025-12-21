using Microsoft.AspNetCore.Identity;
using OnlineSinavPortal.Models;

namespace OnlineSinavPortal.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Veritabanı oluşturulmuş mu kontrol et
            if (!context.Database.CanConnect())
            {
                return;
            }

            // Rolleri oluştur
            string[] roles = { "Admin", "User" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Varsayılan admin kullanıcısı oluştur
            var adminEmail = "admin@onlinesinav.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    AdSoyad = "Sistem Yöneticisi",
                    OgrenciNumarasi = "12345678",
                    Aktif = true,
                    OlusturmaTarihi = DateTime.Now
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else
            {
                // Kullanıcı varsa admin rolünü ekle (yoksa)
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
