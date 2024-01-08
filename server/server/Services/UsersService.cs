using server.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections;
using System.Collections.Generic;

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

        public async Task<List<User>> GetUserAsync(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            return await _userCollection.Find(filter).ToListAsync();
        }

        public async Task CreateAsync(User user)
        {
            bool exists = await _userCollection.Find(_ => _.Subject == user.Subject).AnyAsync();
            // Using the Google's provided "Subject" id to make sure that user can register only once. 
            if (!exists) {
                await _userCollection.InsertOneAsync(user);
            }
            return;
        }

        public async Task UpdateUserGame(string id, Game game)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            UpdateDefinition<User> update = Builders<User>.Update.AddToSet<Game>("games", game);
            await _userCollection.UpdateOneAsync(filter, update);
        }
        // await _userCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
            await _userCollection.DeleteOneAsync(filter);
            return;
        }
    }
}
