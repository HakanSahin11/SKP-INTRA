using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKP_IntranetSideAPI.Models
{
    public class ForumModel
    {
        public ForumModel(ObjectId id, ObjectId userId, ObjectId subforumId, List<string> attachmentId, string specialty, string title, string input, string username)
        {
            Id = id;
            UserId = userId;
            SubforumId = subforumId;
            AttachmentId = attachmentId;
            Specialty = specialty;
            Title = title;
            Input = input;
            Username = username;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public ObjectId SubforumId { get; set; }
        public List<string> AttachmentId { get; set; }
        public string Specialty { get; set; }
        public string Title { get; set; }
        public string Input { get; set; }
        public string Username { get; set; }
    }
}
