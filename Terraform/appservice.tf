resource "azurerm_app_service_plan" "workshop" {
  name                = "${var.resource_name}-service-plan"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "workshop" {
  name                      = "${var.resource_name}${random_id.workshop.dec}"
  location                  = "${azurerm_resource_group.workshop.location}"
  resource_group_name       = "${azurerm_resource_group.workshop.name}"
  app_service_plan_id       = "${azurerm_app_service_plan.workshop.id}"

  app_settings {
    APPINSIGHTS_INSTRUMENTATIONKEY = "${azurerm_application_insights.workshop.instrumentation_key}"

    AzureCosmosDb__EndPoint = "${azurerm_cosmosdb_account.workshop.endpoint}"
    AzureCosmosDb__Key      = "${azurerm_cosmosdb_account.workshop.primary_master_key}"

    AzureStorage__StorageAccountName = "${azurerm_storage_account.workshop.name}"
    AzureStorage__Key                = "${azurerm_storage_account.workshop.primary_access_key}"
  }

  identity {
    type = "SystemAssigned"
  }
}
