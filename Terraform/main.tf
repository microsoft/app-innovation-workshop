provider "azurerm" {
  version = "~> 1.15"
}

# To use Azure Storage as remote backend to save state, follow the 
# official guide, to setup the Storage Account: 
# https://docs.microsoft.com/en-us/azure/terraform/terraform-backend
terraform {
  backend "local" {}
}

resource "azurerm_resource_group" "workshop" {
  name     = "${var.prefix}${var.resource_name}"
  location = "westeurope"
}
