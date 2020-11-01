using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;
using TodoWebApplication.Repository;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;

namespace TodoWebApplication.Service.Interface
{
    public interface IAuthenticationLoginService
    {
        Task<LoginResultDto> Login(LoginRequestDto requestDto);
    }
}
