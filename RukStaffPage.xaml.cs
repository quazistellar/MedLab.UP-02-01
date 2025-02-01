using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для RukStaffPage.xaml
    /// </summary>
    public partial class RukStaffPage : Page
    {
        private MedLabEntities context = new MedLabEntities();
        public RukStaffPage()
        {
            InitializeComponent();
            LoadData();

            var roles_cbx_data = context.RoleWorker.ToList();
            role_cbx.ItemsSource = roles_cbx_data;
            role_cbx.DisplayMemberPath = "NameRole";
            role_cbx.SelectedValuePath = "ID_Role";

        }

        private void LoadData()
        {
            try
            {
                var staffData = context.Workers
                        .Join(context.RoleWorker,
                            worker => worker.Role_ID,
                            role => role.ID_Role,
                            (worker, role) => new
                            {
                                FullName = worker.LastNameW + " " + worker.FirstNameW + (string.IsNullOrEmpty(worker.PatronymicW) ? "" : " " + worker.PatronymicW),
                                Email = worker.LoginWorker,
                                Password = worker.PasswordWorker,
                                RoleName = role.NameRole,
                                Smena = worker.Smeni
                            })
                        .ToList();
                staffinfo.ItemsSource = staffData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных: " + ex.Message);
            }
        }

        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Workers workers = new Workers();
                workers.LastNameW = fam_tbx.Text;
                workers.FirstNameW = name_tbx.Text;
                workers.PatronymicW = string.IsNullOrEmpty(ptrnm_tbx.Text) ? null : ptrnm_tbx.Text;

                if (role_cbx.SelectedItem != null)
                workers.Role_ID = (int)role_cbx.SelectedValue;

                workers.LoginWorker = email_tbx.Text;
                workers.PasswordWorker = pass_tbx.Text;
                workers.Smeni = one_tbx.Text + "/" + two_tbx.Text;
        

                if (name_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в имя!");
                    return;
                }

                if (name_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в имя сотрудника!");
                    return;
                }

                if (fam_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в фамилию сотрудника!");
                    return;
                }


                if (ptrnm_tbx.Text.Any(c => !char.IsLetter(c)))
                {
                    MessageBox.Show("Нельзя вводить другие символы в отчество сотрудника!");
                    return;
                }


                if (fam_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в фамилию!");
                    return;
                }

                if (ptrnm_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить цифры в отчество!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(fam_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(name_tbx.Text.Trim()) || (email_tbx.Text.Trim() == "" || pass_tbx.Text.Trim() == "" || one_tbx.Text.Trim() == "" || two_tbx.Text.Trim() == ""))
                {
                    MessageBox.Show("Данные не могут быть пустыми!");
                    return;
                }

                if (!one_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить другие символы в смены!");
                    return;
                }


                if (!two_tbx.Text.Any(char.IsDigit))
                {
                    MessageBox.Show("Нельзя вводить другие символы в смены!");
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

                if (!ReqForPass(pass_tbx.Text))
                {
                    MessageBox.Show("Пароль не соответствует требованиям.\n" +
                           "Он должен содержать не менее 8 символов и включать в себя:\n" +
                           "- хотя бы одну строчную букву\n" +
                           "- хотя бы одну заглавную букву\n" +
                           "- хотя бы одну цифру\n" +
                           "- хотя бы один специальный символ (!@#$%^&*())\n",
                           "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    pass_tbx.Focus();
                    return;
                }

                try
                {
                    context.Workers.Add(workers);
                    context.SaveChanges();
                    LoadData();
                    MessageBox.Show("Данные о сотруднике успешно добавлены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: ", ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            
        }

        public static bool ReqForPass(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()]).{8,}$";

            return Regex.IsMatch(password, pattern);
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