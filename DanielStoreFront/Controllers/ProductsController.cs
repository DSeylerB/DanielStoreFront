using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanielStoreFront.Controllers
{
    public class ProductsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            Models.ProductsViewModel model = new Models.ProductsViewModel();
            model.ID = 1;
            model.Name = "Little Boy";
            model.Price = 2000000;
            model.Description = "The original";
            model.ExplosiveYield = 16.6m;

            if([FromQuery] string  == 1  )
            {

            }

            

            return View(model);
        }
    }
}
