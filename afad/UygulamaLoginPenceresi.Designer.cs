namespace afad
{
    partial class UygulamaLoginPenceresi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbGirisTuru = new System.Windows.Forms.ComboBox();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.btnGirisYap = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbGirisTuru
            // 
            this.cmbGirisTuru.FormattingEnabled = true;
            this.cmbGirisTuru.Location = new System.Drawing.Point(150, 40);
            this.cmbGirisTuru.Name = "cmbGirisTuru";
            this.cmbGirisTuru.Size = new System.Drawing.Size(200, 24);
            this.cmbGirisTuru.TabIndex = 0;
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Location = new System.Drawing.Point(150, 90);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(200, 22);
            this.txtKullaniciAdi.TabIndex = 1;
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(150, 140);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(200, 22);
            this.txtSifre.TabIndex = 2;
            this.txtSifre.UseSystemPasswordChar = true;
            // 
            // btnGirisYap
            // 
            this.btnGirisYap.Location = new System.Drawing.Point(150, 190);
            this.btnGirisYap.Name = "btnGirisYap";
            this.btnGirisYap.Size = new System.Drawing.Size(200, 40);
            this.btnGirisYap.TabIndex = 3;
            this.btnGirisYap.Text = "GİRİŞ YAP";
            this.btnGirisYap.UseVisualStyleBackColor = true;
            this.btnGirisYap.Click += new System.EventHandler(this.btnGirisYap_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 43);
            this.label1.Name = "label1";
            this.label1.Text = "Giriş Türü:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 93);
            this.label2.Name = "label2";
            this.label2.Text = "Kullanıcı Adı:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 143);
            this.label3.Name = "label3";
            this.label3.Text = "Şifre:";
            // 
            // UygulamaLoginPenceresi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 280);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGirisYap);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.txtKullaniciAdi);
            this.Controls.Add(this.cmbGirisTuru);
            this.Name = "UygulamaLoginPenceresi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sisteme Giriş Paneli";
            this.Load += new System.EventHandler(this.UygulamaLoginPenceresi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cmbGirisTuru;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Button btnGirisYap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}