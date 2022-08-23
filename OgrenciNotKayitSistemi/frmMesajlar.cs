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
    public partial class frmMesajlar : Form
    {
        public frmMesajlar()
        {
            InitializeComponent();
        }

        public string numara;
        SqlBaglantisi bgl=new SqlBaglantisi();

        private void frmMesajlar_Load(object sender, EventArgs e)
        {
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        
            MskTxtGonderen.Text = numara;
            GelenMesajlar();
            GidenMesajlar();
        } 
        void GelenMesajlar()
        {
            SqlCommand komut = new SqlCommand("select*from Mesajlar where alıcı=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void GidenMesajlar()
        {
            SqlCommand komut = new SqlCommand("select * from Mesajlar where alıcı = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;

        }
    
        string id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            id = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            MskTxtGonderen.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            MskTxtAlici.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtBxKonu.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            RchTxtMesaj.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            this.Text = id;
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Mesajlar (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTxtGonderen.Text);
            komut.Parameters.AddWithValue("@p2", MskTxtAlici.Text);
            komut.Parameters.AddWithValue("@p3", TxtBxKonu.Text);
            komut.Parameters.AddWithValue("@p4", RchTxtMesaj.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Mesajınız Başarıyla İletildi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();
            GelenMesajlar();
            GidenMesajlar();
        }
    }
}
