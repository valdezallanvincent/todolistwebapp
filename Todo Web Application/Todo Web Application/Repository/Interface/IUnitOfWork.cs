using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoWebApplication.DbModel;

namespace TodoWebApplication.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();

        void Add<TEntity>(TEntity entity)
            where TEntity : BaseEntity;

        Task<List<TEntity>> FindAsyncByPredicateWithIncludeProperty<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
         where TEntity : BaseEntity;
        void Remove<TEntity>(TEntity entity)
            where TEntity : BaseEntity;

        void RemoveRange<TEntity>(List<TEntity> entity)
            where TEntity : BaseEntity;
    }
}
