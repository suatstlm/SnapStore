using Application.Features.ProductReviews.Commands.Create;
using Application.Features.ProductReviews.Commands.Delete;
using Application.Features.ProductReviews.Commands.Update;
using Application.Features.ProductReviews.Queries.GetById;
using Application.Features.ProductReviews.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductReviewsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreatedProductReviewResponse>> Add([FromBody] CreateProductReviewCommand command)
    {
        CreatedProductReviewResponse response = await Mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { response.Id }, response);
    }

    [HttpPut]
    public async Task<ActionResult<UpdatedProductReviewResponse>> Update([FromBody] UpdateProductReviewCommand command)
    {
        UpdatedProductReviewResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<DeletedProductReviewResponse>> Delete([FromRoute] Guid id)
    {
        DeleteProductReviewCommand command = new() { Id = id };

        DeletedProductReviewResponse response = await Mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetByIdProductReviewResponse>> GetById([FromRoute] Guid id)
    {
        GetByIdProductReviewQuery query = new() { Id = id };

        GetByIdProductReviewResponse response = await Mediator.Send(query);

        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListProductReviewQuery>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListProductReviewQuery query = new() { PageRequest = pageRequest };

        GetListResponse<GetListProductReviewListItemDto> response = await Mediator.Send(query);

        return Ok(response);
    }
}