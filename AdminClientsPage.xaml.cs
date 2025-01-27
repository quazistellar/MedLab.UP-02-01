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
    /// Логика взаимодействия для AdminClientsPage.xaml
    /// </summary>
    public partial class AdminClientsPage : Page
    {
        private MedLabEntities context = new MedLabEntities();
        private List<OrderInfo> _allOrders;
        public AdminClientsPage()
        {
            InitializeComponent();
            LoadData();

            var statusList = context.TypeAnalyzies.ToList();
            filter_cbx.ItemsSource = statusList;
            filter_cbx.DisplayMemberPath = "NameAnalyz";
            filter_cbx.SelectedValuePath = "ID_TypeAnalyzies";

        }

        public class OrderInfo
        {
            public Orders Order { get; set; }
            public string AnalyzName { get; set; }
            public string ClientFullName { get; set; }
            public string EmailClient { get; set; }
        }

        private void filter_cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filter_cbx.SelectedItem is TypeAnalyzies selectedTypeAnalyz)
            {
                if (_allOrders == null)
                {
                    return;
                }
                analyzes.ItemsSource = _allOrders
                    .Where(orderInfo => context.Analyzis
                    .FirstOrDefault(analyz => analyz.ID_Analyz == orderInfo.Order.Analyz_ID)
                    .TypeAnalyz_ID == selectedTypeAnalyz.ID_TypeAnalyzies)
                    .ToList();
            }
            else
            {
                analyzes.ItemsSource = _allOrders; 
            }
        }

        private void LoadData()
        {
            try
            {
                using (var context = new MedLabEntities())
                {
                    var data = context.Orders
                        .Join(context.Analyzis,
                            order => order.Analyz_ID,
                            analyz => analyz.ID_Analyz,
                            (order, analyz) => new { order, analyz })
                        .Join(context.Clients,
                            combined => combined.order.Client_ID,
                            client => client.ID_Client,
                            (combined, client) => new { combined.order, combined.analyz, client })
                        .Select(x => new
                        {
                            Order = x.order,
                            AnalyzName = x.analyz.NameAnalyz,
                            LastNameC = x.client.LastNameC,
                            FirstNameC = x.client.FirstNameC,
                            PatronymicC = x.client.PatronymicC,
                            EmailClient = x.client.EmailClient
                        })
                        .ToList()
                        .Select(x => new OrderInfo
                        {
                            Order = x.Order,
                            AnalyzName = x.AnalyzName,
                            ClientFullName = $"{x.LastNameC} {x.FirstNameC} {x.PatronymicC}",
                            EmailClient = x.EmailClient
                        })
                        .ToList();


                    _allOrders = data; 
                    analyzes.ItemsSource = _allOrders; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
