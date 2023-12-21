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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    
    public partial class LoginPage : Page
    {
        public static string Log;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userObj = DbConnect.entObj.User.FirstOrDefault(x => x.Login == TxbLogin.Text && x.Password == PsbPassword.Password );
                if (userObj == null)
                {
                    MessageBox.Show("Такой пользователь не найден",
                                  "Уведомления",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
                else
                {
                    Log = TxbLogin.Text;
                    FrameApp.frmObj.Navigate(new MainPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Критический сбой в работе приложения: " + ex.Message.ToString(),
                                "Уведомления",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

            }
        }
    }
}
