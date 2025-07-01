using Microsoft.EntityFrameworkCore;
using WorkbenchTimeTracker.Server.Domain;
using WorkbenchTimeTracker.Server.Infrastructure.Persistence;
using WorkbenchTimeTracker.Server.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<TimeTrackerDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));


builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TimeTrackerDbContext>();
    DbInitializer.Seed(db);
}

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
