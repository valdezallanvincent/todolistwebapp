using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataAccess.Entities
{
    public class TodoTransactionEntity : BaseEntity
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public UserEntity User { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
