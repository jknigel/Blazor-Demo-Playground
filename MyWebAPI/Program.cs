var builder = WebApplication.CreateBuilder(args);

// --- SETUP ---

// This is the line that ADDS all the necessary services for controllers.
// This will fix almost all of your other errors.
builder.Services.AddControllers();

// These are for Swagger/OpenAPI (the nice documentation page)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// --- EXECUTION ---

var app = builder.Build();

// This configures the Swagger documentation page.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// This is the line that TELLS the application to use the controller files you created.
app.MapControllers();

app.Run();