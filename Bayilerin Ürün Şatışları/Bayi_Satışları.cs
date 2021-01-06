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
using FlexCel.XlsAdapter;
using FlexCel.Core;

namespace Bayilerin_Ürün_Şatışları
{
    public partial class Bayi_Satışları : Form
    {
        DataTable dt_Bayisat, dt_Bayi, dt_Urun;
        BindingSource bs_Bayisat, bs_Bayi, bs_Urun;
        OleDbDataAdapter a1, a2, a3;
        OleDbConnection con;
        string sorgu;
        double fiy, mik, tut;
        OleDbCommand komut = new OleDbCommand();
        public Bayi_Satışları()
        {
            InitializeComponent();
        }
        void listele()
        {
            dt_Bayisat = new DataTable();      // tabloyu oluştur.
            a1 = new OleDbDataAdapter(sorgu, con); // sorguyu adaptöre ata
            a1.Fill(dt_Bayisat);                       //  adaptörden geleleri tabloa doldur
            bs_Bayisat = new BindingSource();  // bağlantı nesnesi ile  ilişkilendir.
            bs_Bayisat.DataSource = dt_Bayisat;
            dataGridView1.DataSource = bs_Bayisat;  // data gride bağla ve göster.
            dataGridView1.RowHeadersWidth = 15;
            dataGridView1.Columns[0].Width = 90; dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 100; dataGridView1.Columns[3].Width = 85;
            dataGridView1.Columns[4].Width = 50; dataGridView1.Columns[5].Width = 85;
            dataGridView1.Columns[6].Width = 100;
        }
        void listele2()
        {
            dt_Bayi = new DataTable();      // tabloyu oluştur.
            a2 = new OleDbDataAdapter(sorgu, con); // sorguyu adaptöre ata
            a2.Fill(dt_Bayi);                       //  adaptörden geleleri tabloa doldur
            bs_Bayi = new BindingSource();  // bağlantı nesnesi ile  ilişkilendir.
            bs_Bayi.DataSource = dt_Bayi;
        }
        void listele3()
        {
            dt_Urun = new DataTable();      // tabloyu oluştur.
            a3 = new OleDbDataAdapter(sorgu, con); // sorguyu adaptöre ata
            a3.Fill(dt_Urun);                       //  adaptörden geleleri tabloa doldur
            bs_Urun = new BindingSource();  // bağlantı nesnesi ile  ilişkilendir.
            bs_Urun.DataSource = dt_Urun;
        }
        private void Bayi_Satışları_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox5.Enabled = false;
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
            sorgu = "Select *from Bayı_Satıs";
            listele();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            sorgu = "Select *from Bayı Where Bayi_Id='" + comboBox1.Text + "'";
            listele2();
            textBox1.Text = dt_Bayi.Rows[0]["Bayi_Adı"].ToString();
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı";
            listele2();
            comboBox1.Items.Clear();
            for (int k = 0; k <= dt_Bayi.Rows.Count - 1; k++)
            {
                comboBox1.Items.Add(dt_Bayi.Rows[k]["Bayi_Id"].ToString());
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            sorgu = "Select *from Urun Where Ürün_Kodu='" + comboBox2.Text + "'";
            listele3();
            textBox2.Text = dt_Urun.Rows[0]["Ürün_Adı"].ToString();
            textBox3.Text = dt_Urun.Rows[0]["Ürün_Fiyatı"].ToString();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Urun";
            listele3();
            comboBox2.Items.Clear();
            for (int k = 0; k <= dt_Urun.Rows.Count - 1; k++)
            {
                comboBox2.Items.Add(dt_Urun.Rows[k]["Ürün_Kodu"].ToString());

            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox5.Clear();
            fiy = Convert.ToDouble(textBox3.Text);
            mik = Convert.ToInt32(textBox4.Text);
            tut = fiy * mik;
            textBox5.Text = tut.ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı_Satıs";
            listele();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı_Satıs Where Day(Tarih) = '" + comboBox4.Text + "'";
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı_Satıs Where Month(Tarih) = '" + comboBox3.Text + "'";
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı_Satıs Where Year(Tarih) = '" + textBox6.Text + "'";
            listele();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            sorgu = "Select *from Bayı_Satıs Where Day(Tarih) = '" + comboBox4.Text + "'";
            sorgu = "Select *from Bayı_Satıs Where Month(Tarih) = '" + comboBox3.Text + "'";
            sorgu = "Select *from Bayı_Satıs Where Year(Tarih) = '" + textBox6.Text + "'";
            listele();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                komut.Connection = con;
                komut.CommandText = "insert into Bayı_Satıs (Bayi_Id,Ürün_Kodu,Ürün_Adı,Fiyat,Miktar,Tutar,Tarih) values (" +
              " '" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "')";
                komut.ExecuteNonQuery();  // komutu çalıştır.
                komut.Dispose();   // hafızayı boşalt

                MessageBox.Show("Satış Yapıldı", textBox1.Text);
            }

            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            sorgu = "Select *from Bayı_Satıs";
            listele();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            XlsFile excel = new XlsFile(true);
            excel.NewFile();

            excel.SetCellValue(1, 3, " Bayi Satış");  // başlık ekle
            int ek = 3;
            for (int i = 1; i <= dataGridView1.ColumnCount; i++)   // başlıklar
            {
                excel.SetCellValue(3, i, dataGridView1.Columns[i - 1].Name);
            }

            for (int i = 1; i <= dataGridView1.RowCount - 1; i++)   // satırlar
            {

                for (int k = 1; k <= dataGridView1.ColumnCount; k++)
                {
                    excel.SetCellValue(i + ek, k, dataGridView1[k - 1, i - 1].Value.ToString());

                }
            }
            saveFileDialog1.Filter = "*.xls|*.xls";
            saveFileDialog1.ShowDialog();
            string yol2 = saveFileDialog1.FileName;

            try
            {
                excel.Save("" + yol2 + "");
            }
            catch
            {
                MessageBox.Show("Aktarma Başarısız.."); return;
            }
            MessageBox.Show("Excele Aktarma İşlemi Bitti.");
        }
    }
}
