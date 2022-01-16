using Azure.Storage.Blobs;

using Microsoft.Extensions.Configuration;

namespace Sandbox.AzureStorage;

internal class AzureStorageBlobApp
{
    private readonly IConfiguration _config;
    private const string containerName = "container-name";

    public AzureStorageBlobApp( IConfigurationRoot configuration )
    {
        _config = configuration;
    }

    public void Start()
    {
        var connectionString = _config.GetConnectionString( "StorageAccount" );

        // Container
        BlobContainerClient container = new BlobContainerClient( connectionString, containerName );
        container.CreateIfNotExists();


        // Upload
        string blobName = "docs-and-friends-selfie-stick";
        string fileName = @"C:\Github\Sandbox.AzureStorage\Sandbox.AzureStorage\Assets\docs-and-friends-selfie-stick.png";

        BlobClient blobClient = container.GetBlobClient( blobName );
        blobClient.Upload( fileName, true );
        // blobClient.Delete...
        // blobClient.Download...
        // blobClient.Undelete...


        // List
        var blobs = container.GetBlobs();
        foreach( var blob in blobs )
        {
            Console.WriteLine( $"{blob.Name} --> Created On: {blob.Properties.CreatedOn:yyyy-MM-dd HH:mm:ss}  Size: {blob.Properties.ContentLength}" );
        }
    }
}
