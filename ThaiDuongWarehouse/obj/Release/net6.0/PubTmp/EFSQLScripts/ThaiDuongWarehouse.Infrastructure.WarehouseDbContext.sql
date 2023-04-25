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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [Departments] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(60) NOT NULL,
        CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [Employees] (
        [Id] int NOT NULL IDENTITY,
        [EmployeeId] nvarchar(50) NOT NULL,
        [EmployeeName] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [ItemClass] (
        [ItemClassId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_ItemClass] PRIMARY KEY ([ItemClassId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [Unit] (
        [UnitName] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Unit] PRIMARY KEY ([UnitName])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [Warehouses] (
        [Id] int NOT NULL IDENTITY,
        [WarehouseId] nvarchar(450) NOT NULL,
        [WarehouseName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Warehouses] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [GoodsIssues] (
        [Id] int NOT NULL IDENTITY,
        [GoodsIssueId] nvarchar(450) NOT NULL,
        [Receiver] nvarchar(max) NOT NULL,
        [PurchaseOrderNumber] nvarchar(max) NULL,
        [IsConfirmed] bit NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [EmployeeId] int NOT NULL,
        CONSTRAINT [PK_GoodsIssues] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GoodsIssues_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [GoodsReceipts] (
        [Id] int NOT NULL IDENTITY,
        [GoodsReceiptId] nvarchar(450) NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [IsConfirmed] bit NOT NULL,
        [EmployeeId] int NOT NULL,
        CONSTRAINT [PK_GoodsReceipts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GoodsReceipts_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [Items] (
        [Id] int NOT NULL IDENTITY,
        [ItemId] nvarchar(450) NOT NULL,
        [ItemClassId] nvarchar(450) NOT NULL,
        [UnitName] nvarchar(450) NOT NULL,
        [ItemName] nvarchar(max) NOT NULL,
        [MinimumStockLevel] float NOT NULL,
        [Price] decimal(12,2) NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Items_ItemClass_ItemClassId] FOREIGN KEY ([ItemClassId]) REFERENCES [ItemClass] ([ItemClassId]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_Unit_UnitName] FOREIGN KEY ([UnitName]) REFERENCES [Unit] ([UnitName]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [GoodsIssueEntry] (
        [Id] int NOT NULL IDENTITY,
        [RequestedSublotSize] float NULL,
        [RequestedQuantity] float NOT NULL,
        [GoodsIssueId] int NOT NULL,
        [ItemId] int NOT NULL,
        CONSTRAINT [PK_GoodsIssueEntry] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GoodsIssueEntry_GoodsIssues_GoodsIssueId] FOREIGN KEY ([GoodsIssueId]) REFERENCES [GoodsIssues] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GoodsIssueEntry_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [GoodsReceiptLot] (
        [GoodsReceiptLotId] nvarchar(450) NOT NULL,
        [LocationId] nvarchar(max) NOT NULL,
        [Quantity] float NOT NULL,
        [SublotSize] float NULL,
        [PurchaseOrderNumber] nvarchar(max) NULL,
        [ProductionDate] datetime2 NULL,
        [ExpirationDate] datetime2 NULL,
        [ItemId] int NOT NULL,
        [EmployeeId] int NOT NULL,
        [GoodsReceiptId] int NOT NULL,
        CONSTRAINT [PK_GoodsReceiptLot] PRIMARY KEY ([GoodsReceiptLotId]),
        CONSTRAINT [FK_GoodsReceiptLot_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_GoodsReceiptLot_GoodsReceipts_GoodsReceiptId] FOREIGN KEY ([GoodsReceiptId]) REFERENCES [GoodsReceipts] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GoodsReceiptLot_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [LotAdjustments] (
        [Id] int NOT NULL IDENTITY,
        [LotId] nvarchar(450) NOT NULL,
        [NewPurchaseOrderNumber] nvarchar(50) NOT NULL,
        [OldPurchaseOrderNumber] nvarchar(50) NOT NULL,
        [Note] nvarchar(max) NULL,
        [BeforeQuantity] float NOT NULL,
        [AfterQuantity] float NOT NULL,
        [IsConfirmed] bit NOT NULL,
        [Timestamp] datetime2 NOT NULL,
        [ItemId] int NOT NULL,
        [EmployeeId] int NOT NULL,
        CONSTRAINT [PK_LotAdjustments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LotAdjustments_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LotAdjustments_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE TABLE [GoodsIssueLot] (
        [GoodsIssueLotId] nvarchar(450) NOT NULL,
        [Quantity] float NOT NULL,
        [SublotSize] float NULL,
        [Note] nvarchar(max) NULL,
        [EmployeeId] int NOT NULL,
        [GoodsIssueEntryId] int NOT NULL,
        CONSTRAINT [PK_GoodsIssueLot] PRIMARY KEY ([GoodsIssueLotId]),
        CONSTRAINT [FK_GoodsIssueLot_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_GoodsIssueLot_GoodsIssueEntry_GoodsIssueEntryId] FOREIGN KEY ([GoodsIssueEntryId]) REFERENCES [GoodsIssueEntry] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Employees_EmployeeId] ON [Employees] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssueEntry_GoodsIssueId] ON [GoodsIssueEntry] ([GoodsIssueId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsIssueEntry_ItemId] ON [GoodsIssueEntry] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssueLot_EmployeeId] ON [GoodsIssueLot] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssueLot_GoodsIssueEntryId] ON [GoodsIssueLot] ([GoodsIssueEntryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsIssues_EmployeeId] ON [GoodsIssues] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsIssues_GoodsIssueId] ON [GoodsIssues] ([GoodsIssueId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsReceiptLot_EmployeeId] ON [GoodsReceiptLot] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsReceiptLot_GoodsReceiptId] ON [GoodsReceiptLot] ([GoodsReceiptId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsReceiptLot_ItemId] ON [GoodsReceiptLot] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_GoodsReceipts_EmployeeId] ON [GoodsReceipts] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_GoodsReceipts_GoodsReceiptId] ON [GoodsReceipts] ([GoodsReceiptId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_InventoryLogEntries_ItemId] ON [InventoryLogEntries] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_ItemLots_ItemId] ON [ItemLots] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_ItemLots_LocationId] ON [ItemLots] ([LocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_ItemLots_LotId] ON [ItemLots] ([LotId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_Items_ItemClassId] ON [Items] ([ItemClassId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Items_ItemId] ON [Items] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_Items_UnitName] ON [Items] ([UnitName]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Location_LocationId] ON [Location] ([LocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_Location_WarehouseId] ON [Location] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE INDEX [IX_LotAdjustments_EmployeeId] ON [LotAdjustments] ([EmployeeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_LotAdjustments_ItemId] ON [LotAdjustments] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_LotAdjustments_LotId] ON [LotAdjustments] ([LotId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    CREATE UNIQUE INDEX [IX_Warehouses_WarehouseId] ON [Warehouses] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315032552_Migrations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230315032552_Migrations', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    ALTER TABLE [ItemLots] DROP CONSTRAINT [FK_ItemLots_Location_LocationId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    ALTER TABLE [Location] DROP CONSTRAINT [FK_Location_Warehouses_WarehouseId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    ALTER TABLE [Location] DROP CONSTRAINT [PK_Location];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    EXEC sp_rename N'[Location]', N'Locations';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    EXEC sp_rename N'[Locations].[IX_Location_WarehouseId]', N'IX_Locations_WarehouseId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    EXEC sp_rename N'[Locations].[IX_Location_LocationId]', N'IX_Locations_LocationId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemLots]') AND [c].[name] = N'SublotSize');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ItemLots] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ItemLots] ALTER COLUMN [SublotSize] float NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemLots]') AND [c].[name] = N'PurchaseOrderNumber');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ItemLots] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [ItemLots] ALTER COLUMN [PurchaseOrderNumber] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemLots]') AND [c].[name] = N'ProductionDate');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ItemLots] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ItemLots] ALTER COLUMN [ProductionDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ItemLots]') AND [c].[name] = N'ExpirationDate');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ItemLots] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [ItemLots] ALTER COLUMN [ExpirationDate] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    ALTER TABLE [Locations] ADD CONSTRAINT [PK_Locations] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    ALTER TABLE [ItemLots] ADD CONSTRAINT [FK_ItemLots_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    ALTER TABLE [Locations] ADD CONSTRAINT [FK_Locations_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230317152813_DbMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230317152813_DbMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230318063853_LocationEntityConfig')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230318063853_LocationEntityConfig', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230321091359_GoodsReceiptLot_Migration')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[InventoryLogEntries]') AND [c].[name] = N'ItemLotId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [InventoryLogEntries] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [InventoryLogEntries] ALTER COLUMN [ItemLotId] nvarchar(max) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230321091359_GoodsReceiptLot_Migration')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GoodsReceiptLot]') AND [c].[name] = N'LocationId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [GoodsReceiptLot] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [GoodsReceiptLot] ALTER COLUMN [LocationId] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230321091359_GoodsReceiptLot_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230321091359_GoodsReceiptLot_Migration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230323164850_FixGoodsReceiptModel')
BEGIN
    ALTER TABLE [GoodsReceipts] ADD [Supplier] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230323164850_FixGoodsReceiptModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230323164850_FixGoodsReceiptModel', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230329050315_GoodsIssueMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230329050315_GoodsIssueMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    ALTER TABLE [Items] DROP CONSTRAINT [FK_Items_Unit_UnitName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    DROP TABLE [Unit];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    DROP INDEX [IX_Items_ItemId] ON [Items];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    DROP INDEX [IX_Items_UnitName] ON [Items];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    EXEC sp_rename N'[Items].[UnitName]', N'Unit', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    ALTER TABLE [LotAdjustments] ADD [Unit] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    ALTER TABLE [ItemLots] ADD [Unit] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    ALTER TABLE [InventoryLogEntries] ADD [Unit] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    ALTER TABLE [GoodsReceiptLot] ADD [Unit] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    ALTER TABLE [GoodsIssueEntry] ADD [Unit] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    CREATE UNIQUE INDEX [IX_Items_ItemId_Unit] ON [Items] ([ItemId], [Unit]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230403045719_RemoveUnitTable_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230403045719_RemoveUnitTable_Migration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230406072045_LocationTableMigration')
BEGIN
    ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_Warehouses_WarehouseId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230406072045_LocationTableMigration')
BEGIN
    DROP INDEX [IX_Locations_WarehouseId] ON [Locations];
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locations]') AND [c].[name] = N'WarehouseId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Locations] DROP CONSTRAINT [' + @var6 + '];');
    EXEC(N'UPDATE [Locations] SET [WarehouseId] = 0 WHERE [WarehouseId] IS NULL');
    ALTER TABLE [Locations] ALTER COLUMN [WarehouseId] int NOT NULL;
    ALTER TABLE [Locations] ADD DEFAULT 0 FOR [WarehouseId];
    CREATE INDEX [IX_Locations_WarehouseId] ON [Locations] ([WarehouseId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230406072045_LocationTableMigration')
BEGIN
    ALTER TABLE [Locations] ADD CONSTRAINT [FK_Locations_Warehouses_WarehouseId] FOREIGN KEY ([WarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230406072045_LocationTableMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230406072045_LocationTableMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230407050832_GenerateGoodsReceiptFKMigration')
BEGIN
    ALTER TABLE [GoodsReceiptLot] ADD [Note] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230407050832_GenerateGoodsReceiptFKMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230407050832_GenerateGoodsReceiptFKMigration', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409022916_RemoveLotAdjustmentsUniqueIndex')
BEGIN
    DROP INDEX [IX_LotAdjustments_LotId] ON [LotAdjustments];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409022916_RemoveLotAdjustmentsUniqueIndex')
BEGIN
    CREATE INDEX [IX_LotAdjustments_LotId] ON [LotAdjustments] ([LotId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409022916_RemoveLotAdjustmentsUniqueIndex')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230409022916_RemoveLotAdjustmentsUniqueIndex', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409054652_RemoveLotAdjustmentUniqueIndex')
BEGIN
    DROP INDEX [IX_LotAdjustments_ItemId] ON [LotAdjustments];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409054652_RemoveLotAdjustmentUniqueIndex')
BEGIN
    CREATE INDEX [IX_LotAdjustments_ItemId] ON [LotAdjustments] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409054652_RemoveLotAdjustmentUniqueIndex')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230409054652_RemoveLotAdjustmentUniqueIndex', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411161154_RemoveItemIdIndexInGoodsReceiptLotTable')
BEGIN
    DROP INDEX [IX_GoodsReceiptLot_ItemId] ON [GoodsReceiptLot];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411161154_RemoveItemIdIndexInGoodsReceiptLotTable')
BEGIN
    CREATE INDEX [IX_GoodsReceiptLot_ItemId] ON [GoodsReceiptLot] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230411161154_RemoveItemIdIndexInGoodsReceiptLotTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230411161154_RemoveItemIdIndexInGoodsReceiptLotTable', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230413150011_RemoveIX_GoodsIssueEntry_ItemId')
BEGIN
    DROP INDEX [IX_GoodsIssueEntry_ItemId] ON [GoodsIssueEntry];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230413150011_RemoveIX_GoodsIssueEntry_ItemId')
BEGIN
    CREATE INDEX [IX_GoodsIssueEntry_ItemId] ON [GoodsIssueEntry] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230413150011_RemoveIX_GoodsIssueEntry_ItemId')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230413150011_RemoveIX_GoodsIssueEntry_ItemId', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424140040_One2Many_Item-GoodsReceiptLot')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230424140040_One2Many_Item-GoodsReceiptLot', N'7.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424164202_NewPKGoodsIssueLot')
BEGIN
    ALTER TABLE [GoodsIssueLot] DROP CONSTRAINT [PK_GoodsIssueLot];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424164202_NewPKGoodsIssueLot')
BEGIN
    DROP INDEX [IX_GoodsIssueLot_GoodsIssueEntryId] ON [GoodsIssueLot];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424164202_NewPKGoodsIssueLot')
BEGIN
    ALTER TABLE [GoodsIssueLot] ADD CONSTRAINT [PK_GoodsIssueLot] PRIMARY KEY ([GoodsIssueEntryId], [GoodsIssueLotId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230424164202_NewPKGoodsIssueLot')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230424164202_NewPKGoodsIssueLot', N'7.0.3');
END;
GO

COMMIT;
GO

