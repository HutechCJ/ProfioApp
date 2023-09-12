# Define variables
variable "resourceGroupName" {
  default = "ProfioProxy"
}

variable "resourceGroupLocation" {
  default = "eastus"
}

variable "resourceName" {
  default = "ProfioApp"
}

variable "resourceLocation" {
  default = var.resourceGroupLocation
}

resource "azurerm_resource_group" "rs_group" {
  name     = var.resourceGroupName
  location = var.resourceGroupLocation
}

resource "azurerm_app_service_plan" "plan" {
  name                = "Plan${substr(random_string.secret.result, 0, 6)}"
  location            = azurerm_resource_group.rs_group.location
  resource_group_name = azurerm_resource_group.rs_group.name

  sku {
    tier = "Standard"
    size = "S1"
  }
}

resource "azurerm_app_service" "app_sv" {
  name                = var.resourceName
  location            = azurerm_resource_group.rs_group.location
  resource_group_name = azurerm_resource_group.rs_group.name
  app_service_plan_id  = azurerm_app_service_plan.plan.id
  https_only          = true
  reserved            = false

  site_config {
    dotnet_version = "dotnetcore"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "random_string" "secret" {
  length  = 8
  special = false
}

output "app_service_id" {
  value = azurerm_app_service.app_sv.id
}