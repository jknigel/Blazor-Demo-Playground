using EFCoreModelApp;
using EFCoreModelApp.Models;
using Microsoft.EntityFrameworkCore; // Required for .Include()

Console.WriteLine("--- Database CRUD Operations Test ---");

// We create an instance of our DbContext.
// The 'using' statement ensures the connection is properly closed and disposed of.
using (var dbContext = new HRDbContext())
{
    // --- 1. RETRIEVE AND DISPLAY ALL DATA (READ) ---
    Console.WriteLine("\n--- 1. Displaying All Employees with their Departments ---");
    
    // .Include(e => e.Department) is the magic here.
    // It tells EF Core: "When you fetch the employees, also fetch the
    // related Department data for each one." This is called "eager loading".
    var allEmployees = dbContext.Employees
                                .Include(e => e.Department)
                                .ToList();

    foreach (var emp in allEmployees)
    {
        // Because we used .Include(), emp.Department is NOT null.
        Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.Name}, Department: {emp.Department.DepartmentName}");
    }


    // --- 2. ADD A QUERY FOR A SPECIFIC DEPARTMENT (READ) ---
    Console.WriteLine("\n--- 2. Displaying Employees in the 'Human Resources' Department ---");
    
    var hrEmployees = dbContext.Employees
                               .Where(e => e.Department.DepartmentName == "Human Resources")
                               .ToList();

    foreach (var emp in hrEmployees)
    {
        Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.Name}");
    }


    // --- 3. ADD FUNCTIONALITY TO CREATE A NEW EMPLOYEE (CREATE) ---
    Console.WriteLine("\n--- 3. Creating a new employee ---");

    var newEmployee = new Employee
    {
        Name = "Emily Davis",
        Salary = 75000,
        DepartmentId = 3 // We'll add her to the Marketing department
    };

    // Add the new employee to the context. EF Core starts tracking it.
    dbContext.Employees.Add(newEmployee);
    
    // IMPORTANT: No changes are saved to the database until you call SaveChanges().
    dbContext.SaveChanges();

    Console.WriteLine($"New employee '{newEmployee.Name}' added to the database with ID: {newEmployee.EmployeeId}");


    // --- (Bonus) VERIFY THE CREATE WORKED ---
    Console.WriteLine("\n--- Verifying the new employee was saved ---");
    var foundEmployee = dbContext.Employees.FirstOrDefault(e => e.Name == "Emily Davis");
    if (foundEmployee != null)
    {
        Console.WriteLine($"Successfully found '{foundEmployee.Name}' in the database.");
    }

    
    // --- (Bonus) UPDATE AN EXISTING EMPLOYEE ---
    Console.WriteLine("\n--- 4. Updating an existing employee's salary ---");
    var employeeToUpdate = dbContext.Employees.FirstOrDefault(e => e.Name == "Alice Johnson");
    if (employeeToUpdate != null)
    {
        Console.WriteLine($"Updating '{employeeToUpdate.Name}'s salary from {employeeToUpdate.Salary} to 95000...");
        employeeToUpdate.Salary = 95000;
        dbContext.SaveChanges(); // Save the change to the database.
        Console.WriteLine("Update saved.");
    }


    // --- (Bonus) DELETE THE NEW EMPLOYEE ---
    Console.WriteLine("\n--- 5. Deleting the new employee ---");
    var employeeToDelete = dbContext.Employees.FirstOrDefault(e => e.Name == "Emily Davis");
    if (employeeToDelete != null)
    {
        Console.WriteLine($"Deleting '{employeeToDelete.Name}'...");
        dbContext.Employees.Remove(employeeToDelete);
        dbContext.SaveChanges(); // Save the deletion to the database.
        Console.WriteLine("Deletion saved.");
    }
}

Console.WriteLine("\n--- Test Complete ---");