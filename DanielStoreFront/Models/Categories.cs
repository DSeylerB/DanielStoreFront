using System;
using System.Collections.Generic;

namespace DanielStoreFront.Models
{
    public partial class Categories
    {
        public Categories()
        {
            ProductsCategories = new HashSet<ProductsCategories>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public ICollection<ProductsCategories> ProductsCategories { get; set; }
    }
}
