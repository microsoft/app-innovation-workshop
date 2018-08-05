using System;
using Xamarin.UITest.Queries;

namespace ContosoFieldService.UITests.PageObjects
{
    public class JobsDetailsPage
    {
        public readonly Func<AppQuery, AppQuery> JobName = e => e.Marked("JobName");
    }
}
