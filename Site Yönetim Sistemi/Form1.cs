using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Site_Yönetim_Sistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cmbKtipi.Items.Add("Admin");
            cmbKtipi.Items.Add("Yönetici");
            cmbKtipi.Items.Add("Üye");
         
        }
        siteyonetimsistemiEntities st;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cmbKtipi.SelectedIndex = -1;
            txtKadi.Text = "";
            txtSifre.Text = "";
            try
            {
                this.Show();
            }
            catch
            {
                this.Close();
            }

         }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                st = new siteyonetimsistemiEntities();
                string k_adi = txtKadi.Text;
                string sifre = txtSifre.Text;
                string tipAdi = cmbKtipi.SelectedItem.ToString();
                int tip = cmbKtipi.SelectedIndex;
                kullanici gir = st.kullanicis.Where(x => x.kAdi == k_adi && x.sifre == sifre && x.tipAdi == tipAdi).FirstOrDefault();
                if (gir == null)
                {
                    MessageBox.Show("Öyle bir kullanıcı bulunmamaktadır.");
                }
                else
                {
                    int id = gir.id;
                    this.Hide();
                    switch (tip)
                    {
                        case 0:
                          //  admincs ad = new admincs();
                          //  ad.ShowDialog();
                            break;
                        case 1:
                            yoneticics yn = new yoneticics(id);
                            yn.ShowDialog();
                            break;
                        case 2:
                            uyecs uy = new uyecs(id);
                            uy.ShowDialog();
                            break;
                    }
                }
                Form1_Load(sender, e);
            }
          catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtSifre.PasswordChar = '\0';

            }
            else
            {
                txtSifre.PasswordChar = '*';


            }
        }
    }
}
