using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace SealedHelperServer.Models
{
    [BsonIgnoreExtraElements]
    public class DBMetadata
    {
        public int DecksCount { get; set; }
        public List<int> UsedDeckIndexes { get; set; }
    }
}