using Inject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReheeCmfPackageTest.Data;
namespace ReheeCmfPackageTest
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();
      PropertyInject.Provider = host.Services;
      using (var scope = host.Services.CreateScope())
      {
        using (var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
        {

          db.Database.Migrate();
        }
      }
      host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
              webBuilder.UseStartup<Startup>();
            });
  }
}
