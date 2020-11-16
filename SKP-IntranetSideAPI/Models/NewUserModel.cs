using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Models
{
    public class NewUserModel : ILoginModel, IUserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public List<ObjectId> ProjectId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Specialty { get; set; }
        public string UserType { get; set; }
        public string Salt { get; set; }
        public List<string> Recovery { get; set; }
        public List<string> RecoveryKeysSalt { get; set; }
    }

    public class UserModel : IUserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public List<ObjectId> ProjectId { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
    }
    public interface IUserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }
        List<ObjectId> ProjectId { get; set; }
        string UserType { get; set; }
        string UserName { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Specialty { get; set; }
    }

    public class LoginModel : ILoginModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Recovery { get; set; }

    }
    public interface ILoginModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        ObjectId Id { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        List<string> Recovery { get; set; }
    }
}
