using Microsoft.EntityFrameworkCore;
using MyApp.Data.Abstract;
using MyApp.WebUI.Data.Concrete.EfCore;
using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Data.Concrete.EfCore
{
    public class EfCompanyRepository : EfGenericRepository<Company>, ICompanyRepository
    {
        ApplicationDbContext context;
        public EfCompanyRepository(ApplicationDbContext _context) : base(_context)
        {

        }
    }
}
