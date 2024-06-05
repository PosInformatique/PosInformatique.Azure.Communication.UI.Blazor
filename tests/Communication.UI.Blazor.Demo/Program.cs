//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Demo
{
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            using (var httpClient = new HttpClient())
            using (var jsonStream = await httpClient.GetStreamAsync(new Uri(builder.HostEnvironment.BaseAddress + "/appsettings.json")))
            {
                // usage after the Build(), but it works
                builder.Configuration.AddJsonStream(jsonStream);
            }

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IdentityManager>();
            builder.Services.Configure<IdentityManagerOptions>(opt =>
            {
                opt.ConnectionString = builder.Configuration["ACS_CONNECTION_STRING"]!;
            });

            await builder.Build().RunAsync();
        }
    }
}
