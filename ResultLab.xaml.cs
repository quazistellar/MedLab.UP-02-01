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
    /// Логика взаимодействия для ResultLab.xaml
    /// </summary>
    public partial class ResultLab : Page
    {
        private MedLabEntities context = new MedLabEntities();
        public ResultLab()
        {
            InitializeComponent();

            LoadData();
            var order_cbx_data = context.Orders.ToList();
            order_cbx.ItemsSource = order_cbx_data;
            order_cbx.DisplayMemberPath = "DateCreate";
            order_cbx.SelectedValuePath = "ID_Order";

        }


        private void LoadData()
        {
            try
            {
                var resultAnalyziesData = context.ResultAnalyzies
                    .ToList() 
                    .Select(x => new
                    {
                        Result = x.Result,
                        DateStart = DateTime.Parse(x.DateStart).ToString("dd.MM.yyyy"),
                        DateEnd = DateTime.Parse(x.DateEnd).ToString("dd.MM.yyyy")
                    })
                    .ToList();

                staffinfo.ItemsSource = resultAnalyziesData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            ResultAnalyzies resultAnalyzy = new ResultAnalyzies();

            if (datastart_dpc.SelectedDate != null)
            {
                resultAnalyzy.DateStart = datastart_dpc.SelectedDate.Value.ToString("dd-MM-yyyy");
            }
            else
            {
                MessageBox.Show("Выберите дату начала!");
                return;
            }
            if (dataend_dpc.SelectedDate != null)
            {
                resultAnalyzy.DateEnd = dataend_dpc.SelectedDate.Value.ToString("dd-MM-yyyy");
            }
            else
            {
                MessageBox.Show("Выберите дату окончания!");
                return;
            }

            if (string.IsNullOrWhiteSpace(result_tbx.Text.Trim()) ||
                string.IsNullOrWhiteSpace(datastart_dpc.Text.Trim()) ||
                string.IsNullOrWhiteSpace(dataend_dpc.Text.Trim()))
            {
                MessageBox.Show("Данные не могут быть пустыми!");
                return;
            }


            if (order_cbx.SelectedItem != null)
                resultAnalyzy.Order_ID = (int)order_cbx.SelectedValue;

            if (order_cbx.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ!");
                return;
            }
            resultAnalyzy.Result = result_tbx.Text;

            try
            {
                context.ResultAnalyzies.Add(resultAnalyzy);
                context.SaveChanges();
                LoadData();
                MessageBox.Show("Данные об анализе успешно добавлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка:, {ex.Message}");
            }
        }
    }
}
