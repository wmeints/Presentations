CREATE TABLE [dbo].[Ingredient](
	[IngredientId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[RecipeId] [decimal](18, 0) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Amount] [decimal](18, 0) NULL,
	[UnitId] [decimal](18, 0) NULL,
	[Version] [timestamp] NOT NULL,
	CONSTRAINT [PK_Ingredient] PRIMARY KEY CLUSTERED ([IngredientId] ASC),
	CONSTRAINT [FK_Ingredient_Recipe] FOREIGN KEY ([RecipeId]) REFERENCES [Recipe] ([RecipeId]),
	CONSTRAINT [FK_Ingredient_Unit] FOREIGN KEY ([UnitId]) REFERENCES [Unit] ([UnitId])
) 