resource "azurerm_container_registry" "workshop" {
  name                = "${var.prefix}${var.resource_name}registry"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  location            = "${azurerm_resource_group.workshop.location}"
  admin_enabled       = true
  sku                 = "Classic"
  storage_account_id  = "${azurerm_storage_account.workshop.id}"
}
