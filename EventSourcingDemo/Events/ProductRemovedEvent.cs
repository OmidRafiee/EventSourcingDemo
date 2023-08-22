using System;
using System.Text.Json;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain.Common;
using MediatR;

namespace EventSourcingDemo.Events
{
    public class ProductRemovedEvent : Event
    {
        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            EntityName= "Product";
        }

        public Guid Id { get; set; }
   
    }
}