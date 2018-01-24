using ContosoFieldService.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(ColoredTabBarRenderer))]
namespace ContosoFieldService.iOS.Renderers
{
    public class ColoredTabBarRenderer : TabbedRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            TabBar.TintColor = Color.FromHex("#2dc9d7").ToUIColor();

            if (TabBar?.Items == null)
                return;

            // Go through our elements and change the icons
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                    UpdateTabBarItem(TabBar.Items[i], tabs.Children[i].Icon);
            }

            base.ViewWillAppear(animated);
        }

        private void UpdateTabBarItem(UITabBarItem item, string icon)
        {
            if (item == null || icon == null)
                return;

            // Set our different selected icons
            //icon = icon.Replace(".png", "_selected.png");

            if (item?.SelectedImage?.AccessibilityIdentifier == icon)
                return;

            //item.SelectedImage = UIImage.FromBundle(icon);
            //item.SelectedImage.AccessibilityIdentifier = icon;

            // Set the font for the title.
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("MuseoSans-500", 10), TextColor = Color.FromHex("#757575").ToUIColor() }, UIControlState.Normal);
            item.SetTitleTextAttributes(new UITextAttributes() { Font = UIFont.FromName("MuseoSans-500", 10), TextColor = Color.FromHex("#2dc9d7").ToUIColor() }, UIControlState.Selected);

            // Moves the titles up just a bit.
            item.TitlePositionAdjustment = new UIOffset(0, -2);
        }
    }
}
