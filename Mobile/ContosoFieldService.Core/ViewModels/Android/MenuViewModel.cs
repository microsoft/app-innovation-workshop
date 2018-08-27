using System.Collections.Generic;
using System.Windows.Input;
using FormsToolkit;
using FreshMvvm;
using Xamarin.Forms;

namespace ContosoFieldService.ViewModels.Android
{
    public class MenuViewModel : FreshBasePageModel
    {
        public ICommand NavigateCommand { get; set; }

        public List<MenuItem> MenuItems { get; set; }
        public MenuItem SelectedItem { get; set; }

        public MenuViewModel()
        {
            NavigateCommand = new Command(NavigateToPageModel);

            MenuItems = new List<MenuItem>();

            //MenuItems.Add(new MenuItem() { Value = "Dashboard", Label = "Dashboard", Image = "icon_dashboard.png" });
            MenuItems.Add(new MenuItem() { Value = "Jobs", Label = "Jobs", Image = "icon_jobs.png" });
            MenuItems.Add(new MenuItem() { Value = "Parts", Label = "Parts", Image = "icon_parts.png" });
            MenuItems.Add(new MenuItem() { Value = "Me", Label = "Me", Image = "icon_user.png" });
            MenuItems.Add(new MenuItem() { Value = "Bot", Label = "Bot", Image = "icon_bot.png" });
            MenuItems.Add(new MenuItem() { Value = "Settings", Label = "Settings", Image = "icon_usersettings.png" });
        }

        void NavigateToPageModel()
        {
            MessagingService.Current.SendMessage<string>("NavigationTriggered", SelectedItem.Value);
            SelectedItem = null;
        }
    }

    public class MenuItem
    {
        public string Label { get; set; }
        public string Image { get; set; }
        public string Value { get; set; }
    }
}
