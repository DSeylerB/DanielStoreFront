using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielStoreFront.Models
{
    public class CheckoutViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string BillingState { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string BillingCity { get; set; }

        [System.ComponentModel.DataAnnotations.RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage ="Please enter a 5 or 9 digit zip code")]
        [System.ComponentModel.DataAnnotations.Required]
        public int? BillingZip { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string BillingFirstName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string BillingLastName { get; set; }

        public string DeliveryMethod { get; set; }

        
    }
}
