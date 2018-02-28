using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DanielStoreFront.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanielStoreFront.Controllers
{
    public class ProductsController : Controller
    {
        private DanielTestContext _context;

        public ProductsController(DanielTestContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index(int? IdQuery)
        {
            if (IdQuery.HasValue)
            {
                return View(_context.Products.Include(x => x.Reviews).Where(x => x.Id == IdQuery.Value));
            }
            else
            {
                return View(_context.Products.Include(x => x.Reviews));
            }
        }
       
        [HttpPost]
        public IActionResult Index(string id, bool? extraparam)
        {
            string cartId;
            if(!Request.Cookies.ContainsKey("cartId"))
            {
                cartId = Guid.NewGuid().ToString();
                Response.Cookies.Append("cartId", cartId, 
                    new Microsoft.AspNetCore.Http.CookieOptions
                    {
                        Expires = DateTime.Now.AddYears(1)
                    });
            }
            else
            {
                Request.Cookies.TryGetValue("cartId", out cartId);
            }

            Response.Cookies.Append("productID", id);

            
            //this.HttpContext.Session.Set(cartId, );

            return RedirectToAction("Index", "Checkout");
        }
    }
}
