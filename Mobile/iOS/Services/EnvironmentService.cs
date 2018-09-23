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
#if ENABLE_TEST_CLOUD
            return false;
#endif

            if (Runtime.Arch == Arch.SIMULATOR ||
                Environment.GetEnvironmentVariable("XAMARIN_TEST_CLOUD") != null)
                return false;

            return true;
        }
    }
}
