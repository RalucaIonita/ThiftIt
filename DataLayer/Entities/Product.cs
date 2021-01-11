using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public User Seller { get; set; }
        public Guid SellerId { get; set; }
        public ProductType Type { get; set; }
    }
}
