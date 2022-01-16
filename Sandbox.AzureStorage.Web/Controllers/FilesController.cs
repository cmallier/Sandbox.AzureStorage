using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Sandbox.AzureStorage.Web.Models;

namespace Sandbox.AzureStorage.Web.Controllers;

[Route("api/[controller]")]
public class FilesController : Controller
{
    // Fields
    private const int MaxFilenameLength = 50;
    private static readonly Regex _filenameRegex = new Regex( "[^a-zA-Z0-9._]" );
    private readonly IStorage _storage;


    // Constructor
    public FilesController( IStorage storage )
    {
        _storage = storage;
    }

    [HttpGet()]
    public async Task<IActionResult> Index()
    {
        var names = await _storage.GetNamesAsync();

        var baseUrl = Request.Path.Value;

        var urls = names.Select( name => $"{baseUrl}/{name}" );

        return Ok( urls );
    }

    
    [HttpPost()]
    public async Task<IActionResult> Upload( IFormFile file )
    {
        var sanitizedFileName =  SanitizeFilename( file.FileName );


        if( String.IsNullOrEmpty( sanitizedFileName ) )
        {
            throw new ArgumentException();
        }


        using( Stream stream = file.OpenReadStream() )
        {
            await _storage.Save( stream, sanitizedFileName );
        }

        return Accepted();
    }

    [HttpGet("{filename}")]
    public async Task<IActionResult> Download( string filename )
    {
        Stream stream = await _storage.Load( filename );

        return File( stream, "application/octet-stream", filename );
    }


    // Private methods
    private static string SanitizeFilename( string fileName )
    {
        string sanitizedFilename = _filenameRegex.Replace( fileName, "" ).TrimEnd( '.' );

        if( sanitizedFilename.Length > MaxFilenameLength )
        {
            sanitizedFilename = sanitizedFilename.Substring( 0, MaxFilenameLength );
        }

        return sanitizedFilename;
    }
}
