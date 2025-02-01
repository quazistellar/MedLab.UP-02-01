using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для EmailDialog.xaml
    /// </summary>
    public partial class EmailDialog : Window
    {
        public string AttachmentPath { get; set; }

        public EmailDialog()
        {
            InitializeComponent();

            MinHeight = 250;
            MinWidth = 300;

            
        }

        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            string fromEmail = txtFromEmail.Text;
            string password = txtPassword.Text;
            string toEmail = toemail.Text;

            if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(toEmail))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try { 
                     SendEmailWithAttachment(AttachmentPath, fromEmail, password, toEmail);
                  
                }

                catch (Exception ex) {
                    MessageBox.Show("Возникла ошибка!");
                
                }
                
            }
        }

        private void SendEmailWithAttachment(string attachmentPath, string fromEmail, string password, string toEmail)
        {
            try
            {
                MailMessage mail = new MailMessage(fromEmail, toEmail);
                mail.Subject = "Результаты анализа";
                mail.Body = "Здравствуйте! \n\nУведомляем Вас о готовности анализа! В приложении вы найдете файл с результатом.\n\nС уважением,\n ООО 'МедЛаб'";
                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(fromEmail, password);

               
                smtp.Send(mail);
                MessageBox.Show("Отчет успешно отправлен на почту!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке письма: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
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
    }
}