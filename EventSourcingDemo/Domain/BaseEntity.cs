using System.ComponentModel.DataAnnotations.Schema;
using EventSourcingDemo.Domain.Common;

namespace EventSourcingDemo.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    private readonly List<IBaseEvent> _domainEvents = new();


    [NotMapped]
    public IReadOnlyCollection<IBaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IBaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IBaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
