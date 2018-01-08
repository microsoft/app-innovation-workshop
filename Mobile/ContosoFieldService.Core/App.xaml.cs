using ContosoFieldService.Abstractions;
using ContosoFieldService.Helpers;
using ContosoFieldService.PageModels;
using FreshMvvm;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContosoFieldService
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if DEBUG
            Settings.UserIsLoggedIn = false;
#endif

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                var tabbedNavigation = new FreshTabbedNavigationContainer("authed");
                tabbedNavigation.AddTab<DashboardPageModel>("Dashboard", null);
                tabbedNavigation.AddTab<JobsPageModel>("Jobs", null);
                tabbedNavigation.AddTab<JobsPageModel>("Parts", null);
                tabbedNavigation.AddTab<ProfilePageModel>("Me", null);

                tabbedNavigation.BarBackgroundColor = Color.FromHex("#222E38");
                tabbedNavigation.BarTextColor = Color.White;
                tabbedNavigation.BackgroundColor = Color.FromHex("#222E38");
                MainPage = tabbedNavigation;
            }
            else
            {
                var navContainer = new CustomAndroidNavigation("AndroidNavigation");

                navContainer.Init("Menu", "hamburger.png");
                navContainer.AddPage<DashboardPageModel>("Start");
                navContainer.AddPage<JobsPageModel>("Jobs");
                navContainer.AddPage<JobsPageModel>("Parts");
                navContainer.AddPage<ProfilePageModel>("Me");

                MainPage = navContainer;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts

            // Only start Visual Studio App Center when running in a real-world scenario
            var environment = DependencyService.Get<IEnvironmentService>();
            if (environment.IsRunningInRealWorld())
            {
                AppCenter.Start(
                    Helpers.Constants.AppCenterIOSKey +
                    Helpers.Constants.AppCenterUWPKey +
                    Helpers.Constants.AppCenterAndroidKey,
                    typeof(Analytics), typeof(Crashes), typeof(Push));
            }
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
