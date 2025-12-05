using CRUDwithMySQL.Models; // We need this to find our Product and ApplicationDbContext classes

Console.WriteLine("--- EF Core CRUD Operations with MySQL ---");

// We wrap our database operations in 'using' blocks to ensure the
// connection is properly managed and closed.
using (var dbContext = new ApplicationDbContext())
{
    // Ensure the database is created. If it already exists, this does nothing.
    dbContext.Database.EnsureCreated();

    // --- CREATE ---
    Console.WriteLine("\n[CREATE] Adding a new product...");

    // 1. Create a new Product object in memory.
    var products = new List<Product>
    {
        new Product { Name = "Laptop Pro", Price = 1499.99m },
        new Product { Name = "Smartphone X", Price = 999.99m },
        new Product { Name = "Tablet S", Price = 599.99m },
        new Product { Name = "Smartwatch Z", Price = 199.99m }
    };

    dbContext.Products.AddRange(products);

    // 3. Save changes to the database. This generates and runs the SQL INSERT command.
    dbContext.SaveChanges();

    Console.WriteLine(" -> Products were successfully saved with the following IDs:");
    foreach (var product in products)
    {
        Console.WriteLine($"    - Product '{product.Name}' has been assigned ID: {product.Id}");
    }

}


using (var dbContext = new ApplicationDbContext())
{
    // --- READ (All) ---
    Console.WriteLine("\n[READ] Retrieving all products...");

    // 1. Query the DbSet to get all products. .ToList() executes the query.
    var allProducts = dbContext.Products.ToList();

    Console.WriteLine($" -> Found {allProducts.Count} products.");
    foreach (var product in allProducts)
    {
        Console.WriteLine($"    - ID: {product.Id}, Name: {product.Name}, Price: {product.Price:C}");
    }


    // --- READ (Single) ---
    Console.WriteLine("\n[READ] Retrieving a single product by its ID...");

    // 1. Use the Find method. This is very efficient for finding by primary key.
    //    We'll find the product we just added. We need to know its ID. Let's assume it's 1.
    int idToFind = 2;
    var foundProduct = dbContext.Products.Find(idToFind);

    if (foundProduct != null)
    {
        Console.WriteLine($" -> Found product with ID {idToFind}: '{foundProduct.Name}'");
    }
    else
    {
        Console.WriteLine($" -> Product with ID {idToFind} not found.");
    }
}


using (var dbContext = new ApplicationDbContext())
{
    // --- UPDATE ---
    Console.WriteLine("\n[UPDATE] Updating an existing product...");
    int idToUpdate = 3;

    // 1. Retrieve the product you want to update using the Find method.
    var productToUpdate = dbContext.Products.Find(idToUpdate);

    if (productToUpdate != null)
    {
        Console.WriteLine($" -> Found '{productToUpdate.Name}'. Changing its price.");

        // 2. Change one of its properties. EF Core automatically detects this change.
        productToUpdate.Price = 1599.00m;

        // 3. Save the changes. This generates and runs the SQL UPDATE command.
        dbContext.SaveChanges();

        Console.WriteLine(" -> Update saved.");
    }
    else
    {
        Console.WriteLine($" -> Product with ID {idToUpdate} not found for update.");
    }
}


using (var dbContext = new ApplicationDbContext())
{
    // --- DELETE ---
    Console.WriteLine("\n[DELETE] Deleting a product...");
    int idToDelete = 4;

    // 1. Retrieve the product you want to remove.
    var productToDelete = dbContext.Products.Find(idToDelete);

    if (productToDelete != null)
    {
        Console.WriteLine($" -> Found '{productToDelete.Name}'. Removing it.");

        // 2. Use the Remove method. EF Core now marks this object for deletion.
        dbContext.Products.Remove(productToDelete);

        // 3. Save the changes. This generates and runs the SQL DELETE command.
        dbContext.SaveChanges();

        Console.WriteLine(" -> Deletion saved.");
    }
    else
    {
        Console.WriteLine($" -> Product with ID {idToDelete} not found for deletion.");
    }
}


Console.WriteLine("\n--- CRUD Test Complete ---");