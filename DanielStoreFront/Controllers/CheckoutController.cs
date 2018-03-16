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
        private SmartyStreets.USStreetApi.Client _usStreetClient;
        private Braintree.BraintreeGateway _braintreeGateway;

        public CheckoutController( Braintree.BraintreeGateway braintreeGateway, SmartyStreets.USStreetApi.Client usStreetClient)
        {
            _usStreetClient = usStreetClient;
            _braintreeGateway = braintreeGateway;
        }

        public IActionResult ValidateAddress(string street, string city, string state)
        {
            SmartyStreets.USStreetApi.Lookup lookup = new SmartyStreets.USStreetApi.Lookup();
            lookup.Street = street;
            lookup.City = city;
            lookup.State = state;
            _usStreetClient.Send(lookup);
            return Json(lookup.Result);
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
        public async Task<IActionResult> Index (CheckoutViewModel model)
        { 
            if (ModelState.IsValid)
            {

                Braintree.CustomerRequest customerRequest = new Braintree.CustomerRequest
                {

                };

                Braintree.AddressRequest addressRequest = new Braintree.AddressRequest
                {
                    StreetAddress = model.Billingaddress,
                    PostalCode = model.BillingZip,
                    Region = model.BillingState,
                    Locality = model.BillingCity,
                    CountryName = "USA"
                };

                Braintree.TransactionRequest saleRequest = new Braintree.TransactionRequest();
                saleRequest.Amount = 10;    //Hard-coded forever because Braintree ain't gonna take $2 million+
                saleRequest.CreditCard = new Braintree.TransactionCreditCardRequest
                {
                    CardholderName = model.creditcardname,
                    CVV = model.creditcardverificationvalue,
                    ExpirationMonth = model.expirationmonth,
                    ExpirationYear = model.expirationyear,
                    Number = model.creditcardnumber
                };
                saleRequest.BillingAddress = new Braintree.AddressRequest
                {
                    StreetAddress = model.Billingaddress,
                    PostalCode = model.BillingZip,
                    Region = model.BillingState,
                    Locality = model.BillingCity,
                    CountryName = "USA"

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
         
            return View();
        }
        

            [HttpPost]
            public IActionResult placeOrder()
            {
                return RedirectToAction("thanks", "Checkout");
            }


            //ViewData["States"] = new string[] { "Alabama", "Arkansas", "Alaska" };

            //return View();
        
    }
}