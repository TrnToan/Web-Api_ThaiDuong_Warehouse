namespace ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate
{
    public class Location
    {
        public string LocationId { get; private set; }
        public IList<ItemLot> ItemLots { get; private set; }

        public Location(string locationId)
        {
            LocationId = locationId;
            ItemLots = new List<ItemLot>();
        }
    }
}
