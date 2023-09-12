variable "proxyResourceGroupName" {
  default = "ProfioProxy"
}

variable "proxyResourceGroupLocation" {
  default = "eastus"
}

variable "proxyResourceName" {
  default = "ProfioApp"
}

variable "proxyResourceLocation" {
  default = var.proxyResourceGroupLocation
}

resource "azurerm_resource_group" "proxy" {
  name     = var.proxyResourceGroupName
  location = var.proxyResourceGroupLocation
}

resource "azurerm_app_service_plan" "proxy" {
  name                = "Plan${substr(random_string.proxy.result, 0, 6)}"
  location            = azurerm_resource_group.proxy.location
  resource_group_name = azurerm_resource_group.proxy.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "proxy" {
  name                = var.proxyResourceName
  location            = azurerm_resource_group.proxy.location
  resource_group_name = azurerm_resource_group.proxy.name
  app_service_plan_id  = azurerm_app_service_plan.proxy.id
  https_only          = true
  reserved            = false

  site_config {
    dotnet_version = "dotnetcore"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "random_string" "proxy" {
  length  = 8
  special = false
}

output "proxy_app_service_id" {
  value = azurerm_app_service.proxy.id
}