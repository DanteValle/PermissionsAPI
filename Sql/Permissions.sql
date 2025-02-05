USE [N5]
GO

/****** Object:  Table [dbo].[Permissions]    Script Date: 2/5/2025 5:16:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameEmployee] [nvarchar](20) NOT NULL,
	[LastNameEmployee] [nvarchar](20) NOT NULL,
	[PermissionTypeId] [int] NOT NULL,
	[PermissionDate] [date] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PermissionTypes] FOREIGN KEY([PermissionTypeId])
REFERENCES [dbo].[PermissionTypes] ([Id])
GO

ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_PermissionTypes]
GO


