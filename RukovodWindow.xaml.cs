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

namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для RukovodWindow.xaml
    /// </summary>
    public partial class RukovodWindow : Window
    {
        public RukovodWindow(string name)
        {
            InitializeComponent();
            page.Content = new RukMainPage(name);
            Name = name;

            MinHeight = 800;
            MinWidth = 1500;
        }

        private void show_otcheti_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new RukOtchetiPage();
        }

        private void show_analyzis_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new RukAnalyzPage();
        }

        private void show_main_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new RukMainPage(Name);
        }

        private void show_staff_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new RukStaffPage();
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
    }
}