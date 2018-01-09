using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {

        public string GravatarSource { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        public Command Login
        {
            get
            {
                return new Command(async () => {
                    Helpers.Settings.UserIsLoggedIn = true;
                    Analytics.TrackEvent("User Logged In");

                    Helpers.Settings.FullName = FullName;
                    Helpers.Settings.Email = Email;

                    await CoreMethods.PopPageModel(true, true);
                });
            }
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CoreMethods.RemoveFromNavigation();
        }

        public Command EmailSet
        {
            get
            {
                return new Command(async () => {
                    GravatarSource = Helpers.Extensions.EmailToGravatarUrl(Email);
                   
                });
            }
        }

    }
}
