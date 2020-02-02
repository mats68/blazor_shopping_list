using Blazored.LocalStorage;
using Einkaufsliste.Components.ListExt;
using Einkaufsliste.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Einkaufsliste
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddSingleton<IEinkaufService, EinkaufService>();
            services.AddSingleton<ArchivService>();
            services.AddSingleton<ListServices>();

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
