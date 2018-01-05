using System;
using System.Linq;
using ContosoFieldService.iOS.Helpers;
using ContosoFieldService.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace ContosoFieldService.iOS.Renderers
{
    public class ContentPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Some basic navigationbar styling.
            var color = Color.FromHex("#222E38");

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFont.FromName("MuseoSans-500", 18),
                TextColor = UIColor.White
            });

            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.SetBackgroundImage(color.ToUIColor().ToUIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.BarTintColor = color.ToUIColor();
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BackgroundColor = color.ToUIColor();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (NavigationController == null)
                return;

            // Move toolbaritems over to the left if we have more than 1.
            var navigationItem = NavigationController.TopViewController.NavigationItem;
            var leftNativeButtons = (navigationItem.LeftBarButtonItems ?? new UIBarButtonItem[] { }).ToList();
            var rightNativeButtons = (navigationItem.RightBarButtonItems ?? new UIBarButtonItem[] { }).ToList();

            if (rightNativeButtons.Count > 1)
            {
                var nativeItem = rightNativeButtons.Last();
                rightNativeButtons.Remove(nativeItem);
                leftNativeButtons.Add(nativeItem);
            }

            navigationItem.RightBarButtonItems = rightNativeButtons.ToArray();
            navigationItem.LeftBarButtonItems = leftNativeButtons.ToArray();


            ModalPresentationCapturesStatusBarAppearance = true;

            // Set the status bar to light
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            // Set the status bar to light
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }
    }
}
