using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielStoreFront.Models
{
    public class CheckoutViewModel
    {
        public string BillingAdress { get; set; }
        public string BillingState { get; set; }
        public string BillingCity { get; set; }
        public int? BillingZip { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }

        public string DeliveryMethod { get; set; }

        public string ShipAdress { get; set; }
        public string ShipState { get; set; }
        public string ShipCity { get; set; }
        public int? ShipZip { get; set; }
        public string ShipFirstName { get; set; }
        public string ShipLastName { get; set; }
    }
}
