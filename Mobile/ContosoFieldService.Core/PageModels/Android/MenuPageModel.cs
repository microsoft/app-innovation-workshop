using System.Collections.Generic;
using System.Windows.Input;
using FormsToolkit;
using FreshMvvm;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels.Android
{
    public class MenuPageModel : FreshBasePageModel
    {
        public ICommand NavigateCommand { get; set; }

        public List<MenuItem> MenuItems { get; set; }
        public MenuItem SelectedItem { get; set; }

        public MenuPageModel()
        {
            NavigateCommand = new Command(NavigateToPageModel);

            MenuItems = new List<MenuItem>();

            MenuItems.Add(new MenuItem() { Value = "Dashboard", Label = "Dashboard", Image = "icon_dashboard.png" });
            MenuItems.Add(new MenuItem() { Value = "Jobs", Label = "Jobs", Image = "icon_jobs.png" });
            MenuItems.Add(new MenuItem() { Value = "Parts", Label = "Parts", Image = "icon_parts.png" });
            MenuItems.Add(new MenuItem() { Value = "Me", Label = "Me", Image = "icon_user.png" });
        }

        void NavigateToPageModel()
        {
            MessagingService.Current.SendMessage<string>("NavigationTriggered", SelectedItem.Value);
        }
    }

    public class MenuItem
    {
        public string Label { get; set; }
        public string Image { get; set; }
        public string Value { get; set; }
    }
}
