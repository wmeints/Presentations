CREATE TABLE [dbo].[RecipeCategory](
	[RecipeId] [decimal](18, 0) NOT NULL,
	[CategoryId] [decimal](18, 0) NOT NULL,
	CONSTRAINT [PK_RecipeCategory] PRIMARY KEY CLUSTERED ([RecipeId] ASC,[CategoryId] ASC),
	CONSTRAINT [FK_RecipeCategory_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]),
	CONSTRAINT [FK_RecipeCategory_Recipe] FOREIGN KEY ([RecipeId]) REFERENCES [Recipe] ([RecipeId])
) 