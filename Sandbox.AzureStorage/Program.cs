using Microsoft.Extensions.Configuration;


namespace Sandbox.AzureStorage
{
    class Program
    {
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath( Directory.GetCurrentDirectory() )
                .AddJsonFile( "appsettings.json" );

            var configuration = builder.Build();


            var blobApp = new AzureStorageBlobApp( configuration );
            blobApp.Start();
        }
    }
}