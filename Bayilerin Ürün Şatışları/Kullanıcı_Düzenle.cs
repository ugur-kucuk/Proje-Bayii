using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Bayilerin_Ürün_Şatışları
{
    public partial class Kullanıcı_Düzenle : Form
    {
        DataTable dt_Uye;
        BindingSource bs_Uye;
        OleDbDataAdapter a1;
        OleDbConnection con;
        string sorgu;
        OleDbCommand komut = new OleDbCommand();
        public Kullanıcı_Düzenle()
        {
            InitializeComponent();
        }
        void listele()
        {
            dt_Uye = new DataTable();      // tabloyu oluştur.
            a1 = new OleDbDataAdapter(sorgu, con); // sorguyu adaptöre ata
            a1.Fill(dt_Uye);                       //  adaptörden geleleri tabloa doldur
            bs_Uye = new BindingSource();  // bağlantı nesnesi ile  ilişkilendir.
            bs_Uye.DataSource = dt_Uye;
        }
        private void Kullanıcı_Load(object sender, EventArgs e)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Bayi.mdb");
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();   // bağlan
                }
            }
            catch
            {
                MessageBox.Show("veritabanı ile bağlantı sağlanamadı");
            }
            sorgu = "Select *from Üye";
            listele();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string durum;
            if (checkBox1.Checked == true)
                durum = "1";
            else
                durum = "0";
            try
            {
                komut.Connection = con;
                komut.CommandText = "Update Üye Set K_Adı = '" + textBox1.Text + "', Parola = '" + textBox2.Text + "',Aktif ='" + durum + "'";
                komut.ExecuteNonQuery();
                komut.Dispose();
                MessageBox.Show("Bilgileriniz Güncellenmiştir.");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
