using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace TodoWebApplication.DbModel
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options)
            : base(options)
        {
            Roles = Set<RoleEntity>();
            TodoTransactions = Set<TodoTransactionEntity>();
            Users = Set<UserEntity>();
            UserRoles = Set<UserRoleEntity>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=todolist.db");
        public DbSet<RoleEntity> Roles { get; }
        public DbSet<TodoTransactionEntity> TodoTransactions { get; }
        public DbSet<UserEntity> Users { get; }
        public DbSet<UserRoleEntity> UserRoles { get; }
    }
}
