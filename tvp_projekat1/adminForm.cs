using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace tvp_projekat1
{
    public partial class adminForm : Form
    {
        private int razmakYOsa;
        private int pocetnaYOsa;
        private string flagZaSaveDugme;
        private delegate void GenerisiKontrole(int brojKontrola);

        private IMongoCollection<Smerovi> kolekcijaSmerova;
        private IMongoCollection<Studenti> kolekcijaStudenata;
        private IMongoCollection<Predmeti> kolekcijaPredmeta;
        private IMongoCollection<IzbornaLista> kolekcijaIzbornihListi;
        private IMongoCollection<Login> kolekcijaLogin;

        private List<Smerovi> sviSmerovi;
        private List<Predmeti> sviPredmeti;
        private List<Studenti> sviStudenti;
        private List<IzbornaLista> sveIzborneListe;

        public adminForm()
        {
            InitializeComponent();
        }

        private void adminForm_Load(object sender, EventArgs e)
        {
            Baza.GetBaza();

            kolekcijaSmerova = Baza.VratiKolekcijuSmerova();
            kolekcijaStudenata = Baza.VratiKolekcijuStudenata();
            kolekcijaPredmeta = Baza.VratiKolekcijuPredmeta();
            kolekcijaIzbornihListi = Baza.VratiIzbornuListu();
            kolekcijaLogin = Baza.VratiKolekcijuLogin();

            sviSmerovi = kolekcijaSmerova.Find(new BsonDocument()).ToList();
            sviPredmeti = kolekcijaPredmeta.Find(new BsonDocument()).ToList();
            sviStudenti = kolekcijaStudenata.Find(new BsonDocument()).ToList();
            sveIzborneListe = kolekcijaIzbornihListi.Find(new BsonDocument()).ToList();

            GenerisiSmerove();
            GenerisiPredmete();
            GenerisiStudente();

            razmakYOsa = 0;
            pocetnaYOsa = 20;
        }

        private void buttonAddSmer_Click(object sender, EventArgs e)
        {
            flagZaSaveDugme = "smer";
            
            GenerisiKontrole generisiKontrole = new GenerisiKontrole(GenerisiKontroleZaSmerove);
            generisiKontrole(2);

            Button azuriranjeSmeraBtn = (Button)(mainPanel.Controls.Find("azuriranjeSmeraBtn", true))[0];
            Button brisanjeSmeraBtn = (Button)(mainPanel.Controls.Find("brisanjeSmeraBtn", true))[0];
            Button prikazStatistikeBtn = (Button)(mainPanel.Controls.Find("prikazStatistikeBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            azuriranjeSmeraBtn.Enabled = brisanjeSmeraBtn.Enabled = prikazStatistikeBtn.Enabled = sacuvajIzmeneBtn.Enabled = false;

            buttonSaveIzmene.Enabled = true;
            buttonAddSmer.Enabled = false;
            buttonAddPredmet.Enabled = true;
            buttonAddStudent.Enabled = true;
            buttonSaveIzmene.Click += (s, ea) => VerifikujIDodajSmer(s, ea);
        }

        private void VerifikujIDodajSmer(object sender, EventArgs e)
        {
            if(flagZaSaveDugme.Equals("smer"))
            {
                int noviIdSmera = 0;
                TextBox idSmeraTb = mainPanel.Controls.Find("idSmeraTb", false)[0] as TextBox;
                TextBox nazivSmeraTb = mainPanel.Controls.Find("nazivSmeraTb", false)[0] as TextBox;

                Smerovi noviSmer = new Smerovi();
                noviSmer.NazivSmera = nazivSmeraTb.Text;

                if (Int32.TryParse(idSmeraTb.Text, out noviIdSmera))
                {
                    noviSmer.SifraSmera = noviIdSmera;

                    if (!DuplikatSmera(noviIdSmera))
                    {
                        kolekcijaSmerova.InsertOne(noviSmer);

                        MessageBox.Show("Smer " + noviSmer.NazivSmera + " uspesno dodat!");
                        idSmeraTb.Enabled = false;
                        nazivSmeraTb.Enabled = false;

                        buttonSaveIzmene.Enabled = false;
                        buttonAddSmer.Enabled = true;

                        GenerisiSmerove();
                    }
                    else
                        MessageBox.Show("Smer vec postoji u bazi!");
                }
                else
                {
                    MessageBox.Show("Sifra smera mora biti broj! Pokusajte ponovo.");
                }                
            }
            
        }

        private void buttonAddPredmet_Click(object sender, EventArgs e)
        {
            flagZaSaveDugme = "predmet";

            GenerisiKontrole generisiKontrole = new GenerisiKontrole(GenerisiKontroleZaPredmete);
            generisiKontrole(7);

            Button azuriranjePredmetaBtn = (Button)(mainPanel.Controls.Find("azuriranjePredmetaBtn", true))[0];
            Button brisanjePredmetaBtn = (Button)(mainPanel.Controls.Find("brisanjePredmetaBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            azuriranjePredmetaBtn.Enabled = brisanjePredmetaBtn.Enabled = sacuvajIzmeneBtn.Enabled = false;

            buttonSaveIzmene.Enabled = true;
            buttonAddPredmet.Enabled = false;
            buttonAddSmer.Enabled = true;
            buttonAddStudent.Enabled = true;
            buttonSaveIzmene.Click += (s, ea) => VerifikujIDodajPredmet(s, ea);
        }

        private void VerifikujIDodajPredmet(object sender, EventArgs e)
        {
            if(flagZaSaveDugme.Equals("predmet"))
                SnimanjePredmeta(null);                
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            flagZaSaveDugme = "student";

            GenerisiKontrole generisiKontrole = new GenerisiKontrole(GenerisiKontroleZaStudente);
            generisiKontrole(6);

            Button azuriranjeStudentaBtn = (Button)(mainPanel.Controls.Find("azuriranjeStudentaBtn", true))[0];
            Button brisanjeStudentaBtn = (Button)(mainPanel.Controls.Find("brisanjeStudentaBtn", true))[0];
            Button izbornaListaBtn = (Button)(mainPanel.Controls.Find("izbornaListaBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            azuriranjeStudentaBtn.Enabled = brisanjeStudentaBtn.Enabled = izbornaListaBtn.Enabled = sacuvajIzmeneBtn.Enabled = false;

            buttonSaveIzmene.Enabled = true;
            buttonAddStudent.Enabled = false;
            buttonAddSmer.Enabled = true;
            buttonAddPredmet.Enabled = true;
            buttonSaveIzmene.Click += (s, ea) => VerifikujIDodajStudenta(s, ea);
        }

        private void VerifikujIDodajStudenta(object sender, EventArgs e)
        {
            if (flagZaSaveDugme.Equals("student"))
            {
                TextBox imePrezimeTb = mainPanel.Controls.Find("imePrezimeTb", false)[0] as TextBox;
                TextBox brojIndeksaTb = mainPanel.Controls.Find("brojIndeksaTb", false)[0] as TextBox;
                TextBox datumRodjenjaTb = mainPanel.Controls.Find("datumRodjenjaTb", false)[0] as TextBox;
                TextBox smerTb = mainPanel.Controls.Find("smerTb", false)[0] as TextBox;
                TextBox brojTelefonaTb = mainPanel.Controls.Find("brojTelefonaTb", false)[0] as TextBox;
                TextBox jmbgTb = mainPanel.Controls.Find("jmbgTb", false)[0] as TextBox;

                Studenti noviStudent = new Studenti();

                if(imePrezimeTb.Text.Length > 0 && datumRodjenjaTb.Text.Length > 0 &&
                    brojTelefonaTb.Text.Length > 0 && jmbgTb.Text.Length > 0)
                {
                    List<Smerovi> pronadjeniSmer = kolekcijaSmerova.Find(Builders<Smerovi>.Filter.Eq("nazivSmera", smerTb.Text)).ToList();

                    if (smerTb.Text.Length == 0)
                    {
                        noviStudent.Smer = new Smerovi();
                        noviStudent.ImePrezime = imePrezimeTb.Text;
                        noviStudent.BrojIndeksa = (brojIndeksaTb.Text.Length > 0) ? brojIndeksaTb.Text : ("temp"+jmbgTb.Text);
                        noviStudent.DatumRodjenja = datumRodjenjaTb.Text;
                        noviStudent.JMBG = jmbgTb.Text;
                        noviStudent.BrojTelefona = brojTelefonaTb.Text;

                        IzbornaLista novaIzbornaLista = new IzbornaLista();
                        novaIzbornaLista.Predmeti = new List<Predmeti>();
                        novaIzbornaLista.PredmetiDrugihSmerova = new List<Predmeti>();
                        novaIzbornaLista.BrojIndeksa = (noviStudent.BrojIndeksa.Length > 0) ? noviStudent.BrojIndeksa : ("temp" + jmbgTb.Text);

                        Login login = new Login();
                        login.AccountStatus = "student";
                        login.Username = (noviStudent.BrojIndeksa.Length > 0) ? noviStudent.BrojIndeksa : noviStudent.ImePrezime.Split(' ')[0];
                        login.Password = noviStudent.ImePrezime.Split(' ')[0];

                        if (!DuplikatStudenta(noviStudent.JMBG, noviStudent.BrojTelefona))
                        {

                            kolekcijaStudenata.InsertOne(noviStudent);
                            kolekcijaIzbornihListi.InsertOne(novaIzbornaLista);
                            kolekcijaLogin.InsertOne(login);

                            MessageBox.Show("Student " + noviStudent.ImePrezime + " uspesno dodat!");
                            MessageBox.Show("Izborna lista uspesno kreirana!");
                            MessageBox.Show("Nalog za pristup studentskom servisu kreiran!");

                            buttonSaveIzmene.Enabled = false;
                            buttonAddStudent.Enabled = true;

                            GenerisiStudente();
                        }
                        else
                            MessageBox.Show("Student vec postoji u bazi!");
                    }     
                    else
                    {
                        if (pronadjeniSmer.Count > 0)
                        {
                            noviStudent.Smer = pronadjeniSmer[0];
                            noviStudent.ImePrezime = imePrezimeTb.Text;
                            noviStudent.BrojIndeksa = (brojIndeksaTb.Text.Length > 0) ? brojIndeksaTb.Text : "";
                            noviStudent.DatumRodjenja = datumRodjenjaTb.Text;
                            noviStudent.JMBG = jmbgTb.Text;
                            noviStudent.BrojTelefona = brojTelefonaTb.Text;

                            IzbornaLista izbornaLista = new IzbornaLista();
                            izbornaLista.Predmeti = new List<Predmeti>();
                            izbornaLista.PredmetiDrugihSmerova = new List<Predmeti>();
                            izbornaLista.BrojIndeksa = ((noviStudent.BrojIndeksa.Length > 0) ? noviStudent.BrojIndeksa : "");

                            Login login = new Login();
                            login.AccountStatus = "student";
                            login.Username = (noviStudent.BrojIndeksa.Length > 0) ? noviStudent.BrojIndeksa : noviStudent.ImePrezime.Split(' ')[0];
                            login.Password = noviStudent.ImePrezime.Split(' ')[0];

                            if (!DuplikatStudenta(noviStudent.JMBG, noviStudent.BrojTelefona))
                            {

                                kolekcijaStudenata.InsertOne(noviStudent);
                                kolekcijaIzbornihListi.InsertOne(izbornaLista);
                                kolekcijaLogin.InsertOne(login);

                                MessageBox.Show("Student " + noviStudent.ImePrezime + " uspesno dodat!");
                                MessageBox.Show("Izborna lista uspesno kreirana!");
                                MessageBox.Show("Nalog za pristup studentskom servisu kreiran!");

                                buttonSaveIzmene.Enabled = false;
                                buttonAddStudent.Enabled = true;

                                GenerisiStudente();
                            }
                            else
                                MessageBox.Show("Student vec postoji u bazi!");
                        }
                        else
                        {
                            noviStudent.Smer = new Smerovi();
                            MessageBox.Show("Uneti smer ne postoji!");
                        }
                    }             
                }
                else
                {
                    MessageBox.Show("Greska u unosu! Polja za ime i prezime, datum rodjenja, broj telefona, i JMBG su obavezna!");
                }
            }         
        }

        private void GenerisiSmerove()
        {
            comboBoxSmerovi.Items.Clear();
            List<Smerovi> sviSmerovi = kolekcijaSmerova.Find(new BsonDocument()).ToList();
            
            GenerisiKontrole generisiKontrole = new GenerisiKontrole(GenerisiKontroleZaSmerove);

            foreach (var smer in sviSmerovi)
            {
                comboBoxSmerovi.Items.Add(smer.NazivSmera);
            }

            comboBoxSmerovi.SelectedIndexChanged += (s, ea) => ComboBoxSmerChanged(s, ea, kolekcijaSmerova, generisiKontrole);
        }

        private void GenerisiPredmete()
        {
            comboBoxPredmeti.Items.Clear();
            List<Predmeti> sviPredmeti = kolekcijaPredmeta.Find(new BsonDocument()).ToList();
            GenerisiKontrole generisiKontrole = new GenerisiKontrole(GenerisiKontroleZaPredmete);

            foreach (var predmet in sviPredmeti)
            {
                comboBoxPredmeti.Items.Add(predmet.NazivPredmeta);
            }
            comboBoxPredmeti.SelectedIndexChanged += (s, ea) => ComboBoxPredmetChanged(s, ea, kolekcijaPredmeta, generisiKontrole);
        }

        private void GenerisiStudente()
        {
            comboBoxStudenti.Items.Clear();
            List<Studenti> sviStudenti = kolekcijaStudenata.Find(new BsonDocument()).ToList();
            GenerisiKontrole generisiKontrole = new GenerisiKontrole(GenerisiKontroleZaStudente);

            foreach (var student in sviStudenti)
            {
                comboBoxStudenti.Items.Add(student.BrojIndeksa);
            }

            comboBoxStudenti.SelectedIndexChanged += (s, ea) => ComboBoxStudentChanged(s, ea, kolekcijaStudenata, generisiKontrole);
        }

        private void ComboBoxStudentChanged(object s, EventArgs e, IMongoCollection<Studenti> kolekcijaStudenata, GenerisiKontrole generisiKontrole)
        {
            buttonSaveIzmene.Enabled = false;
            buttonStatistika.Enabled = false;
            generisiKontrole(6);

            Button azuriranjeStudentaBtn = (Button)(mainPanel.Controls.Find("azuriranjeStudentaBtn", true))[0];
            Button brisanjeStudentaBtn = (Button)(mainPanel.Controls.Find("brisanjeStudentaBtn", true))[0];
            Button izbornaListaBtn = (Button)(mainPanel.Controls.Find("izbornaListaBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            sacuvajIzmeneBtn.Enabled = false;

            string SelektovanBrojIndeksa = comboBoxStudenti.SelectedItem.ToString();
            Studenti student = kolekcijaStudenata.Find(Builders<Studenti>.Filter.Eq("brojIndeksa", SelektovanBrojIndeksa))
                .First();

            TextBox imePrezimeTb = mainPanel.Controls.Find("imePrezimeTb", false)[0] as TextBox;
            TextBox brojIndeksaTb = mainPanel.Controls.Find("brojIndeksaTb", false)[0] as TextBox;
            TextBox datumRodjenjaTb = mainPanel.Controls.Find("datumRodjenjaTb", false)[0] as TextBox;
            TextBox smerTb = mainPanel.Controls.Find("smerTb", false)[0] as TextBox;
            TextBox brojTelefonaTb = mainPanel.Controls.Find("brojTelefonaTb", false)[0] as TextBox;
            TextBox jmbgTb = mainPanel.Controls.Find("jmbgTb", false)[0] as TextBox;

            imePrezimeTb.Text = student.ImePrezime;
            brojIndeksaTb.Text = student.BrojIndeksa.ToString();
            datumRodjenjaTb.Text = student.DatumRodjenja;
            smerTb.Text = student.Smer.NazivSmera;
            brojTelefonaTb.Text = student.BrojTelefona;
            jmbgTb.Text = student.JMBG.ToString();

            imePrezimeTb.Enabled = brojIndeksaTb.Enabled = datumRodjenjaTb.Enabled = smerTb.Enabled =
                brojTelefonaTb.Enabled = jmbgTb.Enabled = false;

            azuriranjeStudentaBtn.Click += (sender, ea) => AzuriranjeStudenta(student, imePrezimeTb, brojIndeksaTb, datumRodjenjaTb, smerTb, brojTelefonaTb, jmbgTb);
            brisanjeStudentaBtn.Click += (sender, ea) => BrisanjeStudenta(student);
        }

        private void BrisanjeStudenta(Studenti prethodnoSelektovanStudent)
        {
            var confirmResult = MessageBox.Show("Da li sigurno zelite izbrisati studenta iz baze?", "Potvrda brisanja",  MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                kolekcijaStudenata.FindOneAndDelete(Builders<Studenti>.Filter.Eq("jmbg", prethodnoSelektovanStudent.JMBG));
                MessageBox.Show("Student obrisan!");
                kolekcijaLogin.FindOneAndDelete(Builders<Login>.Filter.Eq("username", prethodnoSelektovanStudent.BrojIndeksa));
                MessageBox.Show("Nalog za pristup studentskom servisu obrisan!");
                kolekcijaIzbornihListi.FindOneAndDelete(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", prethodnoSelektovanStudent.BrojIndeksa));
                MessageBox.Show("Lista izabranih predmeta studenta obrisana!");

                GenerisiStudente();
            }
                
            
        }

        private void AzuriranjeStudenta(Studenti prethodnoSelektovanStudent, TextBox imePrezimeTb, TextBox brojIndeksaTb, TextBox datumRodjenjaTb, TextBox smerTb, TextBox brojTelefonaTb, TextBox jmbgTb)
        {
            Button azuriranjeStudentaBtn = (Button)(mainPanel.Controls.Find("azuriranjeStudentaBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            imePrezimeTb.Enabled = brojIndeksaTb.Enabled = datumRodjenjaTb.Enabled = smerTb.Enabled =
                brojTelefonaTb.Enabled = jmbgTb.Enabled = true;

            sacuvajIzmeneBtn.Enabled = true;
            azuriranjeStudentaBtn.Enabled = false;

            flagZaSaveDugme = "student";
            sacuvajIzmeneBtn.Click += (sender, ea) => SnimanjeStudenta(prethodnoSelektovanStudent);
        }

        private void SnimanjeStudenta(Studenti prethodnoSelektovanStudent)
        {
            if (flagZaSaveDugme.Equals("student"))
            {
                TextBox imePrezimeTb = mainPanel.Controls.Find("imePrezimeTb", false)[0] as TextBox;
                TextBox brojIndeksaTb = mainPanel.Controls.Find("brojIndeksaTb", false)[0] as TextBox;
                TextBox datumRodjenjaTb = mainPanel.Controls.Find("datumRodjenjaTb", false)[0] as TextBox;
                TextBox smerTb = mainPanel.Controls.Find("smerTb", false)[0] as TextBox;
                TextBox brojTelefonaTb = mainPanel.Controls.Find("brojTelefonaTb", false)[0] as TextBox;
                TextBox jmbgTb = mainPanel.Controls.Find("jmbgTb", false)[0] as TextBox;

                Studenti noviStudent = new Studenti();
                IzbornaLista novaIzbornaLista = new IzbornaLista();
                Login noviLogin = new Login();

                IzbornaLista postojecaIzbornaLista = kolekcijaIzbornihListi.Find(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", prethodnoSelektovanStudent.BrojIndeksa)).First();
                Login postojeciLogin = kolekcijaLogin.Find(Builders<Login>.Filter.Eq("username", prethodnoSelektovanStudent.BrojIndeksa)).First();

                noviStudent.ID = prethodnoSelektovanStudent.ID;

                novaIzbornaLista.ID = postojecaIzbornaLista.ID;
                novaIzbornaLista.Predmeti = postojecaIzbornaLista.Predmeti;
                novaIzbornaLista.PredmetiDrugihSmerova = postojecaIzbornaLista.PredmetiDrugihSmerova;
                
                noviLogin.ID = postojeciLogin.ID;
                noviLogin.AccountStatus = "student";

                if (imePrezimeTb.Text.Length > 0 && datumRodjenjaTb.Text.Length > 0 &&
                    brojTelefonaTb.Text.Length > 0 && jmbgTb.Text.Length > 0)
                {
                    List<Smerovi> pronadjeniSmer = kolekcijaSmerova.Find(Builders<Smerovi>.Filter.Eq("nazivSmera", smerTb.Text)).ToList();

                    if (smerTb.Text.Length == 0)
                    {
                        noviStudent.Smer = new Smerovi();
                        noviStudent.ImePrezime = imePrezimeTb.Text;
                        noviStudent.BrojIndeksa = (brojIndeksaTb.Text.Length > 0) ? brojIndeksaTb.Text : ("temp" + jmbgTb.Text);
                        noviStudent.DatumRodjenja = datumRodjenjaTb.Text;
                        noviStudent.JMBG = jmbgTb.Text;
                        noviStudent.BrojTelefona = brojTelefonaTb.Text;

                        novaIzbornaLista.BrojIndeksa = noviStudent.BrojIndeksa;
                        noviLogin.Password = noviStudent.ImePrezime.Split(' ')[0];
                        noviLogin.Username = noviStudent.BrojIndeksa;

                        if (!DuplikatStudenta(noviStudent.JMBG, noviStudent.BrojTelefona)
                            && noviStudent.JMBG.Equals(prethodnoSelektovanStudent.JMBG)
                            && noviStudent.BrojTelefona.Equals(prethodnoSelektovanStudent.BrojTelefona)
                            )
                        {

                            kolekcijaStudenata.FindOneAndReplace(Builders<Studenti>.Filter.Eq("jmbg", prethodnoSelektovanStudent.JMBG), noviStudent);
                            kolekcijaIzbornihListi.FindOneAndReplace(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", prethodnoSelektovanStudent.BrojIndeksa), novaIzbornaLista);
                            kolekcijaLogin.FindOneAndReplace(Builders<Login>.Filter.Eq("username", prethodnoSelektovanStudent.BrojIndeksa), noviLogin);

                            MessageBox.Show("Student " + noviStudent.ImePrezime + " uspesno azuriran!");
                            MessageBox.Show("Izborna lista uspesno azurirana!");
                            MessageBox.Show("Nalog za pristup studentskom servisu azuriran!");

                            buttonSaveIzmene.Enabled = false;
                            buttonAddStudent.Enabled = true;

                            GenerisiStudente();
                        }
                        else
                            MessageBox.Show("Student vec postoji u bazi!");
                    }
                    else
                    {
                        if (pronadjeniSmer.Count > 0)
                        {
                            noviStudent.Smer = pronadjeniSmer[0];
                            noviStudent.ImePrezime = imePrezimeTb.Text;
                            noviStudent.BrojIndeksa = (brojIndeksaTb.Text.Length > 0) ? brojIndeksaTb.Text : "";
                            noviStudent.DatumRodjenja = datumRodjenjaTb.Text;
                            noviStudent.JMBG = jmbgTb.Text;
                            noviStudent.BrojTelefona = brojTelefonaTb.Text;

                            novaIzbornaLista.BrojIndeksa = noviStudent.BrojIndeksa;
                            noviLogin.Password = noviStudent.ImePrezime.Split(' ')[0];
                            noviLogin.Username = noviStudent.BrojIndeksa;

                            if (!DuplikatStudenta(noviStudent.JMBG, noviStudent.BrojTelefona)
                            && noviStudent.JMBG.Equals(prethodnoSelektovanStudent.JMBG)
                            && noviStudent.BrojTelefona.Equals(prethodnoSelektovanStudent.BrojTelefona)
                            )
                            {

                                kolekcijaStudenata.FindOneAndReplace(Builders<Studenti>.Filter.Eq("jmbg", prethodnoSelektovanStudent.JMBG), noviStudent);
                                kolekcijaIzbornihListi.FindOneAndReplace(Builders<IzbornaLista>.Filter.Eq("brojIndeksa", prethodnoSelektovanStudent.BrojIndeksa), novaIzbornaLista);
                                kolekcijaLogin.FindOneAndReplace(Builders<Login>.Filter.Eq("username", prethodnoSelektovanStudent.BrojIndeksa), noviLogin);

                                MessageBox.Show("Student " + noviStudent.ImePrezime + " uspesno azuriran!");
                                MessageBox.Show("Izborna lista uspesno azurirana!");
                                MessageBox.Show("Nalog za pristup studentskom servisu azuriran!");

                                buttonSaveIzmene.Enabled = false;
                                buttonAddStudent.Enabled = true;

                                GenerisiStudente();
                            }
                            else
                                MessageBox.Show("Student vec postoji u bazi!");
                        }
                        else
                        {
                            noviStudent.Smer = new Smerovi();
                            MessageBox.Show("Uneti smer ne postoji!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Greska u unosu! Polja za ime i prezime, datum rodjenja, broj telefona, i JMBG su obavezna!");
                }
            }
        }

        private void ComboBoxPredmetChanged(object s, EventArgs e, IMongoCollection<Predmeti> kolekcijaPredmeta, GenerisiKontrole generisiKontrole)
        {
            buttonSaveIzmene.Enabled = false;
            buttonStatistika.Enabled = true;
            generisiKontrole(7);

            Button azuriranjePredmetaBtn = (Button)(mainPanel.Controls.Find("azuriranjePredmetaBtn", true))[0];
            Button brisanjePredmetaBtn = (Button)(mainPanel.Controls.Find("brisanjePredmetaBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            sacuvajIzmeneBtn.Enabled = false;

            string SelektovanNazivPredmeta = comboBoxPredmeti.SelectedItem.ToString();
            Predmeti predmet = kolekcijaPredmeta.Find(Builders<Predmeti>.Filter.Eq("nazivPredmeta", SelektovanNazivPredmeta))
                .First();

            TextBox sifraPredmetaTb = mainPanel.Controls.Find("sifraPredmetaTb", false)[0] as TextBox;
            TextBox nazivPredmetaTb = mainPanel.Controls.Find("nazivPredmetaTb", false)[0] as TextBox;
            TextBox profesorTb = mainPanel.Controls.Find("profesorTb", false)[0] as TextBox;
            TextBox obavezanTb = mainPanel.Controls.Find("obavezanTb", false)[0] as TextBox;
            TextBox espbTb = mainPanel.Controls.Find("espbTb", false)[0] as TextBox;
            TextBox smeroviTb = mainPanel.Controls.Find("smeroviTb", false)[0] as TextBox;
            TextBox semestarTb = mainPanel.Controls.Find("semestarTb", false)[0] as TextBox;

            sifraPredmetaTb.Text = predmet.SifraPredmeta;
            nazivPredmetaTb.Text = predmet.NazivPredmeta;
            profesorTb.Text = predmet.Profesor;
            obavezanTb.Text = predmet.Obavezan ? "Obavezan" : "Neobavezan";
            espbTb.Text = predmet.ESPB.ToString();

            foreach(var smer in predmet.SmeroviPredmeta)
            {
                smeroviTb.Text += smer.NazivSmera + ", ";
            }

            if(smeroviTb.Text.Length > 2)
                smeroviTb.Text = smeroviTb.Text.Remove(smeroviTb.Text.Length - 2, 2);
            semestarTb.Text = predmet.Semestar.ToString();

            sifraPredmetaTb.Enabled = nazivPredmetaTb.Enabled = profesorTb.Enabled = obavezanTb.Enabled = espbTb.Enabled =
                smeroviTb.Enabled = semestarTb.Enabled = false;

            azuriranjePredmetaBtn.Click += (sender, ea) => AzuriranjePredmeta(predmet, sifraPredmetaTb, nazivPredmetaTb, profesorTb, obavezanTb, espbTb, smeroviTb, semestarTb);
            brisanjePredmetaBtn.Click += (sender, ea) => BrisanjePredmeta(predmet);
        }

        private void BrisanjePredmeta(Predmeti prethodnoSelektovanPredmet)
        {
            throw new NotImplementedException();
        }

        private void AzuriranjePredmeta(Predmeti predmet, TextBox sifraPredmetaTb, TextBox nazivPredmetaTb, TextBox profesorTb, TextBox obavezanTb, TextBox espbTb, TextBox smeroviTb, TextBox semestarTb)
        {
            Button azuriranjePredmetaBtn = (Button)(mainPanel.Controls.Find("azuriranjePredmetaBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            sifraPredmetaTb.Enabled = nazivPredmetaTb.Enabled = profesorTb.Enabled = obavezanTb.Enabled = espbTb.Enabled =
                smeroviTb.Enabled = semestarTb.Enabled = true;

            sacuvajIzmeneBtn.Enabled = true;
            azuriranjePredmetaBtn.Enabled = false;

            flagZaSaveDugme = "predmet";
            sacuvajIzmeneBtn.Click += (sender, ea) => SnimanjePredmeta(predmet);
        }

        private void SnimanjePredmeta(Predmeti postojeciPredmet)
        {
            // if( predmet != null ) => AŽURIRANJE

            TextBox sifraPredmetaTb = mainPanel.Controls.Find("sifraPredmetaTb", false)[0] as TextBox;
            TextBox nazivPredmetaTb = mainPanel.Controls.Find("nazivPredmetaTb", false)[0] as TextBox;
            TextBox profesorTb = mainPanel.Controls.Find("profesorTb", false)[0] as TextBox;
            TextBox obavezanTb = mainPanel.Controls.Find("obavezanTb", false)[0] as TextBox;
            TextBox espbTb = mainPanel.Controls.Find("espbTb", false)[0] as TextBox;
            TextBox smeroviTb = mainPanel.Controls.Find("smeroviTb", false)[0] as TextBox;
            TextBox semestarTb = mainPanel.Controls.Find("semestarTb", false)[0] as TextBox;

            int semestar = 0;
            int espb = 0;

            bool errorFlag = false;

            Predmeti noviPredmet = new Predmeti();

            if (sifraPredmetaTb.Text.Length == 0 || nazivPredmetaTb.Text.Length == 0 ||
                obavezanTb.Text.Length == 0 ||
                espbTb.Text.Length == 0 || semestarTb.Text.Length == 0)
            {
                MessageBox.Show("Sva polja osim Profesor i Smerovi su obavezna! Pokusajte ponovo.");
            }
            else
            {
                if (Int32.TryParse(espbTb.Text, out espb)
                && Int32.TryParse(semestarTb.Text, out semestar))
                {
                    string[] listaSmerovaString;
                    List<Smerovi> listaSmerova = new List<Smerovi>();

                    noviPredmet.ESPB = espb;
                    noviPredmet.SifraPredmeta = sifraPredmetaTb.Text;
                    noviPredmet.NazivPredmeta = nazivPredmetaTb.Text;
                    noviPredmet.Obavezan = (obavezanTb.Text.Equals("Obavezan")) ? true : false;
                    noviPredmet.Semestar = semestar;

                    if (profesorTb.Text.Length == 0) noviPredmet.Profesor = ""; else noviPredmet.Profesor = profesorTb.Text;
                    if (smeroviTb.Text.Length == 0) noviPredmet.SmeroviPredmeta = new List<Smerovi>();
                    else
                    {
                        listaSmerovaString = smeroviTb.Text.Split(',');

                        foreach (string smer in listaSmerovaString)
                        {
                            List<Smerovi> pronadjeniSmer = kolekcijaSmerova
                                .Find(Builders<Smerovi>.Filter.Eq("nazivSmera", smer.Trim())).ToList();
                            if (pronadjeniSmer.Count > 0)
                                listaSmerova.Add(pronadjeniSmer[0]);
                            else
                            {
                                MessageBox.Show("Uneti smerovi su neispravni ili nepostojeci!");
                                listaSmerova = null;
                                errorFlag = true;
                                break;
                            }

                        }

                        noviPredmet.SmeroviPredmeta = listaSmerova;
                    }

                    if (!errorFlag)
                    {
                        if(postojeciPredmet != null)
                        {
                            if (DuplikatPredmeta(noviPredmet.SifraPredmeta) && noviPredmet.SifraPredmeta.Equals(postojeciPredmet.SifraPredmeta))
                            {
                                noviPredmet.ID = postojeciPredmet.ID;
                                kolekcijaPredmeta.FindOneAndReplace(Builders<Predmeti>.Filter.Eq("sifraPredmeta", postojeciPredmet.SifraPredmeta), noviPredmet);
                                MessageBox.Show("Predmet " + noviPredmet.NazivPredmeta + " uspesno azuriran!");
                            }
                            else
                                MessageBox.Show("Predmet sa sifrom " + noviPredmet.SifraPredmeta + " vec postoji u bazi!");
                        }
                        else
                        {
                            if (!DuplikatPredmeta(noviPredmet.SifraPredmeta))
                            {
                                kolekcijaPredmeta.InsertOne(noviPredmet);
                                MessageBox.Show("Predmet " + noviPredmet.NazivPredmeta + " uspesno dodat!");
                            }
                            else
                                MessageBox.Show("Predmet sa sifrom " + noviPredmet.SifraPredmeta + " vec postoji u bazi!");
                        }

                        sifraPredmetaTb.Enabled = nazivPredmetaTb.Enabled = profesorTb.Enabled =
                            obavezanTb.Enabled = espbTb.Enabled = smeroviTb.Enabled = semestarTb.Enabled = false;

                        buttonSaveIzmene.Enabled = false;
                        buttonAddPredmet.Enabled = true;

                        GenerisiPredmete();
                    }
                }
                else
                {
                    MessageBox.Show("Greska u unosu - sifra predmeta, ESPB, i semestar moraju biti brojevi!");
                }
            }
        }

        private void ComboBoxSmerChanged(object s, EventArgs e, IMongoCollection<Smerovi> kolekcijaSmerova, GenerisiKontrole generisiKontrole)
        {
            buttonSaveIzmene.Enabled = false;
            buttonStatistika.Enabled = false;
            generisiKontrole(2);

            Button azuriranjeSmeraBtn = (Button)(mainPanel.Controls.Find("azuriranjeSmeraBtn", true))[0];
            Button brisanjeSmeraBtn = (Button)(mainPanel.Controls.Find("brisanjeSmeraBtn", true))[0];
            Button prikazStatistikeBtn = (Button)(mainPanel.Controls.Find("prikazStatistikeBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            sacuvajIzmeneBtn.Enabled = false;

            string SelektovanNazivSmera = comboBoxSmerovi.SelectedItem.ToString();
            Smerovi smer = kolekcijaSmerova.Find(Builders<Smerovi>.Filter.Eq("nazivSmera", SelektovanNazivSmera))
                .First();

            TextBox idSmeraTb = mainPanel.Controls.Find("idSmeraTb", false)[0] as TextBox;
            TextBox nazivSmeraTb = mainPanel.Controls.Find("nazivSmeraTb", false)[0] as TextBox;

            idSmeraTb.Text = smer.SifraSmera.ToString();
            nazivSmeraTb.Text = smer.NazivSmera;

            idSmeraTb.Enabled = nazivSmeraTb.Enabled = false;

            azuriranjeSmeraBtn.Click += (sender, ea) => AzuriranjeSmera(smer, idSmeraTb, nazivSmeraTb);
            brisanjeSmeraBtn.Click += (sender, ea) => BrisanjeSmera(smer);
        }

        private void BrisanjeSmera(Smerovi smer)
        {
            throw new NotImplementedException();
        }

        private void AzuriranjeSmera(Smerovi prethodnoSelektovanStudent, TextBox idSmeraTb, TextBox nazivSmeraTb)
        {
            Button azuriranjeSmeraBtn = (Button)(mainPanel.Controls.Find("azuriranjeSmeraBtn", true))[0];
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];

            idSmeraTb.Enabled = nazivSmeraTb.Enabled = true;
            azuriranjeSmeraBtn.Enabled = false;
            sacuvajIzmeneBtn.Enabled = true;

            sacuvajIzmeneBtn.Click += (sender, ea) => SnimanjeSmera(prethodnoSelektovanStudent);
            flagZaSaveDugme = "smer";
        }

        private void SnimanjeSmera(Smerovi prethodnoSelektovanSmer)
        {
            if (flagZaSaveDugme.Equals("smer"))
            {
                int noviIdSmera = 0;
                TextBox idSmeraTb = mainPanel.Controls.Find("idSmeraTb", false)[0] as TextBox;
                TextBox nazivSmeraTb = mainPanel.Controls.Find("nazivSmeraTb", false)[0] as TextBox;

                Smerovi noviSmer = new Smerovi();
                noviSmer.ID = prethodnoSelektovanSmer.ID;
                noviSmer.NazivSmera = nazivSmeraTb.Text;

                if (Int32.TryParse(idSmeraTb.Text, out noviIdSmera))
                {
                    if (!DuplikatSmera(noviIdSmera) && noviIdSmera == prethodnoSelektovanSmer.SifraSmera)
                    {
                        noviSmer.SifraSmera = noviIdSmera;
                        kolekcijaSmerova.FindOneAndReplace(Builders<Smerovi>.Filter.Eq("sifraSmera", prethodnoSelektovanSmer.SifraSmera), noviSmer);

                        MessageBox.Show("Smer " + noviSmer.NazivSmera + " uspesno izmenjen!");
                        idSmeraTb.Enabled = false;
                        nazivSmeraTb.Enabled = false;

                        ((Button)(mainPanel.Controls.Find("brisanjeSmeraBtn", true))[0]).Enabled = false;
                        ((Button)(mainPanel.Controls.Find("prikazStatistikeBtn", true))[0]).Enabled = false;
                        ((Button)(mainPanel.Controls.Find("prikazStatistikeBtn", true))[0]).Enabled = false;
                        ((Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0]).Enabled = false;

                        GenerisiSmerove();
                        GenerisiPredmete();
                        GenerisiStudente();
                    }
                    else
                        MessageBox.Show("Vec postoji smer sa sifrom " + noviIdSmera);
                }
                else
                {
                    MessageBox.Show("Sifra smera mora biti broj! Pokusajte ponovo.");
                }
            }
        }

        private bool DuplikatSmera(int idZaProveru)
        {
            bool result = false;
            foreach(Smerovi s in sviSmerovi)
            {
                if(s.SifraSmera.Equals(idZaProveru))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool DuplikatStudenta(string jmbgZaProveru, string brojTelefonaZaProveru)
        {
            bool result = false;
            foreach (Studenti s in sviStudenti)
            {
                if (s.JMBG.Equals(jmbgZaProveru) || s.BrojTelefona.Equals(brojTelefonaZaProveru))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool DuplikatPredmeta(string sifraZaProveru)
        {
            bool result = false;
            foreach (Predmeti p in sviPredmeti)
            {
                if (p.SifraPredmeta.Equals(sifraZaProveru))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private void GenerisiKontroleZaSmerove(int brojKontrola)
        {
            mainPanel.Controls.Clear();

            List<Label> labelList = new List<Label>();
            List<TextBox> textBoxList = new List<TextBox>();
            Button azuriranjeSmeraBtn = new Button();
            Button brisanjeSmeraBtn = new Button();
            Button prikazStatistikeBtn = new Button();
            Button sacuvajIzmeneBtn = new Button();

            Label idSmera = new Label(), nazivSmera = new Label();
            idSmera.Text = "Šifra smera: ";
            nazivSmera.Text = "Naziv smera: ";

            TextBox idSmeraTb = new TextBox(), nazivSmeraTb = new TextBox();
            idSmeraTb.Name = "idSmeraTb"; nazivSmeraTb.Name = "nazivSmeraTb";

            labelList.Add(idSmera);
            labelList.Add(nazivSmera);

            textBoxList.Add(idSmeraTb);
            textBoxList.Add(nazivSmeraTb);

            foreach(Label label in labelList)
            {
                label.Location = new Point(25, (pocetnaYOsa + (razmakYOsa += 30)));
                mainPanel.Controls.Add(label);
            }

            razmakYOsa = 0;

            foreach (TextBox textBox in textBoxList)
            {
                textBox.Location = new Point(140, (pocetnaYOsa + (razmakYOsa += 30)));
                textBox.Size = new Size(250, 20);
                mainPanel.Controls.Add(textBox);
            }

            azuriranjeSmeraBtn.Text = "Ažuriranje smera";
            azuriranjeSmeraBtn.Size = new Size(100, 50);
            azuriranjeSmeraBtn.Location = new Point(25, (pocetnaYOsa + (razmakYOsa += 75)));
            azuriranjeSmeraBtn.Name = "azuriranjeSmeraBtn";

            brisanjeSmeraBtn.Text = "Brisanje smera";
            brisanjeSmeraBtn.Size = new Size(100, 50);
            brisanjeSmeraBtn.Location = new Point(150, (pocetnaYOsa + razmakYOsa));
            brisanjeSmeraBtn.Name = "brisanjeSmeraBtn";

            prikazStatistikeBtn.Text = "Prikaz statistike smera";
            prikazStatistikeBtn.Size = new Size(100, 50);
            prikazStatistikeBtn.Location = new Point(275, (pocetnaYOsa + razmakYOsa));
            prikazStatistikeBtn.Name = "prikazStatistikeBtn";

            sacuvajIzmeneBtn.Text = "Sačuvaj izmene";
            sacuvajIzmeneBtn.Size = new Size(100, 50);
            sacuvajIzmeneBtn.Location = new Point(25, (pocetnaYOsa + razmakYOsa + 75));
            sacuvajIzmeneBtn.Name = "sacuvajIzmeneBtn";

            mainPanel.Controls.Add(azuriranjeSmeraBtn);
            mainPanel.Controls.Add(brisanjeSmeraBtn);
            mainPanel.Controls.Add(prikazStatistikeBtn);
            mainPanel.Controls.Add(sacuvajIzmeneBtn);

            razmakYOsa = 0;
        }

        private void GenerisiKontroleZaPredmete(int brojKontrola)
        {
            mainPanel.Controls.Clear();

            List<Label> labelList = new List<Label>();
            List<TextBox> textBoxList = new List<TextBox>();

            Button azuriranjePredmetaBtn = new Button();
            Button brisanjePredmetaBtn = new Button();
            Button sacuvajIzmeneBtn = new Button();

            Label sifraPredmeta = new Label(), nazivPredmeta = new Label(),
                profesor = new Label(), smerovi = new Label(),
                espb = new Label(), obavezan = new Label(),
                semestar = new Label();

            sifraPredmeta.Text = "Šifra predmeta: ";
            nazivPredmeta.Text = "Naziv predmeta: ";
            profesor.Text = "Predmetni profesor: ";
            smerovi.Text = "Smer(ovi): ";
            espb.Text = "ESPB: ";
            obavezan.Text = "Obavezan: ";
            semestar.Text = "Semestar: ";

            TextBox sifraPredmetaTb = new TextBox(), nazivPredmetaTb = new TextBox(),
               profesorTb = new TextBox(), smeroviTb = new TextBox(),
               espbTb = new TextBox(), obavezanTb = new TextBox(),
               semestarTb = new TextBox();

            sifraPredmetaTb.Name = "sifraPredmetaTb"; nazivPredmetaTb.Name = "nazivPredmetaTb";
            profesorTb.Name = "profesorTb"; smeroviTb.Name = "smeroviTb";
            espbTb.Name = "espbTb"; obavezanTb.Name = "obavezanTb";
            semestarTb.Name = "semestarTb";

            labelList.Add(sifraPredmeta); labelList.Add(nazivPredmeta);
            labelList.Add(profesor); labelList.Add(smerovi);
            labelList.Add(espb); labelList.Add(obavezan);
            labelList.Add(semestar);

            textBoxList.Add(sifraPredmetaTb); textBoxList.Add(nazivPredmetaTb);
            textBoxList.Add(profesorTb); textBoxList.Add(smeroviTb);
            textBoxList.Add(espbTb); textBoxList.Add(obavezanTb);
            textBoxList.Add(semestarTb);

            foreach (Label label in labelList)
            {
                label.Location = new Point(25, (pocetnaYOsa + (razmakYOsa += 30)));
                mainPanel.Controls.Add(label);
            }

            razmakYOsa = 0;

            foreach (TextBox textBox in textBoxList)
            {
                textBox.Location = new Point(140, (pocetnaYOsa + (razmakYOsa += 30)));
                textBox.Size = new Size(250, 20);
                mainPanel.Controls.Add(textBox);
            }

            azuriranjePredmetaBtn.Text = "Ažuriranje predmeta";
            azuriranjePredmetaBtn.Size = new Size(100, 50);
            azuriranjePredmetaBtn.Location = new Point(25, (pocetnaYOsa + (razmakYOsa += 75)));
            azuriranjePredmetaBtn.Name = "azuriranjePredmetaBtn";

            brisanjePredmetaBtn.Text = "Brisanje predmeta";
            brisanjePredmetaBtn.Size = new Size(100, 50);
            brisanjePredmetaBtn.Location = new Point(150, (pocetnaYOsa + razmakYOsa));
            brisanjePredmetaBtn.Name = "brisanjePredmetaBtn";

            sacuvajIzmeneBtn.Text = "Sačuvaj izmene";
            sacuvajIzmeneBtn.Size = new Size(100, 50);
            sacuvajIzmeneBtn.Location = new Point(25, (pocetnaYOsa + razmakYOsa + 75));
            sacuvajIzmeneBtn.Name = "sacuvajIzmeneBtn";

            mainPanel.Controls.Add(azuriranjePredmetaBtn);
            mainPanel.Controls.Add(brisanjePredmetaBtn);
            mainPanel.Controls.Add(sacuvajIzmeneBtn);

            razmakYOsa = 0;
        }

        private void GenerisiKontroleZaStudente(int brojKontrola)
        {
            mainPanel.Controls.Clear();

            List<Label> labelList = new List<Label>();
            List<TextBox> textBoxList = new List<TextBox>();

            Button azuriranjeStudentaBtn = new Button();
            Button brisanjeStudentaBtn = new Button();
            Button izbornaListaBtn = new Button();
            Button sacuvajIzmeneBtn = new Button();

            Label imePrezime = new Label(), brojIndeksa = new Label(),
                datumRodjenja = new Label(), smer = new Label(),
                brojTelefona = new Label(), jmbg = new Label();
            imePrezime.Text = "Ime i prezime: ";
            brojIndeksa.Text = "Broj indeksa: ";
            datumRodjenja.Text = "Datum rodjenja: ";
            smer.Text = "Smer: ";
            brojTelefona.Text = "Kontakt telefon: ";
            jmbg.Text = "JMBG: ";

            labelList.Add(imePrezime); labelList.Add(brojIndeksa);
            labelList.Add(datumRodjenja); labelList.Add(smer);
            labelList.Add(brojTelefona); labelList.Add(jmbg);

            TextBox imePrezimeTb = new TextBox(), brojIndeksaTb = new TextBox(),
               datumRodjenjaTb = new TextBox(), smerTb = new TextBox(),
               brojTelefonaTb = new TextBox(), jmbgTb = new TextBox();

            imePrezimeTb.Name = "imePrezimeTb"; brojIndeksaTb.Name = "brojIndeksaTb";
            datumRodjenjaTb.Name = "datumRodjenjaTb"; smerTb.Name = "smerTb";
            brojTelefonaTb.Name = "brojTelefonaTb"; jmbgTb.Name = "jmbgTb";

            textBoxList.Add(imePrezimeTb); textBoxList.Add(brojIndeksaTb);
            textBoxList.Add(datumRodjenjaTb); textBoxList.Add(smerTb);
            textBoxList.Add(brojTelefonaTb); textBoxList.Add(jmbgTb);

            foreach (Label label in labelList)
            {
                label.Location = new Point(25, (pocetnaYOsa + (razmakYOsa += 30)));
                mainPanel.Controls.Add(label);
            }

            razmakYOsa = 0;

            foreach (TextBox textBox in textBoxList)
            {
                textBox.Location = new Point(140, (pocetnaYOsa + (razmakYOsa += 30)));
                textBox.Size = new Size(250, 20);
                mainPanel.Controls.Add(textBox);
            }

            azuriranjeStudentaBtn.Text = "Ažuriranje studenta";
            azuriranjeStudentaBtn.Size = new Size(100, 50);
            azuriranjeStudentaBtn.Location = new Point(25, (pocetnaYOsa + (razmakYOsa += 50)));
            azuriranjeStudentaBtn.Name = "azuriranjeStudentaBtn";

            brisanjeStudentaBtn.Text = "Brisanje studenta";
            brisanjeStudentaBtn.Size = new Size(100, 50);
            brisanjeStudentaBtn.Location = new Point(150, (pocetnaYOsa + razmakYOsa));
            brisanjeStudentaBtn.Name = "brisanjeStudentaBtn";

            izbornaListaBtn.Text = "Izborna lista studenta";
            izbornaListaBtn.Size = new Size(100, 50);
            izbornaListaBtn.Location = new Point(275, (pocetnaYOsa + razmakYOsa));
            izbornaListaBtn.Name = "izbornaListaBtn";

            sacuvajIzmeneBtn.Text = "Sačuvaj izmene";
            sacuvajIzmeneBtn.Size = new Size(100, 50);
            sacuvajIzmeneBtn.Location = new Point(25, (pocetnaYOsa + razmakYOsa + 75));
            sacuvajIzmeneBtn.Name = "sacuvajIzmeneBtn";

            mainPanel.Controls.Add(azuriranjeStudentaBtn);
            mainPanel.Controls.Add(brisanjeStudentaBtn);
            mainPanel.Controls.Add(izbornaListaBtn);
            mainPanel.Controls.Add(sacuvajIzmeneBtn);

            razmakYOsa = 0;
        }

        private void buttonIzbornaListaStudenta_Click(object sender, EventArgs e)
        {
            
        }

    }
}