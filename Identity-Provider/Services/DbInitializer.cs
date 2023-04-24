using IdentityModel;
using IdentityProvider.DbContexts;
using IdentityProvider.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityProvider.Services
{
    public class DbInitializer : IDbInitializer
    {

        private readonly IdentityDataContext _db;
        private readonly UserManager<IdentityUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(IdentityDataContext db, UserManager<IdentityUserModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync(Config.Admin).Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole(Config.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Config.Customer)).GetAwaiter().GetResult();
            }
            else { return; }

            IdentityUserModel adminUser = new()
            {
                UserName = "admin1@gmail.com",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                Name = "Ben Admin"
            };

            _userManager.CreateAsync(adminUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, Config.Admin).GetAwaiter().GetResult();

            var claims1 = _userManager.AddClaimsAsync(adminUser, new Claim[] {
                new Claim(JwtClaimTypes.Name,adminUser.Name),
                new Claim(JwtClaimTypes.Role,Config.Admin)
            }).Result;



            IdentityUserModel customerUser = new()
            {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                Name = "Ben Customer"
            };

            _userManager.CreateAsync(customerUser, "Admin123*").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, Config.Customer).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[] {
                 new Claim(JwtClaimTypes.Name,customerUser.Name),
                new Claim(JwtClaimTypes.Role,Config.Customer),
            }).Result;
        }
    }
}
