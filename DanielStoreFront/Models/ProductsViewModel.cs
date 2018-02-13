using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielStoreFront.Models
{
    public class ProductsViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal ExplosiveYield { get; set; }


    }
}
