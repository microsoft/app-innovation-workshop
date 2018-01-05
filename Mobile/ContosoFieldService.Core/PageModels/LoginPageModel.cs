using System;
using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;

namespace ContosoFieldService.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public LoginPageModel()
        {
        }

        public Command Login
        {
            get
            {
                return new Command(async () => {
                    Helpers.Settings.UserIsLoggedIn = true;
                    Analytics.TrackEvent("User Logged In");
                    await CoreMethods.PopPageModel(true, true);
                });
            }

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            CoreMethods.RemoveFromNavigation();
        }
    }
}
