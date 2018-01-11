using System;
using ContosoFieldService.Models;
using FreshMvvm;
using MvvmHelpers;
using Xamarin.Forms;
using ContosoFieldService.Services;
using System.Threading.Tasks;

namespace ContosoFieldService.PageModels
{
    public class PartsPageModel : FreshBasePageModel
    {
        public ObservableRangeCollection<Part> Parts { get; set; }
        public bool IsRefreshing { get; set; }
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
                    await CoreMethods.PushPageModel<PartDetailsPageModel>(part);
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
                    await CoreMethods.PushPageModel<CreateNewJobPageModel>(null, true, true);
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
                await ReloadData();
        }

        public override async void ReverseInit(object returndData)
        {
            base.ReverseInit(returndData);
            SelectedPart = null;
            await ReloadData();
        }

        #endregion

        #region Private Methods
        async Task ReloadData()
        {
            IsRefreshing = true;

            var parts = await partsApiService.GetPartsAsync();
            Parts.Clear();
            Parts.AddRange(parts);

            IsRefreshing = false;
        }
        #endregion

        #region Private Fields
        PartsAPIService partsApiService = new PartsAPIService();
        #endregion




    }
}
