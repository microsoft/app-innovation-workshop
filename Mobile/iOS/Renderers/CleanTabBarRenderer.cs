using ContosoFieldService.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CleanTabbedRenderer))]
namespace ContosoFieldService.iOS.Renderers
{
    public class CleanTabbedRenderer : TabbedRenderer
    {
        public CleanTabbedRenderer()
        {
            TabBar.Translucent = false;
            //TabBar.Opaque = true;

            // Remove shadow and separator line
            TabBar.BackgroundImage = new UIImage();
            TabBar.ShadowImage = new UIImage();
        }

    }
}