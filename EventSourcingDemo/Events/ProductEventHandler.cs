using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Domain.Common;
using MediatR;
using Newtonsoft.Json;

namespace EventSourcingDemo.Events
{
    public class ProductEventHandler<T> : INotificationHandler<T> where T : Event
    {
        private readonly EventStoreDbContext _dbContext;

        public ProductEventHandler(EventStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task Handle(T theEvent, CancellationToken cancellationToken)
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoreEventEntity(theEvent,serializedData,"0610154151");
            
            _dbContext.Add(storedEvent);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}