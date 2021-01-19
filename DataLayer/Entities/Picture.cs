using System;

namespace DataLayer.Entities
{
    public class Picture : BaseEntity
    {
        public string Body { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
