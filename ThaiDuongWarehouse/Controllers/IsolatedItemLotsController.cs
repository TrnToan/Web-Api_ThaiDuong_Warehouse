using ThaiDuongWarehouse.Api.Applications.Commands.IsolatedItemLots;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IsolatedItemLotsController : ControllerBase
{
    private readonly IMediator _mediator;

    public IsolatedItemLotsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch]
    [Route("{itemLotId}/unisolate")]
    public async Task<IActionResult> PatchAsync(string itemLotId, [FromBody]List<UnisolatedItemSublotViewModel> unisolatedItemSublots)
    {
        UnisolateItemLotCommand command = new (itemLotId, unisolatedItemSublots);
        try
        {
            var result = await _mediator.Send(command);

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
