# Use Azure Storage as remote backend to save state.
# Follow the official guide, to setup the Storage Account:
# https://docs.microsoft.com/en-us/azure/terraform/terraform-backend

terraform {
  backend "azurerm" {
    storage_account_name = "__TFSTATE-STORAGE-ACCOUNT-NAME__" # <-- Replace this
    container_name       = "__TFSTATE-CONTAINER-NAME__"       # <-- Replace this
    key                  = "terraform.tfstate"
    access_key           = "__TFSTATE-STORAGE-ACCESS-KEY__"   # <-- Replace this
  }
}

variable "prefix" {
  description = "A personal prefix (1-10 chars) that is attached to every resource to ensure its name is unique."
}

variable "resource_name" {
  default     = "contoso"
  description = "Name of the project. Will be attached to every resource."
}

resource "azurerm_resource_group" "workshop" {
  name     = "${var.prefix}${var.resource_name}workshop"
  location = "westeurope"
}
