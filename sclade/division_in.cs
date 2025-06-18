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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

using Word = Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
namespace sclade
{
    public partial class division_in : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public int id_d;
        public string name;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public division_in(NpgsqlConnection con, int id_d, string name)
        {
            this.id_d = id_d;
            this.con = con;
            this.name = name;
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(MainForm_MouseDown);
            this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
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
        public void Update()
        {
            label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
            textBox2.Font = new Font("Arial", 11);
            textBox3.Font = new Font("Arial", 11);
            textBox4.Font = new Font("Arial", 11);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            label3.Font = new Font("Arial", 11);
            label4.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);




            if (this.id_d != 0)

            {
                button2.Visible = false;
            }

            if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where name ILIKE '";
                sql += textBox1.Text;
                sql += "%' ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where name ILIKE '";
                sql += textBox1.Text;
                sql += "%' and country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where name ILIKE '";
                sql += textBox1.Text;
                sql += "%' and city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where name ILIKE '";
                sql += textBox1.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' and city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where city_d  ILIKE '";
                sql += textBox3.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
            {
                String sql = "Select *  from Division where name  ILIKE '";
                sql += textBox1.Text;
                sql += "%' and country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' and city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where name  ILIKE '";
                sql += textBox1.Text;
                sql += "%' and country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where name  ILIKE '";
                sql += textBox1.Text;
                sql += "%' and city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where country_d  ILIKE '";
                sql += textBox2.Text;
                sql += "%' and city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
            {
                String sql = "Select *  from Division where name  ILIKE '";
                sql += textBox1.Text;
                sql += "%' and city_d ILIKE '";
                sql += textBox3.Text;
                sql += "%' and street_d ILIKE '";
                sql += textBox4.Text;
                sql += "%' and country_d ILIKE '";
                sql += textBox2.Text;
                sql += "%' ORDER BY id ASC;";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);

            }
            dt = ds.Tables[0];
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Название";
            dataGridView1.Columns[2].HeaderText = "Дата открытия";
            dataGridView1.Columns[3].HeaderText = "Стран";
            dataGridView1.Columns[4].HeaderText = "Город";
            dataGridView1.Columns[5].HeaderText = "Улица";
            dataGridView1.Columns[6].HeaderText = "Дом";
            dataGridView1.Columns[7].HeaderText = "Индекс";

            this.StartPosition = FormStartPosition.CenterScreen;

            //else
            //{
            //    String sql = "Select *  from Division where id=";
            //    sql += id_d.ToString();
            //    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            //    ds.Reset();
            //    da.Fill(ds);
            //    dt = ds.Tables[0];
            //    dataGridView1.DataSource = dt;
            //    dataGridView1.Columns[0].Visible = false;
            //    dataGridView1.Columns[1].HeaderText = "Название";
            //    dataGridView1.Columns[2].HeaderText = "Дата открытия";
            //    dataGridView1.Columns[3].HeaderText = "Стран";
            //    dataGridView1.Columns[4].HeaderText = "Город";
            //    dataGridView1.Columns[5].HeaderText = "Улица";
            //    dataGridView1.Columns[6].HeaderText = "Дом";
            //    dataGridView1.Columns[7].HeaderText = "Индекс";

            //    this.StartPosition = FormStartPosition.CenterScreen;
            //textBox1.Visible = false;
            //textBox2.Visible = false;
            //textBox3.Visible = false;
            //textBox4.Visible = false;
            //label2.Visible = false;
            //label4.Visible = false;
            //label5.Visible = false;
            //label6.Visible = false;
            //label7.Visible = false;
            //button3.Visible = false;


            //}

        }


        private void division_in_Load(object sender, EventArgs e)
        {
            Update();
            dataGridView1.ReadOnly = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                int id_ = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string name_ = (string)dataGridView1.CurrentRow.Cells[1].Value;

                this.name = name_;
                this.id_d = id_;
                Close();
            }
        }
        private void ExportToExcel_all(DataGridView dataGridView, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                // Создаем новую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;
                // Записываем заголовки столбцов
                //if (comboBox1.SelectedValue == null)
                //{
                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                    h++;
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {

                        worksheet.Cells[i + 2, m] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        m++;

                    }
                }

                workbook.SaveAs(filePath);
                // Освобождаем ресурсы
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }


        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;

                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                    h++;
                }

                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {

                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {

                        worksheet.Cells[2, m] = dataGridView.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value?.ToString();
                        m++;

                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


                workbook.SaveAs(filePath);
                // Освобождаем ресурсы
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportJSON_all(DataGridView dataGridView, string filePath)
        {
            try
            {
                var dataList = new List<Dictionary<string, object>>();

                // Сбор данных из DataGridView
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow) // Игнорируем пустую строку
                    {
                        var data = new Dictionary<string, object>();
                        for (int j = 1; j < dataGridView.Columns.Count; j++)
                        {


                            data[dataGridView.Columns[j].HeaderText] = row.Cells[j].Value ?? ""; // Добавляем данные в словарь

                        }
                        dataList.Add(data);
                    }
                }

                // Сериализация списка в JSON
                string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                // Сохранение JSON в файл
                File.WriteAllText(filePath, json);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportToJSON(DataGridView dataGridView, string filePath)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    var data = new Dictionary<string, object>();

                    // Сбор данных только из выбранной строки
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {

                        data[dataGridView1.Columns[j].HeaderText] = dataGridView1.CurrentRow.Cells[j].Value ?? ""; // Добавляем данные в словарь

                    }

                    // Сериализация в JSON
                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                    // Сохранение JSON в файл
                    File.WriteAllText(filePath, json);
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void ExportToWord_all(DataGridView dataGridView, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;

            try
            {
                // Создаем новый экземпляр Word
                wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();

                // Добавляем заголовок
                Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                titleParagraph.Range.Text = "Подразделения";
                titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph.Range.Font.Size = 12;

                titleParagraph.Range.InsertParagraphAfter();


                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count - 1);

                // Добавляем заголовки столбцов
                for (int i = 1; i < dataGridView.Columns.Count; i++)
                {
                    table.Cell(1, i).Range.Text = dataGridView.Columns[i].HeaderText;
                    table.Cell(1, i).Range.Font.Bold = 1; // Заголовок жирный
                    table.Cell(1, i).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    table.Cell(1, i).Range.Font.Size = 8;
                }

                // Заполняем таблицу данными
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        table.Cell(i + 2, j).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        table.Cell(i + 2, j).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(i + 2, j).Range.Font.Size = 8;
                    }
                }
                table.Borders.Enable = 1; // Включаем рамки для всей таблицы
                foreach (Word.Row row in table.Rows)
                {
                    foreach (Word.Cell cell in row.Cells)
                    {
                        cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                    }
                }
                // Сохраняем документ
                wordDoc.SaveAs(filePath);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                // Освобождаем ресурсы
                if (table != null) Marshal.ReleaseComObject(table);
                if (wordDoc != null)
                {
                    wordDoc.Close();
                    Marshal.ReleaseComObject(wordDoc);
                }
                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }
            }





        }
        private void ExportToWord(DataGridView dataGridView, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;

            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    // Создаем новый экземпляр Word
                    wordApp = new Word.Application();
                    wordDoc = wordApp.Documents.Add();

                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Подразделения";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();


                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count - 1);

                    // Добавляем заголовки столбцов
                    for (int i = 1; i < dataGridView.Columns.Count; i++)
                    {
                        table.Cell(1, i).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, i).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1, i).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, i).Range.Font.Size = 8;
                    }

                    // Заполняем таблицу данными

                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        table.Cell(2, j).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                        table.Cell(2, j).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(2, j).Range.Font.Size = 8;
                    }

                    table.Borders.Enable = 1; // Включаем рамки для всей таблицы
                    foreach (Word.Row row in table.Rows)
                    {
                        foreach (Word.Cell cell in row.Cells)
                        {
                            cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                        }
                    }
                    // Сохраняем документ
                    wordDoc.SaveAs(filePath);
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                // Освобождаем ресурсы
                if (table != null) Marshal.ReleaseComObject(table);
                if (wordDoc != null)
                {
                    wordDoc.Close();
                    Marshal.ReleaseComObject(wordDoc);
                }
                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }
            }





        }




        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    saveFileDialog.Title = "Сохранить файл Excel";
                    DateTime time = DateTime.Today.Date;

                    saveFileDialog.FileName = "Division_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                    }
                }
            }
            catch { }
        }

        private void вExcelДанныеВыбранногоПодразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ExportToExcel(dataGridView1, filePath);
                if (dataGridView1.CurrentRow != null)
                {

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Division_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel(dataGridView1, saveFileDialog.FileName);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    saveFileDialog.Title = "Сохранить файл Excel";
                    DateTime time = DateTime.Today.Date;

                    saveFileDialog.FileName = "Divisions_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                    }
                }
            }
            catch { }
        }

        private void вExcelИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ExportToExcel(dataGridView1, filePath);
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Divisions_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel(dataGridView1, saveFileDialog.FileName);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void вWordИнформациюВсехТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {



                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Files|*.docx";
                    saveFileDialog.Title = "Сохранить файл Word";
                    saveFileDialog.FileName = "Divisions_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToWord_all(dataGridView1, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вWordИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Word Files|*.docx";
                        saveFileDialog.Title = "Сохранить файл Word";
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Divisions_" + code + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWord(dataGridView1, saveFileDialog.FileName);
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

        private void вJSONИнформациюВсехТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    saveFileDialog.FileName = $"Divisions_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Вызываем метод экспорта с выбранным путем
                        ExportJSON_all(dataGridView1, saveFileDialog.FileName);
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void вJSONИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                        saveFileDialog.Title = "Сохраните файл JSON как";
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = $"Divisions_{code}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportToJSON(dataGridView1, saveFileDialog.FileName);
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
    }
}
