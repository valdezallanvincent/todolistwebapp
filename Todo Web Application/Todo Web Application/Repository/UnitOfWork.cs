using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TodoWebApplication.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoContext _context;

        public UnitOfWork(TodoContext context)
        {
            _context = context;
        }

        public void RemoveRange<TEntity>(List<TEntity> entity)
            where TEntity : BaseEntity
        {
            _context.RemoveRange(entity);
        }
        public void Remove<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            _context.Remove<TEntity>(entity);
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Add<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            _context.Add<TEntity>(entity);
        }

        public Task<List<TEntity>> FindAsyncByPredicateWithIncludeProperty<TEntity>(Expression<Func<TEntity, bool>> predicate,
           params Expression<Func<TEntity, object>>[] includeProperties)
           where TEntity : BaseEntity
        {
            var entityContext = _context.Set<TEntity>();
            foreach (var includedProperty in includeProperties)
            {
                entityContext.Include(includedProperty).Load();
            }
            if (predicate != null)
            {
                return entityContext.Where(predicate).ToListAsync();
            }
            return entityContext.ToListAsync();
        }
    }
}
