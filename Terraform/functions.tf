resource "azurerm_app_service_plan" "func" {
  name                = "${var.prefix}${var.resource_name}functionsserviceplan"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "workshop" {
  name                      = "${var.prefix}${var.resource_name}functions"
  location                  = "${azurerm_resource_group.workshop.location}"
  resource_group_name       = "${azurerm_resource_group.workshop.name}"
  app_service_plan_id       = "${azurerm_app_service_plan.func.id}"
  storage_connection_string = "${azurerm_storage_account.workshop.primary_connection_string}"

  # looks like at the moment for v2 http version has to be http1.1 and app has to be 32bit
  version = "~2"

  app_settings {
    APPINSIGHTS_INSTRUMENTATIONKEY = "${azurerm_application_insights.workshop.instrumentation_key}"

    CosmosDB = "AccountEndpoint=${azurerm_cosmosdb_account.workshop.endpoint};AccountKey=${azurerm_cosmosdb_account.workshop.primary_master_key};"

    CognitiveServicesEndpoint = "${azurerm_template_deployment.workshop.outputs["vision_endpoint"]}"
    CognitiveServicesKey      = "${azurerm_template_deployment.workshop.outputs["vision_key"]}"
  }

  identity {
    type = "SystemAssigned"
  }
}
