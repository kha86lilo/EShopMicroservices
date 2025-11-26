var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();
app.MapGet("/", () => "Hello from Catalog Service!");
app.MapCarter();
app.Run();
