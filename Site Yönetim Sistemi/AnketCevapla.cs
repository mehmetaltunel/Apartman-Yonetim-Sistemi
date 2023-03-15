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
    public partial class AnketCevapla : Form
    {
        int id, uyeid;
        siteyonetimsistemiEntities st;
        List<anketSorulari> ans;
        int i = 0;
        public AnketCevapla(int _id, int _uyeid)
        {
            InitializeComponent();
            id = _id;
            uyeid = _uyeid;
            st = new siteyonetimsistemiEntities();
            ans = st.anketSorularis.Where(x => x.anketId == id).ToList();
            //label1.Text = ans[0].soru;
        }

        private void AnketCevapla_Load(object sender, EventArgs e)
        {
            label1.Text = ans[i].soru;
        }

        private void btnCevapla_Click(object sender, EventArgs e)
        {
            anketSonuclari ank = st.anketSonuclaris.Where(x => x.id == id).FirstOrDefault();
            int s1 = Convert.ToInt32(ank.s1);
            int s2 = Convert.ToInt32(ank.s2);
            int s3 = Convert.ToInt32(ank.s3);
            int s4 = Convert.ToInt32(ank.s4);
            int s5 = Convert.ToInt32(ank.s5);
            if (radioButton1.Checked)
            {
                s1++;
            }
            else if (radioButton2.Checked)
            {
                s2++;
            }
            else if (radioButton3.Checked)
            {
                s3++;
            }
            else if (radioButton4.Checked)
            {
                s4++;
            }
            else
            {
                s5++;
            }
            ank.s1 = s1;
            ank.s2 = s2;
            ank.s3 = s3;
            ank.s4 = s4;
            ank.s5 = s5;
            st.SaveChanges();
            i++;
            radioButton1.Checked = true;
            if (i == ans.Count)
            {

                AnketiGorenler ankg = new AnketiGorenler();
                ankg.anketId = ank.id;
                ankg.uyeId = uyeid;
                st.AnketiGorenlers.Add(ankg);
                st.SaveChanges();
                this.Close();
            }

            AnketCevapla_Load(sender, e);
        }
    }
}
