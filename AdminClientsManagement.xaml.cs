using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для AdminClientsManagement.xaml
    /// </summary>
    public partial class AdminClientsManagement : Page
    {
        private MedLabEntities context = new MedLabEntities();
        private List<dynamic> _fullClientData = new List<dynamic>(); 
        public AdminClientsManagement()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            _fullClientData.Clear(); 

            var staffData = context.Clients
                .Select(clients => new
                {
                    ID_Client = clients.ID_Client,
                    FirstNamec = clients.FirstNameC,
                    LastNameC = clients.LastNameC,
                    PatronymicC = clients.PatronymicC,
                    EmailC = clients.EmailClient,
                    NameCompany = clients.NameCompany,
                    Dolznost_ID = clients.Dolznost_ID,
                    Dolznost = clients.Dolznosti.NameDolznost,
                    RekvisitOrganisation = clients.RekvisitOrganisation,
                    PolicyC = clients.PolicyC,
                    Date_Born = clients.Date_Born,
                    TypeClient = clients.TypeClient.NameTypeClient,
                    TypeClient_ID = clients.TypeClient_ID,
                })
                .ToList();
            _fullClientData = staffData.ToList<dynamic>();

            var displayData = staffData
            .Select(client => new
            {
                ID_Client = client.ID_Client,
                FullName = $"{client.LastNameC} {client.FirstNamec}{(string.IsNullOrEmpty(client.PatronymicC) ? "" : " " + client.PatronymicC)}",
                Email = client.EmailC,
            })
            .ToList();

            staffinfo.ItemsSource = displayData;
        }


        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {

            if (staffinfo.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
                return;
            }

            var selectedItem = staffinfo.SelectedItem as dynamic;
            string clientEmail = selectedItem.Email; 

            try
            {
                var clientToDelete = context.Clients.FirstOrDefault(c => c.EmailClient == clientEmail);

                if (clientToDelete != null)
                {
                    context.Clients.Remove(clientToDelete);
                    context.SaveChanges();
                    LoadData(); 
                }
                else
                {
                    MessageBox.Show("Клиент с указанным email не найден в базе данных!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления (скорее всего данные связаны с другой таблицей)! " + ex.Message);
            }
        }

        private void create_ur_btn_Click(object sender, RoutedEventArgs e)
        {
           CreateUr create = new CreateUr();
           create.ShowDialog();

        }

        private void edit_ur_btn_Click(object sender, RoutedEventArgs e)
        {
            if (staffinfo.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования!");
                return;
            }


            var selectedDisplayItem = staffinfo.SelectedItem as dynamic;
            if (selectedDisplayItem == null)
                return;

            int selectedClientId = selectedDisplayItem.ID_Client;

            var selectedClient = _fullClientData.FirstOrDefault(item => item.ID_Client == selectedClientId);

            if (selectedClient == null)
            {
                MessageBox.Show("Не удалось найти данные выбранного клиента!");
                return;
            }


            var typeClient = context.TypeClient.FirstOrDefault(tc => tc.NameTypeClient == "Юридическое лицо");
            if (typeClient == null)
            {
                MessageBox.Show("Тип клиента 'Юридическое лицо' не найден в базе данных.");
                return;
            }

            if (selectedClient.TypeClient_ID != typeClient.ID_TypeClient)
            {
                MessageBox.Show("Для редактирования нужно выбрать только клиента с типом 'Юридическое лицо'!");
                return;
            }


            EditUr edit = new EditUr(
                selectedClient.ID_Client,
                selectedClient.FirstNamec,
                selectedClient.LastNameC,
                selectedClient.PatronymicC,
                selectedClient.EmailC,
                selectedClient.NameCompany,
                selectedClient.Dolznost_ID,
                selectedClient.Dolznost,
                selectedClient.RekvisitOrganisation

            );

            edit.ShowDialog();
        }

        private void create_fiz_btn_Click(object sender, RoutedEventArgs e)
        {

            CreateFiz createf = new CreateFiz();
            createf.ShowDialog();
        }

        private void edit_fiz_btn_Click(object sender, RoutedEventArgs e)
        {
            if (staffinfo.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования!");
                return;
            }


            var selectedDisplayItem = staffinfo.SelectedItem as dynamic;
            if (selectedDisplayItem == null)
                return;

            int selectedClientId = selectedDisplayItem.ID_Client;

            var selectedClient = _fullClientData.FirstOrDefault(item => item.ID_Client == selectedClientId);


            var typeClient = context.TypeClient.FirstOrDefault(tc => tc.NameTypeClient == "Физическое лицо");
            if (typeClient == null)
            {
                MessageBox.Show("Тип клиента 'Физическое лицо' не найден в базе данных.");
                return;
            }

            if (selectedClient.TypeClient_ID != typeClient.ID_TypeClient)
            {
                MessageBox.Show("Для редактирования нужно выбрать только клиента с типом 'Физическое лицо'!");
                return;
            }


            EditFiz edit = new EditFiz(
                selectedClient.ID_Client,
                selectedClient.FirstNamec,
                selectedClient.LastNameC,
                selectedClient.PatronymicC,
                selectedClient.EmailC,
                selectedClient.Date_Born,
                selectedClient.PolicyC
            );

            edit.ShowDialog();
        }
    }
}