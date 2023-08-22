using EventSourcingDemo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EventSourcingDemo.Migrations;

internal class DomainEventSaveChangesDispatcher : SaveChangesInterceptor
{
    private readonly IMediator _mediator;

    public DomainEventSaveChangesDispatcher(IMediator mediator)
        => _mediator = mediator;

    //events before saving 
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result) 
        => SavingChangesAsync(eventData, result).GetAwaiter().GetResult();

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await DispatchDomainEventsAsync(eventData.Context.ChangeTracker);
        return result;
    }

    private async Task DispatchDomainEventsAsync(ChangeTracker contextChangeTracker)
    {
        var domainEntities = contextChangeTracker
            .Entries<BaseEntity>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents?.Any() == true)?
            .ToList();

        foreach (var entity in domainEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}