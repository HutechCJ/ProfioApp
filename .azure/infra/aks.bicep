@description('Azure region to deploy resources into. Defaults to location of target resource group')
param location string = resourceGroup().location

@description('Name of the AKS cluster. Defaults to "profio"')
param clusterName string = 'profio'

@description('The size of the Virtual Machine.')
param vmSize string = 'Standard_B1s'

@description('The number of nodes for the cluster.')
@minValue(1)
@maxValue(50)
param agentCount int = 4

resource aksCluster 'Microsoft.ContainerService/managedClusters@2023-07-02-preview' = {
  name: clusterName
  location: location
  properties: {
    kubernetesVersion: '1.28.2'
    agentPoolProfiles: [
      {
        name: 'agentpool'
        osDiskSizeGB: 0
        count: agentCount
        vmSize: vmSize
        osType: 'Linux'
        mode: 'System'
      }
    ]
    dnsPrefix: '${clusterName}-dns'
    enableRBAC: true
    addonProfiles: {
      httpApplicationRouting: {
        enabled: true
      }
    }
  }
  identity: {
    type: 'SystemAssigned'
  }
}

output kubeConfig string = aksCluster.name
