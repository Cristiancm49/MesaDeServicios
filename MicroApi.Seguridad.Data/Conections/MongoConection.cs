using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MicroApi.Seguridad.Data.Utilities;
using MicroApi.Seguridad.Domain.Models.Mongo;

namespace MicroApi.Seguridad.Data.Conections
{
    public class MongoConnection
    {
        private readonly IMongoDatabase database;
        private readonly string collectionName;

        public MongoConnection(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            collectionName = mongoDbSettings.Value.CollectionName;
        }

        public IMongoCollection<Evidencia> GetEvidenciaCollection()
        {
            return database.GetCollection<Evidencia>(collectionName);
        }
    }
}
