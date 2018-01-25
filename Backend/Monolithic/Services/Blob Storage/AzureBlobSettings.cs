using System;

namespace ContosoMaintenance.WebAPI.Services.BlobStorage
{
    public class AzureBlobSettings
    {
        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string ContainerName { get; }

        public AzureBlobSettings(string storageAccount, string storageKey, string containerName)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("StorageKey");

            if (string.IsNullOrEmpty(containerName))
                throw new ArgumentNullException("ContainerName");

            StorageAccount = storageAccount;
            StorageKey = storageKey;
            ContainerName = containerName;
        }
    }
}
