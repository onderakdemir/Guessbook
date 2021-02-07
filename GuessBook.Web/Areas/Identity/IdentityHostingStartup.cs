using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(GuessBook.Web.Areas.Identity.IdentityHostingStartup))]
namespace GuessBook.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}