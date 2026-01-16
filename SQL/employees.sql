CREATE DATABASE MyEmployeeDB;
USE MyEmployeeDB;

CREATE TABLE Employees (
    EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10, 2),
    HireDate DATE
);

INSERT INTO Employees (FirstName, LastName, Department, Salary, HireDate) VALUES
('Liam', 'Nguyen', 'Engineering', 85000.00, '2020-03-15'),
('Sophia', 'Smith', 'Marketing', 72000.00, '2019-05-22'),
('Raj', 'Patel', 'Sales', 64000.00, '2021-07-01'),
('Aisha', 'Khan', 'HR', 60000.00, '2020-09-12'),
('Carlos', 'Martinez', 'Engineering', 93000.00, '2018-12-01'),
('Chen', 'Zhao', 'Marketing', 77000.00, '2017-11-05'),
('Amara', 'Okafor', 'Sales', 67000.00, '2022-03-18');

SELECT * FROM Employees;

SELECT CONCAT(FirstName, ' ', LastName) AS FullName FROM Employees;

SELECT UPPER(Department) AS DepartmentUpper FROM `Employees`;

SELECT LOWER(LastName) AS LowerLastName FROM Employees;

SELECT LENGTH(FirstName) AS FirstNameLength FROM Employees;

SELECT SUBSTRING(LastName, 1,3) AS LastName3 FROM Employees;

SELECT COUNT(*) AS TotalEmployees FROM Employees;

SELECT SUM(Salary) AS TotalSalary FROM `Employees`;

SELECT AVG(Salary) AS AvgEngineeringSalary FROM `Employees` WHERE Department='Engineering';

SELECT MIN(Salary) AS MinimumSalary FROM `Employees`;

SELECT MAX(Salary) AS MaxSalesSalary FROM `Employees` WHERE `Department`='Sales';

SELECT
    FirstName,
    LastName,
    Salary
FROM
    Employees
WHERE
    Salary = (SELECT MAX(Salary) FROM Employees WHERE Department='Sales');

SELECT Department, SUM(Salary) AS TotalSalary FROM `Employees` GROUP BY `Department`;

SELECT Department, AVG(Salary) AS AvgSalary FROM `Employees` GROUP BY `Department`;

SELECT Department, Count(*) AS EmployeeCount FROM `Employees` GROUP BY `Department`;

SELECT CONCAT(FirstName, ' ', LastName) AS FullName, LENGTH(CONCAT(FirstName, ' ', LastName)) AS NameLength FROM `Employees`;

SELECT YEAR(HireDate) AS HireYear, COUNT(*) AS EmployeeCount FROM Employees GROUP BY HireYear;

SELECT YEAR(HireDate) AS HireYear, SUM(Salary) AS TotalSalary FROM `Employees` GROUP BY HireYear;