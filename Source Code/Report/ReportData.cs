using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Report
{
    public class ReportData
    {
        // Question 1: How many successful deployments have taken place
        public int CountOfSuccessfulDeployments(ImportFile importFile)
        {
            try
            {
                var successfulDeployments = importFile.Projects
                    .SelectMany(x => x.Releases)
                    .SelectMany(y => y.Deployments)
                    .Count(z => z.State == "Success");

                return successfulDeployments;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calculating the successful deployments. Error: {e.Message}");
            }
        }
        
        // Question 2 (a) by Project Group: How does this break down by project group, by environment, by year?
        public Dictionary<string, int> SuccessfulDeploymentsByProjectGroup(ImportFile importFile)
        {
            try
            {
                var projectGroupDeployments = new Dictionary<string, int>();

                var groupedProjects = importFile.Projects
                    .OrderBy(x=>x.Group)
                    .GroupBy(x => x.Group);

                foreach (var project in groupedProjects)
                {
                    var successfulDeployments = project
                        .SelectMany(x=>x.Releases)
                        .SelectMany(y => y.Deployments)
                        .Count(z => z.State == "Success");

                    projectGroupDeployments.Add(project.Key, successfulDeployments);
                }

                return projectGroupDeployments;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calculating the successful deployments by project group. Error: {e.Message}", e);
            }
        }

        // Question 2 (b) by Environment: How does this break down by project group, by environment, by year?
        public Dictionary<string, int> SuccessfulDeploymentsByEnvironment(ImportFile importFile)
        {
            try
            {
                var environmentDeployments = new Dictionary<string, int>();

                var groupedEnvironments = importFile.Projects
                    .SelectMany(x => x.Releases)
                    .SelectMany(y => y.Deployments)
                    .GroupBy(z => z.Environment)
                    .OrderBy(x => x.Key);

                foreach (var environment in groupedEnvironments)
                {
                    environmentDeployments.Add(environment.Key, environment.Count(x=>x.State == "Success"));
                }

                return environmentDeployments;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calculating the successful deployments by environment. Error: {e.Message}", e);
            }
        }

        // Question 2 (c) by Year: How does this break down by project group, by environment, by year?
        public Dictionary<string, int> SuccessfulDeploymentsByYear(ImportFile importFile)
        {
            try
            {
                var yearDeployments = new Dictionary<string, int>();

                var groupedYears = importFile.Projects
                    .SelectMany(x => x.Releases)
                    .SelectMany(y => y.Deployments)
                    .GroupBy(z => z.Created.Year);

                foreach (var year in groupedYears.OrderBy(x=>x.Key))
                {
                    yearDeployments.Add(year.Key.ToString(), year.Count(x => x.State == "Success"));
                }

                return yearDeployments;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calculating the successful deployments by year. Error: {e.Message}", e);
            }
        }

        // Question 3: Which is the most popular day of the week for live deployments?
        public string MostPopularDayOfTheWeekForLiveDeployments(ImportFile importFile)
        {
            try
            {
                var liveDeployments = importFile.Projects
                    .SelectMany(x => x.Releases)
                    .SelectMany(x => x.Deployments)
                    .Where(x => x.Environment == "Live")
                    .GroupBy(x => x.Created.DayOfWeek)
                    .OrderByDescending(x=>x.Count());

                var mostPopularDay = liveDeployments.First();

                return mostPopularDay.Key.ToString();
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calculating the most popular day of the week for live deployments. Error: {e.Message}", e);
            }
        }

        // Question 4: What is the average length of time a release takes from integration to live, by project group?
        public Dictionary<string, TimeSpan> AverageReleaseTimeFromIntegrationToLiveByProjectGroup(ImportFile importFile)
        {
            try
            {
                var averageReleaseTimes = new Dictionary<string, TimeSpan>();

                var groupedProjects = importFile.Projects
                    .OrderBy(x => x.Group)
                    .GroupBy(x => x.Group);

                foreach (var project in groupedProjects)
                {
                    var timespans = new List<TimeSpan>();

                    var allReleases = project
                        .SelectMany(x => x.Releases);

                    foreach (var release in allReleases)
                    {
                        var integration = release.Deployments.FirstOrDefault(x => x.Environment == "Integration");

                        var live = release.Deployments.FirstOrDefault(x => x.Environment == "Live");

                        if (integration != null && live != null)
                        {
                            timespans.Add(live.Created-integration.Created);
                        }
                    }

                    if (timespans.Any())
                    {
                        var ticks = timespans.Average(x => x.Ticks);
                        var longTicks = Convert.ToInt64(ticks);
                        var timespan = new TimeSpan(longTicks);

                        averageReleaseTimes.Add(project.Key, timespan);
                    }
                }

                return averageReleaseTimes;
            }
            catch (Exception e)
            {
                throw new Exception($"Error when calculating the average release time from Integration to Live by project group", e);
            }
        }

        // Question 5: Please provide a break down by project group of success and unsuccessful deployments (success being releases that are deployed to live),
        // the number of deployments involved in the release pipeline and whether some environments had to be repeatedly deployed.
        public IEnumerable<IGrouping<string, Project>> ProjectsByGroup(ImportFile importFile)
        {
            try
            {
                return importFile.Projects.GroupBy(x => x.Group);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to group projects by project group", e);
            }
        }
    }
}
