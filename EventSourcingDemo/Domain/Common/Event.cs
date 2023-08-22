using MediatR;

namespace EventSourcingDemo.Domain.Common
{
    public class Event : IBaseEvent
    {
        public Event()
        {
            Action = GetType().Name;
        }

        public Guid AggregateId { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
