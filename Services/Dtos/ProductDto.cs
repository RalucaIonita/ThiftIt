using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class ProductDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }

        public string SellerName { get; set; }
        public string SellerEmail { get; set; }
        public string SellerPhoneNumber { get; set; }
    }
}
