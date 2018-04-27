using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static IMongoCollection<IzbornaLista> VratiIzbornuListu()
        {
            return baza.GetCollection<IzbornaLista>("izbornaLista");
        }

        public static IMongoCollection<Login> VratiKolekcijuLogin()
        {
            return baza.GetCollection<Login>("login");
        }

        public static List<IzbornaLista> VratiKolekcijuIzbPredmeta(string brojIndeksa)
        {
            IMongoCollection<IzbornaLista> sveListe = baza.GetCollection<IzbornaLista>("predmetiDrugihSmerova");
            List<IzbornaLista> listaStudenta = 
                sveListe
                    .Find(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", brojIndeksa))
                    .ToList();

            return listaStudenta;
        }

    }
}
