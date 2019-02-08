provider "azurerm" {
  version = "~> 1.15"
}

# Use Azure Storage as remote backend to save state.
# Follow the official guide, to setup the Storage Account:
# https://docs.microsoft.com/en-us/azure/terraform/terraform-backend

terraform {
  backend "azurerm" {
    storage_account_name = "__TFSTATE-STORAGE-ACCOUNT-NAME__" # <-- Replace this with your values
    container_name       = "__TFSTATE-CONTAINER-NAME__"       # <-- Replace this with your values
    access_key           = "__TFSTATE-STORAGE-ACCESS-KEY__"   # <-- Replace this with your values
    key                  = "terraform.tfstate"
  }
}

resource "azurerm_resource_group" "workshop" {
  name     = "${var.prefix}${var.resource_name}workshop"
  location = "westeurope"
}
