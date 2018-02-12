# App Service

Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP and Python.

As App Service is fully managed, we only need to worry about setting the maximum number of instances on which we want to run our backend app on. Microsoft will then manage the scaling and load balancing accross multiple instances to ensure your app preform well under heavy load. Microsoft manages the underlying compute infrastructurer required to run our code, as well as patching and updating the OS and Frameworks when required. 

Before we can deploy an App Service instance, we need to create a resource

## 1. Create a Resource Group

Resource groups can be thought of as logical containers for your Azure Resources. You may wish to create separate resource groups per location, or alternatively, you may wish to have one resource group per project.

  

In this workshop, we’ll be deploying just one resource group to manage all of our required services. We can create a resource group at the same time we deploy our first Ap Service.

  

### 1.1 Navigate to the Azure Portal

Navigate to the [portal.azure.com](portal.azure.com) and sign in with your MSDN credentials.
 
![Azure Portal](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS1.png?raw=true)


### 1.2 Configure & Create Resource Group


![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/RSG1.png?raw=true)

1. Click 'Resource Groups'.
2. Click 'Add' to bring up configuration pane. 
3. Supply configuration data. Keep in mind its difficult to change resource group names later. 
4. Click 'Create' and relax. 


![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS3.png?raw=true)

Select <kbd>Web App</kbd> from the list of results. You’ll want to ensure that the category is ‘Web + Mobile’.



![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/RSG1.png?raw=true)

Click on the <kbd>Create</kbd> button.


### 1.3 Configure new App Service & Resource Group

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




