using Authenticates;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.OData;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;
using ODataControllers;
using ReheeCmf.API;
using ReheeCmf.Base.Entities;
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
      services.DefaultApiSetup<ApplicationDbContext, MyUser, RegisterDTO>(Configuration,
        additionalOdataRouter: new (string, Action<Microsoft.OData.ModelBuilder.ODataConventionModelBuilder>)[]
        {
          ("Api/EF",builder=> builder.EntitySet<HealthCheck>(nameof(HealthCheck)).EntityType.HasKey(b => b.Id))
        });



      services.AddCors();

      //services.AddingHttpClient(Configuration, "EncrytEndpoints");
      //services.AddSingleton<ICryptService, CryptService>(sp =>
      //  new CryptService(() => sp.GetService<IHttpClientFactory>().CreateClient("EncrytEndpoints"))
      //);
      services
        .AddSwaggerGen(c =>
        {
          c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
          {
            Title = "Cmf Api",
            Version = "v1",
          });
          c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
          {
            Description = @"JWT Authorization header using the Bearer scheme. <br>
                      Enter 'Bearer' [space] and then your token in the text input below. <br>
                      Example: 'Bearer 12345abcdef'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
          });
          c.AddSecurityRequirement(new OpenApiSecurityRequirement()
          {
            {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
                {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
              },
              new List<string>()
            }
          });
        });
      services.AddOdataSwaggerSupport();
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

      //app.UseODataRouteDebug();

      //app.UseODataQueryRequest();

      // Add the OData Batch middleware to support OData $Batch
      //app.UseODataBatching();

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "cmf api v1"));

      app.UseEndpoints(endpoints =>
      {
        endpoints.ODataController(sp);
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action}/{id?}");
        endpoints.MapRazorPages();
        endpoints.MapFallbackToFile("/index.html");
      });
    }
  }
}
