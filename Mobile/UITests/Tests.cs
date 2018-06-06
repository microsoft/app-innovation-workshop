using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using ContosoFieldService.UITests.PageObjects;

namespace ContosoFieldService.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        string testJobName = "Paint Boeing 737";

        LoginPage loginPage;
        JobsPage jobsPage;
        JobsDetailsPage jobsDetailsPage;

        public Tests(Platform platform)
        {
            this.platform = platform;
            this.loginPage = new LoginPage();
            this.jobsPage = new JobsPage(testJobName);
            this.jobsDetailsPage = new JobsDetailsPage();
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void ShowLoginPage()
        {
            app.WaitForElement(loginPage.ProceedButton);
            app.Screenshot("Login page shown.");
        }

        [Test]
        public void NavigateThroughJobs()
        {
            // Skip login
            app.WaitForElement(loginPage.ProceedButton);
            app.Tap(loginPage.ProceedButton);

            // Show list of Jobs
            app.WaitForElement(jobsPage.JobsListView);
            app.Screenshot("Jobs list shown.");

            // Scroll down to Paint Boeing 737 job
            app.ScrollDownTo(jobsPage.TestJob);
            app.Screenshot("Paint Boeing 737 job shown.");

            // Open job
            app.Tap(jobsPage.TestJob);
            app.WaitForElement(jobsDetailsPage.JobName);
            app.Screenshot("Job Details shown.");

            // Check if displayed name is correct
            var jobNameText = app.Query(jobsDetailsPage.JobName).First().Text;
            Assert.AreEqual(jobNameText, testJobName);
        }
    }
}
