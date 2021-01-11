﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Entities
{
    public class User : IdentityUser<Guid>
    {
        public IList<Product> Products { get; set; }
    }

}
