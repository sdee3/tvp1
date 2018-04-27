using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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