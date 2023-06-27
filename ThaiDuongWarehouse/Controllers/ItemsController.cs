using Microsoft.AspNetCore.Mvc;
using ThaiDuongWarehouse.Api.Applications.Commands.Items;
using ThaiDuongWarehouse.Api.Applications.Queries.Items;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IItemQueries _queries;
    public ItemsController(IMediator mediator, IItemQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemViewModel>> GetAllItems()
    {
        return await _queries.GetAllItemsAsync();
    }

    [HttpGet]
    [Route("{itemId}/{unit}")]
    public async Task<ItemViewModel?> GetItemById([FromRoute]string itemId, [FromRoute]string unit)
    {
        return await _queries.GetItemByIdAsync(itemId, unit);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateItemCommand command)
    {
        var result = await _mediator.Send(command);
        try
        {
            if(!result)
            {
                return BadRequest();
            }
            else return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("items")]
    public async Task<IActionResult> PostItemsAsync([FromBody] CreateItemsCommand command)
    {
        bool result = await _mediator.Send(command);
        try
        {
            if (!result)
            {
                return BadRequest();
            }
            else return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync(UpdateItemCommand command)
    {
        var result = await _mediator.Send(command);
        try
        {
            if (result is false)
            {
                return BadRequest();
            }
            else return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
