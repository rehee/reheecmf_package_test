using Cruds;
using Microsoft.AspNetCore.Mvc;
using Reflects;

namespace ReheeCmfPackageTest.Controllers
{
  public class HealthCheckController : Controller
  {
    private readonly IContextFactory factory;

    public HealthCheckController(IContextFactory factory)
    {
      this.factory = factory;
    }
    public IActionResult Index()
    {
      foreach (var t in ReflectPool.EntityMapping.Select(b => b.Key))
      {
        t.GetType().ThisType().GetMap();
      }
      var result = factory.HealthCheckFunc();
      return Ok(result.Content);
    }
  }
}
