using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
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
            FormsMaps.Init();
            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

            // Configure App Center
            Distribute.DontCheckForUpdatesInDebug();

            // HACK: Get the linker to behave
            var ignore = new CircleTransformation();

#if ENABLE_TEST_CLOUD
            // Code for starting up the Xamarin Test Cloud Agent
            Xamarin.Calabash.Start();
#endif

            var formsApp = new App();
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
            UITabBar.Appearance.BarTintColor = ((Color)formsApp.Resources["BackgroundColor"]).ToUIColor();
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
