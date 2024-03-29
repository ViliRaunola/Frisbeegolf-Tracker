﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace server.Models
{
    [BsonIgnoreExtraElements]
    public class Game
    {
        [BsonElement("mapId")]
        public string MapId { get; set; } = string.Empty;

        [BsonElement("score")]
        public List<int>? Score { get; set; } = null!;

        [BsonElement("dateTime")]
        public DateTime? DateTime { get; set; } = System.DateTime.Now;
    }
}
