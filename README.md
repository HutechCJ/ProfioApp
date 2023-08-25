<h1 align="center">
	<a name="readme-top"></a>
	<p><img loading="lazy" width=25% src="./static/img/logo.png"></p>
Profio Application
</h1>

<p align="center">
	Profio Application is an innovative GitHub repository focusing on a streamlined application designed for advanced profile management.
</p>

# Overview

<p align="justify">
	Profio Application, a blend of <b>profile</b> and <b>portfolio</b>, is a versatile platform for crafting and overseeing personal portfolios. Users can tailor themes, document educational and career milestones, and incorporate external links. Tech aficionados benefit from a distinct tech stack showcase. Enhanced with drag-and-drop functionality, all user data is anchored in a sophisticated database structure
</p>

# Getting Started

## 💻 Infrastructure

- **[Nx](https://nx.dev)** - Nx is a set of extensible dev tools for monorepos, which helps you develop like Google, Facebook, and Microsoft.
- **[node.js](https://nodejs.org/en/)** - Node.js® is a JavaScript runtime built on Chrome's V8 JavaScript engine.
- **[npm](https://www.npmjs.com/)** - npm is the package manager for the Node JavaScript platform.
- **[.NET Core](https://dotnet.microsoft.com/)** - .NET is a developer platform with tools and libraries for building any type of app, including web, mobile, desktop, games, IoT, cloud, and microservices.
- **[Docker (Kubernetes Enabled)](https://www.docker.com/)** - Docker is an open platform for developing, shipping, and running applications.
- **[Redis](https://redis.io/)** - Redis is an open source (BSD licensed), in-memory data structure store, used as a database, cache, and message broker.

## 📦 Services

- **[Render](https://render.com/)** - Render is a unified platform to build and run all your apps and websites with free SSL, global CDN, private networks and auto deploys from Git.
- **[Neo4j aura](https://neo4j.com/)** - Neo4j Aura is the simplest way to run Neo4j in the cloud. It is a fully-managed database as a service with built-in graph analytics to answer questions about how people, processes and systems are interrelated.
- **[Azure](https://azure.microsoft.com/)** - Azure is an ever-expanding set of cloud computing services to help your organization meet its business challenges.


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

For the API, navigate to the `apps/Profio.Api` directory and run the following command:

```bash
npx nx serve Profio.Api
```

For the Application, navigate to the `apps/profio-app` directory and run the following command:

```bash
npx nx serve profio-app
```

For running all applications, navigate to the root directory of the project and run the following command:

```bash
npx nx run-many --target=serve --all
```

## 🧪 Testing the application

For the CMS, navigate to the `apps/cms-e2e` directory and run the following command:

```bash
npx nx e2e cms-e2e
```

For the API, navigate to the `apps/Profio.Api` directory and run the following command:

```bash
npx nx test Profio.Api
```

For the Application, navigate to the `apps/profio-app-e2e` directory and run the following command:

```bash
npx nx e2e profio-app-e2e
```

# Dependency Graph

You can see the dependency graph of the project by running the following command:

```bash
npx nx dep-graph
```

Here is the dependency graph of the project:

<p align="center">
	<img loading="lazy"  src="./static/img/graph.png" width="100%" alt="Dependency Graph">
</p>

# License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
