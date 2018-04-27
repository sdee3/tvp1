using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tvp_projekat1
{
    class Studenti
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement ("imePrezime")]
        public string ImePrezime { get; set; }

        [BsonElement ("brojIndeksa")]
        public string BrojIndeksa { get; set; }

        [BsonElement("datumRodjenja")]
        public string DatumRodjenja { get; set; }

        [BsonElement ("jmbg")]
        public string JMBG { get; set; }

        [BsonElement ("brojTelefona")]
        public string BrojTelefona { get; set; }

        [BsonElement ("smerovi")]
        public Smerovi Smer { get; set; }
    }
}