using System;
using System.Collections.ObjectModel;
using ContosoFieldService.Models;
using FreshMvvm;
using Microsoft.AppCenter.Push;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ContosoFieldService.ViewModels
{
    public class SettingsViewModel : FreshBasePageModel
    {
        #region Configuration Settings

        string baseUrl;
        public string BaseUrl
        {
            get { return baseUrl; }
            set
            {
                baseUrl = value;
                Helpers.Constants.BaseUrl = value;
                RaisePropertyChanged();
            }
        }

        string apiManagementKey;
        public string ApiManagementKey
        {
            get { return apiManagementKey; }
            set
            {
                apiManagementKey = value;
                Helpers.Constants.ApiManagementKey = value;
                RaisePropertyChanged();
            }
        }

        string azureADB2CTenant;
        public string AzureADB2CTenant
        {
            get { return azureADB2CTenant; }
            set
            {
                azureADB2CTenant = value;
                Helpers.Constants.Tenant = value;
                RaisePropertyChanged();
            }
        }

        string azureADB2CApplicationId;
        public string AzureADB2CApplicationId
        {
            get { return azureADB2CApplicationId; }
            set
            {
                azureADB2CApplicationId = value;
                Helpers.Constants.ApplicationId = value;
                RaisePropertyChanged();
            }
        }

        string azureADB2CSignUpSignInPolicy;
        public string AzureADB2CSignUpSignInPolicy
        {
            get { return azureADB2CSignUpSignInPolicy; }
            set
            {
                azureADB2CSignUpSignInPolicy = value;
                Helpers.Constants.SignUpAndInPolicy = value;
                RaisePropertyChanged();
            }
        }

        string azureADB2CScope;
        public string AzureADB2CScope
        {
            get { return azureADB2CScope; }
            set
            {
                azureADB2CScope = value;
                Helpers.Constants.Scopes[0] = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public ObservableCollection<ThirdPartyLibrary> ThirdPartyLibraries { get; set; }
        public string Version { get; set; }

        bool notificationsEnabled;
        public bool NotificationsEnabled
        {
            get
            {
                return notificationsEnabled;
            }
            set
            {
                notificationsEnabled = value;
                Push.SetEnabledAsync(notificationsEnabled);
            }
        }

        ThirdPartyLibrary selectedLibrary;
        public ThirdPartyLibrary SelectedLibrary
        {
            get
            {
                return selectedLibrary;
            }
            set
            {
                selectedLibrary = value;
                RaisePropertyChanged();
                if (value != null)
                    OpenUrlCommand.Execute(selectedLibrary.Url);
            }
        }

        public Command GitHubCommand
        {
            get
            {
                return new Command(() =>
                {
                    Device.OpenUri(new Uri("http://aka.ms/mobilecloudworkshop"));
                });
            }
        }

        public Command<string> OpenUrlCommand
        {
            get
            {
                return new Command<string>((string url) =>
                {
                    Device.OpenUri(new Uri(url));
                    SelectedLibrary = null;
                });
            }
        }

        public SettingsViewModel()
        {
            Version = $"{VersionTracking.CurrentVersion} (Build {VersionTracking.CurrentBuild})";

            ThirdPartyLibraries = new ObservableCollection<ThirdPartyLibrary>();
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("CarouselView", "Alex Rainman", "https://github.com/alexrainman/CarouselView"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Lottie", "AirBnB, Martijn van Dijk", "https://github.com/martijn00/LottieXamarin"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Corcav.Behaviors", "Corrado Cavalli", "https://github.com/corradocavalli/Corcav.Behaviors"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Forms Toolkit", "James Montemagno", "https://github.com/jamesmontemagno/xamarin.forms-toolkit"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("FreshMvvm", "Michael Ridland", "https://github.com/rid00z/FreshMvvm"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Humanizer", "Mehdi Khalili", "https://github.com/Humanizr/Humanizer"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Refit", "Paul Betts", "https://github.com/paulcbetts/refit"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("MvvmHelpers", "James Montemagno", "https://github.com/jamesmontemagno/mvvm-helpers"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("FFImageLoading", "Daniel Luberda", "https://github.com/luberda-molinet/FFImageLoading"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Visual Studio App Center", "Microsoft", "http://appcenter.ms"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Version Tracking Plugin", "Colby Williams", "https://github.com/colbylwilliams/VersionTrackingPlugin"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("Media Plugin for Xamarin and Windows", "James Montemagno", "https://github.com/jamesmontemagno/MediaPlugin"));
            ThirdPartyLibraries.Add(new ThirdPartyLibrary("FlowListView for Xamarin.Forms", "Daniel Luberda", "https://github.com/daniel-luberda/DLToolkit.Forms.Controls/tree/master/FlowListView"));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Version = $"{VersionTracking.CurrentVersion} (Build {VersionTracking.CurrentBuild})";
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            NotificationsEnabled = await Push.IsEnabledAsync();
            BaseUrl = Helpers.Constants.BaseUrl;
            ApiManagementKey = Helpers.Constants.ApiManagementKey;
            AzureADB2CTenant = Helpers.Constants.Tenant;
            AzureADB2CApplicationId = Helpers.Constants.ApplicationId;
            AzureADB2CSignUpSignInPolicy = Helpers.Constants.SignUpAndInPolicy;
            AzureADB2CScope = Helpers.Constants.Scopes[0];
        }
    }
}
