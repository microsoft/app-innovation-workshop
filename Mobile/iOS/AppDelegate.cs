using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using UIKit;

using MikeCodesDotNET.iOS;

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

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
			Xamarin.Calabash.Start();
#endif

            LoadApplication(new App());

            UITabBar.Appearance.BarTintColor = "#222E38".ToUIColor();
            UITabBar.Appearance.TintColor = UIColor.White;

            UINavigationBar.Appearance.BackgroundColor = "#222E38".ToUIColor();
            UINavigationBar.Appearance.TintColor = UIColor.White;
            return base.FinishedLaunching(app, options);
        }
    }
}
