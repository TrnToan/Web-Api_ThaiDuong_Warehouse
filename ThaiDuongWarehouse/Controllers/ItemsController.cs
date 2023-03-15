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
    [Route("{itemId}")]
    public async Task<ItemViewModel> GetItemById(string itemId)
    {
        return await _queries.GetItemByIdAsync(itemId);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateItemCommand command)
    {
        var result = await _mediator.Send(command);
        try
        {
            if(result != true)
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
            if (result is null)
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
