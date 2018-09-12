resource "azurerm_resource_group" "workshop" {
  name     = "${var.resource_name}"
  location = "northeurope"
}

resource "random_id" "workshop" {
  keepers = {
    # Generate a new ID only when a new resource group is defined
    resource_group = "${azurerm_resource_group.workshop.name}"
  }

  byte_length = 2
}