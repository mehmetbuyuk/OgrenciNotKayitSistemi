using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace OgrenciNotKayitSistemi
{
     class SqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-1OC2DR4;Initial Catalog=OgrenciNotKayitDB;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
