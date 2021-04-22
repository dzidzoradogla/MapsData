USE [MapsData]
GO

/****** Object:  Table [dbo].[locationData]    Script Date: 4/21/2021 10:53:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[locationData](
	[locationID] [nvarchar](50) NOT NULL,
	[time] [datetime] NOT NULL,
	[AtmosphericPressure] [nvarchar](50) NULL,
	[WindDirection] [nvarchar](50) NULL,
	[WindSpeed] [nvarchar](50) NULL,
	[Gust] [nvarchar](50) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


