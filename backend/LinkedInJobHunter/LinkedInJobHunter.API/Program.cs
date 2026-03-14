using LinkedInJobHunter.Application.Interfaces;
using LinkedInJobHunter.Application.Services;
using LinkedInJobHunter.Infrastructure.DependencyInjection;
using LinkedInJobHunter.API.Middleware;
using LinkedInJobHunter.API.Workers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddHostedService<JobScraperWorker>();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();