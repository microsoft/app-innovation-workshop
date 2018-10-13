using System;
using FreshMvvm;
using ContosoFieldService.Models;
using Xamarin.Forms;

namespace ContosoFieldService.ViewModels
{
    public class PartDetailsViewModel : FreshBasePageModel
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public decimal PriceInUSD { get; set; }
        public string ImageSource { get; set; }

        Part Part;

        public override void Init(object initData)
        {
            if (initData != null)
            {

                Part = (Part)initData;
                Name = Part.Name;
                Manufacturer = Part.Manufacturer;
                ModelNumber = Part.ModelNumber;
                SerialNumber = Part.SerialNumber;
                PartNumber = Part.PartNumber;
                PriceInUSD = Part.PriceInUSD;
                ImageSource = Part.ImageSource;
                CreateDeepLinkEntry();
            }
            else
            {
                Part = new Part();
            }
        }

        public Command OrderPart
        {
            get
            {
                return new Command(async () =>
                {
                    //TODO: Needs to be implemented
                    await CoreMethods.DisplayAlert("Not implemented yet.", ":(", "OK");
                });
            }
        }

        void CreateDeepLinkEntry()
        {
            var url = $"{Helpers.Constants.BaseUrl}/part/{Part.Id}";

            var entry = new AppLinkEntry
            {
                Title = Part.Name,
                Description = Part.Manufacturer,
                AppLinkUri = new Uri(url, UriKind.RelativeOrAbsolute),
                IsLinkActive = true,
                Thumbnail = Xamarin.Forms.ImageSource.FromFile("icon_greentool.png")
            };

            entry.KeyValues.Add("contentType", "Parts");
            entry.KeyValues.Add("appName", "Field Service");
            entry.KeyValues.Add("companyName", "Contoso Maintenance");

            try
            {
                Application.Current.AppLinks.RegisterLink(entry);
            }
            catch (Exception)
            {
                // Crashes on Android currently
                // TODO: Fix DeepLink support on Android
            }

        }

    }
}
