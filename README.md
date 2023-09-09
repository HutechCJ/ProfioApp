<div align="center">
	<picture>
		<img loading="lazy" width="100%" src="./assets/img/cj-logistics.png" alt="Logo CJ">
	</picture>
	<p>
		<a href="https://app.deepsource.com/gh/HutechCJ/ProfioApp/?ref=repository-badge" target="_blank">
			<img loading="lazy" src="https://app.deepsource.com/gh/HutechCJ/ProfioApp.svg/?label=resolved+issues&show_trend=true&token=LhQScOINz_ITerHwJ5CKaFuM" alt="Deepsource">
		</a>
		<a href="https://github.com/HutechCJ/ProfioApp/network/updates" target="_blank">
			<img loading="lazy" src="https://img.shields.io/badge/dependabot-enabled-025e8c?logo=Dependabot" alt="Dependabot Status">
		</a>
		<a href="https://profioapp.azurewebsites.net/ " target="_blank">
			<img loading="lazy" src="https://img.shields.io/badge/azure-%230072C6.svg?logo=microsoftazure&logoColor=white" alt="Azure">
		</a>
		<a href="https://github.com/HutechCJ/ProfioApp/blob/main/LICENSE" target="_blank">
			<img loading="lazy" src="https://img.shields.io/badge/price-0%20₫-0098f7.svg" alt="Price">
		</a>
    <a href="https://gitpod.io/new/#https://github.com/HutechCJ/ProfioApp" target="_blank">
			<img loading="lazy" src="https://img.shields.io/badge/Gitpod-ready--to--code-blue?logo=gitpod" alt="Gitpod">
		</a>
	</p>
</div>

<hr>

<h1 align="justify"> Profio Application - 🚛 A Modern Logistics Management System ✈️ </h1>

<p align="center">
	Profio Application is a monorepo project built with <a href="https://nx.dev/">Nx</a> technology. It is a set of extensible dev tools for monorepos, which helps you develop like Google, Facebook, and Microsoft.
</p>

<h1>Table of Contents</h1>

- [Overview](#overview)
- [Tentative technologies](#tentative-technologies)
- [Building blocks](#building-blocks)
- [Getting Started](#getting-started)
  - [💻 Infrastructure](#-infrastructure)
  - [📦 Services](#-services)
  - [🛠️ Setup](#️-setup)
  - [🚀 Running the application](#-running-the-application)
  - [🧪 Testing the application](#-testing-the-application)
  - [🐳 Running services with Docker](#-running-services-with-docker)
- [API development](#api-development)
- [Open API](#open-api)
- [Dependency Graph](#dependency-graph)
- [CI/CD](#cicd)
- [External services](#external-services)
  - [📦 Container Management](#-container-management)
  - [🧑‍⚕️ Health Check](#️-health-check)
  - [📊 Monitoring](#-monitoring)
  - [📈 Tracing](#-tracing)
  - [📝 Logging](#-logging)
- [Contributing](#contributing)
- [Contributors](#contributors)
- [Support and Organization](#support-and-organization)
- [License](#license)

# Overview

<p align="justify">
	Profio - the symbol of professionalism in transportation management. Whether you need a solution for managing a fleet of vehicles or ships, Profio provides a powerful tool, optimizing and simplifying the process, ensuring every movement is quick, safe, and efficient.
</p>

<blockquote>
	<p align="justify">
		This is a product of the CJ Code your Future 2023 contest. The purpose of this project is to help CJ Logistics improve the quality of transportation management. This project is not for commercial purposes 👍
	</p>
	<p align="justify">
		We are a group of students from Hutech University. We are very happy to be able to participate in this contest. We hope that our project will be useful to CJ Logistics.
	</p>
</blockquote>

<p align="justify">
	📑 Read <a href="#" target="_blank">documentation</a> for more information about the project.
</p>

<p align="justify">
	If you want to find out more about the contest, please visit the <a href="https://www.facebook.com/itHutech/posts/pfbid033vvjf1btrN6JdvRoPrqarKHjqYQv5cBWyayJ5ghi1g9oRXDWCi9drQFinh8pM4YJl" target="_blank">CJ Code Your Future</a> from the Hutech IT Fanpage.
</p>

# Tentative technologies

- [Next.js 13](https://nextjs.org/)
- [ASP.NET Core](https://dotnet.microsoft.com/)
- [Flutter 3.13](https://flutter.dev/)
- [Python 3.11](https://www.python.org/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Redis](https://redis.io/)
- [PostgreSQL](https://www.postgresql.org/)
- [MQTT Broker](https://www.emqx.io/)
- [DeepSource](https://deepsource.io/)
- [CircleCI](https://circleci.com/)
- [K8s](https://kubernetes.io/), [Helm](https://helm.sh/)
- [OpenTelemetry](https://opentelemetry.io/)
- [Grafana](https://grafana.com/), [Prometheus](https://prometheus.io/), [Jaeger](https://www.jaegertracing.io/), [Seq](https://datalust.co/seq)

# Building blocks

<img loading="lazy" src="./assets/img/building-blocks.png" width="100%" alt="Building Blocks">

<table>
  <thead>
    <th>Name</th>
    <th>Usecase</th>
    <th>Technology</th>
  </thead>
  <tbody>
		<tr>
			<td><b>Client</b></td>
			<td>
				<p align="justify">
					It will show the information of orders, vehicles, drivers, and 	other information related to the transportation process. It will also provide a dashboard for managers to monitor the transportation process.
				</p>
			</td>
			<td>
				<p align="justify">
					Next.js
				</p>
			</td>
		</tr>
		<tr>
			<td><b>Load Balancer</b></td>
			<td>
				<p align="justify">
					It will distribute the load to the available servers. It will also 	provide a dashboard for managers to monitor the load of the servers.
				</p>
			</td>
			<td>
				<p align="justify">
					YARP
				</p>
			</td>
    </tr>
		<tr>
			<td><b>Driver App</b></td>
			<td>
				<p align="justify">
					It provides a dashboard for drivers to monitor the transportation process. It will send the location of the vehicle to the server.
				</p>
			</td>
			<td>
				<p align="justify">
					Flutter
				</p>
			</td>
		</tr>
		<tr>
			<td><b>MQTT Broker</b></td>
			<td>
				<p align="justify">
					It will receive the location of the vehicle from the driver app and send it to the server.
				</p>
			</td>
			<td>
				<p align="justify">
					EMQX
				</p>
			</td>
		</tr>
		<tr>
			<td><b>API Server</b></td>
			<td>
				<p align="justify">
					It will receive the location of the vehicle from the MQTT Broker and send it to the database. It will also send the information of orders, vehicles, drivers, and other information related to the transportation process to the client.
				</p>
			</td>
			<td>
				<p align="justify">
					ASP.NET Core
				</p>
			</td>
		</tr>
		<tr>
			<td><b>Database</b></td>
			<td>
				<p align="justify">
					It will store the information of orders, vehicles, drivers, and other information related to the transportation process. We use Redis to cache the data.
				</p>
			</td>
			<td>
				<p align="justify">
					PostgreSQL, Redis
				</p>
			</td>
		</tr>
		<tr>
			<td><b>OpenTelemetry Collector</b></td>
			<td>
				<p align="justify">
					It will receive the telemetry data from the OpenTelemetry. It will send the telemetry data to the OpenTelemetry Processor.
				</p>
			</td>
			<td>
				<p align="justify">
					OpenTelemetry Collector
				</p>
			</td>
		</tr>
		<tr>
			<td><b>Health Check</b></td>
			<td>
				<p align="justify">
					It will check the health of the servers. It will send the health status of the servers to the Load Balancer.
				</p>
			</td>
			<td>
				<p align="justify">
					ASP.NET Core
				</p>
			</td>
		</tr>
		<tr>
			<td><b>Exporter</b></td>
			<td>
				<p align="justify">
					It will export the telemetry data to the OpenTelemetry Collector.
    		</p>
    		</td>
    		<td>
    			<p align="justify">
    				Grafana, Prometheus, Jaeger, Seq
    			</p>
    		</td>
		</tr>
  </tbody>
</table>

# Getting Started

## 💻 Infrastructure

<ul>
	<li align="justify">
		<b><a href="https://nx.dev" target="_blank">Nx</a></b> - Nx is a set of 	extensible dev tools for monorepos, which helps you develop like Google, 	Facebook, and Microsoft.
	</li>
	<li align="justify">
		<b><a href="https://nodejs.org/en/" target="_blank">node.js</a></b> - Node.	js® is a JavaScript runtime built on Chrome's V8 JavaScript engine.
	</li>
	<li align="justify">
		<b><a href="https://www.npmjs.com/" target="_blank">npm</a></b> - npm is 	the package manager for the Node JavaScript platform.
	</li>
	<li align="justify">
		<b><a href="https://dotnet.microsoft.com/" target="_blank">.NET Core</a></b> - .NET is a developer platform with tools and libraries for building 	any type of app, including web, mobile, desktop, games, IoT, cloud, and 	microservices.
	</li>
	<li align="justify">
		<b><a href="https://https://www.python.org/" target="_blank">Python</a></b> - Python is a programming language that lets 	you work quickly and integrate systems more effectively.
	</li>
	<li align="justify">
		<b><a href="https://flutter.dev/" target="_blank">Flutter</a></b> - Flutter is Google’s UI toolkit for building beautiful, natively 	compiled applications for mobile, web, and desktop from a single codebase.
	</li>
	<li align="justify">
		<b><a href="https://www.docker.com/" target="_blank">Docker (Kubernetes 	Enabled)</a></b> - Docker is an open platform for developing, shipping, 	and running applications.
	</li>
	<li align="justify">
		<b><a href="https://docs.microsoft.com/en-us/windows/wsl/install-win10" 	target="_blank">WSL 2 - Ubuntu OS</a></b> - WSL 2 is a new version of the architecture 	that powers the Windows Subsystem for Linux to run ELF64 Linux binaries on 	Windows.
	</li>
</ul>

## 📦 Services

<ul>
	<li align="justify">
		<b><a href="https://render.com/" target="_blank">Render</a></b> - Render 	is a unified platform to build and run all your apps and websites with 	free SSL, global CDN, private networks and auto deploys from Git.
	</li>
	<li align="justify">
		<b><a href="https://redislabs.com/" target="_blank">Redis Labs</a></b> - 	Redis Labs is the home of Redis, the world’s most popular in-memory 	database, and commercial provider of Redis Enterprise.
	</li>
	<li align="justify">
		<b><a href="https://azure.microsoft.com/" target="_blank">Azure</a></b> - 	Azure is an ever-expanding set of cloud computing services to help your 	organization meet its business challenges.
	</li>
	<li align="justify">
		<b><a href="https://www.cloudamqp.com/" target="_blank">CloudAMQP</a></b> 	- CloudAMQP automates every part of setup, running and scaling of RabbitMQ 	clusters. Available on all major cloud and application platforms.
	</li>
	<li align="justify">
		<b><a href="https://www.emqx.io/" target="_blank">EMQX</a></b> - EMQ X 	Broker is a fully open source, highly scalable, highly available 	distributed MQTT messaging broker for IoT, M2M and Mobile applications 	that can handle tens of millions of concurrent clients.
	</li>
	<li align="justify">
		<b><a href="https://vercel.com/" target="_blank">Vercel</a></b> - Vercel 	is a cloud platform for static sites and Serverless Functions that fits 	perfectly with your workflow. It enables developers to host Jamstack 	websites and web services that deploy instantly, scale automatically, and 	require no supervision, all with no configuration.
	</li>
	<li align="justify">
		<b><a href="https://cloud.google.com/" target="_blank">Google Cloud 	Platform</a></b> - Google Cloud Platform, offered by Google, is a suite of 	cloud computing services that runs on the same infrastructure that Google 	uses internally for its end-user products, such as Google Search, Gmail, 	file storage, and YouTube.
	</li>
</ul>

## 🛠️ Setup

First, clone the repository to your local machine:

```bash
git clone https://github.com/HutechCJ/ProfioApp.git
```

Next, navigate to the root directory of the project and install the dependencies:

```bash
npm install --force
```

## 🚀 Running the application

For the CMS, navigate to the `apps/cms` directory and run the following command:

```bash
npx nx serve cms
```

It will open the CMS in your browser at [http://localhost:4200/](http://localhost:4200/).

For the API, navigate to the `apps/Profio.Api` directory and run the following command:

```bash
npx nx serve Profio.Api
```

It will open the API in your browser at [http://localhost:9023/](http://localhost:5023/).

For the Proxy, navigate to the `apps/Profio.Proxy` directory and run the following command:

```bash
npx nx serve Profio.Proxy
```

It will open the Proxy in your browser at [http://localhost:7221/](http://localhost:7221/).

For the Driver App, navigate to the `apps/profio-app` directory and run the following command:

```bash
npx nx serve profio-app
```

For running all applications, navigate to the root directory of the project and run the following command:

```bash
npx nx run-many --target=serve --all
```

> **Warming**
> All connections strings will be unavailable when this repository is public.

## 🧪 Testing the application

For the CMS, navigate to the `apps/cms-e2e` directory and run the following command:

```bash
npx nx e2e cms-e2e
```

For the API, navigate to the `apps/Profio.Api` directory and run the following command:

```bash
npx nx test Profio.Api
```

## 🐳 Running services with Docker

For running all services, navigate to the root directory of the project and run the following command:

```bash
docker-compose up -d
```

# API development

We use Clean Architecture for the API development. You can read more about Clean Architecture [here](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture). The CQRS and Mediator patterns are also used in the API development. You can read more about CQRS [here](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs) and Mediator [here](https://refactoring.guru/design-patterns/mediator).

<img loading="lazy" src="./assets/img/clean-architecture.png" width="100%" alt="Clean Architecture">

# Open API

<img loading="lazy" src="./assets/img/open-api.png" width="100%" alt="Open API">

> **Note**
> We have implemented Redoc to generate the API documentation.

# Dependency Graph

You can see the dependency graph of the project by running the following command:

```bash
npx nx dep-graph
```

Here is the dependency graph of the project, generated by Nx:

<p align="center">
	<img loading="lazy"  src="./assets/img/graph.png" width="100%" alt="Dependency Graph">
</p>

# CI/CD

<img loading="lazy"  src="./assets/img/ci-cd.png" width="100%" alt="Dependency Graph">

# External services

## 📦 Container Management

<p align="justify">
	We used the <b>Portainer</b> service to manage the containers. Portainer is an open-source lightweight management UI which allows you to easily manage your Docker environments.
</p>

<img loading="lazy"  src="./assets/img/portainer.png" width="100%" alt="Dependency Graph">

## 🧑‍⚕️ Health Check

<p align="justify">
	For the API health check, We have used the <b>ASP.NET Core Health Checks</b> library to monitor the health of the application. The health check endpoint is exposed at <b>/hc</b> or <b>/hc-ui</b> and can be accessed via HTTP GET.
</p>

<img loading="lazy"  src="./assets/img/hc-ui.png" width="100%" alt="Dependency Graph">

## 📊 Monitoring

<p align="justify">
For the monitoring service, We have used the <b>Prometheus</b> and <b>Grafana</b> services to collect and analyze metrics.
</p>

<img loading="lazy" src="./assets/img/Prometheus.png" width="100%" alt="Prometheus">

<img loading="lazy" src="./assets/img/Grafana.png" width="100%" alt="Grafana">

## 📈 Tracing

<p align="justify">
For the tracing service, We have used the <b>Jaeger</b> service to collect and analyze traces.
</p>

<img loading="lazy" src="./assets/img/Jaeger.png" width="100%" alt="Jaeger">

## 📝 Logging

<p align="justify">
We set up the <b>seq</b> service to collect and analyze logs.
</p>

<img loading="lazy" src="./assets/img/seq.jpg" width="100%" alt="seq">

# Contributing

Wanna be here? [Contribute](./.github/CONTRIBUTING.md).

- Fork this repository.
- Create your new branch with your feature: `git checkout -b my-feature`.
- Commit your changes: `git commit -am 'feat: My new feature'`.
- Push to the branch: `git push origin my-feature`.
- Submit a pull request 👌.

# Contributors

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key))

<table>
	<tr>
		<td align="center" valign="top">
				<img loading="lazy" width="150" height="150" src="https://github.com/foxminchan.png?s=150">
        <br>
        <a href="https://github.com/foxminchan">Xuan Nhan</a>
        <p>
          <a href="https://github.com/HutechCJ/ProfioApp/commits?author=foxminchan" title="Developer">💻</a>
          <a href="#docs" title="Documentation">📖</a>
          <a href="#infra" title="Infrastructure">🚇</a>
          <a href="#tool" title="Tools">🔧</a>
					<a href="#mentoring" title="Mentoring">🧑‍🏫</a>
        </p>
		</td>
		<td align="center" valign="top">
				<img loading="lazy" width="150" height="150" src="https://github.com/Slimaeus.png?s=150">
        <br>
        <a href="https://github.com/Slimaeus">Hong Thai</a>
        <p>
          <a href="https://github.com/HutechCJ/ProfioApp/commits?author=Slimaeus" title="Developer">💻</a>
          <a href="#infra" title="Infrastructure">🚇</a>
					<a href="#projectManagement" title="Project Management">📆</a>
					<a href="#maintenance" title="Maintenance">🚧</a>
					<a href="#review" title="Reviewed Pull Requests">👀</a>
        </p>
		</td>
		<td align="center" valign="top">
				<img loading="lazy" width="150" height="150" src="https://github.com/nhonvo.png?s=150">
        <br>
        <a href="https://github.com/nhonvo">Truong Nhon</a>
        <p>
          <a href="https://github.com/HutechCJ/ProfioApp/commits?author=nhonvo" title="Developer">💻</a>
          <a href="#ideas" title="Ideas, Planning, & Feedback">🤔</a>
					<a href="#data" title="Data">🔣</a>
					<a href="#business" title="Business Development">💼</a>
        </p>
		</td>
		<td align="center" valign="top">
				<img loading="lazy" width="150" height="150" src="https://github.com/fiezt1492.png?s=150">
        <br>
        <a href="https://github.com/fiezt1492">Tien Dat</a>
        <p>
          <a href="https://github.com/HutechCJ/ProfioApp/commits?author=fiezt1492" title="Developer">💻</a>
          <a href="#design" title="Design">🎨</a>
					<a href="#content" title="Content">🖋</a>
					<a href="#maintenance" title="Maintenance">🚧</a>
        </p>
		</td>
		<td align="center" valign="top">
				<img loading="lazy" width="150" height="150" src="https://github.com/MeiCloudie.png?s=150">
        <br>
        <a href="https://github.com/MeiCloudie">Thuc Van</a>
        <p>
          <a href="https://github.com/HutechCJ/ProfioApp/commits?author=MeiCloudie" title="Developer">💻</a>
          <a href="#design" title="Design">🎨</a>
					<a href="#content" title="Content">🖋</a>
					<a href="#talk" title="Talks">📢</a>
        </p>
		</td>
	</tr>
</table>

# Support and Organization

<p align="center">
	<a href="https://cjvietnam.vn/" target="_blank">
		<img loading="lazy" src="./assets/img/cj-logo.png" width="100px" alt="CJ">
	</a>
	<a href="https://hutech.edu.vn/" target="_blank">
		<img loading="lazy" src="./assets/img/hutech-logo.png" width="100px" alt="CJ">
	</a>
</p>

# License

This project is made available under the MIT license. See the [LICENSE](LICENSE) file for details
