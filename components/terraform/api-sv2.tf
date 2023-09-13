variable "apiSv2ResourceName" {
  default = "Profio-Sv2"
}

resource "azurerm_api_management" "example_sv2" {
  name                = var.apiSv2ResourceName
  location            = var.resourceGroupLocation
  resource_group_name = var.resourceGroupName
  publisher_name      = "Nguyen Xuan Nhan"
  publisher_email     = "nguyenxuannhan407@gmail.com"

  sku {
    name     = "Developer"
    capacity = 1
  }

  identity {
    type = "SystemAssigned"
  }
}
