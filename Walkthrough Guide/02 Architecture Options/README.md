![Banner](Assets/Banner.png)

# 1. Architecture Options 
Deciding how to architect a solution isn't an easy task and depending on who you ask, you'll likely get very different answers. There are many different ways we could design such a system, and we'll cover a few of them below. 

We're looking for a solution that allows us lots of flexibility with minimal maintenance.  We're interested in focusing on the business problem rather than deploying and maintaining a set of virtual machines. 

It's for the reason that we'll opt to use Platform as a Service (PaaS) as much as possible within our design. 
 


## The real architecture
![Azure Functions Architecture](Assets/WebAPI.png)
Above you can see a high-level overview of our production architecture. Some key decisions: 

### Orchestration 
We were going to leverage our .NET skills and build a ASP.NET Web API targeting .NET Core; we've lots of flexibility on where and how to host the code. 

We picked Azure App Service as it supports great IDE integration for both Visual Studio PC and Visual Studio Mac, as well as offering all the PaaS goodness we need to focus on other parts of the solution.  

### Security
As we're not going to implement Authentication in today's workshop, we decided to add API Management to add a security layer through the use of API keys. 

### Data Storage 
We've opted for a NoSQL approach using CosmosDB. Our reasoning for this is based on a few reasons. An important part is the geo-replication features of CosmosDB make it a natural choice for new projects, but secondly, our deadline meant we didn't have time to spend migrating database for small model changes (something we did a lot in the beginning). 

---

## Azure Functions
![Azure Functions Architecture](Assets/Functions.png)

We can swap out the orchestration service from App Service to Azure Functions if we're looking to cut costs and move to a 'serverless' architecture. 

The truth is, there is a server. Azure Functions runs in the same environment as App Services, but the way we as developers interact with the service is a little different. 

The most significant difference is how we scale. With Azure Functions, we do not ever have to worry about scaling our services to meet demand. Azure Functions runs on what we call "dynamic compute", in that Microsoft will scale up, down, out and in instances of your code to meet demand. 

We will be developing a version of the backend that is entirely Azure Functions based on the future. 

---
## Micro-Services
![Azure Functions Architecture](Assets/MicroServices.png)

Mr Michael Sivers is currently investigating adding a Micro-Services architecture as a branch. 

If you're interested in helping, then please reach out to us! 

Learn more about [Service Fabric](https://azure.microsoft.com/en-us/services/service-fabric/)

## Connecting to remote resources securely
ExpressRoute is an Azure service that lets you create private connections between Microsoft datacenters and infrastructure that’s on your premises or in a colocation facility. ExpressRoute connections do not go over the public Internet, instead ExpressRoute uses dedicated connectivity from your resources to Azure. This provides reliability and speeds guarantees with lower latencies than typical connections over the Internet. Microsoft Azure ExpressRoute lets you extend your on-premises networks into the Microsoft cloud over a private connection facilitated by a connectivity provider. Connectivity can be from an any-to-any (IP VPN) network, a point-to-point Ethernet network, or a virtual cross-connection through a connectivity provider at a co-location facility.

Microsoft uses industry standard dynamic routing protocol (BGP) to exchange routes between your on-premises network, your instances in Azure, and Microsoft public addresses. We establish multiple BGP sessions with your network for different traffic profiles. The advantage of ExpressRoute connections over S2S VPN or accessing Microsoft cloud services over internet are as follows;

* more reliability
* faster speeds
* lower latencies
* higher security than typical connections over the Internet
* extend ExpressRoute connectivity across geopolitical boundaries (using premium add-on)

Bandwidth options available in ExpressRoute are 50 Mbps, 100 Mbps, 200 Mbps, 500 Mbps, 1 Gbps, 2 Gbps, 5 Gbps and 10 Gbps.

![Express Route Connectivity Model](Assets/ERConnectivityModel.png)

There are three ways to connect customer’s on-premise infrastructure to Azure (or microsoft cloud services) using ExpressRoute, they are; 
 
1. WAN integration (or call IPVPN or MPLS or any-to-any connectivity) 
2. Cloud Exchange through Co-Location Provider 
3. Point-to-Point Ethernet Connection 

A Site-to-Site VPN gateway connection is used to connect your on-premises network to an Azure virtual network over an IPsec/IKE (IKEv1 or IKEv2) VPN tunnel. This type of connection requires a VPN device located on-premises that has an externally facing public IP address assigned to it.
![Site to Site Connectivity Model](Assets/SiteToSiteConnectivityModel.png)
 
# Next Steps 
[Web API](../03%20Web%20API/README.md)
