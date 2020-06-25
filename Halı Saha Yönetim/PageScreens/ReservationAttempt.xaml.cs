using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;
using System.Data;

namespace Halı_Saha_Yönetim.PageScreens
{
    /// <summary>
    /// Interaction logic for ReservationAttempt.xaml
    /// </summary>
    public partial class ReservationAttempt : Page
    {
        db_sahaEntities db = new db_sahaEntities();
        public ReservationAttempt()
        {
            InitializeComponent();
            FillDataGrid();
        }
        SqlConnection con;
        SqlCommand cmd;
        public void sqlConStarter()
        {
            con = new SqlConnection(@"data source=.\SQLEXPRESS02;initial catalog=db_saha;integrated security=True;");
            con.Open();

        }

        string tarih = "";
        string saat = "";
        private string connectionString;

        public void Tarih_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            tarih = date.Value.ToShortDateString();
        }

        public void cmbSaat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            saat = cmbSaat.SelectedValue.ToString().Replace("System.Windows.Controls.ComboBoxItem:", "").Trim();
            if (tarih != "" && saat != "")
            {
                var sahalar = db.tbl_saha.ToList();
                sqlConStarter();
                cmd = new SqlCommand("SELECT * FROM tbl_rezervasyon WHERE TARIH='" + tarih + "' AND SAAT='" + saat + "'", con);
                SqlDataReader oku = cmd.ExecuteReader();
                string ver = "";
                while (oku.Read())
                {
                    if (ver != "")
                        ver += ",";
                    ver += oku["REZ_SAHA_ID"].ToString().Trim();
                }
                con.Close();

                sqlConStarter();
                if (ver != "") { 
                cmd = new SqlCommand("SELECT * FROM TBL_SAHA WHERE SAHA_ID NOT IN (" + ver + ")", con);

                }
                else
                    cmd = new SqlCommand("SELECT * FROM TBL_SAHA", con);

                oku = cmd.ExecuteReader();
                while (oku.Read())
                {
                    cmbSaha.Items.Add(oku["SAHA_ADI"].ToString());
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Lütfen tarih ve saat bilgilerini eksiksiz giriniz...");
                //}
            }
        }
        private void FillDataGrid()
        {
            sqlConStarter();
            cmd = new SqlCommand("select * from tbl_rezervasyon", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("tbl_rezervasyon");
            sda.Fill(dt);
            dgRezervasyon.ItemsSource = dt.DefaultView;

        }

        private void Rez_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSaha.SelectedItem == null )
            { 
                MessageBox.Show("Lütfen rezervasyon yapmak istediğiniz sahayı seçiniz...");
            }

            else if(txtRezSahip == null)
            {
                MessageBox.Show("Lütfen rezervasyon sahibini belirtiniz...");
            }

            else if (txtRezAciklama == null)
            {
                MessageBox.Show("Lütfen açıklama giriniz...");
            }

            else
            {
                tbl_rezervasyon rez = new tbl_rezervasyon();
                saat = cmbSaat.Text;
                var saha = db.tbl_saha.Where(y => y.SAHA_ADI == cmbSaha.Text).FirstOrDefault();
                rez.REZ_SAHA_ID = saha.SAHA_ID;
                rez.REZ_UCRET = saha.UCRET;
                rez.TARIH = tarih;
                rez.SAAT = saat;
                rez.REZ_SAHIP = txtRezSahip.Text;
                rez.ACIKLAMA = txtRezAciklama.Text;
                rez.REZ_DURUM = true;
                db.tbl_rezervasyon.Add(rez);
                db.SaveChanges();
                FillDataGrid();
            }
        }



    }
}



