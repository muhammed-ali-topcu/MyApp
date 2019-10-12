
using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Concrete.EfCore
{
    public class EfCutomerRepository : ICutomerRepository
    {
        ApplicationDbContext context;
        public EfCutomerRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public bool Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                context.Customers.Remove(entity);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Customer> GetAll()
        {
            return context.Customers;
        }
        public IQueryable<Customer> GetAll(int companyId)
        {
            return context.Customers.Where(i => i.CompanyId == companyId);
        }
        public Customer GetById(int id)
        {
            return context.Customers.FirstOrDefault(i => i.CustomerId == id);
        }

        public bool Save(Customer entity)
        {
            if (entity.CustomerId <= 0)//yeni kayit
            {
                context.Customers.Add(entity);
            }
            else//guncelleme
            {
                var customer = GetById(entity.CustomerId);
                if (customer != null)
                {
                    customer.Name = entity.Name;
                    customer.Email = entity.Email;
                    customer.Address = entity.Address;
                    customer.CompanyId = entity.CompanyId;
                    customer.Image = entity.Image;
                    customer.Tel = entity.Tel;
                }

            }
            context.SaveChanges();
            return true;

        }
                       
    }
}
