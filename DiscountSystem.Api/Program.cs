using DiscountSystem.Api.Configuration;
using DiscountSystem.Data.Context;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();


try
{

  var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
// Add services to the container.
AutoMapperConfig.Configure(service);
ServiceExtensionConfig.ConfigureServices(service);
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
}
catch (Exception exception)
{
  // NLog: catch setup errors
  logger.Error(exception, "Stopped program because of exception");
  throw;
}
finally
{
  // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
  NLog.LogManager.Shutdown();
}