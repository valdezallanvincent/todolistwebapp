using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;
using TodoWebApplication.Repository;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;
using TodoWebApplication.Service.Interface;

namespace TodoWebApplication.Service
{
    public class AuthenticationLoginService : IAuthenticationLoginService
    {
        private IUnitOfWork _unitOfWork;
        public AuthenticationLoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LoginResultDto> Login(LoginRequestDto requestDto)
        {
            var users = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<UserEntity>(x => x.UserName == requestDto.UserName && x.Password == requestDto.Password);
            if (users.Count == 0)
            {
                return new LoginResultDto();
            }
            var user = users.FirstOrDefault();
            var userRoles = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<UserRoleEntity>(x => x.UserId == user.Id, e => e.Role, e => e.User);
            user.UserRoles = userRoles;

            var loginResult = new LoginResultDto(user);
            return loginResult;
        }
    }
}
