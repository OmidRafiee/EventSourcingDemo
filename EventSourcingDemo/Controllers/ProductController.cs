using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingDemo.Commands;
using EventSourcingDemo.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
	readonly IMediator _mediator;
	
	public ProductController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var products = await _mediator.Send(new GetProductsQuery());
		return Ok(products);
	}
	
	[HttpPost]
	public async Task<IActionResult> Post(CreateProductCommand createProductCommand)
	{
		var productId = await _mediator.Send(createProductCommand);
		return Ok(productId);
	}

    [HttpPost("Update")]
    public async Task<IActionResult> Update(UpdateProductCommand updateProductCommand)
    {
        var productId = await _mediator.Send(updateProductCommand);
        return Ok(productId);
    }

    [HttpPost("Remove")]
    public async Task<IActionResult> Remove(RemoveProductCommand removeProductCommand)
    {
        var productId = await _mediator.Send(removeProductCommand);
        return Ok(productId);
    }
}
