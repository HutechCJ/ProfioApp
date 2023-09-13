variable "apiSv1ResourceName" {
  default = "Profio-Sv1"
}

resource "azurerm_api_management" "example_sv1" {
  name                = var.apiSv1ResourceName
  location            = var.resourceGroupLocation
  resource_group_name = var.resourceGroupName
  publisher_name      = "Nguyen Hong Thai"
  publisher_email     = "nguyenhongthai28042002@gmail.com"

  sku {
    name     = "Developer"
    capacity = 1
  }

  identity {
    type = "SystemAssigned"
  }
}
