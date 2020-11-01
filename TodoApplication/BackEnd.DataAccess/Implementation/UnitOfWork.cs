using BackEnd.DataAccess.Entities;
using BackEnd.DataAccess.Interface;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace BackEnd.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
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

        public ICollection<TEntity> FindAsyncByPredicateWithIncludeProperty<TEntity>(Expression<Func<TEntity, bool>> predicate,
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
                return entityContext.Where(predicate).ToList();
            }
            return entityContext.ToList();
        }
    }
}
