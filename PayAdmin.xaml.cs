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
    /// Логика взаимодействия для PayAdmin.xaml
    /// </summary>
    public partial class PayAdmin : Page
    {
        private MedLabEntities context = new MedLabEntities();
        public PayAdmin()
        {
            InitializeComponent();

            LoadData();

            var order_cbx_data = context.PayedType.ToList();
            type_pay_cbx.ItemsSource = order_cbx_data;
            type_pay_cbx.DisplayMemberPath = "NameTypePay";
            type_pay_cbx.SelectedValuePath = "ID_TypePay";


            var result_cbx_data = context.ResultAnalyzies.ToList();
            result_cbx.ItemsSource = result_cbx_data;
            result_cbx.DisplayMemberPath = "Result";
            result_cbx.SelectedValuePath = "ID_Result";
        }


        private void LoadData()
        {
            try
            {
                var paymentResultData = context.Payed
                    .Join(
                        context.ResultAnalyzies,
                        order => order.Result_ID,
                        resultAnalyzy => resultAnalyzy.ID_Result,
                        (order, resultAnalyzy) => new { order, resultAnalyzy }
                    )
                    .Join(
                       context.PayedType,
                       joined => joined.order.TypePay_ID,
                       payedType => payedType.ID_TypePay,
                       (joined, payedType) => new
                       {
                           DatePay = joined.order.DatePay,
                           TypePay = payedType.NameTypePay,
                           Result = joined.resultAnalyzy.Result
                       }
                    )
                    .ToList() 
                     .Select(x => new
                     {
                         DateCreate = DateTime.Parse(x.DatePay).ToString("dd.MM.yyyy"),
                         TypePay = x.TypePay,
                         Result = x.Result
                     }
                    ).ToList();


                staffinfo.ItemsSource = paymentResultData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            Payed pay = new Payed();

    
            if (datapay_dpc.SelectedDate != null)
            {
                pay.DatePay = datapay_dpc.SelectedDate.Value.ToString("dd-MM-yyyy");
            }
            else
            {
                MessageBox.Show("Выберите дату оплаты!");
            }

            if (string.IsNullOrWhiteSpace(datapay_dpc.Text.Trim()) ||
            string.IsNullOrWhiteSpace(type_pay_cbx.Text.Trim()) ||
            string.IsNullOrWhiteSpace(result_cbx.Text.Trim()))
                    {
                MessageBox.Show("Данные не могут быть пустыми!");
                return;
            }


            if (type_pay_cbx.SelectedItem != null)
                pay.TypePay_ID = (int)type_pay_cbx.SelectedValue;

            if (type_pay_cbx.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип оплаты!");
                return;
            }

            if (result_cbx.SelectedItem != null)
                pay.Result_ID = (int)result_cbx.SelectedValue;

            if (result_cbx.SelectedItem == null)
            {
                MessageBox.Show("Выберите результат!");
                return;
            }


            using (var context = new MedLabEntities()) 
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Payed.Add(pay);
                        context.SaveChanges();

                        var statusIssued = context.StatusOrder
                                                .FirstOrDefault(s => s.NameStat == "Выдан клиенту");

                        if (statusIssued == null)
                        {
                            throw new Exception("Статус 'Выдан клиенту' не найден!");
                        }

                        var resultAnalyz = context.ResultAnalyzies
                                                .FirstOrDefault(r => r.ID_Result == pay.Result_ID);

                        if (resultAnalyz == null)
                        {
                            throw new Exception("Результат анализа не найден!");
                        }
                        var orderId = resultAnalyz.Order_ID;

                        var order = context.Orders.FirstOrDefault(o => o.ID_Order == orderId);

                        if (order == null)
                        {
                            throw new Exception("Заказ не найден!");
                        }
                        order.StatOrder_ID = statusIssued.ID_StatusOrder;
                        context.SaveChanges();

                        transaction.Commit();

                        LoadData();
                        MessageBox.Show("Данные об оплате успешно добавлены и статус заказа обновлен!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка:, {ex.Message}");
                    }
                }
            }


        }
    }
}
