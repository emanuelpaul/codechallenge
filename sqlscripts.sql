PRINT N'Creating [dbo].[Company]...';


GO
CREATE TABLE [dbo].[Company] (
    [CompanyId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Exchange]  NVARCHAR (100) NOT NULL,
    [Ticker]    NVARCHAR (5)   NOT NULL,
    [ISIN]      VARCHAR (12)   NOT NULL,
    [Website]   NVARCHAR (120) NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);


GO
PRINT N'Creating [dbo].[Company].[IX_UQ_Company_ISIN]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UQ_Company_ISIN]
    ON [dbo].[Company]([ISIN] ASC);


GO
PRINT N'Creating [dbo].[CHK_Company_Length_ISIN]...';


GO
ALTER TABLE [dbo].[Company] WITH NOCHECK
    ADD CONSTRAINT [CHK_Company_Length_ISIN] CHECK (LEN([ISIN]) = 12);


GO
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

DECLARE @companies TABLE([Name] nvarchar(100) NOT NULL,
	[Exchange] NVARCHAR(100) NOT NULL,
	[Ticker] NVARCHAR(5) NOT NULL,
	[ISIN] VARCHAR(12) NOT NULL,
	[Website] NVARCHAR(120));

INSERT INTO @companies([Name], [Exchange], [Ticker], [ISIN], [Website])
VALUES ('Microsoft', 'NYSE', 'MSFT', 'US5949181045', 'https://microsoft.com'),
	   ('Tesla', 'NYSE', 'TSLA', 'US88160R1014', 'https://tesla.com'),
       ('Alphabet', 'NYSE', 'GOOGL', 'US02079K3059', 'https://google.com')

MERGE [Company] AS [target]
USING @companies AS [source]
ON [source].ISIN = [target].ISIN
WHEN NOT MATCHED BY TARGET
	THEN INSERT([Name], [Exchange], [Ticker], [ISIN], [Website]) VALUES([Name], [Exchange], [Ticker], [ISIN], [Website]);

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
ALTER TABLE [dbo].[Company] WITH CHECK CHECK CONSTRAINT [CHK_Company_Length_ISIN];


GO
PRINT N'Update complete.';


GO
