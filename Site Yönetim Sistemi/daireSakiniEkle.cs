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
    public partial class daireSakiniEkle : Form
    {
        int id;
        siteyonetimsistemiEntities st;

        public daireSakiniEkle(int _id)
        {
            InitializeComponent();
            id = _id;
            st = new siteyonetimsistemiEntities();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAd.Text != "" && txtSoyad.Text != "" && txtYas.Text != "")
                {
                    daireSakini dr = new daireSakini();
                    dr.uyeId = id;
                    dr.ad = txtAd.Text;
                    dr.soyAd = txtSoyad.Text;
                    dr.yas = Convert.ToInt32(txtYas.Text);
                    st.daireSakinis.Add(dr);
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
    }
}
