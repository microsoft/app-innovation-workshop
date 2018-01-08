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

            MenuItems.Add(new MenuItem() { Value = "Dashboard", Label = "Dashboard", Image = "start.png" });
            MenuItems.Add(new MenuItem() { Value = "Jobs", Label = "Jobs", Image = "me.png" });
            MenuItems.Add(new MenuItem() { Value = "Parts", Label = "Parts", Image = "me.png" });
            MenuItems.Add(new MenuItem() { Value = "Me", Label = "Me", Image = "me.png" });

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
