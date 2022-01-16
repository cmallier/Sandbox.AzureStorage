# Sandbox.AzureStorage


## Disclaimer

This project is done for learning purposes. It is not necessarily production quality code.


## Azure cli

```bh
> az storage account create \
  --resource-group <rg> \
  --location westus \
  --sku Standard_LRS \
  --name <name>

> az storage account show-connection-string \
  --resource-group <rg> \
  --query connectionString \
  --name <name>

> az storage container list \
--account-name <name>

> az appservice plan create --name blob-exercise-plan --resource-group learn-3478147d-6834-4459-8581-a1e741b13637 --sku FREE --location centralus

> az webapp create --name <your-unique-app-name> --plan blob-exercise-plan --resource-group learn-3478147d-6834-4459-8581-a1e741b13637

> CONNECTIONSTRING=$(az storage account show-connection-string --name <your-unique-storage-account-name> --output tsv)

> az webapp config appsettings set --name <your-unique-app-name> --resource-group learn-3478147d-6834-4459-8581-a1e741b13637 --settings AzureStorageConfig:ConnectionString=$CONNECTIONSTRING AzureStorageConfig:FileContainerName=files

```

## Rest Api

GET https://[url-for-service-account]/?comp=list&include=metadata

- Blobs
  - https://[name].blob.core.windows.net/
- Queues
  - https://[name].queue.core.windows.net/
- Table
  - https://[name].table.core.windows.net/
- Files
  - https://[name].file.core.windows.net/


## Connection

__Connection string__

DefaultEndpointsProtocol=https;AccountName={your-storage};
   AccountKey={your-access-key};
   EndpointSuffix=core.windows.net

Each storage account has two access keys. The reason for this is to allow keys to be rotated (regenerated) periodically as part of security best practice in keeping your storage account secure.

Access keys are the easiest approach to authenticating access to a storage account. However they provide full access to anything in the storage account, similar to a root password on a computer.


__Shared access signatures (SAS)__

Storage accounts offer a separate authentication mechanism called shared access signatures that support expiration and limited permissions for scenarios where you need to grant limited access. You should use this approach when you are allowing other users to read and write data to your storage account.


## Respources / Learn / Credits

- [Api](https://docs.microsoft.com/en-us/dotnet/api/overview/azure/storage)
- [Azure Storage REST API Reference](https://docs.microsoft.com/en-us/rest/api/storageservices/)
- [Grant limited access to Azure Storage resources using shared access signatures (SAS)](https://docs.microsoft.com/en-us/azure/storage/common/storage-sas-overview)
- [Manage storage account keys with Key Vault and the Azure CLI](https://docs.microsoft.com/en-us/azure/key-vault/secrets/overview-storage-keys)
- [Azure Sdk](https://github.com/Azure/azure-sdk-for-net)
- [Azure Sdk Storage](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/storage)
- [Azure Sdk Storage Blobs](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/storage/Azure.Storage.Blobs)
- [Azure Storage Client Library for JavaScript](https://github.com/Azure/azure-storage-node#azure-storage-javascript-client-library-for-browsers)
- [Index data from Azure Blob Storage using Azure Cognitive Search](https://docs.microsoft.com/en-us/azure/search/search-howto-indexing-azure-blob-storage)
- [Naming and Referencing Containers, Blobs, and Metadata](https://docs.microsoft.com/en-us/rest/api/storageservices/naming-and-referencing-containers--blobs--and-metadata)
- [Security recommendations for Blob storage](https://docs.microsoft.com/en-us/azure/storage/blobs/security-recommendations)
- [Configure an ASP.NET Core app for Azure App Service](https://docs.microsoft.com/en-us/azure/app-service/configure-language-dotnetcore?pivots=platform-windows)
