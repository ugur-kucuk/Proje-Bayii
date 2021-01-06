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
    public partial class Urun : Form
    {
        DataTable dt_Urun;
        BindingSource bs_Urun;
        OleDbDataAdapter a1;
        OleDbConnection con;
        string sorgu;
        OleDbCommand komut = new OleDbCommand();
        public Urun()
        {
            InitializeComponent();
        }
        void listele()
        {
            dt_Urun = new DataTable();      // tabloyu oluştur.
            a1 = new OleDbDataAdapter(sorgu, con); // sorguyu adaptöre ata
            a1.Fill(dt_Urun);                       //  adaptörden geleleri tabloa doldur
            bs_Urun = new BindingSource();  // bağlantı nesnesi ile  ilişkilendir.
            bs_Urun.DataSource = dt_Urun;
            dataGridView1.DataSource = bs_Urun;  // data gride bağla ve göster.
            dataGridView1.RowHeadersWidth = 15;
            dataGridView1.Columns[0].Width = 80; dataGridView1.Columns[1].Width = 131;
        }

        private void Urun_Load(object sender, EventArgs e)
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
            sorgu = "Select *from Urun";
            listele();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                komut.Connection = con;
                komut.CommandText = "insert into Urun (Ürün_Kodu,Ürün_Adı,Ürün_Fiyatı) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text.Replace(".", ",") + "')";
                komut.ExecuteNonQuery();
                komut.Dispose();

                sorgu = "Select *from Urun";
                MessageBox.Show("Ürün Eklendi");
                listele();

            }

            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Ürünü silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    komut.Connection = con;
                    komut.CommandText = "delete from Urun where Ürün_Kodu='" + dataGridView1.CurrentRow.Cells["Ürün_Kodu"].Value + "' ";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    sorgu = "select * from Urun";
                    MessageBox.Show("Ürün Silindi");
                    listele();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            XlsFile excel = new XlsFile(true);
            excel.NewFile();

            excel.SetCellValue(1, 3, "Urunler");  // başlık ekle
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
