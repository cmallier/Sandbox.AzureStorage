namespace Sandbox.AzureStorage.Web.Models;

public interface IStorage
{
    Task Save( Stream fileStream, string name );
    Task<IEnumerable<string>> GetNamesAsync();
    Task<Stream> Load( string name );
}