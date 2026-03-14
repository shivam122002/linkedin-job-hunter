using LinkedInJobHunter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkedInJobHunter.Infrastructure.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            builder.HasKey(j => j.Id);

            builder.Property(j => j.Title)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(j => j.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(j => j.Location)
                .HasMaxLength(200);

            builder.Property(j => j.PostUrl)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(j => j.CreatedDate)
                .IsRequired();

            builder.HasIndex(j => j.PostUrl)
                .IsUnique();
        }
    }
}