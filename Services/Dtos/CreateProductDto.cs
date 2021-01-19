using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public List<string> PictureBodies { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
