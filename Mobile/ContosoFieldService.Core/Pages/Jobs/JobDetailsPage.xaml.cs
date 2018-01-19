using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using ContosoFieldService.PageModels;

namespace ContosoFieldService.Pages
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class JobDetailsPage : ContentPage
    {
        public JobDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var pageModel = BindingContext as JobDetailsPageModel;
            if (pageModel.Point != null)
            {
                var pos = new Position(pageModel.Point.Position.Latitude, pageModel.Point.Position.Longitude);

                // Move map to point
                mapView.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(1)));

                // Add pin
                mapView.Pins.Add(new Pin
                {
                    Label = "Position",
                    Type = PinType.Place,
                    Position = pos
                });
            }
            else
            {
                //TODO: Hide map when no point is specified
            }
        }
    }
}
