CREATE TRIGGER CheckEmployeeAge
ON dbo.Restaurant_Employees
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT * FROM inserted WHERE Age < 18 OR Age > 65)
    BEGIN
        RAISERROR('Недопустимый возраст сотрудника. Возраст должен быть от 18 до 65 лет.', 16, 1)
        ROLLBACK TRANSACTION
    END
END