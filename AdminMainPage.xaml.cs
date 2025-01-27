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
using System.Xml.Linq;

namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        private MedLabEntities context = new MedLabEntities();
        public AdminMainPage(string name)
        {
            InitializeComponent();

            var ordersData = from order in context.Orders
                             join client in context.Clients on order.Client_ID equals client.ID_Client
                             join status in context.StatusOrder on order.StatOrder_ID equals status.ID_StatusOrder
                             select new
                             {
                                 FullName = client.LastNameC + " " + client.FirstNameC + " " + client.PatronymicC,
                                 Status = status.NameStat
                             };

            zakaz_fio.ItemsSource = ordersData.ToList();

            var analyzData = from result in context.ResultAnalyzies
                             join order in context.Orders on result.Order_ID equals order.ID_Order
                             join analyz in context.Analyzis on order.Analyz_ID equals analyz.ID_Analyz
                             select new
                             {
                                 AnalyzName = analyz.NameAnalyz,
                                 DateEnd = result.DateEnd
                             };

            analyz_dataend.ItemsSource = analyzData.ToList();
            hello_name.Text = name + "!";
        }
    }
}
