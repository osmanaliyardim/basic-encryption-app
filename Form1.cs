using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sifreli_Veriler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-N9EER84;Initial Catalog=Test;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLVERILER", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            byte[] addizisi = ASCIIEncoding.ASCII.GetBytes(ad);
            //Base64 ile şifreleniyor (SHA, HASH, MD5 şifrelemeleri de mevcut)
            string adsifre = Convert.ToBase64String(addizisi);
            richTextBox1.Text = adsifre;

            string soyad = txtSoyad.Text;
            byte[] soyaddizisi = ASCIIEncoding.ASCII.GetBytes(soyad);
            string soyadsifre = Convert.ToBase64String(soyaddizisi);
            richTextBox1.Text += " " + soyadsifre;

            string mail = txtMail.Text;
            byte[] maildizisi = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailsifre = Convert.ToBase64String(maildizisi);
            richTextBox1.Text += " " + mailsifre;

            string sifre = txtSifre.Text;
            byte[] sifredizisi = ASCIIEncoding.ASCII.GetBytes(sifre);
            string sifresifre = Convert.ToBase64String(sifredizisi);
            richTextBox1.Text += " " + sifresifre;

            string hesapno = txtHesap.Text;
            byte[] hesapnodizisi = ASCIIEncoding.ASCII.GetBytes(hesapno);
            string hesapnosifre = Convert.ToBase64String(hesapnodizisi);
            richTextBox1.Text += " " + hesapnosifre;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLVERILER (AD,SOYAD,MAIL,SIFRE,HESAPNO) values (@P1,@P2,@P3,@P4,@P5)", baglanti);
            komut.Parameters.AddWithValue("@P1", adsifre);
            komut.Parameters.AddWithValue("@P2", soyadsifre);
            komut.Parameters.AddWithValue("@P3", mailsifre);
            komut.Parameters.AddWithValue("@P4", sifresifre);
            komut.Parameters.AddWithValue("@P5", hesapnosifre);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Veriler eklendi ve başarıyla şifrelendi!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string adcoz = txtAd.Text;
            byte[] adcozdizi = Convert.FromBase64String(adcoz);
            string adverisi = ASCIIEncoding.ASCII.GetString(adcozdizi);
            richTextBox1.Text = adverisi;

            string soyadcoz = txtSoyad.Text;
            byte[] soyadcozdizi = Convert.FromBase64String(soyadcoz);
            string soyadverisi = ASCIIEncoding.ASCII.GetString(soyadcozdizi);
            richTextBox1.Text+=" "+soyadverisi;

            string mailcoz = txtMail.Text;
            byte[] mailcozdizi = Convert.FromBase64String(mailcoz);
            string mailverisi = ASCIIEncoding.ASCII.GetString(mailcozdizi);
            richTextBox1.Text+=" "+mailverisi;

            string sifrecoz = txtSifre.Text;
            byte[] sifrecozdizi = Convert.FromBase64String(sifrecoz);
            string sifreverisi = ASCIIEncoding.ASCII.GetString(sifrecozdizi);
            richTextBox1.Text =" "+sifreverisi;

            string hesapnocoz = txtHesap.Text;
            byte[] hesapnocozdizi = Convert.FromBase64String(hesapnocoz);
            string hesapnoverisi = ASCIIEncoding.ASCII.GetString(hesapnocozdizi);
            richTextBox1.Text+=" "+hesapnoverisi;

            MessageBox.Show("Veri şifreleri başarıyla çözüldü!");
        }
    }
}
