![Banner](Assets/Banner.png)

# 1. Mobile Overview
  The mobile app currently run on both iOS and Android devices using Xamarin.Forms. Although UWP, macOS and Linux support should technically also work but they're outside the scope of todays learnings.

![iPhone App Design](Assets/AppDesign.png)

### 2.1 Development SDK
The apps have been built with [Xamarin.Forms](https://github.com/xamarin/Xamarin.Forms) targetting .NET Standard 2.0. You should find all your favourite .NET libraries will work with the both the backend (also targeting .NET Standard 2.0) and the mobile apps. We're using C#, though we could have picked F#. We opted not to, given we're terrible F# developers and we want you to learn something today. If you fancy learning a little bit of information on functional programming with Forms, then checkout the [blog post](http://www.charlespetzold.com/blog/2015/10/Writing-Xamarin-Forms-Apps-in-FSharp.html) by Charles Petzold (author of the [Xamarin.Forms ebook](https://developer.xamarin.com/guides/xamarin-forms/creating-mobile-apps-xamarin-forms/)), who writes a guide on how to get started. 

### Introduction to Xamarin.Forms 
Xamarin.Forms is an open-source cross-platform developement library for building native apps for iOS, Android, Mac, Windows, Linux and more. By picking Xamarin.Forms, we're able to reuse our previous experiance with Silverlight, WPF and UWP development to target a variety of new platforms. 

Xamarin.Forms comes with 24 controls out of the box, which map directly to their native type. For example, a Xamarin.Forms Button will actually create a Widget.Button on Android and UIKit.UIButton on iOS. Forms provides a consistant API across all the platforms it supports. This allows us to ensure that functionality we call on iOS will behave the same on Android.

Forms is a great way to leverage existing C# and .NET knowleadge to build apps for platforms you may have historically considered not .NET compatbile. 

### 2.2 Mvvm with FreshMvvm
We opted to use [FreshMvvm](https://github.com/rid00z/FreshMvvm) as our Mvvm library due to its small size and flexibility. Its specifically designed for Xamarin.Forms and offers lots of helpful extensions around navigation which we make full use of. 

### 2.3 Architecture 
We've tried to keep platform specific code to a mininum with the development of this app. This is because we wanted you to see how its possible to create pleasent user experiances whilst maximising code reuse.


#### Core Project
The core project contains our app's Pages, ViewModels (we call them PageModels), Models and network services. 