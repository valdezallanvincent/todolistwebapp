using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;
using TodoWebApplication.Exceptions;
using TodoWebApplication.Repository;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;
using TodoWebApplication.Service.Helper;
using TodoWebApplication.Service.Interface;

namespace TodoWebApplication.Service
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        ICryptographer _cryptographer;
        public UserService(IUnitOfWork unitOfWork, ICryptographer cryptographer)
        {
            _unitOfWork = unitOfWork;
            _cryptographer = cryptographer;
        }

        public async Task<List<UserDto>> GetUserList(ClaimsPrincipal principal)
        {
            var userDtos = new List<UserDto>();
            var userEntities = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<UserEntity>(null, e => e.UserRoles);

            //automapper can be used
            foreach (var user in userEntities)
            {
                var userDto = new UserDto(user);
                userDtos.Add(userDto);
            }
            return userDtos;
        }
        public async Task<long> AddUser(UserRequestDto requestDto)
        {
            var user = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<UserEntity>(x => x.UserName == requestDto.UserName);

            if (user.Count > 0)
            {
                throw new ConflictException($"{requestDto.UserName} user name already exists");
            }
            var userRoles = new List<UserRoleEntity>() { new UserRoleEntity() { RoleId = 2 } };

            var userEntity = new UserEntity();
            userEntity.UserName = requestDto.UserName;
            userEntity.FullName = requestDto.FullName;
            userEntity.UserRoles = userRoles;
            userEntity.Password = _cryptographer.Encrypt(requestDto.Password);

            _unitOfWork.Add<UserEntity>(userEntity);
            await _unitOfWork.SaveChanges();
            return userEntity.Id;
        }
    }
}
