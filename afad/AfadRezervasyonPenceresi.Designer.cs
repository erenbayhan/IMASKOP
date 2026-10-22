namespace afad
{
    partial class AfadRezervasyonPenceresi
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
        dgvAfadRezervasyonlar = new DataGridView();
        btnAfadGeriDon = new Button();
        btnRezervasyonIptal = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvAfadRezervasyonlar).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 9);
        label1.Name = "label1";
        label1.Size = new Size(222, 20);
        label1.TabIndex = 0;
        label1.Text = "AFAD Aktif Araç Rezervasyonları";
        // 
        // dgvAfadRezervasyonlar
        // 
        dgvAfadRezervasyonlar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvAfadRezervasyonlar.Location = new Point(12, 48);
        dgvAfadRezervasyonlar.Name = "dgvAfadRezervasyonlar";
        dgvAfadRezervasyonlar.RowHeadersWidth = 51;
        dgvAfadRezervasyonlar.Size = new Size(300, 188);
        dgvAfadRezervasyonlar.TabIndex = 1;
        // 
        // btnAfadGeriDon
        // 
        btnAfadGeriDon.BackColor = Color.Lime;
        btnAfadGeriDon.Location = new Point(12, 295);
        btnAfadGeriDon.Name = "btnAfadGeriDon";
        btnAfadGeriDon.Size = new Size(147, 29);
        btnAfadGeriDon.TabIndex = 2;
        btnAfadGeriDon.Text = "ANA EKRANA DÖN";
        btnAfadGeriDon.UseVisualStyleBackColor = false;
        btnAfadGeriDon.Click += btnAfadGeriDon_Click;
        // 
        // btnRezervasyonIptal
        // 
        btnRezervasyonIptal.BackColor = Color.Red;
        btnRezervasyonIptal.Location = new Point(12, 260);
        btnRezervasyonIptal.Name = "btnRezervasyonIptal";
        btnRezervasyonIptal.Size = new Size(186, 29);
        btnRezervasyonIptal.TabIndex = 3;
        btnRezervasyonIptal.Text = "REZERVASYONU İPTAL ET";
        btnRezervasyonIptal.UseVisualStyleBackColor = false;
        btnRezervasyonIptal.Click += btnRezervasyonIptal_Click;
        // 
        // AfadRezervasyonPenceresi
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1415, 494);
        Controls.Add(btnRezervasyonIptal);
        Controls.Add(btnAfadGeriDon);
        Controls.Add(dgvAfadRezervasyonlar);
        Controls.Add(label1);
        Name = "AfadRezervasyonPenceresi";
        Text = "AFAD - Aktif Rezervasyon ve Sevk Takip Ekranı";
        Load += AfadRezervasyonPenceresi_Load;
        ((System.ComponentModel.ISupportInitialize)dgvAfadRezervasyonlar).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private DataGridView dgvAfadRezervasyonlar;
    private Button btnAfadGeriDon;
    private Button btnRezervasyonIptal;
}
}