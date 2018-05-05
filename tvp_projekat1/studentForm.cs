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
        private int brojacESPB;
        private Studenti student;

        private IMongoCollection<Studenti> kolekcijaStudenata;
        private IMongoCollection<Predmeti> kolekcijaSvihPredmeta;

        private List<Predmeti> sviPredmeti;

        public studentForm()
        {
            InitializeComponent();
        }

        public studentForm(string brojIndeksa) : this()
        {
            this.brojIndeksa = brojIndeksa;
            brojacESPB = 0;
        }

        private void studentForm_Load(object sender, EventArgs e)
        {
            Baza.GetBaza();
            kolekcijaStudenata = Baza.VratiKolekcijuStudenata();
            kolekcijaSvihPredmeta = Baza.VratiKolekcijuPredmeta();
            sviPredmeti = kolekcijaSvihPredmeta.Find(new BsonDocument()).ToList();

            student = kolekcijaStudenata.Find(Builders<Studenti>.Filter.Eq("brojIndeksa", brojIndeksa)).First();

            /* LABELE SA LEVE STRANE */
            labelImePrezime.Text += " " + student.ImePrezime;
            labelBrIndeksa.Text += " " + student.BrojIndeksa;
            labelBrojTelefona.Text += " " + student.BrojTelefona;
            labelDatumRodjenja.Text += " " + student.DatumRodjenja;
            labelJMBG.Text += " " + student.JMBG;
            labelSmer.Text += " " + student.Smer.NazivSmera;

            int brojac = 0;

            /* LISTA DOSTUPNIH PREDMETA */
            foreach (Predmeti predmet in sviPredmeti)
            {
                bool flag = false;

                foreach (Smerovi s in predmet.SmeroviPredmeta)
                    if (s.NazivSmera.Equals(student.Smer.NazivSmera))
                    {
                        flag = true;
                        break;
                    }

                if (flag)
                {
                    checkedListBox.Items.Add(predmet.NazivPredmeta);
                    if (predmet.Obavezan)
                    {
                        checkedListBox.SetItemCheckState(brojac++, CheckState.Indeterminate);
                    }
                    else brojac++;

                    flag = false;
                }
            }
            
            SortirajPredmete();
            GenerisiPredmeteDrugihSmerova(sviPredmeti);
        }

        private void GenerisiPredmeteDrugihSmerova(List<Predmeti> sviPredmeti)
        {
            bool flag = false;

            foreach (Predmeti p in sviPredmeti)
            {
                foreach (Smerovi s in p.SmeroviPredmeta)
                {
                    if (s.NazivSmera.Equals(student.Smer.NazivSmera))
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    comboBoxPredmetiDrugihSmerova.Items.Add(p.NazivPredmeta);
                else flag = false;
            }
        }

        private void SortirajPredmete()
        {
            
        }

        private void checkedListBox_ItemCheck(object s, ItemCheckEventArgs e)
        {
            Predmeti selektovanPredmet = Baza.VratiPredmetPoNazivu(checkedListBox.Items[e.Index].ToString());

            if (e.NewValue == CheckState.Checked || e.NewValue == CheckState.Indeterminate)
            {
                brojacESPB += selektovanPredmet.ESPB;
                labelSumaESPB.Text = brojacESPB.ToString();
            }
            else if(e.NewValue == CheckState.Unchecked)
            {
                brojacESPB -= selektovanPredmet.ESPB;
                labelSumaESPB.Text = brojacESPB.ToString();
            }
     
        }

        private void ComboBoxChanged(object s, EventArgs e, Predmeti predmet)
        {
            brojacESPB += predmet.ESPB;
            labelSumaESPB.Text = brojacESPB.ToString();

            if (brojacESPB >= 48)
                buttonPrijaviPredmete.Enabled = true;
        }

    }

}
