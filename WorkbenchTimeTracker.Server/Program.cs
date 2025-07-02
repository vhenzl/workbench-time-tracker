using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WorkbenchTimeTracker.Server.Application.BuildingBlocks;
using WorkbenchTimeTracker.Server.Domain;
using WorkbenchTimeTracker.Server.Infrastructure.Persistence;
using WorkbenchTimeTracker.Server.Infrastructure.Persistence.Repositories;
using WorkbenchTimeTracker.Server.Infrastructure.Processing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<TimeTrackerDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));


builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.Scan(scan => scan
    .FromEntryAssembly()
    .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.Scan(scan => scan
    .FromEntryAssembly()
    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);
builder.Services.Scan(scan => scan
        .FromEntryAssembly()
        .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);
builder.Services.Decorate(
    typeof(ICommandHandler<,>),
    typeof(ValidatorCommandHandlerDecorator<,>)
);
builder.Services.Decorate(
    typeof(ICommandHandler<,>),
    typeof(UnitOfWorkCommandHandlerDecorator<,>)
);


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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            InvalidCommandException => StatusCodes.Status400BadRequest,
            BusinessException => StatusCodes.Status409Conflict,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
    });
});

app.Run();
