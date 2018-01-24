using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;

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
                return BadRequest();
            
            if (file.Length == 0)
                return BadRequest();

            try
            {
                //We'll store this into Blob Storage. 
                var blobName = file.FileName;
                var fileStream = file.OpenReadStream();
                blobName = string.Format($"{blobName}");
                await blobStorage.UploadAsync(blobName, fileStream);

                //Create a message on our queue for the Azure Function to process the image. 


                return new ObjectResult(true);
            }
            catch
            {
                return new ObjectResult(false);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Download(string blobName, string name)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            var stream = await blobStorage.DownloadAsync(blobName);
            return File(stream.ToArray(), "applicationt/octet-stream", name);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string blobName)
        {
            if (string.IsNullOrEmpty(blobName))
                return Content("Blob Name not present");

            await blobStorage.DeleteAsync(blobName);

            return RedirectToAction("Index");
        }
    }
}
