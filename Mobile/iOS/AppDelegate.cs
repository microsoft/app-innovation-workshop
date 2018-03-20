using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Transformations;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using MikeCodesDotNET.iOS;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Plugin.VersionTracking;
using Microsoft.AppCenter.Distribute;

namespace ContosoFieldService.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            AnimationViewRenderer.Init();
            FormsMaps.Init();
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

            // Configure App Center
            Distribute.DontCheckForUpdatesInDebug();

            //HACK to get the linker to behave
            var ignore = new CircleTransformation();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
            Xamarin.Calabash.Start();
#endif

            var formsApp = new App();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            UITabBar.Appearance.BarTintColor = ((Color)formsApp.Resources["BackgroundColor"]).ToUIColor(); ;
            UITabBar.Appearance.TintColor = ((Color)formsApp.Resources["AccentColor"]).ToUIColor();

            LoadApplication(formsApp);

            return base.FinishedLaunching(app, options);
        }
    }
}
