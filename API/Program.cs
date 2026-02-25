using System.Text.Json.Serialization;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApiVersioningConfig();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.AddRateLimiting();
builder.Services.AddServices();
builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
