using MyApp.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  MyApp.WebUI.Data.Abstract
{
   public interface IPageRepository
    {
        IQueryable<Page> GetAll();
        
    }
}
