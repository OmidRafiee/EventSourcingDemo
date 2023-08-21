using EventSourcingDemo.Domain;
using Microsoft.EntityFrameworkCore;


public class EventStoreDbContext : DbContext
{
    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
    {
    }

    public DbSet<EventEntity> EventEntity { get; set; }
}