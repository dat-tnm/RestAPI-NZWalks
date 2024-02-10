using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class User_Role
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }

        //navigation
        [NotMapped]
        public User User { get; set; }
        [NotMapped]
        public Role Role { get; set; }
    }
}
