using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataAccess.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
