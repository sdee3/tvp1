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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPredmetiSmera = new System.Windows.Forms.TextBox();
            this.textBoxPredmetiDrugihSmerova = new System.Windows.Forms.TextBox();
            this.buttonAzuriraj = new System.Windows.Forms.Button();
            this.buttonSaveIzmene = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Predmeti sa smera:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Predmeti sa drugih smerova:";
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
            this.textBoxPredmetiDrugihSmerova.Multiline = true;
            this.textBoxPredmetiDrugihSmerova.Name = "textBoxPredmetiDrugihSmerova";
            this.textBoxPredmetiDrugihSmerova.ReadOnly = true;
            this.textBoxPredmetiDrugihSmerova.Size = new System.Drawing.Size(404, 216);
            this.textBoxPredmetiDrugihSmerova.TabIndex = 3;
            // 
            // buttonAzuriraj
            // 
            this.buttonAzuriraj.Location = new System.Drawing.Point(35, 509);
            this.buttonAzuriraj.Name = "buttonAzuriraj";
            this.buttonAzuriraj.Size = new System.Drawing.Size(193, 30);
            this.buttonAzuriraj.TabIndex = 4;
            this.buttonAzuriraj.Text = "Azuriraj podatke";
            this.buttonAzuriraj.UseVisualStyleBackColor = true;
            // 
            // buttonSaveIzmene
            // 
            this.buttonSaveIzmene.Location = new System.Drawing.Point(245, 509);
            this.buttonSaveIzmene.Name = "buttonSaveIzmene";
            this.buttonSaveIzmene.Size = new System.Drawing.Size(193, 30);
            this.buttonSaveIzmene.TabIndex = 5;
            this.buttonSaveIzmene.Text = "Sacuvaj izmene";
            this.buttonSaveIzmene.UseVisualStyleBackColor = true;
            // 
            // izbornaListaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 561);
            this.Controls.Add(this.buttonSaveIzmene);
            this.Controls.Add(this.buttonAzuriraj);
            this.Controls.Add(this.textBoxPredmetiDrugihSmerova);
            this.Controls.Add(this.textBoxPredmetiSmera);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "izbornaListaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Izborna lista";
            this.Load += new System.EventHandler(this.izbornaListaForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPredmetiSmera;
        private System.Windows.Forms.TextBox textBoxPredmetiDrugihSmerova;
        private System.Windows.Forms.Button buttonAzuriraj;
        private System.Windows.Forms.Button buttonSaveIzmene;
    }
}