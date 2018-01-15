using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Xamarin.Forms;

//Created by Michael Sivers 
namespace ContosoFieldService.Services
{
    /// <summary>
    /// Azure Storage functions for getting and uploading blob content.
    /// This includes a UploadBlobInBlocksAsync method that uploads the blob in blocks and publishes the percentage completed via the UploadStatusMessage class.
    /// </summary>
    public static class AzureStorageService
    {
        /// <summary>
        /// Gets the Azure Storage container and is used by all other operations. Azure Storage connection and container name are defined as constants in the Settings static class.
        /// </summary>
        /// <returns>Azure Storage container.</returns>
        static CloudBlobContainer GetContainer()
        {
            // TODO: This should be set up for reuse. Not sure about expiry though?
            var account = CloudStorageAccount.Parse(Settings.StorageConnection);
            var client = account.CreateCloudBlobClient();
            return client.GetContainerReference(Settings.StorageContainerName);
        }

        /// <summary>
        /// Gets a list of all blobs in the Azure Storage container.
        /// </summary>
        /// <returns>The blobs list (List<string>) as a IList<string>.</returns>
        public static async Task<IList<string>> GetBlobsListAsync()
        {
            var container = GetContainer();

            var allBlobsList = new List<string>();
            BlobContinuationToken token = null;

            do
            {
                var result = await container.ListBlobsSegmentedAsync(token);
                if (result.Results.Count() > 0)
                {
                    var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
                    allBlobsList.AddRange(blobs);
                }
                token = result.ContinuationToken;
            } while (token != null);

            return allBlobsList;
        }

        /// <summary>
        /// Gets an individual blob from Azure Storage.
        /// </summary>
        /// <returns>The blob as a byte array.</returns>
        /// <param name="blobname">Blobname to get.</param>
        public static async Task<byte[]> GetBlobAsync(string blobname)
        {
            var container = GetContainer();

            var blob = container.GetBlobReference(blobname);
            if (await blob.ExistsAsync())
            {
                await blob.FetchAttributesAsync();
                byte[] blobBytes = new byte[blob.Properties.Length];

                await blob.DownloadToByteArrayAsync(blobBytes, 0);
                return blobBytes;
            }
            return null;
        }

        /// <summary>
        /// Uploads a blob (stream) to Azure Storage. The stream is uploaded in a single block, if you want to report the upload percentage then consider using UploadBlobInBlocksAsync.
        /// Guid is used for blobname.
        /// </summary>
        /// <returns>The blob in blocks async.</returns>
        /// <param name="stream">System.IO.Stream to upload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<string> UploadBlobAsync(Stream stream, CancellationToken cancellationToken)
        {
            var container = GetContainer();
            await container.CreateIfNotExistsAsync();

            var blobname = Guid.NewGuid().ToString();
            var blobBlob = container.GetBlockBlobReference(blobname);

            await blobBlob.UploadFromStreamAsync(stream, stream.Length, null, null, null, cancellationToken);

            return blobname;
        }

        /// <summary>
        /// Uploads a blobl (stream) to Azure Storage in blocks and publishes upload status, including percentage completed, via UploadStatusMessage.
        /// BlockSize is set to 256k.
        /// </summary>
        /// <returns>The blob in blocks async.</returns>
        /// <param name="stream">System.IO.Stream to upload.</param>
        /// <param name="blobname">Blobname for saving in Azure Storage. If null, empty or whitespace then a Guid is used.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<string> UploadBlobInBlocksAsync(Stream stream, string blobname = null, CancellationToken cancellationToken = new CancellationToken())
        {
            return await UploadBlobInBlocksAsync(stream, 256 * 1024, blobname, cancellationToken); // 256k default to block size.
        }

        /// <summary>
        /// Uploads a blob (stream) to Azure Storage in blocks and publishes upload status, including percentage completed, via UploadStatusMessage.
        /// </summary>
        /// <returns>The blob in blocks async.</returns>
        /// <param name="stream">System.IO.Stream to upload.</param>
        /// <param name="blockSize">Block size. Recommended to be 256k or above for larger blobs such as videos. If 0 is specified then 256k will be used, lowest permitted is 1k.</param>
        /// <param name="blobname">Blobname for saving in Azure Storage. If null, empty or whitespace then a Guid is used.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public static async Task<string> UploadBlobInBlocksAsync(Stream stream, int blockSize, string blobname = null, CancellationToken cancellationToken = new CancellationToken())
        {
            var container = GetContainer();
            await container.CreateIfNotExistsAsync();

            if (blockSize < 1)
                blockSize = 256 * 1024;
            else if (blockSize < 1024)
                blockSize = 1024;

            if (string.IsNullOrWhiteSpace(blobname))
                blobname = Guid.NewGuid().ToString();

            var blob = container.GetBlockBlobReference(blobname);

            blob.StreamWriteSizeInBytes = blockSize;
            long bytesToUpload = stream.Length;
            long blobSize = bytesToUpload;

            List<string> blockIds = new List<string>();
            int index = 1;
            long startPosition = 0;
            long bytesUploaded = 0;

            do
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    MessagingCenter.Send<UploadStatusMessage>(new UploadStatusMessage() { Percentage = 0, Status = UploadStatus.Cancelled }, "UploadStatus");
                    return null;
                }

                var bytesToRead = Math.Min(blockSize, bytesToUpload);
                var blobContents = new byte[bytesToRead];

                stream.Position = startPosition;
                stream.Read(blobContents, 0, (int)bytesToRead);

                ManualResetEvent manualResetEvent = new ManualResetEvent(false);

                var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(index.ToString("d6")));
                blockIds.Add(blockId);

                var putBlockTask = blob.PutBlockAsync(blockId, new MemoryStream(blobContents), null);
                await putBlockTask.ContinueWith(t =>
                {
                    bytesUploaded += bytesToRead;
                    bytesToUpload -= bytesToRead;
                    startPosition += bytesToRead;
                    index++;

                    double percentComplete = (double)bytesUploaded / (double)blobSize;
                    MessagingCenter.Send<UploadStatusMessage>(new UploadStatusMessage() { Percentage = percentComplete, Status = UploadStatus.InProgress }, "UploadStatus");

                    manualResetEvent.Set();
                });
                manualResetEvent.WaitOne();
            }
            while (bytesToUpload > 0);

            var putBlockListTask = blob.PutBlockListAsync(blockIds).ContinueWith(t =>
            {
                MessagingCenter.Send<UploadStatusMessage>(new UploadStatusMessage() { Percentage = 100, Status = UploadStatus.Completed }, "UploadStatus");
            });

            return blobname;
        }

        /// <summary>
        /// Deletes a blob from Azure Storage.
        /// </summary>
        /// <returns>Boolean to denote success.</returns>
        /// <param name="blobname">Blobname of blob to delete.</param>
        public static async Task<bool> DeleteBlobAsync(string blobname)
        {
            var container = GetContainer();
            var blob = container.GetBlobReference(blobname);
            return await blob.DeleteIfExistsAsync();
        }

        /// <summary>
        /// Deletes the container from Azure Storage.
        /// </summary>
        /// <returns>The container async.</returns>
        public static async Task<bool> DeleteContainerAsync()
        {
            var container = GetContainer();
            return await container.DeleteIfExistsAsync();
        }
    }
}
