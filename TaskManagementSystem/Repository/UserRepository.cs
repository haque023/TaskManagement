using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.IRepository;
using TaskManagementSystem.ViewModel;

namespace TaskManagementSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManger = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        public async Task<object> LoginUserAsync(LoginDTO model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
                return new
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };


            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var claims = new List<Claim>
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var userRoles = await _userManger.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Auth:Issuer"],
                audience: _configuration["Auth:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };

        }
        public async Task<object> RegisterUserAsync(UserRegisterDTO model)
        {
            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));

                if (await _roleManager.RoleExistsAsync(model.Role))
                    await _userManger.AddToRoleAsync(identityUser, model.Role);

                return new { Message = "User Create Successfully", Code = 200 };
            }
            else
            {
                return new { Message = "User Not Create", Code = 200 };
            }

        }
    }
}
