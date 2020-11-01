using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;

namespace TodoWebApplication.Service.DtoModel
{
    public class RoleDto
    {
        public string Role { get; set; }
        public RoleDto(RoleEntity role)
        {
            Role = role.Role;
        }
    }
}
