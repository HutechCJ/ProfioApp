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

resource websiteDeployment 'apps/Deployment@v1' = {
  metadata: {
    name: 'website'
    labels: {
      app: 'website'
    }
  }
  spec: {
    replicas: 1
    selector: {
      matchLabels: {
        app: 'website'
      }
    }
    template: {
      metadata: {
        labels: {
          app: 'website'
        }
      }
      spec: {
        containers: [
          {
            name: 'website'
            image: '${containerRegistry}/hutechcj/profio-website:${containerTag}'
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

resource websiteService 'core/Service@v1' = {
  metadata: {
    name: 'website'
    labels: {
      app: 'website'
    }
  }
  spec: {
    selector: {
      app: 'website'
    }
    ports: [
      {
        port: 80
        targetPort: '5272'
        protocol: 'TCP'
      }
    ]
    type: 'ClusterIP'
  }
}
