using System;

namespace DataLayer.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid ToUserId { get; set; }
        public User ToUser { get; set; }
        public Guid FromUser { get; set; }
        public User FromUserId { get; set; }
        public float Amount { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
