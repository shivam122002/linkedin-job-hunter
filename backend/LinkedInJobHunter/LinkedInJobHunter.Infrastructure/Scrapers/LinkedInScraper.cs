using LinkedInJobHunter.Domain.Entities;
using LinkedInJobHunter.Domain.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LinkedInJobHunter.Infrastructure.Scrapers
{
    public class LinkedInScraper
    {
        public async Task<List<Job>> ScrapeJobsAsync()
        {
            var jobs = new List<Job>();

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--headless=new");   // run browser in background
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            using var driver = new ChromeDriver(options);

            var keywords = new[]
            {
                ".NET developer",
                "Angular developer",
                "Full stack .NET developer"
            };

            foreach (var keyword in keywords)
            {
                var url =
                    $"https://www.linkedin.com/jobs/search/?keywords={Uri.EscapeDataString(keyword)}&location=India";

                driver.Navigate().GoToUrl(url);
                await Task.Delay(5000);
                var jobCards = driver.FindElements(By.CssSelector(".base-card"));
                foreach (var card in jobCards)
                {
                    try
                    {
                        string title = card
                                .FindElement(By.CssSelector("h3.base-search-card__title"))
                                .GetAttribute("innerText");

                        string company = card
                            .FindElement(By.CssSelector("h4.base-search-card__subtitle"))
                            .GetAttribute("innerText");

                        string location = card
                            .FindElement(By.CssSelector(".job-search-card__location"))
                            .GetAttribute("innerText");

                        string link = card.FindElement(By.TagName("a")).GetAttribute("href");
                        jobs.Add(new Job
                        {
                            Id = Guid.NewGuid(),
                            Title = title,
                            CompanyName = company,
                            Location = location,
                            PostUrl = link,
                            PostedDate = DateTime.UtcNow,
                            Technology = DetectTechnology(keyword),
                            Experience = ExperienceLevel.OneToTwoYears,
                            CreatedDate = DateTime.UtcNow
                        });
                    }
                    catch
                    {
                        // ignore parsing errors
                    }
                }
            }

            driver.Quit();

            return jobs;
        }
        private TechnologyType DetectTechnology(string keyword)
        {
            keyword = keyword.ToLower();

            if (keyword.Contains("angular"))
                return TechnologyType.Angular;

            if (keyword.Contains("full stack"))
                return TechnologyType.FullStackDotNet;

            if (keyword.Contains(".net"))
                return TechnologyType.DotNet;

            return TechnologyType.Unknown;
        }
    }
}