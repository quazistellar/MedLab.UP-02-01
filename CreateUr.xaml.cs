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
    /// Логика взаимодействия для CreateUr.xaml
    /// </summary>
    public partial class CreateUr : Window
    {
        private MedLabEntities context = new MedLabEntities();
        public CreateUr()
        {
            InitializeComponent();

            MinHeight = 800;
            MinWidth = 1500;

            var roles_cbx_data = context.Dolznosti.ToList();
            dolz_cbx.ItemsSource = roles_cbx_data;
            dolz_cbx.DisplayMemberPath = "NameDolznost";
            dolz_cbx.SelectedValuePath = "ID_Dolznost";
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

                if (dolz_cbx.SelectedItem != null)
                    clients.Dolznost_ID = (int)dolz_cbx.SelectedValue;

                clients.EmailClient = email_tbx.Text;
                clients.RekvisitOrganisation = rekviz_tbx.Text;
                clients.NameCompany = company_tbx.Text;

                var typeClient = context.TypeClient.FirstOrDefault(tc => tc.NameTypeClient == "Юридическое лицо");
                try
                {
                    clients.TypeClient_ID = typeClient.ID_TypeClient;
                }
                catch (Exception ex){ MessageBox.Show("В базе данных не найден тип клиента для юридического лица!"); }
 
                if (name_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в имя клиента!");
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


                if (otch_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в отчество клиента!");
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

                if (string.IsNullOrWhiteSpace(fam_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(name_tbx.Text.Trim()) || (email_tbx.Text.Trim() == "" || company_tbx.Text.Trim() == "" || email_tbx.Text.Trim() == "" || rekviz_tbx.Text.Trim() == ""))
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

                if (dolz_cbx.SelectedItem == null)
                {
                    MessageBox.Show("Выберите должность клиента!");
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

        public static bool ReqForEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);


        }
    }
}
