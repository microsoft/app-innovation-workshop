![Banner](Assets/TipsTricks.png)
# Tips & Tricks
You've now deployed your first App Service instance! We'll now review some 'Pro tips' to help you get the most out of your Azure service. 

## Controlling Density 
Most users will have a low number (usually less than 10) applications per App Service Plan. In scenarios where you expect you'll be running many more applications, it's crucial to prevent over-saturating the underlying compute capacity. 

Let's imagine that we've deployed one instance of our admin web portal and two instances of our mobile web API to the same App Service Plan. By default, all apps contained in a given App Service Plan will run on all the available compute resources (servers) allocated. If we only have a single server in our App Service Plan, we'll find that this single server will run all our available apps. Alternatively, if we scale out the App Service Plan to run on two servers, we'll run all our applications (3 apps) on both sets of servers. 

This approach is absolutely fine if you find that your apps are using approximately the same amount of compute resources. If this isn't the case, then you may find that one app is consuming the lions share of compute resources, thus degrading the entire system performance. In our case, the mobile API will likely drive significant consumption of server resources, so we need to mitigate its effects on the performance of the admin portal. 

To do this, what we can do is move lower-volume applications (such as the portal) into a single App Service Plan running on a single compute resource. 

Place high demand apps into an App Service Plan which is configured to auto-scale based on CPU and memory utilisation. 

## Per-App Scaling 
Another alternative for running large numbers of applications more efficiently is to use the per-app scaling feature of Azure App Service. We've [documententation](https://msdn.microsoft.com/en-us/magazine/mt793270.aspx) that covers per-app scaling in detail. Per-App scaling lets you control the maximum number of servers allocated to a given application, and you can do so per application. In this case, an application will run on the defined maximum number of servers and not on all available servers.

## Application Slots
App Service has a feature called [deployment slots](https://docs.microsoft.com/en-gb/azure/app-service/web-sites-staged-publishing). In a nutshell, a deployment slot enables you to have another application (slot) other than your production app. It’s another application that you can use to test new code before swapping into production.

Application slots are among the most used feature in App Service. However, it’s important to understand that each application slot is also an application in its own right. This means application slots can have custom domains associated with them, different SSL certificates, different application settings and so on. It also means the assignment of an application slot to an App Service Plan can be managed separately from the App Service Plan associated with the main production slot.

By default, each application slot is created in the same App Service Plan as the production slot. For low-volume applications, and/or applications with low CPU/memory utilization, this approach is fine.

However, because all applications in an App Service Plan run on the same servers, this means by default all of an Application’s Slots are running on the same underlying server as production. This can lead to problems such as CPU or memory constraints if you decide to run stress tests against non-production slots, which run on the same server as your production application slot.

If resource competition is scoped just to scenarios such as running load tests, then temporarily moving a slot to a different App Service Plan, with its own set of servers, will do the following:

* Create additional App Service Plan for the non-production slots. Important note: Each App Service Plan needs to be in the same resource group and the same region as the production slot’s App Service Plan.
* Move a non-production slot to a different App Service Plan and, thus, a separate pool of compute resources.
* Carry out resource-intensive (or risky) tasks while running in the separate App Service Plan. For example, load tests can be run against a non-production slot without negatively impacting the production slot because there won’t be any resource contention.
* When the non-production slot is ready to be swapped into production, move it back to the same App Service Plan running the production slot. Then the slot swap operation can be carried out.

## Deploying to Production with no downtime 
You have a successful application running on an App Service Plan, and you have a great team to make updates to your application on a daily basis. In this case, you don’t want to deploy bits directly into production. You want to control the deployment and minimize downtime. For that, you can use your application slots. Set your deployment to the “pre-production” slot, which can be configured with production setting, and deploy your latest code. You can now safely test your app. Once you’re satisfied, you can swap the new bits into production. The swap operation doesn’t restart your application, and in return, the Controller notifies the front-end load balancer to redirect traffic to the latest slots.

Some applications need to warm up before they can safely handle production load—for example, if your application needs to load data into cache, or for a .NET application to allow the .NET runtime to JIT your assemblies. In this case, you’ll also want to use application slots to warm up your application before swapping it into production.

We often see customers having a pre-production slot that’s used to both test and warm up the application. You can use Continuous Deployment tools such as Visual Studio Release Manager to set up a pipeline for your code to get deployed into pre-production slots, run test for verification and warm all required paths in your app before swapping it into production.

## Public Virtual IPs
By default, there’s a single public VIP for all inbound HTTP traffic. Any app is addressable to a single VIP. If you have an app on App Service, try running nslookup command and see the result. Here’s an example:

```
» nslookup myawesomestartupapi.azurewebsites.net 
Server:        2001:4898::1050:1050
Address:    2001:4898::1050:1050#53

Non-authoritative answer:
myawesomestartupapi.azurewebsites.net    canonical name = waws-prod-ln1-013.vip.azurewebsites.windows.net.
waws-prod-ln1-013.vip.azurewebsites.windows.net    canonical name = waws-prod-ln1-013.cloudapp.net.
Name:    waws-prod-ln1-013.cloudapp.net
Address: 51.140.59.233
```

You’ll notice that an App Service scale unit is deployed on Azure Cloud Service (by the cloudapp.net suffix). WAWS stands for Windows Azure (when Azure was still called Windows) Web sites (the original name of App Service).

## Outbound Virtual IPs
Most likely your application is connected to other Azure and non-Azure services. As such, your application makes outbound network calls to endpoints, not on the scale unit of your application. This includes calling out to Azure services such as SQL Database and Azure Storage. There are up to five VIPs (the one public VIP and four outbound dedicated VIPs) used for outbound communication. You can’t choose which VIP your app uses, and all outbound calls from all apps in scale unit are using the five allocated VIPs. If your application uses a service that requires you to whitelist IPs that are allowed to make API calls into such a service, you’ll need to register all five VIPs of the scale unit. To view which IPs are allocated to outbound VIPs for a given unit of scale (or for your app from your perspective) go to the Azure portal, as shown in the below image. 

 ![Create new App Service Plan](Assets/OutboundVIP.png)

If you require a dedicated set of inbound and outbound IPs, you should explore using a fully isolated and dedicated App Service Environment. 

## IP And SSL
App Service supports IP-based SSL certificates. When using IP-SSL, App Service allocates to your application a dedicated IP address for only in-bound HTTP traffic.

Unlike the rest of Azure dedicated IP addresses, the IP address with App Service via IP-SSL is allocated as long as you opt to use it. You don’t own the IP address, and when you delete your IP-SSL, you might lose the IP address (as it might be allocated to a different application).

App Service also supports SNI SSL, which doesn’t require a dedicated IP address and is supported by modern browsers. 

## Network Port Capacity for Outbound Network Calls
A common requirement for applications is the ability to make outbound network calls to other network endpoints. This includes calling out to Azure internal services such as SQL Database and Azure Storage. It also includes cases where applications make calls to HTTP/HTTPS API endpoints—for example, calling a Bing Search API or calling an API “application” that implements back-end business logic for a Web application.

In almost all of these cases, the calling app running on Azure App Service is implicitly opening a network socket and making outbound calls to endpoints that are considered “remote” from an Azure Networking perspective. This is an important point because calls made from an app running on Azure App Service to a remote endpoint rely on Azure Networking to set up and manage a table of Network Address Translation (NAT) mappings.

Creating new entries in this NAT mapping takes time, and there’s ultimately a finite limit on the total number of NAT mappings that can be established for a single Azure App Service scale unit. Because of this, App Service enforces limits on the number of outbound connections that can be outstanding at any given point in time.

The maximum connection limits are the following:

* 1,920 connections per B1/S1/P1 instance
* 3,968 connections per B2/S2/P2 instance
* 8,064 connections per B3/S3/P3 instance
* 64K max upper limit per App Service Environment

Applications that “leak” connections invariably run into these connection limits. Applications will start intermittently failing because calls to remote endpoints fail, with the failures sometimes correlating closely to periods of higher application load. You’ll frequently see errors like the following: “An attempt was made to access a socket in a way forbidden by its access permissions aaa.bbb.ccc.ddd.”

The likelihood of running into this problem can be substantially mitigated with a few best practices:

* For .NET applications using ADO.NET/EF, use database connection pooling.
* For .NET applications making outbound HTTP/HTTPS calls, pool and reuse instances of System.Net.Http.HttpClient or use Keep-alive connections with System.Net.HttpWebRequest. 

- Tips and Tricks Source: [MSDN](https://msdn.microsoft.com/en-us/magazine/mt793270.aspx)

---
### Further Reading
* [Offical Documentation](https://docs.microsoft.com/en-us/azure/app-service/)
* [Adding API documentation with Swagger](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger)
