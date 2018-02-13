using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace ContosoMaintenance.WebAPI.Services
{
    public class DocumentDBRepositoryBase<T> where T : class
    {
        string CollectionId;
        string databaseId;
        DocumentClient client;

        public void Initialize(string endpoint, string key, string databaseId)
        {
            this.databaseId = databaseId;
            client = new DocumentClient(new Uri(endpoint), key, new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseId, CollectionId, id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public int GetItemsCount()
        {
            try
            {
                var document = client.CreateDocumentCollectionQuery(UriFactory.CreateDocumentCollectionUri(databaseId, CollectionId), "SELECT c.id FROM c");
                return (int)(dynamic)document.Count();
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return 0;
                }
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            CollectionId = GetCollectionName();

            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(databaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<Document> CreateItemAsync(T item)
        {
            CollectionId = GetCollectionName();
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseId, CollectionId), item);
        }

        public async Task<Document> UpdateItemAsync(string id, T item)
        {
            CollectionId = GetCollectionName();
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, CollectionId, id), item);
        }

        public async Task DeleteItemAsync(string id)
        {
            CollectionId = GetCollectionName();
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseId, CollectionId, id));
        }

        async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = databaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        async Task CreateCollectionIfNotExistsAsync()
        {
            CollectionId = GetCollectionName();

            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(databaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 400 });
                }
                else
                {
                    throw;
                }
            }
        }

        string GetCollectionName()
        {
            var name = typeof(T).Name.ToLower();
            if (name.ToCharArray().Last().ToString() != "s")
                return $"{name}s";
            return name;
        }
    }
}