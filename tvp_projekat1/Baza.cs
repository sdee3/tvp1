using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace tvp_projekat1
{
    class Baza
    {
        private static IMongoDatabase baza;
        private static bool instanca;

        private Baza ()
        {
            baza = new MongoClient("mongodb://sdee3:sdee3@ds147964.mlab.com:47964/tvp").GetDatabase("tvp");
        }

        public static void GetBaza()
        {
            if (instanca == false)
                new Baza();

            instanca = true;
        }

        public static IMongoCollection<Predmeti> VratiKolekcijuPredmeta()
        {
           return baza.GetCollection<Predmeti>("predmeti");
        }

        public static IMongoCollection<Smerovi> VratiKolekcijuSmerova()
        {
            return baza.GetCollection<Smerovi>("smerovi");
        }

        public static IMongoCollection<Studenti> VratiKolekcijuStudenata()
        {
            return baza.GetCollection<Studenti>("studenti");
        }

        public static IMongoCollection<IzbornaLista> VratiKolekcijuIzbornihLista()
        {
            return baza.GetCollection<IzbornaLista>("izbornaLista");
        }

        public static IMongoCollection<Login> VratiKolekcijuLogin()
        {
            return baza.GetCollection<Login>("login");
        }

        public static IzbornaLista VratiKolekcijuPredmetaStudenta(string brojIndeksa)
        {
            IMongoCollection<IzbornaLista> sveListe = baza.GetCollection<IzbornaLista>("izbornaLista");
            return sveListe.Find(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", brojIndeksa)).First(); ;
        }

        public static Predmeti VratiPredmetPoNazivu(string nazivPredmeta)
        {
            IMongoCollection<Predmeti> sviPredmetiCollection = baza.GetCollection<Predmeti>("predmeti");
            List<Predmeti> sviPredmeti = sviPredmetiCollection.Find(new BsonDocument()).ToList();
            Predmeti pronadjenPredmet = new Predmeti();

            foreach (Predmeti p in sviPredmeti)
                if (p.NazivPredmeta.Equals(nazivPredmeta))
                {
                    pronadjenPredmet = p;
                    break;
                }

            if (pronadjenPredmet != null)
                return pronadjenPredmet;
            else
                return null;
        }

    }
}
