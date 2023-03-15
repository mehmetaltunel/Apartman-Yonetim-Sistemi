namespace Site_Yönetim_Sistemi
{
    partial class faturaOlustur
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
            this.cmbAy = new System.Windows.Forms.ComboBox();
            this.txtElektrik = new System.Windows.Forms.TextBox();
            this.txtSu = new System.Windows.Forms.TextBox();
            this.txtDogal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbAy
            // 
            this.cmbAy.DropDownHeight = 150;
            this.cmbAy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAy.FormattingEnabled = true;
            this.cmbAy.IntegralHeight = false;
            this.cmbAy.Location = new System.Drawing.Point(162, 46);
            this.cmbAy.Name = "cmbAy";
            this.cmbAy.Size = new System.Drawing.Size(77, 21);
            this.cmbAy.TabIndex = 20;
            // 
            // txtElektrik
            // 
            this.txtElektrik.Location = new System.Drawing.Point(22, 98);
            this.txtElektrik.Name = "txtElektrik";
            this.txtElektrik.Size = new System.Drawing.Size(100, 20);
            this.txtElektrik.TabIndex = 21;
            // 
            // txtSu
            // 
            this.txtSu.Location = new System.Drawing.Point(139, 98);
            this.txtSu.Name = "txtSu";
            this.txtSu.Size = new System.Drawing.Size(100, 20);
            this.txtSu.TabIndex = 22;
            // 
            // txtDogal
            // 
            this.txtDogal.Location = new System.Drawing.Point(263, 98);
            this.txtDogal.Name = "txtDogal";
            this.txtDogal.Size = new System.Drawing.Size(100, 20);
            this.txtDogal.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Elektrik";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Su";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(283, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Doğal Gaz";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Oluştur";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // faturaOlustur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 170);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDogal);
            this.Controls.Add(this.txtSu);
            this.Controls.Add(this.txtElektrik);
            this.Controls.Add(this.cmbAy);
            this.Name = "faturaOlustur";
            this.Text = "faturaOlustur";
            this.Load += new System.EventHandler(this.faturaOlustur_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbAy;
        private System.Windows.Forms.TextBox txtElektrik;
        private System.Windows.Forms.TextBox txtSu;
        private System.Windows.Forms.TextBox txtDogal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
    }
}