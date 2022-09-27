using AUS2.Core.DAL.IRepository;
using AUS2.Core.DBObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AUS2.Core.DAL.Repository
{
    public class Repository<T> : IServices<T> where T : class
    {
        internal ApplicationContext _context;
        internal DbSet<T> _db;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public void Add(T entity) => _context.Set<T>().Add(entity);

        public void AddRange(IEnumerable<T> entities) => _context.Set<T>().AddRange(entities);

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _db.AttachRange(entities);
            _context.Entry(entities).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression, string includeProperties = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
                query = query.Where(expression);

            if (!string.IsNullOrEmpty(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return query;
        }

        public IEnumerable<T> GetAll(string includeProperties = null)
        {
            IQueryable<T> query = _db.Select(x => x);

            if (!string.IsNullOrEmpty(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return query.ToList();
        }

        public void Remove(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _db.Attach(entity);

            _db.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (_context.Entry(entities).State == EntityState.Detached)
                _db.AttachRange(entities);

            _db.RemoveRange(entities);
        }
    }
}
