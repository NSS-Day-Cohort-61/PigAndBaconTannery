CREATE TABLE [Product] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(55) NOT NULL,
  [Price] decimal(5, 2) NOT NULL,
  [Quantity] int,
  [VendorId] int
)
GO

CREATE TABLE [ProductDetail] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Description] nvarchar(255) NOT NULL,
  [Weight] int NOT NULL,
  [ProductId] int NOT NULL
)
GO

CREATE TABLE [ProductCategory] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ProductId] int NOT NULL,
  [CategoryId] int NOT NULL
)
GO

CREATE TABLE [Category] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(55) NOT NULL
)
GO

CREATE TABLE [Vendor] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] varchar(75) NOT NULL,
  [Address] nvarchar(255)
)
GO

ALTER TABLE [Product] ADD FOREIGN KEY ([VendorId]) REFERENCES [Vendor] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductCategory] ADD FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [ProductCategory] ADD FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Product] ADD FOREIGN KEY ([Id]) REFERENCES [ProductDetail] ([ProductId])
GO
