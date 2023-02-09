using server.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;

namespace server.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UsersService(
        IOptions<UsersDatabaseSettings> userDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                userDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userDatabaseSettings.Value.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(
                userDatabaseSettings.Value.UsersCollectionName);
        }


        public async Task<List<User>> GetAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return;
        }

        public async Task AddToGamesAsync(string id, string gameId)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            UpdateDefinition<User> update = Builders<User>.Update.AddToSet<string>("gameIds", gameId);
            await _userCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            await _userCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
