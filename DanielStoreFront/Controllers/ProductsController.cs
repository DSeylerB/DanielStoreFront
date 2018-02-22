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
        private Models.ConnectionStrings _connectionStrings;

        public ProductsController
            (Microsoft.Extensions.Options.IOptions<Models.ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value;
        }

        // GET: /<controller>/
        public IActionResult Index(string IdQuery)
        {         
            List<Models.ProductsViewModel> model = new List<Models.ProductsViewModel>();
            
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionStrings.DefaultConnection))
            {
                connection.Open();
                var command = connection.CreateCommand();


                command.CommandText = "sp_GetProduct";
                command.Parameters.AddWithValue("@id", IdQuery);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
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
                                Name = reader.IsDBNull(nameColumn) ? "" : reader.GetString(nameColumn),  
                                Price = reader.IsDBNull(priceColumn) ? 0m : reader.GetDecimal(priceColumn),
                                Description = reader.IsDBNull(descriptionColumn) ? "" : reader.GetString(descriptionColumn),
                                ExplosiveYield = reader.IsDBNull(explosiveYieldColumn) ? 0 : reader.GetInt32(explosiveYieldColumn)
                            });

                    }
                }
                //    This is what the model looks like
                //    model = new Models.ProductsViewModel();
                //    model.ID = 1;
                //    model.Name = "Little Boy";
                //    model.Price = 2000000;
                //    model.Description = "The original";
                //    model.ExplosiveYield = 16.6m;

                connection.Close();
            }

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
