
CREATE PROCEDURE AddEmployee
    @Lastname VARCHAR(100),
    @Firstname VARCHAR(100),
    @Middlename VARCHAR(100),
    @Age VARCHAR(100),
    @Gender VARCHAR(1),
    @Adress VARCHAR(100),
    @Phone VARCHAR(100),
    @Passport VARCHAR(100),
    @Post_Code INT
AS
BEGIN
    INSERT INTO dbo.Restaurant_Employees (Lastname, Firstname, Middlename, Age, Gender, Adress, Phone, Passport, Post_Code)
    VALUES (@Lastname, @Firstname, @Middlename, @Age, @Gender, @Adress, @Phone, @Passport, @Post_Code)
END