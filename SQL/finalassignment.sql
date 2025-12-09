# Generating basic SELECT queries
SELECT
    p.Name AS ProductName,
    c.Name AS Category,
    p.Price,
    p.StockLevel
FROM
    Products p
INNER JOIN
    Categories c ON p.CategoryId = c.CategoryId;


# Filtering & Sorting
SELECT
    p.Name AS ProductName,
    c.Name AS Category,
    p.Price,
    p.StockLevel
FROM
    Products p
INNER JOIN
    Categories c ON p.CategoryId = c.CategoryId
WHERE
    c.Name = 'Electronics';

SELECT
    Name AS ProductName,
    Price,
    StockLevel
FROM
    Products
WHERE
    StockLevel < 10
ORDER BY
    StockLevel ASC;

SELECT
    Name AS ProductName,
    Price,
    StockLevel
FROM
    Products
ORDER BY
    Price ASC;

# Multi-table joins
SELECT
    s.SaleDate,
    p.ProductName,
    sup.SupplierName,
    s.QuantitySold
FROM
    Sales AS s
INNER JOIN
    Products AS p ON s.ProductID = p.ProductID
INNER JOIN
    Suppliers AS sup ON p.SupplierID = sup.SupplierID
WHERE
    s.SaleDate >= '2024-01-01'
ORDER BY
    s.SaleDate DESC;

# Write queries with multi-table join
SELECT
    p.ProductName,
    s.SaleDate,
    st.StoreLocation,
    s.UnitsSold
FROM
    Sales AS s
INNER JOIN
    Products AS p ON s.ProductID = p.ProductID
INNER JOIN
    Stores AS st ON s.StoreID = st.StoreID
ORDER BY
    s.SaleDate DESC;

# Subquery to calculate total sales for each product
SELECT
    p.ProductName,
    (
        SELECT SUM(s.UnitsSold)
        FROM Sales s
        WHERE s.ProductID = p.ProductID
    ) AS TotalUnitsSold
FROM
    Products p
ORDER BY
    TotalUnitsSold DESC;

# Subquery to identify suppliers with most delayed deliveries
SELECT
    s.SupplierName,
    AVG(DATEDIFF(po.DeliveryDate, po.OrderDate)) AS AverageDeliveryDays
FROM
    Suppliers s
INNER JOIN
    PurchaseOrders po ON s.SupplierID = po.SupplierID
GROUP BY
    s.SupplierName
HAVING
    AVG(DATEDIFF(po.DeliveryDate, po.OrderDate)) = (
        SELECT MAX(AvgDelay)
        FROM (
            SELECT
                AVG(DATEDIFF(po_inner.DeliveryDate, po_inner.OrderDate)) AS AvgDelay
            FROM
                PurchaseOrders po_inner
            GROUP BY
                po_inner.SupplierID
        ) AS SupplierDelays
    );

# Aggregate functions
SELECT
    c.CategoryName,
    SUM(s.UnitsSold * p.Price) AS TotalRevenue
FROM
    Sales s
INNER JOIN
    Products p ON s.ProductID = p.ProductID
INNER JOIN
    Categories c ON p.CategoryId = c.CategoryId
GROUP BY
    c.CategoryName
ORDER BY
    TotalRevenue DESC;

SELECT
    d.DepartmentName,
    AVG(e.Salary) AS AverageSalary
FROM
    Employees e
INNER JOIN
    Departments d ON e.DepartmentId = d.DepartmentId
GROUP BY
    d.DepartmentName
ORDER BY
    AverageSalary DESC;

SELECT
    p.ProductName,
    COUNT(s.SaleID) AS NumberOfSales,
    MAX(s.UnitsSold) AS LargestSingleSale
FROM
    Sales s
INNER JOIN
    Products p ON s.ProductID = p.ProductID
GROUP BY
    p.ProductName
ORDER BY
    NumberOfSales DESC;

-- Index for the foreign key on the Products table
CREATE INDEX IX_Products_CategoryId ON Products(CategoryId);

-- Index for the foreign key on the Sales table
CREATE INDEX IX_Sales_ProductID ON Sales(ProductID);

-- Index for the foreign key on the Employees table
CREATE INDEX IX_Employees_DepartmentID ON Employees(DepartmentID);

-- Index for the name column on the Departments table, which is used in WHERE clauses
CREATE INDEX IX_Departments_Name ON Departments(Name);

SELECT
    p.ProductName,
    SUM(s.UnitsSold) AS TotalUnitsSold
FROM
    Products p
LEFT JOIN
    Sales s ON p.ProductID = s.ProductID
GROUP BY
    p.ProductID, p.ProductName -- Group by both ID and Name
ORDER BY
    TotalUnitsSold DESC;

SELECT
    s.SupplierName,
    AVG(DATEDIFF(po.DeliveryDate, po.OrderDate)) AS AverageDeliveryDays
FROM
    Suppliers s
INNER JOIN
    PurchaseOrders po ON s.SupplierID = po.SupplierID
GROUP BY
    s.SupplierID, s.SupplierName
ORDER BY
    AverageDeliveryDays DESC
LIMIT 1;