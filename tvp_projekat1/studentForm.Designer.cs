namespace tvp_projekat1
{
    partial class studentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxPredmetiDrugihSmerova = new System.Windows.Forms.ComboBox();
            this.labelPredmetiDrugihSmerova = new System.Windows.Forms.Label();
            this.c = new System.Windows.Forms.Label();
            this.labelPredmeti = new System.Windows.Forms.Label();
            this.labelUkupnoESPBtxt = new System.Windows.Forms.Label();
            this.labelSumaESPB = new System.Windows.Forms.Label();
            this.buttonPrijaviPredmete = new System.Windows.Forms.Button();
            this.labelImePrezime = new System.Windows.Forms.Label();
            this.labelBrIndeksa = new System.Windows.Forms.Label();
            this.labelJMBG = new System.Windows.Forms.Label();
            this.labelDatumRodjenja = new System.Windows.Forms.Label();
            this.labelBrojTelefona = new System.Windows.Forms.Label();
            this.labelSmer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxPredmetiDrugihSmerova
            // 
            this.comboBoxPredmetiDrugihSmerova.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPredmetiDrugihSmerova.FormattingEnabled = true;
            this.comboBoxPredmetiDrugihSmerova.Location = new System.Drawing.Point(529, 341);
            this.comboBoxPredmetiDrugihSmerova.Name = "comboBoxPredmetiDrugihSmerova";
            this.comboBoxPredmetiDrugihSmerova.Size = new System.Drawing.Size(193, 21);
            this.comboBoxPredmetiDrugihSmerova.TabIndex = 0;
            this.comboBoxPredmetiDrugihSmerova.SelectionChangeCommitted += new System.EventHandler(this.ComboBoxPredmetiDrugihSmerova_SelectedValueChanged);
            // 
            // labelPredmetiDrugihSmerova
            // 
            this.labelPredmetiDrugihSmerova.AutoSize = true;
            this.labelPredmetiDrugihSmerova.Location = new System.Drawing.Point(526, 310);
            this.labelPredmetiDrugihSmerova.Name = "labelPredmetiDrugihSmerova";
            this.labelPredmetiDrugihSmerova.Size = new System.Drawing.Size(196, 13);
            this.labelPredmetiDrugihSmerova.TabIndex = 1;
            this.labelPredmetiDrugihSmerova.Text = "Predmeti dostupni na drugim smerovima:";
            // 
            // c
            // 
            this.c.AutoSize = true;
            this.c.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c.Location = new System.Drawing.Point(67, 31);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(138, 48);
            this.c.TabIndex = 2;
            this.c.Text = "Dobrodošli na sistem!\r\n\r\nPodaci o Vama:";
            this.c.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPredmeti
            // 
            this.labelPredmeti.AutoSize = true;
            this.labelPredmeti.Location = new System.Drawing.Point(359, 31);
            this.labelPredmeti.Name = "labelPredmeti";
            this.labelPredmeti.Size = new System.Drawing.Size(154, 13);
            this.labelPredmeti.TabIndex = 3;
            this.labelPredmeti.Text = "Predmeti dostupni za Vaš smer:";
            // 
            // labelUkupnoESPBtxt
            // 
            this.labelUkupnoESPBtxt.AutoSize = true;
            this.labelUkupnoESPBtxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUkupnoESPBtxt.Location = new System.Drawing.Point(230, 339);
            this.labelUkupnoESPBtxt.Name = "labelUkupnoESPBtxt";
            this.labelUkupnoESPBtxt.Size = new System.Drawing.Size(116, 20);
            this.labelUkupnoESPBtxt.TabIndex = 6;
            this.labelUkupnoESPBtxt.Text = "Ukupno ESPB:";
            // 
            // labelSumaESPB
            // 
            this.labelSumaESPB.AutoSize = true;
            this.labelSumaESPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSumaESPB.Location = new System.Drawing.Point(454, 339);
            this.labelSumaESPB.Name = "labelSumaESPB";
            this.labelSumaESPB.Size = new System.Drawing.Size(18, 20);
            this.labelSumaESPB.TabIndex = 7;
            this.labelSumaESPB.Text = "0";
            // 
            // buttonPrijaviPredmete
            // 
            this.buttonPrijaviPredmete.Enabled = false;
            this.buttonPrijaviPredmete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrijaviPredmete.Location = new System.Drawing.Point(18, 389);
            this.buttonPrijaviPredmete.Name = "buttonPrijaviPredmete";
            this.buttonPrijaviPredmete.Size = new System.Drawing.Size(196, 39);
            this.buttonPrijaviPredmete.TabIndex = 8;
            this.buttonPrijaviPredmete.Text = "Prijavi predmete*";
            this.buttonPrijaviPredmete.UseVisualStyleBackColor = true;
            // 
            // labelImePrezime
            // 
            this.labelImePrezime.AutoSize = true;
            this.labelImePrezime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelImePrezime.Location = new System.Drawing.Point(6, 16);
            this.labelImePrezime.Name = "labelImePrezime";
            this.labelImePrezime.Size = new System.Drawing.Size(85, 15);
            this.labelImePrezime.TabIndex = 11;
            this.labelImePrezime.Text = "Ime i prezime:";
            // 
            // labelBrIndeksa
            // 
            this.labelBrIndeksa.AutoSize = true;
            this.labelBrIndeksa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBrIndeksa.Location = new System.Drawing.Point(6, 47);
            this.labelBrIndeksa.Name = "labelBrIndeksa";
            this.labelBrIndeksa.Size = new System.Drawing.Size(78, 15);
            this.labelBrIndeksa.TabIndex = 12;
            this.labelBrIndeksa.Text = "Broj indeksa:";
            // 
            // labelJMBG
            // 
            this.labelJMBG.AutoSize = true;
            this.labelJMBG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJMBG.Location = new System.Drawing.Point(6, 105);
            this.labelJMBG.Name = "labelJMBG";
            this.labelJMBG.Size = new System.Drawing.Size(44, 15);
            this.labelJMBG.TabIndex = 13;
            this.labelJMBG.Text = "JMBG:";
            // 
            // labelDatumRodjenja
            // 
            this.labelDatumRodjenja.AutoSize = true;
            this.labelDatumRodjenja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDatumRodjenja.Location = new System.Drawing.Point(6, 134);
            this.labelDatumRodjenja.Name = "labelDatumRodjenja";
            this.labelDatumRodjenja.Size = new System.Drawing.Size(92, 15);
            this.labelDatumRodjenja.TabIndex = 14;
            this.labelDatumRodjenja.Text = "Datum rođenja:";
            // 
            // labelBrojTelefona
            // 
            this.labelBrojTelefona.AutoSize = true;
            this.labelBrojTelefona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBrojTelefona.Location = new System.Drawing.Point(6, 164);
            this.labelBrojTelefona.Name = "labelBrojTelefona";
            this.labelBrojTelefona.Size = new System.Drawing.Size(79, 15);
            this.labelBrojTelefona.TabIndex = 15;
            this.labelBrojTelefona.Text = "Broj telefona:";
            // 
            // labelSmer
            // 
            this.labelSmer.AutoSize = true;
            this.labelSmer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSmer.Location = new System.Drawing.Point(6, 75);
            this.labelSmer.Name = "labelSmer";
            this.labelSmer.Size = new System.Drawing.Size(40, 15);
            this.labelSmer.TabIndex = 16;
            this.labelSmer.Text = "Smer:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSmer);
            this.groupBox1.Controls.Add(this.labelBrojTelefona);
            this.groupBox1.Controls.Add(this.labelDatumRodjenja);
            this.groupBox1.Controls.Add(this.labelJMBG);
            this.groupBox1.Controls.Add(this.labelBrIndeksa);
            this.groupBox1.Controls.Add(this.labelImePrezime);
            this.groupBox1.Location = new System.Drawing.Point(9, 96);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 188);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // labelWarning
            // 
            this.labelWarning.AutoSize = true;
            this.labelWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWarning.Location = new System.Drawing.Point(231, 394);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(219, 30);
            this.labelWarning.TabIndex = 18;
            this.labelWarning.Text = "* Nedostupno ukoliko je suma ESPB\r\nVaših izabranih predmeta manja od 48";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(526, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 26);
            this.label1.TabIndex = 20;
            this.label1.Text = "** Moguće je izabrati samo jedan\r\npredmet sa liste.";
            // 
            // checkedListBox
            // 
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Location = new System.Drawing.Point(362, 70);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.Size = new System.Drawing.Size(329, 214);
            this.checkedListBox.TabIndex = 21;
            this.checkedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_ItemCheck);
            // 
            // studentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.checkedListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelWarning);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonPrijaviPredmete);
            this.Controls.Add(this.labelSumaESPB);
            this.Controls.Add(this.labelUkupnoESPBtxt);
            this.Controls.Add(this.labelPredmeti);
            this.Controls.Add(this.c);
            this.Controls.Add(this.labelPredmetiDrugihSmerova);
            this.Controls.Add(this.comboBoxPredmetiDrugihSmerova);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "studentForm";
            this.Text = "Studentski servis - Pregled liste predmeta i prijava";
            this.Load += new System.EventHandler(this.studentForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPredmetiDrugihSmerova;
        private System.Windows.Forms.Label labelPredmetiDrugihSmerova;
        private System.Windows.Forms.Label c;
        private System.Windows.Forms.Label labelPredmeti;
        private System.Windows.Forms.Label labelUkupnoESPBtxt;
        private System.Windows.Forms.Label labelSumaESPB;
        private System.Windows.Forms.Button buttonPrijaviPredmete;
        private System.Windows.Forms.Label labelImePrezime;
        private System.Windows.Forms.Label labelBrIndeksa;
        private System.Windows.Forms.Label labelJMBG;
        private System.Windows.Forms.Label labelDatumRodjenja;
        private System.Windows.Forms.Label labelBrojTelefona;
        private System.Windows.Forms.Label labelSmer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkedListBox;
    }
}