resource "azurerm_cosmosdb_account" "workshop" {
    name                = "cosmos-db-${random_id.workshop.dec}"
    location            = "${azurerm_resource_group.workshop.location}"
    resource_group_name = "${azurerm_resource_group.workshop.name}"
    offer_type          = "Standard"
    kind                = "GlobalDocumentDB"

    enable_automatic_failover = false

    consistency_policy {
        consistency_level       = "BoundedStaleness"
        max_interval_in_seconds = 10
        max_staleness_prefix    = 200
    }

    geo_location {
        location          = "${azurerm_resource_group.workshop.location}"
        failover_priority = 0
    }
}