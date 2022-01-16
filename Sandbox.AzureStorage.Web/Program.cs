using Sandbox.AzureStorage.Web.Models;
using Microsoft.Extensions.Options;

// Create Builder
var builder = WebApplication.CreateBuilder( args );

// builder.Configuration
// builder.Environment


// Add services to the container.
builder.Services.AddOptions();

builder.Services.Configure<AzureStorageConfig>( builder.Configuration.GetSection( "AzureStorageConfig" ) );

builder.Services.AddSingleton<IStorage>( serviceProvider => {
                    var blobStorage = new BlobStorage( serviceProvider.GetService<IOptions<AzureStorageConfig>>() );
                    blobStorage.Initialize().GetAwaiter().GetResult();
                    return blobStorage;
                } );
builder.Services.AddControllersWithViews();

var app = builder.Build();



// Configure the HTTP request pipeline.
if( !app.Environment.IsDevelopment() )
{
    app.UseExceptionHandler( "/Home/Error" );
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}" );

app.Run();
