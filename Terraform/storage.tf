resource "azurerm_storage_account" "workshop" {
  name                     = "${var.prefix}${var.resource_name}store"
  resource_group_name      = "${azurerm_resource_group.workshop.name}"
  location                 = "${azurerm_resource_group.workshop.location}"
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "large" {
  name                  = "images-large"
  resource_group_name   = "${azurerm_resource_group.workshop.name}"
  storage_account_name  = "${azurerm_storage_account.workshop.name}"
  container_access_type = "blob"
}

resource "azurerm_storage_container" "medium" {
  name                  = "images-medium"
  resource_group_name   = "${azurerm_resource_group.workshop.name}"
  storage_account_name  = "${azurerm_storage_account.workshop.name}"
  container_access_type = "blob"
}

resource "azurerm_storage_container" "icon" {
  name                  = "images-icon"
  resource_group_name   = "${azurerm_resource_group.workshop.name}"
  storage_account_name  = "${azurerm_storage_account.workshop.name}"
  container_access_type = "blob"
}

resource "azurerm_storage_queue" "workshop" {
  name                 = "processphotos"
  resource_group_name  = "${azurerm_resource_group.workshop.name}"
  storage_account_name = "${azurerm_storage_account.workshop.name}"
}
