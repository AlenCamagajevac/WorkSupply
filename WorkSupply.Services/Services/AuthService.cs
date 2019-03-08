using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WorkSupply.Core.DTOs.Auth;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Settings;
using WorkSupply.Core.Models.Token;
using WorkSupply.Core.Service;

namespace WorkSupply.Services.Services
{
    public class AuthService : IAuthService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<AuthService>();
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<JwtSettings> jwtSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (createResult.Succeeded) return true;
            Log.Information($"Could not create user: {createResult.Errors.ToList()}");
            return false;
        }

        public async Task<bool> AssignUserToRoleAsync(string userId, string roleId)
        {   
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                Log.Warning($"Trying to add user: {userId} into non-existing role: {roleId}");
                throw new EntityNotFoundException($"Role does not exist");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Log.Warning($"Trying to add non-existing user: {userId} into role: {roleId}");
                throw new EntityNotFoundException($"User does not exist");
            }

            if ((await _userManager.GetRolesAsync(user)).Count > 0)
            {
                Log.Warning($"Trying to add user: {userId} with assigned role into another role: {roleId}");
                throw new UserAlreadyInRoleException("User already has assigned role");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, await _roleManager.GetRoleNameAsync(role));
            return addToRoleResult.Succeeded;
        }

        public async Task<Jwt> CreateJwtToken(string email, string password)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: await ConstructUserClaimsAsync(email, password),
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: signingCredentials
            );

            return new Jwt
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Expiration = jwtSecurityToken.ValidTo
            };
        }
        
        private async Task<List<Claim>> ConstructUserClaimsAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if (!result.Succeeded) 
                throw new CouldNotLogInUserException("Could not sign in user");
            var user = await _userManager.FindByEmailAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);

            //Create user claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, email)
            };
                
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }

        // TODO: use this maybe
        private async Task<bool> RemoveUser(ApplicationUser user)
        {
            var deleteResult = await _userManager.DeleteAsync(user);
            return deleteResult.Succeeded;
        }
    }
}