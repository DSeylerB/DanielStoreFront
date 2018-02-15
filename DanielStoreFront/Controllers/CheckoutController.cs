using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DanielStoreFront.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DanielStoreFront.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string productName = "Your bomb";
            Request.Cookies.TryGetValue("productID", out productName);
            ViewData["productName"] = productName;

            // ViewData["States"] = new string[] {"Alabama", "Arkansas", "Alaska" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View();
            }

            ViewData["States"] = new string[] { "Alabama", "Arkansas", "Alaska" };

            return View();
        }
    }
}