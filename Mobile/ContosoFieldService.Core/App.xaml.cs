using ContosoFieldService.PageModels;
using FreshMvvm;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Xamarin.Forms;

namespace ContosoFieldService
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppCenter.Start(Helpers.Constants.AppCenterIOSKey + Helpers.Constants.AppCenterUWPKey + Helpers.Constants.AppCenterAndroidKey,
                            typeof(Analytics), typeof(Crashes), typeof(Push));

#if DEBUG
            Helpers.Settings.UserIsLoggedIn = false;
#endif
            var tabbedNavigation = new FreshTabbedFONavigationContainer(string.Empty, "authed");
            tabbedNavigation.AddTab<DashboardPageModel>("Dashboard", null);
            tabbedNavigation.AddTab<JobsPageModel>("Jobs", null);
            tabbedNavigation.AddTab<JobsPageModel>("Parts", null);
            tabbedNavigation.AddTab<ProfilePageModel>("Me", null);

            tabbedNavigation.BarBackgroundColor = Color.FromHex("#222E38");
            tabbedNavigation.BarTextColor = Color.White;
            tabbedNavigation.BackgroundColor = Color.FromHex("#222E38");
            MainPage = tabbedNavigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
