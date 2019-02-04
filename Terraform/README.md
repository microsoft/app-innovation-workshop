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
az account set --subscription {subscription id}
```

## Step 3 (optional)

To store the Terraform state in the cloud and not locally, you should create an Azure Storage Account. You can do this easily, by running the following commands.

```bash
RESOURCE_GROUP_NAME=terraform
STORAGE_ACCOUNT_NAME=terraformstate$RANDOM
CONTAINER_NAME=tfstate

# Create resource group
az group create --name $RESOURCE_GROUP_NAME --location eastus

# Create storage account
az storage account create --resource-group $RESOURCE_GROUP_NAME --name $STORAGE_ACCOUNT_NAME --sku Standard_LRS --encryption-services blob

# Get storage account key
ACCOUNT_KEY=$(az storage account keys list --resource-group $RESOURCE_GROUP_NAME --account-name $STORAGE_ACCOUNT_NAME --query [0].value -o tsv)

# Create blob container
az storage container create --name $CONTAINER_NAME --account-name $STORAGE_ACCOUNT_NAME --account-key $ACCOUNT_KEY

echo "storage_account_name: $STORAGE_ACCOUNT_NAME"
echo "container_name: $CONTAINER_NAME"
echo "access_key: $ACCOUNT_KEY"
```

Once this is done, navigate to the `Terraform/foundations.tf` file and replace the `storage_account_name` and `access_key` placeholders with the one from your console output.

```
terraform {
  backend "azurerm" {
    storage_account_name = "__TFSTATE-STORAGE-ACCOUNT-NAME__" # <-- Here
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
    access_key           = "__TFSTATE-STORAGE-ACCESS-KEY__"   # <-- Here
  }
}
```

If you want to use the local state, just comment the above section out.

## Step 4

Now run Terraform so that it provisions the environment. Make sure to set a prefix to ensure that your resource names are unique.

```bash
terraform init

terraform apply -var 'prefix=your_unique_prefix'
```

## Step 4

You will need to run the following steps from the main walkthrough to build and deploy the application.

- Deploy the Web API to the App Service
- Deploy the Azure Function to the Functions App
- Trigger the `/api/dummy` endpoint to generate data in Cosmos DB