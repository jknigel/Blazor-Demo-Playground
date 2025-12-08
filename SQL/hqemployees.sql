CREATE DATABASE HQEmployeeDB;

USE HQEmployeeDB;

CREATE TABLE Employees (
    EmployeeID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10, 2),
    HireDate DATE
);

INSERT INTO Employees (FirstName, LastName, Department, Salary, HireDate)
VALUES
    ('Aisha', 'Khan', 'Finance', 85000.00, '2019-03-15'),
    ('Luis', 'Garcia', 'IT', 95000.00, '2020-07-22'),
    ('Chloe', 'Nguyen', 'Marketing', 72000.00, '2018-10-05'),
    ('Amara', 'Smith', 'HR', 67000.00, '2021-01-18'),
    ('Ravi', 'Patel', 'Finance', 88000.00, '2017-11-03');

SELECT * FROM Employees;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

START TRANSACTION;

UPDATE Employees SET `Salary`=`Salary`-5000 WHERE `Department`='Marketing';

SELECT * FROM Employees;

UPDATE Employees SET `Salary`=`Salary`+5000 WHERE `Department`='Finance';

COMMIT;
UNLOCK TABLES;

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees);

#CTE

WITH DepartmentSalaries AS (
    SELECT Department, AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY `Department`
)
SELECT * FROM DepartmentSalaries;

#Stored Procedures

-- Step 1: Temporarily change the delimiter to $$
DELIMITER $$

-- Step 2: Now, write your full CREATE PROCEDURE statement.
CREATE PROCEDURE AdjustSalary(
    DepartmentName VARCHAR(50),
    AdjustmentAmount INT
)
BEGIN
    -- This is the logic inside your procedure. It ends with a standard semicolon.
    UPDATE Employees
    SET `Salary` = `Salary` + AdjustmentAmount
    WHERE `Department` = DepartmentName;
END$$
-- The 'END$$' tells the server that the procedure's definition is over, using our new delimiter.

-- Step 3: Change the delimiter back to the standard semicolon.
DELIMITER ;


DELIMITER $$
CREATE PROCEDURE IncreaseSalary(
    deptName VARCHAR(50),
    increaseAmt INT
)
BEGIN
    UPDATE Employees
    SET `Salary` = `Salary` + increaseAmt
    WHERE `Department` = deptName;
END$$
DELIMITER ;

CALL IncreaseSalary('Finance', 5000);

DELIMITER $$
CREATE FUNCTION CalculateBonus(SalaryValue DECIMAL(10,2))
    RETURNS DECIMAL(10,2)
    DETERMINISTIC
    BEGIN
        RETURN SalaryValue*0.1;
    END$$
DELIMITER ;

SELECT
    FirstName,
    LastName,
    Salary,
    CalculateBonus(Salary) AS BonusAmount
FROM
    Employees;

-- First, drop the old version of the function if it exists
DROP FUNCTION IF EXISTS CalculateBonus;

-- It's good practice to change the delimiter when creating functions
DELIMITER $$

CREATE FUNCTION CalculateBonusWithErrorHandling(SalaryValue DECIMAL(10,2))
    RETURNS DECIMAL(10,2)
    DETERMINISTIC
BEGIN
    IF SalaryValue <= 0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Salary must be greater than zero.';
    END IF;
    RETURN SalaryValue * 0.1;
END$$

-- Change the delimiter back to the standard semicolon
DELIMITER ;

#For testing negative values
INSERT INTO Employees (FirstName, LastName, Department, Salary, HireDate)
VALUES ('John', 'Doe', 'Finance', -50000, '2017-11-03');
SELECT
    FirstName,
    LastName,
    Salary,
    CalculateBonusWithErrorHandling(Salary) AS BonusAmount
FROM
    Employees;

DELIMITER $$
CREATE PROCEDURE IncreaseSalaryWithErrorHandling(
    deptName VARCHAR(50),
    increaseAmt INT
)
BEGIN
    IF increaseAmt<=0 THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Increment must be positive number!';
    END IF;
    IF NOT EXISTS (SELECT 1 FROM Employees WHERE `Department` = deptName) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Department not found.';
    END IF;
    UPDATE Employees
    SET `Salary` = `Salary` + increaseAmt
    WHERE `Department` = deptName;
END$$
DELIMITER ;

CALL IncreaseSalaryWithErrorHandling('HRLOL', 10000);
SELECT * FROM Employees;