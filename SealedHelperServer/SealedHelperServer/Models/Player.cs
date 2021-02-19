using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SealedHelperServer.Models
{
    public class Player
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string Deck { get; set; }
        public string DeckLink { get; set; }
    }
}