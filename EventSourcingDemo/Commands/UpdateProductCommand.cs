using System;
using System.ComponentModel.DataAnnotations;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Domain.Common;
using EventSourcingDemo.Events;
using MediatR;

namespace EventSourcingDemo.Commands
{
    public class UpdateProductCommand : IRequest<ProductEntity>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductEntity>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;

        public UpdateProductCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ProductEntity> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var product = _dbContext.Products.FirstOrDefault(q => q.Id == request.Id);

            if (product == null)
            {
                throw new ValidationException("product not found");
            }

            var newProduct = new ProductEntity
            {
                Name = request.Name,
                Price = request.Price,
            };

            newProduct.AddDomainEvent(new ProductUpdatedEvent(product.Id, product.Name, product.Price));

            _dbContext.Products.Update(newProduct);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return product;
        }
    }
}