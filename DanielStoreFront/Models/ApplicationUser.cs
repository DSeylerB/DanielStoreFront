using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DanielStoreFront.Models
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public ApplicationUser()
        {
            Reviews = new HashSet<Review>();
        }

        //ApplicationUser is really common
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FavoriteColor { get; set; }

        //A user can author many reviews
        public ICollection<Review> Reviews { get; set; }
    }
}
