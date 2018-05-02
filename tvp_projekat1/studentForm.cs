using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace tvp_projekat1
{
    public partial class studentForm : Form
    {
        private string brojIndeksa;
        private int razmakYOsa;
        private int brojacESPB;
        private List<CheckBox> checkboxListaPredmeta;

        public studentForm()
        {
            InitializeComponent();
        }

        public studentForm(string brojIndeksa) : this()
        {
            this.brojIndeksa = brojIndeksa;
            razmakYOsa = 10;
            brojacESPB = 0;
            checkboxListaPredmeta = new List<CheckBox>();
        }

        private void studentForm_Load(object sender, EventArgs e)
        {
            var baza = new MongoClient("mongodb://sdee3:sdee3@ds147964.mlab.com:47964/tvp").GetDatabase("tvp");

            var studenti = baza.GetCollection<BsonDocument>("studenti");
            var predmeti = baza.GetCollection<BsonDocument>("predmeti");

            var student = studenti.Find(Builders<BsonDocument>.Filter.Eq("brojIndeksa", brojIndeksa)).First();
            var dostupniPredmetiCursor = predmeti.Find(new BsonDocument("smerovi", student["smerovi"])).ToCursor();

            /* LABELE SA LEVE STRANE */
            labelImePrezime.Text += " " + student["imePrezime"];
            labelBrIndeksa.Text += " " + this.brojIndeksa;
            labelBrojTelefona.Text += " " + student["brojTelefona"];
            labelDatumRodjenja.Text += " " + student["datumRodjenja"];
            labelJMBG.Text += " " + student["jmbg"];
            labelSmer.Text += " " + (student["smerovi"])["nazivSmera"];

            /* LISTA DOSTUPNIH PREDMETA */
            foreach (var predmet in dostupniPredmetiCursor.ToEnumerable())
            {
                CheckBox noviPredmet = new CheckBox();
                noviPredmet.Text = predmet["nazivPredmeta"].ToString();
                noviPredmet.Location = new Point(375, (65 + (razmakYOsa += 7)));
                noviPredmet.BringToFront();

                noviPredmet.CheckedChanged += (s, ea) => CheckBoxStateChanged(s, ea, predmet);
                if (predmet["obavezan"] == true)
                {
                    noviPredmet.Checked = true;
                    noviPredmet.Enabled = false;
                }

                checkboxListaPredmeta.Add(noviPredmet);
            }

            SortirajIDodajCheckBoxListu();

            GenerisiPredmeteDrugihSmerova(student, predmeti);
        }

        private void GenerisiPredmeteDrugihSmerova(BsonDocument student, IMongoCollection<BsonDocument> predmeti)
        {
            var dostupniPredmetiCursor = predmeti.Find(Builders<BsonDocument>.Filter.Not(new BsonDocument("smerovi", student["smerovi"]))).ToCursor();
            foreach (var predmet in dostupniPredmetiCursor.ToEnumerable())
            {
                object noviPredmet = predmet["nazivPredmeta"].ToString();
                comboBoxPredmetiDrugihSmerova.Items.Add(noviPredmet);
                comboBoxPredmetiDrugihSmerova.SelectedIndexChanged += (s, ea) => ComboBoxChanged(s, ea, predmet);
            }
        }

        private void SortirajIDodajCheckBoxListu()
        {
            checkboxListaPredmeta.Sort();
            foreach (CheckBox c in checkboxListaPredmeta)
                this.Controls.Add(c);
        }

        private void CheckBoxStateChanged(object s, EventArgs e, BsonDocument predmet)
        {
            if((s as CheckBox).Checked)
                brojacESPB += predmet["espb"].ToInt32();
            else
                brojacESPB -= predmet["espb"].ToInt32();

            labelSumaESPB.Text = brojacESPB.ToString();

            if(brojacESPB >= 48)
                buttonPrijaviPredmete.Enabled = true;
        }

        private void ComboBoxChanged(object s, EventArgs e, BsonDocument predmet)
        {
            brojacESPB += predmet["espb"].ToInt32();
            labelSumaESPB.Text = brojacESPB.ToString();

            if (brojacESPB >= 48)
                buttonPrijaviPredmete.Enabled = true;
        }
    }

}
