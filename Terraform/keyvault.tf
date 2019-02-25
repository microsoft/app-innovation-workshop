resource "azurerm_key_vault" "workshop" {
  name                = "${var.prefix}${var.resource_name}vault"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  tenant_id           = "${data.azurerm_client_config.current.tenant_id}"

  sku {
    name = "standard"
  }

  access_policy {
    tenant_id = "${data.azurerm_client_config.current.tenant_id}"
    object_id = "${azurerm_app_service.workshop.id}"

    key_permissions = [
      "get",
    ]

    secret_permissions = [
      "get",
    ]
  }
}

resource "azurerm_key_vault_secret" "workshop-cosmosdbkey" {
  name      = "CosmosDbKey"
  value     = "${azurerm_cosmosdb_account.workshop.primary_master_key}"
  vault_uri = "${azurerm_key_vault.workshop.vault_uri}"
}
