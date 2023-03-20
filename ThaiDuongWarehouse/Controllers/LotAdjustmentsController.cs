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
    public async Task<IEnumerable<LotAdjustmentViewModel>> GetAdjustmentsAsync()
    {
        return await _queries.GetUnconfirmedAdjustments();
    }
    [HttpPost]
    [Route("AddNewLotAdjustment/{lotId}")]
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
    [Route("ConfirmLotAdjustment")]
    public async Task<IActionResult> ConfirmLotAdjustment(ConfirmLotAdjustmentCommand command)
    {
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
    [Route("Delete/{lotId}")]
    public async Task<IActionResult> DeleteLotAdjustment([FromRoute] RemoveLotAdjustmentCommand command)
    {
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
