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
        private IzbornaLista izbornaListaStudenta;

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
            izbornaListaStudenta = Baza.VratiKolekcijuPredmetaStudenta(brojIndeksa);

            labelImePrezime.Text += " " + student.ImePrezime;
            labelBrIndeksa.Text += " " + student.BrojIndeksa;
            labelBrojTelefona.Text += " " + student.BrojTelefona;
            labelDatumRodjenja.Text += " " + student.DatumRodjenja;
            labelJMBG.Text += " " + student.JMBG;
            labelSmer.Text += " " + student.Smer.NazivSmera;

            SortirajPredmete();
            GenerisiPredmete();
            GenerisiPredmeteDrugihSmerova(sviPredmeti);
        }
        
        private void SortirajPredmete()
        {
            sviPredmeti.Sort((x,y) => 
            {
                int result = x.Semestar.CompareTo(y.Semestar);
                return result != 0 ? result : x.NazivPredmeta.CompareTo(y.NazivPredmeta);
            });
        }

        private void GenerisiPredmete()
        {
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
                        checkedListBox.SetItemCheckState(checkedListBox.Items.IndexOf(predmet.NazivPredmeta), CheckState.Checked);
                    else
                        foreach (Predmeti p in izbornaListaStudenta.Predmeti)
                            if (p.NazivPredmeta.Equals(predmet.NazivPredmeta))
                                checkedListBox.SetItemCheckState(checkedListBox.Items.IndexOf(predmet.NazivPredmeta), CheckState.Checked);

                    flag = false;
                }
            }
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

                if (!flag) comboBoxPredmetiDrugihSmerova.Items.Add(p.NazivPredmeta);
                else flag = false;
            }

            foreach (Predmeti p in izbornaListaStudenta.PredmetiDrugihSmerova)
                foreach(string naziviPredmeta in comboBoxPredmetiDrugihSmerova.Items)
                if (p.NazivPredmeta.Equals(naziviPredmeta))
                {
                    comboBoxPredmetiDrugihSmerova.SelectedItem = p.NazivPredmeta;
                    break;
                }
        }

        private void ProveraESPBBodova()
        {
            if (brojacESPB >= 48)
                buttonPrijaviPredmete.Enabled = true;
            else
                buttonPrijaviPredmete.Enabled = false;
        }

        private void ComboBoxPredmetiDrugihSmerova_SelectedValueChanged(object sender, EventArgs e)
        {
            Predmeti selektovanPredmet = Baza.VratiPredmetPoNazivu(comboBoxPredmetiDrugihSmerova.SelectedItem.ToString());
            brojacESPB += selektovanPredmet.ESPB;
            labelSumaESPB.Text = brojacESPB.ToString();
            comboBoxPredmetiDrugihSmerova.Enabled = false;
            ProveraESPBBodova();
        }

        private void checkedListBox_ItemCheck(object s, ItemCheckEventArgs e)
        {
            Predmeti selektovanPredmet = Baza.VratiPredmetPoNazivu(checkedListBox.Items[e.Index].ToString());

            if (e.NewValue == CheckState.Checked)
            {
                brojacESPB += selektovanPredmet.ESPB;
                labelSumaESPB.Text = brojacESPB.ToString();
                ProveraESPBBodova();
            }
            else if(e.NewValue == CheckState.Unchecked)
            {
                if (selektovanPredmet.Obavezan)
                    e.NewValue = CheckState.Checked;
                else
                {
                    brojacESPB -= selektovanPredmet.ESPB;
                    labelSumaESPB.Text = brojacESPB.ToString();
                    ProveraESPBBodova();
                }
            }
        }

        private void buttonPrijaviPredmete_Click(object sender, EventArgs e)
        {

        }
    }

}
