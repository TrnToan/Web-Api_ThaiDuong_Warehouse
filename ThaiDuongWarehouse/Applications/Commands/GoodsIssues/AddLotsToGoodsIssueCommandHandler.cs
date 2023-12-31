using ThaiDuongWarehouse.Domain.Exceptions;

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

        List<CreateGoodsIssueLotViewModel> newGoodsIssueLots = new();
        IEnumerable<GoodsIssueLot> existingGoodsIssueLots = goodsIssue.Entries.SelectMany(entry => entry.Lots);
        if (existingGoodsIssueLots.Any())
        {
            foreach (var lot in request.GoodsIssueLots)
            {
                var existedLot = existingGoodsIssueLots.FirstOrDefault(l => l.GoodsIssueLotId == lot.GoodsIssueLotId);
                if (existedLot is null)
                    newGoodsIssueLots.Add(lot);
            }
        }

        if (newGoodsIssueLots.Count == 0)
        {
            newGoodsIssueLots = request.GoodsIssueLots;
        }

        List<ItemLot> dispatchedItemLots = new();

        foreach (var lotViewmodel in newGoodsIssueLots)
        {
            var employee = await _employeeRepository.GetEmployeeById(lotViewmodel.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException($"Employee with Id {lotViewmodel.EmployeeId} doesn't exist.");
            }

            var itemLot = await _itemLotRepository.GetLotByLotId(lotViewmodel.GoodsIssueLotId);
            if (itemLot is null)
            {
                throw new EntityNotFoundException($"Itemlot with id {lotViewmodel.GoodsIssueLotId} doesn't exist.");
            }
            if (itemLot.IsIsolated)
            {
                throw new EntityNotFoundException($"Itemlot with id {lotViewmodel.GoodsIssueLotId} is isolated.");
            }

            double quantity = lotViewmodel.ItemLotLocations.Sum(sub => sub.QuantityPerLocation);

            GoodsIssueLot goodsIssueLot = await CreateGoodsIssueLotAsync(lotViewmodel, quantity, employee.Id, itemLot);
            goodsIssue.Addlot(lotViewmodel.ItemId, lotViewmodel.Unit, goodsIssueLot);

            dispatchedItemLots.Add(itemLot);
        }
        goodsIssue.Confirm(dispatchedItemLots);

        _goodsIssueRepository.Update(goodsIssue);
        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

    private async Task<GoodsIssueLot> CreateGoodsIssueLotAsync(CreateGoodsIssueLotViewModel lotVM, double quantity,
        int employeeId, ItemLot itemLot)
    {
        List<GoodsIssueSublot> goodsIssueSublots = new();

        if (lotVM.ItemLotLocations is null)
        {
            throw new WarehouseDomainException($"You must enter location for itemlot.");
        }
        foreach (var sub in lotVM.ItemLotLocations)
        {
            var location = await _storageRepository.GetLocationById(sub.LocationId);
            if (location is null)
            {
                throw new EntityNotFoundException($"Location does not exist, {sub.LocationId}");
            }

            var isExistedLocation = itemLot.ItemLotLocations.Find(ill => ill.Location.LocationId == sub.LocationId);
            if (isExistedLocation == null)
            {
                throw new EntityNotFoundException($"Cannot found itemlot {itemLot.LotId} with locationId {sub.LocationId}");
            }

            GoodsIssueSublot sublot = new(sub.LocationId, sub.QuantityPerLocation);
            goodsIssueSublots.Add(sublot);
        }
        GoodsIssueLot goodsIssueLot = new(lotVM.GoodsIssueLotId, quantity, lotVM.Note, employeeId, goodsIssueSublots);

        return goodsIssueLot;
    }
}