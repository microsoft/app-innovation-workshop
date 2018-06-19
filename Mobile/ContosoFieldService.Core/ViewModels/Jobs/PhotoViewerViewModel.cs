using System;
using System.Windows.Input;
using ContosoFieldService.Models;
using FreshMvvm;
using Xamarin.Forms;

namespace ContosoFieldService.ViewModels
{
    public class PhotoViewerViewModel : FreshBasePageModel
    {
        public string PhotoUrl { get; set; }

        Command close;
        public Command Close => close ?? (close = new Command(async () =>
        {
            await CoreMethods.PopPageModel(false);
        }));


        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData is Photo photo)
                PhotoUrl = photo.LargeUrl;
        }
    }
}
