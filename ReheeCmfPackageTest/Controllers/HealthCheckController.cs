using Cruds;
using Microsoft.AspNetCore.Mvc;

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
      var result = factory.HealthCheckFunc();
      return Ok(result.Content);
    }
  }
}
