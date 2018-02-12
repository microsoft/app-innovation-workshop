![Banner](Assets/Banner.png)

# 1. Alternaitve Architecture Options 
There are lots of ways for us to design the Azure backend service but for this workshop, we approached this project as a "greenfield / Bluesky" development. 

You may not have that luxury in your projects so we've created a few alternative architecture diagrams that may be more suitable for you. 

## App Service Web API
![Azure Functions Architecture](Assets/WebAPI.png)

---
## Azure Functions
![Azure Functions Architecture](Assets/Functions.png)

Take advantage of serverless compute with Functions.
Easily build the apps you need using simple, serverless functions that scale to meet demand. Use the programming language of your choice,and don’t worry about servers or infrastructure.

Focus on building great apps. Don’t worry about provisioning and maintaining servers, especially when your workload grows. Azure Functions provides a fully managed compute platform with high reliability and security. With scale on demand, you get the resources you need—when you need them.


---
## Micro-Services
![Azure Functions Architecture](Assets/MicroServices.png)

Focus on building applications and business logic, and let Azure solve the hard distributed systems problems such as reliability, scalability, management, and latency. Service Fabric is the foundational technology powering core Azure infrastructure as well as other Microsoft services such as Skype for Business, Intune, Azure Event Hubs, Azure Data Factory, Azure Cosmos DB, Azure SQL Database, Dynamics 365, and Cortana. Designed to deliver highly available and durable services at cloud-scale, Azure Service Fabric intrinsically understands the available infrastructure and resource needs of applications, enabling automatic scale, rolling upgrades, and self-healing from faults when they occur.

Choose from a variety of productive programming models and languages including C# and Java to build your microservice and container-based applications.

Learn more about [Service Fabric](https://azure.microsoft.com/en-us/services/service-fabric/)

---
## Traffic Manager

//TODO - Create Traffic Architecture Diagram which replaces API Management






