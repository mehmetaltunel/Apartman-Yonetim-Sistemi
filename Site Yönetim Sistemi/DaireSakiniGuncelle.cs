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
    
    public partial class DaireSakiniGuncelle : Form
    {
        siteyonetimsistemiEntities st;
        int id;
        public DaireSakiniGuncelle(int _id)
        {
            InitializeComponent();
            st = new siteyonetimsistemiEntities();
            id = _id;
            daireSakini dr = st.daireSakinis.Where(x => x.id == id).FirstOrDefault();
            txtAd.Text = dr.ad;
            txtSoyad.Text = dr.soyAd;
            txtYas.Text = dr.yas.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAd.Text != "" && txtSoyad.Text != "" && txtYas.Text != "")
                {
                    daireSakini dr =st.daireSakinis.Where(x=>x.id==id).FirstOrDefault();
                    dr.uyeId = id;
                    dr.ad = txtAd.Text;
                    dr.soyAd = txtSoyad.Text;
                    dr.yas = Convert.ToInt32(txtYas.Text);
                  
                    st.SaveChanges();
                    MessageBox.Show("Daire sakini başarıyla eklenmiştir");
                    this.Close();
                }
                else
                    MessageBox.Show("Lütfen Bilgileri Doğru giriniz!");

            }
            catch
            {
                MessageBox.Show("Yaş bilgisi Sayı formatında olmalıdır!");
            }
        }

        private void DaireSakiniGuncelle_Load(object sender, EventArgs e)
        {

        }
    }
}
