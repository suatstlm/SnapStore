using Application.Features.ProductDescriptions.Commands.Create;
using Application.Features.ProductDescriptions.Commands.Delete;
using Application.Features.ProductDescriptions.Commands.Update;
using Application.Features.ProductDescriptions.Queries.GetById;
using Application.Features.ProductDescriptions.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductDescriptionsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedProductDescriptionResponse>> Add([FromBody] CreateProductDescriptionCommand command)
    {
        CreatedProductDescriptionResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedProductDescriptionResponse>> Update([FromBody] UpdateProductDescriptionCommand command)
    {
        UpdatedProductDescriptionResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductDescriptionResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductDescriptionCommand command = new() { Id = id };

        DeletedProductDescriptionResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductDescriptionResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductDescriptionQuery query = new() { Id = id };

        GetByIdProductDescriptionResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListProductDescriptionQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductDescriptionQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductDescriptionListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}