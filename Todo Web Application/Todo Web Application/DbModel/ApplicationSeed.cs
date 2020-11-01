using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TodoWebApplication.DbModel
{
    public static class ApplicationSeed
    {
        private static readonly UserEntity[] _userList = new UserEntity[]
        {
            new UserEntity()
            {
                FullName = "Allan Valdez",
                Password = Encrypt("admin123"),
                UserName = "alvin",
                UserRoles = new List<UserRoleEntity>()
                {
                    new UserRoleEntity()
                    {
                        Role = new RoleEntity(){Role="Admin"},
                        //UserId = 1
                    },
                    new UserRoleEntity()
                    {
                        Role = new RoleEntity(){Role="User"},
                        //UserId = 1
                    }
                }
            }
        };
        private static readonly TodoTransactionEntity[] _todoList = new TodoTransactionEntity[]
        {
            new TodoTransactionEntity()
            {
               UserId = 1,
               Description = "To work with todo project",
               CreatedDate = DateTime.Now
            },
            new TodoTransactionEntity()
            {
               UserId = 1,
               Description = "To work with todo project part 2",
               CreatedDate = DateTime.Now
            },
            new TodoTransactionEntity()
            {
               UserId = 1,
               Description = "To work with todo project part 3",
               CreatedDate = DateTime.Now,
               IsCompleted = true,
               CompletedDate = DateTime.Now
            },new TodoTransactionEntity()
            {
               UserId = 1,
               Description = "To work with todo project part 4",
               CreatedDate = DateTime.Now,
               IsCompleted = true,
               CompletedDate = DateTime.Now
            }
        };
        public static void InitializeApplication(TodoContext context, IConfiguration configuration)
        {
            SeedRequiredData(context);
        }
        public static void SeedRequiredData(TodoContext context)
        {
            context = context ?? throw new ArgumentNullException(nameof(context));

            if (!context.Users.Any())
            {
                foreach (var user in _userList)
                {
                    context.Users.Add(user);
                }
            }
            context.SaveChanges();
            if (!context.TodoTransactions.Any())
            {
                foreach (var todoTransaction in _todoList)
                {
                    context.TodoTransactions.Add(todoTransaction);
                }
            }
            context.SaveChanges();
        }
            private static string Encrypt(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);

        }
    }
}
