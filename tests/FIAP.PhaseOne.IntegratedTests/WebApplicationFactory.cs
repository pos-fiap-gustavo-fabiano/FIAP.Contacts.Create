using FIAP.Contacts.Create.Infra.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FIAP.Contacts.Create.IntegratedTests
{
    public class WebApplicationFactory : WebApplicationFactory<Program>
    {
        public ApplicationDbContext Context { get; }
        private IServiceScope scope;
        public WebApplicationFactory()
        {
            scope = Services.CreateScope();
            Context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof
                    (DbContextOptions<ApplicationDbContext>));
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql("Host=ep-frosty-star-a5yudl6r.us-east-2.aws.neon.tech;Port=5432;Database=phase-one-test;Username=phase-one_owner;Password=ZwLjAxC0RnS5");
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    {
                        var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
                        db.Database.Migrate();
                    }
                }
            });
            base.ConfigureWebHost(builder);
        }
    }
}
