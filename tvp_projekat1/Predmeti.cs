using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace tvp_projekat1
{
    class Predmeti
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("sifraPredmeta")]
        public string SifraPredmeta { get; set; }

        [BsonElement("nazivPredmeta")]
        public string NazivPredmeta { get; set; }

        [BsonElement("obavezan")]
        public bool Obavezan { get; set; }

        [BsonElement("profesor")]
        public string Profesor { get; set; }

        [BsonElement("semestar")]
        public int Semestar { get; set; }

        [BsonElement("espb")]
        public int ESPB { get; set; }

        [BsonElement("smerovi")]
        public List<Smerovi> SmeroviPredmeta { get; set; }

    }
}