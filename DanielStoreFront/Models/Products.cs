using System;
using System.Collections.Generic;

namespace DanielStoreFront.Models
{
    public partial class Products
    {
        public Products()
        {
            Reviews = new HashSet<Review>();
            Category = new HashSet<ProductsCategories>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? ExplosiveYield { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }


        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductsCategories> Category { get; set; }

        public ICollection<LineItem> LineItems { get; set; }
    }
}
