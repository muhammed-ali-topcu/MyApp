using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Concrete.EfCore
{
    public class EfPageRepository : IPageRepository
    {
        private ApplicationDbContext context;
        public EfPageRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Page> GetAll()
        {
             return context.pages;
        }

        IQueryable<Page> IPageRepository.GetAll()
        {
            return context.pages;
        }
    }
}
