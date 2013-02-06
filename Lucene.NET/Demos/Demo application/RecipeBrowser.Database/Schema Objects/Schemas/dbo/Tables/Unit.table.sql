CREATE TABLE [dbo].[Unit](
	[UnitId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[Version] [timestamp] NOT NULL,
	CONSTRAINT [PK_Unit] PRIMARY KEY CLUSTERED ([UnitId] ASC)
)
