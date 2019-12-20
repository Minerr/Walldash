IF OBJECT_ID(N'dbo.[Account]', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.[Account]
	(
		Id		int				NOT NULL IDENTITY(10000, 1) PRIMARY KEY,
		Alias	nvarchar(100)	NOT NULL,
	);
END
GO

IF OBJECT_ID(N'dbo.[Metric]', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.[Metric]
	(
		Id			int				NOT NULL IDENTITY(10000, 1) PRIMARY KEY,
		AccountId	int				NOT NULL FOREIGN KEY REFERENCES dbo.[Account](Id),
		Alias		nvarchar(100)	NOT NULL,
		Number		float			NOT NULL,
		Timestamp	datetimeoffset	NOT NULL
	);
END
GO

IF OBJECT_ID(N'dbo.[User]', N'U') IS NULL
BEGIN
	CREATE TABLE dbo.[User]
	(
		Id			int				NOT NULL IDENTITY(10000, 1) PRIMARY KEY,
		AccountId	int				NOT NULL FOREIGN KEY REFERENCES dbo.[Account](Id),
		FirstName	nvarchar(100)	NOT NULL,
		LastName	nvarchar(100)	NOT NULL,
		Email		nvarchar(250)	NOT NULL,
		Password	nvarchar(MAX)	NOT NULL,
		DateCreated	datetimeoffset	NOT NULL
	);
END
GO