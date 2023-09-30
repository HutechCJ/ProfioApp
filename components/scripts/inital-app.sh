#!/bin/bash

sudo apt-get update -y
sudo apt-get upgrade -y

if ! [ -x "$(command -v node)" ]; then
	echo "Installing nodejs and npm..."
	sudo apt-get install -y nodejs npm
fi

if ! [ -x "$(command -v dotnet)" ]; then
	echo "Installing dotnet core..."
	sudo apt-get install -y apt-transport-https
	sudo apt-get update -y
	sudo apt-get install -y dotnet-sdk-7.0
fi

if ! [ -x "$(command -v docker)" ]; then
	echo "Installing docker..."
	sudo apt-get install -y docker.io
fi

if ! [ -x "$(command -v docker-compose)" ]; then
	echo "Installing docker-compose..."
	sudo apt-get install -y docker-compose
fi

echo "Installing dependencies..."

npm install --force

echo "Building nx workspace..."

NX_BRANCH=main npx nx run-many -t build --all --parallel --maxParallel=3

echo "Initializing containers from docker-compose.o11y.yml..."

cd ../docker
docker-compose -f docker-compose.o11y.yml up -d

echo "Would you like to initialize containers from docker-compose.yml? (y/n)"

read answer

if [ "$answer" != "${answer#[Yy]}" ] ;then
		echo "Initializing containers from docker-compose.yml..."
		docker-compose up -d
fi

echo "If you want to use k8s or terraform, please enter [y] to continue, otherwise enter [n]"

read answer

if [ "$answer" != "${answer#[Yy]}" ] ;then
		echo "Installing kubectl..."
		sudo apt-get install -y kubectl
		echo "Installing heml..."
		sudo apt-get install -y helm
		echo "Installing terraform..."
		sudo apt-get install -y terraform
fi

echo "Done!"