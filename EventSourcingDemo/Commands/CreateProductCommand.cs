using System;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Domain.Common;
using EventSourcingDemo.Events;
using MediatR;

namespace EventSourcingDemo.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly AppDbContext _dbContext;
        private readonly  IMediator _mediator;

        public CreateProductCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductEntity
            {
                Id = Guid.NewGuid(),
                                Name = request.Name,
                Price = request.Price
            };
            
            product.AddDomainEvent(new ProductCreatedEvent(product.Id,product.Name,product.Price));

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            
            return product.Id;
        }
    }
}