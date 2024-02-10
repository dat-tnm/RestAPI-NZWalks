using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalksDbContext _nZWalksDb;

        public UserRepository(NZWalksDbContext nZWalksDb)
        {
            _nZWalksDb = nZWalksDb;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var userFromDb = await _nZWalksDb.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

            if (userFromDb == null)
            {
                return null;
            }

            var userRoles = await _nZWalksDb.User_Roles
                .Where(x => x.UserId == userFromDb.Id)
                .ToListAsync(); ;

            if (userRoles.Any())
            {
                foreach (var userRole in userRoles)
                {
                    var role = await _nZWalksDb.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        userFromDb.Roles.Add(role.Name);
                    }
                }
            }

            userFromDb.Password = null;
            return userFromDb;
        }
    }
}
