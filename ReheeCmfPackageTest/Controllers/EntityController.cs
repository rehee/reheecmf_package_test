using Cruds;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.Base.Entities;
using ReheeCmf.Server.Controllers;
using ReheeCmfPackageTest.Data;

namespace ReheeCmfPackageTest.Controllers
{
  [ApiController]
  [Route("Api/Entity")]
  public class EntityController : ReheeCmfController
  {
    ApplicationDbContext db;
    public EntityController(IContext db, IJWTService jwt) : base(db, jwt)
    {
      if (db is ApplicationDbContext db2)
      {
        this.db = db2;
      }
    }
    [HttpGet("Url")]
    public IActionResult GetObj()
    {
      return Ok(db.HealthChecks.AsNoTracking().OrderByDescending(b => b.CheckDate).FirstOrDefault());
    }
    [HttpGet("UrlTask")]
    public Task<object> GetObjTask()
    {
      var task = new Task<object>(() => db.HealthChecks.AsNoTracking().OrderByDescending(b => b.CheckDate).FirstOrDefault());
      task.Start();
      return task;
    }
    [HttpGet("Odata")]
    [EnableQuery]
    public IEnumerable<HealthCheck> GetOdata()
    {
      return db.HealthChecks.AsNoTracking();
    }
    [HttpGet("OdataTask")]
    [EnableQuery]
    public Task<IEnumerable<HealthCheck>> GetOdataTask()
    {
      var task = new Task<IEnumerable<HealthCheck>>(() => db.HealthChecks.AsNoTracking()); ;
      task.Start();
      return task;
    }
  }
}
