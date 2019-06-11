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

        LoginPage loginPage;
        JobsPage jobsPage;
        JobsDetailsPage jobsDetailsPage;

        public Tests(Platform platform)
        {
            this.platform = platform;
            this.loginPage = new LoginPage();
            this.jobsPage = new JobsPage();
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

            // Scroll down a bit
            app.ScrollDown();
            app.Screenshot("Scrolled down");
            Assert.IsTrue(app.Query("jobItem").Any(), "No Jobs found.");

            // Open last job on page
            var lastJobName = app.Query("lblName").FirstOrDefault()?.Text;
            Assert.IsTrue(lastJobName != null, "No Job Title Element found to click on.");
            app.Tap(lastJobName);
            app.WaitForElement(jobsDetailsPage.JobName);
            app.Screenshot("Job Details shown.");

            // Check if displayed name is correct
            var jobNameText = app.Query(jobsDetailsPage.JobName).First().Text;
            Assert.AreEqual(jobNameText, lastJobName, "Selected Job and displayed Job Details do not match.");
        }
    }
}
