using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using UIKit;

using MikeCodesDotNET.iOS;
using FFImageLoading.Forms.Touch;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Transformations;

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

            CachedImageRenderer.Init();
            CarouselViewRenderer.Init();

            //HACK to get the linker to behave
            var ignore = new CircleTransformation();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
            Xamarin.Calabash.Start();
#endif

            LoadApplication(new App());

            UITabBar.Appearance.BarTintColor = "#00D8CB".ToUIColor();
            UITabBar.Appearance.TintColor = UIColor.White;

            UINavigationBar.Appearance.BackgroundColor = "#222E38".ToUIColor();
            UINavigationBar.Appearance.TintColor = "#00D8CB".ToUIColor();
            return base.FinishedLaunching(app, options);
        }
    }
}
