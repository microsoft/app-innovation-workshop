using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using ContosoMaintenance.WebAPI.Services.BlobStorage;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ContosoMaintenance.WebAPI.Services
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        public AzureBlobStorage(AzureBlobSettings settings)
        {
            this.settings = settings;
        }

        public async Task<Uri> UploadAsync(string blobName, string filePath)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Upload
            using (var fileStream = File.Open(filePath, FileMode.Open))
            {
                fileStream.Position = 0;
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return blockBlob.Uri;
        }

        public async Task<Uri> UploadAsync(string blobName, Stream stream)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Upload
            stream.Position = 0;
            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob.Uri;
        }

        public async Task<MemoryStream> DownloadAsync(string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Download
            using (var stream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                return stream;
            }
        }

        public async Task DownloadAsync(string blobName, string path)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Download
            await blockBlob.DownloadToFileAsync(path, FileMode.Create);
        }

        public async Task DeleteAsync(string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Delete
            await blockBlob.DeleteAsync();
        }

        public async Task<bool> ExistsAsync(string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Exists
            return await blockBlob.ExistsAsync();
        }

        public async Task<List<AzureBlobItem>> ListAsync()
        {
            return await GetBlobListAsync();
        }

        public async Task<List<AzureBlobItem>> ListAsync(string rootFolder)
        {
            if (rootFolder == "*") return await ListAsync(); //All Blobs
            if (rootFolder == "/") rootFolder = "";          //Root Blobs

            var list = await GetBlobListAsync();
            return list.Where(i => i.Folder == rootFolder).ToList();
        }

        public async Task<List<string>> ListFoldersAsync()
        {
            var list = await GetBlobListAsync();
            return list.Where(i => !string.IsNullOrEmpty(i.Folder))
                       .Select(i => i.Folder)
                       .Distinct()
                       .OrderBy(i => i)
                       .ToList();
        }

        public async Task<List<string>> ListFoldersAsync(string rootFolder)
        {
            if (rootFolder == "*" || rootFolder == "") return await ListFoldersAsync(); //All Folders

            var list = await GetBlobListAsync();
            return list.Where(i => i.Folder.StartsWith(rootFolder))
                       .Select(i => i.Folder)
                       .Distinct()
                       .OrderBy(i => i)
                       .ToList();
        }



        readonly AzureBlobSettings settings;

        async Task<CloudBlobContainer> GetContainerAsync()
        {
            //Account
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new StorageCredentials(settings.StorageAccount, settings.StorageKey), false);

            //Client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(settings.ContainerName);
            await blobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);

            return blobContainer;
        }

        async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //Blob
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            return blockBlob;
        }

        async Task<List<AzureBlobItem>> GetBlobListAsync(bool useFlatListing = true)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //List
            var list = new List<AzureBlobItem>();
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment =
                    await blobContainer.ListBlobsSegmentedAsync("", useFlatListing, new BlobListingDetails(), null, token, null, null);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    list.Add(new AzureBlobItem(item));
                }
            } while (token != null);

            return list.OrderBy(i => i.Folder).ThenBy(i => i.Name).ToList();
        }

    }
}
