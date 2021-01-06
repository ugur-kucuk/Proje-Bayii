using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bayilerin_Ürün_Şatışları
{
    public partial class Ana : Form
    {
        public Ana()
        {
            InitializeComponent();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Kullanıcı_Düzenle Kul = new Kullanıcı_Düzenle();
            Kul.MdiParent = this;
            Kul.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Bayiler bay = new Bayiler();
            bay.MdiParent = this;
            bay.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Bayi_Satışları bay_sat = new Bayi_Satışları();
            bay_sat.MdiParent = this;
            bay_sat.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Urun ur = new Urun();
            ur.MdiParent = this;
            ur.Show();
        }

        private void Ana_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
