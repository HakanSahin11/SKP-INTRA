using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.Models
{
    public class ProjectModel
    {
        public ProjectModel(ObjectId id, ObjectId userId, List<string> attachmentId, string specialty, string title, string input, string status)
        {
            Id = id;
            UserId = userId;
            AttachmentId = attachmentId;
            Specialty = specialty;
            Title = title;
            Input = input;
            Status = status;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }

        public List<string> AttachmentId { get; set; }
        public string Specialty { get; set; }
        public string Title { get; set; }
        public string Input { get; set; }
        public string Status { get; set; }
    }
}
