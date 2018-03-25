using FreshMvvm;
using Microsoft.AppCenter.Analytics;
using Xamarin.Forms;
using System.Windows.Input;
using System;
using ContosoFieldService.Services;
using ContosoFieldService.Helpers;

namespace ContosoFieldService.ViewModels
{
    public class LoginViewModel : FreshBasePageModel
    {
        readonly AuthenticationService authenticationService;

        public string GravatarSource { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        Command login;
        public Command Login => login ?? (login = new Command(async () =>
        {
            try
            {
                var result = await authenticationService.LoginAsync();
                if (result != null)
                {
                    Analytics.TrackEvent("User Logged In");
                    await CoreMethods.PopPageModel(true, true);
                }
                else
                {
                    await CoreMethods.DisplayAlert("Could not sign in", "Authentication failed.", "Ok");
                }
            }
            catch
            {
                await CoreMethods.DisplayAlert("Authentication failed", "Please make sure, that authentication got configured correctly. You can proceed without logging in for now.", "Ok");
            }
        }));

        Command proceedWithoutLogin;
        public Command ProceedWithoutLogin => proceedWithoutLogin ?? (proceedWithoutLogin = new Command(async () =>
        {
            Analytics.TrackEvent("User proceeded without logging in");
            await CoreMethods.PopPageModel(true, true);
        }));

        public LoginViewModel()
        {
            authenticationService = new AuthenticationService();
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Settings.LoginViewShown = true;
            CoreMethods.RemoveFromNavigation();
        }
    }
}
