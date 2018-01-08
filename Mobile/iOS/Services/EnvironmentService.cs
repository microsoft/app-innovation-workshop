using System;
using ContosoFieldService.Abstractions;
using ContosoFieldService.iOS.Services;
using ObjCRuntime;

[assembly: Xamarin.Forms.Dependency(typeof(EnvironmentService))]
namespace ContosoFieldService.iOS.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public bool IsRunningInRealWorld()
        {
#if DEBUG
            return false;
#endif

            if (Runtime.Arch == Arch.SIMULATOR ||
                Environment.GetEnvironmentVariable("XAMARIN_TEST_CLOUD") != null)
                return false;

            return true;
        }
    }
}
