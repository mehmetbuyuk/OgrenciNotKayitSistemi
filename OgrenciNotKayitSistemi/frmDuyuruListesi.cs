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
    public partial class frmDuyuruListesi : Form
    {
        public frmDuyuruListesi()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void frmDuyuruListesi_Load(object sender, EventArgs e)
        {
            //Listbox Oluşturma
            ListBox listBox = new ListBox();
            Point listBoxKonum = new Point(10,10);
            listBox.Name = "Listbox1";
            listBox.Height = 400;
            listBox.Width = 400;
            listBox.Location= listBoxKonum;
            this.Controls.Add(listBox);
            listBox.Font = new Font("Times New Roman", 13);


            //Duyuruları Oluşturma 
            SqlCommand komut = new SqlCommand("select * from Duyurular",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                listBox.Items.Add(dr[0]+" .Duyuru: "+dr[1]);
            }
            bgl.baglanti().Close();
        }
    }
}
