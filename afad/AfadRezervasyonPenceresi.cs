using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace afad
{
    public partial class AfadRezervasyonPenceresi : Form
    {
        private string aktifKullaniciAdi = "Yetkili Personel";
        private Label lblSeciliFirma = null!;
        private Label lblSeciliAracVeAdet = null!;
        private Label lblSeciliKonum = null!;
        private Label lblSeciliDurum = null!;
        private Label lblAltOzetBilgi = null!;
        private TextBox txtArama = null!;
        private ComboBox cmbDurumFiltre = null!;
        private Button btnGorevTamamla = null!;

        // Ana pencere ile birebir aynı sade kurumsal renk paleti
        private readonly Color renkArkaPlan = Color.FromArgb(28, 30, 36);
        private readonly Color renkPanel = Color.FromArgb(36, 39, 46);
        private readonly Color renkGirdiKutusu = Color.FromArgb(45, 49, 58);
        private readonly Color renkKenarlik = Color.FromArgb(58, 62, 72);
        private readonly Color renkAnaVurgu = Color.FromArgb(45, 115, 210);     // Kurumsal Mavi (Durum etiketi)
        private readonly Color renkBasari = Color.FromArgb(40, 135, 85);         // Mat Yeşil (Tamamla)
        private readonly Color renkTehlike = Color.FromArgb(145, 50, 50);        // Mat Kırmızı (İptal)
        private readonly Color renkIkincilButon = Color.FromArgb(52, 57, 68);
        private readonly Color renkPasifButon = Color.FromArgb(70, 74, 84);      // Pasif (gri) buton
        private readonly Color renkMetinAna = Color.FromArgb(235, 238, 242);
        private readonly Color renkMetinSoluk = Color.FromArgb(160, 166, 178);

        public AfadRezervasyonPenceresi() : this("")
        {
        }

        public AfadRezervasyonPenceresi(string kullaniciAdi)
        {
            InitializeComponent();
            VeriMerkezi.Baslat();

            KullaniciAdiniBelirle(kullaniciAdi);
            TabloSutunlariniHazirla();
            SadeArayuzuOlustur();
        }

        private void KullaniciAdiniBelirle(string gelenAd)
        {
            if (!string.IsNullOrWhiteSpace(gelenAd))
            {
                aktifKullaniciAdi = gelenAd.Trim();
                return;
            }

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

        // Endpoint'lerdeki complete durumunu takip edebilmek için tabloda "Durum" sütunu yoksa ekler
        private void TabloSutunlariniHazirla()
        {
            if (VeriMerkezi.RezervasyonTablosu == null) return;

            if (!VeriMerkezi.RezervasyonTablosu.Columns.Contains("Durum"))
            {
                VeriMerkezi.RezervasyonTablosu.Columns.Add("Durum", typeof(string));

                foreach (DataRow row in VeriMerkezi.RezervasyonTablosu.Rows)
                {
                    if (row["Durum"] == DBNull.Value || string.IsNullOrWhiteSpace(row["Durum"]?.ToString()))
                    {
                        row["Durum"] = "Beklemede";
                    }
                }
            }
        }

        private void SadeArayuzuOlustur()
        {
            this.Controls.Clear();

            this.Text = "AFAD - Rezervasyon ve Sevk Kayıtları";
            this.Size = new Size(1160, 680);
            this.MinimumSize = new Size(980, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = renkArkaPlan;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            // =========================================================
            // 1. ÜST BAR (SOL ÜSTTE "AFAD | {Kullanıcı Adı}")
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

            btnAfadGeriDon = SadeButonOlustur("Sevk Paneline Dön", renkIkincilButon, 165, 36);
            btnAfadGeriDon.Location = new Point(pnlUstBar.Width - 185, 12);
            btnAfadGeriDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAfadGeriDon.Click += btnAfadGeriDon_Click;

            pnlUstBar.Controls.Add(btnAfadGeriDon);

            // =========================================================
            // 2. SOL PANEL (RESERVATIONS: Complete, Cancel)
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
                Text = "Rezervasyon İşlemleri",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblFormBaslik);

            // Seçili Rezervasyon Detay Kartı (GET /reservations/:id karşılığı)
            Panel pnlDetayKutu = new Panel
            {
                Location = new Point(20, 54),
                Size = new Size(278, 175),
                BackColor = renkArkaPlan
            };

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Tedarikçi Firma:", 12));
            lblSeciliFirma = DetayDegerOlustur("Seçim yapılmadı", 30);
            pnlDetayKutu.Controls.Add(lblSeciliFirma);

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Araç Türü ve Miktar:", 56));
            lblSeciliAracVeAdet = DetayDegerOlustur("-", 74);
            pnlDetayKutu.Controls.Add(lblSeciliAracVeAdet);

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Sevk Bölgesi / Konum:", 100));
            lblSeciliKonum = DetayDegerOlustur("-", 118);
            pnlDetayKutu.Controls.Add(lblSeciliKonum);

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Operasyon Durumu:", 144));
            lblSeciliDurum = new Label
            {
                Text = "-",
                ForeColor = renkAnaVurgu,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Location = new Point(125, 143),
                AutoSize = true
            };
            pnlDetayKutu.Controls.Add(lblSeciliDurum);

            pnlSol.Controls.Add(pnlDetayKutu);

            // Aksiyon Butonları (Endpoint karşılıkları)
            Label lblIslemAciklama = new Label
            {
                Text = "Seçili Kayıt Üzerinde İşlem Yap:",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 248),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblIslemAciklama);

            // 1. POST /reservations/:id/complete
            btnGorevTamamla = SadeButonOlustur("Görevi Tamamla (Complete)", renkBasari, 278, 40);
            btnGorevTamamla.Location = new Point(20, 274);
            btnGorevTamamla.Click += BtnRezervasyonTamamla_Click;
            pnlSol.Controls.Add(btnGorevTamamla);

            // 2. POST /reservations/:id/cancel
            btnRezervasyonIptal = SadeButonOlustur("Rezervasyonu İptal Et (Cancel)", renkTehlike, 278, 40);
            btnRezervasyonIptal.Location = new Point(20, 324);
            btnRezervasyonIptal.Click += btnRezervasyonIptal_Click;
            pnlSol.Controls.Add(btnRezervasyonIptal);

            // =========================================================
            // 3. SAĞ PANEL (GET /reservations LİSTESİ VE FİLTRELEME)
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
                Text = "Aktif Rezervasyon ve Sevk Listesi",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            pnlTabloUst.Controls.Add(lblTabloBaslik);

            // Kayıt sayısı başlığın hemen sağına yerleşir (başlığın altında kalıp kesilmesin)
            lblAltOzetBilgi = new Label
            {
                Text = "",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Location = new Point(lblTabloBaslik.PreferredWidth + 8, 13)
            };
            pnlTabloUst.Controls.Add(lblAltOzetBilgi);

            // Sağ Üst Filtreler
            cmbDurumFiltre = new ComboBox
            {
                Width = 135,
                Font = new Font("Segoe UI", 9F),
                BackColor = renkPanel,
                ForeColor = renkMetinAna,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTabloUst.Width - 135, 8)
            };
            cmbDurumFiltre.Items.AddRange(new object[] { "Tüm Durumlar", "Beklemede", "Tamamlandı" });
            cmbDurumFiltre.SelectedIndex = 0;
            cmbDurumFiltre.SelectedIndexChanged += (s, e) => ListeyiFiltrele();

            txtArama = new TextBox
            {
                Width = 170,
                Font = new Font("Segoe UI", 9.5F),
                BackColor = renkPanel,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Firma / Bölge ara...",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTabloUst.Width - 315, 8)
            };
            txtArama.TextChanged += (s, e) => ListeyiFiltrele();

            pnlTabloUst.Controls.Add(txtArama);
            pnlTabloUst.Controls.Add(cmbDurumFiltre);

            // Tablo (dgvAfadRezervasyonlar)
            dgvAfadRezervasyonlar = new DataGridView
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
            dgvAfadRezervasyonlar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvAfadRezervasyonlar.ColumnHeadersDefaultCellStyle.BackColor = baslikRengi;
            dgvAfadRezervasyonlar.ColumnHeadersDefaultCellStyle.ForeColor = renkMetinSoluk;
            dgvAfadRezervasyonlar.ColumnHeadersDefaultCellStyle.SelectionBackColor = baslikRengi;
            dgvAfadRezervasyonlar.ColumnHeadersDefaultCellStyle.SelectionForeColor = renkMetinSoluk;
            dgvAfadRezervasyonlar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvAfadRezervasyonlar.ColumnHeadersHeight = 38;

            dgvAfadRezervasyonlar.DefaultCellStyle.BackColor = renkPanel;
            dgvAfadRezervasyonlar.DefaultCellStyle.ForeColor = renkMetinAna;
            dgvAfadRezervasyonlar.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvAfadRezervasyonlar.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            dgvAfadRezervasyonlar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 68, 92);
            dgvAfadRezervasyonlar.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAfadRezervasyonlar.RowTemplate.Height = 36;

            dgvAfadRezervasyonlar.SelectionChanged += DgvAfadRezervasyonlar_SelectionChanged;

            // Veri bağlanınca DataGridView ilk satırı kendiliğinden seçer; bunu engelle
            dgvAfadRezervasyonlar.DataBindingComplete += (s, e) => dgvAfadRezervasyonlar.ClearSelection();

            pnlSag.Controls.Add(dgvAfadRezervasyonlar);
            pnlSag.Controls.Add(pnlTabloUst);

            this.Controls.Add(pnlSag);
            this.Controls.Add(pnlSol);
            this.Controls.Add(pnlUstBar);

            VerileriBaglaVeGuncelle();
        }

        private Label DetayBaslikOlustur(string metin, int y)
        {
            return new Label
            {
                Text = metin,
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(12, y),
                AutoSize = true
            };
        }

        private Label DetayDegerOlustur(string metin, int y)
        {
            return new Label
            {
                Text = metin,
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Location = new Point(12, y),
                Size = new Size(254, 20),
                AutoEllipsis = true
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

        // Flat butonlarda Enabled=false arka planı değiştirmez; gri rengi elle veriyoruz
        private void ButonDurumunuAyarla(Button btn, bool aktif, Color aktifRenk)
        {
            if (btn == null) return;
            btn.Enabled = aktif;
            btn.BackColor = aktif ? aktifRenk : renkPasifButon;
            btn.Cursor = aktif ? Cursors.Hand : Cursors.Default;
        }

        private void VerileriBaglaVeGuncelle()
        {
            if (VeriMerkezi.RezervasyonTablosu == null)
            {
                DgvAfadRezervasyonlar_SelectionChanged(null, EventArgs.Empty);
                return;
            }

            // Yeni eklenen satırlara varsayılan "Beklemede" durumu ata
            foreach (DataRow row in VeriMerkezi.RezervasyonTablosu.Rows)
            {
                if (VeriMerkezi.RezervasyonTablosu.Columns.Contains("Durum") &&
                    (row["Durum"] == DBNull.Value || string.IsNullOrWhiteSpace(row["Durum"]?.ToString())))
                {
                    row["Durum"] = "Beklemede";
                }
            }

            dgvAfadRezervasyonlar.DataSource = VeriMerkezi.RezervasyonTablosu;
            OzetBilgiyiGuncelle();
            DgvAfadRezervasyonlar_SelectionChanged(null, EventArgs.Empty);
        }

        private void OzetBilgiyiGuncelle()
        {
            if (VeriMerkezi.RezervasyonTablosu == null || lblAltOzetBilgi == null) return;
            lblAltOzetBilgi.Text = $"({VeriMerkezi.RezervasyonTablosu.Rows.Count} Kayıt)";
        }

        private void ListeyiFiltrele()
        {
            if (VeriMerkezi.RezervasyonTablosu == null || VeriMerkezi.RezervasyonTablosu.Columns.Count == 0) return;

            string aranan = txtArama.Text.Trim().Replace("'", "''");
            string seciliDurum = cmbDurumFiltre.SelectedItem?.ToString() ?? "Tüm Durumlar";

            string filtre = "";

            if (!string.IsNullOrEmpty(aranan))
            {
                // Tablodaki metin sütunlarında dinamik arama yapar
                var kosullar = new System.Collections.Generic.List<string>();
                foreach (DataColumn col in VeriMerkezi.RezervasyonTablosu.Columns)
                {
                    if (col.DataType == typeof(string))
                        kosullar.Add($"[{col.ColumnName}] LIKE '%{aranan}%'");
                }
                if (kosullar.Count > 0)
                    filtre += "(" + string.Join(" OR ", kosullar) + ")";
            }

            if (seciliDurum != "Tüm Durumlar" && VeriMerkezi.RezervasyonTablosu.Columns.Contains("Durum"))
            {
                if (!string.IsNullOrEmpty(filtre)) filtre += " AND ";
                filtre += $"[Durum] = '{seciliDurum}'";
            }

            VeriMerkezi.RezervasyonTablosu.DefaultView.RowFilter = filtre;
        }

        private void DgvAfadRezervasyonlar_SelectionChanged(object? sender, EventArgs e)
        {
            bool secimVar = dgvAfadRezervasyonlar.SelectedRows.Count > 0 &&
                            dgvAfadRezervasyonlar.SelectedRows[0].DataBoundItem is DataRowView;

            ButonDurumunuAyarla(btnGorevTamamla, secimVar, renkBasari);
            ButonDurumunuAyarla(btnRezervasyonIptal, secimVar, renkTehlike);

            if (secimVar && dgvAfadRezervasyonlar.SelectedRows[0].DataBoundItem is DataRowView drv)
            {
                DataRow satir = drv.Row;

                // Sütun adları ne olursa olsun güvenli şekilde sırayla veya isimle okur
                string firma = DegerOku(satir, new[] { "Firma Adı", "Firma" }, 0);
                string arac = DegerOku(satir, new[] { "Araç Türü", "Araç" }, 1);
                string adet = DegerOku(satir, new[] { "Adet", "Sevk Adedi", "Miktar", "Stok Adedi" }, 2);
                string konum = DegerOku(satir, new[] { "Sevk Konumu", "Konum", "Bölge", "Görev Bölgesi" }, 3);
                string durum = DegerOku(satir, new[] { "Durum" }, -1);

                lblSeciliFirma.Text = firma;
                lblSeciliAracVeAdet.Text = $"{adet} Adet {arac}";
                lblSeciliKonum.Text = konum;
                lblSeciliDurum.Text = string.IsNullOrEmpty(durum) ? "Beklemede" : durum;
            }
            else
            {
                lblSeciliFirma.Text = "Seçim yapılmadı";
                lblSeciliAracVeAdet.Text = "-";
                lblSeciliKonum.Text = "-";
                lblSeciliDurum.Text = "-";
            }
        }

        private string DegerOku(DataRow row, string[] olasiIsimler, int varsayilanIndeks)
        {
            foreach (string isim in olasiIsimler)
            {
                if (row.Table.Columns.Contains(isim))
                    return row[isim]?.ToString() ?? "-";
            }

            if (varsayilanIndeks >= 0 && varsayilanIndeks < row.Table.Columns.Count)
                return row[varsayilanIndeks]?.ToString() ?? "-";

            return "-";
        }

        // POST /reservations/:id/complete karşılığı
        private void BtnRezervasyonTamamla_Click(object? sender, EventArgs e)
        {
            DurumGuncelle("Tamamlandı", "Sevk görevi başarıyla tamamlandı olarak işaretlendi.");
        }

        private void DurumGuncelle(string yeniDurum, string mesaj)
        {
            if (dgvAfadRezervasyonlar.SelectedRows.Count == 0 ||
                dgvAfadRezervasyonlar.SelectedRows[0].DataBoundItem is not DataRowView drv)
            {
                MessageBox.Show("Lütfen listeden işlem yapılacak bir rezervasyon seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (drv.Row.Table.Columns.Contains("Durum"))
            {
                drv.Row["Durum"] = yeniDurum;
                dgvAfadRezervasyonlar.Refresh();
                DgvAfadRezervasyonlar_SelectionChanged(null, EventArgs.Empty);
                MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // POST /reservations/:id/cancel karşılığı (İptal + Stok İadesi)
        private void btnRezervasyonIptal_Click(object? sender, EventArgs e)
        {
            if (dgvAfadRezervasyonlar.SelectedRows.Count == 0 ||
                dgvAfadRezervasyonlar.SelectedRows[0].DataBoundItem is not DataRowView drv)
            {
                MessageBox.Show("Lütfen iptal etmek istediğiniz rezervasyonu seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult onay = MessageBox.Show(
                "Seçili rezervasyonu iptal etmek istediğinize emin misiniz?\n(Araçlar ilgili firmanın stoğuna iade edilecektir.)",
                "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay != DialogResult.Yes) return;

            DataRow rezSatir = drv.Row;
            StokIadeEt(rezSatir);

            rezSatir.Delete();
            dgvAfadRezervasyonlar.ClearSelection();
            OzetBilgiyiGuncelle();
            DgvAfadRezervasyonlar_SelectionChanged(null, EventArgs.Empty);

            MessageBox.Show("Rezervasyon iptal edildi ve araç stoğu firmaya iade edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StokIadeEt(DataRow rezSatir)
        {
            if (VeriMerkezi.StokTablosu == null) return;

            string firma = DegerOku(rezSatir, new[] { "Firma Adı", "Firma" }, 0);
            string arac = DegerOku(rezSatir, new[] { "Araç Türü", "Araç" }, 1);
            string adetStr = DegerOku(rezSatir, new[] { "Adet", "Sevk Adedi", "Miktar", "Stok Adedi" }, 2);

            if (!int.TryParse(adetStr, out int iadeAdet) || iadeAdet <= 0) return;

            string stokSutun = VeriMerkezi.StokTablosu.Columns.Contains("Stok Adedi") ? "Stok Adedi" : "Eldeki Stok";

            foreach (DataRow stokRow in VeriMerkezi.StokTablosu.Rows)
            {
                string tabloFirma = stokRow["Firma Adı"]?.ToString()?.Trim() ?? "";
                string tabloArac = stokRow["Araç Türü"]?.ToString()?.Trim() ?? "";

                if (tabloFirma.Equals(firma, StringComparison.OrdinalIgnoreCase) &&
                    tabloArac.Equals(arac, StringComparison.OrdinalIgnoreCase))
                {
                    int mevcutStok = Convert.ToInt32(stokRow[stokSutun]);
                    stokRow[stokSutun] = mevcutStok + iadeAdet;
                    break;
                }
            }
        }

        private void AfadRezervasyonPenceresi_Load(object sender, EventArgs e) { }

        private void btnAfadGeriDon_Click(object? sender, EventArgs e)
        {
            AfadUygulamaPenceresi afadForm = new AfadUygulamaPenceresi(aktifKullaniciAdi);
            afadForm.Show();
            this.Hide();
        }
    }
}
