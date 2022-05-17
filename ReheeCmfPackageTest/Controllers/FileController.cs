using Files;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;

namespace ReheeCmfPackageTest.Controllers
{
  [ApiController]
  [Route("ThisFile")]
  public class FileController : Controller
  {
    private readonly IFileService fs;

    public FileController(IFileService fs)
    {
      this.fs = fs;
    }
    [HttpPost]
    [RequestSizeLimit(long.MaxValue)]
    public async Task<IActionResult> Index()
    {

      var file = Request.Form.Files.FirstOrDefault();
      var sm2 = new MemoryStream();
      await file.CopyToAsync(sm2);
      sm2.Seek(0, SeekOrigin.Begin);
      var fileExtend = file.FileName.Split('.').LastOrDefault();
      var allowFiles = new string[]
      {
        "jpg","bmp"
      };
      var needChangeName = allowFiles.Contains(fileExtend);
      await fs.UploadFile(new FileServiceRequest
      {
        FileName = needChangeName ? Guid.NewGuid() + ".jpg" : file.FileName,
        Content = sm2
      }, null);
      return Content("");

      var compressionOption = new FileCompressionOption()
      {
        MaxFileSizeKB = 100,
        AvaliableFileType = "image/gif,image/jpeg,image/png,image/bmp,image/jpeg,image/x-citrix-png,image/x-png"
      };
      //if (!compressionOption.AvaliableFileTypes.Contains(file.ContentType))
      //{
      //  return NotFound();
      //}
      if (file.Length < compressionOption.MaxFileSizeKB * 1024)
      {
        return Content("no need compression");
      }
      var sm = new MemoryStream();
      await file.CopyToAsync(sm);
      sm.Seek(0, SeekOrigin.Begin);

      //if (file.ContentType.Contains("gif", StringComparison.OrdinalIgnoreCase))
      //{
      //  using (var collection = new MagickImageCollection(sm))
      //  {
      //    collection.Coalesce();
      //    foreach (var image in collection)
      //    {
      //      image.SetCompression(CompressionMethod.JPEG);
      //      image.Quality = 50;
      //      image.Resize(200, 0);
      //    }
      //    var wm = new MemoryStream();
      //    collection.Write(wm, MagickFormat.Gif);
      //    wm.Seek(0, SeekOrigin.Begin);
      //    await fs.UploadFile(new FileServiceRequest()
      //    {
      //      FileName = Guid.NewGuid() + ".gif",
      //      FileUUID = Guid.NewGuid() + ".gif ",
      //      Content = wm
      //    }, null);
      //  }
      //  return Content("");
      //}
      if (file.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase))
      {

        using (var image = new MagickImage(sm))
        {

          image.SetCompression(CompressionMethod.JPEG);
          image.Resize(1024, 0);
          var times = file.Length / (compressionOption.MaxFileSizeKB * 1024);
          switch (times)
          {
            case 1:
              image.Quality = 95;
              break;
            case 10:
              image.Quality = 80;
              break;
            case 20:
              image.Quality = 75;
              break;
            default:
              image.Quality = 50;
              break;
          }


          var wm = new MemoryStream();
          image.Write(wm, MagickFormat.Jpeg);
          wm.Seek(0, SeekOrigin.Begin);
          await fs.UploadFile(new FileServiceRequest()
          {
            FileName = Guid.NewGuid() + ".jpg",
            FileUUID = Guid.NewGuid() + ".jpg ",
            Content = wm
          }, null);
        }
      }


      return Content("");
    }

    [HttpGet]
    public async Task<IActionResult> PostFile()
    {
      var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "sample.jpg");
      using (var files = new MagickImage(file))
      {
        var wm = new MemoryStream();
        files.Write(wm);
        wm.Seek(0, SeekOrigin.Begin);
        await fs.UploadFile(new FileServiceRequest
        {
          FileName = Guid.NewGuid() + ".jpg",
          Content = wm
        }, null);
      }
      return Content("");
    }
  }

  public class FileCompressionOption
  {
    public string AvaliableFileType { get; set; }
    public IEnumerable<string> AvaliableFileTypes => AvaliableFileType.Split(",").Select(b => b.Trim());
    public int MaxFileSizeKB { get; set; }
  }
}
