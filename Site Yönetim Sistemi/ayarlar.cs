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
    public partial class ayarlar : Form
    {
        int id;
        string sifre;
        siteyonetimsistemiEntities st;
        kullanici kl;
        public ayarlar(int _id)
        {
            InitializeComponent();
            id = _id;
            st = new siteyonetimsistemiEntities();
        }

        private void ayarlar_Load(object sender, EventArgs e)
        {
           kl = st.kullanicis.Where(x => x.id == id).FirstOrDefault();
            string kadi = kl.kAdi.ToString();
           sifre = kl.sifre.ToString();
            
            textBox1.Text = kadi;
            checkBox1.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sifreDegis = textBox2.Text;
            string sifreDegisTekrar = textBox3.Text;
            if(textBox1.Text!="" && textBox2.Text!="" && textBox3.Text!="" && textBox4.Text==sifre)
            {
                if(sifreDegis==sifreDegisTekrar)
                {
                    kl.kAdi = textBox1.Text;
                    kl.sifre = sifreDegis;
                    DialogResult secenek = MessageBox.Show("Hesap bilgilerinizi değiştirmek istediğinize emin misinz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (secenek == DialogResult.Yes)
                    {
    
                        st.SaveChanges();
   

                    }
                    else if (secenek == DialogResult.No)
                    {
                        MessageBox.Show("İşlem iptal edildi..");
                    }

                    st.SaveChanges();
                    MessageBox.Show("Şifre Başarıyla değiştirildi.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Şifreler eşleşmiyor");
                }

            }
            else
            {
                MessageBox.Show("Bilgiler boş olamaz!");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
