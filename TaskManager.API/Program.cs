using Microsoft.EntityFrameworkCore;
using TaskManager.API.Filters;
using TaskManager.Application;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Context;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Repository;
using TaskManager.Shared;
using TaskManager.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IValidator, BaseValidator>();

builder.Services.AddScoped<IUsuarioApplication, UsuarioApplication>();
builder.Services.AddScoped<IProjetoApplication, ProjetoApplication>();

builder.Services.AddDbContext<TaskManagerContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetService<TaskManagerContext>());
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));


builder.Services.AddControllers(option => option.Filters.Add<ResultFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Task Manager - V1",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "TaskManager.API.xml");
    c.IncludeXmlComments(filePath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskManagerContext>();
    context.Database.EnsureCreated();
    context.SeedData();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
