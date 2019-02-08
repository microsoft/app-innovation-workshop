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

## Step 3 (optional)

To use Azure Kubernetes Service (AKS), you need to create a Service Principal first. For this, run the following commands.

> **Hint:** You only need that, if you want to provision AKS with Terraform. You can skip this, by simple commenting out all lines in the `aks.tf` file.

```bash
az ad sp create-for-rbac --name {choose_a_name}
```

The output should look similar to the JSON below. Save the `appId` and `password` at a secure place, you will need them later.

```bash
{
  "appId": "12345678-1234-1234-1234-123456789012",   // <- Save this
  "displayName": "sp-demoaks",
  "name": "http://sp-demoaks",
  "password": "11111111-1111-1111-1111-11111111111", // <- Save this
  "tenant": "12345678-1234-1234-1234-123456789012"
}
```

Next, we need to assign *Contributor* roles to the Service Principal.

```bash
az role assignment create --assignee {your_sp_appId} --role Contributor
```

## Step 4

Now run Terraform so that it provisions the environment. Make sure to replace the `{your_unique_prefix}` variable with a personal prefix to ensure that your resource names are unique.

```bash
terraform init -backend=false
```

```bash
terraform apply \
  -var prefix={your_unique_prefix} \
  -var sp_client_id={your_sp_appId} \
  -var sp_client_secret={your_sp_password}
```

> **Hint:** Terraform saves the state to your local machine by default. This is okay for testing but might cause problems when working in teams. Take a loot at the `Terraform/foundations.tf` file to see which lines to change, if you want to save state in Azure instead. Afterwards, run `terraform init` again without the `-backend=false` option. Also, take a look at the [official documentation](https://docs.microsoft.com/en-us/azure/terraform/terraform-backend).

## Step 5

You will need to run the following steps from the main walkthrough to build and deploy the application.

- Deploy the Web API to the App Service
- Deploy the Azure Function to the Functions App
- Trigger the `/api/dummy` endpoint to generate data in Cosmos DB
