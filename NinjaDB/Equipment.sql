CREATE TABLE [dbo].[Equipment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Strength] INT NOT NULL DEFAULT 0, 
    [Intelligence] INT NOT NULL DEFAULT 0, 
    [Agility] INT NOT NULL DEFAULT 0, 
    [Category] INT NOT NULL, 
    [Price] INT NOT NULL, 
    CONSTRAINT [FK_EquipmentCategory] FOREIGN KEY ([Category]) REFERENCES [dbo].[EquipmentCategory]([CategoryId])
)
