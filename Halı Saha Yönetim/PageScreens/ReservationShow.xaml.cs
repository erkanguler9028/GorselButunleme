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

namespace Halı_Saha_Yönetim
{
    /// <summary>
    /// Interaction logic for ReservationShow.xaml
    /// </summary>
    public partial class ReservationShow : Page
    {

        db_sahaEntities db = new db_sahaEntities();
        public ReservationShow()
        {
            InitializeComponent();
            dgOdenecek.ItemsSource = db.tbl_rezervasyon.ToList();
            dgOdeme.ItemsSource = db.tbl_odeme.ToList();
        }

        private void Odeme_Click(object sender, RoutedEventArgs e)
        {

            int secim = Convert.ToInt32(txtRezId.Text);
           
            var rez = db.tbl_rezervasyon.Where(w => w.REZ_ID == secim).FirstOrDefault();

            if(rez.REZ_DURUM!=false)
            {            
            rez.REZ_DURUM = false;
            
            //dgRezervasyon.ItemsSource = db.tbl_rezervasyon.ToList();
            //var rez = db.tbl_rezervasyon.Where(y => y.REZ_ID == secim).FirstOrDefault();
            //var UCRET = db.tbl_saha.Where(y => y.SAHA_ID == tbl_saha.SAHA_ID).FirstOrDefault();
            
            tbl_odeme odeme = new tbl_odeme();

            odeme.ODEME_UCRET = rez.REZ_UCRET;
            odeme.ODEME_REZ_ID = rez.REZ_ID;
            odeme.ODEME_SAHA_ID = rez.REZ_SAHA_ID;
            odeme.DURUM = true;

            db.tbl_odeme.Add(odeme);
           // db.tbl_rezervasyon.Remove(rez);
            db.SaveChanges();
            dgOdeme.ItemsSource = db.tbl_odeme.ToList();
            dgOdenecek.ItemsSource = db.tbl_rezervasyon.ToList();
            }

            else
            {
                MessageBox.Show("Bu rezervasyonun ödemesi yapılmıştır!");
            }
        }
    }
}
