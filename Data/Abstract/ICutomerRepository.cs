
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Abstract
{
    public interface ICutomerRepository
    {
        IQueryable<Customer> GetAll();
        IQueryable<Customer> GetAll(int companyId);
        Customer GetById(int id);
        bool Save(Customer entity);//add or update
        bool Delete(int id);
    }
}
