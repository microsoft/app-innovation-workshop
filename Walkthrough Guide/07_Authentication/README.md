![Banner](Assets/Banner.png)

# Authentication 
Although adding Authentication in workshop can be troublesome (you might not have permissions to be deploying Auth services in your Azure Subscription), its an important enough topic that we've opted to include a guide regardless. We do not intend for you to implement authentication into this mobile app 

## Concepts
One of the very first things you want to do when developing a mobile app is to provide users with a unique experience. For our field service app, this could be as simple as providing a job list for the user who is logged in. In more complex applications, this is the gateway to role-based access controls, group rules, and sharing with your friends. In all these cases, properly identifying the user using the phone is the starting point.

Authentication provides a process by which the user that is using the mobile device can be identified securely. This is generally done by entering a username and password. However, modern systems can also provide multi-factor authentication, send you a text message to a registered device, or use your fingerprint as the password. 

### The OAuth Process
In just about every single mobile application, a process called OAuth is used to properly identify a user to the mobile backend. OAuth is not an authentication mechanism in its own right. It is used to route the authentication request to the right place and to verify that the authentication took place. There are three actors in the OAuth protocol:

* The Client is the application attempting to get access to the resource.
* The Resource is the mobile backend that the client is attempting to access.
* The Identity Provider (or IdP) is the service that is responsible for authenticating the client.

At the end of the process, a cryptographically signed token is minted. This token is added to every request made by the client to the resource to securely identify the user.

### Server Side vs. Client Side
There are two types of authentication flow: Server-flow and Client-flow. They are so named because of who controls the flow of the actual authentication.

![AuthFlow](Assets/AuthFlow.png)

#### Server-flow
Server-flow is named because the authentication flow is managed by the Azure App Service (the server) through a webview-based work flow. Using Azure App Service Mobile App SDKs goes as far as providing just one LOC to initiate authentication from the client, which makes this a fantastic way to add auth to your app for development purposes. 

##### Steps
1. The mobile app brings up an embedded WebView and ask for the logic page from the App Service instance. 
2. The App Service instance redirects the app to the selected identity provider.
3. The identity provider does the authentication before redirecting the client back to the resource (with an identity provider token).
4. The resource validates the identity provider token with the identity provider.
5. Finally, the App Service will mint a new resource token that it returns to the client. 

This new token is a key feature of the Server-flow auth approach. Your mobile app wont have access to the identity providers token. If your building a social app, this may mean that you need to think about client-flow auth in order to ensure you have access to the indentity providers token (for accessing their resources, such as profile images or contact details). 

Server-flow has some serious limitations for production use and for that reason, we do not recommend you deploy with Server-flow authentication enabled. 

One of the more significant limitation with Server-flow is linked to your Indentity redirect url. Many providers will only support one redirect url, which will be your production url. If we then deploy multiple versions of our API to different [Application Slots](https://github.com/MikeCodesDotNet/Mobile-Cloud-Workshop/blob/master/Walkthrough%20Guide/03_Web_API/TipsAndTricks.md#application-slots) then we'll find that our authentication flow will stop working (due to the redirect going to a different instance of our app). 

An example of this might be authenticating with Facebook. Our production App Service is running at: 

https://myamazingapp.azurewebsite.nets

We set the Facebook auth redirect url to be: 

https://myamazingapp.azurewebsite.nets
.auth/login/facebook/callback 

If we then deploy a test version of the backend to a staging slot, the url may be:

https://myamazingapp-staging.azurewebsite.nets

This means the redirect URL would need to be set to https://myamazingapp-staging.azurewebsite.nets.auth/login/facebook/callback

To test authentication in the staging slot.  

As you can see, we now have two 







