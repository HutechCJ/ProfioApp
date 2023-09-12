variable "apiSv2ResourceName" {
  default = "Profio-Sv2"
}

resource "azurerm_api_management" "example_sv2" {
  name                = var.apiSv2ResourceName
  location            = var.resourceGroupLocation
  resource_group_name = var.resourceGroupName
  publisher_name      = "Your Company"
  publisher_email     = "your@email.com"

  sku {
    name     = "Developer"
    capacity = 1
  }

  identity {
    type = "SystemAssigned"
  }
}
