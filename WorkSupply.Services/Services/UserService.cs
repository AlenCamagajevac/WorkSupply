using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSupply.Core.Exceptions;
using WorkSupply.Core.Models.AppUser;
using WorkSupply.Core.Models.Pagination;
using WorkSupply.Core.Query;
using WorkSupply.Core.Service;

namespace WorkSupply.Services.Services
{
    public class UserService : IUserService
    {
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<UserService>();

        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PaginatedList<ApplicationUser>> GetUsersAsync(UsersQuery usersQuery)
        {
            IQueryable<ApplicationUser> users;
            if (usersQuery.Role.HasValue)
            {
                users = (await _userManager.GetUsersInRoleAsync(ApplicationRole.GetRoleName(usersQuery.Role.Value)))
                    .AsQueryable();
            }
            else
            {
                users = _userManager.Users;
            }

            if (!string.IsNullOrEmpty(usersQuery.Name))
            {
                users = from u in users 
                    where EF.Functions.Like(u.Name, $"%{usersQuery.Name}%") 
                    select u;
            }
            
            return PaginatedList<ApplicationUser>.Create(users, usersQuery.Page ?? 1, 20);
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null) return user;
            
            Log.Warning("Trying to get non existing user with id: {userId}", userId);
            throw new EntityNotFoundException($"Could not find user with id: {userId}");
        }
    }
}