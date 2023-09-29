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

resource apiDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'api'
    labels: {
      app: 'api'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'api'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'api'
        }
      }
      spec: {
        containers: [
          {
            name: 'api'
            image: '${containerRegistry}/hutechcj/profio-api:${containerTag}'
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

resource apiService 'core/Service@v1' = {
  metadata: {
    name: 'api'
    labels: {
      app: 'api'
    }
  }
  spec: {
    type: 'ClusterIP'
    ports: [
      {
        port: 80
        targetPort: '5023'
      }
    ]
    selector: {
      app: 'api'
    }
  }
}
