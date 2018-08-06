
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CarouselView.FormsPlugin.Android;
using ContosoFieldService.Services;
using FFImageLoading.Forms.Droid;
using Firebase;
using Microsoft.AppCenter.Push;
using Microsoft.Identity.Client;
using Plugin.CurrentActivity;
using Xamarin;
using Xamarin.Forms.Platform.Android.AppLinks;

namespace ContosoFieldService.Droid
{
    [Activity(
        Label = "Field Service",
        Icon = "@mipmap/icon",
        RoundIcon = "@mipmap/icon_round",
        Theme = "@style/MyTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ResizeableActivity = true)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault }, DataScheme = "http", DataHost = "contosomaintenance.azurewebsites.net", DataPathPrefix = "/part/")]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault }, DataScheme = "https", DataHost = "contosomaintenance.azurewebsites.net", DataPathPrefix = "/part/")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            CachedImageRenderer.Init(false);
            //AnimationViewRenderer.Init();
            FormsMaps.Init(this, bundle);
            CarouselViewRenderer.Init();
            Xamarin.Essentials.Platform.Init(this, bundle);

            // Configure App Center Push
            Push.SetSenderId("597659151602");

            // Initialize App Indexing and Deep Links
            FirebaseApp.InitializeApp(this);
            AndroidAppLinks.Init(this);

            // Configure Authentication
            AuthenticationService.UIParent = new UIParent(this);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}
