using Cruds;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Reflects;
using ReheeCmf.Base.Entities;
using ReheeCmf.DBContext.Caches;
using ReheeCmf.JWT;
using ReheeCmf.Server.Controllers;
using ReheeCmfPackageTest.Data;
using ReheeCmfPackageTest.Entities;

namespace ReheeCmfPackageTest.Controllers
{
  public class HealthCheckController : ReheeCmfController
  {
    private readonly IContextFactory factory;

    private readonly ApplicationDbContext db2;
    private readonly ReheeCmfContextCache mc;

    public HealthCheckController(IContextFactory factory, ApplicationDbContext db2, IJWTService jwt,ReheeCmfContextCache mc) : base(db2, jwt)
    {
      this.factory = factory;

      this.db2 = db2;
      this.mc = mc;
    }
    public IActionResult Index()
    {
      var result = factory.HealthCheckFunc();
      mc.CleanExpiredCache();
      return Ok(result.Content);
    }
  }
}
