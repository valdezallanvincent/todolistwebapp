using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;
using TodoWebApplication.Repository;
using TodoWebApplication.Service.DtoModel;
using TodoWebApplication.Service.DtoModel.RequestDto;
using TodoWebApplication.Service.Interface;

namespace TodoWebApplication.Service
{
    public class TodoTransactionService : ITodoTransactionService
    {
        IUnitOfWork _unitOfWork;
        public TodoTransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TodoListDto>> GetTodoLists(ClaimsPrincipal principal)
        {
            var userIdString = principal?.Claims?.FirstOrDefault(c => c.Type == "user_id")?.Value;
            var todoListDto = new List<TodoListDto>();

            var userId = long.Parse(userIdString);
            var todoListEntity = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<TodoTransactionEntity>(x => x.UserId == userId, e => e.User);
            todoListEntity = todoListEntity.OrderByDescending(x => x.CreatedDate).ToList();
            //automapper can be used
            foreach (var todo in todoListEntity)
            {
                var todoDto = new TodoListDto(todo);
                todoListDto.Add(todoDto);
            }
            return todoListDto;
        }

        public async Task<long> AddTodoTransaction(TodoAddRequestDto request, ClaimsPrincipal principal)
        {
            var userIdString = principal?.Claims?.FirstOrDefault(c => c.Type == "user_id")?.Value;

            var todoTransactionEntity = new TodoTransactionEntity();
            todoTransactionEntity.Description = request.Description;
            todoTransactionEntity.UserId = long.Parse(userIdString);
            todoTransactionEntity.CreatedDate = DateTime.Now;

            _unitOfWork.Add(todoTransactionEntity);
            await _unitOfWork.SaveChanges();
            return todoTransactionEntity.Id;
        }

        public async Task<bool> CompleteTodoTransaction(long todoTransactionid)
        {
            var todoTransactions = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<TodoTransactionEntity>(x => x.Id == todoTransactionid);

            if (todoTransactions.Count() == 0)
            {
                throw new Exception($"{nameof(todoTransactionid)} {todoTransactionid} not found");
            }
            var todoTransaction = todoTransactions.FirstOrDefault();
            todoTransaction.IsCompleted = !todoTransaction.IsCompleted;
            if (todoTransaction.IsCompleted)
            {
                todoTransaction.CompletedDate = DateTime.Now;
            }
            else
            {
                todoTransaction.CompletedDate = null;
            }
            await _unitOfWork.SaveChanges();
            return true;
        }
        public async Task<bool> ClearCompletedTodoTransaction(ClaimsPrincipal principal)
        {
            var userIdString = principal?.Claims?.FirstOrDefault(c => c.Type == "user_id")?.Value;

            var todoTransactions = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<TodoTransactionEntity>(x => x.IsCompleted && x.UserId == long.Parse(userIdString));
            if (todoTransactions.Count > 0)
            {
                _unitOfWork.RemoveRange<TodoTransactionEntity>(todoTransactions);

                await _unitOfWork.SaveChanges();
            }
            return true;
        }

        public async Task<bool> DeleteTodoTransaction(long todoTransactionid)
        {

            var todoTransactions = await _unitOfWork.FindAsyncByPredicateWithIncludeProperty<TodoTransactionEntity>(x => x.Id == todoTransactionid);

            if (todoTransactions.Count() == 0)
            {
                throw new Exception($"{nameof(todoTransactionid)} {todoTransactionid} not found");
            }

            var todoTransaction = todoTransactions.FirstOrDefault();
            _unitOfWork.Remove<TodoTransactionEntity>(todoTransaction);

            await _unitOfWork.SaveChanges();

            return true;
        }
    }
}
