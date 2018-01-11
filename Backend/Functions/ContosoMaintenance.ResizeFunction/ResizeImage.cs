using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ContosoMaintenance.Functions
{
    public static class ResizeImage
    {
        [FunctionName("ResizeImage")]
        public static void Run(
            // Trigger
            [CosmosDBTrigger("contosomaintenance", "jobs", ConnectionStringSetting = "contosomaintenancedb_connectionstring", CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> documents,

            // Input
            [Blob("images/large", FileAccess.Read)] CloudBlobDirectory imagesLarge,

            // Output
            [Blob("images/medium", FileAccess.Write)] CloudBlobDirectory imagesMedium,
            [Blob("images/small", FileAccess.Write)] CloudBlobDirectory imagesSmall,

            // Logger
            TraceWriter log)
        {
            log.Info("Documents modified " + documents.Count);
            log.Info("First document Id " + documents[0].Id);

            foreach (var document in documents)
            {
                // Parse document to Job

                // Check for attachements

                // Check if resizing is needed

                // Send image, that need resizing to Cognitive Services Computer Vision

                // Save images to according folder

                // Update Job

            }
        }
    }
}
