using Microsoft.EntityFrameworkCore;
using  MyApp.WebUI.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace  MyApp.WebUI.Data.Concrete.EfCore
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        public EfGenericRepository(ApplicationDbContext _context)
        {
            context = _context;

        }
        public void Creat(T entity)
        {
            context.Set<T>().Add(entity);
            Commit();
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            Commit();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //context.Set<T>().Attach(entity);
            Commit();
        }
    }
}
