# Initial environment provisioning

## preqs

Ensure the following are installed

- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
- [Git](https://git-scm.com/downloads)
- [Terraform](https://www.terraform.io/intro/getting-started/install.html)

## Step 1

Clone the GitHub repository to your local machine

```
git clone https://github.com/MikeCodesDotNET/Mobile-Cloud-Workshop.git
```

## Step 2

Open a console or terminal and navigate to the Terraform directory and login to the Azure Cli

```
az login
```

Navigate to [https://aka.ms/devicelogin](https://aka.ms/devicelogin) and use the code output from the above command and login using an account associated to an Azure subscription.

## Step 3 (Optional)

If your account is associated to more than one tenant or subscription then you can switch to the specific one you want to work with now using 

```
az account set --subscription {subscription id}
```

## Step 4

Now run Terraform so that it provisions the environment

```
terraform init

terraform apply
```

## Step 5

You will need to run the following steps from the main walkthrough to build and deploy the application.

[Deploy App Service](https://github.com/MikeCodesDotNET/Mobile-Cloud-Workshop/tree/master/Walkthrough%20Guide/03%20Web%20API#3-deploy-your-apps-to-app-service)
[Index data in Cosmos](https://github.com/MikeCodesDotNET/Mobile-Cloud-Workshop/tree/master/Walkthrough%20Guide/05%20Search#indexing-our-data)
[Deploy Function](https://github.com/MikeCodesDotNET/Mobile-Cloud-Workshop/tree/master/Walkthrough%20Guide/06%20Functions%20and%20Cognitive%20Services#26-deploy-to-azure)