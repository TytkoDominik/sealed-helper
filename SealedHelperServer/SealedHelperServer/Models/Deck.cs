using MongoDB.Bson.Serialization.Attributes;

namespace SealedHelperServer.Models
{
    [BsonIgnoreExtraElements]
    public class Deck
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string DoKLink { get; set; }
    }
}