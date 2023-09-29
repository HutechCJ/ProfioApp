@description('The kube config for the target Kubernetes cluster.')
@secure()
param kubeConfig string

@description('Address of the container registry where container resides')
param containerRegistry string

@description('Tag of container to use')
param containerTag string = 'latest'

import 'kubernetes@1.0.0' with {
  kubeConfig: kubeConfig
  namespace: 'default'
}

resource proxyDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'proxy'
    labels: {
      app: 'proxy'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'proxy'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'proxy'
        }
      }
      spec: {
        containers: [
          {
            name: 'proxy'
            image: '${containerRegistry}/hutechcj/profio-proxy:${containerTag}'
            imagePullPolicy: 'Always'
            ports: [
              {
                containerPort: 80
              }
            ]
            env: [
              {
                name: 'ASPNETCORE_ENVIRONMENT'
                value: 'Development'
              }
            ]
          }
        ]
      }
    }
  }
}

resource proxyService 'core/Service@v1' = {
  metadata: {
    name: 'proxy'
    labels: {
      app: 'proxy'
    }
  }
  spec: {
    selector: {
      app: 'proxy'
    }
    ports: [
      {
        port: 80
        targetPort: '7221'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
