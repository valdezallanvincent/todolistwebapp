using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;

namespace TodoWebApplication.Service.DtoModel
{
    public class TodoListDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UserId { get; set; }
        public UserDto User { get; set; }
        public DateTime? CompletedDate { get; set; }
        public TodoListDto(TodoTransactionEntity todoTransactionEntity)
        {
            Id = todoTransactionEntity.Id;
            Description = todoTransactionEntity.Description;
            IsCompleted = todoTransactionEntity.IsCompleted;
            CompletedDate = todoTransactionEntity.CompletedDate;
            CreatedDate = todoTransactionEntity.CreatedDate;
            UserId = todoTransactionEntity.UserId;
            User = new UserDto(todoTransactionEntity.User);
        }
    }
}
