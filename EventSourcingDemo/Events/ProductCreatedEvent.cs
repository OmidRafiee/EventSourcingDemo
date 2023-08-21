using System;
using System.Text.Json;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using MediatR;

namespace EventSourcingDemo.Events
{
    public class ProductCreatedEvent : EventEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsReplay { get; set; }


        public string EntityName { get; set; } = "Product";
    }
}