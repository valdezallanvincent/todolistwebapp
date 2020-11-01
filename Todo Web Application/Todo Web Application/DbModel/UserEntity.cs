using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.DbModel
{
    public class UserEntity : BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
