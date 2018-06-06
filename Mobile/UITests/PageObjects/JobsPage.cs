using System;
using Xamarin.UITest.Queries;

namespace ContosoFieldService.UITests.PageObjects
{
    public class JobsPage
    {
        public readonly string TestJobName;
        public readonly Func<AppQuery, AppQuery> JobsListView = e => e.Marked("jobsListView");
        public readonly Func<AppQuery, AppQuery> TestJob;

        public JobsPage(string testJobName)
        {
            TestJobName = testJobName;
            TestJob = e => e.Text(TestJobName);
        }
    }
}
