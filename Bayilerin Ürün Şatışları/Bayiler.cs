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
    public partial class Bayiler : Form
    {
        DataTable dt_Bayi;
        BindingSource bs_Bayi;
        OleDbDataAdapter a1;
        OleDbConnection con;
        string sorgu;
        OleDbCommand komut = new OleDbCommand();
        public Bayiler()
        {
            InitializeComponent();
        }
        void nesneler()
        {
            textBox1.DataBindings.Clear();
            textBox2.DataBindings.Clear();

            textBox1.DataBindings.Add("text", bs_Bayi, "Bayi_Id");
            textBox2.DataBindings.Add("text", bs_Bayi, "Bayi_Adı");
        }
        void listele()
        {
            dt_Bayi = new DataTable();      // tabloyu oluştur.
            a1 = new OleDbDataAdapter(sorgu, con); // sorguyu adaptöre ata
            a1.Fill(dt_Bayi);                       //  adaptörden geleleri tabloa doldur
            bs_Bayi = new BindingSource();  // bağlantı nesnesi ile  ilişkilendir.
            bs_Bayi.DataSource = dt_Bayi;
            dataGridView1.DataSource = bs_Bayi;  // data gride bağla ve göster.
            dataGridView1.RowHeadersWidth = 15;
            dataGridView1.Columns[0].Width = 80; dataGridView1.Columns[1].Width = 131;
            nesneler();
        }
        private void Bayiler_Load(object sender, EventArgs e)
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
            sorgu = "Select *from Bayı";
            listele();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                komut.Connection = con;
                komut.CommandText = "insert into Bayı (Bayi_Id,Bayi_Adı) values ('" + textBox1.Text + "','" + textBox2.Text + "')";
                komut.ExecuteNonQuery();
                komut.Dispose();

                sorgu = "Select *from Bayı";
                MessageBox.Show("Bayi Eklendi");
                listele();

            }

            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Bayiyi silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    komut.Connection = con;
                    komut.CommandText = "delete from Bayı where Bayi_Id='" + dataGridView1.CurrentRow.Cells["Bayi_Id"].Value + "' ";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    sorgu = "select * from Bayı";
                    MessageBox.Show("Bayi Silindi");
                    listele();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı Where Bayi_Id = '" + textBox1.Text + "'";
            listele();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı";
            listele();
        }
    }
}
