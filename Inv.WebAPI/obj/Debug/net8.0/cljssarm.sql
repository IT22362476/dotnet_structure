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

IF SCHEMA_ID(N'Inv') IS NULL EXEC(N'CREATE SCHEMA [Inv];');
GO

CREATE SEQUENCE [dbo].[BITID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[BrandID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[ItemID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[ItemTypeID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[MainCategoryID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[ModelID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[SubCategoryID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[UOMConvID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[UOMID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [Inv].[AuditTrail] (
    [AudtTralID] bigint NOT NULL IDENTITY,
    [AuditDateTimeUtc] datetime2 NOT NULL,
    [UserSerialID] int NOT NULL,
    [FrmSerialID] int NOT NULL,
    [LoginLogSerialID] bigint NOT NULL,
    [TableName] nvarchar(max) NOT NULL,
    [AuditData] nvarchar(max) NOT NULL,
    [MachineName] nvarchar(max) NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AuditTrail] PRIMARY KEY ([AudtTralID])
);
GO

CREATE TABLE [Inv].[Brand] (
    [BrandSerialID] int NOT NULL IDENTITY,
    [BrandID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.BrandID),
    [BrandName] nvarchar(25) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Brand] PRIMARY KEY ([BrandSerialID])
);
GO

CREATE TABLE [Inv].[Item] (
    [ItemSerialID] int NOT NULL IDENTITY,
    [ItemID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.ItemID),
    [ItemCode] int NOT NULL,
    [ItemTypeSerialID] int NOT NULL,
    [ItemDes] nvarchar(max) NOT NULL,
    [MainCategorySerialID] int NOT NULL,
    [SubCategorySerialID] int NOT NULL,
    [BITSerialID] int NOT NULL,
    [ModelSerialID] int NOT NULL,
    [BrandSerialID] int NOT NULL,
    [Weight] float NOT NULL,
    [Volume] float NOT NULL,
    [Size] int NOT NULL,
    [Color] nvarchar(max) NOT NULL,
    [ItemPartNo] nvarchar(max) NOT NULL,
    [Article] nvarchar(max) NOT NULL,
    [Remarks] int NOT NULL,
    [Length] float NOT NULL,
    [Width] float NOT NULL,
    [Height] float NOT NULL,
    [Guage] nvarchar(max) NULL,
    [Construction] nvarchar(max) NULL,
    [SpecialFeatures] nvarchar(max) NULL,
    [UOMSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Item] PRIMARY KEY ([ItemSerialID])
);
GO

CREATE TABLE [Inv].[ItemType] (
    [ItemTypeSerialID] int NOT NULL IDENTITY,
    [ItemTypeID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.ItemTypeID),
    [ItemTypeName] nvarchar(25) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_ItemType] PRIMARY KEY ([ItemTypeSerialID])
);
GO

CREATE TABLE [Inv].[MainCategory] (
    [MainCategorySerialID] int NOT NULL IDENTITY,
    [MainCategoryID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.MainCategoryID),
    [MainCategoryName] nvarchar(25) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_MainCategory] PRIMARY KEY ([MainCategorySerialID])
);
GO

CREATE TABLE [Inv].[Parameter] (
    [ParamSerialID] int NOT NULL IDENTITY,
    [ParamID] int NOT NULL,
    [ParamName] nvarchar(10) NOT NULL,
    [Length] int NOT NULL,
    [LastID] int NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Parameter] PRIMARY KEY ([ParamSerialID])
);
GO

CREATE TABLE [Inv].[UOM] (
    [UOMSerialID] int NOT NULL IDENTITY,
    [UOMID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.UOMID),
    [UOMName] nvarchar(10) NOT NULL,
    [UOMDescription] nvarchar(max) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UOM] PRIMARY KEY ([UOMSerialID])
);
GO

CREATE TABLE [Inv].[Model] (
    [ModelSerialID] int NOT NULL IDENTITY,
    [ModelID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.ModelID),
    [ModelName] nvarchar(25) NOT NULL,
    [BrandSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Model] PRIMARY KEY ([ModelSerialID]),
    CONSTRAINT [FK_Model_Brand_BrandSerialID] FOREIGN KEY ([BrandSerialID]) REFERENCES [Inv].[Brand] ([BrandSerialID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inv].[BrandItemType] (
    [BITSerialID] int NOT NULL IDENTITY,
    [BITID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.BITID),
    [BrandSerialID] int NOT NULL,
    [ItemTypeSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BrandItemType] PRIMARY KEY ([BITSerialID]),
    CONSTRAINT [FK_BrandItemType_Brand_BrandSerialID] FOREIGN KEY ([BrandSerialID]) REFERENCES [Inv].[Brand] ([BrandSerialID]) ON DELETE CASCADE,
    CONSTRAINT [FK_BrandItemType_ItemType_ItemTypeSerialID] FOREIGN KEY ([ItemTypeSerialID]) REFERENCES [Inv].[ItemType] ([ItemTypeSerialID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inv].[SubCategory] (
    [SubCategorySerialID] int NOT NULL IDENTITY,
    [SubCategoryID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.SubCategoryID),
    [SubCategoryName] nvarchar(25) NOT NULL,
    [MainCategorySerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_SubCategory] PRIMARY KEY ([SubCategorySerialID]),
    CONSTRAINT [FK_SubCategory_MainCategory_MainCategorySerialID] FOREIGN KEY ([MainCategorySerialID]) REFERENCES [Inv].[MainCategory] ([MainCategorySerialID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inv].[UOMConversion] (
    [UOMConvSerialID] int NOT NULL IDENTITY,
    [UOMConvID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.UOMConvID),
    [UOMSerialID] int NOT NULL,
    [UOMToID] int NOT NULL,
    [ConversionRate] decimal(18,2) NOT NULL,
    [ConversionDescription] nvarchar(max) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UOMConversion] PRIMARY KEY ([UOMConvSerialID]),
    CONSTRAINT [FK_UOMConversion_UOM_UOMSerialID] FOREIGN KEY ([UOMSerialID]) REFERENCES [Inv].[UOM] ([UOMSerialID]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Brand_BrandID] ON [Inv].[Brand] ([BrandID]);
GO

CREATE INDEX [IX_Brand_IsDeleted] ON [Inv].[Brand] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_BrandItemType_BITID] ON [Inv].[BrandItemType] ([BITID]);
GO

CREATE INDEX [IX_BrandItemType_BrandSerialID] ON [Inv].[BrandItemType] ([BrandSerialID]);
GO

CREATE INDEX [IX_BrandItemType_IsDeleted] ON [Inv].[BrandItemType] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_BrandItemType_ItemTypeSerialID] ON [Inv].[BrandItemType] ([ItemTypeSerialID]);
GO

CREATE INDEX [IX_Item_IsDeleted] ON [Inv].[Item] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Item_ItemID] ON [Inv].[Item] ([ItemID]);
GO

CREATE INDEX [IX_ItemType_IsDeleted] ON [Inv].[ItemType] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_ItemType_ItemTypeID] ON [Inv].[ItemType] ([ItemTypeID]);
GO

CREATE INDEX [IX_MainCategory_IsDeleted] ON [Inv].[MainCategory] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_MainCategory_MainCategoryID] ON [Inv].[MainCategory] ([MainCategoryID]);
GO

CREATE INDEX [IX_Model_BrandSerialID] ON [Inv].[Model] ([BrandSerialID]);
GO

CREATE INDEX [IX_Model_IsDeleted] ON [Inv].[Model] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Model_ModelID] ON [Inv].[Model] ([ModelID]);
GO

CREATE INDEX [IX_SubCategory_IsDeleted] ON [Inv].[SubCategory] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_SubCategory_MainCategorySerialID] ON [Inv].[SubCategory] ([MainCategorySerialID]);
GO

CREATE UNIQUE INDEX [IX_SubCategory_SubCategoryID] ON [Inv].[SubCategory] ([SubCategoryID]);
GO

CREATE INDEX [IX_UOM_IsDeleted] ON [Inv].[UOM] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_UOM_UOMID] ON [Inv].[UOM] ([UOMID]);
GO

CREATE INDEX [IX_UOMConversion_IsDeleted] ON [Inv].[UOMConversion] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_UOMConversion_UOMConvID] ON [Inv].[UOMConversion] ([UOMConvID]);
GO

CREATE INDEX [IX_UOMConversion_UOMSerialID] ON [Inv].[UOMConversion] ([UOMSerialID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241124114304_initial', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'UOMSerialID');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [UOMSerialID] int NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'SubCategorySerialID');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [SubCategorySerialID] int NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Size');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Size] nvarchar(max) NOT NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Remarks');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Remarks] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ModelSerialID');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ModelSerialID] int NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'MainCategorySerialID');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [MainCategorySerialID] int NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemTypeSerialID');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemTypeSerialID] int NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemCode');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemCode] nvarchar(max) NOT NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'BrandSerialID');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [BrandSerialID] int NULL;
GO

ALTER SEQUENCE [dbo].[ItemTypeID] RESTART WITH 10;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241125201311_initial1', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'UOMSerialID');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var9 + '];');
UPDATE [Inv].[Item] SET [UOMSerialID] = 0 WHERE [UOMSerialID] IS NULL;
ALTER TABLE [Inv].[Item] ALTER COLUMN [UOMSerialID] int NOT NULL;
ALTER TABLE [Inv].[Item] ADD DEFAULT 0 FOR [UOMSerialID];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'SubCategorySerialID');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var10 + '];');
UPDATE [Inv].[Item] SET [SubCategorySerialID] = 0 WHERE [SubCategorySerialID] IS NULL;
ALTER TABLE [Inv].[Item] ALTER COLUMN [SubCategorySerialID] int NOT NULL;
ALTER TABLE [Inv].[Item] ADD DEFAULT 0 FOR [SubCategorySerialID];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Size');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Size] nvarchar(max) NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ModelSerialID');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var12 + '];');
UPDATE [Inv].[Item] SET [ModelSerialID] = 0 WHERE [ModelSerialID] IS NULL;
ALTER TABLE [Inv].[Item] ALTER COLUMN [ModelSerialID] int NOT NULL;
ALTER TABLE [Inv].[Item] ADD DEFAULT 0 FOR [ModelSerialID];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'MainCategorySerialID');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var13 + '];');
UPDATE [Inv].[Item] SET [MainCategorySerialID] = 0 WHERE [MainCategorySerialID] IS NULL;
ALTER TABLE [Inv].[Item] ALTER COLUMN [MainCategorySerialID] int NOT NULL;
ALTER TABLE [Inv].[Item] ADD DEFAULT 0 FOR [MainCategorySerialID];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemTypeSerialID');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var14 + '];');
UPDATE [Inv].[Item] SET [ItemTypeSerialID] = 0 WHERE [ItemTypeSerialID] IS NULL;
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemTypeSerialID] int NOT NULL;
ALTER TABLE [Inv].[Item] ADD DEFAULT 0 FOR [ItemTypeSerialID];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemPartNo');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemPartNo] nvarchar(max) NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Color');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Color] nvarchar(max) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'BrandSerialID');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var17 + '];');
UPDATE [Inv].[Item] SET [BrandSerialID] = 0 WHERE [BrandSerialID] IS NULL;
ALTER TABLE [Inv].[Item] ALTER COLUMN [BrandSerialID] int NOT NULL;
ALTER TABLE [Inv].[Item] ADD DEFAULT 0 FOR [BrandSerialID];
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Article');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Article] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241126062908_initial2', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [dbo].[BinLctnID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[RackID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[StoreID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[WHID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[ZoneID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[ItemType]') AND [c].[name] = N'ItemTypeSerialID');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[ItemType] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Inv].[ItemType] ALTER COLUMN [ItemTypeSerialID] int NOT NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'SpecialFeatures');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [SpecialFeatures] nvarchar(100) NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Size');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Size] nvarchar(20) NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Remarks');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Remarks] nvarchar(100) NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemPartNo');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemPartNo] nvarchar(50) NULL;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemDes');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemDes] nvarchar(50) NOT NULL;
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemCode');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [ItemCode] nvarchar(30) NOT NULL;
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Guage');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Guage] nvarchar(20) NULL;
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Construction');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Construction] nvarchar(100) NULL;
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Color');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Color] nvarchar(20) NULL;
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'Article');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [Inv].[Item] ALTER COLUMN [Article] nvarchar(50) NULL;
GO

CREATE TABLE [Inv].[Warehouse] (
    [WHSerialID] int NOT NULL IDENTITY,
    [WHID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.WHID),
    [WHName] nvarchar(60) NOT NULL,
    [Address1] nvarchar(60) NOT NULL,
    [Address2] nvarchar(60) NULL,
    [Address3] nvarchar(60) NULL,
    [ComSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Warehouse] PRIMARY KEY ([WHSerialID])
);
GO

CREATE TABLE [Inv].[Store] (
    [StoreSerialID] int NOT NULL IDENTITY,
    [StoreID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.StoreID),
    [ComSerialID] int NOT NULL,
    [StoreName] nvarchar(30) NOT NULL,
    [WHSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY ([StoreSerialID]),
    CONSTRAINT [FK_Store_Warehouse_WHSerialID] FOREIGN KEY ([WHSerialID]) REFERENCES [Inv].[Warehouse] ([WHSerialID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Inv].[Zone] (
    [ZoneSerialID] int NOT NULL IDENTITY,
    [ZoneID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.ZoneID),
    [ZoneName] nvarchar(30) NOT NULL,
    [ComSerialID] int NOT NULL,
    [WHSerialID] int NOT NULL,
    [StoreSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Zone] PRIMARY KEY ([ZoneSerialID]),
    CONSTRAINT [FK_Zone_Store_StoreSerialID] FOREIGN KEY ([StoreSerialID]) REFERENCES [Inv].[Store] ([StoreSerialID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Zone_Warehouse_WHSerialID] FOREIGN KEY ([WHSerialID]) REFERENCES [Inv].[Warehouse] ([WHSerialID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inv].[Rack] (
    [RackSerialID] int NOT NULL IDENTITY,
    [RackID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.RackID),
    [RackName] nvarchar(30) NOT NULL,
    [RackCode] nvarchar(10) NOT NULL,
    [Rows] int NOT NULL,
    [Columns] int NOT NULL,
    [Compartments] int NULL,
    [ComSerialID] int NOT NULL,
    [WHSerialID] int NOT NULL,
    [StoreSerialID] int NOT NULL,
    [ZoneSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Rack] PRIMARY KEY ([RackSerialID]),
    CONSTRAINT [FK_Rack_Store_StoreSerialID] FOREIGN KEY ([StoreSerialID]) REFERENCES [Inv].[Store] ([StoreSerialID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rack_Warehouse_WHSerialID] FOREIGN KEY ([WHSerialID]) REFERENCES [Inv].[Warehouse] ([WHSerialID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rack_Zone_ZoneSerialID] FOREIGN KEY ([ZoneSerialID]) REFERENCES [Inv].[Zone] ([ZoneSerialID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Inv].[BinLocation] (
    [BinLctnSerialID] int NOT NULL IDENTITY,
    [BinLctnID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.BinLctnID),
    [ItemSerialID] int NOT NULL,
    [Row] int NOT NULL,
    [Column] int NOT NULL,
    [Compartment] int NULL,
    [ComSerialID] int NOT NULL,
    [WHSerialID] int NOT NULL,
    [StoreSerialID] int NOT NULL,
    [ZoneSerialID] int NOT NULL,
    [RackSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_BinLocation] PRIMARY KEY ([BinLctnSerialID]),
    CONSTRAINT [FK_BinLocation_Rack_RackSerialID] FOREIGN KEY ([RackSerialID]) REFERENCES [Inv].[Rack] ([RackSerialID]) ON DELETE CASCADE,
    CONSTRAINT [FK_BinLocation_Store_StoreSerialID] FOREIGN KEY ([StoreSerialID]) REFERENCES [Inv].[Store] ([StoreSerialID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BinLocation_Warehouse_WHSerialID] FOREIGN KEY ([WHSerialID]) REFERENCES [Inv].[Warehouse] ([WHSerialID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BinLocation_Zone_ZoneSerialID] FOREIGN KEY ([ZoneSerialID]) REFERENCES [Inv].[Zone] ([ZoneSerialID]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [IX_BinLocation_BinLctnID] ON [Inv].[BinLocation] ([BinLctnID]);
GO

CREATE INDEX [IX_BinLocation_IsDeleted] ON [Inv].[BinLocation] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_BinLocation_RackSerialID] ON [Inv].[BinLocation] ([RackSerialID]);
GO

CREATE INDEX [IX_BinLocation_StoreSerialID] ON [Inv].[BinLocation] ([StoreSerialID]);
GO

CREATE INDEX [IX_BinLocation_WHSerialID] ON [Inv].[BinLocation] ([WHSerialID]);
GO

CREATE INDEX [IX_BinLocation_ZoneSerialID] ON [Inv].[BinLocation] ([ZoneSerialID]);
GO

CREATE INDEX [IX_Rack_IsDeleted] ON [Inv].[Rack] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Rack_RackID] ON [Inv].[Rack] ([RackID]);
GO

CREATE INDEX [IX_Rack_StoreSerialID] ON [Inv].[Rack] ([StoreSerialID]);
GO

CREATE INDEX [IX_Rack_WHSerialID] ON [Inv].[Rack] ([WHSerialID]);
GO

CREATE INDEX [IX_Rack_ZoneSerialID] ON [Inv].[Rack] ([ZoneSerialID]);
GO

CREATE INDEX [IX_Store_IsDeleted] ON [Inv].[Store] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Store_StoreID] ON [Inv].[Store] ([StoreID]);
GO

CREATE INDEX [IX_Store_WHSerialID] ON [Inv].[Store] ([WHSerialID]);
GO

CREATE INDEX [IX_Warehouse_IsDeleted] ON [Inv].[Warehouse] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Warehouse_WHID] ON [Inv].[Warehouse] ([WHID]);
GO

CREATE UNIQUE INDEX [IX_Warehouse_WHSerialID] ON [Inv].[Warehouse] ([WHSerialID]);
GO

CREATE INDEX [IX_Zone_IsDeleted] ON [Inv].[Zone] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_Zone_StoreSerialID] ON [Inv].[Zone] ([StoreSerialID]);
GO

CREATE INDEX [IX_Zone_WHSerialID] ON [Inv].[Zone] ([WHSerialID]);
GO

CREATE UNIQUE INDEX [IX_Zone_ZoneID] ON [Inv].[Zone] ([ZoneID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241207004451_initial3', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [dbo].[CompatibleItemlID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[CusPriceCatID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [Inv].[CompatibleItem] (
    [CompatibleItemSerialID] int NOT NULL IDENTITY,
    [CompatibleItemlID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.CompatibleItemlID),
    [ItemSerialID] int NOT NULL,
    [ItemCompatibleSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CompatibleItem] PRIMARY KEY ([CompatibleItemSerialID]),
    CONSTRAINT [FK_CompatibleItem_Item_ItemCompatibleSerialID] FOREIGN KEY ([ItemCompatibleSerialID]) REFERENCES [Inv].[Item] ([ItemSerialID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CompatibleItem_Item_ItemSerialID] FOREIGN KEY ([ItemSerialID]) REFERENCES [Inv].[Item] ([ItemSerialID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Inv].[CusPriceCategory] (
    [CusPriceCatSerialID] int NOT NULL IDENTITY,
    [CusPriceCatID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.CusPriceCatID),
    [CusPriceCatName] nvarchar(25) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_CusPriceCategory] PRIMARY KEY ([CusPriceCatSerialID])
);
GO

CREATE UNIQUE INDEX [IX_CompatibleItem_CompatibleItemlID] ON [Inv].[CompatibleItem] ([CompatibleItemlID]);
GO

CREATE INDEX [IX_CompatibleItem_IsDeleted] ON [Inv].[CompatibleItem] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_CompatibleItem_ItemCompatibleSerialID] ON [Inv].[CompatibleItem] ([ItemCompatibleSerialID]);
GO

CREATE INDEX [IX_CompatibleItem_ItemSerialID] ON [Inv].[CompatibleItem] ([ItemSerialID]);
GO

CREATE UNIQUE INDEX [IX_CusPriceCategory_CusPriceCatID] ON [Inv].[CusPriceCategory] ([CusPriceCatID]);
GO

CREATE INDEX [IX_CusPriceCategory_IsDeleted] ON [Inv].[CusPriceCategory] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241207195433_initial4', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Inv].[BinLocation] ADD [BinName] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241208223247_initial5', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Inv].[BinLocation] ADD [BinLctn] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209030922_initial6', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[BinLocation]') AND [c].[name] = N'Column');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[BinLocation] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Inv].[BinLocation] ALTER COLUMN [Column] nvarchar(max) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209223725_initial7', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[Item]') AND [c].[name] = N'ItemDes');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[Item] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [Inv].[Item] DROP COLUMN [ItemDes];
GO

ALTER TABLE [Inv].[Item] ADD [ApprovedBy] int NULL;
GO

ALTER TABLE [Inv].[Item] ADD [ApprovedDate] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241220013816_initial8', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Inv].[Parameter];
GO

CREATE TABLE [Inv].[TheNumbers] (
    [TheNumberSerialID] int NOT NULL IDENTITY,
    [TheNumberID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.TheNumberID),
    [TheNumberName] nvarchar(60) NOT NULL,
    [ComSerialID] int NULL,
    [LastNumber] int NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TheNumbers] PRIMARY KEY ([TheNumberSerialID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250125111849_initial9', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [dbo].[GRNID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[GRNLineItemID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[InvoiceID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[SupplierId] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[TheNumbers]') AND [c].[name] = N'LastNumber');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[TheNumbers] DROP CONSTRAINT [' + @var32 + '];');
UPDATE [Inv].[TheNumbers] SET [LastNumber] = 0 WHERE [LastNumber] IS NULL;
ALTER TABLE [Inv].[TheNumbers] ALTER COLUMN [LastNumber] int NOT NULL;
ALTER TABLE [Inv].[TheNumbers] ADD DEFAULT 0 FOR [LastNumber];
GO

CREATE TABLE [Inv].[GRN] (
    [GRNSerialID] int NOT NULL IDENTITY,
    [GRNID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.GRNID),
    [SupplierSerialID] int NOT NULL,
    [ReceivedDate] datetime2 NOT NULL,
    [Notes] nvarchar(max) NULL,
    [GRNNumber] nvarchar(max) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_GRN] PRIMARY KEY ([GRNSerialID])
);
GO

CREATE TABLE [Inv].[GRNLineItem] (
    [GRNLineItemSerialID] int NOT NULL IDENTITY,
    [GRNLineItemID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.GRNLineItemID),
    [ItemSerialID] int NOT NULL,
    [GRNSerialID] int NOT NULL,
    [Quantity] decimal(18,2) NOT NULL,
    [UnitCost] decimal(18,2) NOT NULL,
    [LineTotal] decimal(18,2) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_GRNLineItem] PRIMARY KEY ([GRNLineItemSerialID])
);
GO

CREATE TABLE [Inv].[Invoice] (
    [InvoiceSerialID] int NOT NULL IDENTITY,
    [InvoiceID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.InvoiceID),
    [SupplierSerialID] int NOT NULL,
    [GRNSerialID] int NOT NULL,
    [InvoiceDate] datetime2 NOT NULL,
    [InvoiceTotal] decimal(18,2) NOT NULL,
    [InvoiceNumber] nvarchar(max) NOT NULL,
    [LastNumber] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Invoice] PRIMARY KEY ([InvoiceSerialID])
);
GO

CREATE TABLE [Inv].[Supplier] (
    [SupplierSerialId] int NOT NULL IDENTITY,
    [SupplierId] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.SupplierId),
    [SupplierName] nvarchar(200) NOT NULL,
    [ContactPerson] nvarchar(200) NOT NULL,
    [Phone] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NULL,
    [Address] nvarchar(250) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Supplier] PRIMARY KEY ([SupplierSerialId])
);
GO

CREATE UNIQUE INDEX [IX_GRN_GRNID] ON [Inv].[GRN] ([GRNID]);
GO

CREATE INDEX [IX_GRN_IsDeleted] ON [Inv].[GRN] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_GRNLineItem_GRNLineItemID] ON [Inv].[GRNLineItem] ([GRNLineItemID]);
GO

CREATE INDEX [IX_GRNLineItem_IsDeleted] ON [Inv].[GRNLineItem] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Invoice_InvoiceID] ON [Inv].[Invoice] ([InvoiceID]);
GO

CREATE INDEX [IX_Invoice_IsDeleted] ON [Inv].[Invoice] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_Supplier_IsDeleted] ON [Inv].[Supplier] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_Supplier_SupplierId] ON [Inv].[Supplier] ([SupplierId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250221025149_initial10', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE INDEX [IX_Invoice_GRNSerialID] ON [Inv].[Invoice] ([GRNSerialID]);
GO

CREATE INDEX [IX_Invoice_SupplierSerialID] ON [Inv].[Invoice] ([SupplierSerialID]);
GO

CREATE INDEX [IX_GRNLineItem_GRNSerialID] ON [Inv].[GRNLineItem] ([GRNSerialID]);
GO

CREATE INDEX [IX_GRNLineItem_ItemSerialID] ON [Inv].[GRNLineItem] ([ItemSerialID]);
GO

CREATE INDEX [IX_GRN_SupplierSerialID] ON [Inv].[GRN] ([SupplierSerialID]);
GO

ALTER TABLE [Inv].[GRN] ADD CONSTRAINT [FK_GRN_Supplier_SupplierSerialID] FOREIGN KEY ([SupplierSerialID]) REFERENCES [Inv].[Supplier] ([SupplierSerialId]) ON DELETE NO ACTION;
GO

ALTER TABLE [Inv].[GRNLineItem] ADD CONSTRAINT [FK_GRNLineItem_GRN_GRNSerialID] FOREIGN KEY ([GRNSerialID]) REFERENCES [Inv].[GRN] ([GRNSerialID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Inv].[GRNLineItem] ADD CONSTRAINT [FK_GRNLineItem_Item_ItemSerialID] FOREIGN KEY ([ItemSerialID]) REFERENCES [Inv].[Item] ([ItemSerialID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Inv].[Invoice] ADD CONSTRAINT [FK_Invoice_GRN_GRNSerialID] FOREIGN KEY ([GRNSerialID]) REFERENCES [Inv].[GRN] ([GRNSerialID]) ON DELETE NO ACTION;
GO

ALTER TABLE [Inv].[Invoice] ADD CONSTRAINT [FK_Invoice_Supplier_SupplierSerialID] FOREIGN KEY ([SupplierSerialID]) REFERENCES [Inv].[Supplier] ([SupplierSerialId]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250221030956_initial11', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [dbo].[InvoiceItemID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[POID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE SEQUENCE [dbo].[POItemID] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

ALTER TABLE [Inv].[ItemType] ADD [Code] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Inv].[Invoice] ADD [Status] nvarchar(max) NOT NULL DEFAULT N'';
GO

ALTER TABLE [Inv].[GRN] ADD [PurchaseOrderPOSerialID] int NULL;
GO

CREATE TABLE [Inv].[InvoiceItem] (
    [InvoiceSerialID] int NOT NULL,
    [InvoiceItemSerialID] int NOT NULL,
    [InvoiceItemID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.InvoiceItemID),
    [GRNLineItemSerialID] int NOT NULL,
    [ItemSerialID] int NOT NULL,
    [BilledQty] decimal(18,2) NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [BilledAmount] decimal(18,2) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_InvoiceItem] PRIMARY KEY ([InvoiceSerialID]),
    CONSTRAINT [FK_InvoiceItem_GRNLineItem_GRNLineItemSerialID] FOREIGN KEY ([GRNLineItemSerialID]) REFERENCES [Inv].[GRNLineItem] ([GRNLineItemSerialID]) ON DELETE CASCADE,
    CONSTRAINT [FK_InvoiceItem_Invoice_InvoiceSerialID] FOREIGN KEY ([InvoiceSerialID]) REFERENCES [Inv].[Invoice] ([InvoiceSerialID]) ON DELETE CASCADE
);
GO

CREATE TABLE [Inv].[PurchaseOrder] (
    [POSerialID] int NOT NULL IDENTITY,
    [POID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.POID),
    [SupplierSerialID] int NOT NULL,
    [OrderDate] datetime2 NOT NULL,
    [TotalAmount] decimal(18,2) NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY ([POSerialID])
);
GO

CREATE TABLE [Inv].[PurchaseOrderItem] (
    [POSerialID] int NOT NULL IDENTITY,
    [POItemSerialID] int NOT NULL,
    [POItemID] int NOT NULL DEFAULT (NEXT VALUE FOR dbo.POItemID),
    [ItemSerialID] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [PurchaseOrderPOSerialID] int NOT NULL,
    [CreatedBy] int NULL,
    [CreatedDate] datetime2 NULL,
    [ModifiedBy] int NULL,
    [ModifiedDate] datetime2 NULL,
    [Active] bit NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_PurchaseOrderItem] PRIMARY KEY ([POSerialID]),
    CONSTRAINT [FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID] FOREIGN KEY ([PurchaseOrderPOSerialID]) REFERENCES [Inv].[PurchaseOrder] ([POSerialID]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_GRN_PurchaseOrderPOSerialID] ON [Inv].[GRN] ([PurchaseOrderPOSerialID]);
GO

CREATE INDEX [IX_InvoiceItem_GRNLineItemSerialID] ON [Inv].[InvoiceItem] ([GRNLineItemSerialID]);
GO

CREATE UNIQUE INDEX [IX_InvoiceItem_InvoiceItemSerialID] ON [Inv].[InvoiceItem] ([InvoiceItemSerialID]);
GO

CREATE INDEX [IX_InvoiceItem_IsDeleted] ON [Inv].[InvoiceItem] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE INDEX [IX_PurchaseOrder_IsDeleted] ON [Inv].[PurchaseOrder] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_PurchaseOrder_POID] ON [Inv].[PurchaseOrder] ([POID]);
GO

CREATE INDEX [IX_PurchaseOrder_SupplierSerialId] ON [Inv].[PurchaseOrder] ([SupplierSerialId]);
GO

CREATE INDEX [IX_PurchaseOrderItem_IsDeleted] ON [Inv].[PurchaseOrderItem] ([IsDeleted]) WHERE [IsDeleted] = 1;
GO

CREATE UNIQUE INDEX [IX_PurchaseOrderItem_POItemID] ON [Inv].[PurchaseOrderItem] ([POItemID]);
GO

CREATE INDEX [IX_PurchaseOrderItem_PurchaseOrderPOSerialID] ON [Inv].[PurchaseOrderItem] ([PurchaseOrderPOSerialID]);
GO

ALTER TABLE [Inv].[GRN] ADD CONSTRAINT [FK_GRN_PurchaseOrder_PurchaseOrderPOSerialID] FOREIGN KEY ([PurchaseOrderPOSerialID]) REFERENCES [Inv].[PurchaseOrder] ([POSerialID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250311195753_initial12', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Inv].[GRNLineItem] DROP CONSTRAINT [FK_GRNLineItem_Item_ItemSerialID];
GO

ALTER TABLE [Inv].[PurchaseOrderItem] DROP CONSTRAINT [FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID];
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[GRNLineItem]') AND [c].[name] = N'LineTotal');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[GRNLineItem] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [Inv].[GRNLineItem] DROP COLUMN [LineTotal];
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[GRNLineItem]') AND [c].[name] = N'Quantity');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[GRNLineItem] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [Inv].[GRNLineItem] DROP COLUMN [Quantity];
GO

EXEC sp_rename N'[Inv].[GRNLineItem].[UnitCost]', N'ReceivedQty', N'COLUMN';
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[PurchaseOrderItem]') AND [c].[name] = N'Quantity');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[PurchaseOrderItem] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [Inv].[PurchaseOrderItem] ALTER COLUMN [Quantity] decimal(18,2) NOT NULL;
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[PurchaseOrderItem]') AND [c].[name] = N'PurchaseOrderPOSerialID');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[PurchaseOrderItem] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Inv].[PurchaseOrderItem] ALTER COLUMN [PurchaseOrderPOSerialID] int NULL;
GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[GRNLineItem]') AND [c].[name] = N'ItemSerialID');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[GRNLineItem] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Inv].[GRNLineItem] ALTER COLUMN [ItemSerialID] int NULL;
GO

ALTER TABLE [Inv].[GRNLineItem] ADD [POItemPOSerialID] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Inv].[GRNLineItem] ADD [POItemSerialID] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Inv].[GRN] ADD [POSerialID] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_GRNLineItem_POItemPOSerialID] ON [Inv].[GRNLineItem] ([POItemPOSerialID]);
GO

ALTER TABLE [Inv].[GRNLineItem] ADD CONSTRAINT [FK_GRNLineItem_Item_ItemSerialID] FOREIGN KEY ([ItemSerialID]) REFERENCES [Inv].[Item] ([ItemSerialID]);
GO

ALTER TABLE [Inv].[GRNLineItem] ADD CONSTRAINT [FK_GRNLineItem_PurchaseOrderItem_POItemPOSerialID] FOREIGN KEY ([POItemPOSerialID]) REFERENCES [Inv].[PurchaseOrderItem] ([POSerialID]) ON DELETE CASCADE;
GO

ALTER TABLE [Inv].[PurchaseOrderItem] ADD CONSTRAINT [FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID] FOREIGN KEY ([PurchaseOrderPOSerialID]) REFERENCES [Inv].[PurchaseOrder] ([POSerialID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250311201801_initial13', N'8.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Inv].[GRN] DROP CONSTRAINT [FK_GRN_PurchaseOrder_PurchaseOrderPOSerialID];
GO

DROP INDEX [IX_GRN_PurchaseOrderPOSerialID] ON [Inv].[GRN];
GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[GRN]') AND [c].[name] = N'PurchaseOrderPOSerialID');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[GRN] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Inv].[GRN] DROP COLUMN [PurchaseOrderPOSerialID];
GO

DROP INDEX [IX_PurchaseOrder_SupplierSerialID] ON [Inv].[PurchaseOrder];
DECLARE @var39 sysname;
SELECT @var39 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Inv].[PurchaseOrder]') AND [c].[name] = N'SupplierSerialID');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Inv].[PurchaseOrder] DROP CONSTRAINT [' + @var39 + '];');
UPDATE [Inv].[PurchaseOrder] SET [SupplierSerialID] = 0 WHERE [SupplierSerialID] IS NULL;
ALTER TABLE [Inv].[PurchaseOrder] ALTER COLUMN [SupplierSerialID] int NOT NULL;
ALTER TABLE [Inv].[PurchaseOrder] ADD DEFAULT 0 FOR [SupplierSerialID];
CREATE INDEX [IX_PurchaseOrder_SupplierSerialID] ON [Inv].[PurchaseOrder] ([SupplierSerialID]);
GO

CREATE INDEX [IX_GRN_POSerialID] ON [Inv].[GRN] ([POSerialID]);
GO

ALTER TABLE [Inv].[GRN] ADD CONSTRAINT [FK_GRN_PurchaseOrder_POSerialID] FOREIGN KEY ([POSerialID]) REFERENCES [Inv].[PurchaseOrder] ([POSerialID]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250311203107_initial14', N'8.0.2');
GO

COMMIT;
GO

