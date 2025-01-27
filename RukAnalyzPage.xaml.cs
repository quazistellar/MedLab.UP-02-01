using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Spire.Doc;
using Section = Spire.Doc.Section;
using Paragraph = Spire.Doc.Documents.Paragraph;


namespace MedLabUP
{
    /// <summary>
    /// Логика взаимодействия для RukAnalyzPage.xaml
    /// </summary>
    public partial class RukAnalyzPage : Page
    {
        private MedLabEntities context = new MedLabEntities();

        public class OrderInfo
        {
            public Orders Order { get; set; }
            public string AnalyzName { get; set; }
            public string ClientFullName { get; set; }
            public string StatusName { get; set; }
        }

        public RukAnalyzPage()
        {
            InitializeComponent();
            LoadData();
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
                    .Where(x => x.StatusName == "Готов" || x.StatusName == "Выдан клиенту")
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
                MessageBox.Show($"Ошибка при загрузке данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (analyzes.SelectedItem is OrderInfo selectedOrder)
            {
                try
                {
                    OrderInfo info = new OrderInfo();
                    var fullname = info.ClientFullName;
                     
                    byte[] wordBytes = GenerateWordFromOrderInfo(selectedOrder, context);
                    if (wordBytes != null)
                    {
                        string filePath = System.IO.Path.Combine("Результат анализа .docx");
                        File.WriteAllBytes(filePath, wordBytes);

                        byte[] pdfBytes = ConvertWordToPdf(filePath);

                        if (pdfBytes != null)
                        {
                            string pdfFilePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Результат анализа .pdf");
                            string uniquePdfFilePath = GenerateUniqueNamePdf(pdfFilePath);
                            File.WriteAllBytes(uniquePdfFilePath, pdfBytes);
                            System.Diagnostics.Process.Start(uniquePdfFilePath);
                            File.Delete(filePath);
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при генерации отчета: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите запись для создания отчета!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private byte[] GeneratePdfFromOrderInfo(OrderInfo orderInfo, MedLabEntities context)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    using (PdfDocument document = new PdfDocument())
                    {
                        PdfPageBase page = document.Pages.Add();
                        Font fontt = new Font("Times New Roman", 12f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point);
                        PdfFont font = new PdfFont(PdfFontFamily.TimesRoman, 12, PdfFontStyle.Regular);
                        PdfBrush brush = PdfBrushes.Black;

                        var result = (from ra in context.ResultAnalyzies
                                      join o in context.Orders on ra.Order_ID equals o.ID_Order
                                      join c in context.Clients on o.Client_ID equals c.ID_Client
                                      join a in context.Analyzis on o.Analyz_ID equals a.ID_Analyz
                                      join ta in context.TypeAnalyzies on a.TypeAnalyz_ID equals ta.ID_TypeAnalyzies
                                      join ob in context.Obrazchi on a.Obrazech_ID equals ob.ID_Obrazech
                                      join addr in context.Addresses on o.AddressoOrd_ID equals addr.ID_Address
                                      where o.ID_Order == orderInfo.Order.ID_Order
                                      select new
                                      {
                                          Result = ra.Result,
                                          DateStart = ra.DateStart,
                                          DateEnd = ra.DateEnd,
                                          NameAnalyz = a.NameAnalyz,
                                          NameObrazech = ob.NameObrazech,
                                          FirstNameC = c.FirstNameC,
                                          LastNameC = c.LastNameC,
                                          PatronymicC = c.PatronymicC,
                                          NameAddress = addr.NameAddress
                                      }).FirstOrDefault();


                        if (result == null)
                        {
                            throw new Exception("Данные не найдены для указанного Order ID");
                        }

                        document.SaveToStream(stream);
                        document.Close();
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании PDF: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

         private byte[] GenerateWordFromOrderInfo(OrderInfo orderInfo, MedLabEntities context)
         {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    Document document = new Document();
                    Section section = document.AddSection();

                    var result = (from ra in context.ResultAnalyzies
                                  join o in context.Orders on ra.Order_ID equals o.ID_Order
                                  join c in context.Clients on o.Client_ID equals c.ID_Client
                                  join a in context.Analyzis on o.Analyz_ID equals a.ID_Analyz
                                  join ta in context.TypeAnalyzies on a.TypeAnalyz_ID equals ta.ID_TypeAnalyzies
                                  join ob in context.Obrazchi on a.Obrazech_ID equals ob.ID_Obrazech
                                  join addr in context.Addresses on o.AddressoOrd_ID equals addr.ID_Address
                                  where o.ID_Order == orderInfo.Order.ID_Order
                                  select new
                                  {
                                      Result = ra.Result,
                                      DateStart = ra.DateStart,
                                      DateEnd = ra.DateEnd,
                                      NameAnalyz = a.NameAnalyz,
                                      NameObrazech = ob.NameObrazech,
                                      FirstNameC = c.FirstNameC,
                                      LastNameC = c.LastNameC,
                                      PatronymicC = c.PatronymicC,
                                      NameAddress = addr.NameAddress
                                  }).FirstOrDefault();


                    if (result == null)
                    {
                        throw new Exception("Данные не найдены для указанного Order ID");
                    }


                    string fio = $"{result.LastNameC} {result.FirstNameC} {result.PatronymicC}";

                    Paragraph title = section.AddParagraph();
                    title.AppendText($"Результат анализа '{result.NameAnalyz}' пациента {fio}");
                    title.BreakCharacterFormat.FontName = "Times New Roman";
                    title.BreakCharacterFormat.FontSize = 24;
                    title.BreakCharacterFormat.Bold = true;

                    section.AddParagraph().AppendText(Environment.NewLine);
                    Paragraph point = section.AddParagraph();
                    point.AppendText($"- - - - - - - - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - - - - ");
                    point.BreakCharacterFormat.FontName = "Times New Roman";
                    point.BreakCharacterFormat.FontSize = 12;

                    section.AddParagraph().AppendText(Environment.NewLine);

                    Paragraph labInfo = section.AddParagraph();
                    labInfo.AppendText($"Лаборатория 'МедЛаб', по адресу {result.NameAddress}.");
                    labInfo.BreakCharacterFormat.FontName = "Times New Roman";
                    labInfo.BreakCharacterFormat.FontSize = 12;

                    Paragraph dates = section.AddParagraph();
                    dates.AppendText($"Начало: {result.DateStart}, окончание: {result.DateEnd}.");
                    dates.BreakCharacterFormat.FontName = "Times New Roman";
                    dates.BreakCharacterFormat.FontSize = 12;

                    section.AddParagraph().AppendText(Environment.NewLine);

                    Paragraph typeAnalyz = section.AddParagraph();
                    typeAnalyz.AppendText($"Тип анализа: {result.NameAnalyz}.");
                    typeAnalyz.BreakCharacterFormat.FontName = "Times New Roman";
                    typeAnalyz.BreakCharacterFormat.FontSize = 12;

                    Paragraph obrazec = section.AddParagraph();
                    obrazec.AppendText($"Образец: {result.NameObrazech}.");
                    obrazec.BreakCharacterFormat.FontName = "Times New Roman";
                    obrazec.BreakCharacterFormat.FontSize = 12;

                    Paragraph resultText = section.AddParagraph();
                    resultText.AppendText($"Результат: {result.Result}.");
                    resultText.BreakCharacterFormat.FontName = "Times New Roman";
                    resultText.BreakCharacterFormat.FontSize = 12;

                    section.AddParagraph().AppendText(Environment.NewLine);
                    Paragraph point2 = section.AddParagraph();
                    point2.AppendText($"- - - - - - - - - - - - - - - - - - - -  - - - - - - - - - - - - - - - - - - - - - - - ");
                    point2.BreakCharacterFormat.FontName = "Times New Roman";
                    point2.BreakCharacterFormat.FontSize = 12;


                    try
                    {
                        string kartImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "iconmedlab.PNG");                     
                        string pechatImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pechat.PNG");

                        if (File.Exists(kartImagePath) && File.Exists(pechatImagePath))
                        {
                             AddImageToWord(section, kartImagePath);
                             AddImageToWord(section, pechatImagePath);
                        }
                        else
                        {
                            Paragraph noImage = section.AddParagraph();
                            noImage.AppendText("Изображения не найдены");
                            noImage.BreakCharacterFormat.FontName = "Times New Roman";
                            noImage.BreakCharacterFormat.FontSize = 12;
                            

                        }
                    }
                    catch (Exception ex)
                    {
                        Paragraph err = section.AddParagraph();
                        err.AppendText($"Ошибка загрузки изображений: {ex.Message}");
                        err.BreakCharacterFormat.FontName = "Times New Roman";
                        err.BreakCharacterFormat.FontSize = 12;

                    }
                     document.SaveToStream(stream, Spire.Doc.FileFormat.Docx);

                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании Word: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

          private void AddImageToWord(Section section, string imagePath)
          {
            try
            {
                Spire.Doc.Documents.Paragraph paragraph = section.AddParagraph();
                Spire.Doc.Fields.DocPicture picture = paragraph.AppendPicture(System.Drawing.Image.FromFile(imagePath));
                picture.Width = 100;
                picture.Height = 100;
                picture.HorizontalAlignment = ShapeHorizontalAlignment.Left;
            }
            catch(Exception ex)
            {
                 MessageBox.Show($"Ошибка при загрузке изображения в Word: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
          }

        private byte[] ConvertWordToPdf(string wordFilePath)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    Document document = new Document();
                    document.LoadFromFile(wordFilePath, (Spire.Doc.FileFormat)Spire.Pdf.FileFormat.PDF);
                    document.SaveToStream(stream, Spire.Doc.FileFormat.PDF);
                    return stream.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при конвертации в PDF: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
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


    }
}
