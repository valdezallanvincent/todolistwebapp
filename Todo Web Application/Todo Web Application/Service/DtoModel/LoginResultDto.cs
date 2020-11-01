using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;

namespace TodoWebApplication.Service.DtoModel
{
    public class LoginResultDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
        public bool IsSuccessfulLogin { get; set; }
        public LoginResultDto()
        {
            IsSuccessfulLogin = false;
        }
        public LoginResultDto(UserEntity user)
        {
            UserId = user.Id;
            FullName = user.FullName;
            UserName = user.UserName;
            IsSuccessfulLogin = true;
            UserRoles = new List<UserRoleDto>();
            foreach (var item in user.UserRoles)
            {
                UserRoles.Add(new UserRoleDto(item));
            }
        }
    }
}
