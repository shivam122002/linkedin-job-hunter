using LinkedInJobHunter.Domain.Enums;

namespace LinkedInJobHunter.Application.DTOs;

public class CreateJobDto
{
    public string Title { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public TechnologyType Technology { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }

    public string ApplyUrl { get; set; } = string.Empty;

    public DateTime PostedDate { get; set; }
}