using LinkedInJobHunter.Domain.Interfaces;
using LinkedInJobHunter.Infrastructure.Data;
using LinkedInJobHunter.Infrastructure.Repositories;
using LinkedInJobHunter.Infrastructure.Scrapers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkedInJobHunter.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<JobHunterDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IJobRepository, JobRepository>();

            services.AddScoped<LinkedInScraper>();

            return services;
        }
    }
}