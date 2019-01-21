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

## Step 2 (Optional)

If your account is associated to more than one tenant or subscription then you can switch to the specific one you want to work with now using

```bash
az account set --subscription {subscription id}
```

## Step 3

Now run Terraform so that it provisions the environment

```bash
terraform init

terraform apply
```

## Step 4

You will need to run the following steps from the main walkthrough to build and deploy the application.

- Deploy the Web API to the App Service
- Deploy the Azure Function to the Functions App
- Trigger the `/api/dummy` endpoint to generate data in Cosmos DB