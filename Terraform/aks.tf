# resource "azurerm_kubernetes_cluster" "workshop" {
#   name                = "${var.prefix}${var.resource_name}aks"
#   location            = "${azurerm_resource_group.workshop.location}"
#   resource_group_name = "${azurerm_resource_group.workshop.name}"
#   dns_prefix          = "${var.prefix}${var.resource_name}aks"
#   agent_pool_profile {
#     name            = "default"
#     count           = 3
#     vm_size         = "Standard_B2s"
#     os_type         = "Linux"
#     os_disk_size_gb = 30
#   }
#   service_principal {
#     client_id     = "${var.prefix}${var.resource_name}akssp"
#     client_secret = ""
#   }
# }

