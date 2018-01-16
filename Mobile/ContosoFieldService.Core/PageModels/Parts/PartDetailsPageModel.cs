using System;
using FreshMvvm;
using ContosoFieldService.Models;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class PartDetailsPageModel : FreshBasePageModel
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
    }
}
