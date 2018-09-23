using System;
using Xamarin.UITest.Queries;

namespace ContosoFieldService.UITests.PageObjects
{
    public class JobsPage
    {
        public readonly Func<AppQuery, AppQuery> JobsListView = e => e.Marked("jobsListView");
    }
}
