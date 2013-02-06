CREATE TABLE [dbo].[Category](
	[CategoryId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Image] [varbinary](max) NULL,
	[IsMenuCourse] [bit] NOT NULL,
	[IsKitchenType] [bit] NOT NULL,
	[IsCookingTechnique] [bit] NOT NULL,
	[IsTheme] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[Version] [timestamp] NOT NULL,
	CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
)