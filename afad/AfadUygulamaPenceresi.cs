using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace afad
{
    public partial class AfadUygulamaPenceresi : Form
    {
        private string aktifKullaniciAdi = "Yetkili Personel";
        private Label lblSeciliFirmaBilgi = null!;
        private Label lblAltOzetBilgi = null!;
        private TextBox txtFiltreKonum = null!;
        private ComboBox cmbFiltreTur = null!;
        private string stokSutunAdi = "Stok Adedi";
        private string konumSutunAdi = "Konum / Bölge";

        // Sade Kurumsal Koyu Tema Renk Paleti
        private readonly Color renkArkaPlan = Color.FromArgb(28, 30, 36);
        private readonly Color renkPanel = Color.FromArgb(36, 39, 46);
        private readonly Color renkGirdiKutusu = Color.FromArgb(45, 49, 58);
        private readonly Color renkKenarlik = Color.FromArgb(58, 62, 72);
        private readonly Color renkAnaVurgu = Color.FromArgb(45, 115, 210);
        private readonly Color renkIkincilButon = Color.FromArgb(52, 57, 68);
        private readonly Color renkMetinAna = Color.FromArgb(235, 238, 242);
        private readonly Color renkMetinSoluk = Color.FromArgb(160, 166, 178);

        // Mevcut Login ekranın parametresiz çağırıyorsa hata vermemesi için:
        public AfadUygulamaPenceresi() : this("")
        {
        }

        // Login ekranından new AfadUygulamaPenceresi(kullaniciAdi) şeklinde gönderebilirsin:
        public AfadUygulamaPenceresi(string kullaniciAdi)
        {
            InitializeComponent();
            VeriMerkezi.Baslat();

            KullaniciAdiniBelirle(kullaniciAdi);
            VeriTablosunuHazirla();
            SadeArayuzuOlustur();
        }

        private void KullaniciAdiniBelirle(string gelenAd)
        {
            if (!string.IsNullOrWhiteSpace(gelenAd))
            {
                aktifKullaniciAdi = gelenAd.Trim();
                return;
            }

            // Eğer VeriMerkezi içinde KullaniciAdi / AktifKullanici gibi bir değişken varsa otomatik çeker
            string[] olasiIsimler = { "AktifKullanici", "KullaniciAdi", "GirisYapanKullanici", "Username", "AdSoyad" };
            Type vmTipi = typeof(VeriMerkezi);

            foreach (string isim in olasiIsimler)
            {
                PropertyInfo? prop = vmTipi.GetProperty(isim, BindingFlags.Public | BindingFlags.Static);
                if (prop != null)
                {
                    string? deger = prop.GetValue(null)?.ToString();
                    if (!string.IsNullOrWhiteSpace(deger)) { aktifKullaniciAdi = deger; return; }
                }

                FieldInfo? field = vmTipi.GetField(isim, BindingFlags.Public | BindingFlags.Static);
                if (field != null)
                {
                    string? deger = field.GetValue(null)?.ToString();
                    if (!string.IsNullOrWhiteSpace(deger)) { aktifKullaniciAdi = deger; return; }
                }
            }
        }

        private void VeriTablosunuHazirla()
        {
            if (VeriMerkezi.StokTablosu == null) return;

            if (VeriMerkezi.StokTablosu.Columns.Contains("Stok Adedi"))
                stokSutunAdi = "Stok Adedi";
            else if (VeriMerkezi.StokTablosu.Columns.Contains("Eldeki Stok"))
                stokSutunAdi = "Eldeki Stok";

            if (VeriMerkezi.StokTablosu.Columns.Contains("Konum / Bölge"))
                konumSutunAdi = "Konum / Bölge";
            else if (VeriMerkezi.StokTablosu.Columns.Contains("Firmanın Konumu"))
                konumSutunAdi = "Firmanın Konumu";

            if (VeriMerkezi.StokTablosu.Rows.Count == 0 && VeriMerkezi.StokTablosu.Columns.Count > 0)
            {
                VeriMerkezi.StokTablosu.Rows.Add("Öz İnşaat A.Ş.", "Ekskavatör", 5, "Ankara Merkez");
                VeriMerkezi.StokTablosu.Rows.Add("Yılmaz Hafriyat", "Kepçe", 8, "Çankaya");
                VeriMerkezi.StokTablosu.Rows.Add("Demir Lojistik", "Vinç", 3, "Yenimahalle");
                VeriMerkezi.StokTablosu.Rows.Add("Kaya Madencilik", "Dozer", 4, "Gölbaşı");
            }
        }

        private void SadeArayuzuOlustur()
        {
            this.Controls.Clear();

            this.Text = "AFAD - Lojistik ve Sevk Sistemi";
            this.Size = new Size(1160, 680);
            this.MinimumSize = new Size(980, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = renkArkaPlan;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // =========================================================
            // 1. ÜST BAR (SOL ÜSTTE SADECE "AFAD | {Kullanıcı Adı}")
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
                Text = $"AFAD | {aktifKullaniciAdi}",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 18)
            };
            pnlUstBar.Controls.Add(lblBaslik);

            // Sağ Üst Butonlar (GET /reservations geçişi ve Çıkış)
            btnAfadRezervasyonAc = SadeButonOlustur("Rezervasyonlar", renkIkincilButon, 145, 36);
            btnAfadRezervasyonAc.Location = new Point(pnlUstBar.Width - 285, 12);
            btnAfadRezervasyonAc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAfadRezervasyonAc.Click += btnAfadRezervasyonAc_Click;

            btnAfadCikis = SadeButonOlustur("Çıkış Yap", Color.FromArgb(145, 50, 50), 115, 36);
            btnAfadCikis.Location = new Point(pnlUstBar.Width - 130, 12);
            btnAfadCikis.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAfadCikis.Click += btnAfadCikis_Click;

            pnlUstBar.Controls.Add(btnAfadRezervasyonAc);
            pnlUstBar.Controls.Add(btnAfadCikis);

            // =========================================================
            // 2. SOL PANEL (REQUESTS: Talep Oluşturma, Match ve Reserve)
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
                Text = "Araç Talebi ve Rezervasyon",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblFormBaslik);

            // Seçili Firma / Araç Bilgi Kutusu
            Panel pnlFirmaKutu = new Panel
            {
                Location = new Point(20, 54),
                Size = new Size(278, 56),
                BackColor = renkArkaPlan
            };
            Label lblFirmaBaslik = new Label
            {
                Text = "Seçili Tedarikçi Firma:",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(10, 8),
                AutoSize = true
            };
            lblSeciliFirmaBilgi = new Label
            {
                Text = "Listeden seçim yapınız",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Location = new Point(10, 27),
                AutoSize = true
            };
            pnlFirmaKutu.Controls.Add(lblFirmaBaslik);
            pnlFirmaKutu.Controls.Add(lblSeciliFirmaBilgi);
            pnlSol.Controls.Add(pnlFirmaKutu);

            // 1. Araç Türü (Type)
            pnlSol.Controls.Add(BaslikEtiketi("Araç Türü", 126));
            cmbAracTuru = new ComboBox
            {
                Location = new Point(20, 148),
                Size = new Size(278, 30),
                BackColor = renkGirdiKutusu,
                ForeColor = renkMetinAna,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            pnlSol.Controls.Add(cmbAracTuru);

            // 2. Talep Edilen Miktar
            pnlSol.Controls.Add(BaslikEtiketi("Talep Edilen Adet", 192));
            nmrAracSayisi = new NumericUpDown
            {
                Location = new Point(20, 214),
                Size = new Size(278, 30),
                BackColor = renkGirdiKutusu,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10.5F),
                Minimum = 1,
                Maximum = 100,
                Value = 1
            };
            pnlSol.Controls.Add(nmrAracSayisi);

            // 3. Sevk Konumu (Location - RichTextBox)
            pnlSol.Controls.Add(BaslikEtiketi("Sevk Edilecek Konum / Bölge", 258));
            txtSevkKonumu = new RichTextBox
            {
                Location = new Point(20, 280),
                Size = new Size(278, 30),
                BackColor = renkGirdiKutusu,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10F),
                Multiline = false
            };
            pnlSol.Controls.Add(txtSevkKonumu);

            // Eşleştirme Butonu (GET /requests/:id/match karşılığı)
            Button btnEslestir = SadeButonOlustur("Uygun Araçları Eşleştir (Match)", renkIkincilButon, 278, 38);
            btnEslestir.Location = new Point(20, 330);
            btnEslestir.Click += BtnEslestir_Click;
            pnlSol.Controls.Add(btnEslestir);

            // Ana Rezervasyon Butonu (POST /requests & POST /requests/:id/reserve karşılığı)
            Button btnOnayla = SadeButonOlustur("Talebi Oluştur ve Rezerve Et", renkAnaVurgu, 278, 42);
            btnOnayla.Location = new Point(20, 378);
            btnOnayla.Click += button1_Click;
            pnlSol.Controls.Add(btnOnayla);

            // =========================================================
            // 3. SAĞ PANEL (GET /vehicles & GET /vehicles/search/query)
            // =========================================================
            Panel pnlSag = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 18, 24, 20),
                BackColor = renkArkaPlan
            };

            // Üst Filtreleme Barı (/vehicles/search/query -> Location, Type)
            Panel pnlTabloUst = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45
            };

            Label lblTabloBaslik = new Label
            {
                Text = "Firma ve Araç Envanteri",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            pnlTabloUst.Controls.Add(lblTabloBaslik);

            lblAltOzetBilgi = new Label
            {
                Text = "",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Location = new Point(185, 12)
            };
            pnlTabloUst.Controls.Add(lblAltOzetBilgi);

            // Sağ Üst Çoklu Arama (Konum ve Tür)
            Button btnFiltreTemizle = SadeButonOlustur("Tümü", renkIkincilButon, 65, 27);
            btnFiltreTemizle.Font = new Font("Segoe UI", 8.5F);
            btnFiltreTemizle.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFiltreTemizle.Location = new Point(pnlTabloUst.Width - 65, 7);
            btnFiltreTemizle.Click += (s, e) =>
            {
                txtFiltreKonum.Clear();
                if (cmbFiltreTur.Items.Count > 0) cmbFiltreTur.SelectedIndex = 0;
                AraclariFiltrele();
            };

            cmbFiltreTur = new ComboBox
            {
                Width = 130,
                Font = new Font("Segoe UI", 9F),
                BackColor = renkPanel,
                ForeColor = renkMetinAna,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTabloUst.Width - 203, 8)
            };
            cmbFiltreTur.SelectedIndexChanged += (s, e) => AraclariFiltrele();

            txtFiltreKonum = new TextBox
            {
                Width = 140,
                Font = new Font("Segoe UI", 9.5F),
                BackColor = renkPanel,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Konum / Firma ara...",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTabloUst.Width - 351, 8)
            };
            txtFiltreKonum.TextChanged += (s, e) => AraclariFiltrele();

            pnlTabloUst.Controls.Add(txtFiltreKonum);
            pnlTabloUst.Controls.Add(cmbFiltreTur);
            pnlTabloUst.Controls.Add(btnFiltreTemizle);

            // Tablo (DataGridView)
            dgwFirmaStoklari = new DataGridView
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
                RowHeadersVisible = false
            };

            Color baslikRengi = Color.FromArgb(42, 46, 54);
            dgwFirmaStoklari.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgwFirmaStoklari.ColumnHeadersDefaultCellStyle.BackColor = baslikRengi;
            dgwFirmaStoklari.ColumnHeadersDefaultCellStyle.ForeColor = renkMetinSoluk;
            dgwFirmaStoklari.ColumnHeadersDefaultCellStyle.SelectionBackColor = baslikRengi;
            dgwFirmaStoklari.ColumnHeadersDefaultCellStyle.SelectionForeColor = renkMetinSoluk;
            dgwFirmaStoklari.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgwFirmaStoklari.ColumnHeadersHeight = 38;

            dgwFirmaStoklari.DefaultCellStyle.BackColor = renkPanel;
            dgwFirmaStoklari.DefaultCellStyle.ForeColor = renkMetinAna;
            dgwFirmaStoklari.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgwFirmaStoklari.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            dgwFirmaStoklari.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 68, 92);
            dgwFirmaStoklari.DefaultCellStyle.SelectionForeColor = Color.White;
            dgwFirmaStoklari.RowTemplate.Height = 36;

            dgwFirmaStoklari.SelectionChanged += DgwFirmaStoklari_SelectionChanged;

            pnlSag.Controls.Add(dgwFirmaStoklari);
            pnlSag.Controls.Add(pnlTabloUst);

            this.Controls.Add(pnlSag);
            this.Controls.Add(pnlSol);
            this.Controls.Add(pnlUstBar);

            VerileriYukleVeGuncelle();
        }

        private Label BaslikEtiketi(string metin, int y)
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

        private void VerileriYukleVeGuncelle()
        {
            if (VeriMerkezi.StokTablosu == null) return;

            dgwFirmaStoklari.DataSource = VeriMerkezi.StokTablosu;

            if (dgwFirmaStoklari.Columns.Contains("Firma Adı"))
                dgwFirmaStoklari.Columns["Firma Adı"].FillWeight = 140;

            cmbAracTuru.Items.Clear();
            cmbFiltreTur.Items.Clear();
            cmbFiltreTur.Items.Add("Tüm Araç Türleri");

            foreach (DataRow row in VeriMerkezi.StokTablosu.Rows)
            {
                string? arac = row["Araç Türü"]?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(arac) && !cmbAracTuru.Items.Contains(arac))
                {
                    cmbAracTuru.Items.Add(arac);
                    cmbFiltreTur.Items.Add(arac);
                }
            }

            cmbFiltreTur.SelectedIndex = 0;
            OzetBilgiyiGuncelle();
        }

        private void OzetBilgiyiGuncelle()
        {
            if (VeriMerkezi.StokTablosu == null || lblAltOzetBilgi == null) return;

            int toplamKayit = VeriMerkezi.StokTablosu.Rows.Count;
            int toplamArac = 0;

            foreach (DataRow row in VeriMerkezi.StokTablosu.Rows)
            {
                toplamArac += Convert.ToInt32(row[stokSutunAdi]);
            }

            lblAltOzetBilgi.Text = $"({toplamKayit} Kayıt, {toplamArac} Araç)";
        }

        // GET /vehicles/search/query (Location, Type) karşılığı filtreleme
        private void AraclariFiltrele()
        {
            if (VeriMerkezi.StokTablosu == null) return;

            string konumVeyaFirma = txtFiltreKonum.Text.Trim().Replace("'", "''");
            string seciliTur = cmbFiltreTur.SelectedItem?.ToString() ?? "Tüm Araç Türleri";

            string filtre = "";

            if (!string.IsNullOrEmpty(konumVeyaFirma))
            {
                if (VeriMerkezi.StokTablosu.Columns.Contains(konumSutunAdi))
                    filtre += $"([Firma Adı] LIKE '%{konumVeyaFirma}%' OR [{konumSutunAdi}] LIKE '%{konumVeyaFirma}%')";
                else
                    filtre += $"[Firma Adı] LIKE '%{konumVeyaFirma}%'";
            }

            if (seciliTur != "Tüm Araç Türleri")
            {
                if (!string.IsNullOrEmpty(filtre)) filtre += " AND ";
                filtre += $"[Araç Türü] = '{seciliTur.Replace("'", "''")}'";
            }

            VeriMerkezi.StokTablosu.DefaultView.RowFilter = filtre;
        }

        // GET /requests/:id/match karşılığı: Sol paneldeki talebe uygun stoğu olan araçları tabloda eşleştirir
        private void BtnEslestir_Click(object? sender, EventArgs e)
        {
            if (VeriMerkezi.StokTablosu == null) return;

            string istenenTur = cmbAracTuru.Text.Trim().Replace("'", "''");
            int istenenAdet = (int)nmrAracSayisi.Value;

            if (string.IsNullOrEmpty(istenenTur))
            {
                MessageBox.Show("Eşleştirme yapmak için önce sol menüden Araç Türü seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            VeriMerkezi.StokTablosu.DefaultView.RowFilter = $"[Araç Türü] = '{istenenTur}' AND [{stokSutunAdi}] >= {istenenAdet}";

            if (dgwFirmaStoklari.Rows.Count == 0)
            {
                MessageBox.Show($"'{istenenTur}' türünde en az {istenenAdet} adet stoğa sahip uygun firma bulunamadı.", "Eşleşme Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VeriMerkezi.StokTablosu.DefaultView.RowFilter = "";
            }
        }

        private void DgwFirmaStoklari_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgwFirmaStoklari.SelectedRows.Count > 0)
            {
                DataGridViewRow satir = dgwFirmaStoklari.SelectedRows[0];
                string firma = satir.Cells["Firma Adı"].Value?.ToString() ?? "-";
                string arac = satir.Cells["Araç Türü"].Value?.ToString() ?? "";
                int stok = Convert.ToInt32(satir.Cells[stokSutunAdi].Value ?? 0);

                if (lblSeciliFirmaBilgi != null)
                    lblSeciliFirmaBilgi.Text = $"{firma}  (Stok: {stok})";

                if (cmbAracTuru != null && cmbAracTuru.Items.Contains(arac))
                    cmbAracTuru.SelectedItem = arac;

                if (nmrAracSayisi != null)
                {
                    nmrAracSayisi.Maximum = stok > 0 ? stok : 1;
                    nmrAracSayisi.Value = 1;
                }
            }
        }

        // POST /requests & POST /requests/:id/reserve karşılığı
        private void button1_Click(object? sender, EventArgs e)
        {
            string secilenArac = cmbAracTuru.Text.Trim();
            int istenenAdet = nmrAracSayisi != null ? (int)nmrAracSayisi.Value : 0;
            string sevkKonumu = txtSevkKonumu?.Text.Trim() ?? "";

            if (string.IsNullOrEmpty(secilenArac) || istenenAdet <= 0 || string.IsNullOrWhiteSpace(sevkKonumu))
            {
                MessageBox.Show("Lütfen araç türünü seçiniz ve sevk konumunu giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSevkKonumu?.Focus();
                return;
            }

            if (dgwFirmaStoklari.SelectedRows.Count > 0)
            {
                if (dgwFirmaStoklari.SelectedRows[0].DataBoundItem is DataRowView seciliVeri)
                {
                    DataRow row = seciliVeri.Row;
                    string tabloArac = row["Araç Türü"]?.ToString()?.Trim() ?? "";
                    int mevcutStok = Convert.ToInt32(row[stokSutunAdi]);

                    if (tabloArac.Equals(secilenArac, StringComparison.OrdinalIgnoreCase) && mevcutStok >= istenenAdet)
                    {
                        SevkTamamla(row, mevcutStok, istenenAdet, secilenArac, sevkKonumu);
                        return;
                    }
                }
            }

            foreach (DataRow row in VeriMerkezi.StokTablosu.Rows)
            {
                string tabloArac = row["Araç Türü"]?.ToString()?.Trim() ?? "";
                if (tabloArac.Equals(secilenArac, StringComparison.OrdinalIgnoreCase))
                {
                    int mevcutStok = Convert.ToInt32(row[stokSutunAdi]);
                    if (mevcutStok >= istenenAdet)
                    {
                        SevkTamamla(row, mevcutStok, istenenAdet, secilenArac, sevkKonumu);
                        return;
                    }
                }
            }

            MessageBox.Show("Seçilen araç için yeterli stok bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SevkTamamla(DataRow row, int mevcutStok, int istenenAdet, string secilenArac, string sevkKonumu)
        {
            row[stokSutunAdi] = mevcutStok - istenenAdet;
            string firmaAdi = row["Firma Adı"]?.ToString() ?? "Firma";

            VeriMerkezi.RezervasyonEkle(firmaAdi, secilenArac, istenenAdet, sevkKonumu);

            MessageBox.Show($"{istenenAdet} adet {secilenArac}, {firmaAdi} firmasından '{sevkKonumu}' bölgesine rezerve edildi.", "Rezervasyon Oluşturuldu", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtSevkKonumu.Clear();
            VeriMerkezi.StokTablosu.DefaultView.RowFilter = "";
            dgwFirmaStoklari.Refresh();
            OzetBilgiyiGuncelle();
            DgwFirmaStoklari_SelectionChanged(null, EventArgs.Empty);
        }

        private void Form1_Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void dgwFirmaStoklari_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnAfadRezervasyonAc_Click(object? sender, EventArgs e)
        {
            AfadRezervasyonPenceresi frm = new AfadRezervasyonPenceresi();
            frm.Show();
            this.Hide();
        }

        private void btnAfadCikis_Click(object? sender, EventArgs e)
        {
            UygulamaLoginPenceresi login = new UygulamaLoginPenceresi();
            login.Show();
            this.Hide();
        }
    }
}