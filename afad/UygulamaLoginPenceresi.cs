using afad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace afad
{
    public partial class UygulamaLoginPenceresi : Form
{
    public UygulamaLoginPenceresi()
    {
        InitializeComponent();
    }

    private void UygulamaLoginPenceresi_Load(object sender, EventArgs e)
    {
        VeriMerkezi.Baslat();
        cmbGirisTuru.Items.Clear();
        cmbGirisTuru.Items.Add("AFAD Yetkilisi");
        cmbGirisTuru.Items.Add("Lojistik Firması");
        cmbGirisTuru.SelectedIndex = 0;
    }

    private void btnGirisYap_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
        {
            MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cmbGirisTuru.SelectedIndex == 0) // AFAD Seçildiyse
        {
            AfadUygulamaPenceresi afadForm = new AfadUygulamaPenceresi();
            afadForm.Show();
            this.Hide();
        }
        else // Firma Seçildiyse
        {
            VeriMerkezi.AktifFirmaAdi = txtKullaniciAdi.Text;
            FirmaUygulamaPenceresi firmaForm = new FirmaUygulamaPenceresi();
            firmaForm.Show();
            this.Hide();
        }
    }
}
}
