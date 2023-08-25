using server.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace server.Services
{
    public class MapsService
    {
        private readonly IMongoCollection<Map> _mapCollection;

        public MapsService(
        IOptions<MapsDatabaseSettings> mapDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mapDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mapDatabaseSettings.Value.DatabaseName);

            _mapCollection = mongoDatabase.GetCollection<Map>(
                mapDatabaseSettings.Value.MapsCollectionName);
        }


        public async Task<List<Map>> GetAsync()
        {
            return await _mapCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Map>> GetMapAsync(string id)
        {
            FilterDefinition<Map> filter = Builders<Map>.Filter.Eq("Id", id);
            return await _mapCollection.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(Map map)
        {
            await _mapCollection.InsertOneAsync(map);
            return;
        }

    }
}
