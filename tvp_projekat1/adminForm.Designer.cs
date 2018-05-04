namespace tvp_projekat1
{
    partial class adminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminForm));
            this.comboBoxSmerovi = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxPredmeti = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxStudenti = new System.Windows.Forms.ComboBox();
            this.buttonStatistika = new System.Windows.Forms.Button();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.buttonAddSmer = new System.Windows.Forms.Button();
            this.buttonAddPredmet = new System.Windows.Forms.Button();
            this.buttonAddStudent = new System.Windows.Forms.Button();
            this.buttonSaveIzmene = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // comboBoxSmerovi
            // 
            this.comboBoxSmerovi.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBoxSmerovi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSmerovi.FormattingEnabled = true;
            this.comboBoxSmerovi.Location = new System.Drawing.Point(586, 25);
            this.comboBoxSmerovi.Name = "comboBoxSmerovi";
            this.comboBoxSmerovi.Size = new System.Drawing.Size(170, 21);
            this.comboBoxSmerovi.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(583, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Smerovi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(583, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Predmeti";
            // 
            // comboBoxPredmeti
            // 
            this.comboBoxPredmeti.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBoxPredmeti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPredmeti.FormattingEnabled = true;
            this.comboBoxPredmeti.Location = new System.Drawing.Point(586, 80);
            this.comboBoxPredmeti.Name = "comboBoxPredmeti";
            this.comboBoxPredmeti.Size = new System.Drawing.Size(170, 21);
            this.comboBoxPredmeti.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(583, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Studenti";
            // 
            // comboBoxStudenti
            // 
            this.comboBoxStudenti.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBoxStudenti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStudenti.FormattingEnabled = true;
            this.comboBoxStudenti.Location = new System.Drawing.Point(586, 136);
            this.comboBoxStudenti.Name = "comboBoxStudenti";
            this.comboBoxStudenti.Size = new System.Drawing.Size(170, 21);
            this.comboBoxStudenti.TabIndex = 4;
            // 
            // buttonStatistika
            // 
            this.buttonStatistika.Enabled = false;
            this.buttonStatistika.Location = new System.Drawing.Point(586, 601);
            this.buttonStatistika.Name = "buttonStatistika";
            this.buttonStatistika.Size = new System.Drawing.Size(170, 32);
            this.buttonStatistika.TabIndex = 8;
            this.buttonStatistika.Text = "Prikaz statistike";
            this.buttonStatistika.UseVisualStyleBackColor = true;
            this.buttonStatistika.Click += new System.EventHandler(this.buttonStatistika_Click);
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Location = new System.Drawing.Point(12, 9);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(422, 130);
            this.labelWelcome.TabIndex = 10;
            this.labelWelcome.Text = resources.GetString("labelWelcome.Text");
            // 
            // buttonAddSmer
            // 
            this.buttonAddSmer.Location = new System.Drawing.Point(586, 197);
            this.buttonAddSmer.Name = "buttonAddSmer";
            this.buttonAddSmer.Size = new System.Drawing.Size(164, 31);
            this.buttonAddSmer.TabIndex = 1;
            this.buttonAddSmer.Text = "Dodaj smer";
            this.buttonAddSmer.UseVisualStyleBackColor = true;
            this.buttonAddSmer.Click += new System.EventHandler(this.buttonAddSmer_Click);
            // 
            // buttonAddPredmet
            // 
            this.buttonAddPredmet.Location = new System.Drawing.Point(586, 275);
            this.buttonAddPredmet.Name = "buttonAddPredmet";
            this.buttonAddPredmet.Size = new System.Drawing.Size(164, 31);
            this.buttonAddPredmet.TabIndex = 2;
            this.buttonAddPredmet.Text = "Dodaj predmet";
            this.buttonAddPredmet.UseVisualStyleBackColor = true;
            this.buttonAddPredmet.Click += new System.EventHandler(this.buttonAddPredmet_Click);
            // 
            // buttonAddStudent
            // 
            this.buttonAddStudent.Location = new System.Drawing.Point(586, 238);
            this.buttonAddStudent.Name = "buttonAddStudent";
            this.buttonAddStudent.Size = new System.Drawing.Size(164, 31);
            this.buttonAddStudent.TabIndex = 3;
            this.buttonAddStudent.Text = "Dodaj studenta";
            this.buttonAddStudent.UseVisualStyleBackColor = true;
            this.buttonAddStudent.Click += new System.EventHandler(this.buttonAddStudent_Click);
            // 
            // buttonSaveIzmene
            // 
            this.buttonSaveIzmene.Enabled = false;
            this.buttonSaveIzmene.Location = new System.Drawing.Point(588, 493);
            this.buttonSaveIzmene.Name = "buttonSaveIzmene";
            this.buttonSaveIzmene.Size = new System.Drawing.Size(162, 33);
            this.buttonSaveIzmene.TabIndex = 7;
            this.buttonSaveIzmene.Text = "Sačuvaj izmene";
            this.buttonSaveIzmene.UseVisualStyleBackColor = true;
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(15, 155);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(551, 478);
            this.mainPanel.TabIndex = 11;
            // 
            // adminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.buttonAddStudent);
            this.Controls.Add(this.buttonSaveIzmene);
            this.Controls.Add(this.buttonAddPredmet);
            this.Controls.Add(this.buttonStatistika);
            this.Controls.Add(this.buttonAddSmer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxStudenti);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxPredmeti);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSmerovi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "adminForm";
            this.Text = "Administracija - Smerovi, predmeti i studenti";
            this.Load += new System.EventHandler(this.adminForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSmerovi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxPredmeti;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxStudenti;
        private System.Windows.Forms.Button buttonStatistika;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Button buttonAddSmer;
        private System.Windows.Forms.Button buttonAddPredmet;
        private System.Windows.Forms.Button buttonAddStudent;
        private System.Windows.Forms.Button buttonSaveIzmene;
        private System.Windows.Forms.Panel mainPanel;
    }
}