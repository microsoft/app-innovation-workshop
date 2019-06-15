using System;
using System.Collections.Generic;
using ContosoFieldService.Abstractions;
using ContosoFieldService.Helpers;
using ContosoFieldService.Services;
using ContosoFieldService.ViewModels;
using DLToolkit.Forms.Controls;
using FreshMvvm;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Push;
using Microsoft.Identity.Client;
using MonkeyCache.FileStore;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ContosoFieldService
{
    public partial class App : Application
    {
        public static object ParentUI { get; set; } = null;
        public static string IOSKeyChainGroupName { get; set; } = null;

        public App()
        {
            InitializeComponent();

            FlowListView.Init();

            // Configure Monkey Cache
            Barrel.ApplicationId = "ContosoFieldService";

            // Stop the keyboard overlaying the chatbot webview on Android (it isn't a problem on iOS).
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.Application.SetWindowSoftInputModeAdjust(this, Xamarin.Forms.PlatformConfiguration.AndroidSpecific.WindowSoftInputModeAdjust.Resize);

            // As iOS and Android follow fundamenntally different navigation patterns, we split up the 
            // navigation style between iOS and Android here. iOS is using a Tabbed Navigation, 
            // while Android uses a Navigation Drawer (Hamburger Menu)
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                // Create tabbed navigation for iOS
                // Tab Bar Color is set in AppDelegate.cs within the iOS project
                var tabbedNavigation = new FreshTabbedFONavigationContainer("Contoso");
                tabbedNavigation.BarBackgroundColor = (Color)Current.Resources["BackgroundColorDark"];
                tabbedNavigation.BarTextColor = Color.White; // Status Bar Color

                // Add first level navigationpages as tabs
                tabbedNavigation.AddTab<JobsViewModel>("Jobs", "icon_jobs.png");
                tabbedNavigation.AddTab<PartsViewModel>("Parts", "icon_parts.png");
                tabbedNavigation.AddTab<ProfileViewModel>("Me", "icon_user.png");
                MainPage = tabbedNavigation;
            }
            else
            {
                var navContainer = new CustomAndroidNavigation("AndroidNavigation");
                navContainer.Init("Menu", "hamburger.png");
                navContainer.AddPage<JobsViewModel>("Jobs");
                navContainer.AddPage<PartsViewModel>("Parts");
                navContainer.AddPage<ProfileViewModel>("Me");
                navContainer.AddPage<BotViewModel>("Bot");
                navContainer.AddPage<SettingsViewModel>("Settings");
                MainPage = navContainer;
            }
        }

        protected override async void OnStart()
        {
            // Handle when your app starts
            VersionTracking.Track();

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

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            var data = uri.ToString().ToLowerInvariant();
            //only if deep linking
            if (!data.Contains("/parts/"))
                return;

            var id = data.Substring(data.LastIndexOf("/", StringComparison.Ordinal) + 1);

            //Navigate based on id here.

            base.OnAppLinkRequestReceived(uri);
        }
    }
}
