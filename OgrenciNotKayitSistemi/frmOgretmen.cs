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
    public partial class frmOgretmen : Form
    {
        public frmOgretmen()
        {
            InitializeComponent();
        }
        public string numara;
        SqlBaglantisi bgl = new SqlBaglantisi();
        void OgrenciListesi()
        {
            SqlCommand komut = new SqlCommand("select*from Ogrenci", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void NotListesi()
        {
            SqlCommand komut = new SqlCommand("Execute Ogrenciler1", bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void frmOgretmen_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LblNumara.Text = numara;

            SqlCommand komut = new SqlCommand("select*from Ogretmen where NUMARA=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[1] + " " + dr[2];
            }
            OgrenciListesi();
            NotListesi();
            bgl.baglanti().Close();
        }
        string Fotograf;
        private void BtnFotografSec_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Fotograf = openFileDialog1.FileName;
            pictureBox1.ImageLocation = Fotograf;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Ogrenci(AD,SOYAD,NUMARA,SIFRE,FOTOGRAF) values (@p1,@p2,@p3,@p4,@p5)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskNumara.Text);
            komut.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p5", Fotograf);
            komut.ExecuteNonQuery();
            MessageBox.Show("Öğrenci Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

            
            OgrenciListesi();
            NotListesi();
            bgl.baglanti().Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskNumara.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSifre.Text= dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            pictureBox1.ImageLocation= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            SqlCommand komut = new SqlCommand("select*from Notlar where OGRID=(select ID from Ogrenci where NUMARA=@p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskNumara.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                TxtSinav1.Text = dr[1].ToString();
                TxtSinav2.Text = dr[2].ToString();
                TxtSinav3.Text = dr[3].ToString();
                TxtProje.Text = dr[4].ToString();
                TxtOrtalama.Text = dr[5].ToString();
                TxtDurum.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
 SqlCommand komut = new SqlCommand("update Ogrenci set ad=@p1,soyad=@p2,sıfre=@p3,fotograf=@p4 where numara=@p5",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", TxtSifre.Text);
            komut.Parameters.AddWithValue("@p4", Fotograf);
            komut.Parameters.AddWithValue("@p5", MskNumara.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            }
            catch (Exception)
            {

            }
           
            //Notları ekleme ve Güncelleme
            SqlCommand komut2 = new SqlCommand("update Notlar set sınav1=@p1,sınav2=@p2,sınav3=@p3,proje=@p4,ortalama=@p5,durum=@p6 where OGRID=(select ID from Ogrenci where numara=@p7)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", TxtSinav1.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSinav2.Text);
            komut2.Parameters.AddWithValue("@p3", TxtSinav3.Text);
            komut2.Parameters.AddWithValue("@p4", TxtProje.Text);
            komut2.Parameters.AddWithValue("@p5", Convert.ToDecimal(TxtOrtalama.Text));
            komut2.Parameters.AddWithValue("@p6", TxtDurum.Text);
            komut2.Parameters.AddWithValue("@p7", MskNumara.Text);
            komut2.ExecuteNonQuery();
            MessageBox.Show("Öğrenci Başarıyla Güncellendi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            NotListesi();
            OgrenciListesi();
            bgl.baglanti().Close();
        }

        private void BtnGuncelle_MouseClick(object sender, MouseEventArgs e)
        {
                
        }

        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            double sinav1, sinav2, sinav3, proje, ortalama;
            sinav1 = Convert.ToDouble(TxtSinav1.Text);
            sinav2 = Convert.ToDouble(TxtSinav2.Text);
            sinav3 = Convert.ToDouble(TxtSinav3.Text);
            proje = Convert.ToDouble(TxtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            TxtOrtalama.Text = ortalama.ToString();
            if (ortalama>=50)
            {
                TxtDurum.Text = "True";
            }
            else
            {
                TxtDurum.Text = "False";
            }
            MessageBox.Show("Öğrenci Notları Başarılı Bir Şekilde Hesaplandı...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            NotListesi();
            OgrenciListesi();
            bgl.baglanti().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            frmDuyuruOlustur frm = new frmDuyuruOlustur();
            frm.Show();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Seçili kayıtı Silmek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{

            //    string secili = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //    SqlCommand komut = new SqlCommand("Delete from Ogrenci where ıd=@p1", bgl.baglanti());
            //    komut.Parameters.Add("@p1", secili);
            //    //SqlCommand komut1 = new SqlCommand("Delete from Notlar where ogrıd=@p1", bgl.baglanti());
            //    //komut1.Parameters.Add("@p1", secili);
            //    //komut1.ExecuteNonQuery();
            //    komut.ExecuteNonQuery();
            //    MessageBox.Show("Duyuru Silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    NotListesi();
            //    OgrenciListesi();
            //}
        }

        private void BtnDuyuruListesi_Click(object sender, EventArgs e)
        {
            frmDuyuruListesi frm = new frmDuyuruListesi();
            frm.Show();
        }

        private void BtnMesajlar_Click(object sender, EventArgs e)
        {
            frmMesajlar frm=new frmMesajlar();
            frm.numara = LblNumara.Text;
            frm.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
