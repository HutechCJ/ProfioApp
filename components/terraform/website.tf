variable "websiteResourceName" {
  default = "Profio-Wesite"
}

resource "azurerm_website_management" "example_website" {
  name                = var.websiteResourceName
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
