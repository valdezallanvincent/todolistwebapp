using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataAccess.Entities
{
    public class UserRoleEntity : BaseEntity
    {
        public long UserId { get; set; }
        public UserEntity User { get; set; }
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }
    }
}
