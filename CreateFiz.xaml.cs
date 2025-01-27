using Spire.Doc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для CreateFiz.xaml
    /// </summary>
    public partial class CreateFiz : Window
    {
        private MedLabEntities context = new MedLabEntities();
        public CreateFiz()
        {
            InitializeComponent();
            MinHeight = 800;
            MinWidth = 1500;

            DateTime today = DateTime.Now.Date;
            date.DisplayDateEnd = today;

        }

        public static bool ReqForEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);


        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(this);

            if (window != null)
            {
                window.Close();
            }
        }

        private void create_client_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clients clients = new Clients();
                clients.LastNameC = fam_tbx.Text;
                clients.FirstNameC = name_tbx.Text;
                clients.PatronymicC = string.IsNullOrEmpty(otch_tbx.Text) ? null : otch_tbx.Text;
                clients.EmailClient = email_tbx.Text;
                var typeClient = context.TypeClient.FirstOrDefault(tc => tc.NameTypeClient == "Физическое лицо");
                try
                {
                    clients.TypeClient_ID = typeClient.ID_TypeClient;
                }
                catch (Exception ex) { MessageBox.Show("В базе данных не найден тип клиента для физического лица!"); }


                if (date.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату рождения клиента!");
                    return;
                }

                if (date.SelectedDate != null)
                {
                    clients.Date_Born = date.SelectedDate.Value.ToString("dd-MM-yyyy");
                }
    

                if (polis_tbx.Text.Length != 16)
                {
                    MessageBox.Show("Полис должен быть равен 16 цифрам!");
                    return;
                }
                else if (polis_tbx.Text.Length == 16)
                {
                    clients.PolicyC = polis_tbx.Text;
                }
             
               
                if (name_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в имя клиента!");
                    return;
                }

                if (fam_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в фамилию клиента!");
                    return;
                }


                if (otch_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в отчество клиента!");
                    return;
                }

                if (polis_tbx.Text.Any(c => !char.IsDigit(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в полис клиента!");
                    return;
                }


                if (fam_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в фамилию клиента!");
                    return;
                }

                if (otch_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в отчество клиента!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(fam_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(name_tbx.Text.Trim()) || (email_tbx.Text.Trim() == "" || polis_tbx.Text.Trim() == "" || date.Text.Trim() == ""))
                {
                    MessageBox.Show("Данные не могут быть пустыми!");
                    return;
                }

                if (!ReqForEmail(email_tbx.Text))
                {
                    MessageBox.Show("Email не имеет корректный формат.\n" +
                          "Он должен содержать символ '@' и точку '.'\n" +
                          "Пример: example@domain.com",
                          "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    email_tbx.Focus();
                    return;
                }

                try
                {
                    context.Clients.Add(clients);
                    context.SaveChanges();
                    MessageBox.Show("Данные о клиенте успешно добавлены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка:, {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }


    }
}
