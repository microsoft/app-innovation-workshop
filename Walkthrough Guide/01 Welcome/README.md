# Welcome  
Welcome to the Azure Mobile Workshop. Today you’re going to learn how to leverage your existing skills to build highly reliable backend systems to power a modern cross platform mobile app. 

We’ve tried to make this workshop as realistic as possible and provide a good foundation for if you wish to pursue development in the future. Think of this as your development starter kit. Where we think theres area for improvement within the code base, you’ll find explanations about how to improve them. 

## 1. Mobile App
The mobile app currently run on both iOS and Android devices. 

### 1.1 Development SDK
They have been built using Xamarin.Forms, which unlocks the possibility of supporting UWP, macOS and Linux in the future, should we find time (Pull Requests are accepted ✌️)

We built the app to target .NET Standard, do you’ll find all your favourite .NET libraries should work with this solution. 

### 1.2 Mvvm with FreshMvvm
We opted to use [FreshMvvm](https://github.com/rid00z/FreshMvvm) as our Mvvm library due to its small size and flexibility. Its specifically designed for Xamarin.Forms and offers lots of helpful extensions around navigation which we make full use of. 

### 1.3 Network Requests
To simplify our services, we use a popular REST library called Refit. It’s inspired by Sqaure’s Retrofit library. It allows us to define our REST API as an interface and pass it into a _RestService_. 

```csharp // Refit Interface definition [Headers(Helpers.Constants.ApiManagementKey)]
public interface IJobsAPI
{
    [Get("/api/{job}")]
    Task<List<Job>> GetJobs();
}
```


We can then use the interface 
```csharp
// Fetch some data from our endpoint. 
var jobsApi = RestService.For<IJobsAPI>("https://contosomaintenance.portal.azure-api.net");

var jobs = await jobsApi.GetJobs();
```

Its fairly common to see Xamarin developers use Refit along side other libraries such as Akavache, ModernHttpClient, Fusillade and Polly. This approach is explained in detail over on Rob Gibbons [blog](http://arteksoftware.com/resilient-network-services-with-xamarin/).  

We did consider implementing this entire approach but Akavache doesn’t currently support .NET Standard 2.0.  It’s a fairly key component in this approach, as its a persistent key-value store used to cache data. We’re currently investigating using [🐒MonkeyCache](https://github.com/jamesmontemagno/monkey-cache) in the future as this offers much greater flexibility. 

## 2. Azure Architecture 



### 2.1 API Management 

### 2.2 App Services

### 2.3 Services






