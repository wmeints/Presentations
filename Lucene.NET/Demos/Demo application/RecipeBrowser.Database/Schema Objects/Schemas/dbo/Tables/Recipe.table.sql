CREATE TABLE [dbo].[Recipe](
	[RecipeId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Image] [varbinary](50) NULL,
	[Rating] [decimal](18, 2) NOT NULL,
	[CookingInstructions] [nvarchar](max) NOT NULL,
	[TimeNeeded] [time](7) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[Version] [timestamp] NOT NULL,
	CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED ([RecipeId] ASC)
) 
