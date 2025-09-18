using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
  var builder = WebApplication.CreateBuilder(args);

  builder.Services.AddSerilog((services, ls) => ls
      .ReadFrom.Configuration(builder.Configuration)
      .ReadFrom.Services(services)
      .Enrich.FromLogContext()
      .WriteTo.Console());
  builder.Services.AddOpenApi();

  var app = builder.Build();

  // Configure the HTTP request pipeline.
  if (app.Environment.IsDevelopment())
  {
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
  }

  app.UseHttpsRedirection();

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
