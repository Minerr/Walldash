-- Get
CREATE OR ALTER PROCEDURE dbo.[Metric_Get]
(
	@AccountId int,
	@MetricId int
)
AS
    SET NOCOUNT ON;

	SELECT
		m.Id,
		m.AccountId,
		m.Alias,
		m.Number,
		m.Timestamp
	FROM 
		dbo.Metric m
	WHERE 
		m.AccountId = @AccountId
	AND m.Id = @MetricId
GO

-- Get All
CREATE OR ALTER PROCEDURE dbo.[Metric_GetAll]
(
	@AccountId int
)
AS
    SET NOCOUNT ON;

	SELECT
		m.Id,
		m.AccountId,
		m.Alias,
		m.Number,
		m.Timestamp
	FROM 
		dbo.Metric m
	WHERE 
		m.AccountId = @AccountId
GO

-- Save
CREATE OR ALTER PROCEDURE dbo.[Metric_Save]
(
	@AccountId int,
	@Alias nvarchar(100),			
	@Number float,	
	@Timestamp datetimeoffset
)				
AS
    SET NOCOUNT ON;

    INSERT INTO dbo.Metric(AccountId, Alias, Number, Timestamp) 
	VALUES(@AccountId, @Alias, @Number, @Timestamp)

    SELECT SCOPE_IDENTITY();
GO

-- Update
CREATE OR ALTER PROCEDURE dbo.[Metric_Update]
(
	@MetricId int,
	@AccountId int,
	@Alias nvarchar(100),			
	@Number float,	
	@Timestamp datetimeoffset
)				
AS
    SET NOCOUNT ON;

	BEGIN
		UPDATE m
		SET
			m.Alias = @Alias,
			m.Number = @Number,
			m.Timestamp = @Timestamp
		FROM dbo.Metric m
		WHERE
			m.AccountId = @AccountId
		AND m.Id = @MetricId
	END
GO

-- Delete
CREATE OR ALTER PROCEDURE dbo.[Metric_Delete]
(
	@MetricId int,
	@AccountId int
)				
AS
    SET NOCOUNT ON;

	BEGIN
		DELETE 
		FROM 
			dbo.Metric
		WHERE 
			AccountId = @AccountId
		AND	Id = @MetricId
	END
GO

-- Get all by Alias
CREATE OR ALTER PROCEDURE dbo.[Metric_GetAllByAlias]
(
	@AccountId int,
	@Alias int
)
AS
    SET NOCOUNT ON;

	SELECT
		m.Id,
		m.AccountId,
		m.Alias,
		m.Number,
		m.Timestamp
	FROM 
		dbo.Metric m
	WHERE 
		m.AccountId = @AccountId
	AND m.Alias = @Alias
GO

-- Get all by datetime
CREATE OR ALTER PROCEDURE dbo.[Metric_GetAllByPeriod]
(
	@AccountId int,
	@DateFrom datetimeoffset,
	@DateTo datetimeoffset
)
AS
    SET NOCOUNT ON;

	SELECT
		m.Id,
		m.AccountId,
		m.Alias,
		m.Number,
		m.Timestamp
	FROM 
		dbo.Metric m
	WHERE 
		m.AccountId = @AccountId
	AND m.Timestamp BETWEEN @DateFrom AND @DateTo
GO


-- Get all aliases
CREATE OR ALTER PROCEDURE dbo.[Metric_GetAliases]
(
	@AccountId int
)
AS
    SET NOCOUNT ON;

	SELECT
		DISTINCT(m.Alias)
	FROM 
		dbo.Metric m
	WHERE 
		m.AccountId = @AccountId
	ORDER BY m.Alias ASC
GO