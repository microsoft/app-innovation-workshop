using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Plugin.VersionTracking;
using Microsoft.AppCenter.Distribute;
using Microsoft.Identity.Client;

namespace ContosoFieldService.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            //AnimationViewRenderer.Init();
            FormsMaps.Init();
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

            // Configure App Center
            Distribute.DontCheckForUpdatesInDebug();

            //HACK to get the linker to behave
            var ignore = new CircleTransformation();

            // Code for starting up the Xamarin Test Cloud Agent
            // TODO: Make sure, the Agent is not started, when you want to distribute to the App Store.
            // Apple will reject that. Currently, we are UI Testing the Release version in Azure DevOps.
            // So a compiler flag would be a solution.
            Xamarin.Calabash.Start();
            
            var formsApp = new App();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            UITabBar.Appearance.BarTintColor = ((Color)formsApp.Resources["BackgroundColor"]).ToUIColor(); ;
            UITabBar.Appearance.TintColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor();

            LoadApplication(formsApp);

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(url);
            return true;
        }
    }
}
