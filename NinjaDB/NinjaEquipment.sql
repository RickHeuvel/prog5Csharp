CREATE TABLE [dbo].[NinjaEquipment]
(
	[NinjaId] INT NOT NULL,
    [EquipmentId] INT NOT NULL, 

	PRIMARY KEY (NinjaId, EquipmentId), 
    CONSTRAINT [FK_NinjaEquipment_ToNinja] FOREIGN KEY (NinjaId) REFERENCES Ninja(Id), 
    CONSTRAINT [FK_NinjaEquipment_ToEquipment] FOREIGN KEY (EquipmentId) REFERENCES Equipment(Id),




)
