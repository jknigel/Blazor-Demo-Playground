var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<TaskItem> tasks = new()
{
    new TaskItem { Id = 1, Name = "Task 1", IsComplete = false },
    new TaskItem { Id = 2, Name = "Task 2", IsComplete = true }
};
app.MapGet("/tasks", () => Results.Ok(tasks));

app.MapPost("/tasks", (TaskItem task) =>
{
    tasks.Add(task);
    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapPut("/tasks/{id}", (int id, TaskItem updatedTask) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task is null)
    {
        return Results.NotFound();
    }
    task.Name = updatedTask.Name;
    task.IsComplete = updatedTask.IsComplete;
    return Results.Ok(task);
});

app.MapDelete("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);
    if (task is null)
    {
        return Results.NotFound();
    }
    tasks.Remove(task);
    return Results.NoContent();
});

app.Run();

public class TaskItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
}
