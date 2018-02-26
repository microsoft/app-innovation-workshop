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

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
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

            // As iOS and Android follow fundamenntally different navigation patterns, we split up the 
            // navigation style between iOS and Android here. iOS is using a Tabbed Navigation, 
            // while Android uses a Navigation Drawer (Hamburger Menu)
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                // Create tabbed navigation for iOS
                // Tab Bar Color is set in AppDelegate.cs within the iOS project
                var tabbedNavigation = new FreshTabbedFONavigationContainer("Contoso");
                tabbedNavigation.BarBackgroundColor = (Color)Current.Resources["BackgroundColorDark"];
                tabbedNavigation.BarTextColor = (Color)Current.Resources["AccentColor"];

                // Add first level navigationpages as tabs
                tabbedNavigation.AddTab<JobsPageModel>("Jobs", "icon_jobs.png");
                tabbedNavigation.AddTab<PartsPageModel>("Parts", "icon_parts.png");
                tabbedNavigation.AddTab<ProfilePageModel>("Me", "icon_user.png");
                MainPage = tabbedNavigation;
            }
            else
            {
                var navContainer = new CustomAndroidNavigation("AndroidNavigation");
                navContainer.Init("Menu", "hamburger.png");
                navContainer.AddPage<JobsPageModel>("Jobs");
                navContainer.AddPage<PartsPageModel>("Parts");
                navContainer.AddPage<ProfilePageModel>("Me");
                MainPage = navContainer;
            }
        }

        protected override void OnStart()
        {
            // Only start Visual Studio App Center when running in a real-world scenario
            // which means on a physical device in Release mode and not in a Test cloud
            // to make sure we have real insights in Visual Studio App Center
            var environment = DependencyService.Get<IEnvironmentService>();
            if (environment.IsRunningInRealWorld())
            {
                AppCenter.Start(
                    Helpers.Constants.AppCenterIOSKey +
                    Helpers.Constants.AppCenterUWPKey +
                    Helpers.Constants.AppCenterAndroidKey,
                    typeof(Analytics), typeof(Crashes), typeof(Push));
            }

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
