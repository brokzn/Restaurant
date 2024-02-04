USE [Restaurant]

IF OBJECT_ID('dbo.Ticket', 'U') IS NOT NULL
DROP TABLE dbo.Ticket;
IF OBJECT_ID('dbo.Menu', 'U') IS NOT NULL
DROP TABLE dbo.Menu ;
IF OBJECT_ID('dbo.Restaurant_Storage', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant_Storage;
IF OBJECT_ID('dbo.Restaurant_Employees', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant_Employees;
IF OBJECT_ID('dbo.Restaurant_Posts', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant_Posts;



IF OBJECT_ID('dbo.Restaurant_Posts', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant_Posts;
CREATE TABLE dbo.Restaurant_Posts
(
Post_Code INT IDENTITY(1,1) CONSTRAINT Post_Code PRIMARY KEY NOT NULL,
Post_Name VARCHAR(100) NOT NULL,
Salary VARCHAR(100) NOT NULL,
Responsibilities VARCHAR(100) NOT NULL,
Requirements VARCHAR(100) NOT NULL,

);


IF OBJECT_ID('dbo.Restaurant_Employees', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant_Employees;
CREATE TABLE dbo.Restaurant_Employees
(
Employee_code INT IDENTITY(1,1) CONSTRAINT Employee_code PRIMARY KEY,
Lastname VARCHAR(100) NOT NULL,
Firstname VARCHAR (100) NOT NULL,
Middlename VARCHAR (100) NOT NULL,
Age VARCHAR(100) NOT NULL,
Gender VARCHAR(100) NOT NULL,
Adress VARCHAR(100) NOT NULL,
Phone VARCHAR(100) NOT NULL,
Passport VARCHAR(100) NOT NULL,
Post_Code INT REFERENCES dbo.Restaurant_Posts(Post_Code) NOT NULL,
);



IF OBJECT_ID('dbo.Restaurant_Storage', 'U') IS NOT NULL
DROP TABLE dbo.Restaurant_Storage;
CREATE TABLE dbo.Restaurant_Storage
(
Ingredient_Code INT IDENTITY(1,1) CONSTRAINT Ingredient_Code PRIMARY KEY,
Ingredient_Name VARCHAR(100) NOT NULL,
Date_of_Issue DATETIME NOT NULL,
Amount VARCHAR(100) NOT NULL,
Shelf_Life VARCHAR(100) NOT NULL,
Cost INT NOT NULL,
Supplier VARCHAR(100) NOT NULL,
);



IF OBJECT_ID('dbo.Menu', 'U') IS NOT NULL
DROP TABLE dbo.Menu ;
CREATE TABLE dbo.Menu
(
Dish_code INT IDENTITY(1,1) CONSTRAINT Dish_code PRIMARY KEY,
Food_Name VARCHAR(100) NOT NULL,
Ingredient_Code_1 INT NOT NULL REFERENCES dbo.Restaurant_Storage(Ingredient_Code),
Ingredient_Amount_1 VARCHAR(100) NOT NULL,
Ingredient_Code_2 INT NOT NULL REFERENCES dbo.Restaurant_Storage(Ingredient_Code),
Ingredient_Amount_2 VARCHAR(100) NOT NULL,
Ingredient_Code_3 INT NOT NULL REFERENCES dbo.Restaurant_Storage(Ingredient_Code),
Ingredient_Amount_3 VARCHAR(100) NOT NULL,
Cost INT NOT NULL,
Cooking_time VARCHAR(100) NOT NULL,
);



IF OBJECT_ID('dbo.Ticket', 'U') IS NOT NULL
DROP TABLE dbo.Ticket;
CREATE TABLE dbo.Ticket (
  Ticket_Code INT IDENTITY(1,1) CONSTRAINT Ticket_Code PRIMARY KEY,
  Ticket_Date DATE NOT NULL,
  Ticket_Time TIME NOT NULL,
  Customers_Name VARCHAR(100) NOT NULL,
  Phone_Number VARCHAR(100) NOT NULL,
  Dish_code_1 INT NOT NULL REFERENCES dbo.Menu(Dish_code),
  Dish_code_2 INT NOT NULL REFERENCES dbo.Menu(Dish_code),
  Dish_code_3 INT NOT NULL REFERENCES dbo.Menu(Dish_code),
  Cost VARCHAR(100) NOT NULL,
  Delivery_note VARCHAR(100) NOT NULL,
  Employee_code INT NOT NULL REFERENCES dbo.Restaurant_Employees(Employee_code),
  Completion_Date DATE NOT NULL,
  Completion_Time TIME NOT NULL
);






SELECT * FROM dbo.Restaurant_Posts;
SELECT * FROM dbo.Restaurant_Employees;
SELECT * FROM dbo.Restaurant_Storage;
SELECT * FROM dbo.Menu;
SELECT * FROM dbo.Ticket;
