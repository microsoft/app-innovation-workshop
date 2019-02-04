resource "azurerm_template_deployment" "workshop" {
  name                = "${var.prefix}${var.resource_name}vision"
  resource_group_name = "${azurerm_resource_group.workshop.name}"

  template_body = <<DEPLOY
{
    "$schema": "http://schema.management.azure.com/schemas/2014-04-01-preview/deploymentTemplate.json#",
    "contentVersion": "1.0.0.0",
    "parameters": {
        "name": {
            "type": "String"
        },
        "location": {
            "type": "String"
        },
        "apiType": {
            "type": "String"
        },
        "sku": {
            "type": "String"
        }
    },
    "resources": [
        {
            "type": "Microsoft.CognitiveServices/accounts",
            "sku": {
                "name": "[parameters('sku')]"
            },
            "kind": "[parameters('apiType')]",
            "name": "[parameters('name')]",
            "apiVersion": "2016-02-01-preview",
            "location": "[parameters('location')]",
            "properties": {}
        }
    ],
    "outputs": {
        "vision_endpoint": {
            "type": "string",
            "value": "[reference(parameters('name')).Endpoint]"
        },
        "vision_key": {            
            "type": "string",
            "value": "[listKeys(resourceId('Microsoft.CognitiveServices/accounts', parameters('name')), providers('Microsoft.CognitiveServices', 'accounts').apiVersions[0]).key1]"
        }
    }
}
  DEPLOY

  parameters {
    "name"     = "${var.prefix}${var.resource_name}vision"
    "location" = "${azurerm_resource_group.workshop.location}"
    "apiType"  = "ComputerVision"
    "sku"      = "S1"
  }

  deployment_mode = "Incremental"
}
