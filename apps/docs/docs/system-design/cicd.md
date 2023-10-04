---
title: CI/CD
description: A CI/CD pipeline
sidebar_position: 3
last_update:
  author: Nguyen Xuan Nhan
---

# CI/CD

<img
loading="lazy"
src={require('../../static/img/architechture/ci-cd.png').default}
alt="CI/CD"
style={{ width: '100%', height: 'auto' }}
/>

## Continuous Integration (CI) Workflow

### Activating Code Inspection Services:

<p align="justify">

When a developer pushes code to the GitHub repository, our system automatically triggers a series of services to ensure that the code maintains high quality and security:

- **Deepsource:** The Deepsource service is used to perform source code analysis. It checks the source code and assesses whether it adheres to best practices and is free from syntax errors.
- **Gitguardian: **Gitguardian is a powerful tool for scanning source code to search for sensitive information such as passwords or access keys. This ensures that no critical information leaks in the source code.
- **Discord Notifications:** The Discord system is used to create a notification channel for the entire team. Everyone receives notifications about new changes in the source code, including commits and pull requests.

</p>

### CI Process:

<p align="justify">

After these services have checked and processed the source code, GitHub Actions proceeds with the CI process. In this phase, we perform the following steps:

- **Building and Packaging:** The source code is built, and corresponding containerized applications are created. This includes compiling the code, gathering necessary resources, and packaging them into containers.
- **Pushing to GitHub Container Registry:** The created containers are pushed to the GitHub Container Registry. This establishes a container image repository for future deployments.
- **Deployment to Render and Azure App Service:** The containerized applications are deployed to the Render and Azure App Service platforms, providing a runtime environment for testing before deploying to the production environment.

</p>

## Continuous Deployment (CD) Workflow

### Using ArgoCD:

<p align="justify">
To manage the deployment process, we utilize ArgoCD. ArgoCD is a Kubernetes deployment management tool that automates the deployment of applications to the production environment.
</p>

### Utilizing Helm Charts:

<p align="justify">

We use Helm Charts to package the application and create deployment manifests based on specific parameters. Helm Charts provide a convenient way to manage the deployment of applications on Kubernetes. ArgoCD leverages these Helm Charts to execute the application deployment on the Kubernetes environment.

This workflow ensures that the code is continuously tested and deployed automatically and securely. We employ professional tools and services to ensure security and performance of the application while providing a notification channel for the entire team to monitor the development and deployment process.

</p>
