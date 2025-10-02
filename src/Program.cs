using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using ToDoList.Infrastructure;
using ToDoList.Endpoints;
using ToDoList.Services.Interfaces;
using ToDoList.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
  Log.Information("App started");
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddScoped<ITodoService, TodoService>();
  builder.Services.AddScoped<ITodoRepository, TodoRepository>();

  builder.Services.AddSerilog((services, ls) => ls
      .ReadFrom.Configuration(builder.Configuration)
      .ReadFrom.Services(services)
      .Enrich.FromLogContext()
      .WriteTo.Console());

  SqlConnectionStringBuilder sqlConnectionStringBuilder = new(
      builder.Configuration.GetConnectionString("TodoDbContext"))
  {
    Password = builder.Configuration["TodoContext:Password"]
  };

  builder.Services.AddDbContext<TodoDbContext>(options =>
      options.UseSqlServer(sqlConnectionStringBuilder.ConnectionString));

  builder.Services.AddOpenApi();
  builder.Services.AddProblemDetails();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
    app.UseDeveloperExceptionPage();
  }
  else
  {
    app.UseExceptionHandler();
    app.UseStatusCodePages();
  }

  app.UseHttpsRedirection();

  app.MapGroup("")
    .RouteTodoList();

  app.MapGroup("/todo")
    .RouteTodo();

  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Terminated unexpectedly");
}
finally
{
  Log.CloseAndFlush();
}
