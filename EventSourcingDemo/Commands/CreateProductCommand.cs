using System;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Events;
using MediatR;

namespace EventSourcingDemo.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly AppDbContext _dbContext;
        private readonly  IMediator _mediator;

        public CreateProductCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                Name = request.Name,
                Price = request.Price
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Reload the product to get the Id
            await _dbContext.Entry(product).ReloadAsync(cancellationToken);

            var productCreatedEvent = new ProductCreatedEvent
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            await _mediator.Publish(productCreatedEvent, cancellationToken);

            return product.Id;
        }
    }
}