using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IdDeleted { get; set; }
    }
}
