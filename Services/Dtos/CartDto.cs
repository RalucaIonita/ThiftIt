using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Entities;

namespace Services.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public IList<Product> Products { get; set; }
    }
}
