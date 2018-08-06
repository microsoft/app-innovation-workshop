using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using ContosoFieldService.ViewModels;

namespace ContosoFieldService.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobDetailsPage : ContentPage
    {
        public JobDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Setup map
            var pageModel = BindingContext as JobDetailsViewModel;
            if (pageModel?.Point?.Latitude != null && pageModel?.Point?.Longitude != null)
            {
                var pos = new Position(pageModel.Point.Latitude, pageModel.Point.Longitude);

                // Move map to point
                mapView.IsVisible = true;
                mapView.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(1)));

                // Add pin
                mapView.Pins.Add(new Pin
                {
                    Label = "Job",
                    Type = PinType.Place,
                    Position = pos
                });
            }
            else
            {
                // Hide map when no point is specified
                mapView.IsVisible = false;
            }
        }
    }
}
