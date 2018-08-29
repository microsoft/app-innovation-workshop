resource "azurerm_search_service" "workshop" {
  name                = "${var.resource_name}${random_id.workshop.dec}search"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  location            = "${azurerm_resource_group.workshop.location}"
  sku                 = "standard"
}