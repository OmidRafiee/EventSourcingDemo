using System;
using System.Text.Json.Serialization;

namespace EventSourcingDemo.Domain
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}