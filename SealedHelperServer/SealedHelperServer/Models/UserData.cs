using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SealedHelperServer.Models
{
    [BsonIgnoreExtraElements]
    public class UserData
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
    }
}