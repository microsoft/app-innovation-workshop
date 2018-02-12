# App Service

Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP and Python.

As App Service is fully managed, we only need to worry about setting the maximum number of instances on which we want to run our backend app on. Microsoft will then manage the scaling and load balancing accross multiple instances to ensure your app preform well under heavy load. Microsoft manages the underlying compute infrastructurer required to run our code, as well as patching and updating the OS and Frameworks when required. 

Before we can deploy an App Service instance, we need to create a resource group to hold todays services. 

### 1.1 Resource Group

Resource groups can be thought of as logical containers for your Azure Resources. You may wish to create separate resource groups per location, or alternatively, you may wish to have one resource group per project.

In this workshop, we’ll be deploying just one resource group to manage all of our required services. 

Resource groups are great for grouping all the resources associated with a mobile application together. During development, it means you can delete all the resources in one operation. For production, it means you can see how much the service is costing you and how the resources are being used.

#### 1.1.1 Create Resource Group
![Create new Resource Group](Assets/CreateResourceGroup.png)

Navigate to the [portal.azure.com](portal.azure.com) and sign in with your MSDN credentials.

1. Click 'Resource Groups' in the top-left corner.
2. Click 'Add' to bring up configuration pane. 
3. Supply configuration data. Keep in mind its difficult to change resource group names later. 
4. Click 'Create' and relax. 

Navigate to the newly created Resource Group.

![Create new Resource Group](Assets/EmptyResourceGroup.png)

### 1.2 App Service Plan

#### 1.2.1 Overview 
In App Service, an app runs in an App Service plan. An App Service plan defines a set of compute resources for a web app to run. These compute resources are analogous to the server farm in conventional web hosting. One or more apps can be configured to run on the same computing resources (or in the same App Service plan).

When you create an App Service plan in a certain region (for example, West Europe), a set of compute resources is created for that plan in that region. Whatever apps you put into this App Service plan run on these compute resources as defined by your App Service plan. Each App Service plan defines:

* **Region** (West US, East US, etc.)
* **Number of VM instances**
* **Size of VM instances** (Small, Medium, Large)
* **Pricing tier** (Free, Shared, Basic, Standard, Premium, PremiumV2, Isolated, Consumption)

#### 1.2.2 Create App Service Plan

From within your new Resource Group, do the following: 
* Click "Add" in the top bar. 
* Search for "App Sercvice Plan"

 ![Search for App Service Plan](Assets/AddNewAppServicePlan.png)

 ![Create new App Service Plan](Assets/CreateNewAppServicePlan.png)

 The process for creating an App Service Plan is straight forward but you have a couple of decisions to make. The first decision is where is the service going to run. In a production environment, the correct answer would be "near your end user". In development, we'd want our app running "Close to the developers". You'll have lots of options for where to deploy the plan, so give some thought about where most requests will be coming from and pick a location that's as close as possible. 

 ![Create new App Service Plan](Assets/ConfigureAppServicePlan.png)

 The second decision you'll have to make is what to run the service on; also known as the Pricing tier. If you Click View all, you will see you have lots of choices. F1 Free and D1 Shared, for example, run on shared resources and are CPU limited. You should avoid these as the service will stop responding when you are over the CPU quota. That leaves Basic, Standard and Premium. Basic has no automatic scaling and can run up to 3 instances - perfect for development tasks. Standard and Premium both have automatic scaling, automatic backups, and large amounts of storage; they differ in features: the number of sites or instances you can run on them, for example. Finally, there is a number after the plan. This tells you how big the virtual machine is that the plan is running on. The numbers differ by number of cores and memory.

For our purposes, an B1 Basic site is enough to run this  workshop project. More complex development projects should use something in the Standard range of pricing plans. Production apps should be set up in Standard or Premium pricing plans.

Once you have created your app service plan and saved it, Click "Create".

The creation of the service can take a couple of minutes. You can monitor the process of deployment by clicking on the Notifications icon. This is in the top bar on the right-hand side and looks like a Bell. Clicking on a specific notification will provide more information about the activity. 

 ![Create new App Service Plan](Assets/CreateNewAppServicePlan.png)
 
### 1.3 Adding an App to our App Service
Right now the App Service Plan doesn't contain any Apps. We will want at least one app for our ASP.NET Core 2.0 Web API service. To create this, lets navigate back to the Resource Group and clic "Add" again. This time, we'll be searching for a "Web API". 

 ![Create new App Service Plan](Assets/WebAPISearchResults.png)

* Select 'Web App' from the list and click Create. 

 ![Create new App Service Plan](Assets/NewWebAppConfiguration.png)

We'll need to provide a unique app name, which will become part of the URL we use to navigate to the service. We should also select our subscription service and most importantly, we'll want to run this app in the App Service Plan we just deployed. 

Given that we're running our app in Platform as a Service, we don't really need to worry too much about the underlying operating system. With that said, I highly recommend picking Windows as we've thoroughly tested this workshop with that configuration. 

With all the configuration options set, hit "Create" and hold tight. Once the deployment has finished, we should be able to navigate to our app through the browser and see a generic Azure landing page. 

Because my app name was: MyAwesomeStartupAPI
The unique URL would be: https://myawesomestartupapi.azurewebsites.net

You should see something similar to the image below: 

 ![Create new App Service Plan](Assets/AppServiceDeployed.png)


# Less than obvious best pratices 
You've now deployed your first App Service instance! We'll now review some 'Pro tips' to help you get the most out of your Azure service. 

## Controlling Density 
Most users will have a low number (usually less than 10) applications per App Service Plan. In scenarios where you expect you'll be running many more applications, its important to prevent over saturating the underlying compute capacity. 

Lets imagine that we've deployed one instance of our admin web portal and two instances of our mobile web API to the same App Service Plan. By default, all apps contained in a given App Service Plan will run on all the available compute resources (servers) allocated. If we only havea single server in our App Service Plan, we'll find that this single server will run all our avaiaible applications. Alternatively, if we scale out the App Service Plan to run on two servers, we'll run all three applications on both servers. 

This approach is absoutly fine if you find that your apps are using approximatly the same amount of compute resources. If this isn't the case then you may find that one app is consuming the lions share of compute resources, thus degrading the entire system performance. In our case, the mobile API will likely drive significant consumption of server resources, so we need to mitigates its effects on the performance of the admin portal. 

To do this, what we can do is move lower-volume applications (such as the portal) into a single App Service Plan running on a single compute resource. 

Place high demand apps into an App Service Plan which is configured to auto-scale based on CPU and memory utilization. 

## Per App Scaling 
Another alternative for running large numbers of applications in a more efficient manner is to use the per-app scaling feature of Azure App Service. We've [documententation](https://msdn.microsoft.com/en-us/magazine/mt793270.aspx) that covers per-app scaling in detail. Per-App scaling lets you control the maximum number of servers allocated to a given application, and you can do so per application. In this case, an application will run on the defined maximum number of servers and not on all available servers.

## Application Slots
App Service has a feature called [deployment slots](https://docs.microsoft.com/en-gb/azure/app-service/web-sites-staged-publishing). In a nutshell, a deployment slot enables you to have another application (slot) other than your production app. It’s another application that you can use to test new code prior to swapping into production.

Application slots is among the most used feature in App Service. However, it’s important to understand that each application slot is also an application in its own right. This means application slots can have custom domains associated with them, different SSL certificates, different application settings and so on. It also means the assignment of an application slot to an App Service Plan can be managed separately from the App Service Plan associated with the main production slot.

By default, each application slot is created in the same App Service Plan as the production slot. For low-volume applications, and/or applications with low CPU/memory utilization, this approach is fine.

However, because all applications in an App Service Plan run on the same servers, this means by default all of an Application’s Slots are running on the exact same underlying server as production. This can lead to problems such as CPU or memory constraints if you decide to run stress tests against non-production slots, which run on the same server as your production application slot.

If resource competition is scoped just to scenarios such as running load tests, then temporarily moving a slot to a different App Service Plan, with its own set of servers, will do the following:

* Create additional App Service Plan for the non-production slots. Important note: Each App Service Plan needs to be in the same resource group and same region as the production slot’s App Service Plan.
* Move a non-production slot to a different App Service Plan and, thus, a separate pool of compute resources.
* Carry out resource-intensive (or risky) tasks while running in the separate App Service Plan. For example, load tests can be run against a non-production slot without negatively impacting the production slot because there won’t be any resource contention.
* When the non-production slot is ready to be swapped into production, move it back to the same App Service Plan running the production slot. Then the slot swap operation can be carried out.

## Deploying to Production with no downtime 
You have a successful application running on an App Service Plan and you have a great team to make updates to your application on a daily basis. In this case, you don’t want to deploy bits directly into production. You want to control the deployment and minimize downtime. For that you can use your application slots. Set your deployment to the “pre-production” slot, which can be configured with production setting, and deploy your latest code. You can now safely test your app. Once you’re satisfied, you can swap the new bits into production. The swap operation doesn’t restart your application and in return the Controller notifies the front-end load balancer to redirect traffic to the latest slots.

Some applications need to warm up before they can safely handle production load—for example, if your application needs to load data into cache, or for a .NET application to allow the .NET runtime to JIT your assemblies. In this case, you’ll also want to use application slots to warm up your application prior to swapping it into production.

We often see customers having a pre-production slot that’s used to both test and warm up the application. You can use Continuous Deployment tools such as Visual Studio Release Manager to set up a pipeline for your code to get deployed into pre-production slots, run test for verification and warm all required paths in your app prior to swapping it into production.
