using Application.Features.ProductSizes.Commands.Create;
using Application.Features.ProductSizes.Commands.Delete;
using Application.Features.ProductSizes.Commands.Update;
using Application.Features.ProductSizes.Queries.GetById;
using Application.Features.ProductSizes.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductSizesController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedProductSizeResponse>> Add([FromBody] CreateProductSizeCommand command)
    {
        CreatedProductSizeResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedProductSizeResponse>> Update([FromBody] UpdateProductSizeCommand command)
    {
        UpdatedProductSizeResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductSizeResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductSizeCommand command = new() { Id = id };

        DeletedProductSizeResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductSizeResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductSizeQuery query = new() { Id = id };

        GetByIdProductSizeResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListProductSizeQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductSizeQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductSizeListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}