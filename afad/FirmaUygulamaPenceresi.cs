using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace afad
{
    public partial class FirmaUygulamaPenceresi : Form
    {
        private Label lblStokOzet = null!;
        private TextBox txtStokArama = null!;

        // AFAD pencereleri ile birebir aynı sade kurumsal renk paleti
        private readonly Color renkArkaPlan = Color.FromArgb(28, 30, 36);
        private readonly Color renkPanel = Color.FromArgb(36, 39, 46);
        private readonly Color renkGirdiKutusu = Color.FromArgb(45, 49, 58);
        private readonly Color renkKenarlik = Color.FromArgb(58, 62, 72);
        private readonly Color renkAnaVurgu = Color.FromArgb(45, 115, 210);     // Kurumsal Mavi (Rezervasyonlar)
        private readonly Color renkBasari = Color.FromArgb(40, 135, 85);         // Mat Yeşil (Kaydet)
        private readonly Color renkIkincilButon = Color.FromArgb(52, 57, 68);
        private readonly Color renkMetinAna = Color.FromArgb(235, 238, 242);
        private readonly Color renkMetinSoluk = Color.FromArgb(160, 166, 178);

        public FirmaUygulamaPenceresi()
        {
            InitializeComponent();
            VeriMerkezi.Baslat();

            SadeArayuzuOlustur();
        }

        private void SadeArayuzuOlustur()
        {
            this.Controls.Clear();

            string firmaAdi = string.IsNullOrWhiteSpace(VeriMerkezi.AktifFirmaAdi) ? "Firma" : VeriMerkezi.AktifFirmaAdi;

            this.Text = "Firma - Araç Stok Yönetimi";
            this.Size = new Size(1160, 680);
            this.MinimumSize = new Size(980, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = renkArkaPlan;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // =========================================================
            // 1. ÜST BAR (SOL ÜSTTE "FİRMA | {Firma Adı}")
            // =========================================================
            Panel pnlUstBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = renkPanel
            };

            Panel pnlUstCizgi = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = renkKenarlik
            };
            pnlUstBar.Controls.Add(pnlUstCizgi);

            Label lblBaslik = new Label
            {
                Text = $"FİRMA | {firmaAdi}",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 18)
            };
            pnlUstBar.Controls.Add(lblBaslik);

            Button btnCikis = SadeButonOlustur("Çıkış Yap", Color.FromArgb(145, 50, 50), 120, 36);
            btnCikis.Location = new Point(pnlUstBar.Width - 140, 12);
            btnCikis.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCikis.Click += btnFirmaCikis_Click;
            pnlUstBar.Controls.Add(btnCikis);

            Button btnRezervasyonlar = SadeButonOlustur("Rezervasyonlarım", renkAnaVurgu, 165, 36);
            btnRezervasyonlar.Location = new Point(pnlUstBar.Width - 315, 12);
            btnRezervasyonlar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRezervasyonlar.Click += btnFirmaRezervasyonAc_Click;
            pnlUstBar.Controls.Add(btnRezervasyonlar);

            // =========================================================
            // 2. SOL PANEL (ARAÇ STOĞU EKLEME FORMU)
            // =========================================================
            Panel pnlSol = new Panel
            {
                Dock = DockStyle.Left,
                Width = 320,
                BackColor = renkPanel
            };

            Panel pnlSolCizgi = new Panel
            {
                Dock = DockStyle.Right,
                Width = 1,
                BackColor = renkKenarlik
            };
            pnlSol.Controls.Add(pnlSolCizgi);

            Label lblFormBaslik = new Label
            {
                Text = "Araç Stoğu Ekle",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblFormBaslik);

            // Firma Adı (giriş yapan firmaya sabit)
            pnlSol.Controls.Add(AlanBaslikOlustur("Firma Adı", 62));
            txtFirmaAdi = new TextBox
            {
                Location = new Point(20, 84),
                Width = 278,
                Font = new Font("Segoe UI", 10F),
                BackColor = renkGirdiKutusu,
                ForeColor = renkMetinSoluk,
                BorderStyle = BorderStyle.FixedSingle,
                ReadOnly = true,
                TabStop = false,
                Text = VeriMerkezi.AktifFirmaAdi
            };
            pnlSol.Controls.Add(txtFirmaAdi);

            // Araç Türü
            pnlSol.Controls.Add(AlanBaslikOlustur("Araç Türü", 124));
            cmbFirmaAracTuru = new ComboBox
            {
                Location = new Point(20, 146),
                Width = 278,
                Font = new Font("Segoe UI", 10F),
                BackColor = renkGirdiKutusu,
                ForeColor = renkMetinAna,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFirmaAracTuru.Items.AddRange(new object[] { "Vinç", "Kamyon", "İş Makinesi (Ekskavatör)", "Ambulans", "Jeneratör Aracı" });
            cmbFirmaAracTuru.SelectedIndex = 0;
            pnlSol.Controls.Add(cmbFirmaAracTuru);

            // Araç Sayısı
            pnlSol.Controls.Add(AlanBaslikOlustur("Araç Sayısı", 188));
            nudFirmaAracSayisi = new NumericUpDown
            {
                Location = new Point(20, 210),
                Width = 278,
                Font = new Font("Segoe UI", 10F),
                BackColor = renkGirdiKutusu,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                Minimum = 1,
                Maximum = 1000,
                Value = 1
            };
            pnlSol.Controls.Add(nudFirmaAracSayisi);

            Button btnKaydet = SadeButonOlustur("Stoğa Ekle", renkBasari, 278, 40);
            btnKaydet.Location = new Point(20, 262);
            btnKaydet.Click += btnAracKaydet_Click;
            pnlSol.Controls.Add(btnKaydet);

            Label lblBilgi = new Label
            {
                Text = "Aynı araç türü daha önce eklendiyse\nmevcut stok miktarının üzerine eklenir.",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(20, 314),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblBilgi);

            // =========================================================
            // 3. SAĞ PANEL (STOK LİSTESİ VE ARAMA)
            // =========================================================
            Panel pnlSag = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 18, 24, 20),
                BackColor = renkArkaPlan
            };

            Panel pnlTabloUst = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            Label lblTabloBaslik = new Label
            {
                Text = "Araç Stok Listesi",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            pnlTabloUst.Controls.Add(lblTabloBaslik);

            lblStokOzet = new Label
            {
                Text = "",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Location = new Point(lblTabloBaslik.PreferredWidth + 8, 13)
            };
            pnlTabloUst.Controls.Add(lblStokOzet);

            txtStokArama = new TextBox
            {
                Width = 200,
                Font = new Font("Segoe UI", 9.5F),
                BackColor = renkPanel,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Firma / Araç ara...",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTabloUst.Width - 200, 8)
            };
            txtStokArama.TextChanged += (s, e) => ListeyiFiltrele();
            pnlTabloUst.Controls.Add(txtStokArama);

            // Tablo (dgvFirmaStok)
            dgvFirmaStok = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = renkPanel,
                BorderStyle = BorderStyle.FixedSingle,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = renkKenarlik,
                EnableHeadersVisualStyles = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            Color baslikRengi = Color.FromArgb(42, 46, 54);
            dgvFirmaStok.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvFirmaStok.ColumnHeadersDefaultCellStyle.BackColor = baslikRengi;
            dgvFirmaStok.ColumnHeadersDefaultCellStyle.ForeColor = renkMetinSoluk;
            dgvFirmaStok.ColumnHeadersDefaultCellStyle.SelectionBackColor = baslikRengi;
            dgvFirmaStok.ColumnHeadersDefaultCellStyle.SelectionForeColor = renkMetinSoluk;
            dgvFirmaStok.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvFirmaStok.ColumnHeadersHeight = 38;

            dgvFirmaStok.DefaultCellStyle.BackColor = renkPanel;
            dgvFirmaStok.DefaultCellStyle.ForeColor = renkMetinAna;
            dgvFirmaStok.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvFirmaStok.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            dgvFirmaStok.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 68, 92);
            dgvFirmaStok.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvFirmaStok.RowTemplate.Height = 36;

            // Açılışta ilk satırın kendiliğinden seçili görünmesini engelle
            dgvFirmaStok.DataBindingComplete += (s, e) => dgvFirmaStok.ClearSelection();

            pnlSag.Controls.Add(dgvFirmaStok);
            pnlSag.Controls.Add(pnlTabloUst);

            this.Controls.Add(pnlSag);
            this.Controls.Add(pnlSol);
            this.Controls.Add(pnlUstBar);

            dgvFirmaStok.DataSource = VeriMerkezi.StokTablosu;
            OzetBilgiyiGuncelle();
        }

        private Label AlanBaslikOlustur(string metin, int y)
        {
            return new Label
            {
                Text = metin,
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, y),
                AutoSize = true
            };
        }

        private Button SadeButonOlustur(string metin, Color arkaPlan, int genislik, int yukseklik)
        {
            Button btn = new Button
            {
                Text = metin,
                Size = new Size(genislik, yukseklik),
                BackColor = arkaPlan,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(70, 75, 88);
            return btn;
        }

        private void OzetBilgiyiGuncelle()
        {
            if (VeriMerkezi.StokTablosu == null || lblStokOzet == null) return;
            lblStokOzet.Text = $"({VeriMerkezi.StokTablosu.Rows.Count} Kayıt)";
        }

        private void ListeyiFiltrele()
        {
            if (VeriMerkezi.StokTablosu == null || VeriMerkezi.StokTablosu.Columns.Count == 0) return;

            string aranan = txtStokArama.Text.Trim().Replace("'", "''");
            string filtre = "";

            if (!string.IsNullOrEmpty(aranan))
            {
                // Tablodaki metin sütunlarında dinamik arama yapar
                var kosullar = new System.Collections.Generic.List<string>();
                foreach (DataColumn col in VeriMerkezi.StokTablosu.Columns)
                {
                    if (col.DataType == typeof(string))
                        kosullar.Add($"[{col.ColumnName}] LIKE '%{aranan}%'");
                }
                if (kosullar.Count > 0)
                    filtre = "(" + string.Join(" OR ", kosullar) + ")";
            }

            VeriMerkezi.StokTablosu.DefaultView.RowFilter = filtre;
        }

        // Stok tablosu diğer pencerelerle paylaşıldığı için çıkarken arama filtresini temizler
        private void FiltreyiTemizle()
        {
            if (VeriMerkezi.StokTablosu != null)
                VeriMerkezi.StokTablosu.DefaultView.RowFilter = "";
        }

        private void FirmaUygulamaPenceresi_Load(object? sender, EventArgs e) { }

        private void btnAracKaydet_Click(object? sender, EventArgs e)
        {
            string firma = txtFirmaAdi.Text.Trim();
            string arac = cmbFirmaAracTuru.Text.Trim();
            int adet = (int)nudFirmaAracSayisi.Value;

            if (string.IsNullOrEmpty(firma))
            {
                MessageBox.Show("Firma bilgisi bulunamadı. Lütfen tekrar giriş yapınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(arac) || adet <= 0)
            {
                MessageBox.Show("Lütfen araç türü ve geçerli bir araç sayısı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable tablo = VeriMerkezi.StokTablosu;
            string stokSutun = tablo.Columns.Contains("Stok Adedi") ? "Stok Adedi" : "Eldeki Stok";
            bool sutunlarVar = tablo.Columns.Contains("Firma Adı") && tablo.Columns.Contains("Araç Türü") && tablo.Columns.Contains(stokSutun);

            // Aynı firma + araç türü zaten varsa yeni satır açmak yerine stoğu artırır
            if (sutunlarVar)
            {
                foreach (DataRow row in tablo.Rows)
                {
                    if (row.RowState == DataRowState.Deleted) continue;

                    string tabloFirma = row["Firma Adı"]?.ToString()?.Trim() ?? "";
                    string tabloArac = row["Araç Türü"]?.ToString()?.Trim() ?? "";

                    if (tabloFirma.Equals(firma, StringComparison.OrdinalIgnoreCase) &&
                        tabloArac.Equals(arac, StringComparison.OrdinalIgnoreCase))
                    {
                        int mevcutStok = Convert.ToInt32(row[stokSutun]);
                        row[stokSutun] = mevcutStok + adet;

                        OzetBilgiyiGuncelle();
                        MessageBox.Show($"Mevcut {arac} stoğu {adet} adet artırıldı. (Yeni stok: {mevcutStok + adet})", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nudFirmaAracSayisi.Value = 1;
                        return;
                    }
                }
            }

            tablo.Rows.Add(firma, arac, adet);

            OzetBilgiyiGuncelle();
            MessageBox.Show("Araç stoğu başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            nudFirmaAracSayisi.Value = 1;
        }

        private void btnFirmaRezervasyonAc_Click(object? sender, EventArgs e)
        {
            FiltreyiTemizle();
            FirmaRezervasyonPenceresi rezForm = new FirmaRezervasyonPenceresi();
            rezForm.Show();
            this.Hide();
        }

        private void btnFirmaCikis_Click(object? sender, EventArgs e)
        {
            FiltreyiTemizle();
            UygulamaLoginPenceresi login = new UygulamaLoginPenceresi();
            login.Show();
            this.Hide();
        }
    }
}
