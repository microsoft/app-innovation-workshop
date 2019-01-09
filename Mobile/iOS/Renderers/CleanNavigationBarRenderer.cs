using ContosoFieldService.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CleanNavigationBarRenderer))]
namespace ContosoFieldService.iOS.Renderers
{
    public class CleanNavigationBarRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                Font = UIFont.FromName("MuseoSans-500", 18),
                TextColor = UIColor.FromRGB(0, 216, 203)
            });

            // Remove transparency, shadow and separator line
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);

            // Set color of Back Button and Navigation Bar Buttons
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(0, 216, 203);
        }
    }
}