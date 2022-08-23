using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OgrenciNotKayitSistemi
{
    public partial class frmOgrenci : Form
    {
        public frmOgrenci()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        public string numara;
        private void frmOgrenci_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;

            SqlCommand komut = new SqlCommand("select*from Ogrenci where NUMARA=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
                pictureBox1.ImageLocation = dr[5].ToString();
            }
            NotListesi();
        }

        void NotListesi()
        {
            SqlCommand komut = new SqlCommand("select * from Notlar where OGRID=(select ID from Ogrenci Where NUMARA=@p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",LblNumara.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblSinav1.Text = dr[1].ToString();
                LblSinav2.Text = dr[2].ToString();
                LblSinav3.Text = dr[3].ToString();
                LblProje.Text = dr[4].ToString();
                LblOrtalama.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();

            if (Convert.ToDouble(LblOrtalama.Text)>=50)
            {
                LblDurum.Text = "Geçti";
            }
            else
            {
                LblDurum.Text = "Kaldı";
            }
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMesajlar_Click(object sender, EventArgs e)
        {
            frmMesajlar frm = new frmMesajlar();
            frm.numara = LblNumara.Text;
            frm.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            frmDuyuruListesi frm = new frmDuyuruListesi();
            frm.Show();
        }

        private void BtnHesapMakinesi_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Calc.exe");
        }
    }
}
