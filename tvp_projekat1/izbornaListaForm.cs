using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace tvp_projekat1
{
    public partial class izbornaListaForm : Form
    {
        private Studenti selektovanStudent;
        private IzbornaLista izbornaListaStudenta;

        public izbornaListaForm()
        {
            InitializeComponent();
        }

        private void izbornaListaForm_Load(object sender, EventArgs e)
        {
            buttonSaveIzmene.Enabled = false;

            if(selektovanStudent != null && izbornaListaStudenta != null)
            {
                textBoxPredmetiSmera.Text = IspisPredmeta();
                textBoxPredmetiDrugihSmerova.Text = IspisPredmetaDrugihSmerova();
            }
        }

        private string IspisPredmetaDrugihSmerova()
        {
            string result = "";

            if (izbornaListaStudenta.PredmetiDrugihSmerova.Count == 0)
                return "Student jos uvek nije izabrao nijedan predmet.";
            else
                foreach (Predmeti p in izbornaListaStudenta.PredmetiDrugihSmerova)
                    result += p.NazivPredmeta + Environment.NewLine;

            return result;
        }

        private string IspisPredmeta()
        {
            string result = "";

            if (izbornaListaStudenta.Predmeti.Count == 0)
                return "Student jos uvek nije izabrao nijedan predmet.";
            else
                foreach (Predmeti p in izbornaListaStudenta.Predmeti)
                    result += p.NazivPredmeta + Environment.NewLine;

            return result;
        }

        internal void SetStudentInfo(Studenti selektovanStudent, IzbornaLista izbornaListaStudenta)
        {
            this.selektovanStudent = selektovanStudent;
            this.izbornaListaStudenta = izbornaListaStudenta;
        }

        private void buttonAzuriraj_Click(object sender, EventArgs e)
        {
            buttonSaveIzmene.Enabled = true;
            buttonAzuriraj.Enabled = false;

            if (textBoxPredmetiSmera.Text.StartsWith("Student jos uvek"))
                textBoxPredmetiSmera.Text = "";
            if (textBoxPredmetiDrugihSmerova.Text.StartsWith("Student jos uvek"))
                textBoxPredmetiDrugihSmerova.Text = "";

            textBoxPredmetiSmera.ReadOnly = textBoxPredmetiDrugihSmerova.ReadOnly = false;
        }

        private void buttonSaveIzmene_Click(object sender, EventArgs e)
        {
            List<Predmeti> noviPredmeti = new List<Predmeti>();
            List<Predmeti> noviPredmetiDrugihSmerova = new List<Predmeti>();
            IzbornaLista novaIzbornaLista = new IzbornaLista();
            IMongoCollection<IzbornaLista> sveIzborneListeBaze = Baza.VratiKolekcijuIzbornihLista();

            buttonSaveIzmene.Enabled = false;
            buttonAzuriraj.Enabled = true;
            textBoxPredmetiSmera.ReadOnly = textBoxPredmetiDrugihSmerova.ReadOnly= true;

            novaIzbornaLista.BrojIndeksa = izbornaListaStudenta.BrojIndeksa;
            novaIzbornaLista.ID = izbornaListaStudenta.ID;

            string[] nizNovihPredmeta = textBoxPredmetiSmera.Text.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            string[] nizNovihPredmetaDrSmerova = textBoxPredmetiDrugihSmerova.Text.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string nazivPredmeta in nizNovihPredmeta)
            {
                Predmeti tmpPredmet = Baza.VratiPredmetPoNazivu(nazivPredmeta);
                bool flagPronadjenSmerStudenta = false;

                if (tmpPredmet.NazivPredmeta != null)
                {
                    foreach (Smerovi s in tmpPredmet.SmeroviPredmeta)
                        if (s.NazivSmera.Equals(selektovanStudent.Smer.NazivSmera))
                        {
                            noviPredmeti.Add(tmpPredmet);
                            flagPronadjenSmerStudenta = true;
                            break;
                        }

                    if (!flagPronadjenSmerStudenta)
                        MessageBox.Show("Predmet " + tmpPredmet.NazivPredmeta + " ne pripada smeru studenta!");
                }
                else if(tmpPredmet.NazivPredmeta == null && textBoxPredmetiSmera.Text.Length > 0)
                    MessageBox.Show("Predmet " + nazivPredmeta + " ne postoji u bazi!");
            }

            foreach (string nazivPredmeta in nizNovihPredmetaDrSmerova)
            {
                Predmeti tmpPredmet = Baza.VratiPredmetPoNazivu(nazivPredmeta);

                if (tmpPredmet.NazivPredmeta != null)                
                    noviPredmetiDrugihSmerova.Add(tmpPredmet);                    
                else if (tmpPredmet.NazivPredmeta == null && textBoxPredmetiDrugihSmerova.Text.Length > 0)
                    MessageBox.Show("Predmet " + nazivPredmeta + " ne postoji u bazi!");
            }

            novaIzbornaLista.Predmeti = noviPredmeti;
            novaIzbornaLista.PredmetiDrugihSmerova = noviPredmetiDrugihSmerova;

            sveIzborneListeBaze.FindOneAndReplace(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", selektovanStudent.BrojIndeksa), novaIzbornaLista);
            MessageBox.Show("Uspesno izmenjena izborna lista!");
        }
    }
}
