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

namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MedLabEntities context = new MedLabEntities();
        public string UserName { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MinHeight = 232;
            MinWidth = 400;
        }

        private void Autorize_btn_Click(object sender, RoutedEventArgs e)
        {
            var credentials = context.Workers.ToList();
        

            bool check = false;

            foreach (var data in credentials)
            {
                if (data.LoginWorker == LoginUser_tbx.Text && data.PasswordWorker == Password_tbx.Password && data.Role_ID == 1)
                {
                    var name = data.FirstNameW.ToString();
                    UserName = name;
                    check = true;
                    AdminWindow admin = new AdminWindow(name);
                    admin.Show();
                    Close();
                }

                if (data.LoginWorker == LoginUser_tbx.Text && data.PasswordWorker == Password_tbx.Password && data.Role_ID == 2)
                {
                  
                    var name = data.FirstNameW.ToString();
                    UserName = name;
                    check = true;
                    LabWindow lab = new LabWindow(name);
                    lab.Show();
                    Close();
                }

                if (data.LoginWorker == LoginUser_tbx.Text && data.PasswordWorker == Password_tbx.Password && data.Role_ID == 3)
                {
                    var name = data.FirstNameW.ToString();
                    UserName = name;
                    check = true;
                    RukovodWindow ruk = new RukovodWindow(name);
                    ruk.Show();
                    Close();
                }
            }

            if (check == false)
            {
                MessageBox.Show("Неверные данные: логин или пароль", Title="Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Cancel);
            }
        }
    }
}
