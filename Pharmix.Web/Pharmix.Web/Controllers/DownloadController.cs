using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Pharmix.Web.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        string _FreshLoad = string.Empty;
        string _myChangeSetPath = string.Empty;

        private IConfiguration _configuration;

        public DownloadController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _FreshLoad = _configuration["FreshLoad"].ToString();
            _myChangeSetPath = _configuration["MyChansetZipFilePath"].ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadFromMail(string fileName, bool isFreshLoad = true)
        {
            string rootFolder = isFreshLoad == true ? _FreshLoad : _myChangeSetPath;
            TempData["filename"] = fileName;
            TempData.Keep("filename");
            return downloadFile(rootFolder);
        }
        public FileResult downloadFile(string filePath)
        {
            IFileProvider provider = new PhysicalFileProvider(filePath);
            IFileInfo fileInfo = provider.GetFileInfo(TempData["filename"].ToString());
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, TempData["filename"].ToString());
        }
    }
}