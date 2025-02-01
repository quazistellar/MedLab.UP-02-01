using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для StatusAnalyzPage.xaml
    /// </summary>
    public partial class StatusAnalyzPage : Page
    {
        private MedLabEntities context = new MedLabEntities();
        private OrderInfo selectedOrderInfo;

        public class OrderInfo
        {
            public Orders Order { get; set; }
            public string AnalyzName { get; set; }
            public string ClientFullName { get; set; }
            public string StatusName { get; set; }
        }

        public StatusAnalyzPage()
        {
            InitializeComponent();
            LoadData();

            var statusList = context.StatusOrder.ToList();
            change_status_cbx.ItemsSource = statusList;
            change_status_cbx.DisplayMemberPath = "NameStat";
            change_status_cbx.SelectedValuePath = "ID_StatusOrder";
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
                    .Join(context.StatusOrder,
                        combined => combined.order.StatOrder_ID,
                        status => status.ID_StatusOrder,
                        (combined, status) => new
                        {
                            Order = combined.order,
                            AnalyzName = combined.analyz.NameAnalyz,
                            LastNameC = combined.client.LastNameC,
                            FirstNameC = combined.client.FirstNameC,
                            PatronymicC = combined.client.PatronymicC,
                            StatusName = status.NameStat
                        })
                    .Where(x => x.StatusName == "Готов" || x.StatusName == "Выполняется") 
                    .ToList()
                    .Select(x => new OrderInfo
                    {
                        Order = x.Order,
                        AnalyzName = x.AnalyzName,
                        ClientFullName = $"{x.LastNameC} {x.FirstNameC} {x.PatronymicC}",
                        StatusName = x.StatusName
        })
        .ToList();

                    analyzes.ItemsSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrderInfo == null)
            {
                MessageBox.Show("Выберите запись для изменения статуса", "Отсутствие выбранной записи", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (change_status_cbx.SelectedItem is StatusOrder selectedStatus)
            {
                try
                {
                    using (var context = new MedLabEntities())
                    {
                        var orderToUpdate = context.Orders.Find(selectedOrderInfo.Order.ID_Order);
                        if (orderToUpdate != null)
                        {
                            orderToUpdate.StatOrder_ID = selectedStatus.ID_StatusOrder;
                            context.SaveChanges();
                            LoadData();
                            MessageBox.Show("Статус заказа успешно изменен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления статуса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите статус из выпадающего списка", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void analyzes_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (analyzes.SelectedItem is OrderInfo selected)
            {
                selectedOrderInfo = selected;
                change_status_cbx.SelectedValue = selectedOrderInfo.Order.StatOrder_ID;
            }


        }

        private void change_status_cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}