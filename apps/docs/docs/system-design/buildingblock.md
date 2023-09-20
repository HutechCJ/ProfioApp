---
title: Building Blocks
description: A brief description of the building blocks
sidebar_position: 1
---

<img
loading="lazy"
src={require('../../static/img/architechture/building-blocks.png').default}
alt="Profio Introduction"
style={{ width: '100%', height: 'auto' }}
/>

## Components

### Load Balancer

<p align="justify">

- The Load Balancer serves as the entry point for incoming client requests, distributing them to the Profio API servers.
- Ensures high availability and load distribution for improved system reliability and scalability.

</p>

### OpenTelemetry Collector

<p align="justify">

- The OpenTelemetry Collector is crucial for observability within the architecture.
- Collects, processes, and transmits observability data, including traces, metrics, and logs.
- This data is essential for monitoring system health, diagnosing performance issues, and optimizing system behavior.

</p>

### MQTT Broker

- The MQTT Broker facilitates real-time communication between the Profio API servers and the Driver App.
- Utilizes the MQTT protocol for low-latency and efficient message exchange, enabling real-time updates.

### Health Check

<p align="justify">

- The Health Check component continuously monitors the Profio API servers' health.
- Conducts routine health assessments, ensuring server responsiveness and functionality.
- Triggers automated responses in case of detected issues to maintain system reliability.

</p>

### Profio API Servers

<p align="justify">

- The Profio API is divided into two server instances, "API Server 1" and "API Server 2.".
- Core of the system, developed using ASP.NET Core, a versatile framework for web applications and APIs.
- These servers are responsible for processing incoming client requests, executing business logic, and interacting with data sources.

</p>

### Database

<p align="justify">

- PostgreSQL serves as the database management system for storing and managing the system's data. PostgreSQL is a robust choice for relational database needs, offering data consistency and durability.

</p>

### Cache

- Redis acts as an in-memory cache, optimizing data retrieval and reducing the database load.
- Improves response times and overall system performance.

### Driver App

<p align="justify">

- The mobile interface, built using Flutter, offers cross-platform capabilities.
- Efficiently developed and maintained from a single codebase, ensuring consistent user experiences on iOS and Android platforms.

</p>

### External Services Cluster

<p align="justify">

- Enhances system observability and monitoring:
  - **Prometheus**: Collects and stores essential metrics and statistics for real-time monitoring and automated alerting.
  - **Grafana**: Customizable dashboarding platform for visualizing system metrics and data analysis.
  - **Jaeger**: Enables detailed analysis of request flows, identifying latency bottlenecks and performance optimizations.
  - **Seq**: Centralized log management and analysis for troubleshooting and debugging.

</p>

### Client Framework

<p align="justify">

- For the web interface, Next.js is chosen as the client-side framework.
- Next.js excels in building server-rendered React applications, offering improved performance, SEO, and user experience.

</p>

## API Architecture & Patterns

### Clean Architecture

<img
loading="lazy"
src={require('../../static/img/architechture/clean.png').default}
alt="Clean Architecture"
style={{ width: '100%', height: 'auto' }}
/>

<p align="justify">
Clean Architecture is a software design principle that emphasizes organizing code in a way that separates concerns, making it easier to maintain and test. It divides an application into distinct layers: the Core (containing business logic and entities), Use Cases (defining application-specific rules), Interface Adapters (connecting to the outside world), and Frameworks/Drivers (external dependencies). This separation allows for flexibility, testability, and maintainability, reducing the risk of unintended side effects when making changes. Clean Architecture promotes robust and adaptable software systems.
</p>

### CQRS and Mediator Pattern

<img
loading="lazy"
src={require('../../static/img/architechture/cqrs.png').default}
alt="CQRS and Mediator Pattern"
style={{ width: '100%', height: 'auto' }}
/>

<p align="justify">
CQRS (Command Query Responsibility Segregation) is a software design pattern that separates the read and write operations of a system into two distinct models. The Command model handles write operations, while the Query model handles read operations. This separation allows for the optimization of each model for its specific purpose, improving performance and scalability. The Mediator Pattern is used to implement CQRS, decoupling the Command and Query models and enabling communication between them. The Mediator Pattern also allows for the addition of new handlers without modifying existing handlers, improving the system's flexibility and maintainability.
</p>

## Communication Protocols

### HTTP

<p align="justify">
Client-server communication is handled using the HTTP protocol. HTTP is a widely used application-layer protocol for transmitting data between clients and servers. It is a stateless protocol, meaning that each request is independent of the previous request. HTTP is a reliable and efficient protocol for client-server communication.
</p>

### MQTT

<p align="justify">
The GPS tracking functionality is implemented using the MQTT protocol. MQTT is a lightweight messaging protocol designed for IoT devices. It is a publish-subscribe protocol, meaning that clients can subscribe to topics and receive messages published to those topics. MQTT is a reliable and efficient protocol for real-time communication.
</p>

### OTLP

<p align="justify">
The OpenTelemetry Protocol (OTLP) is used for transmitting observability data. OTLP is a protocol for transmitting telemetry data, including traces, metrics, and logs. OTLP is a reliable and efficient protocol for transmitting observability data.
</p>
