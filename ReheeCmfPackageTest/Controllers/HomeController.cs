using Authenticates;
using Cruds;
using JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ReheeCmf.Base.Entities;
using ReheeCmf.DBContext;
using ReheeCmf.Server.Controllers;
using ReheeCmfPackageTest.Data;
using ReheeCmfPackageTest.Entities;
using ReheeCmfPackageTest.Models;
using Requests;
using System.Diagnostics;

namespace ReheeCmfPackageTest.Controllers
{
  public class HomeController : ReheeCmfController
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IContext db;
    private readonly IOptions<CrudOption> options;
    private readonly ApplicationDbContext db2;
    private readonly UserManagementOption option;


    public HomeController(ILogger<HomeController> logger, IOptions<UserManagementOption> coptions, IContext db, IJWTService jwt, ApplicationDbContext db2, IOptions<CrudOption> options) : base(db, jwt)
    {
      _logger = logger;
      this.db = db;
      this.options = options;
    }
    public IActionResult Index()
    {



      return View();
    }
    public async Task<IActionResult> TTT()
    {
      var c = new Requesta(options.Value);
      var result = await c.TryAdding(db);
      var total = result.Sum(b => b.timeMs);
      var first = result.OrderByDescending(b => b.timeMs).FirstOrDefault();
      return Ok(new
      {
        total = total,
        avg = total / result.Length,
        result = result,
        max = first.timeMs,
        maxLine = first.line,
      });
    }
    public async Task<IActionResult> TTT2()
    {
      var c = new Requesta(options.Value);
      var result = await c.TryQuery(db);
      var total = result.Sum(b => b.timeMs);
      var first = result.OrderByDescending(b => b.timeMs).FirstOrDefault();
      return Ok(new
      {
        total = total,
        avg = total / result.Length,
        max = first.timeMs,
        maxLine = first.line,
        result = result
      });
    }
    public IActionResult Options()
    {
      return StatusCode(200, option);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }

  public class Requesta : RequestBase
  {
    public Requesta(CrudOption option)
    {
      Option = option;
    }

    public CrudOption Option { get; }

    public async Task<checkResult[]> TryAdding(IContext db)
    {
      await Task.CompletedTask;
      var result = new List<checkResult>();
      //var dbset = db.Create<>
      for (var i = 0; i < 100; i++)
      {
        var dic = new Dictionary<string, string>();
        var json = JsonConvert.SerializeObject(dic);
        var start = DateTime.Now;
        db.Create<EntityInput>(new EntityInput() { });
        //var r = await Request(GetHttpClient(), HttpMethod.Get,
        //  "https://localhost:7183/api/data/read/healthcheck?$orderby=checkdate desc&$top=1&$count=true");
        var end = DateTime.Now;
        Console.WriteLine($"line {i} spend {(end - start).TotalMilliseconds} ms and request is");
        result.Add(new checkResult() { line = i, timeMs = (int)(end - start).TotalMilliseconds }); ;
      }
      return result.ToArray();
    }
    public async Task<checkResult[]> TryQuery(IContext db)
    {
      await Task.CompletedTask;
      var result = new List<checkResult>();
      //var dbset = db.Create<>
      for (var i = 0; i < 100; i++)
      {
        var dic = new Dictionary<string, string>();
        var json = JsonConvert.SerializeObject(dic);
        var start = DateTime.Now;
        //db.Create<EntityInput>(new EntityInput() { });
        var r = await Request(GetHttpClient(), HttpMethod.Get,
          $"{Option.EntityQueryUri}/api/data/read/healthcheck?$orderby=checkdate desc&$top=1&$count=true");
        var end = DateTime.Now;
        Console.WriteLine($"line {i} spend {(end - start).TotalMilliseconds} ms and request is {r.Success}");
        result.Add(new checkResult() { line = i, timeMs = (int)(end - start).TotalMilliseconds }); ;
      }
      return result.ToArray();
    }
    protected override HttpClient GetHttpClient(string name = null)
    {
      return new HttpClient();
    }
  }
  public class checkResult
  {
    public int line { get; set; }
    public int timeMs { get; set; }
  }
}