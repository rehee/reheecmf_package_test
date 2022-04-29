using Authenticates;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.OData;
using ODataControllers;
using ReheeCmf.API;
using ReheeCmfPackageTest.Data;

namespace ReheeCmfPackageTest
{
  public class Startup
  {
    public IConfiguration Configuration { get; set; }
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<FormOptions>(options =>
      {
        options.MultipartBodyLengthLimit = long.MaxValue;
      });
      services.DefaultApiSetup<ApplicationDbContext, MyUser, RegisterDTO>(Configuration);



      services.AddCors();

      //services.AddingHttpClient(Configuration, "EncrytEndpoints");
      //services.AddSingleton<ICryptService, CryptService>(sp =>
      //  new CryptService(() => sp.GetService<IHttpClientFactory>().CreateClient("EncrytEndpoints"))
      //);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider sp)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        //app.UseReverseProxyHttpsEnforcer();
      }
      //app.Use(async (context, next) =>
      //{
      //  if (context.Request.Scheme.ToLower() != "https")
      //  {
      //    context.Response.Redirect($"https://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}");
      //  }
      //  await next.Invoke();
      //});

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCors(cors => cors
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        );
      app.UseStatusCodePages(async context =>
      {
        await Task.CompletedTask;

        if (context.HttpContext.Response.StatusCode == 401)
        {
          //context.HttpContext.Response.Redirect("/");
        }
        if (context.HttpContext.Response.StatusCode == 404)
        {
          context.HttpContext.Response.Redirect("/");
        }
      });

      app.UseODataRouteDebug();

      app.UseODataQueryRequest();

      // Add the OData Batch middleware to support OData $Batch
      app.UseODataBatching();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();


      app.UseEndpoints(endpoints =>
      {
        endpoints.ODataController(sp);
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();

      });
    }
  }
}
