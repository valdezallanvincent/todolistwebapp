using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;

namespace TodoWebApplication.Service.DtoModel
{
    public class UserDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public UserDto(UserEntity userEntity)
        {
            FullName = userEntity.FullName;
            UserName = userEntity.UserName;
        }
    }
}
