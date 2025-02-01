using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для EditFiz.xaml
    /// </summary>
    public partial class EditFiz : Window
    {
        private MedLabEntities context = new MedLabEntities();
        private int clientId;
        public EditFiz(int idclient, string firstName, string lastName, string patronymic, string email, string dateborn, string policyc)
        {
            InitializeComponent();

            clientId = idclient;
            email_tbx.Text = email;
            fam_tbx.Text = lastName;
            name_tbx.Text = firstName;
            otch_tbx.Text = patronymic;
            polis_tbx.Text = policyc;

            if (!string.IsNullOrEmpty(dateborn))
            {
                if (DateTime.TryParseExact(dateborn, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    date.SelectedDate = parsedDate;
                }
                else
                {
                    MessageBox.Show($"Не удалось распознать дату: {dateborn}");
                }
            }

        }

        private void create_client_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = context.Clients.Find(clientId);

                if (client == null)
                {
                    MessageBox.Show("Клиент не найден!");
                    return;
                }

                if (!date.SelectedDate.HasValue)
                {
                    MessageBox.Show("Выберите дату рождения клиента!");
                    return;
                }

                client.Date_Born = date.SelectedDate.Value.ToString("dd-MM-yyyy");
                client.LastNameC = fam_tbx.Text;
                client.FirstNameC = name_tbx.Text;
                client.PatronymicC = string.IsNullOrEmpty(otch_tbx.Text) ? null : otch_tbx.Text;
                client.PolicyC = polis_tbx.Text;
                client.EmailClient = email_tbx.Text;

                if (string.IsNullOrWhiteSpace(fam_tbx.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(name_tbx.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(email_tbx.Text.Trim()) ||
                    string.IsNullOrWhiteSpace(polis_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(date.Text.Trim()))

                {
                    MessageBox.Show("Данные не могут быть пустыми!");
                    return;
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

                if (!string.IsNullOrWhiteSpace(otch_tbx.Text) && otch_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в отчество клиента!");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(polis_tbx.Text))
                {
                    if (polis_tbx.Text.Any(c => !char.IsDigit(c)))
                    {
                        MessageBox.Show("В полисе можно вводить только цифры!");
                        return;
                    }

                    if (polis_tbx.Text.Length != 16)
                    {
                        MessageBox.Show("Длина полиса должна быть равна 16 символам.");
                        return;
                    }
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

                context.SaveChanges();
                MessageBox.Show("Данные клиента успешно обновлены!");
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

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
    }
}