using System;
using ContosoFieldService.Models;
using FreshMvvm;
using MvvmHelpers;
using Xamarin.Forms;
using ContosoFieldService.Services;
using System.Threading.Tasks;

namespace ContosoFieldService.ViewModels
{
    public class PartsViewModel : FreshBasePageModel
    {
        public ObservableRangeCollection<Part> Parts { get; set; }
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; RaisePropertyChanged(); }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; RaisePropertyChanged(); }
        }

        public string SearchText { get; set; }

        Part selectedPart;
        public Part SelectedPart
        {
            get
            {
                return selectedPart;
            }
            set
            {
                selectedPart = value;
                RaisePropertyChanged();
                if (value != null)
                    PartSelected.Execute(value);
            }
        }

        #region Bindable Commands
        public Command Refresh
        {
            get
            {
                return new Command(async () =>
                {
                    await ReloadData();
                });
            }
        }

        public Command<Part> PartSelected
        {
            get
            {
                return new Command<Part>(async (part) =>
                {
                    await CoreMethods.PushPageModel<PartDetailsViewModel>(part);
                });
            }
        }

        public Command Search
        {
            get
            {
                return new Command(async () =>
                {
                    var searchResults = await partsApiService.SearchPartsAsync(SearchText);
                    Parts.Clear();
                    Parts.AddRange(searchResults);
                });
            }
        }

        public Command AddJobClicked
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<CreateNewJobViewModel>(null, true, true);
                });
            }
        }

        #endregion

        #region Overrides
        public override void Init(object initData)
        {
            base.Init(initData);

            Parts = new ObservableRangeCollection<Part>();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (Parts.Count == 0)
                await ReloadData(true);
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            SelectedPart = null;
        }

        public override async void ReverseInit(object returndData)
        {
            base.ReverseInit(returndData);
            SelectedPart = null;
            await ReloadData();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reloads the data.
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="isSilent">If set to <c>true</c> no loading indicators will be shown.</param>
        /// <param name="force">If set to <c>true</c> cache will be ignored.</param>
        async Task ReloadData(bool isSilent = false, bool force = false)
        {
            IsRefreshing = !isSilent;
            IsLoading = true;

            try
            {
                // Inform user about missing connectivity but proceed as data could have been cached
                if (!Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
                    await CoreMethods.DisplayAlert("Network Error", "No internet connectivity found", "OK");

                // Get parts from server or cache
                var newParts = await partsApiService.GetPartsAsync();
                Parts.ReplaceRange(newParts);
            }
            // TODO: Handle Exceptions centralized in BaseViewModel
            catch (UriFormatException)
            {
                // No or invalid BaseUrl set in Constants.cs
                await CoreMethods.DisplayAlert(
                    "Backend Error",
                    "No backend connection has been specified or the specified URL is malformed.",
                    "Ok");
            }
            catch (ArgumentException)
            {
                // Backend not found at specified BaseUrl in Constants.cs or call limit reached
                await CoreMethods.DisplayAlert(
                    "Backend Error",
                    "Cannot communicate with specified backend. Maybe your call rate limit is exceeded.",
                    "Ok");
            }
            catch (Exception ex)
            {
                // Everything else
                await CoreMethods.DisplayAlert(
                    "Backend Error",
                    "An error occured while communicating with the backend. Please check your settings and try again.",
                    "Ok");
            }

            IsRefreshing = false;
            IsLoading = false;
        }

        #endregion

        #region Private Fields
        PartsAPIService partsApiService = new PartsAPIService();
        bool isRefreshing;
        bool isLoading;
        #endregion




    }
}
