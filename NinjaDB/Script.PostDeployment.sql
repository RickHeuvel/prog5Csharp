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
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('tinfoil head',12,-12,32, 1, 10);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('vergiet',10,100,50, 1, 2);

INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('gekke nektas',32,80,23, 2, 10);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('schoudervulling',34,56,12, 2, 89);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('rugzak',23,43,12, 2, 32);

INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('borsthaar',23,-21,12, 3, 25);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('nog meer borsthaar',24,-21,12, 3, 25);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('gek veel borsthaar',25,-21,12, 3, 25);

INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('Gucci riem',34,40,5, 4, 49);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('hippe ninja riem',32,45,15, 4, 32);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('geen riem',342,23,32, 4, 324);

INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('beenwarmers',56,56,-56, 5, 34);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('gele zwembroek',232,13,-342, 5, 342);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('idk',342,23,-5634, 5, 32);

INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('slippers met sokken',-34,45,434, 6, 23);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('klompen',34,32,434, 6, 12);
INSERT INTO Equipment(Name, Strength, Intelligence, Agility, CategoryId, Price) Values('ninja pantoffels',3223,243,434, 6, 23);

INSERT INTO Ninja(Name,Gold) Values('piet', 2000)
INSERT INTO Ninja(Name,Gold) Values('jan', 2000)
