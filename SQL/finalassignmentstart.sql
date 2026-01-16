-- =====================================================================
--  DATABASE SETUP SCRIPT FOR A SIMPLE RETAIL DATABASE
-- =====================================================================

-- To avoid errors if you run this script multiple times, we first
-- drop the tables. We must do this in reverse order of creation
-- to avoid foreign key constraint violations.
DROP TABLE IF EXISTS Sales;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Stores;
DROP TABLE IF EXISTS Suppliers;
DROP TABLE IF EXISTS Categories;


-- =====================================================================
--  1. CREATE THE "LOOKUP" OR "DIMENSION" TABLES
--     These tables hold descriptive information (who, what, where).
-- =====================================================================

-- Create the Suppliers table
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY AUTO_INCREMENT,
    SupplierName VARCHAR(100) NOT NULL
);

-- Create the Categories table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY AUTO_INCREMENT,
    CategoryName VARCHAR(100) NOT NULL
);

-- Create the Stores table
CREATE TABLE Stores (
    StoreID INT PRIMARY KEY AUTO_INCREMENT,
    StoreLocation VARCHAR(100) NOT NULL
);


-- =====================================================================
--  2. CREATE THE "MAIN" TABLES THAT LINK THE LOOKUP TABLES
-- =====================================================================

-- Create the Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY AUTO_INCREMENT,
    ProductName VARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    StockLevel INT NOT NULL,
    -- Foreign keys to link to other tables
    CategoryID INT,
    SupplierID INT,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);

-- Create the Sales table (the "Fact" table)
CREATE TABLE Sales (
    SaleID INT PRIMARY KEY AUTO_INCREMENT,
    SaleDate DATETIME NOT NULL,
    UnitsSold INT NOT NULL,
    -- Foreign keys to link to products and stores
    ProductID INT,
    StoreID INT,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (StoreID) REFERENCES Stores(StoreID)
);


-- =====================================================================
--  3. INSERT SAMPLE DATA INTO THE TABLES
--     We insert into the dimension tables first.
-- =====================================================================

-- Insert data into Suppliers
INSERT INTO Suppliers (SupplierID, SupplierName) VALUES
(1, 'ElectroCorp'),
(2, 'Gadgetron'),
(3, 'Office Essentials Inc.');

-- Insert data into Categories
INSERT INTO Categories (CategoryID, CategoryName) VALUES
(1, 'Electronics'),
(2, 'Office Supplies'),
(3, 'Accessories');

-- Insert data into Stores
INSERT INTO Stores (StoreID, StoreLocation) VALUES
(1, 'New York'),
(2, 'San Francisco'),
(3, 'Online');

-- Insert data into Products, using the IDs from the tables above
INSERT INTO Products (ProductID, ProductName, Price, StockLevel, CategoryID, SupplierID) VALUES
(1, 'Laptop Pro 15"', 1499.99, 50, 1, 1),
(2, 'Wireless Mouse', 49.99, 250, 3, 2),
(3, 'Mechanical Keyboard', 129.99, 120, 3, 1),
(4, 'LED Desk Lamp', 79.50, 80, 2, 3),
(5, '4K Monitor 27"', 399.00, 75, 1, 2);

-- Insert data into Sales, representing different transactions
INSERT INTO Sales (SaleDate, UnitsSold, ProductID, StoreID) VALUES
('2024-05-01 10:30:00', 1, 1, 1), -- 1 Laptop Pro sold in New York
('2024-05-01 11:15:00', 2, 2, 1), -- 2 Wireless Mice sold in New York
('2024-05-02 14:00:00', 1, 5, 2), -- 1 4K Monitor sold in San Francisco
('2024-05-03 09:00:00', 1, 3, 3), -- 1 Keyboard sold Online
('2024-05-03 16:45:00', 5, 4, 2); -- 5 Desk Lamps sold in San Francisco


-- =====================================================================
--  4. VERIFICATION
--     Run a final query to confirm everything is linked correctly.
-- =====================================================================
SELECT
    s.SaleDate,
    p.ProductName,
    c.CategoryName,
    st.StoreLocation,
    s.UnitsSold,
    sup.SupplierName
FROM
    Sales s
JOIN Products p ON s.ProductID = p.ProductID
JOIN Categories c ON p.CategoryID = c.CategoryID
JOIN Stores st ON s.StoreID = st.StoreID
JOIN Suppliers sup ON p.SupplierID = sup.SupplierID
ORDER BY
    s.SaleDate;