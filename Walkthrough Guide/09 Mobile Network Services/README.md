![Banner](Assets/Banner.png)

# Mobile App Network Services 
Our mobile app connects to our Azure API Management sending HTTP requests to remote services to request resources. The implementation within this demo is very lightweight and designed for use in a POC rather than a production app. If you’d like to see a more resilient approach to building networking services then check out the “resilient networking” branch. Here we’ve implemented a [data caching and a request retry policy](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/b4833120d9ceb70abb8753581f133f3467665edd/Mobile/ContosoFieldService.Core/Services/JobsAPIService.cs#L45), which exponentially delays retry attempts.  We’ll cover this in more detail later, but for our standard app, we’re using an MVP approach. 

We separate out each API from the API management service that we’ll be interacting with. In this case, you’ll see the following directory structure in the [Xamarin.Forms main shared library.](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/tree/master/Mobile/ContosoFieldService.Core) 

### Structure
* [JobsAPIService.cs](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/master/Mobile/ContosoFieldService.Core/Services/JobsAPIService.cs)
	* _IJobServiceAPI_
	* _JobsAPIService_
* [PartsAPIService.cs](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/master/Mobile/ContosoFieldService.Core/Services/PartsAPIService.cs)
	* _IPartsServiceAPI_
	* _PartsAPIService._
* [PhotoAPIService.cs](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/master/Mobile/ContosoFieldService.Core/Services/PhotoAPIService.cs)
	* _IPhotoServiceAPI_
	* _PhotoAPIService_

Each file contains two classes (we know this is bad practice, but keep with us 😏), where you can easily see how we’ve abstracted away our REST calls using a 3rd party package. 

## Refit
Refit is a REST library for .NET developers to easily interact with remote APIs . It make heavy usage of generics and abstractions to minimises the amount of boiler-plate code required to make http requests.

It requires us to define our REST API calls as a C# Interface which is then used with a HTTPClient to handle all the requests"


#### Security 
Because we’re using Azure API Management, we have the ability to restrict access to our APIs through the use of API Keys. With a unique API key, we’re able to confirm that this app is allowed access to our services. We’ll want to ensure we add the API key to all our requests and Refit makes this super easy! We just need to add the _Headers_ attribute to our interface. Here we grab our API key from the constants class. 


 ```cs
[Headers(Helpers.Constants.ApiManagementKey)]`
public interface IJobServiceAPI`
{

}

```


To define a request, we’re again going to use attributes. Take GetJobs for example 

 ```cs

[Get("/job/")]
Task<List<Job>> GetJobs();

```

Adding fields is easy

 ```cs
 
[Get("/job/{id}/")]
Task<Job> GetJobById(string id);

//And we can do lots more.
[Post("/job/")]
Task<Job> CreateJob([Body] Job job);

[Delete("/job/{id}/")]
Task<Job> DeleteJob(string id);

[Put("/job/{id}/")]
Task<Job> UpdateJob(string id, [Body] Job job);

```

#### Using the service Interface
Our service implementation is pretty straight forward. We create a class to handle the service implementation. Well stub out the methods to map closely to our interface. 

 ```cs
 
public async Task<Job> CreateJobAsync(Job job)
{
}

public async Task<List<Job>> GetJobsAsync()
{
}

public async Task<Job> GetJobByIdAsync(string id)
{
}

public async Task<Job> DeleteJobByIdAsync(string id)
{
}

public async Task<Job> UpdateJob(Job job)
{
}

```


#### Implementations 
As I mentioned, we’re implementing a basic service layer so our methods need only be a couple of lines of code. 

**Basic**

```cs
public async Task<Job> CreateJobAsync(Job job)
 {
    var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
    return await contosoMaintenanceApi.CreateJob(job);
}

//Get by ID
public async Task<Job> GetJobByIdAsync(string id)
{
    var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);
    return await contosoMaintenanceApi.GetJobById(id);
}
```

**Resilient**

To build a service layer that is resilient to network outages or poor connectivity, we would want to grab a few extra packages. The first being the Xamarin Connectivity Plugin. This allows us to query what our network connectivity looks like before we decide how to process a request for data. We may want to return a cached copy if its still valid and we’ve poor connectivity. Alternatively we may want to do a remote fetch and save the response for next time.  To help combat against poor connectivity, we also use Polly to handle timeouts and retry logic. You can see in the example below, we will try 5 times before giving up. 

```cs

public async Task<List<Job>> GetJobsAsync()
{
    var key = "Jobs";

    Handle online/offline scenario
    if (!CrossConnectivity.Current.IsConnected && Barrel.Current.Exists(key))
    {
        //If no connectivity, we'll return the cached jobs list.
        return Barrel.Current.Get<List<Job>>(key);
    }

    //If the data isn't too old, we'll go ahead and return it rather than call the backend again.
    if (!Barrel.Current.IsExpired(key) && Barrel.Current.Exists(key))
    {
        return Barrel.Current.Get<List<Job>>(key);
    }            

    //Create an instance of the Refit RestService for the job interface.
    var contosoMaintenanceApi = RestService.For<IJobServiceAPI>(Helpers.Constants.BaseUrl);

    //Use Polly to handle retrying (helps with bad connectivity) 
    var jobs = await Policy
        .Handle<WebException>()
        .Or<HttpRequestException>()
        .Or<TimeoutException>()
        .WaitAndRetryAsync
        (
            retryCount: 5,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
        ).ExecuteAsync(async () => await contosoMaintenanceApi.GetJobs());


    //Save jobs into the cache
    Barrel.Current.Add(key: key, data: jobs, expireIn: TimeSpan.FromSeconds(5));

    return jobs;
}
```

---
# Next Steps 
[App Center](../12%20Anayltics/README.md)




