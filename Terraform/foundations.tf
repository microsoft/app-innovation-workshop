# Comment the following object to use local state
terraform {
  backend "azurerm" {
    storage_account_name = "__TFSTATE-STORAGE-ACCOUNT-NAME__" # <-- TODO: Replace this
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
    access_key           = "__TFSTATE-STORAGE-ACCESS-KEY__"   # <-- TODO: Replace this
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
