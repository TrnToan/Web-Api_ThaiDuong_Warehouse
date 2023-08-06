using ThaiDuongWarehouse.Api.Applications.Commands.Items;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ApiControllerBase
{
    private readonly IItemQueries _queries;
    public ItemsController(IMediator mediator, IItemQueries queries) : base(mediator)
    {
        _queries = queries;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemViewModel>> GetAllItems([FromQuery] string? itemClassId)
    {
        return await _queries.GetAllItemsAsync(itemClassId);
    }

    [HttpGet]
    [Route("{itemId}/{unit}")]
    public async Task<ItemViewModel?> GetItemById([FromRoute]string itemId, [FromRoute]string unit)
    {
        return await _queries.GetItemByIdAsync(itemId, unit);
    }

    [HttpPost]
    [Route("item")]
    public async Task<IActionResult> PostAsync([FromBody] CreateItemCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPost]
    public async Task<IActionResult> PostItemsAsync([FromBody] CreateItemsCommand command)
    {
        return await CommandAsync(command);
    }

    [HttpPatch]
    public async Task<IActionResult> PatchAsync(UpdateItemCommand command)
    {
        return await CommandAsync(command);
    }
}
