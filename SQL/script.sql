CREATE DATABASE EmployeeDB;
USE EmployeeDB;

CREATE TABLE Employees (
    ID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Department VARCHAR(50),
    Salary DECIMAL(10,2),
    YearsExperience INT
);

INSERT INTO Employees (FirstName, LastName, Department, Salary, YearsExperience) VALUES
('John', 'Doe', 'HR', 60000, 10),
('Jane', 'Smith', 'Finance', 70000, 8),
('Michael', 'Brown', 'IT', 50000, 5),
('Emily', 'Davis', 'HR', 45000, 2),
('Chris', 'Wilson', 'Finance', 80000, 15);

# Retrieve All Employees Sorted by Last Name (Ascending)
SELECT 
    *
FROM 
    Employees
ORDER BY 
    LastName ASC;

# Retrieve HR Employees, Sorted by Salary (Descending)
SELECT
    *
FROM
    Employees
WHERE
    Department = 'HR'
ORDER BY
    Salary DESC;

# Retrieve the Top 3 Highest Earners
SELECT
    *
FROM
    Employees
ORDER BY
    Salary DESC
LIMIT 3;

# Retrieve IT Dept >3years Desc
SELECT
    *
FROM
    `Employees`
WHERE
    YearsExperience>3
ORDER BY
    YearsExperience DESC;

#Salary 50k to 75k, sorted firstname
SELECT
    *
FROM
    `Employees`
WHERE
    Salary > 50000 AND Salary <75000
ORDER BY
    FirstName ASC;