using LinkedInJobHunter.Domain.Enums;

namespace LinkedInJobHunter.Application.DTOs
{
    public class JobDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public TechnologyType Technology { get; set; }

        public ExperienceLevel Experience { get; set; }

        public string PostUrl { get; set; } = string.Empty;

        public DateTime PostedDate { get; set; }
    }
}