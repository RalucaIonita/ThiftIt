using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Picture
    {
        public string Body { get; set; }
        public string Title { get; set; }
        public Guid ProductId { get; set; }
    }
}
