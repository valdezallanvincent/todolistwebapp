using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;

namespace TodoWebApplication.Service.DtoModel
{
    public class UserRoleDto
    {
        public long UserId { get; set; }
        public UserDto User  { get; set; }
        public long RoleId { get; set; }
        public RoleDto Role { get; set; }
        public UserRoleDto(UserRoleEntity userRole)
        {
            UserId = userRole.UserId;
            RoleId = userRole.RoleId;
            User = new UserDto(userRole.User);
            Role = new RoleDto(userRole.Role);
        }
    }
}
