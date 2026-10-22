using System;
using System.Data;

namespace afad
{
    public static class VeriMerkezi
    {
        public static DataTable StokTablosu = new DataTable();
        public static DataTable RezervasyonTablosu = new DataTable();
        public static string AktifFirmaAdi = "Kuzey Lojistik A.Ş.";
        private static bool _baslatildi = false;

        public static void Baslat()
        {
            if (_baslatildi) return;

            // 1. Stok Tablosu Sütunları
            StokTablosu.Columns.Add("Firma Adı", typeof(string));
            StokTablosu.Columns.Add("Araç Türü", typeof(string));
            StokTablosu.Columns.Add("Stok Adedi", typeof(int));
            StokTablosu.Columns.Add("Konum / Bölge", typeof(string));

            // Başlangıç Örnek Verileri
            StokTablosu.Rows.Add("Kuzey Lojistik A.Ş.", "Vinç", 5, "Ankara");
            StokTablosu.Rows.Add("Kuzey Lojistik A.Ş.", "Kamyon", 12, "İstanbul");
            StokTablosu.Rows.Add("Anadolu Nakliyat", "İş Makinesi (Ekskavatör)", 4, "İzmir");

            // 2. Rezervasyon Tablosu Sütunları
            RezervasyonTablosu.Columns.Add("Rezervasyon No", typeof(int));
            RezervasyonTablosu.Columns.Add("Firma Adı", typeof(string));
            RezervasyonTablosu.Columns.Add("Araç Türü", typeof(string));
            RezervasyonTablosu.Columns.Add("İstenen Adet", typeof(int));
            RezervasyonTablosu.Columns.Add("Sevk Bölgesi", typeof(string));
            RezervasyonTablosu.Columns.Add("Durum", typeof(string));

            _baslatildi = true;
        }

        public static void RezervasyonEkle(string firmaAdi, string aracTuru, int adet, string bolge)
        {
            Baslat();
            int yeniNo = RezervasyonTablosu.Rows.Count + 1;
            RezervasyonTablosu.Rows.Add(yeniNo, firmaAdi, aracTuru, adet, bolge, "Firma Onayı Bekliyor");
        }
    }
}