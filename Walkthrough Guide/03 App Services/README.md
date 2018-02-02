# App Service
Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP and Python. 

Using App Service, we have host our apps backend APIs in an environment which allows for easy configuration of load balancing, backups, SSO and more. 

Before we can deploy an App Service instance, we need to create a resource 

## 1. Resource Groups
Resource groups can be thought of as logical containers for your Azure Resources. You may wish to create separate resource groups per location, or alternatively, you may wish to have one resource group per project.

In this workshop, we’ll be deploying just one resource group to manage all of our required services. We can create a resource group at the same time we deploy our first Ap Service. 

### 1.1 Create a new resource group 
Navigate to the [portal.azure.com](portal.azure.com)and sign in with your MSDN credentials. 

![Azure Portal](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS1.png?raw=true)
Click on the ‘Create a Resource’ button. 
<kbd>Create a Resource</kbd>

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS2.png?raw=true)
Search for ‘Web App’. 

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS3.png?raw=true)
Select <kbd>Web App</kbd> from the list of results. You’ll want to ensure that the category is ‘Web + Mobile’. 

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS4.png?raw=true)
Click on the <kbd>Create</kbd> button. 

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS5.png?raw=true)


![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS6.png?raw=true)

![](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/walkthrough/Walkthrough%20Guide/Misc/APS7.png?raw=true)


