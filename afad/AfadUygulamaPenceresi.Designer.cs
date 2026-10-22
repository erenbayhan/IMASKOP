namespace afad
{
    partial class AfadUygulamaPenceresi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            groupBox1 = new GroupBox();
            btnSevkEmri = new Button();
            btnAfadRezervasyonAc = new Button();
            txtSevkKonumu = new RichTextBox();
            nmrAracSayisi = new NumericUpDown();
            cmbAracTuru = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            dgwFirmaStoklari = new DataGridView();
            btnAfadCikis = new Button();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nmrAracSayisi).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgwFirmaStoklari).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tableLayoutPanel1.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 20, 3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(0, 35, 0, 0);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1045, 508);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnAfadCikis);
            groupBox1.Controls.Add(btnSevkEmri);
            groupBox1.Controls.Add(btnAfadRezervasyonAc);
            groupBox1.Controls.Add(txtSevkKonumu);
            groupBox1.Controls.Add(nmrAracSayisi);
            groupBox1.Controls.Add(cmbAracTuru);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(3, 38);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(359, 447);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Araç Talep ve Sevk Bilgileri";
            // 
            // btnSevkEmri
            // 
            btnSevkEmri.BackColor = Color.White;
            btnSevkEmri.ForeColor = Color.SeaGreen;
            btnSevkEmri.Location = new Point(181, 407);
            btnSevkEmri.Name = "btnSevkEmri";
            btnSevkEmri.Size = new Size(165, 29);
            btnSevkEmri.TabIndex = 6;
            btnSevkEmri.Text = "SEVK EMRİNİ ONAYLA";
            btnSevkEmri.UseVisualStyleBackColor = false;
            btnSevkEmri.Click += button1_Click;
            // 
            // btnAfadRezervasyonAc
            // 
            btnAfadRezervasyonAc.BackColor = Color.White;
            btnAfadRezervasyonAc.Location = new Point(12, 407);
            btnAfadRezervasyonAc.Name = "btnAfadRezervasyonAc";
            btnAfadRezervasyonAc.Size = new Size(161, 29);
            btnAfadRezervasyonAc.TabIndex = 3;
            btnAfadRezervasyonAc.Text = "REZERVASYONLARIM";
            btnAfadRezervasyonAc.UseVisualStyleBackColor = false;
            btnAfadRezervasyonAc.Click += btnAfadRezervasyonAc_Click;
            // 
            // txtSevkKonumu
            // 
            txtSevkKonumu.Location = new Point(221, 253);
            txtSevkKonumu.Name = "txtSevkKonumu";
            txtSevkKonumu.Size = new Size(125, 120);
            txtSevkKonumu.TabIndex = 5;
            txtSevkKonumu.Text = "";
            // 
            // nmrAracSayisi
            // 
            nmrAracSayisi.Location = new Point(150, 136);
            nmrAracSayisi.Name = "nmrAracSayisi";
            nmrAracSayisi.Size = new Size(143, 27);
            nmrAracSayisi.TabIndex = 4;
            // 
            // cmbAracTuru
            // 
            cmbAracTuru.FormattingEnabled = true;
            cmbAracTuru.Items.AddRange(new object[] { "Ekskavatör", "Kepçe", "Vinç", "Dozer", "Damperli Kamyon" });
            cmbAracTuru.Location = new Point(159, 24);
            cmbAracTuru.Name = "cmbAracTuru";
            cmbAracTuru.Size = new Size(134, 28);
            cmbAracTuru.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 256);
            label3.Name = "label3";
            label3.Size = new Size(209, 20);
            label3.TabIndex = 2;
            label3.Text = "Sevk Edilecek Konum / Bölge :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 138);
            label2.Name = "label2";
            label2.Size = new Size(138, 20);
            label2.TabIndex = 1;
            label2.Text = "İstenen Araç Sayısı :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(155, 20);
            label1.TabIndex = 0;
            label1.Text = "İhtiyaç Duyulan Araç : ";
            label1.Click += label1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgwFirmaStoklari);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(368, 38);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(674, 467);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Firmaların Mevcut Araç Stok ve Konum Durumu";
            // 
            // dgwFirmaStoklari
            // 
            dgwFirmaStoklari.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwFirmaStoklari.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgwFirmaStoklari.BackgroundColor = Color.White;
            dgwFirmaStoklari.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgwFirmaStoklari.Dock = DockStyle.Fill;
            dgwFirmaStoklari.Location = new Point(3, 23);
            dgwFirmaStoklari.Margin = new Padding(3, 70, 3, 3);
            dgwFirmaStoklari.Name = "dgwFirmaStoklari";
            dgwFirmaStoklari.RowHeadersWidth = 51;
            dgwFirmaStoklari.Size = new Size(668, 441);
            dgwFirmaStoklari.TabIndex = 3;
            // 
            // btnAfadCikis
            // 
            btnAfadCikis.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAfadCikis.BackColor = Color.White;
            btnAfadCikis.ForeColor = Color.Red;
            btnAfadCikis.Location = new Point(0, 46);
            btnAfadCikis.Name = "btnAfadCikis";
            btnAfadCikis.Size = new Size(94, 34);
            btnAfadCikis.TabIndex = 4;
            btnAfadCikis.Text = "ÇIKIŞ YAP";
            btnAfadCikis.UseVisualStyleBackColor = false;
            btnAfadCikis.Click += btnAfadCikis_Click;
            // 
            // AfadUygulamaPenceresi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
            ClientSize = new Size(1045, 508);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AfadUygulamaPenceresi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AFAD - Afet Bölgesi Araç Sevk Koordinasyon Paneli";
            tableLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nmrAracSayisi).EndInit();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgwFirmaStoklari).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private Button btnSevkEmri;
        private Button btnAfadRezervasyonAc;
        private RichTextBox txtSevkKonumu;
        private NumericUpDown nmrAracSayisi;
        private ComboBox cmbAracTuru;
        private Label label3;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Button btnAfadCikis;
        private DataGridView dgwFirmaStoklari;
    }
}
