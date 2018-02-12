# App Service

Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP and Python.

As App Service is fully managed, we only need to worry about setting the maximum number of instances on which we want to run our backend app on. Microsoft will then manage the scaling and load balancing accross multiple instances to ensure your app preform well under heavy load. Microsoft manages the underlying compute infrastructurer required to run our code, as well as patching and updating the OS and Frameworks when required. 

Before we can deploy an App Service instance, we need to create a resource group to hold todays services. 

## 1. Create a Resource Group

Resource groups can be thought of as logical containers for your Azure Resources. You may wish to create separate resource groups per location, or alternatively, you may wish to have one resource group per project.

![Create new Resource Group](Assets/CreateResourceGroup.png)

In this workshop, we’ll be deploying just one resource group to manage all of our required services. 

Resource groups are great for grouping all the resources associated with a mobile application together. During development, it means you can delete all the resources in one operation. For production, it means you can see how much the service is costing you and how the resources are being used.

Navigate to the [portal.azure.com](portal.azure.com) and sign in with your MSDN credentials.

1. Click 'Resource Groups' in the top-left corner.
2. Click 'Add' to bring up configuration pane. 
3. Supply configuration data. Keep in mind its difficult to change resource group names later. 
4. Click 'Create' and relax. 

![Create new Resource Group](Assets/EmptyResourceGroup.png)

### 1.1 Configure & Create new resources


Select <kbd>Web App</kbd> from the list of results. You’ll want to ensure that the category is ‘Web + Mobile’.



![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/RSG1.png?raw=true)

Click on the <kbd>Create</kbd> button.


### 1.3 Configure new App Service 

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS5.png?raw=true)

####   Basic configuration
* **App Name** - This must be unique 
* **Subscription** - Select your Azure subscription or 'Pay as you go'.
* **Resource Group** - We'll want to create a new resource group. Its name isn't important. 
* **OS** - Select Windows. 
* **App Service Plan** - Create a new App Service Plan if non exist.  
* **App Insights** - You should set this to 'No'. 

Click <kbd>Create</kbd>


### 1.4 Validate Successful Deployment

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS6.png?raw=true)

Once the App Service has been deployed, you'll recieve a notification within the portal with an option to <kbd>Navigate to new resource</kbd>. If you click this, you'll see the App Service overview panel. Here you'll find the URL. 
 

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS7.png?raw=true)

Navigate in your browser to the URL provided in the Overview panel above. If the deployment was successful, you should see something similair to the above screenshot.  

## 2. Configuring App Service






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
