using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Site_Yönetim_Sistemi
{
    public partial class detayliBilgi :Form
    {
        siteyonetimsistemiEntities st;
        int id;
        int fiyat;
        public detayliBilgi(int _id)
        {
            InitializeComponent();
            id = _id;
            st = new siteyonetimsistemiEntities();

            kullanici kl = st.kullanicis.Where(x => x.id == id).FirstOrDefault();
            label1.Text = kl.ad + " " + kl.soyAd;

            lblIsım.Text = kl.ad;
            lblSoyisim.Text = kl.soyAd;
            lblDogumTarihi.Text = kl.dogumTarihi.ToString();
            if (kl.cinsiyet == "e")
                lblCinsiyet.Text = "Erkek";
            else
                lblCinsiyet.Text = "Kadın";
            lblMail.Text = kl.mailAdresi;
            lblTelefon.Text = kl.telefon;
            if (kl.medeniDurum == "e")
                lblMedeniDurum.Text = "Evli";
            else
                lblMedeniDurum.Text = "Bekar";
            uye uy = st.uyes.Where(x => id == x.Id).FirstOrDefault();

            int aratmanId = Convert.ToInt32(uy.apartmanId);
            apartman aprt = st.apartmen.Where(x => x.id == aratmanId).FirstOrDefault();
            lblApartmanAdi.Text = aprt.apartmanAdi;
            lblBlok.Text = aprt.apartmanBlok;
            lblKat.Text = uy.kat.ToString();
            lblDaireno.Text = uy.daireNo.ToString();
            fiyat = Convert.ToInt32(aprt.aidat);
            fiyat = fiyat / Convert.ToInt32(aprt.daireSayisi);
            kullanici kulY = st.kullanicis.Where(x => x.id == aprt.apartmanYoneticisi).FirstOrDefault();
            lblYonetici.Text = kulY.ad;
            lablesoyad.Text = kulY.soyAd;
            var drs = st.daireSakinis.Where(x => x.uyeId == id).Select(x => x.ad).ToList();// daire sakinleri dolduruldu
            cmbDaireSakini.DataSource = drs;
            resimGetir();
        }
        private void resimGetir()
        {
            try
            {
                resimler rs = st.resimlers.Where(x => x.id == id).FirstOrDefault();
                byte[] resim = rs.resim;
                MemoryStream mstream = new MemoryStream();
                mstream.Write(resim, 0, Convert.ToInt32(resim.Length));
                Bitmap bmp = new Bitmap(mstream, false);
                pictureBox1.Image = bmp;
            }
            catch (Exception ex)
            {

            }
        }
        private void detayliBilgi_Load(object sender, EventArgs e)
        {
            string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
            cmbAy.DataSource = aylar;
        }

        private void cmbDaireSakini_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
        }

        private void cmbAy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string yaz;
            listBox2.Items.Clear();
            int index = cmbAy.SelectedIndex + 1;
            aidatAy ay = st.aidatAys.Where(x => x.aidatId == id && x.ay == index).FirstOrDefault();
            if (ay == null)
            {
                yaz = fiyat + "TL " + "Ödenmedi!";
            }
            else
                yaz = fiyat + "TL " + "Ödendi";
            listBox2.Items.Add(yaz);
        }

        private void btnOdendi_Click_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            int index = cmbAy.SelectedIndex + 1;
            aidatAy ay = st.aidatAys.Where(x => x.aidatId == id && x.ay == index).FirstOrDefault();
            if (ay == null)
            {
                try
                {
                    aidatAy ad = new aidatAy();
                    ad.aidatId = id;
                    ad.ay = index;
                    st.aidatAys.Add(ad);
                    st.SaveChanges();
                    MessageBox.Show("Aidat Ödenmiştir!");
                    cmbAy.SelectedIndex = index - 1;
                    cmbAy_SelectedIndexChanged_1(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
            {

            }



        }

        private void btnFaturalar_Click(object sender, EventArgs e)
        {

        }

        private void cmbDaireSakini_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string i = cmbDaireSakini.SelectedItem.ToString();
            daireSakini drskn = st.daireSakinis.Where(x => x.ad == i).FirstOrDefault();
            List<aracbilgisi> arc = st.aracbilgisis.Where(x => x.aracSahibi == drskn.id).ToList();
            listBox1.Items.Add("Ad     :" + drskn.ad);
            listBox1.Items.Add("Soyad  :" + drskn.soyAd);
            listBox1.Items.Add("Yaş    :" + drskn.yas);
            listBox1.Items.Add("");
            listBox1.Items.Add("Araç Bilgisi");
            listBox1.Items.Add("");

            for (int j = 0; j < arc.Count; j++)
            {


                listBox1.Items.Add(arc[j].aracTipi.ToString());
                listBox1.Items.Add(arc[j].plaka.ToString());
                listBox1.Items.Add("");
            }
        }
    }

}
