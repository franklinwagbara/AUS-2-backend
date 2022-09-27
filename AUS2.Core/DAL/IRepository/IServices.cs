using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AUS2.Core.DAL.IRepository
{
    public interface IServices<T> where T : class
    {
        IEnumerable<T> GetAll(string includeProperties = null);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression, string includeProperties = null);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
