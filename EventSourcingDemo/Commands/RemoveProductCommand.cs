using System;
using System.ComponentModel.DataAnnotations;
using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Domain.Common;
using EventSourcingDemo.Events;
using MediatR;

namespace EventSourcingDemo.Commands
{
    public class RemoveProductCommand : IRequest<ValidationResult>
    {
        public Guid Id { get; set; }
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, ValidationResult>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;

        public RemoveProductCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {

            var product = _dbContext.Products.FirstOrDefault(q => q.Id == request.Id);

            if (product == null)
            {
                throw new ValidationException("product not found");
            }
            
            product.AddDomainEvent(new ProductRemoveEvent(product.Id));

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return new ValidationResult("product remove success"); ;
        }
    }
}