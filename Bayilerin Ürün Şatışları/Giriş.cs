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
    public partial class Giriş : Form
    {

        OleDbConnection con;
        OleDbDataReader gir,oto;
        string sorgu;
        public Giriş()
        {
            InitializeComponent();
        }

        private void Giriş_Load(object sender, EventArgs e)
        {
        }
        private void Giriş_Shown(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Bayi.mdb");
            con.Open();
            sorgu = "SELECT * FROM Üye";
            OleDbCommand kom = new OleDbCommand(sorgu, con);
            kom.Connection = con;
            kom.CommandText = "SELECT * FROM Üye where Aktif= '0' ";
            oto = kom.ExecuteReader();
            if (oto.Read())
            {
                Ana ana = new Ana();
                ana.Show();
                this.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Bayi.mdb");
            con.Open();
            sorgu = "SELECT * FROM Üye";
            OleDbCommand kom = new OleDbCommand(sorgu, con);
            kom.Connection = con;
            kom.CommandText = "SELECT * FROM Üye where K_Adı='" + textBox1.Text + "' AND Parola='" + textBox2.Text + "'";
            gir = kom.ExecuteReader();
            if (gir.Read())
            {
                gir.Close();
                Ana ana = new Ana();
                ana.Show();
                this.Visible = false;
            }
            else
            {
                label5.Text = "Kullanıcı Adı Veya Parola Yanlış!!!";
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
