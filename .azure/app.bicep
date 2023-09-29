@description('Name of the container registry. Defaults to unique hashed ID prefixed with "profio"')
param registryName string = 'profio-${uniqueString(resourceGroup().id)}'

@description('Name of the AKS cluster. Defaults to a unique hash prefixed with "profio"')
param clusterName string = 'profio'

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2023-08-01-preview' existing = {
  name: registryName
}

resource aksCluster 'Microsoft.ContainerService/managedClusters@2023-07-02-preview' existing = {
  name: clusterName
}

module reverseproxy 'app/proxy.bicep' = {
  name: 'proxy'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module api 'app/api.bicep' = {
  name: 'api'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module website 'app/website.bicep' = {
  name: 'website'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module cms 'app/cms.bicep' = {
  name: 'cms'
  params: {
    containerRegistry: containerRegistry.properties.loginServer
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}

module ingress 'app/ingress.bicep' = {
  name: 'ingress'
  params: {
    HTTPApplicationRoutingZoneName: aksCluster.properties.addonProfiles.httpApplicationRouting.config.HTTPApplicationRoutingZoneName
    kubeConfig: aksCluster.listClusterAdminCredential().kubeconfigs[0].value
  }
}
