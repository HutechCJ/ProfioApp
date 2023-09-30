#!/bin/bash

REGISTRY=ghcr.io/hutechcj

echo $REGISTRY

echo "Step 1: Building images..."

echo "Ready for building api image"

docker build -f apps/Profio.Api/Dockerfile . --tag $REGISTRY/profio-api:latest

echo "Ready for building proxy image"

docker build -f apps/Profio.Proxy/Dockerfile . --tag $REGISTRY/profio-proxy:latest

echo "Ready for building website image"

docker build -f apps/Profio.Website/Dockerfile . --tag $REGISTRY/profio-website:latest

echo "Ready for building cms image"

docker build -f apps/cms/Dockerfile . --tag $REGISTRY/profio-cms:latest

echo "Step 2: Pushing images..."

echo "Ready for pushing api image"

docker push $REGISTRY/profio-api:latest

echo "Ready for pushing proxy image"

docker push $REGISTRY/profio-proxy:latest

echo "Ready for pushing website image"

docker push $REGISTRY/profio-website:latest

echo "Ready for pushing cms image"

docker push $REGISTRY/profio-cms:latest

echo "Step 3: Done!"