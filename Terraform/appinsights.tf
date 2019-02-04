resource "azurerm_application_insights" "workshop" {
  name                = "${var.prefix}${var.resource_name}insights"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  application_type    = "Web"
}
