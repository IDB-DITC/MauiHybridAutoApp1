using MauiHybridAutoApp1.Shared.Services;
using MauiHybridAutoApp1.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Utilities;

namespace MauiHybridAutoApp1.Web.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add device-specific services used by the MauiHybridAutoApp1.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddScoped<ILocalStorage, ClientLocalStorage>();
            await builder.Build().RunAsync();
        }
    }
}
