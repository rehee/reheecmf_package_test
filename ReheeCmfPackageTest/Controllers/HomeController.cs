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
        max = first.timeMs,
        maxLine = first.line,
        result = result.OrderByDescending(b => b.timeMs),

      });
    }
    public async Task<IActionResult> TTT2()
    {
      var c = new Requesta(options.Value);
      var url = $"{options.Value.EntityQueryUri}/api/data/read/healthcheck?$orderby=checkdate desc&$top=1&$count=true";
      //var url = "https://localhost:5001/Api/Data/Read/EntityInput?$top=1&$count=true";
      //var url = "https://reheeaddon.herokuapp.com/home/ttt2";
      //var url = "https://reheecmfwin.azurewebsites.net/home/ttt2";
      //var url = "https://reheecmfwin.azurewebsites.net/api/data/read/healthcheck";
      //var url = "https://reheecmfcode.azurewebsites.net/home/ttt2";
      //var url = "https://reheecmf.azurewebsites.net/home/ttt2";

      var result = await c.TryQuery(db, url);
      var total = result.Sum(b => b.timeMs);
      var first = result.OrderByDescending(b => b.timeMs).FirstOrDefault();
      return Ok(new
      {
        total = total,
        avg = total / result.Length,
        max = first.timeMs,
        maxLine = first.line,
        url = url,
        result = result.OrderByDescending(b => b.timeMs)
      });
    }
    public async Task<IActionResult> TTT3(string id)
    {
      var c = new Requesta(options.Value);
      var url = id;
      var result = await c.TryQuery(db, url);
      var total = result.Sum(b => b.timeMs);
      var first = result.OrderByDescending(b => b.timeMs).FirstOrDefault();
      return Ok(new
      {
        total = total,
        avg = total / result.Length,
        max = first.timeMs,
        maxLine = first.line,
        url = url,
        result = result.OrderByDescending(b => b.timeMs)
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
        Thread.Sleep(100);
      }
      return result.ToArray();
    }
    public async Task<checkResult[]> TryQuery(IContext db, string url, bool isSumamry = false)
    {
      await Task.CompletedTask;
      var result = new List<checkResult>();
      //var dbset = db.Create<>
      
      for (var i = 0; i < 100; i++)
      {
        var dic = new Dictionary<string, string>();
        var json = JsonConvert.SerializeObject(dic);
        var start = DateTime.Now;
        var r = await Request(GetHttpClient(), HttpMethod.Get, url);
        var end = DateTime.Now;



        result.Add(new checkResult() { line = i, timeMs = (int)(end - start).TotalMilliseconds });
        //Thread.Sleep(100);
        if (!isSumamry)
        {
          Console.WriteLine($"line {i} spend {(end - start).TotalMilliseconds} ms and request is {r.Success}");
        }
        else
        {
          int moreThen500 = 0;
          var r2 = JsonConvert.DeserializeObject<checkResultsummary>(r.Content);
          Console.WriteLine($"line {i} max {r2.max} in {r2.maxLine} avg {r2.avg}");
          var over500 = r2.result.Where(b => b.timeMs > 500).ToArray();
          if (over500.Length > 0)
          {
            Console.WriteLine($"line {over500.Length} over 500 is{ JsonConvert.SerializeObject(over500)}");
            moreThen500 = moreThen500 + over500.Length;
          }
          Console.WriteLine($"totel {moreThen500} lines more tham 500ms");

        }


      }
      
      return result.ToArray();
    }
    protected override HttpClient GetHttpClient(string name = null)
    {
      return new HttpClient();
    }
  }
  public class checkResultsummary
  {
    public double total { get; set; }
    public double avg { get; set; }
    public double max { get; set; }
    public double maxLine { get; set; }
    public checkResult[] result { get; set; }
  }
  public class checkResult
  {
    public int line { get; set; }
    public int timeMs { get; set; }
  }
}