using System;
using System.Collections.ObjectModel;
using ContosoFieldService.Models;
using FreshMvvm;
using Microsoft.AppCenter.Push;

namespace ContosoFieldService.PageModels
{
    public class SettingsPageModel : FreshBasePageModel
    {
        bool notificationsEnabled; 
        public ObservableCollection<ThirdPartyLibrary> ThirdPartyLibraries;

        public bool NotificationsEnabled
        {
            get
            {
                return notificationsEnabled;
            }
            set
            {
                notificationsEnabled = value; 
                Push.SetEnabledAsync(true);
            }
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            NotificationsEnabled = await Push.IsEnabledAsync();

            ThirdPartyLibraries = new ObservableCollection<ThirdPartyLibrary>();
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("CarouselView", "Alex Rainman", "https://github.com/alexrainman/CarouselView"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Lottie", "AirBnB, Martijn van Dijk", "https://github.com/martijn00/LottieXamarin"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Corcav.Behaviors", "Corrado Cavalli", "https://github.com/corradocavalli/Corcav.Behaviors"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Forms Toolkit", "James Montemagno", "https://github.com/jamesmontemagno/xamarin.forms-toolkit"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("FreshMvvm", "Michael Ridland", "https://github.com/rid00z/FreshMvvm"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Humanizer", "Mehdi Khalili", "https://github.com/Humanizr/Humanizer"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Refit", "Paul Betts", "https://github.com/paulcbetts/refit"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("MvvmHelpers", "James Montemagno", "https://github.com/jamesmontemagno/mvvm-helpers"));
        }
    }
}
