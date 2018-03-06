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

        private Braintree.BraintreeGateway _braintreeGateway;

        public CheckoutController( Braintree.BraintreeGateway braintreeGateway)
        {
			
            _braintreeGateway = braintreeGateway;
        }

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
        public async Task<IActionResult> Index(CheckoutViewModel model, string creditcardnumber, string creditcardname, string creditcardverificationvalue, string expirationmonth, string expirationyear)
        {
            if (ModelState.IsValid)
            {
                Braintree.TransactionRequest saleRequest = new Braintree.TransactionRequest();
                saleRequest.Amount = 10;    //Hard-coded for now
                saleRequest.CreditCard = new Braintree.TransactionCreditCardRequest
                {
                    CardholderName = creditcardname,
                    CVV = creditcardverificationvalue,
                    ExpirationMonth = expirationmonth,
                    ExpirationYear = expirationyear,
                    Number = creditcardnumber
                };
                var result = await _braintreeGateway.Transaction.SaleAsync(saleRequest);
                if (result.IsSuccess())
                {
                    //If model state is valid, proceed to the next step.
                    return this.RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors.All())
                {
                    ModelState.AddModelError(error.Code.ToString(), error.Message);
                }
            }

            //ViewData["States"] = new string[] { "Alabama", "Arkansas", "Alaska" };

            return View();
        }
    }
}