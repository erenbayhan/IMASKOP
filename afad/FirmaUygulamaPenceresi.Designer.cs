namespace afad
{
    partial class FirmaUygulamaPenceresi
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtFirmaAdi = new TextBox();
            cmbFirmaAracTuru = new ComboBox();
            nudFirmaAracSayisi = new NumericUpDown();
            btnAracKaydet = new Button();
            btnFirmaRezervasyonAc = new Button();
            btnFirmaCikis = new Button();
            label5 = new Label();
            dgvFirmaStok = new DataGridView();
            txtFirmaAracKonum = new TextBox();
            ((System.ComponentModel.ISupportInitialize)nudFirmaAracSayisi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFirmaStok).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 0;
            label1.Text = "Firma Adı : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 1;
            label2.Text = "Araç Türü : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 97);
            label3.Name = "label3";
            label3.Size = new Size(91, 20);
            label3.TabIndex = 2;
            label3.Text = "Araç Sayısı : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 136);
            label4.Name = "label4";
            label4.Size = new Size(156, 20);
            label4.TabIndex = 3;
            label4.Text = "Bulunduğu Bölge / İl : ";
            // 
            // txtFirmaAdi
            // 
            txtFirmaAdi.Location = new Point(102, 9);
            txtFirmaAdi.Name = "txtFirmaAdi";
            txtFirmaAdi.Size = new Size(125, 27);
            txtFirmaAdi.TabIndex = 4;
            // 
            // cmbFirmaAracTuru
            // 
            cmbFirmaAracTuru.FormattingEnabled = true;
            cmbFirmaAracTuru.Location = new Point(102, 54);
            cmbFirmaAracTuru.Name = "cmbFirmaAracTuru";
            cmbFirmaAracTuru.Size = new Size(151, 28);
            cmbFirmaAracTuru.TabIndex = 6;
            // 
            // nudFirmaAracSayisi
            // 
            nudFirmaAracSayisi.Location = new Point(109, 97);
            nudFirmaAracSayisi.Name = "nudFirmaAracSayisi";
            nudFirmaAracSayisi.Size = new Size(150, 27);
            nudFirmaAracSayisi.TabIndex = 7;
            // 
            // btnAracKaydet
            // 
            btnAracKaydet.BackColor = Color.Lime;
            btnAracKaydet.Location = new Point(12, 194);
            btnAracKaydet.Name = "btnAracKaydet";
            btnAracKaydet.Size = new Size(156, 29);
            btnAracKaydet.TabIndex = 8;
            btnAracKaydet.Text = "ARACI SİSTEME EKLE";
            btnAracKaydet.UseVisualStyleBackColor = false;
            btnAracKaydet.Click += btnAracKaydet_Click;
            // 
            // btnFirmaRezervasyonAc
            // 
            btnFirmaRezervasyonAc.BackColor = Color.Lime;
            btnFirmaRezervasyonAc.Location = new Point(12, 229);
            btnFirmaRezervasyonAc.Name = "btnFirmaRezervasyonAc";
            btnFirmaRezervasyonAc.Size = new Size(241, 29);
            btnFirmaRezervasyonAc.TabIndex = 9;
            btnFirmaRezervasyonAc.Text = "GELEN REZERVASYON TALEPLERİ";
            btnFirmaRezervasyonAc.UseVisualStyleBackColor = false;
            btnFirmaRezervasyonAc.Click += btnFirmaRezervasyonAc_Click;
            // 
            // btnFirmaCikis
            // 
            btnFirmaCikis.BackColor = Color.Red;
            btnFirmaCikis.Location = new Point(12, 264);
            btnFirmaCikis.Name = "btnFirmaCikis";
            btnFirmaCikis.Size = new Size(94, 29);
            btnFirmaCikis.TabIndex = 10;
            btnFirmaCikis.Text = "ÇIKIŞ YAP";
            btnFirmaCikis.UseVisualStyleBackColor = false;
            btnFirmaCikis.Click += btnFirmaCikis_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1104, 12);
            label5.Name = "label5";
            label5.Size = new Size(206, 20);
            label5.TabIndex = 11;
            label5.Text = "Firmanın Mevcut Araç Stokları";
            // 
            // dgvFirmaStok
            // 
            dgvFirmaStok.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFirmaStok.Location = new Point(1104, 45);
            dgvFirmaStok.Name = "dgvFirmaStok";
            dgvFirmaStok.RowHeadersWidth = 51;
            dgvFirmaStok.Size = new Size(300, 188);
            dgvFirmaStok.TabIndex = 12;
            // 
            // txtFirmaAracKonum
            // 
            txtFirmaAracKonum.Location = new Point(179, 136);
            txtFirmaAracKonum.Name = "txtFirmaAracKonum";
            txtFirmaAracKonum.Size = new Size(125, 27);
            txtFirmaAracKonum.TabIndex = 13;
            // 
            // FirmaUygulamaPenceresi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1416, 479);
            Controls.Add(txtFirmaAracKonum);
            Controls.Add(dgvFirmaStok);
            Controls.Add(label5);
            Controls.Add(btnFirmaCikis);
            Controls.Add(btnFirmaRezervasyonAc);
            Controls.Add(btnAracKaydet);
            Controls.Add(nudFirmaAracSayisi);
            Controls.Add(cmbFirmaAracTuru);
            Controls.Add(txtFirmaAdi);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FirmaUygulamaPenceresi";
            Text = "Firma - Araç ve Stok Yönetim Paneli";
            Load += FirmaUygulamaPenceresi_Load;
            ((System.ComponentModel.ISupportInitialize)nudFirmaAracSayisi).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFirmaStok).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private TextBox txtFirmaAdi;
    private TextBox textBox2;
    private ComboBox cmbFirmaAracTuru;
    private NumericUpDown nudFirmaAracSayisi;
    private Button btnAracKaydet;
    private Button btnFirmaRezervasyonAc;
    private Button btnFirmaCikis;
    private Label label5;
    private DataGridView dgvFirmaStok;
        private TextBox txtFirmaAracKonum;
    }
}