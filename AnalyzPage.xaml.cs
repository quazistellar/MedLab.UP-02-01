using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AnalyzPage.xaml
    /// </summary>
    public partial class AnalyzPage : Page
    {
        private MedLabEntities context = new MedLabEntities();

        public AnalyzPage()
        {
            InitializeComponent();

            LoadData();

            try
            {
                var obraz_cmx = context.Obrazchi.ToList();
                obraz_cbx.ItemsSource = obraz_cmx;
                obraz_cbx.DisplayMemberPath = "NameObrazech";
                obraz_cbx.SelectedValuePath = "ID_Obrazech";

                var types_cbx = context.TypeAnalyzies.ToList();
                type_cbx.ItemsSource = types_cbx;
                type_cbx.DisplayMemberPath = "NameAnalyz";
                type_cbx.SelectedValuePath = "ID_TypeAnalyzies";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

        private void LoadData()
        {
            var analyzdata = from Analyzis in context.Analyzis
                             join typeAnalyz in context.TypeAnalyzies on Analyzis.TypeAnalyz_ID equals typeAnalyz.ID_TypeAnalyzies
                             join Obrazech in context.Obrazchi on Analyzis.Obrazech_ID equals Obrazech.ID_Obrazech
                             select new
                             {
                                 NameAnalyz = Analyzis.NameAnalyz,
                                 CostAnalyz = Analyzis.CostAnalyz,
                                 Ogranizhenia = Analyzis.Ogranizhenia,
                                 TimeWork = Analyzis.TimeWork,
                                 TypeName = typeAnalyz.NameAnalyz,
                                 ObrazechName = Obrazech.NameObrazech
                             };
            analyzesinfo.ItemsSource = analyzdata.ToList();
        }

        private void create_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Analyzis analyzis = new Analyzis();
                analyzis.NameAnalyz = name_tbx.Text;

                if (string.IsNullOrWhiteSpace(name_tbx.Text.Trim()) || string.IsNullOrWhiteSpace(cost_tbx.Text.Trim()) || (time_tbx.Text.Trim() == "" || ogranich_tbx.Text.Trim() == ""))
                {
                    MessageBox.Show("Данные не могут быть пустыми");
                    return;
                }

                if (string.IsNullOrWhiteSpace(cost_tbx.Text) || !int.TryParse(cost_tbx.Text, out int cost))
                {
                    MessageBox.Show("Введите корректную стоимость");
                    return;
                }

                if (cost_tbx.Text.Contains("-"))
                {
                    MessageBox.Show("Цена не может быть отрицательной!");
                    return;
                }
                else if (int.TryParse(cost_tbx.Text, out cost)) 
                {
                    if (cost < 0)
                    {
                        MessageBox.Show("Цена не может быть отрицательной!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный ввод цены!");
                    return;
                }

                analyzis.CostAnalyz = cost;
                analyzis.Ogranizhenia = ogranich_tbx.Text;

                if (string.IsNullOrWhiteSpace(time_tbx.Text) || !int.TryParse(time_tbx.Text, out int time))
                {
                    MessageBox.Show("Введите корректное время");
                    return;
                }
                analyzis.TimeWork = time;


                if (obraz_cbx.SelectedItem != null)
                {
                    analyzis.Obrazech_ID = (int)obraz_cbx.SelectedValue;
                }

                else
                {
                    MessageBox.Show("Выберите образец!");
                    return;
                }


                if (type_cbx.SelectedItem != null)
                {
                    analyzis.TypeAnalyz_ID = (int)type_cbx.SelectedValue;
                }

                else
                {
                    MessageBox.Show("Выберите тип!");
                    return;
                }


                context.Analyzis.Add(analyzis);
                context.SaveChanges();
                LoadData(); 
                MessageBox.Show("Данные успешно добавлены!");
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Ошибка формата ввода: {ex.Message}");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Свойство: {validationError.PropertyName}, Ошибка: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
            }
        }
    
    }
}
