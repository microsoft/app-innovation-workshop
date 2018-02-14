using System;

namespace ContosoMaintenance.WebAPI.Services.StorageQueue
{
    public class AzureStorageQueueSettings
    {
        public string StorageAccount { get; }
        public string StorageKey { get; }
        public string QueueName { get; }

        public AzureStorageQueueSettings(string storageAccount, string storageKey, string queueName)
        {
            StorageAccount = storageAccount;
            StorageKey = storageKey;
            QueueName = queueName;
        }
    }
}