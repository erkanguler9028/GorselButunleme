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

namespace Halı_Saha_Yönetim.PageScreens
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        db_sahaEntities db = new db_sahaEntities();
        public Settings()
        {
            InitializeComponent();
            dgsaha.ItemsSource = db.tbl_saha.ToList();
        }
   
        private void Button_Ekle(object sender, RoutedEventArgs e)
        {
            tbl_saha yeniSaha = new tbl_saha();
            
            yeniSaha.SAHA_ADI = txtad.Text;
            yeniSaha.UCRET = Convert.ToInt32(txtfiyat.Text);
            yeniSaha.KAPASITE = Convert.ToInt32(txtkapasite.Text);
            
            db.tbl_saha.Add(yeniSaha);
            db.SaveChanges();
            dgsaha.ItemsSource = db.tbl_saha.ToList(); //Kayıt ekledikten sonra yenilemek için                                                          
        }
      

        private void Button_Sil(object sender, RoutedEventArgs e)
        {
            int silinecek = Convert.ToInt32(txtSecim.Text);
            var silineceksaha = db.tbl_saha.Where(w => w.SAHA_ID == silinecek).FirstOrDefault();
            db.tbl_saha.Remove(silineceksaha);
            db.SaveChanges();
            dgsaha.ItemsSource = db.tbl_saha.ToList();
        }
    }
}
