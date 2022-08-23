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
    public partial class frmDuyuruOlustur : Form
    {
        public frmDuyuruOlustur()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select * From Duyurular",bgl.baglanti());
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    

        private void frmDuyuruOlustur_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           
            Listele();
        }   
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Duyurular (ıcerık) values (@p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",RchDuyuru.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Duyuru Oluşturuldu.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            bgl.baglanti().Close();
            Listele();
        }
        string id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            RchDuyuru.Text=dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            this.Text = id;
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Seçili kayıtı Silmek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string secili = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                SqlCommand komut = new SqlCommand("Delete from Duyurular where ıd=@p1", bgl.baglanti());
                komut.Parameters.Add("@p1", secili);
                komut.ExecuteNonQuery();
                MessageBox.Show("Duyuru Silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Listele();
            }
            //try
            //{

            //    SqlCommand komut = new SqlCommand("Delete from Duyurular where ID=@p1", bgl.baglanti());
            //    komut.Parameters.AddWithValue("@p1", id);
            //    MessageBox.Show("Duyuru Silindi ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    komut.ExecuteNonQuery();
            //    bgl.baglanti().Close();
            //    Listele();
            //}
            //catch (Exception hata)
            //{

            //    MessageBox.Show("HATA: " + hata);
            //}

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili kayıtı Güncellemek İstiyor Musunuz ?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("update Duyurular set ICERIK=@p1 where ID=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", RchDuyuru.Text);
                komut.Parameters.AddWithValue("@p2", id);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru Başarıyla Güncellendi. ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Listele();
            }
        }
    }
}
