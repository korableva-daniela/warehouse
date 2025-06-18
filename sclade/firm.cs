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
using System.Security.Cryptography;
namespace sclade
{
    public partial class firm : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        DataSet dsi = new DataSet();
        public int id;

        public string name;

        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана

        public firm(NpgsqlConnection con, int id, string name)
        {
            this.id = id;

            this.name = name;

            this.con = con;
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
            if (id != 0)
            {
                button1.Visible = false;
                this.WindowState = FormWindowState.Maximized;

            }
            if (id == -2)
            {
                button1.Visible = false;


            }
            //if (id == 0)
            //{
            //    dataGridView2.Visible = false;
            //   label1.Visible = false;
            //}

            try
            {
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                textBox1.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);


                dataGridView2.Font = new Font("Arial", 9);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                if (this.name != "")
                {
                    textBox1.Text = this.name;
                }
                if (textBox1.Text == "")
                {
                    String sql = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id ORDER BY Firm.name_f ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Контактный телефон";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].HeaderText = "Статус поставщика";
                    dataGridView1.Columns[4].HeaderText = "Страна регистрации";
                    dataGridView1.Columns[5].HeaderText = "ИНН";
                    dataGridView1.Columns[6].HeaderText = "КПП";
                    dataGridView1.Columns[7].HeaderText = "ОРГН";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    String sql = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id and Firm.name_f ILIKE '";
                    sql += textBox1.Text;
                    sql += "%' ORDER BY  Firm.name_f ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Контактный телефон";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].HeaderText = "Статус поставщика";
                    dataGridView1.Columns[4].HeaderText = "Страна регистрации";
                    dataGridView1.Columns[5].HeaderText = "ИНН";
                    dataGridView1.Columns[6].HeaderText = "КПП";
                    dataGridView1.Columns[7].HeaderText = "ОРГН";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch { }
        }
        public void updateaddressinfo(int id)
        {
            try
            {
                if (id != null)
                {
                    String sqli = "Select Address_f.id, Firm.id, Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm , Address_f  where Firm.id =  Address_f.id_f and Firm.id=:id ORDER BY Address_f.id ASC;";

                    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                    dai.SelectCommand.Parameters.AddWithValue("id", id);
                    dsi.Reset();
                    dai.Fill(dsi);
                    dti = dsi.Tables[0];
                    dataGridView2.DataSource = dti;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].HeaderText = "Стран";
                    dataGridView2.Columns[3].HeaderText = "Город";
                    dataGridView2.Columns[4].HeaderText = "Улица";
                    dataGridView2.Columns[5].HeaderText = "Дом";
                    dataGridView2.Columns[6].HeaderText = "Индекс";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    String sqli = "Select Address_f.id, Firm.id,  Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm, Address_f  where Firm.id =  Address_f.id_f ORDER BY Address_f.id ASC;";

                    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);

                    dsi.Reset();
                    dai.Fill(dsi);
                    dti = dsi.Tables[0];
                    dataGridView2.DataSource = dti;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].HeaderText = "Стран";
                    dataGridView2.Columns[3].HeaderText = "Город";
                    dataGridView2.Columns[4].HeaderText = "Улица";
                    dataGridView2.Columns[5].HeaderText = "Дом";
                    dataGridView2.Columns[6].HeaderText = "Индекс";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch { }
        }

        private void firm_Load(object sender, EventArgs e)
        {
            try
            {
                Update();

                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                newfirm f = new newfirm(con, -1, "", "", "", "", "", "", "", "", "", "");
                f.ShowDialog();
                Update();
            }
            catch { }
        }

        private void адресToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                newaddressinfo_f f = new newaddressinfo_f(con, -1, id, "", "", "", "", "");
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string name = (string)dataGridView1.CurrentRow.Cells[1].Value;
                string phone = (string)dataGridView1.CurrentRow.Cells[2].Value;
                //string fio = (string)dataGridView1.CurrentRow.Cells[3].Value;
                string view = (string)dataGridView1.CurrentRow.Cells[3].Value;
                string country_of_registration = (string)dataGridView1.CurrentRow.Cells[4].Value;
                string INN = (string)dataGridView1.CurrentRow.Cells[5].Value;
                string KPP = (string)dataGridView1.CurrentRow.Cells[6].Value;
                string OGRN = (string)dataGridView1.CurrentRow.Cells[7].Value;
                string pc = (string)dataGridView1.CurrentRow.Cells[8].Value;
                string bank = (string)dataGridView1.CurrentRow.Cells[9].Value;
                string bik = (string)dataGridView1.CurrentRow.Cells[10].Value;

                newfirm f = new newfirm(con, id, name, phone, view, country_of_registration, INN, KPP, OGRN, pc, bank, bik);
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void адресToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView2.CurrentRow.Cells[0].Value;
                int id_f = (int)dataGridView2.CurrentRow.Cells[1].Value;
                string country = (string)dataGridView2.CurrentRow.Cells[2].Value;
                string city = (string)dataGridView2.CurrentRow.Cells[3].Value;
                string street = (string)dataGridView2.CurrentRow.Cells[4].Value;
                string house = (string)dataGridView2.CurrentRow.Cells[5].Value;
                string post_in = (string)dataGridView2.CurrentRow.Cells[6].Value;
                newaddressinfo_f f = new newaddressinfo_f(con, id, id_f, country, city, street, house, post_in);
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            { int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Firm WHERE id=:id", con);
                NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM  Address_f   WHERE id_f=:id", con);
                command.Parameters.AddWithValue("id", id);
                command1.Parameters.AddWithValue("id", id);
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    command1.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    Update();
                }
                else
                    Update();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void адресToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView2.CurrentRow.Cells["id"].Value;
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM  Address_f   WHERE id=:id", con);
                command.Parameters.AddWithValue("id", id);
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {

                    command.ExecuteNonQuery();
                    Update();
                }
                else
                    Update();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.name = textBox1.Text;
            Update();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (dataGridView1.CurrentRow != null)
                    if (dataGridView1.CurrentRow.Index != 0)
                    {
                        id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    }
                    else
                    {
                        if(dataGridView1.Rows[0].Cells[0].Value != null)
                        {
                            String sql1 = "Select * from firm  where id = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
                            //String sql1 = "Select * from invoices_in,storehouse where flag = 0  and storehouse.id_div = " + this.div.ToString() + " and storehouse.id=invoices_in.id_storehouse  ORDER BY invoices_in.num_invoices  ASC LIMIT 1 ;";
                            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                            ds6.Reset();
                            da6.Fill(ds6);
                            dt6 = ds6.Tables[0];
                            if (dt6.Rows.Count > 0)
                            {
                                id = Convert.ToInt32(dt6.Rows[0]["id"]);

                            }
                            else { id = -1; }
                        }else { id = -1; }
                    }
                else id = dataGridView1.RowCount;
                updateaddressinfo(id);
           
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                int id_ = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string name_ = (string)dataGridView1.CurrentRow.Cells[1].Value;

                this.name = name_;
                this.id = id_;
                Close();
            }
        }
        private void ExportToExcel(DataGridView dataGridView, string filePath)
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
                    if (i == 11)
                    {


                    }

                    else
                    {

                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }
                //}




                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    // Записываем данные
                    //for (int i = 0; i < dataGridView.Rows.Count; i++)
                    //{
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11)
                        {

                        }


                        else
                        {


                            worksheet.Cells[2, m] = dataGridView.Rows[0].Cells[j].Value?.ToString();
                            m++;
                        }


                    }
                }
                else
                {
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        int m = 1;
                        for (int j = 1; j < dataGridView.Columns.Count; j++)
                        {
                            if (j == 11)
                            {

                            }


                            else
                            {


                                worksheet.Cells[i + 2, m] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                                m++;
                            }

                        }
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

                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    if (i == 11)
                    {


                    }


                    else
                    {


                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11)
                        {

                        }


                        else
                        {
                            worksheet.Cells[i + 2, m] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            m++;
                        }

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
        private void ExportToExcel_address(DataGridView dataGridView, DataGridView dataGridView2, string filePath)
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
                    if (i == 11)
                    {


                    }

                    else
                    {

                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }
                //}




                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    // Записываем данные
                    //for (int i = 0; i < dataGridView.Rows.Count; i++)
                    //{
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11)
                        {

                        }


                        else
                        {


                            worksheet.Cells[2, m] = dataGridView.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value?.ToString();
                            m++;
                        }


                    }

                }
                int h_1 = 1;
                // Записываем заголовки столбцов
                //if (comboBox1.SelectedValue == null)
                //{
                for (int i = 2; i < dataGridView2.Columns.Count; i++)

                {
                    if (i == 11)
                    {


                    }

                    else
                    {

                        worksheet.Cells[4, h_1] = dataGridView2.Columns[i].HeaderText;
                        h_1++;
                    }
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 2; j < dataGridView2.Columns.Count; j++)
                    {



                        worksheet.Cells[i + 5, m] = dataGridView2.Rows[i].Cells[j].Value?.ToString();
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
        private void ExportJSON_all(DataGridView dataGridView, DataGridView dataGridView3, string filePath)
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


                        //if (dataGridView.Columns[j].Visible == true)
                        //{
                        data[dataGridView.Columns[j].HeaderText] = dataGridView1.CurrentRow.Cells[j].Value ?? ""; // Добавляем данные в словарь
                        //}

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
        private void ExportToJSON_address(DataGridView dataGridView, string filePath)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    var dataList = new List<Dictionary<string, object>>();

                    // Сбор данных из DataGridView
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (!row.IsNewRow) // Игнорируем пустую строку
                        {
                            var data = new Dictionary<string, object>();
                            for (int j = 2; j < dataGridView.Columns.Count; j++)
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
        private void ExportToWord_all(DataGridView dataGridView, DataGridView dataGridView3, string filePath)
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
                titleParagraph.Range.Text = "Контрагенты";
                titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph.Range.Font.Size = 12;

                titleParagraph.Range.InsertParagraphAfter();

                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count - 1);

                int h = 1;
                int tmp = 0;
                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {

                    //if (dataGridView.Columns[i].Visible == true)
                    //{
                    table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                    table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                    table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    table.Cell(1, h).Range.Font.Size = 8;
                    h++;
                    //}
                    //tmp = h;
                }



                // Добавляем заголовки столбцов
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {


                        table.Cell(i + 2, m).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(i + 2, m).Range.Font.Size = 8;
                        m++;



                    }





                }



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
        private void ExportToWord(DataGridView dataGridView, DataGridView dataGridView3, string filePath)
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
                    titleParagraph.Range.Text = "Контрагенты";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();


                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count -1);

                    int h = 1;
                    int tmp = 0;
                    for (int i = 1; i < dataGridView.Columns.Count; i++)

                    {

                        //if (dataGridView.Columns[i].Visible == true)
                        //{
                        table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, h).Range.Font.Size = 8;
                        h++;
                        //}
                        //tmp = h;
                    }


                  
                    // Заполняем таблицу данными
                    // Добавляем заголовки столбцов


                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        //if (dataGridView.Columns[j].Visible == true)
                        //{
                            table.Cell(2, m).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                            table.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(2, m).Range.Font.Size = 8;
                            m++;
                        //}


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
        }



        private void ExportToWord_address(DataGridView dataGridView, DataGridView dataGridView2, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;
            Word.Table table2 = null;
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                    // Создаем новый экземпляр Word
                    wordApp = new Word.Application();
                    wordDoc = wordApp.Documents.Add();
                    string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Контрагент";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();


                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count - 1);

                    int h = 1;
                    int tmp = 0;
                    for (int i = 1; i < dataGridView.Columns.Count; i++)

                    {

                        //if (dataGridView.Columns[i].Visible == true)
                        //{
                        table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, h).Range.Font.Size = 8;
                        h++;
                        //}
                        //tmp = h;
                    }



                    // Заполняем таблицу данными
                    // Добавляем заголовки столбцов


                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        //if (dataGridView.Columns[j].Visible == true)
                        //{
                        table.Cell(2, m).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                        table.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(2, m).Range.Font.Size = 8;
                        m++;
                        //}


                    }






                    table.Borders.Enable = 1; // Включаем рамки для всей таблицы
                    foreach (Word.Row row in table.Rows)
                    {
                        foreach (Word.Cell cell in row.Cells)
                        {
                            cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                        }
                    }





                    //string code = (string)dataGridView1.CurrentRow.Cells[1].Value;

                    Word.Paragraph titleParagraph2 = wordDoc.Content.Paragraphs.Add();
                    titleParagraph2.Range.Text = "Адреса контрагента " + code;
                    titleParagraph2.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph2.Range.Font.Size = 12;

                    titleParagraph2.Range.InsertParagraphAfter();
                    if (dataGridView2.Rows.Count == 0)
                    {
                        MessageBox.Show("Ошибка: Нет данных в DataGridView2.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создаем таблицу
                    table2 = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView2.Rows.Count + 1, dataGridView2.Columns.Count - 2);




                     h = 1;

                    for (int i = 2; i < dataGridView2.Columns.Count; i++)

                    {

                        //if (dataGridView.Columns[i].Visible == true)
                        //{
                        table2.Cell(1, h).Range.Text = dataGridView2.Columns[i].HeaderText;
                        table2.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table2.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table2.Cell(1, h).Range.Font.Size = 8;
                        h++;
                        //}
                        //tmp = h;
                    }



                    //// Добавляем заголовки столбцов
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        m = 1;
                        for (int j = 2; j < dataGridView2.Columns.Count; j++)
                        {


                            table2.Cell(i + 2, m).Range.Text = dataGridView2.Rows[i].Cells[j].Value?.ToString();
                            table2.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(i + 2, m).Range.Font.Size = 8;
                            m++;



                        }





                    }





                    foreach (Word.Row row in table2.Rows)
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
                if (table2 != null) Marshal.ReleaseComObject(table2);
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

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;

                        saveFileDialog.FileName = "counterparties_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
            }
            catch { }
        }

        private void вExcelДанныеВыбранногоПодразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            saveFileDialog.FileName = "counterparty_" + "_" + code.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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
            }
            catch { }
        }

        private void вExcelДанныеАдресовФирмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            saveFileDialog.FileName = "counterparty_address_" + "_" + code.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel_address(dataGridView1, dataGridView2, saveFileDialog.FileName);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            catch { }
        }

        private void вWordВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {



                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Files|*.docx";
                    saveFileDialog.Title = "Сохранить файл Word";
                    saveFileDialog.FileName = "counterparty_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToWord_all(dataGridView1, dataGridView2, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вWordВсеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = "counterparty_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWord(dataGridView1, dataGridView2, saveFileDialog.FileName);
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

        private void вJSONВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    saveFileDialog.FileName = $"counterparty_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Вызываем метод экспорта с выбранным путем
                        ExportJSON_all(dataGridView1, dataGridView2, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вJSONВыбранногоКонтрагентаToolStripMenuItem_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = $"counterparty_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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

        private void вJSONАдресКонтагентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { 
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                    saveFileDialog.FileName = $"counterparty_address_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Вызываем метод экспорта с выбранным путем
                        ExportToJSON_address(dataGridView2, saveFileDialog.FileName);
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

        private void вWordАдресаКонтрагентаToolStripMenuItem_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = $"counterparty_address_"+code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportToWord_address(dataGridView1, dataGridView2, saveFileDialog.FileName);
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

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
    }

