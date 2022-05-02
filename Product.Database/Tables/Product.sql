CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Guid] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
	[Name] NVARCHAR(250) NOT NULL,
	[ProductGroupNk] NVARCHAR(10) NOT NULL,
	[Price] DECIMAL(18,2) NOT NULL,

    [Comments] NVARCHAR(250) NULL, 
    CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY ([ProductGroupNk]) REFERENCES [dbo].[ProductGroup] ([Nk]),

)

