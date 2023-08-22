using System;
using System.Text.Json;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Domain.Common;
using MediatR;

namespace EventSourcingDemo.Events
{
    public class ProductRemoveEvent : Event
    {
        public ProductRemoveEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            EntityName= "Product";
        }

        public Guid Id { get; set; }
   
    }
}