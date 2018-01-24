using System;
namespace ContosoMaintenance.WebAPI.Services.BlobStorage
{
    public class AzureBlobSettings
    {
        public AzureBlobSettings(string storageAccount,
                                       string storageKey,
                                       string containerName)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("StorageKey");

            if (string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException("ContainerName");

            this.StorageAccount = storageAccount;
            this.StorageKey = storageKey;
            this.ContainerName = containerName;
        }

        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string ContainerName { get; }
    }
}
