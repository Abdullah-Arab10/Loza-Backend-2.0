using AutoMapper;
using Identity.Provider.DTOs;
using IdentityProvider.DbContexts;
using IdentityProvider.DTOs;
using IdentityProvider.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityProvider.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUserModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        private readonly IMapper _mapper;
        private readonly IdentityDataContext _db;

        public UserService(IdentityDataContext db, IConfiguration configuration,
            UserManager<IdentityUserModel> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _roleManager = roleManager;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.Users
                .SingleOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());



            if (user == null) return null;

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (isValidPassword == false)
            {
                return new LoginResponseDTO()
                {
                    Token = null,
                    User = null
                };
            }

            //if user was found generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);


            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = new UserDTO(user.Id, user.Email, user.Name, user.UserName),

            };
            return loginResponseDTO;
        }

        public async Task<UserDTO> Register(RegisterRequestDTO registerationRequestDTO)
        {

            IdentityUserModel user = new()
            {
                Email = registerationRequestDTO.Email,
                NormalizedEmail = registerationRequestDTO.Email.ToUpper(),
                Name = registerationRequestDTO.Name,
                UserName = registerationRequestDTO.Email

            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("customer"));
                    }
                    var res = _userManager.AddToRoleAsync(user, Config.Customer).GetAwaiter().GetResult();


                    var userToReturn = _db.Users
                        .FirstOrDefault(u => u.Email == registerationRequestDTO.Email);

                    //Todo : check automapper problem
                    return new UserDTO(userToReturn.Id,userToReturn.Email,userToReturn.Name,userToReturn.UserName);

                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return new UserDTO();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync() > 0);
        
       }

        public async Task<bool> DeleteIdentityUser(string id)
        {
            var user =await _userManager.FindByIdAsync(id);

            if( user is not null)
            {
                var result = await _userManager.DeleteAsync(user);

                if( result.Succeeded )
                {
                    // Delete any related data from the database
                    var userClaims = await _userManager.GetClaimsAsync(user);
                    foreach (var claim in userClaims)
                    {
                        var claimEntity = _db.UserClaims.FirstOrDefault(c => c.UserId == user.Id && c.ClaimType == claim.Type && c.ClaimValue == claim.Value);
                        if (claimEntity != null)
                        {
                            _db.UserClaims.Remove(claimEntity);
                        }
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
