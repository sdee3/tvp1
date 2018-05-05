namespace tvp_projekat1
{
    partial class izbornaListaForm
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
            this.labelPredmetiSmera = new System.Windows.Forms.Label();
            this.labelPredmetiDrSmerova = new System.Windows.Forms.Label();
            this.textBoxPredmetiSmera = new System.Windows.Forms.TextBox();
            this.textBoxPredmetiDrugihSmerova = new System.Windows.Forms.TextBox();
            this.buttonAzuriraj = new System.Windows.Forms.Button();
            this.buttonSaveIzmene = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPredmetiSmera
            // 
            this.labelPredmetiSmera.AutoSize = true;
            this.labelPredmetiSmera.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPredmetiSmera.Location = new System.Drawing.Point(32, 9);
            this.labelPredmetiSmera.Name = "labelPredmetiSmera";
            this.labelPredmetiSmera.Size = new System.Drawing.Size(137, 18);
            this.labelPredmetiSmera.TabIndex = 0;
            this.labelPredmetiSmera.Text = "Predmeti sa smera:";
            // 
            // labelPredmetiDrSmerova
            // 
            this.labelPredmetiDrSmerova.AutoSize = true;
            this.labelPredmetiDrSmerova.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPredmetiDrSmerova.Location = new System.Drawing.Point(31, 243);
            this.labelPredmetiDrSmerova.Name = "labelPredmetiDrSmerova";
            this.labelPredmetiDrSmerova.Size = new System.Drawing.Size(194, 18);
            this.labelPredmetiDrSmerova.TabIndex = 1;
            this.labelPredmetiDrSmerova.Text = "Predmet sa drugih smerova:";
            // 
            // textBoxPredmetiSmera
            // 
            this.textBoxPredmetiSmera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPredmetiSmera.Location = new System.Drawing.Point(34, 30);
            this.textBoxPredmetiSmera.Multiline = true;
            this.textBoxPredmetiSmera.Name = "textBoxPredmetiSmera";
            this.textBoxPredmetiSmera.ReadOnly = true;
            this.textBoxPredmetiSmera.Size = new System.Drawing.Size(404, 198);
            this.textBoxPredmetiSmera.TabIndex = 2;
            // 
            // textBoxPredmetiDrugihSmerova
            // 
            this.textBoxPredmetiDrugihSmerova.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPredmetiDrugihSmerova.Location = new System.Drawing.Point(34, 264);
            this.textBoxPredmetiDrugihSmerova.Name = "textBoxPredmetiDrugihSmerova";
            this.textBoxPredmetiDrugihSmerova.ReadOnly = true;
            this.textBoxPredmetiDrugihSmerova.Size = new System.Drawing.Size(404, 22);
            this.textBoxPredmetiDrugihSmerova.TabIndex = 3;
            // 
            // buttonAzuriraj
            // 
            this.buttonAzuriraj.Location = new System.Drawing.Point(35, 319);
            this.buttonAzuriraj.Name = "buttonAzuriraj";
            this.buttonAzuriraj.Size = new System.Drawing.Size(193, 30);
            this.buttonAzuriraj.TabIndex = 4;
            this.buttonAzuriraj.Text = "Azuriraj podatke";
            this.buttonAzuriraj.UseVisualStyleBackColor = true;
            this.buttonAzuriraj.Click += new System.EventHandler(this.buttonAzuriraj_Click);
            // 
            // buttonSaveIzmene
            // 
            this.buttonSaveIzmene.Location = new System.Drawing.Point(245, 319);
            this.buttonSaveIzmene.Name = "buttonSaveIzmene";
            this.buttonSaveIzmene.Size = new System.Drawing.Size(193, 30);
            this.buttonSaveIzmene.TabIndex = 5;
            this.buttonSaveIzmene.Text = "Sacuvaj izmene";
            this.buttonSaveIzmene.UseVisualStyleBackColor = true;
            this.buttonSaveIzmene.Click += new System.EventHandler(this.buttonSaveIzmene_Click);
            // 
            // izbornaListaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.buttonSaveIzmene);
            this.Controls.Add(this.buttonAzuriraj);
            this.Controls.Add(this.textBoxPredmetiDrugihSmerova);
            this.Controls.Add(this.textBoxPredmetiSmera);
            this.Controls.Add(this.labelPredmetiDrSmerova);
            this.Controls.Add(this.labelPredmetiSmera);
            this.Name = "izbornaListaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izborna lista";
            this.Load += new System.EventHandler(this.izbornaListaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPredmetiSmera;
        private System.Windows.Forms.Label labelPredmetiDrSmerova;
        private System.Windows.Forms.TextBox textBoxPredmetiSmera;
        private System.Windows.Forms.TextBox textBoxPredmetiDrugihSmerova;
        private System.Windows.Forms.Button buttonAzuriraj;
        private System.Windows.Forms.Button buttonSaveIzmene;
    }
}