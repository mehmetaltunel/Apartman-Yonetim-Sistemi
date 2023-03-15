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
    public partial class uyecs : Form
    {
        siteyonetimsistemiEntities st;
        int id;
        int aratmanId;
        uye uy;
        public uyecs(int _id)
        {
            InitializeComponent();
            st = new siteyonetimsistemiEntities();
            id = _id;
            uy = st.uyes.Where(x => x.Id == id).FirstOrDefault();
            aratmanId = Convert.ToInt32(uy.apartmanId);
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
                ptbResim.Image = bmp;
            }
            catch (Exception ex)
            {

            }
        }
        private void uyecs_Load(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
            try
            {
                listBox4.Items.Clear();
                apartman apr = st.apartmen.Where(x => x.id == aratmanId).FirstOrDefault();
                int yoneticiId = Convert.ToInt32(apr.apartmanYoneticisi);
                List<anket> ank = st.ankets.Where(x => x.yoneticiId == yoneticiId).ToList();
                List<AnketiGorenler> ankg = st.AnketiGorenlers.Where(x => x.uyeId == id).ToList();
                bool b;
                for (int i = 0; i < ank.Count; i++)
                {
                    b = true;
                    for (int j = 0; j < ankg.Count; j++)
                    {
                        if (ank[i].id == ankg[j].anketId)
                            b = false;
                    }
                    if (b == true)
                        listBox4.Items.Add(ank[i].anketKonusu);
                }
                if (listBox4.Items.Count == 0)
                    listBox4.Items.Add("Bütün anketler Çözülmüştür!");
            }
            catch (Exception ex)
            {
                listBox4.Items.Add("Bütün anketler Çözülmüştür!");
            }

        }
        private void duyuru()
        {
           
        }
        private void mesaj()
        {

        }
        private void listBoxBosalt()
        {
           

        }
        private void btnResimdegistir_Click(object sender, EventArgs e)
        {
            try
            {
                resimler resimlerr = st.resimlers.Where(x => x.id == id).FirstOrDefault();

                if (resimlerr == null)
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
                        rs.resimAd = "resim";
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

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        void apartmanSakinleri()
        {
            
        }
        private void tabDuyuru_Click(object sender, EventArgs e)
        {

               
            
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            ayarlar ay = new ayarlar(id);
            ay.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbEvSahibi_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
        int oncelik;
        private void lbmesajisim_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           

        }

        private void cmbDaireSakini_SelectedIndexChanged(object sender, EventArgs e)
        {
           
           
        }

        private void btnGonder_Click_1(object sender, EventArgs e)
        {
         

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
           try
            {
                string konu = listBox4.SelectedItem.ToString();
                anket ank = st.ankets.Where(x => x.anketKonusu == konu).FirstOrDefault();
                int ad = ank.id;

                AnketCevapla anc = new AnketCevapla(ad, id);
                anc.ShowDialog();

            }
            catch
            { }
            uyecs_Load(sender, e);

        }

        private void cmbAy_SelectedIndexChanged(object sender, EventArgs e)
        {


           
        }

        private void cmbAys_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbAyd_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbYil_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
