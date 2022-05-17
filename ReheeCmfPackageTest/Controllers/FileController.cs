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
    public async Task<IActionResult> Index()
    {
      var file = Request.Form.Files.FirstOrDefault();
      var sm = new MemoryStream();
      await file.CopyToAsync(sm);
      sm.Seek(0, SeekOrigin.Begin);
      using (var image = new MagickImage(sm))
      {
        image.SetCompression(CompressionMethod.JPEG);
        image.Quality = 50;

        var wm = new MemoryStream();
        image.Write(wm, MagickFormat.Jpg);
        wm.Seek(0, SeekOrigin.Begin);
        await fs.UploadFile(new FileServiceRequest()
        {
          FileName = Guid.NewGuid() + ".jpg",
          FileUUID = Guid.NewGuid() + ".jpg",
          Content = wm
        }, null);
      }

      return Content("");
    }
  }
}
