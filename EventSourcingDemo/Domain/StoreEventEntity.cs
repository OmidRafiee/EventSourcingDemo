using EventSourcingDemo.Domain.Common;
using MediatR;

namespace EventSourcingDemo.Domain
{
    public class StoreEventEntity : Event
    {
        public StoreEventEntity(Event theEvent, string data, string customerIsin)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            EntityName = theEvent.EntityName;
            Action=theEvent.Action;
            Data = data;
            CustomerIsin = customerIsin;
        }

        // EF Constructor
        protected StoreEventEntity() { }
        public Guid Id { get; private set; }

        public string Data { get; private set; }
        public string CustomerIsin { get; private set; }
    }
}
