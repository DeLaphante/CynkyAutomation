using CynkyAutomation.Configuration;
using CynkyUtilities;
using Microsoft.Azure.Cosmos;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CynkyAutomation.Utilities
{
    static class CosmosDB
    {
        #region Database Query Request

        static List<TEntity> RunQuery<TEntity>(string dbName, string collectionName, string query, Dictionary<string, object> queryParameters = null, string alternateSecrets = null)
        {
            List<TEntity> entities = new List<TEntity>();
            CosmosClientOptions clientOptions = new CosmosClientOptions()
            {
                RequestTimeout = TimeSpan.FromMinutes(2),
                IdleTcpConnectionTimeout = TimeSpan.FromMinutes(12),
                OpenTcpConnectionTimeout = TimeSpan.FromMinutes(2)
            };

            string cosmosSecrets = alternateSecrets == null ? dbName : alternateSecrets;

            using (var client = new CosmosClient(ConfigManager.AzureCosmosDBURL.GetValueOrDefault(cosmosSecrets),
                ConfigManager.AzureCosmosDBSecretKey.GetValueOrDefault(cosmosSecrets), clientOptions))
            {
                var container = client.GetContainer(dbName, collectionName);
                var queryDefinition = new QueryDefinition(query);
                if (queryParameters != null)
                {
                    foreach (var keyValuePair in queryParameters)
                    {
                        queryDefinition = queryDefinition.WithParameter(keyValuePair.Key, keyValuePair.Value);
                    }
                }

                using (FeedIterator<TEntity> resultSet = container.GetItemQueryIterator<TEntity>(queryDefinition))
                {
                    while (resultSet.HasMoreResults)
                    {
                        var response = resultSet.ReadNextAsync().Result;
                        entities.AddRange(response);
                    }
                }
            }
            return entities;
        }

        static List<BsonDocument> GetCredentialsItemsFromMongoDB(string dbName, string collectionName)
        {
            var dbClient = new MongoClient($"mongodb://-auth-{StringGenerator.GetStringViaRegex("", null)}-master:{ConfigManager.AzureCosmosDBSecretKey.GetValueOrDefault(dbName)}" +
                           $"@-auth-{StringGenerator.GetStringViaRegex("", null)}-master.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@-auth-{StringGenerator.GetStringViaRegex("", null)}-master@");
            IMongoDatabase db = dbClient.GetDatabase(dbName);

            var credentials = db.GetCollection<BsonDocument>(collectionName);
            var documents = credentials.Find(new BsonDocument()).Limit(1000000).ToList();

            return documents;
        }
        #endregion

        public static void UpdateItem(string dbName, string collectionName, dynamic jsonItem)
        {
            using (var _client = new CosmosClient(ConfigManager.AzureCosmosDBURL.GetValueOrDefault(dbName),
                ConfigManager.AzureCosmosDBSecretKey.GetValueOrDefault(dbName)))
            {
                var container = _client.GetContainer(dbName, collectionName);
                dynamic response = container.ReplaceItemAsync(jsonItem, jsonItem.id).Result;
            }
        }

        public static void CreateItem(string dbName, string collectionName, object jsonItem)
        {
            using (var _client = new CosmosClient(ConfigManager.AzureCosmosDBURL.GetValueOrDefault(dbName),
                ConfigManager.AzureCosmosDBSecretKey.GetValueOrDefault(dbName)))
            {
                var container = _client.GetContainer(dbName, collectionName);
                dynamic createResponse = container.CreateItemAsync(jsonItem).Result;
            }
        }

        public static void BulkDeleteItems(string dbName, string collectionName, string sqlQueryText)
        {
            MakeClient(dbName, collectionName).Scripts
            .ExecuteStoredProcedureAsync<dynamic>(
                "BulkDelete",
                PartitionKey.None,
                new dynamic[] { sqlQueryText });
        }

        static Container MakeClient(string dbName, string containerName)
        {
            using (var client = new CosmosClient(ConfigManager.AzureCosmosDBURL.GetValueOrDefault(dbName),
                ConfigManager.AzureCosmosDBSecretKey.GetValueOrDefault(dbName)))
            {
                var container = client.GetContainer(dbName, containerName);
                return container;
            }
        }
    }
}