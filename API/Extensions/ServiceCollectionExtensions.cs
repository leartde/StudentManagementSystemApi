using API.Data;
using API.Services;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
  public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<ApplicationDbContext>(options =>
      options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
  }

  public static void AddSwaggerDocumentation(this IServiceCollection services)
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Student Management API",
        Version = "v1",
        Description = "API for managing students, grades, and schedules"
      });
    });
  }

  public static void AddApiVersioningConfig(this IServiceCollection services)
  {
    services.AddApiVersioning(options =>
    {
      options.ReportApiVersions = true;
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.DefaultApiVersion = new ApiVersion(1, 0);
    });
  }

  public static void AddServices(this IServiceCollection services)
  {
    services.AddScoped<StudentService>();
    services.AddScoped<ProfessorService>();
    services.AddScoped<SubjectService>();
    services.AddScoped<GradeService>();
    services.AddScoped<RedisCacheService>();
  }

  public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddStackExchangeRedisCache(options =>
    {
      options.Configuration = configuration.GetConnectionString("Redis");
      options.InstanceName = "StudentSystemCaching_";
    });
    services.AddMemoryCache(options =>
    {
      options.SizeLimit = 1024 * 1024 * 100;
    });
  }
}
