![Banner](Assets/Banner.png)

# App Service

Azure App Service is Microsoft’s fully managed, highly scalable platform for hosting web, mobile and API apps built using .NET, Java, Ruby, Node.js, PHP, and Python or Docker containers.

App Service is fully managed and allows us to set the maximum number of instances on which we want to run our backend app on. Microsoft will then manage the scaling and load balancing across multiple instances to ensure your app perform well under heavy load. Microsoft manages the underlying compute infrastructure required to run our code, as well as patching and updating the OS and Frameworks when required.

## 1. Resource Group

Before we can deploy an App Service instance, we need to create a resource group to hold today's services. Resource groups can be thought of as logical folders for your Azure Services (Resources). You may wish to create separate resource groups per department, or you may want to have one resource group per project. Resource groups are great for grouping all the services associated with a solution together. During development, it means you can quickly delete all the resources in one operation!

In this workshop, we’ll be deploying just one resource group to manage all of our required services. When in production, it means we can see how much the services are costing us and how the resources are being used.

### 1.1 Create a new Resource Group

![Create a new Resource Group](Assets/CreateResourceGroup.png)

Navigate to the [portal.azure.com](portal.azure.com) and sign in with your credentials.

1. Click ***Resource groups*** in the top-left corner.
2. Click ***Add*** to bring up configuration pane.
3. Supply configuration data. Keep in mind its difficult to change resource group names later.
4. Click ***Create*** and relax.

Navigate to the newly created Resource Group.

![Create new Resource Group](Assets/EmptyResourceGroup.png)

And voilà, we are done. Now we can start to add services to our newly created Resource Group!

## 2. Containers

Container Registry...

<details><summary>stuff with *mark* **down**</summary><p>

## _formatted_ **heading** with [a](link)

---
{{standard 3-backtick code block omitted from here due to escaping issues}}
---

Collapsible until here.
</p></details>

## 3. Create the compute layer with App Services or Container Orchestrators

When it comes to the compute layer of our backend architecture, we have a bunch of options.

### [Use Azure App Service](Walkthrough%20Guide/02%20Web%20API/01%20App%20Service/)

### [Use Azure Kubernetes Service](Walkthrough%20Guide/02%20Web%20API/02%20AKubernetes/)

