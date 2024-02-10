using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Reflection.Emit;

namespace NZWalks.API.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly NZWalksDbContext _db;

        public DbInitializer(NZWalksDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                //var affectedRows = _db.Database.ExecuteSqlRaw($"");

                if (!_db.Users.Any() && !_db.Roles.Any())
                {
                    var userList = new List<User>
                    {
                        new User { Id = new Guid(), Username = "ChrisF", FirstName = "Chris", LastName = "F", Email = "chris.f@nzwalk.com", Password = "dat123456" },
                        new User { Id = new Guid(), Username = "ChrisP", FirstName = "Chris", LastName = "P", Email = "chris.p@nzwalk.com", Password = "dat123456" }
                    };
                    _db.Users.AddRange(userList);

                    var roleList = new List<Role>
                    {
                        new Role { Id = new Guid(), Name = "Reader" },
                        new Role { Id = new Guid(), Name = "Writer" }
                    };
                    _db.Roles.AddRange(roleList);
                    _db.SaveChanges();

                    var user_RoleList = new List<User_Role>{
                        new User_Role { Id = new Guid(), UserId = userList[0].Id, RoleId = roleList[0].Id },
                        new User_Role { Id = new Guid(), UserId = userList[1].Id, RoleId = roleList[1].Id }
                    };
                    _db.User_Roles.AddRange(user_RoleList);
                    _db.SaveChanges();
                }


                
            }
            catch (Exception)
            { }
        }
    }
}
