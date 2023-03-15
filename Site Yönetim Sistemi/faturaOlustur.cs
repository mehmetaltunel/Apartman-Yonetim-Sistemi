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
    public partial class faturaOlustur : Form
    {
        int id;
        siteyonetimsistemiEntities st;
        public faturaOlustur(int _id)
        {
            InitializeComponent();
            id = _id;
            st = new siteyonetimsistemiEntities();
            apartman apr = st.apartmen.Where(x => x.apartmanYoneticisi == id).FirstOrDefault();
            List<uye> uyeler = st.uyes.Where(x => x.apartmanId == apr.id).ToList();
            
            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
          
            cmbAy.DataSource = aylar;
        }

        private void faturaOlustur_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                apartman apr = st.apartmen.Where(x => x.apartmanYoneticisi == id).FirstOrDefault();
                List<uye> uyeler = st.uyes.Where(x => x.apartmanId == apr.id).ToList();
                for (int i = 0; i < uyeler.Count; i++)
                {
                    elektrik er = new elektrik();
                    su sr = new su();
                    dogalGaz dg = new dogalGaz();

                    er.faturaİd = uyeler[i].Id;
                    er.miktar = Convert.ToInt32(txtElektrik.Text);
                    er.yil = 2017;
                    er.ay = cmbAy.SelectedIndex + 1;
                    er.faturaNo = id + 7891 + 1;
                    er.sonOdemeTarihi = Convert.ToDateTime("2017" + "-" + (cmbAy.SelectedIndex + 1).ToString() + "-" + 30);
                    er.odenme = 0;

                    sr.faturaId = uyeler[i].Id;
                    sr.miiktar = Convert.ToInt32(txtSu.Text);
                    sr.yil = 2017;
                    sr.ay = cmbAy.SelectedIndex + 1;
                    sr.faturaNo = id + 7891 + 1;
                    sr.sonOdemeTarihi = Convert.ToDateTime("2017" + "-" + (cmbAy.SelectedIndex + 1).ToString() + "-" + 30);
                    sr.odenme = 0;

                    dg.faturaId = uyeler[i].Id;
                    dg.miktar = Convert.ToInt32(txtDogal.Text);
                    dg.yil = 2017;
                    dg.ay = cmbAy.SelectedIndex + 1;
                    dg.faturaNo = id + 7891 + 1;
                    dg.sonOdemeTarihi = Convert.ToDateTime("2017" + "-" + (cmbAy.SelectedIndex + 1).ToString() + "-" + 30);
                    dg.odenme = 0;

                    st.elektriks.Add(er);
                    st.sus.Add(sr);
                    st.dogalGazs.Add(dg);
                    st.SaveChanges();
                    
                }
                          MessageBox.Show("Fatura Başarıyla olşmuştur");
            this.Close();  
            }
            catch
            { }

        }
    }
}
