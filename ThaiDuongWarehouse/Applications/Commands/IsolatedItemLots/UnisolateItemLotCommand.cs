namespace ThaiDuongWarehouse.Api.Applications.Commands.IsolatedItemLots;

public class UnisolateItemLotCommand : IRequest<bool>
{
    public string ItemLotId { get; private set; }
    public List<UnisolatedItemSublotViewModel> UnisolatedItemSublots { get; private set; }

    public UnisolateItemLotCommand(string itemLotId, List<UnisolatedItemSublotViewModel> unisolatedItemSublots)
    {
        ItemLotId = itemLotId;
        UnisolatedItemSublots = unisolatedItemSublots;
    }
}
