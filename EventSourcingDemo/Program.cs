using EventSourcingDemo.Data;
using EventSourcingDemo.Migrations;
using EventSourcingDemo.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);

builder.Services.AddScoped<DomainEventSaveChangesDispatcher>();
// Add Write DB Context
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext"));

    // Interceptors
    var entityAuditProvider = serviceProvider.GetRequiredService<DomainEventSaveChangesDispatcher>();
    options.AddInterceptors(entityAuditProvider);
});


// Add Audit Log DB Context
builder.Services.AddDbContext<EventStoreDbContext>((serviceProvider, options) =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("EventStoreDbContext"));

   

});


// Register Event Replay Service
builder.Services.AddScoped<EventReplayService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
await Initialise(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




//---------------------    Initialise and seed database  ----------------------------
async Task Initialise(WebApplication webApplication)
{

    using (var scope = webApplication.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await initialiser.Database.MigrateAsync();

    }

    using (var scope = webApplication.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<EventStoreDbContext>();
        await initialiser.Database.MigrateAsync();
    }

}
