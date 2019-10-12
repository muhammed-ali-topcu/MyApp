using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.WebUI.Data.Abstract;
using MyApp.WebUI.Data.Concrete.EfCore;
using MyApp.WebUI.Entity;

namespace MyApp.WebUI.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        IPageRepository repository;
        public MenuViewComponent(IPageRepository _repository )
        {
            repository = _repository;
        }
              
        public IViewComponentResult Invoke()
        {
            return View(repository.GetAll().OrderBy(i=>i.Code));
        }

    }
}
