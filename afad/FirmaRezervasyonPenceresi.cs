using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace afad
{
    public partial class FirmaRezervasyonPenceresi : Form
    {
        private string aktifFirmaAdi = "";
        private DataView rezervasyonGorunumu = null!;

        private Label lblSeciliArac = null!;
        private Label lblSeciliKonum = null!;
        private Label lblSeciliNo = null!;
        private Label lblSeciliDurum = null!;
        private Label lblTalepOzet = null!;
        private Label lblBekleyenSayi = null!;
        private Label lblOnaylananSayi = null!;
        private Label lblTamamlananSayi = null!;
        private TextBox txtArama = null!;
        private ComboBox cmbDurumFiltre = null!;
        private Button btnTalepOnayla = null!;

        // AFAD pencereleri ile birebir aynı sade kurumsal renk paleti
        private readonly Color renkArkaPlan = Color.FromArgb(28, 30, 36);
        private readonly Color renkPanel = Color.FromArgb(36, 39, 46);
        private readonly Color renkKenarlik = Color.FromArgb(58, 62, 72);
        private readonly Color renkAnaVurgu = Color.FromArgb(45, 115, 210);     // Kurumsal Mavi (Onaylandı)
        private readonly Color renkBasari = Color.FromArgb(40, 135, 85);         // Mat Yeşil (Onayla / Tamamlandı)
        private readonly Color renkUyari = Color.FromArgb(215, 160, 60);         // Amber (Beklemede)
        private readonly Color renkIkincilButon = Color.FromArgb(52, 57, 68);
        private readonly Color renkPasifButon = Color.FromArgb(70, 74, 84);      // Pasif (gri) buton
        private readonly Color renkMetinAna = Color.FromArgb(235, 238, 242);
        private readonly Color renkMetinSoluk = Color.FromArgb(160, 166, 178);

        private const string DurumBeklemede = "Beklemede";
        private const string DurumOnaylandi = "Onaylandı";
        private const string DurumTamamlandi = "Tamamlandı";

        public FirmaRezervasyonPenceresi()
        {
            InitializeComponent();
            VeriMerkezi.Baslat();

            aktifFirmaAdi = VeriMerkezi.AktifFirmaAdi?.Trim() ?? "";
            TabloSutunlariniHazirla();
            SadeArayuzuOlustur();
        }

        // AFAD penceresiyle aynı mantık: "Durum" sütunu yoksa ekler, boş olanlara "Beklemede" yazar
        private void TabloSutunlariniHazirla()
        {
            if (VeriMerkezi.RezervasyonTablosu == null) return;

            if (!VeriMerkezi.RezervasyonTablosu.Columns.Contains("Durum"))
                VeriMerkezi.RezervasyonTablosu.Columns.Add("Durum", typeof(string));

            foreach (DataRow row in VeriMerkezi.RezervasyonTablosu.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;
                if (row["Durum"] == DBNull.Value || string.IsNullOrWhiteSpace(row["Durum"]?.ToString()))
                    row["Durum"] = DurumBeklemede;
            }
        }

        private void SadeArayuzuOlustur()
        {
            this.Controls.Clear();

            this.Text = "Firma - AFAD Sevk Talepleri";
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
                Text = $"FİRMA | {(string.IsNullOrEmpty(aktifFirmaAdi) ? "Firma" : aktifFirmaAdi)}",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 12.5F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 18)
            };
            pnlUstBar.Controls.Add(lblBaslik);

            Button btnGeriDon = SadeButonOlustur("Stok Paneline Dön", renkIkincilButon, 165, 36);
            btnGeriDon.Location = new Point(pnlUstBar.Width - 185, 12);
            btnGeriDon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGeriDon.Click += btnFirmaGeriDon_Click;
            pnlUstBar.Controls.Add(btnGeriDon);

            // =========================================================
            // 2. SOL PANEL (TALEP DETAYI, ONAY VE ÖZET)
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
                Text = "Talep Detayı",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblFormBaslik);

            // Seçili talep kartı
            Panel pnlDetayKutu = new Panel
            {
                Location = new Point(20, 54),
                Size = new Size(278, 175),
                BackColor = renkArkaPlan
            };

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Talep No:", 12));
            lblSeciliNo = DetayDegerOlustur("Seçim yapılmadı", 30);
            pnlDetayKutu.Controls.Add(lblSeciliNo);

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("İstenen Araç ve Miktar:", 56));
            lblSeciliArac = DetayDegerOlustur("-", 74);
            pnlDetayKutu.Controls.Add(lblSeciliArac);

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Sevk Bölgesi / Konum:", 100));
            lblSeciliKonum = DetayDegerOlustur("-", 118);
            pnlDetayKutu.Controls.Add(lblSeciliKonum);

            pnlDetayKutu.Controls.Add(DetayBaslikOlustur("Talep Durumu:", 144));
            lblSeciliDurum = new Label
            {
                Text = "-",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                Location = new Point(105, 143),
                AutoSize = true
            };
            pnlDetayKutu.Controls.Add(lblSeciliDurum);

            pnlSol.Controls.Add(pnlDetayKutu);

            btnTalepOnayla = SadeButonOlustur("Talebi Onayla ve Sevk Et", renkBasari, 278, 40);
            btnTalepOnayla.Location = new Point(20, 244);
            btnTalepOnayla.Click += btnRezervasyonOnayla_Click;
            pnlSol.Controls.Add(btnTalepOnayla);

            // Talep özeti (bekleyen / onaylanan / tamamlanan)
            Label lblOzetBaslik = new Label
            {
                Text = "Talep Özeti",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 304),
                AutoSize = true
            };
            pnlSol.Controls.Add(lblOzetBaslik);

            lblBekleyenSayi = OzetKartiEkle(pnlSol, "Bekleyen", renkUyari, 20);
            lblOnaylananSayi = OzetKartiEkle(pnlSol, "Onaylanan", renkAnaVurgu, 114);
            lblTamamlananSayi = OzetKartiEkle(pnlSol, "Tamamlanan", renkBasari, 208);

            // =========================================================
            // 3. SAĞ PANEL (AFAD'DAN GELEN TALEPLER LİSTESİ)
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
                Text = "AFAD'dan Gelen Sevk Talepleri",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(0, 10)
            };
            pnlTabloUst.Controls.Add(lblTabloBaslik);

            lblTalepOzet = new Label
            {
                Text = "",
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Location = new Point(lblTabloBaslik.PreferredWidth + 8, 13)
            };
            pnlTabloUst.Controls.Add(lblTalepOzet);

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
            cmbDurumFiltre.Items.AddRange(new object[] { "Tüm Durumlar", DurumBeklemede, DurumOnaylandi, DurumTamamlandi });
            cmbDurumFiltre.SelectedIndex = 0;
            cmbDurumFiltre.SelectedIndexChanged += (s, e) => ListeyiFiltrele();

            txtArama = new TextBox
            {
                Width = 170,
                Font = new Font("Segoe UI", 9.5F),
                BackColor = renkPanel,
                ForeColor = renkMetinAna,
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Araç / Bölge ara...",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlTabloUst.Width - 315, 8)
            };
            txtArama.TextChanged += (s, e) => ListeyiFiltrele();

            pnlTabloUst.Controls.Add(txtArama);
            pnlTabloUst.Controls.Add(cmbDurumFiltre);

            // Tablo (dgvFirmaRezervasyonlar)
            dgvFirmaRezervasyonlar = new DataGridView
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
            dgvFirmaRezervasyonlar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvFirmaRezervasyonlar.ColumnHeadersDefaultCellStyle.BackColor = baslikRengi;
            dgvFirmaRezervasyonlar.ColumnHeadersDefaultCellStyle.ForeColor = renkMetinSoluk;
            dgvFirmaRezervasyonlar.ColumnHeadersDefaultCellStyle.SelectionBackColor = baslikRengi;
            dgvFirmaRezervasyonlar.ColumnHeadersDefaultCellStyle.SelectionForeColor = renkMetinSoluk;
            dgvFirmaRezervasyonlar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgvFirmaRezervasyonlar.ColumnHeadersHeight = 38;

            dgvFirmaRezervasyonlar.DefaultCellStyle.BackColor = renkPanel;
            dgvFirmaRezervasyonlar.DefaultCellStyle.ForeColor = renkMetinAna;
            dgvFirmaRezervasyonlar.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvFirmaRezervasyonlar.DefaultCellStyle.Padding = new Padding(6, 0, 0, 0);
            dgvFirmaRezervasyonlar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 68, 92);
            dgvFirmaRezervasyonlar.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvFirmaRezervasyonlar.RowTemplate.Height = 36;

            dgvFirmaRezervasyonlar.SelectionChanged += DgvFirmaRezervasyonlar_SelectionChanged;
            dgvFirmaRezervasyonlar.CellFormatting += DgvFirmaRezervasyonlar_CellFormatting;

            // Veri bağlanınca DataGridView ilk satırı kendiliğinden seçer; bunu engelle
            dgvFirmaRezervasyonlar.DataBindingComplete += (s, e) => dgvFirmaRezervasyonlar.ClearSelection();

            pnlSag.Controls.Add(dgvFirmaRezervasyonlar);
            pnlSag.Controls.Add(pnlTabloUst);

            this.Controls.Add(pnlSag);
            this.Controls.Add(pnlSol);
            this.Controls.Add(pnlUstBar);

            VerileriBagla();
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

        // Sol paneldeki küçük sayaç kartı; sayı etiketini döndürür
        private Label OzetKartiEkle(Panel hedef, string baslik, Color vurgu, int x)
        {
            Panel kart = new Panel
            {
                Location = new Point(x, 328),
                Size = new Size(90, 70),
                BackColor = renkArkaPlan
            };

            Panel serit = new Panel
            {
                Dock = DockStyle.Top,
                Height = 3,
                BackColor = vurgu
            };
            kart.Controls.Add(serit);

            Label lblSayi = new Label
            {
                Text = "0",
                ForeColor = renkMetinAna,
                Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold),
                Location = new Point(8, 12),
                AutoSize = true
            };
            kart.Controls.Add(lblSayi);

            Label lblBaslik = new Label
            {
                Text = baslik,
                ForeColor = renkMetinSoluk,
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(9, 45),
                AutoSize = true
            };
            kart.Controls.Add(lblBaslik);

            hedef.Controls.Add(kart);
            return lblSayi;
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

        private void VerileriBagla()
        {
            if (VeriMerkezi.RezervasyonTablosu == null)
            {
                DgvFirmaRezervasyonlar_SelectionChanged(null, EventArgs.Empty);
                return;
            }

            // AFAD penceresinin filtresini etkilememek için tabloya ayrı bir görünüm üzerinden bağlanır
            rezervasyonGorunumu = new DataView(VeriMerkezi.RezervasyonTablosu);
            ListeyiFiltrele();

            dgvFirmaRezervasyonlar.DataSource = rezervasyonGorunumu;
            OzetBilgiyiGuncelle();
            DgvFirmaRezervasyonlar_SelectionChanged(null, EventArgs.Empty);
        }

        // Onaylanmamış ve tamamlanmamış her talep "bekleyen" sayılır
        // (AFAD sevk paneli "Beklemede" dışında bir başlangıç durumu yazsa da onaylanabilsin)
        private bool BeklemedeMi(string durum)
        {
            durum = durum?.Trim() ?? "";
            return !durum.Equals(DurumOnaylandi, StringComparison.OrdinalIgnoreCase) &&
                   !durum.Equals(DurumTamamlandi, StringComparison.OrdinalIgnoreCase) &&
                   !durum.StartsWith("Firma Onayladı", StringComparison.OrdinalIgnoreCase);
        }

        // Seçilen arama / durum kriterlerini uygular
        private void ListeyiFiltrele()
        {
            if (rezervasyonGorunumu == null) return;
            DataTable tablo = rezervasyonGorunumu.Table!;

            var kosullar = new List<string>();

            string aranan = txtArama?.Text.Trim().Replace("'", "''") ?? "";
            if (!string.IsNullOrEmpty(aranan))
            {
                var aramaKosullari = new List<string>();
                foreach (DataColumn col in tablo.Columns)
                {
                    if (col.DataType == typeof(string))
                        aramaKosullari.Add($"[{col.ColumnName}] LIKE '%{aranan}%'");
                }
                if (aramaKosullari.Count > 0)
                    kosullar.Add("(" + string.Join(" OR ", aramaKosullari) + ")");
            }

            string seciliDurum = cmbDurumFiltre?.SelectedItem?.ToString() ?? "Tüm Durumlar";
            if (seciliDurum != "Tüm Durumlar" && tablo.Columns.Contains("Durum"))
                kosullar.Add($"[Durum] = '{seciliDurum}'");

            rezervasyonGorunumu.RowFilter = string.Join(" AND ", kosullar);
            OzetBilgiyiGuncelle();
        }

        private void OzetBilgiyiGuncelle()
        {
            if (rezervasyonGorunumu == null || lblTalepOzet == null) return;

            lblTalepOzet.Text = $"({rezervasyonGorunumu.Count} Talep)";

            // Sayaçlar arama/durum filtresinden bağımsız, tüm talepleri sayar
            DataTable tablo = rezervasyonGorunumu.Table!;
            int bekleyen = 0, onaylanan = 0, tamamlanan = 0;

            foreach (DataRow row in tablo.Rows)
            {
                if (row.RowState == DataRowState.Deleted) continue;

                string durum = tablo.Columns.Contains("Durum") ? row["Durum"]?.ToString()?.Trim() ?? "" : "";
                if (durum.Equals(DurumTamamlandi, StringComparison.OrdinalIgnoreCase)) tamamlanan++;
                else if (BeklemedeMi(durum)) bekleyen++;
                else onaylanan++;
            }

            lblBekleyenSayi.Text = bekleyen.ToString();
            lblOnaylananSayi.Text = onaylanan.ToString();
            lblTamamlananSayi.Text = tamamlanan.ToString();
        }

        private Color DurumRengi(string durum)
        {
            if (durum.Trim().Equals(DurumTamamlandi, StringComparison.OrdinalIgnoreCase)) return renkBasari;
            if (!BeklemedeMi(durum)) return renkAnaVurgu;
            return renkUyari;
        }

        // "Durum" sütununu renklendirir: Beklemede amber, Onaylandı mavi, Tamamlandı yeşil
        private void DgvFirmaRezervasyonlar_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.CellStyle == null) return;
            if (dgvFirmaRezervasyonlar.Columns[e.ColumnIndex].DataPropertyName != "Durum") return;

            Color renk = DurumRengi(e.Value?.ToString() ?? "");
            e.CellStyle.ForeColor = renk;
            e.CellStyle.SelectionForeColor = renk;
            e.CellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
        }

        private void DgvFirmaRezervasyonlar_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvFirmaRezervasyonlar.SelectedRows.Count > 0 &&
                dgvFirmaRezervasyonlar.SelectedRows[0].DataBoundItem is DataRowView drv)
            {
                DataRow satir = drv.Row;

                string no = DegerOku(satir, new[] { "Rezervasyon No", "Talep No", "No", "Id" }, -1);
                string arac = DegerOku(satir, new[] { "Araç Türü", "Araç" }, 1);
                string adet = DegerOku(satir, new[] { "İstenen Adet", "Adet", "Sevk Adedi", "Miktar" }, 2);
                string konum = DegerOku(satir, new[] { "Sevk Bölgesi", "Sevk Konumu", "Konum", "Bölge", "Görev Bölgesi" }, 3);
                string durum = DegerOku(satir, new[] { "Durum" }, -1);
                if (string.IsNullOrEmpty(durum) || durum == "-") durum = DurumBeklemede;

                lblSeciliNo.Text = no == "-" ? $"#{satir.Table.Rows.IndexOf(satir) + 1}" : no;
                lblSeciliArac.Text = $"{adet} Adet {arac}";
                lblSeciliKonum.Text = konum;
                lblSeciliDurum.Text = durum;
                lblSeciliDurum.ForeColor = DurumRengi(durum);

                // Sadece bekleyen talepler onaylanabilir
                ButonDurumunuAyarla(btnTalepOnayla, BeklemedeMi(durum), renkBasari);
            }
            else
            {
                lblSeciliNo.Text = "Seçim yapılmadı";
                lblSeciliArac.Text = "-";
                lblSeciliKonum.Text = "-";
                lblSeciliDurum.Text = "-";
                lblSeciliDurum.ForeColor = renkMetinAna;

                ButonDurumunuAyarla(btnTalepOnayla, false, renkBasari);
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

        private void FirmaRezervasyonPenceresi_Load(object? sender, EventArgs e) { }

        private void btnRezervasyonOnayla_Click(object? sender, EventArgs e)
        {
            if (dgvFirmaRezervasyonlar.SelectedRows.Count == 0 ||
                dgvFirmaRezervasyonlar.SelectedRows[0].DataBoundItem is not DataRowView drv)
            {
                MessageBox.Show("Lütfen listeden onaylanacak bir talep seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow satir = drv.Row;
            string arac = DegerOku(satir, new[] { "Araç Türü", "Araç" }, 1);
            string adet = DegerOku(satir, new[] { "İstenen Adet", "Adet", "Sevk Adedi", "Miktar" }, 2);
            string konum = DegerOku(satir, new[] { "Sevk Bölgesi", "Sevk Konumu", "Konum", "Bölge", "Görev Bölgesi" }, 3);

            DialogResult onay = MessageBox.Show(
                $"{adet} adet {arac} aracının {konum} bölgesine sevkini onaylıyor musunuz?",
                "Sevk Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay != DialogResult.Yes) return;

            satir["Durum"] = DurumOnaylandi;

            dgvFirmaRezervasyonlar.Refresh();
            OzetBilgiyiGuncelle();
            DgvFirmaRezervasyonlar_SelectionChanged(null, EventArgs.Empty);

            MessageBox.Show("AFAD araç sevk talebi onaylandı!", "Onay", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFirmaGeriDon_Click(object? sender, EventArgs e)
        {
            FirmaUygulamaPenceresi firmaForm = new FirmaUygulamaPenceresi();
            firmaForm.Show();
            this.Hide();
        }
    }
}
