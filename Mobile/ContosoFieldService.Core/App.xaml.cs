using System;
using System.Collections.Generic;
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
using Plugin.VersionTracking;
using Microsoft.AppCenter.Distribute;
using ContosoFieldService.Services;
using MonkeyCache.FileStore;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ContosoFieldService
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = "ContosoFieldService";

#if DEBUG
            Settings.LoginViewShown = false;
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

        protected override async void OnStart()
        {
            // Handle when your app starts
            CrossVersionTracking.Current.Track();

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
                    typeof(Analytics), typeof(Crashes), typeof(Push), typeof(Distribute));

                // Subscribe to Push Notification Event
                Push.PushNotificationReceived += PushNotificationReceived;

                // Track application start
                Analytics.TrackEvent("App Started", new Dictionary<string, string>
                {
                    { "Day of week", DateTime.Now.ToString("dddd") }
                });
            }
        }

        async void PushNotificationReceived(object sender, PushNotificationReceivedEventArgs e)
        {
            // Rudimentary handle push notifications
            if (e.Title != null || e.Message != null)
            {
                await MainPage?.DisplayAlert(e.Title, e.Message, "Ok");
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
