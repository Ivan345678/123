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
using Reception.ServiceData;

namespace Reception.Pages
{
   
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static string Condition;
        public MainPage()
        {
            
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new LoginPage());
        }

        private void ListProductsBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Condition = "В процессе " + LoginPage.Log;
                var productObj = DbConnect.entObj.Receipt.FirstOrDefault();
                if (productObj.Condition == Condition)
                {
                    FrameApp.frmObj.Navigate(new ListPage());
                }
                else
                {
                    Receipt newReceipt = new Receipt
                    {
                        Condition = Condition
                    };
                    DbConnect.entObj.Receipt.Add(newReceipt);
                    DbConnect.entObj.SaveChanges();

                    FrameApp.frmObj.Navigate(new ListPage());                   
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
