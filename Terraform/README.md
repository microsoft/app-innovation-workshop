# Initial environment provisioning

## Prerequisites

Ensure the following are installed

- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest)
- [Git](https://git-scm.com/downloads)
- [Terraform](https://www.terraform.io/intro/getting-started/install.html)

## Step 1

Open a console or terminal and navigate to the `Terraform` directory and login to the Azure CLI

```bash
az login
```

Navigate to [https://aka.ms/devicelogin](https://aka.ms/devicelogin) and use the code output from the above command and login using an account associated to an Azure subscription.

## Step 2 (optional)

If your account is associated to more than one tenant or subscription then you can switch to the specific one you want to work with now using

```bash
az account set --subscription {your_subscription_id}
```

## Step 3

Now run Terraform so that it provisions the environment. Make sure to set a prefix to ensure that your resource names are unique.

```bash
terraform init

terraform apply -var prefix={your_unique_prefix}
```

> **Hint:** Terraform saves the state to your local machine by default. This is okay for testing but might cause problems when working in teams. Take a loot at the `Terraform/foundations.tf` file to see which lines to change, if you want to save state in Azure instead or take a look at the [official documentation](https://docs.microsoft.com/en-us/azure/terraform/terraform-backend).

## Step 4

You will need to run the following steps from the main walkthrough to build and deploy the application.

- Deploy the Web API to the App Service
- Deploy the Azure Function to the Functions App
- Trigger the `/api/dummy` endpoint to generate data in Cosmos DB
