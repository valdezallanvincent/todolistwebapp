using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoWebApplication.Service.DtoModel.RequestDto;
using TodoWebApplication.Service.Interface;

namespace TodoWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        ITodoTransactionService _todoListService;
        public TodoController(ITodoTransactionService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpGet]
        [Route("gettodolist")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> GetTodoList()
        {
            var result = await _todoListService.GetTodoLists(User);

            return Ok(result);
        }

        [HttpPost]
        [Route("addtodotransaction")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> AddTodoTransaction(TodoAddRequestDto requestDto)
        {
            var result = await _todoListService.AddTodoTransaction(requestDto, User);

            return Ok(result);
        }

        [HttpPut]
        [Route("completetodotransaction")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> CompleteTodoTransaction([FromQuery]long todoTransactionId)
        {
            var result = await _todoListService.CompleteTodoTransaction(todoTransactionId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("clearcompletedtodotransaction")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> ClearCompletedTodoTransaction()
        {
            var result = await _todoListService.ClearCompletedTodoTransaction(User);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("deletetodotransaction")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> DeleteTodoTransaction([FromQuery]long todoTransactionId)
        {
            var result = await _todoListService.DeleteTodoTransaction(todoTransactionId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
