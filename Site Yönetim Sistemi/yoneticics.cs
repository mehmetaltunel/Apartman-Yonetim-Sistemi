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
    public partial class yoneticics : Form
    {
        int id;
        siteyonetimsistemiEntities st;
        kullanici kl;
        uye uy;
        int aratmanId;
        public yoneticics(int _id)
        {

            InitializeComponent();
            try
            {
                id = _id;
                st = new siteyonetimsistemiEntities();
                kl = st.kullanicis.Where(x => x.id == id).FirstOrDefault();
                label1.Text = "Hoş geldinniz " + kl.ad + " " + kl.soyAd;
                resimGetir();
                uy = st.uyes.Where(x => x.Id == id).FirstOrDefault();
                aratmanId = Convert.ToInt32(uy.apartmanId);
                panelPasifEt();
                var ank = st.ankets.Where(x => x.yoneticiId == id).Select(x => x.anketKonusu).ToList();
                lblAnketler.DataSource = ank;
                guvenlik gv = st.guvenliks.Where(x => x.id == aratmanId).FirstOrDefault();
                lblGuvenlik.Text = gv.Ad + " " + gv.Soyad;
                lbAnketYapan.DataSource = ank;

            }
            catch { }

        }
        void SongelenMesajlar()
        {

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
                ptbResim.Image = bmp;
            }
            catch (Exception ex)
            {

            }
        }
        private void yoneticics_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            var uyell = st.uyes.Where(x => x.Id == id);

            mesaj();


        }

        private void panelPasifEt()
        {
            pnlBilgiler.Visible = false;
            pnlDaireler.Visible = false;

        }
        private void btnResimdegistir_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (ptbResim.Image == null)
                {
                    openFileDialog1.Title = "Resim aç";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                        string isim = openFileDialog1.FileName;
                        FileStream fs = new FileStream(isim, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        byte[] resim = br.ReadBytes((int)fs.Length);
                        br.Close();
                        fs.Close();
                        resimler rs = new resimler();
                        rs.id = id;
                        rs.resim = resim;
                        st.resimlers.Add(rs);
                        st.SaveChanges();
                        ptbResim.Image = bmp;
                        MessageBox.Show("Resim başarıyla değiştirildi.");

                    }
                }
                else
                {
                    openFileDialog1.Title = "Resim aç";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                        string isim = openFileDialog1.FileName;
                        FileStream fs = new FileStream(isim, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        byte[] resim = br.ReadBytes((int)fs.Length);
                        br.Close();
                        fs.Close();
                        resimler rs = st.resimlers.Where(x => x.id == id).FirstOrDefault();
                        rs.id = id;
                        rs.resimAd = "resim" + id.ToString();
                        rs.resim = resim;
                        st.SaveChanges();
                        ptbResim.Image = bmp;
                        MessageBox.Show("Resim başarıyla değiştirildi.");
                    }
                }
            }

            catch
            {
                MessageBox.Show("Lütfen bir resim dosyası seçiniz!");
            }

        }




        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            try

            {


                if (tabControl1.SelectedIndex == 0)
                {


                }
                else if (tabControl1.SelectedIndex == 1)
                {

                    //  tabControl1.SelectedIndex = 0;
                    panelPasifEt();
                    pnlBilgiler.Visible = true;
                    lblIsım.Text = kl.ad;
                    lblSoyisim.Text = kl.soyAd;
                    lblDogumTarihi.Text = kl.dogumTarihi.ToString();
                    if (kl.cinsiyet == "e")
                        lblCinsiyet.Text = "Erkek";
                    else
                        lblCinsiyet.Text = "Kadın";
                    lblMail.Text = kl.mailAdresi;
                    lblTelefon.Text = kl.telefon;
                    if (kl.mailAdresi == "e")
                        lblMedeniDurum.Text = "Evli";
                    else
                        lblMedeniDurum.Text = "Bekar";
                    uye uy = st.uyes.Where(x => id == x.Id).FirstOrDefault();
                    aratmanId = Convert.ToInt32(uy.apartmanId);

                    apartman aprt = st.apartmen.Where(x => x.id == aratmanId).FirstOrDefault();
                    lblApartmanAdi.Text = aprt.apartmanAdi;
                    lblBlok.Text = aprt.apartmanBlok;
                    lblKat.Text = uy.kat.ToString();
                    lblDaireno.Text = uy.daireNo.ToString();

                    kullanici kulY = st.kullanicis.Where(x => x.id == aprt.apartmanYoneticisi).FirstOrDefault();
                    lblYonetici.Text = kulY.ad;
                    lablesoyad.Text = kulY.soyAd;


                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    apartmanSakinleri();
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                    mesaj();
                }
                else if (tabControl1.SelectedIndex == 4)
                {
                    duyuru();
                }
            }
            catch
            { }
        }

        private void duyuru()
        {
            List<duyurular> duy = st.duyurulars.Where(x => x.yoneticiId == id).ToList();
            listBox1.Items.Clear();

            for (int i = duy.Count - 1; i > -1; i--)
            {
                listBox1.Items.Add(duy[i].duyuruKonusu);
            }
        }
        private void listBoxBosalt()
        {
            lbDaireNo.Items.Clear();
            lbEvSahibi.Items.Clear();
            lbKat.Items.Clear();
            lbKisiSayisi.Items.Clear();

        }

        void apartmanSakinleri()
        {
            listBoxBosalt();
            panelPasifEt();
            pnlDaireler.Visible = true;

            apartman apr = st.apartmen.Where(x => x.id == aratmanId).FirstOrDefault();

            lblApartman.Text = apr.apartmanAdi + " Apartmanı " + apr.apartmanBlok + " Blok";
            List<uye> uyeler = st.uyes.Where(x => x.apartmanId == aratmanId).OrderBy(x => x.daireNo).ToList();

            var uyeId = st.uyes.Where(x => x.apartmanId == aratmanId).Select(x => x.Id).ToList();
            List<kullanici> kullanicilar = st.kullanicis.ToList();

            List<daireSakini> daireSakin = st.daireSakinis.ToList();
            for (int i = 0; i < uyeler.Count; i++)
            {
                lbKat.Items.Add(uyeler[i].kat);
                lbDaireNo.Items.Add(uyeler[i].daireNo);
                var kad = kullanicilar.Where(x => x.id == uyeler[i].Id).Select(x => x.ad).FirstOrDefault();
                var ksoyad = kullanicilar.Where(x => x.id == uyeler[i].Id).Select(x => x.soyAd).FirstOrDefault();
                var kisiSay = daireSakin.Where(x => x.uyeId == uyeId[i]).ToList();
                lbKisiSayisi.Items.Add((Convert.ToInt32(kisiSay.Count) + 1));
                lbEvSahibi.Items.Add(kad + " " + ksoyad);
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            uyeekle uy = new uyeekle(id);
            uy.ShowDialog();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {


        }


        private void btnDetayliBilgi_Click_1(object sender, EventArgs e)
        {

            try
            {
                int daireNo = Convert.ToInt32(lbDaireNo.SelectedItem);
                uye uyeler = st.uyes.Where(x => x.daireNo == daireNo && x.apartmanId == uy.apartmanId).FirstOrDefault();
                detayliBilgi dt = new detayliBilgi(uyeler.Id);
                dt.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Lütfen birini seçiniz");
            }
            apartmanSakinleri();
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
        {

            try
            {
                int daireNo = Convert.ToInt32(lbDaireNo.SelectedItem);
                uye uyeler = st.uyes.Where(x => x.daireNo == daireNo && x.apartmanId == uy.apartmanId).FirstOrDefault();
                Guncelle dt = new Guncelle(uyeler.Id);
                dt.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Lütfen birini seçiniz");
            }
            apartmanSakinleri();
        }

        private void btnEkle_Click_1(object sender, EventArgs e)
        {
            uyeekle uy = new uyeekle(id);
            uy.ShowDialog();
            apartmanSakinleri();
        }

        private void pnlBilgiler_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            ayarlar ay = new ayarlar(id);
            ay.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Çıkış yapmak istediğinize emin misinz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("İşlem iptal edildi..");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Çıkış yapmak istediğinize emin misinz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                this.Close();
            }
            else if (secenek == DialogResult.No)
            {
                MessageBox.Show("İşlem iptal edildi..");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int daireNo = Convert.ToInt32(lbDaireNo.SelectedItem);
                uye uyeler = st.uyes.Where(x => x.daireNo == daireNo && x.apartmanId == aratmanId).FirstOrDefault();


                aidatlar ai = st.aidatlars.Where(x => x.id == uyeler.Id).FirstOrDefault();
                List<aidatAy> ad = st.aidatAys.Where(x => x.aidatId == uyeler.Id).ToList();
                fatura fr = st.faturas.Where(x => x.id == uyeler.Id).FirstOrDefault();
                kullanici kll = st.kullanicis.Where(x => x.id == uyeler.Id).FirstOrDefault();
                List<daireSakini> dr = st.daireSakinis.Where(x => x.uyeId == uyeler.Id).ToList();
                resimler rs = st.resimlers.Where(x => x.id == uyeler.Id).FirstOrDefault();
                mesaj ms = st.mesajs.Where(x => x.id == uyeler.Id).FirstOrDefault();
                List<gelenMesaj> gms = st.gelenMesajs.Where(x => x.mesajId == uyeler.Id).ToList();
                for (int i = 0; i < gms.Count; i++)
                {
                    st.gelenMesajs.Remove(gms[i]);
                }
                st.mesajs.Remove(ms);
                for (int i = 0; i < dr.Count; i++)
                {
                    st.daireSakinis.Remove(dr[i]);
                }
                List<aracbilgisi> ar = st.aracbilgisis.Where(x => x.uyeId == uyeler.Id).ToList();
                for (int i = 0; i < ar.Count; i++)
                {
                    st.aracbilgisis.Remove(ar[i]);
                }
                for (int i = 0; i < ad.Count; i++)
                {
                    st.aidatAys.Remove(ad[i]);
                }
                List<elektrik> el = st.elektriks.Where(x => x.faturaİd == uyeler.Id).ToList();
                List<su> su = st.sus.Where(x => x.faturaId == uyeler.Id).ToList();
                List<dogalGaz> dg = st.dogalGazs.Where(x => x.faturaId == uyeler.Id).ToList();
                for (int i = 0; i < el.Count; i++)
                {
                    st.elektriks.Remove(el[i]);
                }
                for (int i = 0; i < su.Count; i++)
                {
                    st.sus.Remove(su[i]);
                }
                for (int i = 0; i < dg.Count; i++)
                {
                    st.dogalGazs.Remove(dg[i]);
                }
                DialogResult secenek = MessageBox.Show(kll.ad + " isme sahip üyeyi silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (secenek == DialogResult.Yes)
                {
                    st.faturas.Remove(fr);
                    st.aidatlars.Remove(ai);
                    st.uyes.Remove(uyeler);
                    st.kullanicis.Remove(kl);
                    st.SaveChanges();
                    MessageBox.Show("Üye Silindi!");
                    listBox1.SelectedIndex = 0;

                }
                else if (secenek == DialogResult.No)
                {
                    MessageBox.Show("Silme işlemi iptal edildi..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            apartmanSakinleri();
        }

        private void lbKat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            lbKat.SelectedIndex = lb.SelectedIndex;
            lbDaireNo.SelectedIndex = lb.SelectedIndex;
            lbEvSahibi.SelectedIndex = lb.SelectedIndex;
            lbKisiSayisi.SelectedIndex = lb.SelectedIndex;
        }

        private void btnYayinla_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDuyuru.Text.Length > 0 && txtDuyuruKonu.Text.Length > 0)
                {
                    duyurular dr = new duyurular();
                    dr.duyuruKonusu = txtDuyuruKonu.Text;
                    dr.yoneticiId = id;
                    dr.duyurAdi = txtDuyuru.Text;
                    st.duyurulars.Add(dr);
                    st.SaveChanges();
                    MessageBox.Show("Duyuru başarıyla yayınlanmıştır!");
                }
                else
                    MessageBox.Show("Lütfen bilgileri doğru bir şekilde doldurunuz!");
            }
            catch
            { }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                List<duyurular> duy = st.duyurulars.Where(x => x.yoneticiId == id).ToList();
                int top = duy.Count - 1;
                int index = listBox1.SelectedIndex;
                listBox2.Items.Add(duy[top - index].duyurAdi);
            }
            catch
            { }
        }
        int oncelik;
        private void lbmesajisim_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sec = lbmesajisim.SelectedIndex;
            lbMesaj.Items.Clear();
            try
            {
                List<gelenMesaj> mesajlar = st.gelenMesajs.ToList();
                List<kullanici> kullanicilar = st.kullanicis.ToList();
                var uyeId = st.uyes.Where(x => x.apartmanId == aratmanId).Select(x => x.Id).ToList();
                int uyeid = uyeId[sec];
                string ben = kl.ad + " " + kl.soyAd + " :";
                var kad = kullanicilar.Where(x => x.id == uyeid).Select(x => x.ad).FirstOrDefault();
                var kSoyAd = kullanicilar.Where(x => x.id == uyeid).Select(x => x.soyAd).FirstOrDefault();

                List<gelenMesaj> mesajla = st.gelenMesajs.Where(x => ((x.mesajId == id) || (x.mesajId == uyeid)) && ((x.gelenId == id && x.gidenId == uyeid) || (x.gelenId == uyeid && x.gidenId == id))).OrderBy(x => x.oncelik).ToList();
                string mesaj = "";
                bool b = true;
                for (int i = 0; i < mesajlar.Count; i++)
                {

                    if (mesajla[i].mesajId == id)
                    {
                        mesaj = ben + mesajla[i].mesaj;

                    }
                    else
                    {
                        mesaj = kad + " " + kSoyAd + " : " + mesajla[i].mesaj;

                    }
                    lbMesaj.Items.Add(mesaj);
                    oncelik = Convert.ToInt32(mesajla[i].oncelik);
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {

                int index = lbmesajisim.SelectedIndex;
                var uyeId = st.uyes.Where(x => x.apartmanId == aratmanId).Select(x => x.Id).ToList();
                int uyeid = uyeId[index];

                oncelik++;
                gelenMesaj gm = new gelenMesaj();
                gm.mesaj = txtMesaj.Text;
                gm.gidenId = id;
                gm.gelenId = uyeid;
                gm.mesajId = id;
                gm.oncelik = oncelik;
                st.gelenMesajs.Add(gm);
                st.SaveChanges();
                txtMesaj.Clear();
                lbmesajisim_SelectedIndexChanged(sender, e);
            }
            catch
            { }

        }
        private void mesaj()
        {
            lbmesajisim.Items.Clear();
            List<uye> uyeler = st.uyes.Where(x => x.apartmanId == aratmanId).ToList();

            var uyeId = st.uyes.Where(x => x.apartmanId == aratmanId).Select(x => x.Id).ToList();

            List<kullanici> kullanicilar = st.kullanicis.ToList();

            for (int i = 0; i < uyeler.Count; i++)
            {
                lbKat.Items.Add(uyeler[i].kat);
                lbDaireNo.Items.Add(uyeler[i].daireNo);
                var kad = kullanicilar.Where(x => x.id == uyeId[i]).Select(x => x.ad).FirstOrDefault();

                lbmesajisim.Items.Add(kad);
            }
        }

        private void btnBitir_Click(object sender, EventArgs e)
        {
            if (txtAnketKonusu.Text.Length > 0)
            {

                anketSorulari sorular;
                AnketiGorenler ankGorenler = new AnketiGorenler();
                ankGorenler.anketId = ankId;
                st.AnketiGorenlers.Add(ankGorenler);
                st.SaveChanges();
                for (int i = 0; i < lbAnketSorulari.Items.Count; i++)
                {
                    sorular = new anketSorulari();
                    sorular.soru = lbAnketSorulari.Items[i].ToString();
                    sorular.anketId = ankId;
                    st.anketSorularis.Add(sorular);
                    st.SaveChanges();
                }
                MessageBox.Show("Anket Başarıyla Hazırlanmıştır!");
                txtAnketKonusu.Enabled = true;
                btnAnketBaslat.Enabled = true;
                lbAnketSorulari.Enabled = false;
                btnSoruEkle.Enabled = false;
                btnBitir.Enabled = false;
                txtAnketSorusu.Enabled = false;
                txtAnketKonusu.Text = "";
                lbAnketSorulari.Items.Clear();
                txtAnketSorusu.Text = "";
            }
            else
                MessageBox.Show("Lütfen Konuyu giriniz!");


        }
        int ankId;
        private void btnSoruEkle_Click_1(object sender, EventArgs e)
        {
            if (txtAnketSorusu.Text != "")
            {
                lbAnketSorulari.Items.Add((i + 1) + ") " + txtAnketSorusu.Text);

                txtAnketSorusu.Clear();
                i++;
            }
        }
        int i;
        private void btnAnketBaslat_Click_1(object sender, EventArgs e)
        {
            i = 0;
            try
            {
                anket ank = new anket();
                ank.anketKonusu = txtAnketKonusu.Text;
                ank.yoneticiId = id;
                st.ankets.Add(ank);
                anketSonuclari anks = new anketSonuclari();

                anks.s1 = 0;
                anks.s2 = 0;
                anks.s3 = 0;
                anks.s4 = 0;
                anks.s5 = 0;
                anks.kisiSayisi = 0;
                st.anketSonuclaris.Add(anks);
                st.SaveChanges();
                ankId = ank.id;
                txtAnketKonusu.Enabled = false;
                btnAnketBaslat.Enabled = false;
                lbAnketSorulari.Enabled = true;
                btnSoruEkle.Enabled = true;
                btnBitir.Enabled = true;
                txtAnketSorusu.Enabled = true;
            }
            catch
            { }
        }

        private void btnSecileniSil_Click_1(object sender, EventArgs e)
        {
            try
            {
                lbAnketSorulari.Items.RemoveAt(lbAnketSorulari.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("Lütfen Soru Seçiniz!");
            }

        }

        private void lblAnketler_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                anket an = st.ankets.Where(x => x.yoneticiId == id && x.anketKonusu == lblAnketler.SelectedItem).FirstOrDefault();
                anketSonuclari ankS = st.anketSonuclaris.Where(x => x.id == an.id).FirstOrDefault();
                int s1 = Convert.ToInt32(ankS.s1);
                int s2 = Convert.ToInt32(ankS.s2);
                int s3 = Convert.ToInt32(ankS.s3);
                int s4 = Convert.ToInt32(ankS.s4);
                int s5 = Convert.ToInt32(ankS.s5);

                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }

                chart1.Series["MEMNUNIYET"].Points.Add(s1);
                chart1.Series["MEMNUNIYET"].Points.Add(s2);
                chart1.Series["MEMNUNIYET"].Points.Add(s3);
                chart1.Series["MEMNUNIYET"].Points.Add(s4);
                chart1.Series["MEMNUNIYET"].Points.Add(s5);

                chart1.Series["MEMNUNIYET"].Points[0].AxisLabel = "Çok iyi";
                chart1.Series["MEMNUNIYET"].Points[1].AxisLabel = "İyi";
                chart1.Series["MEMNUNIYET"].Points[2].AxisLabel = "Orta";
                chart1.Series["MEMNUNIYET"].Points[3].AxisLabel = "Kötü";
                chart1.Series["MEMNUNIYET"].Points[4].AxisLabel = "Çok Kötü";

                chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -70;
            }
            catch
            { }
        }

        private void lbMesajAtan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbAnketYapan_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            try
            {
                anket a = st.ankets.Where(x => x.yoneticiId == id && x.anketKonusu == lbAnketYapan.SelectedItem).FirstOrDefault();
                List<AnketiGorenler> gorenler = st.AnketiGorenlers.Where(x => x.anketId == a.id).ToList();
                List<kullanici> kr = st.kullanicis.ToList();
                int n = Convert.ToInt32(gorenler[1].id);
                for (int i = 0; i < gorenler.Count; i++)
                {
                    try
                    {
                        kullanici kt = kr.Where(x => x.id == gorenler[i].uyeId).FirstOrDefault();
                        string k = kt.ad + " " + kt.soyAd;
                        listBox3.Items.Add(k);
                    }
                    catch
                    { }

                }
            }
            catch
            {
                listBox3.Items.Add("Cevaplayan Kimse yok!");
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFaturaOlustur_Click(object sender, EventArgs e)
        {

            faturaOlustur fr = new faturaOlustur(id);
            fr.ShowDialog();
        }





        private void cmbAy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
