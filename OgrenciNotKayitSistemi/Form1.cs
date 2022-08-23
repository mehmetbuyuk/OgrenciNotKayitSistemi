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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from Ogretmen where NUMARA=@p1 and SIFRE=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskOgretmenNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgretmenSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmOgretmen frm = new frmOgretmen();
                frm.numara = MskOgretmenNumara.Text;
                frm.Show();
                MessageBox.Show("Sisteme Hoş Geldiniz","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Numara ya da Şifre","Uyarı!",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            bgl.baglanti().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select*from Ogrenci where NUMARA=@p1 and SIFRE=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskOgrenciNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtOgrenciSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmOgrenci frm = new frmOgrenci();
                frm.numara= MskOgrenciNumara.Text;
                frm.Show();
                MessageBox.Show("Sisteme Hoş Geldiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Numara ya da Şifre", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            bgl.baglanti().Close();
        }
    }
}
