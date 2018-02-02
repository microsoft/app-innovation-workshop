# App Service

Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP and Python.

  

Using App Service, we have host our apps backend APIs in an environment which allows for easy configuration of load balancing, backups, SSO and more.

  

Before we can deploy an App Service instance, we need to create a resource

  

## 1. Resource Groups

Resource groups can be thought of as logical containers for your Azure Resources. You may wish to create separate resource groups per location, or alternatively, you may wish to have one resource group per project.

  

In this workshop, we’ll be deploying just one resource group to manage all of our required services. We can create a resource group at the same time we deploy our first Ap Service.

  

### 1.1 Navigate to the Azure Portal

Navigate to the [portal.azure.com](portal.azure.com) and sign in with your MSDN credentials.
 
![Azure Portal](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS1.png?raw=true)

Click on the <kbd>Create a Resource</kbd> button.


### 1.2 Search for resource templates


![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS2.png?raw=true)

Search for ‘Web App’.


![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS3.png?raw=true)

Select <kbd>Web App</kbd> from the list of results. You’ll want to ensure that the category is ‘Web + Mobile’.



![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS4.png?raw=true)

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

