using System;
using System.Collections.Generic;

namespace DanielStoreFront.Models
{
    public partial class Cart
    {
        public Guid Id { get; set; }
        public string AspNetUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastModified { get; set; }
    }
}
