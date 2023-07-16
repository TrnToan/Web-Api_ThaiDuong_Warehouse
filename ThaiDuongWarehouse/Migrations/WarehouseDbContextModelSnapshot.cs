﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThaiDuongWarehouse.Infrastructure;

#nullable disable

namespace ThaiDuongWarehouse.Api.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    partial class WarehouseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate.LotAdjustment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("AfterQuantity")
                        .HasColumnType("float");

                    b.Property<double>("BeforeQuantity")
                        .HasColumnType("float");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("LotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ItemId");

                    b.HasIndex("LotId");

                    b.ToTable("LotAdjustments");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.DepartmentAggregate.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate.FinishedProductIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FinishedProductIssueId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Receiver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FinishedProductIssueId")
                        .IsUnique();

                    b.ToTable("FinisedProductIssues");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate.FinishedProductReceipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FinishedProductReceiptId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FinishedProductReceiptId")
                        .IsUnique();

                    b.ToTable("FinishedProductReceipts");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate.GoodsIssue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("GoodsIssueId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Receiver")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GoodsIssueId")
                        .IsUnique();

                    b.ToTable("GoodsIssues");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("GoodsReceiptId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Supplier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("GoodsReceiptId")
                        .IsUnique();

                    b.ToTable("GoodsReceipts");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ItemClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MinimumStockLevel")
                        .HasColumnType("float");

                    b.Property<double?>("PacketSize")
                        .HasColumnType("float");

                    b.Property<string>("PacketUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ItemClassId");

                    b.HasIndex("ItemId", "Unit")
                        .IsUnique();

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.ItemClass", b =>
                {
                    b.Property<string>("ItemClassId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ItemClassId");

                    b.ToTable("ItemClass");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocation", b =>
                {
                    b.Property<int>("ItemLotId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<double>("QuantityPerLocation")
                        .HasColumnType("float");

                    b.HasKey("ItemLotId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("ItemLotLocations");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate.InventoryLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("BeforeQuantity")
                        .HasColumnType("float");

                    b.Property<double>("ChangedQuantity")
                        .HasColumnType("float");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("ItemLotId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ReceivedQuantity")
                        .HasColumnType("float");

                    b.Property<double>("ShippedQuantity")
                        .HasColumnType("float");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TrackingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("InventoryLogEntries");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate.ItemLot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsIsolated")
                        .HasColumnType("bit");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("LotId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("LotId")
                        .IsUnique();

                    b.ToTable("ItemLots");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate.FinishedProductInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("PurchaseOrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("FinishedProductInventories");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LocationId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.HasIndex("WarehouseId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("WarehouseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId")
                        .IsUnique();

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate.LotAdjustment", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate.FinishedProductIssue", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate.FinishedProductIssueEntry", "Entries", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("FinishedProductIssueId")
                                .HasColumnType("int");

                            b1.Property<int>("ItemId")
                                .HasColumnType("int");

                            b1.Property<string>("Note")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PurchaseOrderNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<double>("Quantity")
                                .HasColumnType("float");

                            b1.HasKey("Id");

                            b1.HasIndex("FinishedProductIssueId");

                            b1.HasIndex("ItemId");

                            b1.ToTable("FinishedProductIssueEntry");

                            b1.WithOwner()
                                .HasForeignKey("FinishedProductIssueId");

                            b1.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                                .WithMany()
                                .HasForeignKey("ItemId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Item");
                        });

                    b.Navigation("Employee");

                    b.Navigation("Entries");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate.FinishedProductReceipt", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate.FinishedProductReceiptEntry", "Entries", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("FinishedProductReceiptId")
                                .HasColumnType("int");

                            b1.Property<int>("ItemId")
                                .HasColumnType("int");

                            b1.Property<string>("Note")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PurchaseOrderNumber")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<double>("Quantity")
                                .HasColumnType("float");

                            b1.HasKey("Id");

                            b1.HasIndex("FinishedProductReceiptId");

                            b1.HasIndex("ItemId");

                            b1.ToTable("FinishedProductReceiptEntry");

                            b1.WithOwner()
                                .HasForeignKey("FinishedProductReceiptId");

                            b1.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                                .WithMany()
                                .HasForeignKey("ItemId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.Navigation("Item");
                        });

                    b.Navigation("Employee");

                    b.Navigation("Entries");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate.GoodsIssue", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate.GoodsIssueEntry", "Entries", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("GoodsIssueId")
                                .HasColumnType("int");

                            b1.Property<int>("ItemId")
                                .HasColumnType("int");

                            b1.Property<double>("RequestedQuantity")
                                .HasColumnType("float");

                            b1.HasKey("Id");

                            b1.HasIndex("GoodsIssueId");

                            b1.HasIndex("ItemId");

                            b1.ToTable("GoodsIssueEntry");

                            b1.WithOwner()
                                .HasForeignKey("GoodsIssueId");

                            b1.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                                .WithMany()
                                .HasForeignKey("ItemId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate.GoodsIssueLot", "Lots", b2 =>
                                {
                                    b2.Property<int>("GoodsIssueEntryId")
                                        .HasColumnType("int");

                                    b2.Property<string>("GoodsIssueLotId")
                                        .HasColumnType("nvarchar(450)");

                                    b2.Property<int>("EmployeeId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Note")
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<double>("Quantity")
                                        .HasColumnType("float");

                                    b2.HasKey("GoodsIssueEntryId", "GoodsIssueLotId");

                                    b2.HasIndex("EmployeeId");

                                    b2.ToTable("GoodsIssueLot");

                                    b2.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                                        .WithMany()
                                        .HasForeignKey("EmployeeId")
                                        .OnDelete(DeleteBehavior.Restrict)
                                        .IsRequired();

                                    b2.WithOwner()
                                        .HasForeignKey("GoodsIssueEntryId");

                                    b2.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate.GoodsIssueSublot", "Sublots", b3 =>
                                        {
                                            b3.Property<int>("GoodsIssueLotGoodsIssueEntryId")
                                                .HasColumnType("int");

                                            b3.Property<string>("GoodsIssueLotId")
                                                .HasColumnType("nvarchar(450)");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("int");

                                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b3.Property<int>("Id"));

                                            b3.Property<string>("LocationId")
                                                .IsRequired()
                                                .HasColumnType("nvarchar(max)");

                                            b3.Property<double>("QuantityPerLocation")
                                                .HasColumnType("float");

                                            b3.HasKey("GoodsIssueLotGoodsIssueEntryId", "GoodsIssueLotId", "Id");

                                            b3.ToTable("GoodsIssueSublot");

                                            b3.WithOwner()
                                                .HasForeignKey("GoodsIssueLotGoodsIssueEntryId", "GoodsIssueLotId");
                                        });

                                    b2.Navigation("Employee");

                                    b2.Navigation("Sublots");
                                });

                            b1.Navigation("Item");

                            b1.Navigation("Lots");
                        });

                    b.Navigation("Employee");

                    b.Navigation("Entries");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceiptLot", "Lots", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<int>("EmployeeId")
                                .HasColumnType("int");

                            b1.Property<DateTime?>("ExpirationDate")
                                .HasColumnType("datetime2");

                            b1.Property<int>("GoodsReceiptId")
                                .HasColumnType("int");

                            b1.Property<string>("GoodsReceiptLotId")
                                .IsRequired()
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("ItemId")
                                .HasColumnType("int");

                            b1.Property<string>("Note")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<DateTime?>("ProductionDate")
                                .HasColumnType("datetime2");

                            b1.Property<double>("Quantity")
                                .HasColumnType("float");

                            b1.HasKey("Id");

                            b1.HasIndex("EmployeeId");

                            b1.HasIndex("GoodsReceiptId");

                            b1.HasIndex("GoodsReceiptLotId")
                                .IsUnique();

                            b1.HasIndex("ItemId");

                            b1.ToTable("GoodsReceiptLot");

                            b1.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate.Employee", "Employee")
                                .WithMany()
                                .HasForeignKey("EmployeeId")
                                .OnDelete(DeleteBehavior.Restrict)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("GoodsReceiptId");

                            b1.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                                .WithMany()
                                .HasForeignKey("ItemId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.OwnsMany("ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceiptSublot", "Sublots", b2 =>
                                {
                                    b2.Property<int>("GoodsReceiptLotId")
                                        .HasColumnType("int");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int");

                                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b2.Property<int>("Id"));

                                    b2.Property<string>("LocationId")
                                        .IsRequired()
                                        .HasColumnType("nvarchar(max)");

                                    b2.Property<double>("QuantityPerLocation")
                                        .HasColumnType("float");

                                    b2.HasKey("GoodsReceiptLotId", "Id");

                                    b2.ToTable("GoodsReceiptSublot");

                                    b2.WithOwner()
                                        .HasForeignKey("GoodsReceiptLotId");
                                });

                            b1.Navigation("Employee");

                            b1.Navigation("Item");

                            b1.Navigation("Sublots");
                        });

                    b.Navigation("Employee");

                    b.Navigation("Lots");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.ItemClass", "ItemClass")
                        .WithMany("Items")
                        .HasForeignKey("ItemClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemClass");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocation", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate.ItemLot", "ItemLot")
                        .WithMany("ItemLotLocations")
                        .HasForeignKey("ItemLotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Location", "Location")
                        .WithMany("ItemLotLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ItemLot");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate.InventoryLogEntry", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate.ItemLot", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate.FinishedProductInventory", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Location", b =>
                {
                    b.HasOne("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Warehouse", null)
                        .WithMany("Locations")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.ItemClass", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate.ItemLot", b =>
                {
                    b.Navigation("ItemLotLocations");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Location", b =>
                {
                    b.Navigation("ItemLotLocations");
                });

            modelBuilder.Entity("ThaiDuongWarehouse.Domain.AggregateModels.StorageAggregate.Warehouse", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
