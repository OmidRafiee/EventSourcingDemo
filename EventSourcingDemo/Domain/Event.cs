using EventSourcingDemo.Domain.Common;
using MediatR;

namespace EventSourcingDemo.Domain
{
    public class Event : IBaseEvent
    {
        public Event()
        {
            Action = this.GetType().Name;
        }

        public Guid AggregateId { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
