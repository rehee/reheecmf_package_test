using Cruds;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ReheeCmf.Base.Entities;
using ReheeCmf.Server.Controllers;

namespace ReheeCmfPackageTest.Controllers
{
  [ApiController]
  [Route("Api/EF")]
  public class EF2Controller : ReheeCmfController
  {
    public EF2Controller(IContext db, IJWTService jwt) : base(db, jwt)
    {
    }
    [EnableQuery]
    [HttpGet("json")]
    [OdataSet(nameof(HealthCheck))]
    public IEnumerable<HealthCheck> Index()
    {
      return db.Read<HealthCheck>().Content;
    }
  }
}
