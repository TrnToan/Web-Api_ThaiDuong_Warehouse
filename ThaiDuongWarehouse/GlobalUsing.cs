﻿global using ThaiDuongWarehouse.Infrastructure;
global using Microsoft.EntityFrameworkCore;
global using ThaiDuongWarehouse.Api.Applications.Queries.Items;
global using ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate;
global using AutoMapper;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using System.Runtime.Serialization;
global using Microsoft.Extensions.DependencyInjection;
global using ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
global using ThaiDuongWarehouse.Infrastructure.Repositories;
global using ThaiDuongWarehouse.Api.Applications.Queries.Employees;
global using ThaiDuongWarehouse.Api.Applications.Queries.Department;
global using ThaiDuongWarehouse.Domain.AggregateModels.DepartmentAggregate;
global using ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;
global using ThaiDuongWarehouse.Api.Applications.Queries.Warehouses;
global using ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate;
global using ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;
global using ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;
global using ThaiDuongWarehouse.Api.Applications.Exceptions;
global using ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;
global using ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;
global using ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
global using ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;
global using ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;
global using ThaiDuongWarehouse.Api.Applications.Queries.Warnings;
global using ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
global using ThaiDuongWarehouse.Domain.AggregateModels.InventoryLogAggregate;
global using ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;
global using ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;
global using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate;
global using ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductReceipts;
