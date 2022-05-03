using Authenticates;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReheeCmfPackageTest.Models;
using Requests;
using System.Diagnostics;

namespace ReheeCmfPackageTest.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration configuration;


    public HomeController(ILogger<HomeController> logger, IConfiguration Configuration)
    {
      _logger = logger;
      configuration = Configuration;
    }

    public IActionResult Index()
    {
      return View();
    }
    public async Task<IActionResult> TTT()
    {
      var c = new Requesta();
      await c.TryAdding();
      return View();
    }

    public IActionResult Options()
    {
      return StatusCode(200, configuration.GetOption<UserManagementOption>());
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
    public async Task TryAdding()
    {
      await Task.CompletedTask;
      //for (var i = 0; i < int.MaxValue; i++)
      //{
      //  var dic = new Dictionary<string, string>();
      //  var json = JsonConvert.SerializeObject(dic);
      //  var start = DateTime.Now;
      //  await Request(GetHttpClient(), HttpMethod.Post, "https://reheecmf.azurewebsites.net/Api/Data/EntityInput/-1", json);
      //  var end = DateTime.Now;

      //  Console.WriteLine($"line {i} processed {(end - start).TotalMilliseconds} total ms");
      //}
    }
    protected override HttpClient GetHttpClient(string name = null)
    {
      return new HttpClient();
    }
  }
}