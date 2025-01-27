using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для LabWindow.xaml
    /// </summary>
    public partial class LabWindow : Window
    {
        public LabWindow(string name)
        {
            InitializeComponent();
            page.Content = new LabMainPage(name);
            Name = name;

            MinHeight = 800;
            MinWidth = 1500;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", Title="Подтверждение выхода", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            } 
        }

        private void show_create_analyz_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new StatusAnalyzPage();
        }

        private void show_main_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new LabMainPage(Name);
        }

        private void show_analyz_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new AnalyzPage();
        }

        private void show_results_Click(object sender, RoutedEventArgs e)
        {
            page.Content = new ResultLab();
        }
    }
}
