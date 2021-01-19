using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Product> Products { get; set; }
    }

}
