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

                Smerovi smer = new Smerovi();
                smer.NazivSmera = nazivSmeraTb.Text;

                if (Int32.TryParse(idSmeraTb.Text, out noviIdSmera))
                {
                    smer.SifraSmera = noviIdSmera;
                    kolekcijaSmerova.InsertOne(smer);

                    MessageBox.Show("Smer " + smer.NazivSmera + " uspesno dodat!");
                    idSmeraTb.Enabled = false;
                    nazivSmeraTb.Enabled = false;

                    buttonSaveIzmene.Enabled = false;
                    buttonAddSmer.Enabled = true;

                    GenerisiSmerove();
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
            {
                TextBox sifraPredmetaTb = mainPanel.Controls.Find("sifraPredmetaTb", false)[0] as TextBox;
                TextBox nazivPredmetaTb = mainPanel.Controls.Find("nazivPredmetaTb", false)[0] as TextBox;
                TextBox profesorTb = mainPanel.Controls.Find("profesorTb", false)[0] as TextBox;
                TextBox obavezanTb = mainPanel.Controls.Find("obavezanTb", false)[0] as TextBox;
                TextBox espbTb = mainPanel.Controls.Find("espbTb", false)[0] as TextBox;
                TextBox smeroviTb = mainPanel.Controls.Find("smeroviTb", false)[0] as TextBox;
                TextBox semestarTb = mainPanel.Controls.Find("semestarTb", false)[0] as TextBox;

                int sifraPredmeta = 0;
                int semestar = 0;
                int espb = 0;

                bool errorFlag = false;

                Predmeti predmet = new Predmeti();

                if(sifraPredmetaTb.Text.Length == 0 || nazivPredmetaTb.Text.Length == 0 ||
                    obavezanTb.Text.Length == 0 ||
                    espbTb.Text.Length == 0 || semestarTb.Text.Length == 0)
                {
                    MessageBox.Show("Sva polja osim Profesor i Smerovi su obavezna! Pokusajte ponovo.");
                }
                else
                {
                    if (Int32.TryParse(sifraPredmetaTb.Text, out sifraPredmeta) && Int32.TryParse(espbTb.Text, out espb)
                    && Int32.TryParse(semestarTb.Text, out semestar))
                    {
                        string[] listaSmerovaString;
                        List<Smerovi> listaSmerova = new List<Smerovi>();

                        predmet.ESPB = espb;
                        predmet.SifraPredmeta = sifraPredmeta.ToString();
                        predmet.NazivPredmeta = nazivPredmetaTb.Text;
                        predmet.Obavezan = (obavezanTb.Text.Equals("Obavezan")) ? true : false;
                        predmet.Semestar = semestar;

                        if (profesorTb.Text.Length == 0) predmet.Profesor = "";
                        if (smeroviTb.Text.Length == 0) predmet.SmeroviPredmeta = new List<Smerovi>();
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

                            predmet.SmeroviPredmeta = listaSmerova;
                        }
                            
                        if(!errorFlag)
                        {
                            kolekcijaPredmeta.InsertOne(predmet);

                            MessageBox.Show("Predmet " + predmet.NazivPredmeta + " uspesno dodat!");
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

                Studenti student = new Studenti();

                if(imePrezimeTb.Text.Length > 0 && datumRodjenjaTb.Text.Length > 0 &&
                    brojTelefonaTb.Text.Length > 0 && jmbgTb.Text.Length > 0)
                {
                    List<Smerovi> pronadjeniSmer = kolekcijaSmerova.Find(Builders<Smerovi>.Filter.Eq("nazivSmera", smerTb.Text)).ToList();

                    if (smerTb.Text.Length == 0)
                    {
                        student.Smer = new Smerovi();
                        student.ImePrezime = imePrezimeTb.Text;
                        student.BrojIndeksa = (brojIndeksaTb.Text.Length > 0) ? brojIndeksaTb.Text : "temp";
                        student.DatumRodjenja = datumRodjenjaTb.Text;
                        student.JMBG = jmbgTb.Text;
                        student.BrojTelefona = brojTelefonaTb.Text;

                        IzbornaLista izbornaLista = new IzbornaLista();
                        izbornaLista.Predmeti = new List<Predmeti>();
                        izbornaLista.PredmetiDrugihSmerova = new List<Predmeti>();
                        izbornaLista.BrojIndeksa = ((student.BrojIndeksa.Length > 0) ? student.BrojIndeksa : "temp");

                        Login login = new Login();
                        login.AccountStatus = "student";
                        login.Username = (student.BrojIndeksa.Length > 0) ? student.BrojIndeksa : student.ImePrezime.Split(' ')[0];
                        login.Password = student.ImePrezime.Split(' ')[0];

                        kolekcijaStudenata.InsertOne(student);
                        kolekcijaIzbornihListi.InsertOne(izbornaLista);
                        kolekcijaLogin.InsertOne(login);

                        MessageBox.Show("Student " + student.ImePrezime + " uspesno dodat!");
                        MessageBox.Show("Izborna lista uspesno kreirana!");
                        MessageBox.Show("Nalog za pristup studentskom servisu kreiran!");

                        buttonSaveIzmene.Enabled = false;
                        buttonAddStudent.Enabled = true;

                        GenerisiStudente();
                    }     
                    else
                    {
                        if (pronadjeniSmer.Count > 0)
                        {
                            student.Smer = pronadjeniSmer[0];
                            student.ImePrezime = imePrezimeTb.Text;
                            student.BrojIndeksa = (brojIndeksaTb.Text.Length > 0) ? brojIndeksaTb.Text : "";
                            student.DatumRodjenja = datumRodjenjaTb.Text;
                            student.JMBG = jmbgTb.Text;
                            student.BrojTelefona = brojTelefonaTb.Text;

                            IzbornaLista izbornaLista = new IzbornaLista();
                            izbornaLista.Predmeti = new List<Predmeti>();
                            izbornaLista.PredmetiDrugihSmerova = new List<Predmeti>();
                            izbornaLista.BrojIndeksa = ((student.BrojIndeksa.Length > 0) ? student.BrojIndeksa : "");

                            Login login = new Login();
                            login.AccountStatus = "student";
                            login.Username = (student.BrojIndeksa.Length > 0) ? student.BrojIndeksa : student.ImePrezime.Split(' ')[0];
                            login.Password = student.ImePrezime.Split(' ')[0];

                            kolekcijaStudenata.InsertOne(student);
                            kolekcijaIzbornihListi.InsertOne(izbornaLista);
                            kolekcijaLogin.InsertOne(login);

                            MessageBox.Show("Student " + student.ImePrezime + " uspesno dodat!");
                            MessageBox.Show("Izborna lista uspesno kreirana!");
                            MessageBox.Show("Nalog za pristup studentskom servisu kreiran!");

                            buttonSaveIzmene.Enabled = false;
                            buttonAddStudent.Enabled = true;

                            GenerisiStudente();
                        }
                        else
                        {
                            student.Smer = new Smerovi();
                            MessageBox.Show("Uneti smer ne postoji!");
                        }
                    }             
                }
                else
                {
                    MessageBox.Show("Greska u unosu! Polja za ime i prezime, datum rodjenja, broj telefona i JMBG su obavezna!");
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

            azuriranjeStudentaBtn.Click += (sender, ea) => AzuriranjeStudenta(imePrezimeTb, brojIndeksaTb, datumRodjenjaTb, smerTb, brojTelefonaTb, jmbgTb);
            brisanjeStudentaBtn.Click += (sender, ea) => BrisanjeStudenta(student);
        }

        private void BrisanjeStudenta(Studenti student)
        {
            throw new NotImplementedException();
        }

        private void AzuriranjeStudenta(TextBox imePrezimeTb, TextBox brojIndeksaTb, TextBox datumRodjenjaTb, TextBox smerTb, TextBox brojTelefonaTb, TextBox jmbgTb)
        {
            imePrezimeTb.Enabled = brojIndeksaTb.Enabled = datumRodjenjaTb.Enabled = smerTb.Enabled =
                brojTelefonaTb.Enabled = jmbgTb.Enabled = true;

            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];
            sacuvajIzmeneBtn.Enabled = true;

            sacuvajIzmeneBtn.Click += (sender, ea) => SnimanjeStudenta();
        }

        private void SnimanjeStudenta()
        {
           
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

            azuriranjePredmetaBtn.Click += (sender, ea) => AzuriranjePredmeta(sifraPredmetaTb, nazivPredmetaTb, profesorTb, obavezanTb, espbTb, smeroviTb, semestarTb);
            brisanjePredmetaBtn.Click += (sender, ea) => BrisanjePredmeta(predmet);
        }

        private void BrisanjePredmeta(Predmeti predmet)
        {
            throw new NotImplementedException();
        }

        private void AzuriranjePredmeta(TextBox sifraPredmetaTb, TextBox nazivPredmetaTb, TextBox profesorTb, TextBox obavezanTb, TextBox espbTb, TextBox smeroviTb, TextBox semestarTb)
        {
            sifraPredmetaTb.Enabled = nazivPredmetaTb.Enabled = profesorTb.Enabled = obavezanTb.Enabled = espbTb.Enabled =
                smeroviTb.Enabled = semestarTb.Enabled = true;

            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];
            sacuvajIzmeneBtn.Enabled = true;

            sacuvajIzmeneBtn.Click += (sender, ea) => SnimanjePredmeta();
        }

        private void SnimanjePredmeta()
        {
            
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

            azuriranjeSmeraBtn.Click += (sender, ea) => AzuriranjeSmera(idSmeraTb, nazivSmeraTb);
            brisanjeSmeraBtn.Click += (sender, ea) => BrisanjeSmera(smer);
        }

        private void BrisanjeSmera(Smerovi smer)
        {
            throw new NotImplementedException();
        }

        private void AzuriranjeSmera(TextBox idSmeraTb, TextBox nazivSmeraTb)
        {
            idSmeraTb.Enabled = nazivSmeraTb.Enabled = true;
            Button sacuvajIzmeneBtn = (Button)(mainPanel.Controls.Find("sacuvajIzmeneBtn", true))[0];
            sacuvajIzmeneBtn.Enabled = true;

            sacuvajIzmeneBtn.Click += (sender, ea) => SnimanjeSmera();
        }

        private void SnimanjeSmera()
        {
            
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