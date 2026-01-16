CREATE DATABASE SampleDB;

USE SampleDB;

CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Age INT
);

INSERT INTO Users (FirstName, LastName, Email, Age) VALUES
('Aisha', 'Khan', 'aisha.khan@example.com', 29),
('Carlos', 'Garcia', 'carlos.garcia@example.com', 35),
('Mei', 'Chen', 'mei.chen@example.com', 24);

INSERT INTO Users (FirstName, LastName, Email, Age) VALUES
('Arjun', 'Patel', 'arjun.patel@example.com', 41)

SELECT * FROM Users;

UPDATE `Users` SET Age=26 WHERE `FirstName`='Mei';

SELECT * FROM Users;

DELETE FROM `Users` WHERE `LastName`='Garcia';

UPDATE Users SET `Age`=30;

ROLLBACK;

UPDATE Users SET `Age`=28 WHERE `FirstName`='Aisha';