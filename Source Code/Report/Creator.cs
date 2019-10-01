using System;
using System.Linq;
using System.Text;
using Models;

namespace Report
{
    public class Creator
    {
        public string Generate(ImportFile importFile)
        {
            try
            {
                var report = new StringBuilder();

                var reportData = new ReportData();

                var question1 = reportData.CountOfSuccessfulDeployments(importFile);

                var question2a = reportData.SuccessfulDeploymentsByProjectGroup(importFile);

                var question2b = reportData.SuccessfulDeploymentsByEnvironment(importFile);

                var question2c = reportData.SuccessfulDeploymentsByYear(importFile);

                var question3 = reportData.MostPopularDayOfTheWeekForLiveDeployments(importFile);

                var question4 = reportData.AverageReleaseTimeFromIntegrationToLiveByProjectGroup(importFile);

                var question5 = reportData.ProjectsByGroup(importFile);

                report.AppendLine("Report for ANSM5");
                report.AppendLine("");
                report.AppendLine("Question 1: How many successful deployments have taken place?");
                report.AppendLine("-------------------------------------------------------------");
                report.AppendLine("");
                report.AppendLine($"Answer: {question1}");
                report.AppendLine("");
                report.AppendLine("Question 2 has been interpreted as three separate questions. These are are:");

                report.AppendLine(""); 
                report.AppendLine("2a) How does this break down by project group?"); 
                report.AppendLine("----------------------------------------------"); 
                report.AppendLine("");
                report.AppendLine("Answer:");
                report.AppendLine("");
                foreach (var item in question2a)
                {
                    report.AppendLine($"Project group: {item.Key}   Successful deployments: {item.Value}");
                }
                report.AppendLine("");

                report.AppendLine("2b) How does this break down by environment?");
                report.AppendLine("--------------------------------------------");
                report.AppendLine("");
                report.AppendLine("Answer:");
                report.AppendLine("");
                foreach (var item in question2b)
                {
                    report.AppendLine($"Environment: {item.Key}   Successful deployments: {item.Value}");
                }
                report.AppendLine("");

                report.AppendLine("2c) How does this break down by year?");
                report.AppendLine("-------------------------------------");
                report.AppendLine("");
                report.AppendLine("Answer:");
                report.AppendLine("");
                foreach (var item in question2c)
                {
                    report.AppendLine($"Year: {item.Key}   Successful deployments: {item.Value}");
                }
                report.AppendLine("");

                report.AppendLine("Question 3: Which is the most popular day of the week for live deployments");
                report.AppendLine("");
                report.AppendLine($"Answer: {question3}");
                report.AppendLine("");

                report.AppendLine("Question 4: What is the average length of time a release takes from integration to live, by project group");
                report.AppendLine("");
                report.AppendLine($"Answer: {question3}");
                report.AppendLine("");
                foreach (var item in question4)
                {
                    report.AppendLine($"Project group: {item.Key}   Average time: [Days: {item.Value.Days}, Minutes {item.Value.Minutes}, Seconds {item.Value.Seconds}]");
                }
                report.AppendLine("");

                // Q5
                report.AppendLine("Question 5: Please provide a break down by project group of success and unsuccessful deployments (success being releases that are deployed to live)");
                report.AppendLine("the number of deployments involved in the release pipeline and whether some environments had to be repeatedly deployed.");
                report.AppendLine("---------------------------------------------------------------------------------------------------------------------------------------------------");
                report.AppendLine("");

                foreach (var project in question5)
                {
                    
                    var unsuccessfulReleases = project
                        .SelectMany(x => x.Releases)
                        .Where(x => x.Deployments
                            .Any(y => y.Environment == "Live" && y.State != "Success"))
                        .ToList();

                    var successfulReleases = project
                        .SelectMany(x => x.Releases)
                        .Where(x => x.Deployments
                            .Any(y => y.Environment == "Live" && y.State == "Success"))
                        .ToList();

                    report.AppendLine($"Project Group: {project.Key}");
                    report.AppendLine("");
                    report.AppendLine($"Successful Releases: {successfulReleases.Count}");
                    report.AppendLine($"Unsuccessful Releases: {unsuccessfulReleases.Count}");
                    
                    if (successfulReleases.Any())
                    {
                        report.AppendLine("");
                        report.AppendLine(" Successful Releases");
                        report.AppendLine("");

                        foreach (var release in successfulReleases)
                        {
                            var distinctEnvironments = release.Deployments.Select(x => x.Environment).Distinct().Count();
                            var repeatedlyDeployedEnvironments = string.Join(", ",
                                release.Deployments
                                    .OrderBy(x => x.Environment)
                                    .GroupBy(x => x.Environment)
                                    .Where(x => x.Count() > 1)
                                    .Select(x => x.Key));

                            report.AppendLine($"    Version:                            {release.Version}");
                            report.AppendLine($"    Number of distinct deployments:     {distinctEnvironments}");
                            report.AppendLine($"    Repeatedly deployed environments:   {repeatedlyDeployedEnvironments}");
                            report.AppendLine("     ------------------------------------------------------");
                        }
                        report.AppendLine("");
                    }

                    if (unsuccessfulReleases.Any())
                    {
                        report.AppendLine(" Unsuccessful Releases");
                        report.AppendLine("");

                        foreach (var release in unsuccessfulReleases)
                        {
                            var distinctEnvironments = release.Deployments.Select(x => x.Environment).Distinct().Count();
                            var repeatedlyDeployedEnvironments = string.Join(", ",
                                release.Deployments
                                    .OrderBy(x => x.Environment)
                                    .GroupBy(x => x.Environment)
                                    .Where(x => x.Count() > 1)
                                    .Select(x => x.Key));

                            report.AppendLine($"    Version:                            {release.Version}");
                            report.AppendLine($"    Number of distinct deployments:     {distinctEnvironments}");
                            report.AppendLine($"    Repeatedly deployed environments:   {repeatedlyDeployedEnvironments}");
                            report.AppendLine("     ------------------------------------------------------");
                        }
                        report.AppendLine("");
                    }
                    
                    report.AppendLine("");
                }

                return report.ToString();
            }
            catch (Exception e)
            {
                throw new Exception($"Error when generating report: {e.Message}");
            }
        }
    }
}
