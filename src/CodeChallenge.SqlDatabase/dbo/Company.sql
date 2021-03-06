﻿CREATE TABLE [dbo].[Company]
(
	[CompanyId] INT NOT NULL IDENTITY(1,1),
	[Name] nvarchar(100) NOT NULL,
	[Exchange] NVARCHAR(100) NOT NULL,
	[Ticker] NVARCHAR(5) NOT NULL,
	[ISIN] VARCHAR(12) NOT NULL,
	[Website] NVARCHAR(120),

	CONSTRAINT PK_Company PRIMARY KEY ([CompanyId]),
	CONSTRAINT CHK_Company_Length_ISIN CHECK (LEN([ISIN]) = 12)
)

GO;

CREATE UNIQUE INDEX IX_UQ_Company_ISIN
	ON Company(ISIN);
