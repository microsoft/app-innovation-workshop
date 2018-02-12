using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using ContosoFieldService.iOS.Renderers;
using Foundation;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(SearchBar), typeof(CustomSearchBarRenderer))]
namespace ContosoFieldService.iOS.Renderers
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            // Match text field within SearchBar to its background color
            using (var searchKey = new NSString("_searchField"))
            {
                var textField = (UITextField)Control.ValueForKey(searchKey);
                textField.BackgroundColor = e.NewElement.BackgroundColor.ToUIColor();
                textField.TintColor = UIColor.White;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Hide Cancel Button
            if (e.PropertyName == "Text")
            {
                Control.ShowsCancelButton = false;
            }
        }
    }
}
