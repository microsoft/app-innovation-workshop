using System;
using Xamarin.UITest.Queries;

namespace ContosoFieldService.UITests.PageObjects
{
    public class LoginPage
    {
        public readonly Func<AppQuery, AppQuery> ProceedButton = e => e.Marked("btnProceed");

        public LoginPage()
        {
        }
    }
}
