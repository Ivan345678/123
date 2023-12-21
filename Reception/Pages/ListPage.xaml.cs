using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Collections.ObjectModel;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace Reception.Pages
{
   
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public static AuchanEntities entObj = new AuchanEntities();
        public ListPage()
        {
            InitializeComponent();

            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                int bar = Convert.ToInt32(SearchTxb.Text);
                var conditionObj = DbConnect.entObj.Receipt.FirstOrDefault(x => x.Condition == MainPage.Condition);
                if (conditionObj == null)
                {
                    MessageBox.Show ("Неправильный чек",
                                "Уведомления",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                }
                else
                {
                    var productObj = DbConnect.entObj.Products.FirstOrDefault(x => x.Barcode == bar);
                    if (productObj == null)
                    {
                        MessageBox.Show("Неправильный штрих код",
                                "Уведомления",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                    }
                    else
                    {
                        ListProducts newListProducts = new ListProducts
                        {
                            Name = productObj.Name,
                            Price = productObj.Price,
                            Receipt = conditionObj.Id
                        };
                        DbConnect.entObj.ListProducts.Add(newListProducts);
                        DbConnect.entObj.SaveChanges();

                        var products = from p in entObj.ListProducts
                                       where p.Receipt == conditionObj.Id
                                       select p;

                        // Заполнение DataGrid
                        ProductGrid.ItemsSource = products.ToList();
                    }
                    
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


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new MainPage());
        }

        private void Ex_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var conditionObj = DbConnect.entObj.Receipt.FirstOrDefault(x => x.Condition == MainPage.Condition);
                string Condition = "Завершон " + LoginPage.Log;
                if (conditionObj == null)
                {
                    MessageBox.Show("Неправильный чек",
                                "Уведомления",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                }
                else
                {
                    conditionObj.Condition = Condition;
                    DbConnect.entObj.SaveChanges();

                    MessageBox.Show("Ваш чек: " + conditionObj.Id,
                               "Уведомления",
                               MessageBoxButton.OK,
                               MessageBoxImage.Warning);

                    FrameApp.frmObj.Navigate(new MainPage());
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
