resource "azurerm_cosmosdb_account" "workshop" {
  name                = "${var.prefix}${var.resource_name}db"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  offer_type          = "Standard"
  kind                = "GlobalDocumentDB"

  enable_automatic_failover = false

  consistency_policy {
    consistency_level       = "Session"
    max_interval_in_seconds = 5
    max_staleness_prefix    = 100
  }

  geo_location {
    location          = "${azurerm_resource_group.workshop.location}"
    failover_priority = 0
  }
}
