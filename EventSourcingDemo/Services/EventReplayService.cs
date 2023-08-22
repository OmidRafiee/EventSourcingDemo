using System;
using System.Text.Json;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Services
{
    public class EventReplayService
    {
        readonly EventStoreDbContext _auditLogDbContext;
        readonly IMediator _mediator;

        public EventReplayService(EventStoreDbContext auditLogDbContext, IMediator mediator)
        {
            _auditLogDbContext = auditLogDbContext;
            _mediator = mediator;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _auditLogDbContext.EventEntity.ToListAsync();
        }

        public async Task ReplayEventsAsync()
        {
            var events = await _auditLogDbContext.EventEntity.ToListAsync();

            foreach (var @event in events)
            {

                var eventType = @event.EntityName;

                switch (eventType)
                {
                    case "ProductCreatedEvent":
                        {
                            var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(@event.EntityName);

                            // Send the event to the ProductCreatedEventHandler and set the IsReplay flag to true
                            //productCreatedEvent.IsReplay = true;
                            await _mediator.Publish(productCreatedEvent);

                            break;
                        }
                }

            }
        }
    }
}