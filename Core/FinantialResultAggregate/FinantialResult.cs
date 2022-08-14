using Common.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Core.FinantialResultAggregate
{
    public class FinantialResult: IDocumentEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int Year { get; set; }
        public decimal GrowthWrittenPremium { get; set; }
        public ELineOfBusiness LineOfBusiness { get; set; }
        public ECountry Country { get; set; }

    }
}