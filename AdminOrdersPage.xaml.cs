using Spire.Doc.Documents;
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
    /// Логика взаимодействия для AdminOrdersPage.xaml
    /// </summary>
    public partial class AdminOrdersPage : Page
    {
        private MedLabEntities context = new MedLabEntities();
        public AdminOrdersPage()
        {
            InitializeComponent();
            LoadData();

            var analyzList = context.Analyzis.ToList();
            analyz_cbx.ItemsSource = analyzList;
            analyz_cbx.DisplayMemberPath = "NameAnalyz";
            analyz_cbx.SelectedValuePath = "ID_Analyz";

            var addressList = context.Addresses.ToList();
            address_cbx.ItemsSource = addressList;
            address_cbx.DisplayMemberPath = "NameAddress";
            address_cbx.SelectedValuePath = "ID_Address";

            var clientsList = context.Clients.ToList();
            client_cbx.ItemsSource = clientsList;
            client_cbx.DisplayMemberPath = "LastNameC";
            client_cbx.SelectedValuePath = "ID_Client";

            List<Workers> labsList = context.Workers
                                                   .Where(worker => worker.Role_ID == 2)
                                                   .ToList();

            laborant_cbx.ItemsSource = labsList;
            laborant_cbx.DisplayMemberPath = "LastNameW";
            laborant_cbx.SelectedValuePath = "ID_Worker";


            var statusList = context.StatusOrder.ToList();
            status_cbx.ItemsSource = statusList;
            status_cbx.DisplayMemberPath = "NameStat";
            status_cbx.SelectedValuePath = "ID_StatusOrder";


        }

        public class OrderInfo
        {
            public Orders Order { get; set; }
            public string StatusOrderName { get; set; }
            public string ClientFullName { get; set; }
            public string AddressOrder { get; set; }
            public string DateCreate { get; set; }

        }

        private void LoadData()
        {
            try
            {

                var data = context.Orders
                    .Join(context.StatusOrder,
                        order => order.StatOrder_ID,
                        status => status.ID_StatusOrder,
                        (order, status) => new { order, status })
                    .Join(context.Clients,
                        combined => combined.order.Client_ID,
                        client => client.ID_Client,
                        (combined, client) => new { combined.order, combined.status, client })
                      .Join(context.Addresses,
                        combined => combined.order.AddressoOrd_ID,
                        address => address.ID_Address,
                        (combined, address) => new { combined.order, combined.status, combined.client, address })
                    .Select(x => new
                    {
                        Order = x.order,
                        StatusOrderName = x.status.NameStat,
                        LastNameC = x.client.LastNameC,
                        FirstNameC = x.client.FirstNameC,
                        PatronymicC = x.client.PatronymicC,
                        AddressOrder = x.address.NameAddress,
                        DateCreate = x.order.DateCreate
                    })
                    .ToList()
                    .Select(x => new OrderInfo
                    {
                        Order = x.Order,
                        StatusOrderName = x.StatusOrderName,
                        ClientFullName = $"{x.LastNameC} {x.FirstNameC} {x.PatronymicC}",
                        AddressOrder = x.AddressOrder,
                        DateCreate = x.DateCreate
                    })
                    .ToList();

                analyzesinfo.ItemsSource = data;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных в датагрид: {ex.Message}");
            }
        }

        private void create_btn_Click(object sender, RoutedEventArgs e)
        {

            Orders orders = new Orders();

            if (analyz_cbx.SelectedItem != null)
            {
                orders.Analyz_ID = (int)analyz_cbx.SelectedValue;
            }

            else
            {
                MessageBox.Show("Выберите анализ!");
                return;
            }

            if (client_cbx.SelectedItem != null)
            {
                orders.Client_ID = (int)client_cbx.SelectedValue;
            }

            else
            {
                MessageBox.Show("Выберите пациента!");
                return;
            }

            if (status_cbx.SelectedItem != null)
            {
                orders.StatOrder_ID = (int)status_cbx.SelectedValue;
            }

            else
            {
                MessageBox.Show("Выберите статус!");
                return;
            }

            if (address_cbx.SelectedItem != null)
            {
                orders.AddressoOrd_ID = (int)address_cbx.SelectedValue;
            }

            else
            {
                MessageBox.Show("Выберите адрес!");
                return;
            }

            if (laborant_cbx.SelectedItem != null)
            {
                orders.Worker_ID = (int)laborant_cbx.SelectedValue;
            }

            else
            {
                MessageBox.Show("Выберите лаборанта, ответственного за анализ!");
                return;
            }

            orders.DateCreate = DateTime.Now.ToString("dd-MM-yyyy");

            try
            {
                context.Orders.Add(orders);
                context.SaveChanges();
                LoadData();

                MessageBox.Show("Данные успешно добавлены!");
            }
            catch (Exception ex) {
                MessageBox.Show("Ошибка: ", ex.Message);
            
            }
        }
    }
}
