resource "azurerm_application_insights" "workshop" {
  name                = "${var.resource_name}${random_id.workshop.dec}insights"
  location            = "${azurerm_resource_group.workshop.location}"
  resource_group_name = "${azurerm_resource_group.workshop.name}"
  application_type    = "Web"
}
