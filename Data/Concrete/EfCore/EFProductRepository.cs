using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Concrete.EfCore
{

    public class EFProductRepository : IProductRepository
    {
        ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public bool Create(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
            return true;
        }

        public IQueryable<Product> GetAll()
        {
            return context.Products;
        }
        public IQueryable<Product> GetAll(int companyId)
        {
            return context.Products.Where(i => i.CompanyId == companyId);
        }

        public Product GetById(int id)
        {
            return context.Products.FirstOrDefault(p => p.ProductId == id);
        }

        public bool Update(Product entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           // context.Products.Update(entity);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (GetById(id) != null)
            {
                context.Products.Remove(GetById(id));
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool Save(Product entity)
        {
            if (entity.ProductId == 0)//yeni kayit
            {
                entity.FirstSaveDate = DateTime.Now;
                context.Products.Add(entity);
            }
            else //guncelleme
            {
                var product = GetById(entity.ProductId);
                    if (product != null)
                {
                    product.FirstSaveDate = entity.FirstSaveDate;
                    product.LastSaveDate = DateTime.Now;
                    product.CompanyId = entity.CompanyId;
                    product.Name = entity.Name;
                    product.Parcode = entity.Parcode;
                    product.PurchasePrice = entity.PurchasePrice;
                    product.Quntity = entity.Quntity;
                    product.ShortCode = entity.ShortCode;
                    product.WholesalePrice = entity.WholesalePrice;
                    product.Image = entity.Image;
                    product.Notes = entity.Notes;
                }
            }

            context.SaveChanges();
            return true;
        }
    }
}
