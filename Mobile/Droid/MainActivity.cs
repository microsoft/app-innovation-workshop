using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Lottie.Forms.Droid;
using FFImageLoading.Forms.Droid;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Transformations;
using Xamarin;
using Microsoft.AppCenter.Push;
using Plugin.Permissions;
using ContosoFieldService.Services;
using Microsoft.Identity.Client;

namespace ContosoFieldService.Droid
{
	[Activity(Label = "Field Service", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			CachedImageRenderer.Init(false);
			//ImageCircleRenderer.Init();
			AnimationViewRenderer.Init();
			FormsMaps.Init(this, bundle);
			CarouselViewRenderer.Init();

			// Configure App Center Push
			Push.SetSenderId("597659151602");

			// Configure Authentication
			AuthenticationService.UIParent = new UIParent(Xamarin.Forms.Forms.Context as Activity);

			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
		}
	}
}
