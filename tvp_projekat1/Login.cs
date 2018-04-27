using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvp_projekat1
{
    class Login
    {
        [BsonId]
        public ObjectId ID { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("accountStatus")]
        public string AccountStatus { get; set; }
    }
}
