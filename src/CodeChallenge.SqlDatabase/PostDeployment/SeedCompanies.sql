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
