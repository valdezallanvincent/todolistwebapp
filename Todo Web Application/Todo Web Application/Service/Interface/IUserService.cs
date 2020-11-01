using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;

namespace TodoWebApplication.Service.Interface
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetUserList(ClaimsPrincipal principal);
        public Task<long> AddUser(UserRequestDto requestDto);
    }
}
