using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.Service.DtoModel.RequestDto;
using TodoWebApplication.Service.Interface;

namespace TodoWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("getuserlist")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetUserList()
        {
            var result = await _userService.GetUserList(User);

            return Ok(result);
        }

        [HttpPost]
        [Route("adduser")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddUser(UserRequestDto requestDto)
        {
            var result = await _userService.AddUser(requestDto);

            return Ok(result);
        }
    }
}
