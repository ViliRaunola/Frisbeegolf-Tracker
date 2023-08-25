using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace server.Models
{
    [BsonIgnoreExtraElements]
    public class Fairway
    {
        [BsonElement("number")]
        public int Number { get; set; }
        
        [BsonElement("par")]
        public int Par { get; set; }

        [BsonElement("distance")]
        public int Distance { get; set; }

    }
}
