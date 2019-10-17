/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO EquipmentCategory(Name) values('Head'); 
INSERT INTO EquipmentCategory(Name) values('Shoulders'); 
INSERT INTO EquipmentCategory(Name) values('Chest'); 
INSERT INTO EquipmentCategory(Name) values('Belt'); 
INSERT INTO EquipmentCategory(Name) values('Legs'); 
INSERT INTO EquipmentCategory(Name) values('Boots'); 

INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('Gucci pet',10,-100,5, 1, 20);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('gekke nektas',32,80,23, 2, 10);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('borsthaar',23,-21,12, 3, 25);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('Gucci riem',34,40,5, 4, 49);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('beenwarmers',56,56,-56, 5, 34);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('slippers met sokken',-34,45,434, 6, 23);

INSERT INTO Ninja(Name,Gold) Values('piet', 2000)
INSERT INTO Ninja(Name,Gold) Values('jan', 2000)
