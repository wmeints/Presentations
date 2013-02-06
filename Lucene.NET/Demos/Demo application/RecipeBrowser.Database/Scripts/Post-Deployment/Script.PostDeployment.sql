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

DELETE FROM Ingredient
DELETE FROM RecipeCategory
DELETE FROM Recipe
DELETE FROM Category
DELETE FROM Unit
GO

SET IDENTITY_INSERT [dbo].[Category] ON
GO

INSERT INTO [Category]
           ([CategoryId],[Name],[Image],[IsMenuCourse],[IsKitchenType],[IsCookingTechnique]
           ,[IsTheme],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy])
     VALUES (1,'Voorgerecht',0x0,1,0,0,0,GETDATE(),null,'willemm',null)

INSERT INTO [Category]
           ([CategoryId],[Name],[Image],[IsMenuCourse],[IsKitchenType],[IsCookingTechnique]
           ,[IsTheme],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy])
     VALUES (2,'Tussengerecht',0x0,1,0,0,0,GETDATE(),null,'willemm',null)

INSERT INTO [Category]
           ([CategoryId],[Name],[Image],[IsMenuCourse],[IsKitchenType],[IsCookingTechnique]
           ,[IsTheme],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy])
     VALUES (3,'Hoofdgerecht',0x0,1,0,0,0,GETDATE(),null,'willemm',null)

INSERT INTO [Category]
           ([CategoryId],[Name],[Image],[IsMenuCourse],[IsKitchenType],[IsCookingTechnique]
           ,[IsTheme],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy])
     VALUES (4,'Nagerecht',0x0,1,0,0,0,GETDATE(),null,'willemm',null)
GO

SET IDENTITY_INSERT [Category] OFF
GO

SET IDENTITY_INSERT [Unit] ON
GO

INSERT INTO [Unit] ([UnitId],[Name],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy]) VALUES (1,'gram',GETDATE(),null,'willemm',null)
INSERT INTO [Unit] ([UnitId],[Name],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy]) VALUES (2,'ml',GETDATE(),null,'willemm',null)
INSERT INTO [Unit] ([UnitId],[Name],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy]) VALUES (3,'stuks',GETDATE(),null,'willemm',null)
INSERT INTO [Unit] ([UnitId],[Name],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy]) VALUES (4,'kg',GETDATE(),null,'willemm',null)
INSERT INTO [Unit] ([UnitId],[Name],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy]) VALUES (5,'dl',GETDATE(),null,'willemm',null)
INSERT INTO [Unit] ([UnitId],[Name],[DateCreated],[DateModified],[CreatedBy],[ModifiedBy]) VALUES (6,'eetlepels',GETDATE(),null,'willemm',null)
GO

SET IDENTITY_INSERT [Unit] OFF
GO

SET IDENTITY_INSERT [Recipe] ON
GO

INSERT INTO [Recipe]
           (RecipeId
		   ,[Name]
           ,[Description]
           ,[Image]
           ,[Rating]
           ,[CookingInstructions]
           ,[TimeNeeded]
           ,[DateCreated]
           ,[DateModified]
           ,[CreatedBy]
           ,[ModifiedBy])
     VALUES
           (1
		   ,'Stamppot'
           ,'Een oerhollandse stamppot'
           ,0x0
           ,5
           ,'Giet een laagje water van ca. 4 cm in een pan. Voeg er de aardappels met wat zout aan toe en breng aan de kook. Leg, zodra de aardappels koken, de boerenkool erop en de rookworst. Draai het vuur lager en kook de aardappels en groenten - met het deksel op de op pan - in circa 15-20 minuten gaar. Prik in de aardappels om te voelen of ze zacht zijn. Bak intussen in een droge koekenpan de spekblokjes uit. Giet het kookvocht af. Stamp de aardappels en de boerenkool door elkaar met een stamper. Roer de boter erdoor en maak de stamppot wat smeuiger met de warme melk. Schep er de spekblokjes door en breng de stamppot op smaak met zout en peper en een scheutje azijn. Serveer de stamppot met de rookworst.'
           ,'00:30'
           ,GETDATE()
           ,null
           ,'willemm'
           ,null)
GO

SET IDENTITY_INSERT [Recipe] OFF
GO

INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Aardappelen',null,1,4)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Boerenkool',null,500,1)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Rookworst',null,1,null)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Magere spekblokjes',null,100,1)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Zout',null,null,null)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Peper',null,null,null)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Boter',null,20,1)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Melk',null,1.5,5)
INSERT INTO [Ingredient]([RecipeId],[Name],[Description],[Amount],[UnitId])VALUES(1,'Azijn',null,2,6)
GO
