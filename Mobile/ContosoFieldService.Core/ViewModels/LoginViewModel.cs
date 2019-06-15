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
        readonly IAuthenticationService authenticationService;

        public string GravatarSource { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        Command login;
        public Command Login => login ?? (login = new Command(async () =>
        {
            try
            {
                await authenticationService.LoginAsync();
                await CoreMethods.PopPageModel(true, true);
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
            authenticationService = new AzureADB2CAuthenticationService(
                Helpers.Constants.Tenant,
                Helpers.Constants.ClientID,
                Helpers.Constants.RedirectUri,
                Helpers.Constants.SignUpAndInPolicy,
                Helpers.Constants.Scopes,
                App.ParentUI,
                App.IOSKeyChainGroupName
            );
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Settings.LoginViewShown = true;
            CoreMethods.RemoveFromNavigation();
        }
    }
}
