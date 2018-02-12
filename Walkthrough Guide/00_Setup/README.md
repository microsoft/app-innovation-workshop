![Banner](Assets/Banner.png)

# Setting Up	
> Hint: We highly recommend you setup and configure your system before attending the mobile workshop. Although we’ve allowed an hour in the morning to assist in trouble shooting configurations, we won’t have time to do a fresh installation.   
- - - -

The setup of a Xamarin development environment can get a little bit tricky and time consuming as it has dependancies on many SDKs and technologies from different companies. 

For todays workshop, you can use both, Windows 10 or macOS to develop. You’ll find all the documentation demonstrations using macOS but keep in mind, everything you see in Visual Studio for Mac is possible in Visual Studio for PC. 

## Install Xamarin

### On Windows
When working on Windows, [Visual Studio](https://www.visualstudio.com/downloads/) will be the right IDE for you. You can check if you have a license for the paid versions or even go with the free Community Edition. Both will work for you.

#### Visual Studio 2017
For a quick guide on how to prepare it for Xamarin Development, please [follow this guide](http://motzcod.es/post/158155898027/setting-up-vs-2017-for-xamarin-dev)!

![Add Xamarin to Visual Studio installation Screenshot](../Misc/vsinstallxamarinfeatures.png)
Make sure, that the following features get installed:
- [ ] Windows and Web Development
	- [ ] Universal Windows App Development Tools
		- [ ] Tools and Windows 10 SDK
		- [ ] Windows 10 SDK (10.0.14393)
		- [ ] Windows 10 SDK (10.0.10240)
	- [ ] Windows 8.1 and Windows Phone 8.0/8.1 Tools
		- [ ] Tools and SDKs
- [ ] Cross Plaform Mobile Development
	- [ ] C#/.NET (Xamarin)
	- [ ] Microsoft Visual Studio Emulator for Android (optional)
	- [ ] Common Tools and Software Development Kits
		- [ ] Android Native Development Kit (T10E, 32 bits)
		- [ ] Android SDK
		- [ ] Android SDK Setup (API Level 19 and 21)
		- [ ] Android SDK Setup (API Level 23)
		- [ ] Java SE Development Kit

> Hint: If you already have Visual Studio installed, you can modify its features via the **Programs and Features** tool at the **Control Panel**. Locate **Microsoft Visual Studio** there and click the <kbd>Change</kbd> button to rerun the installer.  
>   
> ![Modify Visual Studio features afterwards Screenshot](../Misc/winchangevsfeatures.png)  

### On Mac
On a Mac, Xamarin needs to be installed differently. [Download the installer](https://www.xamarin.com/download) and run it. It should install all Xamarin components you need, including the Android SDK and Visual Studio for Mac, which will be your IDE on macOS.

## Prepare for Android development
### Android SDK
Both ways of installing Xamarin for Mac or Windows should install the Android SDK automatically. If anything goes wrong, you can also [download it from the Google Developer portal](https://developer.android.com/studio/index.html).

You should also check if all the necessary Android components are installed or if any updates are available. We can check this inside the **Android SDK Manager**. Open it in Visual Studio or Visual Studio Studio for Mac at <kbd>Tools<_kbd> <kbd>Android<_kbd> <kbd>Android SDK Manager...</kbd>.

![Modify Visual Studio features afterwards Screenshot](../Misc/androidsdkmanager.png)

Make sure, the following components are installed and up to date:

- [ ] Tools
	- [ ] Android SDK Tools
	- [ ] Android SDK Platform-tools
	- [ ] Android SDK Build-tools (23.0.1)
	- [ ] Android SDK Build-tools (23.0.1)
- [ ] Android 6.0 (API 23)
	- [ ] SDK Platform (optional but recommended)
- [ ] Android 5.0.1 (API 21)
	- [ ] SDK Platform (optional but recommended)
- [ ] Android 4.4.2 (API 19)
	- [ ] SDK Platform (optional but recommended)
- [ ] Android 4.0.3 (API 15)
	- [ ] SDK Platform
- [ ] Extras
	- [ ] Android Support Repository
	- [ ] Google Play Services (optional)

### Android Emulator
To test your Android apps, you might need an emulator. You can use any emulator you like, for example 
	- [ ] [Android Emulator for Visual Studio](https://www.visualstudio.com/vs/msft-android-emulator/) on Windows
	- [ ] Google Emulator that comes with [Android Studio](https://developer.android.com/studio/index.html)
	- [ ] [Genymotion](https://www.genymotion.com/#!/)

## Prepare for iOS development
### iOS SDK and Simulator
To work with iOS, you need a Mac with a copy of[Xcode](https://itunes.apple.com/de/app/xcode/id497799835?mt=12) installed. By default, this will install the latest iOS SDK and several simulators needed for iOS development. You will need to start Xcode at least once after the installation process in order to initialise everything.

### Mac Build Host for Windows
In case you are working on a Windows machine and still want to develop iOS applications, you will need a Mac as Remote Building host anywhere in your local network and connect it to Visual Studio.

First, make sure, that the Mac has Xamarin (in the exact same version as on Windows) and Xcode installed. Then click on <kbd>Tools<_kbd> <kbd>Options...<_kbd>, scroll down to the **Xamarin** section and click on **iOS Settings** to navigate to the configuration window. Here you can click on <kbd>Find Xamarin Mac Agent</kbd> to conenct you Mac. Follow the steps from the wizard to establish the connection.

![Connect Visual Studio with Mac Build Host Screenshot](../Misc/vsxamariniossettings.png)

### Remote iOS Simulator for Windows
Here you can also select the **Remote iOS Simulator** on Windows. If you check this, the iOS Simulator from the Mac will be streamed to your Windows machine and you can test your app there.

## Test your configuration
To test you development environment, you can download the simple blank test app that is [attached to this module](./Setup%20Test%20App) and try to run it on all platforms you want to target. If you run in any errors, head over to the [Troubleshooting](../Troubleshooting) section and check if your problem can be resolved easily.

1. Open the Solution by double-clicking on the `XamarinSetupTest.sln` file and wait until your IDE spins up.
2. Right-click on the Solution name at the top of the folder structure and select <kbd>Restore NuGet Packages</kbd> to download the packages
3. Wait until all packages have been restored
4. Android
		1. Right-click the **XamarinSetupTest.Droid** project, select <kbd>Set as StartUp Project</kbd>
		2. Make sure the Run settings at the top bar are **Debug**, (**Any CPU**) and a Android Emulator is selected
		3. Click the green **Run** button
		4. An Android Emulator should spin up and launch the app
5. iOS
		1. Only on Windows: Make sure that the Remote Mac Build Host is connected as shown above
		2. Right-click the **XamarinSetupTest.iOS** project, select <kbd>Set as StartUp Project</kbd>
		3. Make sure the Run settings at the top bar are **Debug**, **iPhone Simulator** and a iOS Simulator is selected
		4. Click the green **Run** button
		5. An iOS Simulator should spin up and launch the app

## Move on
If this works for the platforms you want to target, you are ready to move on. This was the hardest part. Now you can discover the beautiful world of Xamarin and Azure! 
