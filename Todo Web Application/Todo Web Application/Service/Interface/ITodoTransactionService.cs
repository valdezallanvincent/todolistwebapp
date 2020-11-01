using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;

namespace TodoWebApplication.Service.Interface
{
    public interface ITodoTransactionService
    {
        public Task<List<TodoListDto>> GetTodoLists(ClaimsPrincipal principal);
        public Task<long> AddTodoTransaction(TodoAddRequestDto requestDto, ClaimsPrincipal principal);
        public Task<bool> CompleteTodoTransaction(long todoTransactionid);
        public Task<bool> ClearCompletedTodoTransaction(ClaimsPrincipal principal);
        public Task<bool> DeleteTodoTransaction(long todoTransactionid);
    }
}
