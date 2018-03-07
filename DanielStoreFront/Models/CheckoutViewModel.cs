using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DanielStoreFront.Models
{
    public class CheckoutViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Billing
        [Required]
        public string Billingaddress { get; set; }

        [Required]
        public string BillingState { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string BillingCity { get; set; }

        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage ="Please enter a 5 or 9 digit zip code")]
        [System.ComponentModel.DataAnnotations.Required]
        public string BillingZip { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string BillingFirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string BillingLastName { get; set; }


        //Shipping
        [System.ComponentModel.DataAnnotations.Required]
        public string shippingaddress { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingState { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingCity { get; set; }

        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Please enter a 5 or 9 digit zip code")]
        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingZip { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingFirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string ShippingLastName { get; set; }



        public string DeliveryMethod { get; set; }


        //Credit Cards
        [System.ComponentModel.DataAnnotations.Required]
        public string creditcardnumber { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string creditcardname { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string creditcardverificationvalue { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string expirationmonth { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string expirationyear { get; set; }




    }
}
