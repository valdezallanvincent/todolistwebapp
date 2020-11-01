using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoWebApplication.DbModel
{
    public class TodoTransactionEntity : BaseEntity
    {
        public string Description { get; set; }
        public long UserId { get; set; }
        public UserEntity User { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
