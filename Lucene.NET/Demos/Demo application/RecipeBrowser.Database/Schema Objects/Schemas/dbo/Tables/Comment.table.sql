CREATE TABLE [dbo].[Comment](
	[CommentId] [decimal](18, 0) IDENTITY(1,1) NOT NULL,
	[RecipeId] [decimal](18, 0) NOT NULL,
	[CommentText] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[Version] [timestamp] NOT NULL,
	CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([CommentId] ASC),
	CONSTRAINT [FK_Comment_Recipe] FOREIGN KEY ([RecipeId]) REFERENCES [Recipe] ([RecipeId])
)