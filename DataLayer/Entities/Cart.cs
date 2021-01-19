using System;
using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Cart : BaseEntity
    {
        public IList<Product> Products { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
