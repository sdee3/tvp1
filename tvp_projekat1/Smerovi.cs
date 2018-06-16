using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tvp_projekat1
{
    class Smerovi
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("sifraSmera")]
        public int SifraSmera { get; set; }

        [BsonElement("nazivSmera")]
        public string NazivSmera { get; set; }
    }
}