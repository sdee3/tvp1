using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace tvp_projekat1
{
    public partial class statForm : Form
    {
        private Predmeti selektovanPredmet;
        private List<IzbornaLista> sveIzborneListe;
        private int brojStudenataIzabralo;
        private int ukupnoStudenata;

        private Rectangle rectangleKruga;

        public statForm()
        {
            InitializeComponent();
        }

        private void statForm_Paint(object sender, PaintEventArgs e)
        {
            float procenatStudenata = ((brojStudenataIzabralo * (float)3.6) * 360) / (ukupnoStudenata * (float)3.6);

            rectangleKruga = new Rectangle(150, 100, 120, 120);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.DrawEllipse(Pens.Black, rectangleKruga);
            e.Graphics.FillEllipse(Brushes.Black, rectangleKruga);
            e.Graphics.FillPie(Brushes.Aqua, rectangleKruga, -90, procenatStudenata);
        }

        private void statForm_Load(object sender, EventArgs e)
        {
            foreach (IzbornaLista i in sveIzborneListe)
            {
                foreach (Predmeti p in i.Predmeti)
                    if (p.NazivPredmeta.Equals(selektovanPredmet.NazivPredmeta))
                        brojStudenataIzabralo++;
                foreach (Predmeti p in i.PredmetiDrugihSmerova)
                    if (p.NazivPredmeta.Equals(selektovanPredmet.NazivPredmeta))
                        brojStudenataIzabralo++;
            }

            labelPredmetIzabralo.Text = "Predmet " + selektovanPredmet.NazivPredmeta + " je izabralo: " 
                + brojStudenataIzabralo + " od " + ukupnoStudenata + " studenata.";

            this.Invalidate();
            this.Update();
        }

        internal void SetPredmet(Predmeti predmet, int ukupnoStudenata, List<IzbornaLista> sveIzborneListe)
        {
            this.brojStudenataIzabralo = 0;
            this.selektovanPredmet = predmet;
            this.ukupnoStudenata = ukupnoStudenata;
            this.sveIzborneListe = sveIzborneListe;
        }
    }
}