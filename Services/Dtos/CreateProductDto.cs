using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public float Price { get; set; }
    }
}
