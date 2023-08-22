using System;
using System.Text.Json;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain.Common;
using MediatR;

namespace EventSourcingDemo.Events
{
    public class ProductUpdatedEvent : Event
    {
        public ProductUpdatedEvent(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
            AggregateId = id;
            EntityName= "Product";
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }


    }
}