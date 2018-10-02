resource "azurerm_app_service_plan" "workshop" {
  name                = "${var.resource_name}-service-plan"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"

  # Define Linux as Host OS
  kind = "Linux"

  sku {
    tier = "Standard"
    size = "S1"
  }

  properties {
    reserved = true # Mandatory for Linux plans
  }
}

resource "azurerm_app_service" "workshop" {
  name                = "${var.resource_name}${random_id.workshop.dec}"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  app_service_plan_id = "${azurerm_app_service_plan.workshop.id}"

  app_settings {
    APPINSIGHTS_INSTRUMENTATIONKEY = "${azurerm_application_insights.workshop.instrumentation_key}"

    AzureCosmosDb__Endpoint = "${azurerm_cosmosdb_account.workshop.endpoint}"
    AzureCosmosDb__Key      = "${azurerm_cosmosdb_account.workshop.primary_master_key}"

    AzureStorage__StorageAccountName = "${azurerm_storage_account.workshop.name}"
    AzureStorage__Key                = "${azurerm_storage_account.workshop.primary_access_key}"

    WEBSITES_ENABLE_APP_SERVICE_STORAGE = false
  }

  # Configure Docker Image to load on start
  site_config {
    linux_fx_version = "DOCKER|robinmanuelthiel/contosomaintenance-api:latest"
    always_on        = "true"
  }

  identity {
    type = "SystemAssigned"
  }
}
