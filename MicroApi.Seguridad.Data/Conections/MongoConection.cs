using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MicroApi.Seguridad.Data.Utilities;

namespace MicroApi.Seguridad.Data.Conections
{
    public class MongoConnection
    {
        private readonly IMongoDatabase _database;

        public MongoConnection(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }

        public IMongoCollection<Evidencia> GetCollection<Evidencia>(string collectionName)
        {
            return _database.GetCollection<Evidencia>(collectionName);
        }
    }
}