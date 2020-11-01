using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.DbModel
{
    public class UserRoleEntity : BaseEntity
    {
        public long UserId { get; set; }
        public UserEntity User { get; set; }
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
