resource "random_id" "workshop" {
  byte_length = 2
}

resource "azurerm_resource_group" "workshop" {
  name     = "${var.resource_name}-${random_id.workshop.dec}"
  location = "westeurope"
}

variable "resource_name" {
  default = "contosomaintenance"
}
