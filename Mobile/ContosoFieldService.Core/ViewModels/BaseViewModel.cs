using System;
using FreshMvvm;
using ContosoFieldService.Helpers;
using System.Threading.Tasks;

namespace ContosoFieldService.ViewModels
{
    public class BaseViewModel : FreshBasePageModel
    {
        protected async Task HandleResponseCodeAsync(ResponseCode code)
        {
            switch (code)
            {
                case ResponseCode.NotConnected:
                    await CoreMethods.DisplayAlert(
                        "Network Error",
                        "No internet connectivity found",
                        "OK");
                    break;

                case ResponseCode.ConfigurationError:
                    await CoreMethods.DisplayAlert(
                        "Backend Error",
                        "No backend connection has been specified or the specified URL is malformed.",
                        "Ok");
                    break;

                case ResponseCode.BackendNotFound:
                    await CoreMethods.DisplayAlert(
                        "Backend Error",
                        "Cannot communicate with specified backend. Maybe your call rate limit is exceeded.",
                        "Ok");
                    break;

                case ResponseCode.Error:
                    await CoreMethods.DisplayAlert(
                        "Backend Error",
                        "An error occured while communicating with the backend. Please check your settings and try again.",
                        "Ok");
                    break;
            }
        }
    }
}
