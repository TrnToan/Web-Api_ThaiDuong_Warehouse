﻿namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueEntryViewModel
{
    public double? RequestedSublotSize { get; private set; }
    public double RequestedQuantity { get; private set; }
    public ItemViewModel Item { get; private set; }
    public List<GoodsIssueLotViewModel> Lots { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public GoodsIssueEntryViewModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {   }
    public GoodsIssueEntryViewModel(double? requestedSublotSize, double requestedQuantity, 
        ItemViewModel item, List<GoodsIssueLotViewModel> lots)
    {
        RequestedSublotSize = requestedSublotSize;
        RequestedQuantity = requestedQuantity;
        Item = item;
        Lots = lots;
    }
}

