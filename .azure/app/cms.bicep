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

resource cmsDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'cms'
    labels: {
      app: 'cms'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'cms'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'cms'
        }
      }
      spec: {
        containers: [
          {
            name: 'cms'
            image: '${containerRegistry}/hutechcj/profio-cms:${containerTag}'
            imagePullPolicy: 'Always'
            ports: [
              {
                containerPort: 80
              }
            ]
            env: [
              {
                name: 'NEXT_PUBLIC_GOOGLE_MAP_API_KEY'
                value: 'AIzaSyDAW0v16XSZI3GdNte36gFHDynsed4-cz0'
              }
              {
                name: 'API_BASEURL'
                value: 'https://profioapp.azurecmss.net/api/v1'
              }
              {
                name: 'SERVER_HOSTNAME'
                value: 'profioapp.azurecmss.net'
              }
            ]
          }
        ]
      }
    }
  }
}

resource cmsService 'core/Service@v1' = {
  metadata: {
    name: 'cms'
    labels: {
      app: 'cms'
    }
  }
  spec: {
    selector: {
      app: 'cms'
    }
    ports: [
      {
        port: 80
        targetPort: '4200'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
