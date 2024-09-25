# Azure Function App - HTTP Trigger with Azure Table Storage

## Overview

This repository contains an Azure Function App that utilizes HTTP triggers to interact with Azure Table Storage. The application allows storing and retrieving data via API endpoints. The project is designed to demonstrate basic cloud-based serverless function deployment with error handling, logging, and efficient data storage.

## Features

- **HTTP Trigger**: The function responds to HTTP requests (GET, POST) to handle CRUD operations on product data.
- **Azure Table Storage**: The function stores and retrieves product information using Azure Table Storage.
- **Error Handling**: Implements proper HTTP status code responses (e.g., `200 OK`, `400 Bad Request`, `404 Not Found`, `500 Internal Server Error`).
- **Scalable**: The function can be deployed to Azure and scaled dynamically as needed.
  
## Technologies Used

- **.NET 6**: Cross-platform framework for building modern cloud-based applications.
- **Azure Functions**: Serverless compute service for running event-driven functions.
- **Azure Table Storage**: NoSQL key-value store for semi-structured data.
- **Azure SDK for .NET**: SDK for interacting with Azure services like Table Storage.
- **Visual Studio Code**: Recommended IDE for editing, debugging, and deploying the function.

## Build Requirements

- [TargetFramework: net6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Azure Storage Emulator or Azurite](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio) (for local testing)

## Local Development Environment Setup

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/your-repo-name.git
    cd your-repo-name
    ```

2. **Install Azure Functions Core Tools**:
    - If you haven’t already installed Azure Functions Core Tools, follow the [installation guide](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local).

3. **Start the Azure Storage Emulator**:
    - You can use [Azurite](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio) to emulate Azure Storage for local development.

4. **Configure local settings**:
    - Update `local.settings.json` to point to the local Azure Storage emulator or Azurite.

## Build Steps

To clean and build the project, run the following commands:

```bash
dotnet clean
dotnet build
