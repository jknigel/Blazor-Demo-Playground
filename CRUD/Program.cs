var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Basic routes

app.MapGet("/", () => "Welcome to the Simple Web API!");
app.MapGet("/all-items", () => Results.Ok(ItemRepository.GetAllItems()));
app.MapGet("/item/{id}", (int id) =>
{
    var item = ItemRepository.GetItemById(id);
    return item is not null ? Results.Ok(item) : Results.NotFound();
});
app.MapPost("/item", (Item newItem) =>
{
    ItemRepository.AddItem(newItem);
    return Results.Created($"/item/{newItem.Id}", newItem);
});
app.MapPut("/item/{id}", (int id, Item updatedItem) =>
{
    var existingItem = ItemRepository.GetItemById(id);
    if (existingItem is null)
    {
        return Results.NotFound();
    }

    existingItem.Name = updatedItem.Name;
    existingItem.Price = updatedItem.Price;
    return Results.Ok(existingItem);
});
app.MapDelete("/item/{id}", (int id) =>
{
    var item = ItemRepository.GetItemById(id);
    if (item is null)
    {
        return Results.NotFound();
    }

    ItemRepository.DeleteItem(id);
    return Results.NoContent();
});

app.Run();

public static class ItemRepository
{
    private static List<Item> Items = new();

    public static IEnumerable<Item> GetAllItems() => Items;
    public static Item? GetItemById(int id) => Items.FirstOrDefault(i => i.Id == id);
    public static void AddItem(Item item)
    {
        item.Id = Items.Any() ? Items.Max(i => i.Id) + 1 : 1;
        Items.Add(item);
    }
    public static void DeleteItem(int id)
    {
        var item = GetItemById(id);
        if (item is not null)
        {
            Items.Remove(item);
        }
    }
}