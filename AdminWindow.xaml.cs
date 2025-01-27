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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(string name)
        {
            InitializeComponent();
            page.Content = new AdminMainPage(name);
            Name = name;
            MinHeight = 800;
            MinWidth = 1500;
        }

        private void show_clients_Click(object sender, RoutedEventArgs e)
        {
           page.Content = new AdminClientsPage();
        }

        private void show_create_clients_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new AdminClientsManagement();
        }

        private void show_main_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new AdminMainPage(Name);
        }

        private void show_orders_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new AdminOrdersPage();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", Title = "Подтверждение выхода", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void show_pay_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new PayAdmin();
        }
    }
}
