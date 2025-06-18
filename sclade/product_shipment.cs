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
    public partial class product_shipment : Form
    {
        public NpgsqlConnection con;
        public int stor;
        public int id_em;
        public string code;
        public int pro;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dt15 = new DataTable();
        DataSet ds15 = new DataSet();
        DataTable dt4 = new DataTable();
        DataSet ds4 = new DataSet();
        DataTable dt5 = new DataTable();
        DataSet ds5 = new DataSet();
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        DataTable dt7 = new DataTable();
        DataSet ds7 = new DataSet();
        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        DateTime shipment;
        DateTime shipment_to;
        List<String> messages = new List<String>();
        public int div;
        public product_shipment(NpgsqlConnection con, int stor, string code, int id_em, int pro,int div)
        {
            this.div = div;
            this.code = code;
            this.id_em = id_em;
            this.stor = stor;
            this.con = con;
            this.pro = pro;
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
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {


                    int num_invoices = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    string stor = (string)dataGridView1.CurrentRow.Cells[5].Value;
                    int all_quantity = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);
                    int quantity = Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value);
                    if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Расходная")
                    {
                        new_product_shipment f = new new_product_shipment(con, num_invoices, stor, 0, this.id_em, Convert.ToInt32(quantity), Convert.ToInt32(all_quantity), this.div);
                        f.ShowDialog();
                    }
                    if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Перемещение со склада")
                    {
                        new_product_shipment f = new new_product_shipment(con, num_invoices, stor, 1, this.id_em, Convert.ToInt32(quantity), Convert.ToInt32(all_quantity), this.div);
                        f.ShowDialog();
                    }
                    Update();
                    updateinvoices_in_info(num_invoices);

                }
                Update();
                Update_filt(messages);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void product_shipment_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox3.Text = "Типы накладных";
                comboBox3.Font = new Font("Arial", 11);
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                comboBox3.Enabled = true; // Сделать ComboBox доступным для выбора

                label1.Font = new Font("Arial", 11);

                label3.Font = new Font("Arial", 11);

                comboBox1.Font = new Font("Arial", 11);


                Update();
                dataGridView1.ReadOnly = true;
                Update_filt(
                    messages);

            }
            catch { }
        }
        public void Update_filt(List<string> messages)
        {

            if (messages.Count == 0)
            {
                if ((comboBox1.Text != "Склад не выбран"))
                {


                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text)));


                    // Проверяем, есть ли отфильтрованные строки
                    if (filterRows.Any())
                    {
                        // Создаем новый DataTable для отображения отфильтрованных данных
                        DataTable filteredTable = filterRows.CopyToDataTable();
                        // Обновляем DataGridView
                        dataGridView1.DataSource = filteredTable;
                    }

                    else
                    {
                        MessageBox.Show("Поступлений не было.");

                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "Номер накладной";
                        dataGridView1.Columns[2].HeaderText = "Дата";
                        dataGridView1.Columns[3].HeaderText = "Количество товара";
                        dataGridView1.Columns[4].HeaderText = "Количество собранного товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                        dataGridView1.Columns[7].HeaderText = "Статус";
                        dataGridView1.Columns[8].HeaderText = "Дата доставки";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран"))
                {

                    Update();

                }

            }
            if (messages.Count == 1)
            {
                if ((comboBox1.Text != "Склад не выбран"))
                {


                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<string>("invoice_type") == messages[0])));


                    // Проверяем, есть ли отфильтрованные строки
                    if (filterRows.Any())
                    {
                        // Создаем новый DataTable для отображения отфильтрованных данных
                        DataTable filteredTable = filterRows.CopyToDataTable();
                        // Обновляем DataGridView
                        dataGridView1.DataSource = filteredTable;
                    }

                    else
                    {
                        MessageBox.Show("Поступлений не было.");

                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "Номер накладной";
                        dataGridView1.Columns[2].HeaderText = "Дата";
                        dataGridView1.Columns[3].HeaderText = "Количество товара";
                        dataGridView1.Columns[4].HeaderText = "Количество собранного товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                        dataGridView1.Columns[7].HeaderText = "Статус";
                        dataGridView1.Columns[8].HeaderText = "Дата доставки";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран"))
                {
                    var filterRows = dt.AsEnumerable()
                     //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                     .Where(row => ((row.Field<string>("invoice_type") == messages[0])));


                    // Проверяем, есть ли отфильтрованные строки
                    if (filterRows.Any())
                    {
                        // Создаем новый DataTable для отображения отфильтрованных данных
                        DataTable filteredTable = filterRows.CopyToDataTable();
                        // Обновляем DataGridView
                        dataGridView1.DataSource = filteredTable;
                    }
                    else
                    {
                        MessageBox.Show("Поступлений не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "Номер накладной";
                        dataGridView1.Columns[2].HeaderText = "Дата";
                        dataGridView1.Columns[3].HeaderText = "Количество товара";
                        dataGridView1.Columns[4].HeaderText = "Количество собранного товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                        dataGridView1.Columns[7].HeaderText = "Статус";
                        dataGridView1.Columns[8].HeaderText = "Дата доставки";
                    }
                }

                if (messages.Count == 2)
                {
                    if ((comboBox1.Text != "Склад не выбран"))
                    {


                        var filterRows = dt.AsEnumerable()
                        //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                        .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


                        // Проверяем, есть ли отфильтрованные строки
                        if (filterRows.Any())
                        {
                            // Создаем новый DataTable для отображения отфильтрованных данных
                            DataTable filteredTable = filterRows.CopyToDataTable();
                            // Обновляем DataGridView
                            dataGridView1.DataSource = filteredTable;
                        }

                        else
                        {
                            MessageBox.Show("Поступлений не было.");

                            var originalTable = (DataTable)dataGridView1.DataSource;
                            dataGridView1.DataSource = null; // Очищаем DataSource
                            dataGridView1.DataSource = originalTable.Clone();
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.Columns[1].HeaderText = "Номер накладной";
                            dataGridView1.Columns[2].HeaderText = "Дата";
                            dataGridView1.Columns[3].HeaderText = "Количество товара";
                            dataGridView1.Columns[4].HeaderText = "Количество собранного товара";
                            dataGridView1.Columns[5].HeaderText = "Склад";
                            dataGridView1.Columns[6].HeaderText = "Тип накладной";
                            dataGridView1.Columns[7].HeaderText = "Статус";
                            dataGridView1.Columns[8].HeaderText = "Дата доставки";
                        }
                    }
                    if ((comboBox1.Text == "Склад не выбран"))
                    {


                        var filterRows = dt.AsEnumerable()
                        //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                        .Where(row => (((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


                        // Проверяем, есть ли отфильтрованные строки
                        if (filterRows.Any())
                        {
                            // Создаем новый DataTable для отображения отфильтрованных данных
                            DataTable filteredTable = filterRows.CopyToDataTable();
                            // Обновляем DataGridView
                            dataGridView1.DataSource = filteredTable;
                        }
                        else
                        {
                            MessageBox.Show("Поступлений не было.");
                            var originalTable = (DataTable)dataGridView1.DataSource;
                            dataGridView1.DataSource = null; // Очищаем DataSource
                            dataGridView1.DataSource = originalTable.Clone();
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.Columns[1].HeaderText = "Номер накладной";
                            dataGridView1.Columns[2].HeaderText = "Дата";
                            dataGridView1.Columns[3].HeaderText = "Количество товара";
                            dataGridView1.Columns[4].HeaderText = "Количество собранного товара";
                            dataGridView1.Columns[5].HeaderText = "Склад";
                            dataGridView1.Columns[6].HeaderText = "Тип накладной";
                            dataGridView1.Columns[7].HeaderText = "Статус";
                            dataGridView1.Columns[8].HeaderText = "Дата доставки";
                        }
                    }

                }
            }
        }
        public void updateinvoices_in_info(int id)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    string type = (string)dataGridView1.CurrentRow.Cells[6].Value;
                    if (type == "Перемещение со склада")
                    {
                        String sqli = "Select moving_info.id, moving.id,moving.num_invoices,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter, moving_info.quantity,moving_info.count  from Product_card,batch_number,unit_of_measurement,moving_info,moving where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and moving.id =moving_info.invoices_in and batch_number.id=moving_info.id_batch_number and moving.id=:id ORDER BY moving_info.id ASC;";

                        NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                        dai.SelectCommand.Parameters.AddWithValue("id", id);
                        dsi.Reset();
                        dai.Fill(dsi);
                        dti = dsi.Tables[0];
                        dataGridView2.DataSource = dti;
                        dataGridView2.Columns[0].Visible = false;
                        dataGridView2.Columns[1].Visible = false;
                        dataGridView2.Columns[2].Visible = false;
                        dataGridView2.Columns[3].HeaderText = "Номер партии";
                        dataGridView2.Columns[4].HeaderText = "Код товара";
                        dataGridView2.Columns[5].HeaderText = "Название товара";
                        dataGridView2.Columns[6].HeaderText = "Производитель";
                        dataGridView2.Columns[7].HeaderText = "Единица измерения";
                        dataGridView2.Columns[8].HeaderText = "Количество";
                        dataGridView2.Columns[9].HeaderText = "Количество собранного товара";
                    }
                    else
                    {
                        if (type == "Расходная")
                        {
                            String sqli = "Select invoices_in_info.id, invoices_in.id,invoices_in.num_invoices,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter, invoices_in_info.quantity,invoices_in_info.count from Product_card,batch_number,unit_of_measurement,invoices_in_info,invoices_in where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and invoices_in.id =invoices_in_info.invoices_in and batch_number.id=invoices_in_info.id_batch_number  and invoices_in.id=:id ORDER BY invoices_in_info.id ASC;";

                            NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                            dai.SelectCommand.Parameters.AddWithValue("id", id);
                            dsi.Reset();
                            dai.Fill(dsi);
                            dti = dsi.Tables[0];
                            dataGridView2.DataSource = dti;
                            dataGridView2.Columns[0].Visible = false;
                            dataGridView2.Columns[1].Visible = false;
                            dataGridView2.Columns[2].Visible = false;
                            dataGridView2.Columns[3].HeaderText = "Номер партии";
                            dataGridView2.Columns[4].HeaderText = "Код товара";
                            dataGridView2.Columns[5].HeaderText = "Название товара";
                            dataGridView2.Columns[6].HeaderText = "Производитель";
                            dataGridView2.Columns[7].HeaderText = "Единица измерения";
                            dataGridView2.Columns[8].HeaderText = "Количество";
                            dataGridView2.Columns[9].HeaderText = "Количество собранного товара";
                        }

                    }
                }


                else
                {


                }
            }
            catch { }
        }
        public void updatestorehouseinfo(int id_s)
        {
            try
            {
                String sql3 = "Select * from storehouse where id=";
                sql3 += id_s.ToString();

                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
                ds3.Reset();
                da3.Fill(ds3);
                dt3 = ds3.Tables[0];
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void Update()
        {

            try
            {
                if (this.stor != -1)
                {
                    updatestorehouseinfo(this.stor);

                }
                else
                {
                    comboBox1.Text = "Склад не выбран";
                }



                label1.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                //label2.Font = new Font("Arial", 11);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.Font = new Font("Arial", 9);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                //label5.Visible = false;
                comboBox1.Font = new Font("Arial", 11);
                comboBox1.Enabled = false;
                //comboBox3.Font = new Font("Arial", 11);


                //comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                //comboBox3.Enabled = true; // Сделать ComboBox доступным для выбора
                //comboBox3.Font = new Font("Arial", 11);
                //comboBox3.Text = "Типы накладных";
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
                dataGridView2.ContextMenuStrip = contextMenuStrip2;
                try
                {
                    //if ((this.stor != -1) & (this.pro != -1) & (shipment != dateTimePicker1.MinDate) & (shipment_to != dateTimePicker2.MaxDate))
                    //{

                    //}
                    //        if ((this.stor != -1) & (this.pro != -1) & (shipment != dateTimePicker1.MinDate) & (shipment_to != dateTimePicker2.MaxDate))
                    //{
                    String sql1 = "SELECT " +
    "    i.id AS id_invoice_number, " +
    "    i.num_invoices AS invoice_number,  " +
    "    i.shipment AS shipment_date,    " +
    "   SUM(ii.quantity) AS quantity,                  " +
    "   SUM(ii.count) AS count,                  " +
    "   s.name AS storehouse_name,                " +
    "  CASE " +
    "      WHEN i.flag = 0 THEN 'Приходная' " +
    "     WHEN i.flag = 1 THEN 'Расходная' " +
    "      WHEN i.flag = 2 THEN 'Перемещение' " +
    "   END AS invoice_type, " +
    "    i.status AS status,    " +
    "    i.shipment AS shipment    " +
    "FROM " +
    "    invoices_in_info ii " +
    "JOIN " +
    "    invoices_in i ON ii.invoices_in = i.id " +

    "JOIN " +
    "    storehouse s ON i.id_storehouse = s.id  where (i.status='Не указано' or i.status='В обработке') and i.flag = 1 and s.id_div = " + this.div.ToString() + "   GROUP BY i.id, i.num_invoices, i.shipment, s.name, i.flag " +


    "UNION ALL " +



    "SELECT" +
    " m.id AS id_moving_number, " +

    "   m.num_invoices AS invoice_number,           " +
    "   m.shipment AS shipment_date,           " +
    "   SUM(mi.quantity) AS quantity,                  " +
    "   SUM(mi.count) AS count,                  " +
    "  s1.name AS storehouse_name,             " +
    "   'Перемещение со склада' AS invoice_type, " +
    "    m.status AS status,    " +
    "    m.shipment AS shipment    " +
    "   " +
    "FROM " +
    "   moving_info mi " +
    "JOIN " +
    "  moving m ON mi.invoices_in = m.id " +

    "JOIN " +
    "   storehouse s1 ON m.id_storehouse_1 = s1.id   where (m.status='Не указано' or m.status='В обработке') and s1.id_div = "+this.div.ToString()+"  GROUP BY m.id, m.num_invoices, m.shipment, s1.name  ORDER BY shipment_date DESC";






                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                    ds.Reset();
                    da.Fill(ds);
                    //}


                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Номер накладной";
                    dataGridView1.Columns[2].HeaderText = "Дата";
                    dataGridView1.Columns[3].HeaderText = "Количество товара";
                    dataGridView1.Columns[4].HeaderText = "Количество собранного товара";
                    dataGridView1.Columns[5].HeaderText = "Склад";
                    dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    dataGridView1.Columns[7].HeaderText = "Статус";
                    dataGridView1.Columns[8].HeaderText = "Дата доставки";
                    this.StartPosition = FormStartPosition.CenterScreen;

                }
                catch { }


            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            updatestorehouseinfo(-1);
            comboBox1.Text = "Склад не выбран";
            this.stor = -1;
            Update_filt(messages);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name, div, "");
                fp.ShowDialog();
                if (fp.name != "")
                {

                    updatestorehouseinfo(fp.id_c);
                    this.stor = fp.id_c;
                    Update_filt(messages);
                }
                else
                {
                    comboBox1.Text = "Склад не выбран";

                }

            }
            catch { }
        }

        private void информацияОПартииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView2.CurrentRow.Cells[3].Value != null)
                {


                    string id_batch_number = (string)dataGridView2.CurrentRow.Cells[3].Value;

                    batch_info fp = new batch_info(con, id_batch_number, -1);
                    fp.Show();
                }

            }
            catch { }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView2.CurrentRow.Cells[4].Value != null)
                {

                    string id_pro = (string)dataGridView2.CurrentRow.Cells[4].Value;



                    prod_info fp = new prod_info(con, id_pro, -1);
                    fp.Show();
                }
            }
            catch { }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = 0;

            messages.Clear();

            string name_in = "";
            if (comboBox3.Text == "Все")
            {
                Update_filt(messages);
            }
            if (comboBox3.Text == "Приходные")
            {
                messages.Add("Приходная");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Расходные")
            {
                messages.Add("Расходная");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Перемещение")
            {
                messages.Add("Перемещение со склада");
                messages.Add("Перемещение на склад");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Перемещение со склада")
            {

                messages.Add("Перемещение со склада");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Перемещение на склад")
            {
                messages.Add("Перемещение на склад");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Приходные и Перемещение на склад")
            {
                messages.Add("Приходная");
                messages.Add("Перемещение на склад");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Расходные и Перемещение со склада")
            {
                messages.Add("Расходная");
                messages.Add("Перемещение со склада");
                Update_filt(messages);
            }
            if (comboBox3.Text == "Приходные и Расходные")
            {
                messages.Add("Приходная");
                messages.Add("Расходная");
                Update_filt(messages);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (dataGridView1.CurrentRow != null)
                {
                    if (dataGridView1.CurrentRow.Index != 0)
                    {
                        id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    }


                    else
                    {
                        if (dataGridView1.CurrentRow.Cells[6].Value != null && dataGridView1.Rows[0].Cells[0].Value != null)
                        {
                            if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Расходная")
                            {
                                String sql1 = "Select * from invoices_in where id = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
                                NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                                ds6.Reset();
                                da6.Fill(ds6);
                                dt6 = ds6.Tables[0];
                                if (dt6.Rows.Count > 0)
                                {
                                    id = Convert.ToInt32(dt6.Rows[0]["id"]);

                                }
                                else { id = -1; }
                            }
                            else
                            {


                                if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Перемещение со склада")
                                {
                                    String sql2 = "Select * from moving where  id = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
                                    NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql2, con);
                                    ds9.Reset();
                                    da9.Fill(ds9);
                                    dt9 = ds9.Tables[0];
                                    if (dt9.Rows.Count > 0)
                                    {
                                        id = Convert.ToInt32(dt9.Rows[0]["id"]);

                                    }
                                    else { id = -1; }
                                }
                                else { id = -1; }
                            }
                        }
                        else { id = -1; }
                    }
                }

                else id = dataGridView1.RowCount;
                updateinvoices_in_info(id);
            }
            catch { }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {


                    string number = (string)dataGridView1.CurrentRow.Cells[1].Value;
                    if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Приходная")
                    {
                        invoices_in fp = new invoices_in(con, this.stor, this.id_em, number, 0, this.div);
                        fp.Show();
                    }
                    else if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Расходная")
                    {
                        invoices_ fp = new invoices_(con, this.stor, this.id_em, number, 0, this.div);
                        fp.Show();
                    }
                    else if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Перемещение на склад")
                    {
                        moving fp = new moving(con, -1, this.id_em, this.stor, number, 0, this.div);
                        fp.Show();
                    }
                    else if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Перемещение со склада")
                    {
                        moving fp = new moving(con, this.stor, this.id_em, -1, number, 0, this.div);
                        fp.Show();
                    }
                }
        }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
