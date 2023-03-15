IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [Departments] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(60) NOT NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [EmployeeId] nvarchar(50) NOT NULL,
        [EmployeeName] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [GoodsIssues] (
        [Id] int NOT NULL IDENTITY,
        [GoodsIssueId] nvarchar(450) NOT NULL,
        [Receiver] nvarchar(max) NOT NULL,
        [PurchaseOrderNumber] nvarchar(max) NULL,
        [IsConfirmed] bit NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        CONSTRAINT [PK_GoodsIssues] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [GoodsReceipts] (
        [Id] int NOT NULL IDENTITY,
        [GoodsReceiptId] nvarchar(450) NOT NULL,
        [PurchaseOrderNumber] nvarchar(max) NULL,
        [Timestamp] datetime2 NOT NULL,
        [IsConfirmed] bit NOT NULL,
        CONSTRAINT [PK_GoodsReceipts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [ItemClass] (
        [ItemClassId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_ItemClass] PRIMARY KEY ([ItemClassId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [Unit] (
        [UnitName] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Unit] PRIMARY KEY ([UnitName])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [Warehouses] (
        [Id] int NOT NULL IDENTITY,
        [WarehouseId] nvarchar(450) NOT NULL,
        [WarehouseName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Warehouses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [Items] (
        [Id] int NOT NULL IDENTITY,
        [ItemId] nvarchar(450) NOT NULL,
        [ItemClassId] nvarchar(450) NOT NULL,
        [UnitName] nvarchar(450) NOT NULL,
        [ItemName] nvarchar(max) NOT NULL,
        [MinimumStockLevel] float NOT NULL,
        [Price] float NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Items_ItemClass_ItemClassId] FOREIGN KEY ([ItemClassId]) REFERENCES [ItemClass] ([ItemClassId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Unit_UnitName] FOREIGN KEY ([UnitName]) REFERENCES [Unit] ([UnitName]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [Location] (
        [Id] int NOT NULL IDENTITY,
        [LocationId] nvarchar(450) NOT NULL,
        [WarehouseId] int NULL,
        CONSTRAINT [PK_Location] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Location_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [GoodsIssueEntry] (
        [Id] int NOT NULL IDENTITY,
        [GoodsIssueId] int NOT NULL,
        [RequestedSublotSize] float NULL,
        [RequestedQuantity] float NOT NULL,
        [ItemId] int NOT NULL,
        CONSTRAINT [PK_GoodsIssueEntry] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GoodsIssueEntry_GoodsIssues_GoodsIssueId] FOREIGN KEY ([GoodsIssueId]) REFERENCES [GoodsIssues] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GoodsIssueEntry_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [GoodsReceiptLot] (
        [GoodsReceiptLotId] nvarchar(450) NOT NULL,
        [ItemId] int NOT NULL,
        [EmployeeId] int NOT NULL,
        [LocationId] nvarchar(max) NOT NULL,
        [Quantity] float NOT NULL,
        [SublotSize] float NULL,
        [PurchaseOrderNumber] nvarchar(max) NULL,
        [ProductionDate] datetime2 NULL,
        [ExpirationDate] datetime2 NULL,
        [GoodsReceiptId] int NOT NULL,
        CONSTRAINT [PK_GoodsReceiptLot] PRIMARY KEY ([GoodsReceiptLotId]),
        CONSTRAINT [FK_GoodsReceiptLot_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GoodsReceiptLot_GoodsReceipts_GoodsReceiptId] FOREIGN KEY ([GoodsReceiptId]) REFERENCES [GoodsReceipts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GoodsReceiptLot_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [InventoryLogEntries] (
        [Id] int NOT NULL IDENTITY,
        [ItemId] int NOT NULL,
        [ItemLotId] int NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [BeforeQuantity] float NOT NULL,
        [ChangedQuantity] float NOT NULL,
        CONSTRAINT [PK_InventoryLogEntries] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InventoryLogEntries_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [LotAdjustments] (
        [Id] int NOT NULL IDENTITY,
        [LotId] nvarchar(450) NOT NULL,
        [ItemId] int NOT NULL,
        [EmployeeId] int NOT NULL,
        [NewPurchaseOrderNumber] nvarchar(50) NOT NULL,
        [OldPurchaseOrderNumber] nvarchar(50) NOT NULL,
        [Note] nvarchar(max) NULL,
        [BeforeQuantity] float NOT NULL,
        [AfterQuantity] float NOT NULL,
        [IsConfirmed] bit NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        CONSTRAINT [PK_LotAdjustments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LotAdjustments_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LotAdjustments_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [ItemLots] (
        [Id] int NOT NULL IDENTITY,
        [LotId] nvarchar(450) NOT NULL,
        [LocationId] int NOT NULL,
        [ItemId] int NOT NULL,
        [IsIsolated] bit NOT NULL,
        [Quantity] float NOT NULL,
        [SublotSize] float NOT NULL,
        [PurchaseOrderNumber] nvarchar(max) NOT NULL,
        [ProductionDate] datetime2 NOT NULL,
        [ExpirationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_ItemLots] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ItemLots_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ItemLots_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE TABLE [GoodsIssueLot] (
        [GoodsIssueLotId] nvarchar(450) NOT NULL,
        [EmployeeId] int NOT NULL,
        [Quantity] float NOT NULL,
        [SublotSize] float NULL,
        [Note] nvarchar(max) NULL,
        [GoodsIssueEntryId] int NOT NULL,
        CONSTRAINT [PK_GoodsIssueLot] PRIMARY KEY ([GoodsIssueLotId]),
        CONSTRAINT [FK_GoodsIssueLot_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GoodsIssueLot_GoodsIssueEntry_GoodsIssueEntryId] FOREIGN KEY ([GoodsIssueEntryId]) REFERENCES [GoodsIssueEntry] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Employees_EmployeeId] ON [Employees] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssueEntry_GoodsIssueId] ON [GoodsIssueEntry] ([GoodsIssueId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsIssueEntry_ItemId] ON [GoodsIssueEntry] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssueLot_EmployeeId] ON [GoodsIssueLot] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssueLot_GoodsIssueEntryId] ON [GoodsIssueLot] ([GoodsIssueEntryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsIssues_GoodsIssueId] ON [GoodsIssues] ([GoodsIssueId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsReceiptLot_EmployeeId] ON [GoodsReceiptLot] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsReceiptLot_GoodsReceiptId] ON [GoodsReceiptLot] ([GoodsReceiptId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsReceiptLot_ItemId] ON [GoodsReceiptLot] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsReceipts_GoodsReceiptId] ON [GoodsReceipts] ([GoodsReceiptId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_InventoryLogEntries_ItemId] ON [InventoryLogEntries] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_ItemLots_ItemId] ON [ItemLots] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_ItemLots_LocationId] ON [ItemLots] ([LocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_ItemLots_LotId] ON [ItemLots] ([LotId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_Items_ItemClassId] ON [Items] ([ItemClassId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Items_ItemId] ON [Items] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_Items_UnitName] ON [Items] ([UnitName]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Location_LocationId] ON [Location] ([LocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_Location_WarehouseId] ON [Location] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE INDEX [IX_LotAdjustments_EmployeeId] ON [LotAdjustments] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_LotAdjustments_ItemId] ON [LotAdjustments] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_LotAdjustments_LotId] ON [LotAdjustments] ([LotId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Warehouses_WarehouseId] ON [Warehouses] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230309040701_Migrations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230309040701_Migrations', N'7.0.3');
END;
GO

COMMIT;
GO

