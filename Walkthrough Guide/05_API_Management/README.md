![Banner](Assets/Banner.png)

# API Management
Azure API Management is a turnkey solution for publishing APIs to external and internal consumers. It facilitates the quick creation of consistent and modern API gateways for existing backend services hosted anywhere, enabling security and protection of same APIs from abuse and overuse, and empowers organizations gain insights into API usage and health. Plus, automate and scale developer Onboarding to help get your API program up and running in no time.

## Key Features

### Security 
Azure API Management has a number of security features for ensuring access to back end services happen in a secure manner. 

* **Client profiling and Authorization:** consumers of backend API’s via API Management are created as users on the Developer portal and assigned to groups. These groups are granted access to Products accordingly. Products contain a set of 1 or more API definitions

* **Subscription keys:** consumers of backend API’s are assigned primary and secondary subscription keys. This key must be included in all requests made to the API. If the request isn't provided, we will return a 401 Invalid Subscription response. 

* **VPN’s and ExpressRoute:** with the Premium tier, virtual networks can be setup in Azure using a VPN (or ExpressRoute). This virtual network is used to bridge from  your internal network into Azure allowing API Management to invoke the back end API’s without their needing to be exposed publicly. 

### Authentication
* **OAuth Configuration:** configuration of OAuth 2.0 Authorization servers in APIM by specifying supported flows and configuration of endpoints. These can be subsequently associated with configured APIs. APIM can also be configured to check that requests have a valid JWT to prevent unauthorized requests from even reaching the your back-end APIs. This is configured through the use of policies

* **Mutual certificate authentication:** Client certificates can be configured on the back end services and uploaded to APIM. All communication would require authentication via these certificates

### Management
* **Developer Portal:** A self-service developer portal offers access to:
* **Subscription Portal:** this is an administrative interface where API programs are setup and are used to:
* Define or import API schemas
    * Package API’s into products
    * Setup policies like quotas or transformations on APIs
    * Get insight from Analytics
    * Manage users