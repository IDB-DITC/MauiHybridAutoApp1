using MauiHybridAutoApp1.Shared.Services;
using MauiHybridAutoApp1.Web.Components;
using MauiHybridAutoApp1.Web.Services;
using Utilities;

namespace MauiHybridAutoApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            // Add device-specific services used by the MauiHybridAutoApp1.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            //server side app
            builder.Services.AddDataProtection();
            builder.Services.AddScoped<ILocalStorage, ServerLocalStorage>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(
                    typeof(MauiHybridAutoApp1.Shared._Imports).Assembly,
                    typeof(MauiHybridAutoApp1.Web.Client._Imports).Assembly);

            app.Run();
        }
    }
}
