using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using NLog;
using Word = Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
namespace sclade
{
    public partial class dates : Form
    {
        public NpgsqlConnection con;
        public int stor;
        public int id;
        DataTable dt7 = new DataTable();
        DataSet ds7 = new DataSet();
        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        DataTable dt30 = new DataTable();
        DataSet ds30 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt200 = new DataTable();
        DataSet ds200 = new DataSet();
        private ProgressBar progressBar;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public dates(NpgsqlConnection con, int id, int stor)
        {
            this.con = con;
            this.MouseDown += new MouseEventHandler(MainForm_MouseDown);
            this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
            this.id = id;
            this.stor = stor;
            InitializeComponent();
            InitializeProgressBar();
        }
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Начинаем перетаскивание, если нажали левую кнопку мыши
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; // Получаем текущую позицию курсора
                dragFormPoint = this.Location; // Получаем текущее местоположение формы
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Если перетаскиваем форму, обновляем её позицию
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Завершаем перетаскивание
            dragging = false;
        }
        private void dates_Load(object sender, EventArgs e)
        {

        }
        private void InitializeProgressBar()
        {
            progressBar = new ProgressBar();
            progressBar.Location = new Point(200, 15); // Установите нужные координаты
            progressBar.Size = new Size(200, 30); // Установите нужный размер
            progressBar.Visible = false; // Скрываем его изначально
            this.Controls.Add(progressBar); // Добавляем ProgressBar на форму
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.id != -1)
            {
                if (this.stor ==-1)
                {
                    MessageBox.Show("Пожалуйста, выберите склад.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    String sql30 = "Select * from organization ";
                    NpgsqlDataAdapter da30 = new NpgsqlDataAdapter(sql30, con);
                    ds30.Reset();
                    da30.Fill(ds30);
                    dt30 = ds30.Tables[0];
                    if (dt30.Rows.Count > 0)
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {

                            saveFileDialog.Filter = "Word Files|*.docx";
                            saveFileDialog.Title = "Сохранить файл Word";
                            saveFileDialog.FileName = "accounting_" + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Создаем и настраиваем BackgroundWorker
                                BackgroundWorker worker = new BackgroundWorker();
                                worker.WorkerReportsProgress = true;

                                worker.DoWork += (s, args) =>
                                {
                                    int id = this.id;
                                    // Создание экземпляра Word
                                    Word.Application wordApp = new Word.Application();
                                    // Создание экземпляра Word
                                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "MX-5.docx");

                                    // Указываем путь для копии документа
                                    string copyPath = Path.Combine(saveFileDialog.FileName);

                                    // Копируем файл
                                    File.Copy(templatePath, copyPath, true); // true - перезаписывает файл, если он существует

                                    // Открываем копию документа
                                    Word.Document wordDoc = wordApp.Documents.Open(copyPath);

                                    // Делаем приложение видимым (по желанию)
                                    wordApp.Visible = true;

                                    String sql1 = "SELECT " +
             " CONCAT('  ',organization.name_f, ' , ИНН: ',organization.INN , ' , КПП: ',organization.KPP, ' , ОГРН: ',organization.OGRN  ) AS recipient," +
             "  CONCAT('Склад:  ', storehouse1.name, ' , Подразделение: ', Division1.name, ' , адрес: ', storehouse1.country_d, ' , ', storehouse1.city_d, ' ,  ', storehouse1.street_d, ' ,  ', storehouse1.house_d, ' , ', storehouse1.post_in_d) AS sclade_1, " +

              "  prod_storehouse.num_place AS worh" +


            " FROM prod_storehouse JOIN  storehouse AS storehouse1 ON storehouse1.id = prod_storehouse.id_store" +
                                    " JOIN Division AS Division1 ON storehouse1.id_div = Division1.id" +
            " JOIN organization ON organization.id=1" +
            " WHERE  prod_storehouse.id =  " + id;
                                    NpgsqlDataAdapter da7 = new NpgsqlDataAdapter(sql1, con);
                                    ds7.Reset();
                                    da7.Fill(ds7);
                                    dt7 = ds7.Tables[0];
                                    if (dt7.Rows.Count > 0)
                                    {
                                        for (int j = 0; j < dt7.Columns.Count; j++)
                                        {
                                            // Получаем значение ячейки
                                            var cellValue = dt7.Rows[0][j]?.ToString();

                                            // Заменяем закладки в документе
                                            string bookmarkName = dt7.Columns[j].ColumnName; // Пример имени закладки

                                            if (wordDoc.Bookmarks.Exists(bookmarkName))
                                            {
                                                wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue;
                                            }

                                            // Отправляем информацию о прогрессе
                                            int progressPercentage = (int)((j + 1) / (double)dt7.Columns.Count * 100);
                                            worker.ReportProgress(progressPercentage);
                                        }
                                        string startDate = dateTimePicker1.Value.ToString("dd MM yyyy");
                                        string endDate = dateTimePicker2.Value.ToString("dd MM yyyy");
                                        // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                                        wordDoc.Bookmarks["date1"].Range.Text = startDate.ToString();
                                        wordDoc.Bookmarks["date2"].Range.Text = endDate.ToString();
                                        String sql8 = "Select row_number() over (partition by prod_storehouse_info.id_prod_storehouse  order by prod_storehouse_info.id) as row_n,prod_storehouse_info.date_add, CONCAT('  ',Product_card.code,' , ',Product_card.name,' , номер партии:', batch_number.number, ' , ' ,Product_card.name_firm,' , ', Product_card.code) as num_pro, unit_of_measurement.litter as litter,Firm.name_f, prod_storehouse_info.count as col_pro, batch_number.price as price,batch_number.price*prod_storehouse_info.count as sum  from Firm,Product_card,batch_number,unit_of_measurement,prod_storehouse_info,prod_storehouse,storehouse where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and prod_storehouse.id =prod_storehouse_info.id_prod_storehouse  and batch_number.id=prod_storehouse_info.id_batch_number and batch_number.id_Firm = Firm.id  and prod_storehouse.id= " + id + " and   prod_storehouse_info.date_add >= '" + startDate + @"' 
        AND prod_storehouse_info.date_add <= '" + endDate + @"' and  storehouse.id=prod_storehouse.id_store and prod_storehouse_info.count>0 ORDER BY prod_storehouse_info.id ASC;";
                                        NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                                        da8.SelectCommand.Parameters.AddWithValue("id", id);
                                        ds8.Reset();
                                        da8.Fill(ds8);
                                        dt8 = ds8.Tables[0];
                                        // Вставка данных из DataGridView
                                        if (dt8.Rows.Count == 0)
                                        {
                                            MessageBox.Show("Поступлений за этот период не было!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {// Проверяем, существует ли закладка
                                            string bookmarkName = "table"; // Имя закладки соответствует имени столбца
                                            if (wordDoc.Bookmarks.Exists(bookmarkName))
                                            {
                                                // Получаем закладку
                                                Word.Bookmark bookmark = wordDoc.Bookmarks[bookmarkName];

                                                Word.Range range = bookmark.Range;
                                                Word.Table table = wordDoc.Tables.Add(range, 3, 10);
                                                foreach (Word.Cell cell in table.Rows[1].Cells)
                                                {
                                                    cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
                                                    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                }
                                                foreach (Word.Cell cell in table.Rows[2].Cells)
                                                {
                                                    cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
                                                    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                }
                                                foreach (Word.Cell cell in table.Rows[3].Cells)
                                                {
                                                    cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
                                                    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                }


                                                table.Cell(1, 6).Merge(table.Cell(1, 7));
                                                table.Cell(1, 6).Range.Text = "Товарный  документ";



                                                // "сумма, руб. коп." - 11 столбец
                                                table.Cell(2, 1).Range.Text = "Номер по порядку";

                                                table.Cell(2, 2).Range.Text = "Дата";



                                                table.Cell(2, 3).Range.Text = "Продукция, товарно - материальные ценности";

                                                table.Cell(2, 4).Range.Text = "Единица измерения";
                                                table.Cell(2, 5).Range.Text = "Поставщик (грузоотправитель)";

                                                table.Cell(2, 6).Range.Text = "Номер";
                                                table.Cell(2, 7).Range.Text = "Дата";
                                                table.Cell(2, 8).Range.Text = "Количество";
                                                table.Cell(2, 9).Range.Text = "Цена, руб.коп.";
                                                table.Cell(2, 10).Range.Text = "Сумма,руб.коп.";

                                                // Настройка выравнивания текста в заголовках
                                                string[] colNumbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
                                                for (int i = 0; i < colNumbers.Length; i++)
                                                {
                                                    table.Cell(3, i + 1).Range.Text = colNumbers[i];
                                                }


                                                foreach (Word.Row row in table.Rows)
                                                {
                                                    foreach (Word.Cell cell in row.Cells)
                                                    {
                                                        cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                        cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                                                    }
                                                }


                                                int k = 0;
                                                for (int i = 0; i < dt8.Rows.Count; i++)
                                                {
                                                    int h = 0;
                                                    Word.Row newRow = table.Rows.Add();
                                                    for (int j = 0; j < dt8.Columns.Count + 2; j++)
                                                    {
                                                        if (j != 5 && j != 6)
                                                        {
                                                            // Получаем значение ячейки
                                                            var cellValue = dt8.Rows[i][h]?.ToString();
                                                            newRow.Cells[j + 1].Range.Text = cellValue;
                                                            newRow.Cells[j + 1].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                            newRow.Cells[j + 1].Range.Font.Size = 8;
                                                            //if (wordDoc.Bookmarks.Exists(bookmarkName))
                                                            //{
                                                            //    wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue; // Вставляем значение в закладку
                                                            //}

                                                            ////Заменяем закладки в документе
                                                            //string bookmarkName_pro = dt8.Columns[j].ColumnName; // Пример имени закладки
                                                            //if (wordDoc.Bookmarks.Exists(bookmarkName_pro))
                                                            //{
                                                            //    wordDoc.Bookmarks[bookmarkName_pro].Range.Text = cellValue;
                                                            //    //newRow.Cells[j + 1].Range.Text = cellValue;
                                                            //}
                                                            h++;
                                                        }


                                                    }
                                                    k = i;
                                                }
                                                String sql200 = "Select SUM(prod_storehouse_info.count) as total_col,SUM(prod_storehouse_info.count*batch_number.price) as total_sum_nds from prod_storehouse,prod_storehouse_info , batch_number where batch_number.id = prod_storehouse_info.id_batch_number and prod_storehouse.id = prod_storehouse_info.id_prod_storehouse and prod_storehouse.id = " + id + " and   prod_storehouse_info.date_add >= '" + startDate + @"' 
        AND prod_storehouse_info.date_add <= '" + endDate + @"'    GROUP BY prod_storehouse.id";


                                                NpgsqlDataAdapter da200 = new NpgsqlDataAdapter(sql200, con);
                                                da200.SelectCommand.Parameters.AddWithValue("id", id);
                                                ds200.Reset();
                                                da200.Fill(ds200);
                                                dt200 = ds200.Tables[0];
                                                // Вставка данных из DataGridView
                                                if (dt200.Rows.Count > 0)
                                                {

                                                    int t = 8;
                                                    Word.Row newRow = table.Rows.Add();


                                                    // Получаем значение ячейки
                                                    var cellValue = dt200.Rows[0][0]?.ToString();
                                                    newRow.Cells[t].Range.Text = cellValue;
                                                    newRow.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                    newRow.Cells[t].Range.Font.Size = 8;

                                                    t = 10;
                                                    var cellValue2 = dt200.Rows[0][1]?.ToString();
                                                    newRow.Cells[t].Range.Text = cellValue2;
                                                    newRow.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                    newRow.Cells[t].Range.Font.Size = 8;

                                                    //if (wordDoc.Bookmarks.Exists(bookmarkName))
                                                    //{
                                                    //    wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue; // Вставляем значение в закладку
                                                    //}

                                                    ////Заменяем закладки в документе
                                                    //string bookmarkName_pro = dt8.Columns[j].ColumnName; // Пример имени закладки
                                                    //if (wordDoc.Bookmarks.Exists(bookmarkName_pro))
                                                    //{
                                                    //    wordDoc.Bookmarks[bookmarkName_pro].Range.Text = cellValue;
                                                    //    //newRow.Cells[j + 1].Range.Text = cellValue;


                                                    newRow.Cells[1].Merge(newRow.Cells[7]);

                                                    newRow.Cells[1].Range.Text = "Итого";

                                                    newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

                                                    string bookmarkName1 = dt200.Columns[1].ColumnName; // Пример имени закладки


                                                }
                                                table.Borders.Enable = 1; // Включаем рамки для всей таблицы
                                                foreach (Word.Row row in table.Rows)
                                                {
                                                    foreach (Word.Cell cell in row.Cells)
                                                    {
                                                        cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                                                    }
                                                }
                                            }
                                        }
                                    }





                                    else
                                    {

                                        MessageBox.Show("Место хранения не найдено.");
                                    }
                                    // Вставка данных из DataGridView
                                    //for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                    //{
                                    //    // Получаем значение ячейки
                                    //    var cellValue = dataGridView1.CurrentRow.Cells[j].Value?.ToString();

                                    //    // Заменяем закладки в документе
                                    //    string bookmarkName = dataGridView1.Columns[j].Name; // Пример имени закладки
                                    //    if (wordDoc.Bookmarks.Exists(bookmarkName))
                                    //    {
                                    //        wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue;
                                    //    }

                                    //    // Отправляем информацию о прогрессе
                                    //    int progressPercentage = (int)((j + 1) / (double)dataGridView1.Columns.Count * 100);
                                    //    worker.ReportProgress(progressPercentage);
                                    //}

                                    // Показываем Word
                                    wordApp.Visible = true;

                                    // Освобождаем ресурсы
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc);
                                    System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                                };

                                worker.ProgressChanged += (s, args) =>
                                {
                                    // Обновляем ProgressBar
                                    progressBar.Value = args.ProgressPercentage;
                                };

                                worker.RunWorkerCompleted += (s, args) =>
                                {
                                    // Скрываем ProgressBar после завершения
                                    progressBar.Visible = false;
                                };

                                // Настраиваем и запускаем ProgressBar
                                progressBar.Visible = true;
                                progressBar.Value = 0;

                                // Запускаем фоновую работу
                                worker.RunWorkerAsync();
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, заполните данные Вашей организации!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

}

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
