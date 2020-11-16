using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Models
{
    public class SaltModel
    {
        public SaltModel(ObjectId id, string salt, string saltPass, List<string> recoveryKeysSalt)
        {
            Id = id;
            Salt = salt;
            SaltPass = saltPass;
            RecoveryKeysSalt = recoveryKeysSalt;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string Salt { get; set; }
        public string SaltPass { get; set; }
        public List<string> RecoveryKeysSalt { get; set; }
    }

    public class SaltHash
    {
        public SaltHash(string pass, string salt)
        {
            Pass = pass;
            Salt = salt;
        }
        public string Pass { get; set; }
        public string Salt { get; set; }
    }
}

