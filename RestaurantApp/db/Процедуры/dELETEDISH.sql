CREATE PROCEDURE DeleteDish
    @Dish_code INT
AS
BEGIN
    DELETE FROM dbo.Menu
    WHERE Dish_code = @Dish_code
END
