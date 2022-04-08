using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FirestoreSample.Model
{
    public class ReadingInt
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public long meteringPointId { get; set; }
        public string eanId { get; set; }
        public string externalReference { get; set; }
        public DateTime queryDate { get; set; }
        public string queryReason { get; set; }
        public string energyMeterId { get; set; }
        public int retryCount { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string readings { get; set; }
        public string _class { get; set; }

    }
}
