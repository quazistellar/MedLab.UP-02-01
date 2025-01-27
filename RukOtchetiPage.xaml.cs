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
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Spire.Pdf.Tables;
using Spire.Doc;
using System.Data.Entity;
using System.IO;
using System.Threading;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using SkiaSharp;

namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для RukOtchetiPage.xaml
    /// </summary>
    public partial class RukOtchetiPage : Page
    {
        private MedLabEntities context = new MedLabEntities();
        public RukOtchetiPage()
        {
            InitializeComponent();

            List<string> list_cbx = new List<string> {"Статистика по типам анализов", "Доход лаборатории за указанный период", "Загрузка сотрудников" };
            choose_otchet.ItemsSource = list_cbx;

            s_data.Visibility = Visibility.Collapsed;
            border_po.Visibility = Visibility.Collapsed;
            border_s.Visibility = Visibility.Collapsed;
            po_data.Visibility = Visibility.Collapsed;
            period_po.Visibility = Visibility.Collapsed;
            period_s.Visibility = Visibility.Collapsed;

        }

        private void download_pdf_Click(object sender, RoutedEventArgs e)
        {
      
            var Month = DateTime.Now.ToString("MMMM");

            if (choose_otchet.SelectedIndex == 0)
            {
              
                string txtFilePath = System.IO.Path.Combine("output.txt");
                string pdfFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Статистика по типам анализов за {(Month)} .pdf");

                using (var context = new MedLabEntities())
                {
                    var currentMonth = DateTime.Now.Month;
                    var currentYear = DateTime.Now.Year;

                    var query = from ta in context.TypeAnalyzies
                                join a in context.Analyzis on ta.ID_TypeAnalyzies equals a.TypeAnalyz_ID
                                join o in context.Orders on a.ID_Analyz equals o.Analyz_ID
                                where
                                o.DateCreate != null
                                &&
                                SqlFunctions.DatePart("month", o.DateCreate) == currentMonth
                                && SqlFunctions.DatePart("year", o.DateCreate) == currentYear
                                group o by ta.NameAnalyz into g
                                orderby g.Count() descending
                                select new
                                {
                                    TypeName = g.Key,
                                    OrderCount = g.Count()
                                };


                    using (StreamWriter writer = new StreamWriter(txtFilePath, true, Encoding.UTF8))
                    {
                        string header1 = $"Статистика по типам анализов за {Month}";
                        int nameColumnWidth = 40;
                        int countColumnWidth = 30;
                        int lineWidth = nameColumnWidth + countColumnWidth;

                        string CenterString(string text, int width)
                        {
                            if (text.Length >= width) return text;
                            int padding = (width - text.Length) / 2;
                            return new string(' ', padding) + text;
                        }
                        writer.WriteLine(CenterString(header1, lineWidth));
                        writer.WriteLine(new string('-', lineWidth));
                        writer.WriteLine(string.Format("{0,-" + nameColumnWidth + "} {1," + countColumnWidth + "}", "Тип анализа", "Количество"));
                        writer.WriteLine(new string('-', lineWidth));

                        foreach (var item in query)
                        {
                            writer.WriteLine(string.Format("{0,-" + nameColumnWidth + "} {1," + (countColumnWidth - 5) + "}", item.TypeName, item.OrderCount));
                        }

                        writer.WriteLine(new string('-', lineWidth));
                    }

                    ConvertTxtToPdf(txtFilePath, pdfFilePath);
                    File.Delete(txtFilePath);
                }

              
            }


            if (choose_otchet.SelectedIndex == 2)
            {

                string txtFilePath2 = System.IO.Path.Combine("output_1.txt");
                string pdfFilePath2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Общая загрузка сотрудников.pdf");
                using (FileStream fs = new FileStream(txtFilePath2, FileMode.Create, FileAccess.Write, FileShare.None))
                using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    string header = "Общая загрузка сотрудников";
                    int lineWidth = 70;

                    string CenterString(string text, int width)
                    {
                        if (text.Length >= width) return text;
                        int padding = (width - text.Length) / 2;
                        return new string(' ', padding) + text;
                    }
                    writer.WriteLine(CenterString(header, lineWidth));
                    writer.WriteLine(new string('-', lineWidth));


                    var workersData = context.Workers
                       .Include(w => w.Role_ID)
                        .Select(w => new
                        {
                            w.LastNameW,
                            w.FirstNameW,
                            w.PatronymicW,
                            Position = w.RoleWorker.NameRole,
                            Contact = w.LoginWorker,
                        }).ToList();


                    foreach (var worker in workersData)
                    {
                        string FullName = $"{worker.LastNameW} {worker.FirstNameW} {worker.PatronymicW}";
                        writer.WriteLine($"{"ФИО:",-10} {FullName}");
                        writer.WriteLine($"{"Должность:",-10} {worker.Position}");
                        writer.WriteLine($"{"Контакт:",-10} {worker.Contact}");
                        writer.WriteLine(new string('-', lineWidth));
                    }

                }

                ConvertTxtToPdf(txtFilePath2, pdfFilePath2);
                File.Delete(txtFilePath2);
            }

            if (choose_otchet.SelectedIndex == 1)
            {
                if (!s_data.SelectedDate.HasValue || !po_data.SelectedDate.HasValue)
                {
                    MessageBox.Show("Необходимо выбрать начальную и конечную даты.");
                    return;
                }

                try
                {
                    string txtFilePath3 = System.IO.Path.Combine("output_2.txt");
                    string pdfFilePath3 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Доходы лаборатории .pdf");
                    using (FileStream fs = new FileStream(txtFilePath3, FileMode.Create, FileAccess.Write, FileShare.None))
                    using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
                    {
                        DateTime startDateValue = s_data.SelectedDate.Value.Date;
                        DateTime endDateValue = po_data.SelectedDate.Value.Date;

                        string header = $"Доходы лаборатории с {startDateValue:d} по {endDateValue:d}";
                        int lineWidth = 70;

                        string CenterString(string text, int width)
                        {
                            if (text.Length >= width) return text;
                            int padding = (width - text.Length) / 2;
                            return new string(' ', padding) + text;
                        }

                        writer.WriteLine(CenterString(header, lineWidth));
                        writer.WriteLine(new string('-', lineWidth));

                        using (var context = new MedLabEntities()) 
                        {
                            var incomeData = context.Set<Orders>()  
                                .Include(o => o.Analyzis)  
                                .ToList();
                            decimal totalIncome = incomeData
                              .Where(o => !string.IsNullOrEmpty(o.DateCreate))
                              .Where(o =>
                              {
                                  if (DateTime.TryParseExact(o.DateCreate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                                  {
                                      return parsedDate >= startDateValue && parsedDate <= endDateValue;
                                  }
                                  return false;
                              })
                              .Sum(o => o.Analyzis != null ? o.Analyzis.CostAnalyz : 0);
                            writer.WriteLine($"Общий доход: {totalIncome:F2} рублей");
                        }
                    }

                    ConvertTxtToPdf(txtFilePath3, pdfFilePath3);
                    File.Delete(txtFilePath3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        public static void ConvertTxtToPdf(string txtFilePath, string pdfFilePath)
        {
            try
            {
                using (PdfDocument document = new PdfDocument())
                {
                    PdfPageBase page = document.Pages.Add();
                    PdfTrueTypeFont font = new PdfTrueTypeFont("Times New Roman", 12f, PdfFontStyle.Regular, true);
                    PdfStringFormat format = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Top);
                    string[] lines = File.ReadAllLines(txtFilePath, Encoding.UTF8);
                    float y = 20, lineHeight = font.MeasureString("A").Height + 2;
                    foreach (string line in lines)
                    {
                        page.Canvas.DrawString(line, font, PdfBrushes.Black, new PointF(20, y), format);
                        y += lineHeight;
                    }
                    string uniquePdfFilePath = GenerateUniqueNamePdf(pdfFilePath);
                    document.SaveToFile(uniquePdfFilePath);
                    MessageBox.Show($"PDF файл успешно создан: {uniquePdfFilePath}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании PDF: {ex.Message}");
            }
        }

        private static string GenerateUniqueNamePdf(string pdfFilePath)
        {
            try
            {
                string directory = System.IO.Path.GetDirectoryName(pdfFilePath);
                string file_name = System.IO.Path.GetFileNameWithoutExtension(pdfFilePath);
                string pdf = System.IO.Path.GetExtension(pdfFilePath);
                int count = 1;
                string uniqueFileName = pdfFilePath;
                while (File.Exists(uniqueFileName))
                {
                    uniqueFileName = System.IO.Path.Combine(directory, $"{file_name}({count}){pdf}");
                    count++;
                }
                return uniqueFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании уникального имени файла");
                return null;
            }

        }

        private void choose_otchet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (choose_otchet.SelectedIndex == 1)
            {
                period_po.Visibility = Visibility.Visible;
                period_s.Visibility = Visibility.Visible;
                s_data.Visibility = Visibility.Visible;
                po_data.Visibility = Visibility.Visible;
                border_po.Visibility = Visibility.Visible;
                border_s.Visibility = Visibility.Visible;
            }
            else
            {
                s_data.Visibility = Visibility.Collapsed;
                po_data.Visibility = Visibility.Collapsed;
                period_po.Visibility = Visibility.Collapsed;
                period_s.Visibility = Visibility.Collapsed;
                border_po.Visibility = Visibility.Collapsed;
                border_s.Visibility = Visibility.Collapsed;
            }
        }
    }
}
