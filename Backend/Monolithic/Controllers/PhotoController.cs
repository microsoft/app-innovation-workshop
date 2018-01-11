using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ContosoMaintenance.WebAPI.Controllers
{
    [Route("/api/photo")]
    public class PhotoController : Controller
    {
        readonly IAzureBlobStorage blobStorage;

        public PhotoController(IAzureBlobStorage blobStorage)
        {
            this.blobStorage = blobStorage;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null)
                return Content("Argument null");

            if (file.Length == 0)
                return Content("file not selected");

            var blobName = file.FileName;
            var fileStream = file.OpenReadStream();
            blobName = string.Format(@"{0}\{1}", blobName, DateTime.Now);

            await blobStorage.UploadAsync(blobName, fileStream);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string blobName, string name)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            var stream = await blobStorage.DownloadAsync(blobName);
            return File(stream.ToArray(), "application/octet-stream", name);
        }

        public async Task<IActionResult> Delete(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            await blobStorage.DeleteAsync(blobName);

            return RedirectToAction("Index");
        }
    }
}
