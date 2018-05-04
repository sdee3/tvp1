namespace tvp_projekat1
{
    partial class statForm
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
            this.labelPredmetIzabralo = new System.Windows.Forms.Label();
            this.labelPredmetIzabraloPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPredmetIzabralo
            // 
            this.labelPredmetIzabralo.AutoSize = true;
            this.labelPredmetIzabralo.Location = new System.Drawing.Point(25, 31);
            this.labelPredmetIzabralo.Name = "labelPredmetIzabralo";
            this.labelPredmetIzabralo.Size = new System.Drawing.Size(88, 13);
            this.labelPredmetIzabralo.TabIndex = 0;
            this.labelPredmetIzabralo.Text = "Predmet izabralo:";
            // 
            // labelPredmetIzabraloPercent
            // 
            this.labelPredmetIzabraloPercent.AutoSize = true;
            this.labelPredmetIzabraloPercent.Location = new System.Drawing.Point(25, 65);
            this.labelPredmetIzabraloPercent.Name = "labelPredmetIzabraloPercent";
            this.labelPredmetIzabraloPercent.Size = new System.Drawing.Size(0, 13);
            this.labelPredmetIzabraloPercent.TabIndex = 1;
            // 
            // statForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 263);
            this.Controls.Add(this.labelPredmetIzabraloPercent);
            this.Controls.Add(this.labelPredmetIzabralo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "statForm";
            this.Text = "Statistika predmeta";
            this.Load += new System.EventHandler(this.statForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPredmetIzabralo;
        private System.Windows.Forms.Label labelPredmetIzabraloPercent;
    }
}