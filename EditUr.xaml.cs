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
    /// Логика взаимодействия для EditUr.xaml
    /// </summary>
    public partial class EditUr : Window
    {
        private MedLabEntities context = new MedLabEntities();
        private int clientId;
        public EditUr(int idclient, string firstName, string lastName, string patronymic, string email, string nameCompany, int? dolznostId, string dolznostName, string rekvisit)
        {
            InitializeComponent();

            MinHeight = 800;
            MinWidth = 1500;

            clientId = idclient;
            company_tbx.Text = nameCompany;
            email_tbx.Text = email;
            fam_tbx.Text = lastName;
            name_tbx.Text = firstName;
            otch_tbx.Text = patronymic;
            rekviz_tbx.Text = rekvisit;


            var roles_cbx_data = context.Dolznosti.ToList();
            dolz_cbx.ItemsSource = roles_cbx_data;
            dolz_cbx.SelectedValue = dolznostId;
            dolz_cbx.DisplayMemberPath = "NameDolznost";
            dolz_cbx.SelectedValuePath = "ID_Dolznost";
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


                client.LastNameC = fam_tbx.Text;
                client.FirstNameC = name_tbx.Text;
                client.PatronymicC = string.IsNullOrEmpty(otch_tbx.Text) ? null : otch_tbx.Text;

                if (dolz_cbx.SelectedItem != null)
                {
                    client.Dolznost_ID = (int)dolz_cbx.SelectedValue;
                }

                client.EmailClient = email_tbx.Text;
                client.RekvisitOrganisation = rekviz_tbx.Text;
                client.NameCompany = company_tbx.Text;


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

                if (otch_tbx.Text != null && otch_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в отчество клиента!");
                    return;
                }

                if (fam_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в фамилию клиента!");
                    return;
                }

                if (otch_tbx.Text != null && otch_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в отчество клиента!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(fam_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(name_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(email_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(company_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(rekviz_tbx.Text.Trim()))
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

                context.SaveChanges();
                MessageBox.Show("Данные клиента успешно обновлены!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(this);

            if (window != null)
            {
                window.Close();
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