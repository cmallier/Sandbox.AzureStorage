using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;


namespace Sandbox.AzureStorage.Web.Models;

internal class BlobStorage : IStorage
{
    // Fields
    private readonly AzureStorageConfig _azureStorageConfig;


    // Constructor
    public BlobStorage( IOptions<AzureStorageConfig> azureStorageConfig )
    {
        _azureStorageConfig = azureStorageConfig.Value;
    }

    // Public methods
    public Task Initialize()
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient( _azureStorageConfig.ConnectionString );
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient( _azureStorageConfig.FileContainerName );

        return containerClient.CreateIfNotExistsAsync();
    }


    // Implemented methods
    public async Task<IEnumerable<string>> GetNamesAsync()
    {
        var names = new List<string>();

        BlobServiceClient blobServiceClient = new BlobServiceClient( _azureStorageConfig.ConnectionString );
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient( _azureStorageConfig.FileContainerName );

        AsyncPageable<BlobItem> blobs = containerClient.GetBlobsAsync();

        await foreach( BlobItem blob in blobs )
        {
            names.Add( blob.Name );
        }

        return names;
    }

    public Task<Stream> Load( string name )
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient( _azureStorageConfig.ConnectionString );
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient( _azureStorageConfig.FileContainerName );

        // Get a client to operate on the blob so we can read it.
        BlobClient blobClient = containerClient.GetBlobClient( name );

        return blobClient.OpenReadAsync();
    }

    public Task Save( Stream fileStream, string name )
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient( _azureStorageConfig.ConnectionString );
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient( _azureStorageConfig.FileContainerName );

        // Get the Blob Client used to interact with (including create) the blob
        BlobClient blobClient = containerClient.GetBlobClient( name );

        // Upload the blob
        return blobClient.UploadAsync( fileStream );

    }
}
