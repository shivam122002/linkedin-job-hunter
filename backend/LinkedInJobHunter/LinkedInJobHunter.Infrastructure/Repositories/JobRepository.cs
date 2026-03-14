using LinkedInJobHunter.Domain.Entities;
using LinkedInJobHunter.Domain.Interfaces;
using LinkedInJobHunter.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkedInJobHunter.Infrastructure.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly JobHunterDbContext _context;

        public JobRepository(JobHunterDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Job job, CancellationToken cancellationToken)
        {
            await _context.Jobs.AddAsync(job, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Job>> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            return await _context.Jobs
                .OrderByDescending(x => x.PostedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Job>> GetLatestJobsAsync(
            int count,
            CancellationToken cancellationToken)
        {
            return await _context.Jobs
                .OrderByDescending(x => x.PostedDate)
                .Take(count)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByUrlAsync(
            string postUrl,
            CancellationToken cancellationToken)
        {
            return await _context.Jobs
                .AnyAsync(x => x.PostUrl == postUrl, cancellationToken);
        }
    }
}