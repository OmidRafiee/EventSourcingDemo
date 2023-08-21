using EventSourcingDemo.Data;
using EventSourcingDemo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventSourcingDemo.Queries;

public class GetProductsQuery : IRequest<IEnumerable<ProductEntity>>
{

}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductEntity>>
{
    private readonly AppDbContext _dbContext;

    public GetProductsQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductEntity>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Products.ToListAsync(cancellationToken);
    }
}
