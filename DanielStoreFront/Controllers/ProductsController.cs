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
            List<Models.ProductsViewModel> model = new List<Models.ProductsViewModel>();

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DanielTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var connection = new System.Data.SqlClient.SqlConnection(connectionString);

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Products";
            var reader = command.ExecuteReader();
            var nameColumn = reader.GetOrdinal("Name");
            var priceColumn = reader.GetOrdinal("Price");
            var explosiveYieldColumn = reader.GetOrdinal("ExplosiveYield");
            var descriptionColumn = reader.GetOrdinal("Description");
            //var imageUrlColumn = reader.GetOrdinal("ImageUrl");
            while (reader.Read())
            {
                model.Add(
                    new Models.ProductsViewModel
                    {
                        Name = reader.GetString(nameColumn),  //I can see name is the second column in the database.
                        Price = reader.GetDecimal(priceColumn),
                        Description = reader.GetString(descriptionColumn),
                        ExplosiveYield = reader.GetDecimal(explosiveYieldColumn)
                    });
                
            }

            //    model[0] = new Models.ProductsViewModel();
            //    model[0].ID = 1;
            //    model[0].Name = "Little Boy";
            //    model[0].Price = 2000000;
            //    model[0].Description = "The original";
            //    model[0].ExplosiveYield = 16.6m;

            connection.Close();

            return View(model);
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
