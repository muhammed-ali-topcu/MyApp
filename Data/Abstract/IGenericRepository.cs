using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace  MyApp.WebUI.Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        T GetById(int id);

        void Creat(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
