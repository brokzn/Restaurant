USE [Restaurant]
GO

-- Создание функции
CREATE FUNCTION dbo.GetTotalSalary(@PostCode INT)
RETURNS DECIMAL(10, 2)
AS
BEGIN
    DECLARE @TotalSalary DECIMAL(10, 2)

    SELECT @TotalSalary = SUM(CAST(Salary AS DECIMAL(10, 2)))
    FROM dbo.Restaurant_Posts
    WHERE Post_Code = @PostCode

    RETURN @TotalSalary
END