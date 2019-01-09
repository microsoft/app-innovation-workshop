using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

namespace ContosoMaintenance.WebAPI.Services.StorageQueue
{
    public class AzureStorageQueue : IAzureStorageQueue
    {
        AzureStorageQueueSettings settings;

        public AzureStorageQueue(AzureStorageQueueSettings settings)
        {
            this.settings = settings;
        }

        public async Task AddMessage(string message)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials(settings.StorageAccount, settings.StorageKey), true);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(settings.QueueName);
            await queue.CreateIfNotExistsAsync();

            CloudQueueMessage queueMessage = new CloudQueueMessage(message);
            await queue.AddMessageAsync(queueMessage);
        }
    }
}
