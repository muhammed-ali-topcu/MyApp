
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
        IQueryable<Product> GetAll(int companyId);
        Product GetById(int id);
        bool Delete(int id);
        bool Update(Product entity);
        bool Create(Product entity);
        bool Save(Product entity);//add or update
    }
}
