namespace afad
{
    partial class FirmaRezervasyonPenceresi
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
        dgvFirmaRezervasyonlar = new DataGridView();
        btnRezervasyonOnayla = new Button();
        btnFirmaGeriDon = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvFirmaRezervasyonlar).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 24);
        label1.Name = "label1";
        label1.Size = new Size(311, 20);
        label1.TabIndex = 0;
        label1.Text = "AFAD Tarafından İstenen Araç Rezervasyonları";
        // 
        // dgvFirmaRezervasyonlar
        // 
        dgvFirmaRezervasyonlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvFirmaRezervasyonlar.Location = new Point(23, 57);
        dgvFirmaRezervasyonlar.Name = "dgvFirmaRezervasyonlar";
        dgvFirmaRezervasyonlar.RowHeadersWidth = 51;
        dgvFirmaRezervasyonlar.Size = new Size(300, 188);
        dgvFirmaRezervasyonlar.TabIndex = 1;
        // 
        // btnRezervasyonOnayla
        // 
        btnRezervasyonOnayla.BackColor = Color.Lime;
        btnRezervasyonOnayla.Location = new Point(23, 269);
        btnRezervasyonOnayla.Name = "btnRezervasyonOnayla";
        btnRezervasyonOnayla.Size = new Size(139, 29);
        btnRezervasyonOnayla.TabIndex = 2;
        btnRezervasyonOnayla.Text = "SEVKİ ONAYLA";
        btnRezervasyonOnayla.UseVisualStyleBackColor = false;
        btnRezervasyonOnayla.Click += btnRezervasyonOnayla_Click;
        // 
        // btnFirmaGeriDon
        // 
        btnFirmaGeriDon.BackColor = Color.Lime;
        btnFirmaGeriDon.Location = new Point(23, 314);
        btnFirmaGeriDon.Name = "btnFirmaGeriDon";
        btnFirmaGeriDon.Size = new Size(191, 29);
        btnFirmaGeriDon.TabIndex = 3;
        btnFirmaGeriDon.Text = "FİRMA PANELİNE DÖN";
        btnFirmaGeriDon.UseVisualStyleBackColor = false;
        btnFirmaGeriDon.Click += btnFirmaGeriDon_Click;
        // 
        // FirmaRezervasyonPenceresi
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1416, 487);
        Controls.Add(btnFirmaGeriDon);
        Controls.Add(btnRezervasyonOnayla);
        Controls.Add(dgvFirmaRezervasyonlar);
        Controls.Add(label1);
        Name = "FirmaRezervasyonPenceresi";
        Text = "Firma - Gelen AFAD Rezervasyon Talepleri";
        Load += FirmaRezervasyonPenceresi_Load;
        ((System.ComponentModel.ISupportInitialize)dgvFirmaRezervasyonlar).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private DataGridView dgvFirmaRezervasyonlar;
    private Button btnRezervasyonOnayla;
    private Button btnFirmaGeriDon;
}
}