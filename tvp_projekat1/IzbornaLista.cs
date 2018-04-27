using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvp_projekat1
{
    class IzbornaLista
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("brojIndeksa")]
        public string BrojIndeksa { get; set; }

        [BsonElement("predmeti")]
        public List<Predmeti> Predmeti { get; set; }

        [BsonElement("predmetiDrugihSmerova")]
        public List<Predmeti> PredmetiDrugihSmerova { get; set; }
    }
}
