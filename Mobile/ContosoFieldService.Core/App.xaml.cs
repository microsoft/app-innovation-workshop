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
using System.Linq;

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

            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                var tabbedNavigation = new FreshTabbedNavigationContainer("authed");
                //tabbedNavigation.AddTab<DashboardPageModel>("Dashboard", "icon_dashboard.png");
                tabbedNavigation.AddTab<JobsPageModel>("Jobs", "icon_jobs.png");
                tabbedNavigation.AddTab<PartsPageModel>("Parts", "icon_parts.png");
                tabbedNavigation.AddTab<ProfilePageModel>("Me", "icon_user.png");

                tabbedNavigation.BarBackgroundColor = Color.FromHex("#222E38");
                tabbedNavigation.BarTextColor = Color.White;
                tabbedNavigation.BackgroundColor = Color.FromHex("#222E38");
                MainPage = tabbedNavigation;
            }
            else
            {
                var navContainer = new CustomAndroidNavigation("AndroidNavigation");
                navContainer.Init("Menu", "hamburger.png");
                //navContainer.AddPage<DashboardPageModel>("Start");
                navContainer.AddPage<JobsPageModel>("Jobs");
                navContainer.AddPage<PartsPageModel>("Parts");
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

    public static class PageModelLocator
    {

        static JobsPageModel jobsPageModel;
        public static JobsPageModel JobsPageModel => jobsPageModel ?? (jobsPageModel = new JobsPageModel { Jobs = DummyData.GetGroupedDummyJobs() });

        static JobDetailsPageModel jobDetailsPageModel;
        public static JobDetailsPageModel JobDetailsPageModel
        {
            get
            {
                if (jobDetailsPageModel == null)
                {
                    jobDetailsPageModel = new JobDetailsPageModel();
                    jobDetailsPageModel.Init(DummyData.GetDummyJobs().First());
                }

                return jobDetailsPageModel;

            }
        }

        static PartsPageModel partsPageModel;
        public static PartsPageModel PartsPageModel => partsPageModel ?? (partsPageModel = new PartsPageModel { Parts = DummyData.GetDummyParts() });

    }
}
