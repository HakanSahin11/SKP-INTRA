using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Models
{
    public class RecoveryModel : IRecoveryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public List<string> RecoveryHashed { get; set; }
    }
    public interface IRecoveryModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
        List<string> RecoveryHashed { get; set; }
    }

    public class RecoveryPost
    {
        public RecoveryPost(string userName, string recoveryPass)
        {
            UserName = userName;
            RecoveryPass = recoveryPass;
        }
        public string UserName { get; set; }
        public string RecoveryPass { get; set; }
    }

    public class RecoveryKeyGenModel
    {
        public RecoveryKeyGenModel(List<string> key, List<string> saltKey, List<string> hashKey)
        {
            Key = key;
            SaltKey = saltKey;
            HashKey = hashKey;
        }
        public List<string> Key { get; set; }
        public List<string> SaltKey { get; set; }
        public List<string> HashKey { get; set; }
    }
}
