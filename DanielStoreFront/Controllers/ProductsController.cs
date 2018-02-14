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
        public IActionResult Index(string IdQuery)
        {
            Models.ProductsViewModel[] model = null;
            //string query = QuerySelector();
            
            if (IdQuery == "0")
            {
                model = new Models.ProductsViewModel[3];
                model[0] = new Models.ProductsViewModel();
                model[0].ID = 1;
                model[0].Name = "Little Boy";
                model[0].Price = 2000000;
                model[0].Description = "The original";
                model[0].ExplosiveYield = 16.6m;

                model[1] = new Models.ProductsViewModel();
                model[1].ID = 2;
                model[1].Name = "Fat Man";
                model[1].Price = 2000000;
                model[1].Description = "Little Boy's bigger brother.";
                model[1].ExplosiveYield = 18.8m;
                
                model[2] = new Models.ProductsViewModel();
                model[2].ID = 3;
                model[2].Name = "Tzar Bomba";
                model[2].Price = 2000000000;
                model[2].Description = "So powerful the russians were too scared to test it at full power! The shockwave still went around the globe THREE times at 50% yield.";
                model[2].ExplosiveYield = 100000;
            }
            
            else if ( IdQuery == "1" )
            {
                model = new Models.ProductsViewModel[1];
                model[0] = new Models.ProductsViewModel();
                model[0].ID = 1;
                model[0].Name = "Little Boy";
                model[0].Price = 2000000;
                model[0].Description = "The original";
                model[0].ExplosiveYield = 16.6m;             
            }
            else if (IdQuery == "2")
            {
                model = new Models.ProductsViewModel[1];
                model[0] = new Models.ProductsViewModel();
                model[0].ID = 2;
                model[0].Name = "Fat Man";
                model[0].Price = 2000000;
                model[0].Description = "Little Boy's bigger brother.";
                model[0].ExplosiveYield = 18.8m;
            }

            return View(model);
        }
       
    }
}
