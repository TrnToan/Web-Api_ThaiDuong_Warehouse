using ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LotAdjustmentsController : ControllerBase
{
    private readonly ILotAdjustmentQueries _queries;
    private readonly IMediator _mediator;
    public LotAdjustmentsController(ILotAdjustmentQueries queries, IMediator mediator)
    {
        _queries = queries;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<LotAdjustmentViewModel>> GetLotAdjustmentsByTimeAsync([FromQuery] TimeRangeQuery query)
    {
        return await _queries.GetAdjustmentsByTime(query);
    }

    [HttpGet]
    [Route("{isConfirmed}")]
    public async Task<IEnumerable<LotAdjustmentViewModel>> GetAdjustmentsAsync(bool isConfirmed)
    {
        return await _queries.GetIsConfirmedAdjustments(isConfirmed);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateLotAdjustmentCommand command)
    {
        var result = await _mediator.Send(command);
        try
        {
            if (result != true)
            {
                return BadRequest(result);
            }
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPatch]
    [Route("Confirm/{lotId}")]
    public async Task<IActionResult> ConfirmLotAdjustment(string lotId)
    {
        ConfirmLotAdjustmentCommand command = new (lotId);
        bool result = await _mediator.Send(command);
        try
        {
            if (result != true)
            {
                return BadRequest(result);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete]
    [Route("{lotId}")]
    public async Task<IActionResult> DeleteLotAdjustment([FromRoute] string lotId)
    {
        var command = new RemoveLotAdjustmentCommand(lotId);
        bool result = await _mediator.Send(command);
        try
        {
            if (result != true)
            {
                return BadRequest();
            }
            else return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
