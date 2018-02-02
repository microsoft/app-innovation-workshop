# App Service
Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP and Python. 

Using App Service, we have host our apps backend APIs in an environment which allows for easy configuration of load balancing, backups, SSO and more. 

Before we can deploy an App Service instance, we need to create a resource 

## 1. Resource Groups
Resource groups can be thought of as logical containers for your Azure Resources. You may wish to create separate resource groups per location, or alternatively, you may wish to have one resource group per project.

In this workshop, we’ll be deploying just one resource group to manage all of our required services. We can create a resource group at the same time we deploy our first Ap Service. 

### 1.1 Create a new resource group 
Navigate to the [portal.azure.com](portal.azure.com)and sign in with your MSDN credentials. 

![](README/4C5B3FE7-BF9F-41C7-82E8-520E4AA88A93.png)
Click on the ‘Create a Resource’ button. 
<kbd>Create a Resource</kbd>

![](README/76F9B56A-D1D9-45F1-A9CE-83F517A4024D.png)
Search for ‘Web App’. 

![](README/BA11E150-8799-49D0-9283-0C9525FE4522.png)
Select <kbd>Web App</kbd> from the list of results. You’ll want to ensure that the category is ‘Web + Mobile’. 

![](README/0CD81D08-7634-468E-AE77-884A9C5BE762.png)
Click on the <kbd>Create</kbd> button. 

![](README/FD333D70-5F22-439B-A0F4-B5D86C5B5EDF.png)


![](README/2982C364-693D-454C-8055-DD012DFFE078.png)

![](README/F1B767C2-CE9B-467A-B87A-FD1F65AC78E0.png)


