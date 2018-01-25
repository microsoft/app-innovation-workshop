using System;

namespace ContosoMaintenance.WebAPI.Services.StorageQueue
{
    public class AzureStorageQueueSetings
    {
        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string QueueName { get; }
        public AzureStorageQueueSetings(string storageAccount, string storageKey, string queueName)
        {
            if (string.IsNullOrEmpty(storageAccount))
                throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey))
                throw new ArgumentNullException("StorageKey");

            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException("processphotos");

            StorageAccount = storageAccount;
            StorageKey = storageKey;
            QueueName = queueName;
        }
    }
}
