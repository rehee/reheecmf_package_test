using Cruds;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Reflects;
using ReheeCmf.Base.Entities;
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

    public HealthCheckController(IContextFactory factory, ApplicationDbContext db2, IJWTService jwt) : base(db2, jwt)
    {
      this.factory = factory;

      this.db2 = db2;
    }
    public IActionResult Index()
    {
      var result = factory.HealthCheckFunc();
      return Ok(result.Content);
    }
  }
}
