using ThaiDuongWarehouse.Domain.AggregateModels;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class AddLotsToGoodsIssueCommandHandler : IRequestHandler<AddLotsToGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IStorageRepository _storageRepository;
    public AddLotsToGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository, IEmployeeRepository employeeRepository, 
        IItemLotRepository itemLotRepository, IStorageRepository storageRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _employeeRepository = employeeRepository;
        _itemLotRepository = itemLotRepository;
        _storageRepository = storageRepository;
    }

    public async Task<bool> Handle(AddLotsToGoodsIssueCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);

        if (goodsIssue is null)
        {
            throw new EntityNotFoundException($"Goodsissue with Id {request.GoodsIssueId} doesn't exist.");
        }

        List<ItemLot> currentItemLots = new();
        foreach(var lotViewmodel in request.GoodsIssueLots)
        {
            var employee = await _employeeRepository.GetEmployeeById(lotViewmodel.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException($"Employee with Id {lotViewmodel.EmployeeId} doesn't exist.");
            }

            var lot = await _itemLotRepository.GetLotByLotId(lotViewmodel.GoodsIssueLotId);
            if (lot is null)
            {
                throw new EntityNotFoundException($"Itemlot with id {lotViewmodel.GoodsIssueLotId} doesn't exist.");
            }
            if (lot.IsIsolated)
            {
                throw new EntityNotFoundException($"Itemlot with id {lotViewmodel.GoodsIssueLotId} is isolated.");
            }

            double quantity = lotViewmodel.ItemLotLocations.Sum(sub => sub.QuantityPerLocation);

            GoodsIssueLot goodsIssueLot = await CreateGoodsIssueLotAsync(lotViewmodel, quantity, employee.Id);
            goodsIssue.Addlot(lotViewmodel.ItemId, goodsIssueLot);

            var itemLot = await _itemLotRepository.GetLotByLotId(lotViewmodel.GoodsIssueLotId);
            if (itemLot is null)
                throw new EntityNotFoundException($"Itemlot with Id {lotViewmodel.GoodsIssueLotId} not found.");

            currentItemLots.Add(itemLot);
        }
        goodsIssue.Confirm(currentItemLots);

        _goodsIssueRepository.Update(goodsIssue);
        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task<GoodsIssueLot> CreateGoodsIssueLotAsync(CreateGoodsIssueLotViewModel lotVM, double quantity, 
        int employeeId)
    {
        List<GoodsIssueSublot> goodsIssueSublots = new List<GoodsIssueSublot>();
        foreach (var sub in lotVM.ItemLotLocations)
        {
            var location = await _storageRepository.GetLocationById(sub.LocationId);
            if (location is null)
            {
                throw new EntityNotFoundException($"Location not found, {sub.LocationId}");
            }

            GoodsIssueSublot sublot = new (sub.LocationId, sub.QuantityPerLocation);
            goodsIssueSublots.Add(sublot);
        }
        GoodsIssueLot goodsIssueLot = new (lotVM.GoodsIssueLotId, quantity, lotVM.Note, employeeId, goodsIssueSublots);

        return goodsIssueLot;
    }
}