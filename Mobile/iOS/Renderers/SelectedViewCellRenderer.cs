using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using ContosoFieldService.iOS.Renderers;

//[assembly: ExportRenderer(typeof(ViewCell), typeof(SelectedViewCellRenderer))]
namespace ContosoFieldService.iOS.Renderers
{
    public class SelectedViewCellRenderer : ViewCellRenderer
    {
        private UIView selectedBackground;

        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            if (selectedBackground == null)
            {
                selectedBackground = new UIView(cell.SelectedBackgroundView.Bounds);
                selectedBackground.Layer.BackgroundColor = Color.FromHex("#171f26").ToCGColor();
            }

            cell.SelectedBackgroundView = selectedBackground;

            return cell;
        }
    }
}
