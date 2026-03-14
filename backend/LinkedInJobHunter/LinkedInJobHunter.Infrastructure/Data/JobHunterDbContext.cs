using LinkedInJobHunter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LinkedInJobHunter.Infrastructure.Data
{
    public class JobHunterDbContext : DbContext
    {
        public JobHunterDbContext(DbContextOptions<JobHunterDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs => Set<Job>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JobHunterDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}