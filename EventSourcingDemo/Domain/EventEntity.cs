using MediatR;

namespace EventSourcingDemo.Domain
{
    public  class EventEntity : BaseEntity, INotification
    {
        public EventEntity()
        {
           Action=this.GetType().Name;
        }

        public Guid AggregateId { get; set; }
        public  string EntityName { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string CustomerIsin { get; set; }
    }
}
