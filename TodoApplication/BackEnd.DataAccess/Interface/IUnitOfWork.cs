using BackEnd.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace BackEnd.DataAccess.Interface
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();

        void Add<TEntity>(TEntity entity)
            where TEntity : BaseEntity;

        ICollection<TEntity> FindAsyncByPredicateWithIncludeProperty<TEntity>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
         where TEntity : BaseEntity;
    }
}
