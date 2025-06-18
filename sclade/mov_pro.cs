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
    public partial class mov_pro : Form
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
        DataTable dt30 = new DataTable();
        DataSet ds30 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt200 = new DataTable();
        DataSet ds200 = new DataSet();
        DateTime shipment;
        public int div;
        DateTime shipment_to;
        private ProgressBar progressBar;
        List<String> messages = new List<String>();

        public mov_pro(NpgsqlConnection con, int stor, string code, int id_em, int pro,int div)
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
                

                label1.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                //label2.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                //label5.Visible = false;
                comboBox1.Font = new Font("Arial", 11);
                comboBox1.Enabled = false;
                //comboBox3.Font = new Font("Arial", 11);
                comboBox2.Enabled = false;

                //comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                //comboBox3.Enabled = true; // Сделать ComboBox доступным для выбора
                //comboBox3.Font = new Font("Arial", 11);
                //comboBox3.Text = "Типы накладных";
                dataGridView1.ContextMenuStrip = contextMenuStrip2;

                try
                {
                    //if ((this.stor != -1) & (this.pro != -1) & (shipment != dateTimePicker1.MinDate) & (shipment_to != dateTimePicker2.MaxDate))
                    //{

                    //}
                    //        if ((this.stor != -1) & (this.pro != -1) & (shipment != dateTimePicker1.MinDate) & (shipment_to != dateTimePicker2.MaxDate))
                    //{
                    String sql1 = "SELECT " +
"   i.num_invoices AS invoice_number,  " +
"    i.shipment AS shipment_date,    " +
"   pc.code AS product_code,       " +
"    bn.number AS batch_number,        " +
"   ii.quantity AS quantity,                  " +
"   s.name AS storehouse_name,                " +
"  CASE " +
"      WHEN i.flag = 0 THEN 'Приходная' " +
"       WHEN i.flag = 1 THEN 'Расходная' " +
"      WHEN i.flag = 2 THEN 'Перемещение' " +
"   END AS invoice_type " +
"FROM " +
"    invoices_in_info ii " +
"JOIN " +
"    invoices_in i ON ii.invoices_in = i.id " +
"JOIN " +
"    batch_number bn ON ii.id_batch_number = bn.id " +
"JOIN " +
"    storehouse s ON i.id_storehouse = s.id " +
"JOIN " +
"    Product_card pc ON ii.id_Product_card = pc.id  and s.id_div = " + this.div.ToString() + "  " +


"UNION ALL " +

"SELECT " +
"   m.num_invoices AS invoice_number,       " +
"   m.shipment AS shipment_date,          " +
"  pc.code AS product_code,     " +
"   bn.number AS batch_number,        " +
"  mi.quantity AS quantity,                 " +
"  s1.name AS storehouse_name,           " +
"  'Перемещение со склада' AS invoice_type  " +
"FROM " +
"   moving_info mi " +
"JOIN " +
"   moving m ON mi.invoices_in = m.id " +
"JOIN " +
"   batch_number bn ON mi.id_batch_number = bn.id " +
"JOIN " +
"  storehouse s1 ON m.id_storehouse_1 = s1.id " +
"JOIN " +
"   Product_card pc ON mi.id_Product_card = pc.id  and s1.id_div = " + this.div.ToString() + "  " +


"UNION ALL " +

"SELECT " +
"   m.num_invoices AS invoice_number,           " +
"   m.shipment AS shipment_date,           " +
"   pc.code AS product_code,       " +
"   bn.number AS batch_number,      " +
"   mi.quantity AS quantity,            " +
"  s2.name AS storehouse_name,             " +
"   'Перемещение на склад' AS invoice_type " +
"   " +
"FROM " +
"   moving_info mi " +
"JOIN " +
"  moving m ON mi.invoices_in = m.id " +
"JOIN " +
"   batch_number bn ON mi.id_batch_number = bn.id " +
"JOIN " +
"   storehouse s2 ON m.id_storehouse_2 = s2.id " +
"JOIN " +
"    Product_card pc ON mi.id_Product_card = pc.id  and s2.id_div = " + this.div.ToString() + "  ORDER BY shipment_date DESC";






                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                    ds.Reset();
                    da.Fill(ds);
                    //}


                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "Номер накладной";
                    dataGridView1.Columns[1].HeaderText = "Дата";
                    dataGridView1.Columns[2].HeaderText = "Код товара";
                    dataGridView1.Columns[3].HeaderText = "Номер партии";
                    dataGridView1.Columns[4].HeaderText = "Количество товара";
                    dataGridView1.Columns[5].HeaderText = "Склад";
                    dataGridView1.Columns[6].HeaderText = "Тип накладной";


                    this.StartPosition = FormStartPosition.CenterScreen;
                    if (dataGridView1.Rows.Count > 0) // Проверяем, есть ли строки в DataGridView
                    {
                        if (this.stor != -1)
                        {
                            updatestorehouseinfo(this.stor);
                            Update_filt(messages);
                        }
                        else
                        {
                            comboBox1.Text = "Склад не выбран";
                        }
                        if (this.pro != -1 || this.code != "")
                        {
                            if (this.pro != -1)
                            {
                                updateProduct_cardinfo(this.pro);
                                Update_filt(messages);
                            }
                            else
                            {
                                updateProduct_cardinfoupdate(this.code);
                                Update_filt(messages);
                            }

                        }

                        else
                        {
                            comboBox2.Text = "Товар не выбран";
                        }
                        int lastRowIndex = dataGridView1.Rows.Count - 2;
                        //DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
                        // Проверяем, есть ли строки и не является ли значение null
                        if (lastRowIndex >= 0 && dataGridView1.Rows[lastRowIndex].Cells[1].Value != null)
                        {
                            // Пытаемся привести значение к типу DateTime
                            DateTime shipmentDate;
                            //if (DateTime.TryParse(dataGridView1.Rows[lastRowIndex].Cells[1].Value, out shipmentDate))
                            //{
                            dateTimePicker1.Value = (DateTime)dataGridView1.Rows[lastRowIndex].Cells[1].Value; // Устанавливаем значение в DateTimePicker
                                                                                                               //}
                        }

                    }

                }
                catch { }


            }
            catch { }
        }
        private void mov_pro_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox3.Text = "Типы накладных";
                comboBox3.Font = new Font("Arial", 11);
                comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                comboBox3.Enabled = true; // Сделать ComboBox доступным для выбора
                comboBox2.Font = new Font("Arial", 11);
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);

                comboBox1.Font = new Font("Arial", 11);


                Update();
                dataGridView1.ReadOnly = true;
                //Update_filt();

            }
            catch { }
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
                if (dataGridView1.Rows.Count > 0) // Проверяем, есть ли строки в DataGridView
                {
                    int lastRowIndex = dataGridView1.Rows.Count - 2;
                    //DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
                    // Проверяем, есть ли строки и не является ли значение null
                    if (lastRowIndex >= 0 && dataGridView1.Rows[lastRowIndex].Cells[1].Value != null)
                    {
                        // Пытаемся привести значение к типу DateTime
                        DateTime shipmentDate;
                        //if (DateTime.TryParse(dataGridView1.Rows[lastRowIndex].Cells[1].Value, out shipmentDate))
                        //{
                        dateTimePicker1.Value = (DateTime)dataGridView1.Rows[lastRowIndex].Cells[1].Value; // Устанавливаем значение в DateTimePicker
                                                                                                           //}
                    }
                }
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[2].Value != null)
                {

                    string id_pro = (string)dataGridView1.CurrentRow.Cells[2].Value;



                    prod_info fp = new prod_info(con, id_pro, -1);
                    fp.Show();
                }
            }
            catch { }
        }

        private void информацияОПартииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[3].Value != null)
                {


                    string id_batch_number = (string)dataGridView1.CurrentRow.Cells[3].Value;

                    batch_info fp = new batch_info(con, id_batch_number, -1);
                    fp.Show();
                }

            }
            catch { }
        }
        public void updateProduct_cardinfo(int id_pro)
        {
            try
            {

                String sql1 = @"SELECT id, code FROM Product_card 
                                                  WHERE id = ";
                sql1 += id_pro.ToString();


                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                comboBox2.DataSource = dt1;
                comboBox2.DisplayMember = "code";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;

            }
            catch { }
        }
        public void updateProduct_cardinfoupdate(string code)
        {
            try
            {

                //if (id_pro != -1)
                //{
                //    comboBox1.Text = "Товар не выбран";
                //comboBox2.Text = "Товар не выбран";
                //String sql1 = "Select * from Product_card ORDER BY code ASC";

                //NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                //ds1.Reset();
                //da1.Fill(ds1);
                //dt1 = ds1.Tables[0];
                //comboBox1.DataSource = dt1;
                //comboBox1.DisplayMember = "code";
                //comboBox1.ValueMember = "id";
                //this.StartPosition = FormStartPosition.CenterScreen;
                ////}
                //else
                //{

                try
                {

                    String sql15 = @"SELECT id, code FROM Product_card 
                                                  WHERE code = '";
                    sql15 += code;
                    sql15 += "'";


                    NpgsqlDataAdapter da15 = new NpgsqlDataAdapter(sql15, con);
                    ds15.Reset();
                    da15.Fill(ds15);
                    dt15 = ds15.Tables[0];
                    comboBox2.DataSource = dt15;
                    comboBox2.DisplayMember = "code";
                    comboBox2.ValueMember = "id";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
                //}
            }
            catch { }



        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {


                int id = 0;
                string name = "";
                string code = "";
                int aa;


                if (this.stor != -1)
                {
                    Product_card fp = new Product_card(con, id, name, code, "", this.stor, this.div);
                    fp.ShowDialog();
                    if (fp.code != "")
                    {

                        updateProduct_cardinfo(fp.id);
                        this.pro = fp.id;
                        Update_filt(messages);

                    }

                }
                else
                {
                    Product_card fp = new Product_card(con, id, name, code, "", -1, this.div);
                    fp.ShowDialog();
                    if (fp.code != "")
                    {

                        updateProduct_cardinfo(fp.id);
                        this.pro = fp.id;
                        Update_filt(messages);

                    }

                }
                if (dataGridView1.Rows.Count > 0) // Проверяем, есть ли строки в DataGridView
                {
                    int lastRowIndex = dataGridView1.Rows.Count - 2;
                    //DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
                    // Проверяем, есть ли строки и не является ли значение null
                    if (lastRowIndex >= 0 && dataGridView1.Rows[lastRowIndex].Cells[1].Value != null)
                    {
                        // Пытаемся привести значение к типу DateTime
                        DateTime shipmentDate;
                        //if (DateTime.TryParse(dataGridView1.Rows[lastRowIndex].Cells[1].Value, out shipmentDate))
                        //{
                        dateTimePicker1.Value = (DateTime)dataGridView1.Rows[lastRowIndex].Cells[1].Value; // Устанавливаем значение в DateTimePicker
                                                                                                           //}
                    }
                }
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            updateProduct_cardinfo(-1);
            comboBox2.Text = "Товар не выбран";
            this.pro = -1;
            Update_filt(messages);
        }
        public void Update_filt(List<string> messages)
        {

            if (messages.Count == 0)
            {
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate)));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");

                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate)));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate)));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate)));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
            }
            if (messages.Count == 1)
            {
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && (row.Field<string>("invoice_type") == messages[0])));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");

                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && (row.Field<string>("invoice_type") == messages[0])));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && (row.Field<string>("invoice_type") == messages[0])));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && (row.Field<string>("invoice_type") == messages[0])));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
            }
            if (messages.Count == 2)
            {
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");

                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text) && (row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    DateTime startDate = dateTimePicker1.Value.Date;
                    DateTime endDate = dateTimePicker2.Value.Date;
                    // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                    endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<DateTime>("shipment_date") >= startDate) && (row.Field<DateTime>("shipment_date") <= endDate) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Update_filt(messages);
            if (dataGridView1.Rows.Count > 0) // Проверяем, есть ли строки в DataGridView
            {
                int lastRowIndex = dataGridView1.Rows.Count - 2;
                //DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
                // Проверяем, есть ли строки и не является ли значение null
                if (lastRowIndex >= 0 && dataGridView1.Rows[lastRowIndex].Cells[1].Value != null)
                {
                    // Пытаемся привести значение к типу DateTime
                    DateTime shipmentDate;
                    //if (DateTime.TryParse(dataGridView1.Rows[lastRowIndex].Cells[1].Value, out shipmentDate))
                    //{
                    dateTimePicker1.Value = (DateTime)dataGridView1.Rows[lastRowIndex].Cells[1].Value; // Устанавливаем значение в DateTimePicker
                    //}
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0) // Проверяем, есть ли строки в DataGridView
            {
                int lastRowIndex = dataGridView1.Rows.Count - 2;
                //DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
                // Проверяем, есть ли строки и не является ли значение null
                if (lastRowIndex >= 0 && dataGridView1.Rows[lastRowIndex].Cells[1].Value != null)
                {
                    // Пытаемся привести значение к типу DateTime
                    DateTime shipmentDate;
                    //if (DateTime.TryParse(dataGridView1.Rows[lastRowIndex].Cells[1].Value, out shipmentDate))
                    //{
                    dateTimePicker1.Value = (DateTime)dataGridView1.Rows[lastRowIndex].Cells[1].Value; // Устанавливаем значение в DateTimePicker
                    //}
                }
            }

            dateTimePicker2.Value = DateTime.Today.Date;
            if (messages.Count == 0)
            {

                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<string>("storehouse_name") == comboBox1.Text)));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text)));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {
                    Update();
                }
            }
            if (messages.Count == 1)
            {
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<string>("storehouse_name") == comboBox1.Text) && ((row.Field<string>("invoice_type") == messages[0]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && ((row.Field<string>("invoice_type") == messages[0]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("storehouse_name") == comboBox1.Text) && ((row.Field<string>("invoice_type") == messages[0]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {


                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => (((row.Field<string>("invoice_type") == messages[0]))));


                    if (filterRows.Any())
                    {
                        // Создаем новый DataTable для отображения отфильтрованных данных
                        DataTable filteredTable = filterRows.CopyToDataTable();
                        // Обновляем DataGridView
                        dataGridView1.DataSource = filteredTable;
                    }
                    else
                    {
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
            }
            if (messages.Count == 2)
            {
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && (row.Field<string>("storehouse_name") == comboBox1.Text) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text != "Товар не выбран"))
                {

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("product_code") == comboBox2.Text) && ((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text != "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
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
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
                if ((comboBox1.Text == "Склад не выбран") && (comboBox2.Text == "Товар не выбран"))
                {


                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => (((row.Field<string>("invoice_type") == messages[0]) || (row.Field<string>("invoice_type") == messages[1]))));


                    if (filterRows.Any())
                    {
                        // Создаем новый DataTable для отображения отфильтрованных данных
                        DataTable filteredTable = filterRows.CopyToDataTable();
                        // Обновляем DataGridView
                        dataGridView1.DataSource = filteredTable;
                    }
                    else
                    {
                        MessageBox.Show("Поступлений за выбранный период не было.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                        dataGridView1.Columns[0].HeaderText = "Номер накладной";
                        dataGridView1.Columns[1].HeaderText = "Дата";
                        dataGridView1.Columns[2].HeaderText = "Код товара";
                        dataGridView1.Columns[3].HeaderText = "Номер партии";
                        dataGridView1.Columns[4].HeaderText = "Количество товара";
                        dataGridView1.Columns[5].HeaderText = "Склад";
                        dataGridView1.Columns[6].HeaderText = "Тип накладной";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void информацияОНакладнойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {


                    string number = (string)dataGridView1.CurrentRow.Cells[0].Value;
                    if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Приходная")
                    {
                        invoices_in fp = new invoices_in(con, this.stor, this.id_em, number,0, div);
                        fp.Show();
                    }
                    else if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Расходная")
                    {
                        invoices_ fp = new invoices_(con, this.stor, this.id_em, number, 0, div);
                        fp.Show();
                    }
                    else if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Перемещение на склад")
                    {
                        moving fp = new moving(con, -1, this.id_em, this.stor, number, 0, div);
                        fp.Show();
                    }
                    else if ((string)dataGridView1.CurrentRow.Cells[6].Value == "Перемещение со склада")
                    {
                        moving fp = new moving(con, this.stor, this.id_em, -1, number, 0, div);
                        fp.Show();
                    }
                }
            }
            catch { }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
            dateTimePicker2.Value = DateTime.Today.Date;
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;
            endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня
            Update_filt(messages);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddMonths(-3);
            dateTimePicker2.Value = DateTime.Today.Date;
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;
            endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня
            Update_filt(messages);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddMonths(-12);
            dateTimePicker2.Value = DateTime.Today.Date;
            DateTime startDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;
            endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня
            Update_filt(messages);
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
            if (dataGridView1.Rows.Count > 0) // Проверяем, есть ли строки в DataGridView
            {
                int lastRowIndex = dataGridView1.Rows.Count - 2;
                //DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
                // Проверяем, есть ли строки и не является ли значение null
                if (lastRowIndex >= 0 && dataGridView1.Rows[lastRowIndex].Cells[1].Value != null)
                {
                    // Пытаемся привести значение к типу DateTime
                    DateTime shipmentDate;
                    //if (DateTime.TryParse(dataGridView1.Rows[lastRowIndex].Cells[1].Value, out shipmentDate))
                    //{
                    dateTimePicker1.Value = (DateTime)dataGridView1.Rows[lastRowIndex].Cells[1].Value; // Устанавливаем значение в DateTimePicker
                    //}
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

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
                for (int i = 0; i < dataGridView.Columns.Count; i++)

                {



                    worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                    h++;

                }
                //}




                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    // Записываем данные
                    //for (int i = 0; i < dataGridView.Rows.Count; i++)
                    //{
                    int m = 1;
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {


                        worksheet.Cells[2, m] = dataGridView.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value?.ToString();
                        m++;



                    }
                }
                else
                {
                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        int m = 1;
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
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

                for (int i = 0; i < dataGridView.Columns.Count; i++)

                {

                    worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                    h++;

                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
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

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void выгрузитьВExcelВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "Склад не выбран")
                //ExportToExcel(dataGridView1, filePath);
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;

                        saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
                else
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;

                        saveFileDialog.FileName = "invoices_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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
                if (comboBox1.Text != "Склад не выбран")
                //ExportToExcel(dataGridView1, filePath);
                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                            string code = (string)dataGridView1.CurrentRow.Cells[0].Value;
                            saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel(dataGridView1, saveFileDialog.FileName);
                            }
                        }

                    }
                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;

                            saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel(dataGridView1, saveFileDialog.FileName);
                            }
                        }
                    }
                }
                else
                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                            string code = (string)dataGridView1.CurrentRow.Cells[0].Value;
                            saveFileDialog.FileName = "invoices_" + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel(dataGridView1, saveFileDialog.FileName);
                            }
                        }

                    }
                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;

                            saveFileDialog.FileName = "invoices_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel(dataGridView1, saveFileDialog.FileName);
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private void ExportToWordProduct(DataGridView dataGridView, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;
            Word.Table table2 = null;
            try
            {
                wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                Word.Paragraph titleParagraph2 = wordDoc.Content.Paragraphs.Add();
                titleParagraph2.Range.Text = "Данные о товаре";
                titleParagraph2.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph2.Range.Font.Size = 12;

                titleParagraph2.Range.InsertParagraphAfter();
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Ошибка: Нет данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Создаем таблицу
                if (comboBox2.Text != "Товар не выбран")
                {



                    String sql8 = "Select Product_card.code as cod_pro,Product_card.name as name_pro,Type_to.name ,Product_card.name_firm as name_firm,Product_card.col_pro as col_pro, unit_of_measurement.litter as u_litter,unit_of_measurement.code as u_code,country_of_origin.litter as litter, country_of_origin.code as code,Product_card.numgtd as numgtd,Product_card.numrnpt as numrnpt,NDS.percent as percent,Product_card.numexcise as numexcise,Product_card.numegis as numegis" +
                              " from Type_to, Product_card, unit_of_measurement, country_of_origin, NDS" +
                              " where Type_to.id = Product_card.id_type  and Product_card.id_ed = unit_of_measurement.id and Product_card.id_coun = country_of_origin.id and" +
                               " Product_card.id_nds = NDS.id and Product_card.id = :code";
                    NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                    da8.SelectCommand.Parameters.AddWithValue("code", comboBox2.SelectedValue);
                    ds8.Reset();
                    da8.Fill(ds8);
                    dt8 = ds8.Tables[0];
                    // Вставка данных из DataGridView
                    if (dt8.Rows.Count > 0)

                    {// Проверяем, существует ли закладка
                     // Имя закладки соответствует имени столбца



                        table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 1, dt8.Columns.Count);
                        foreach (Word.Cell cell in table.Rows[1].Cells)
                        {
                            cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                        }

                        table.Cell(1, 1).Range.Text = "Код товара";
                        table.Cell(1, 2).Range.Text = "Название товара";
                        table.Cell(1, 3).Range.Text = "Тип";
                        table.Cell(1, 4).Range.Text = "Производитель";
                        table.Cell(1, 5).Range.Text = "Количество";
                        table.Cell(1, 6).Range.Text = "Базовая единица измерения";
                        table.Cell(1, 7).Range.Text = "код по ОКЕИ";
                        table.Cell(1, 8).Range.Text = "Страна производитель";
                        table.Cell(1, 9).Range.Text = "Код страны производителя";
                        table.Cell(1, 10).Range.Text = "Номер ГТД";
                        table.Cell(1, 11).Range.Text = "Номер РНПТ";
                        table.Cell(1, 12).Range.Text = "НДС";
                        table.Cell(1, 13).Range.Text = "Номер ставка акциза";
                        table.Cell(1, 14).Range.Text = "Номер ЕГАИС.";







                        Word.Row newRow = table.Rows.Add();
                        for (int j = 0; j < dt8.Columns.Count; j++)
                        {
                            // Получаем значение ячейки
                            var cellValue = dt8.Rows[0][j]?.ToString();
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
                        }


                        foreach (Word.Row row in table.Rows)
                        {
                            foreach (Word.Cell cell in row.Cells)
                            {
                                cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                            }
                        }

                        if (comboBox1.Text == "Склад не выбран")
                        {
                            // Добавляем заголовок
                            Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                            titleParagraph.Range.Text = "Данные о движениях товаров за промежуток от " + dateTimePicker1.Value.Date.ToString() + " по " + dateTimePicker2.Value.Date.ToString();
                            titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            titleParagraph.Range.Font.Size = 12;
                            titleParagraph.Range.InsertParagraphAfter();
                        }

                        else
                        {
                            // Добавляем заголовок
                            Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                            titleParagraph.Range.Text = "Данные о движениях товаров за промежуток от " + dateTimePicker1.Value.ToString() + " по " + dateTimePicker2.Value.ToString() + ". Склад: " + comboBox1.Text;
                            titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            titleParagraph.Range.Font.Size = 12;
                            titleParagraph.Range.InsertParagraphAfter();
                        }

                        // Создаем таблицу
                        table2 = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count);

                        // Добавляем заголовки столбцов
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            table2.Cell(1, i + 1).Range.Text = dataGridView.Columns[i].HeaderText;
                            table2.Cell(1, i + 1).Range.Font.Bold = 1; // Заголовок жирный
                            table2.Cell(1, i + 1).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(1, i + 1).Range.Font.Size = 8;
                        }

                        // Заполняем таблицу данными
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView.Columns.Count; j++)
                            {
                                table2.Cell(i + 2, j + 1).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                                table2.Cell(i + 2, j + 1).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                table2.Cell(i + 2, j + 1).Range.Font.Size = 8;
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
    
                      
        

    private void ExportToWord(DataGridView dataGridView, string filePath)
            {
                Word.Application wordApp = null;
                Word.Document wordDoc = null;
                Word.Table table = null;
           
            try
            {
                
                
            
                
        // Создаем новый экземпляр Word
        wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                if (comboBox1.Text == "Склад не выбран")
                {
                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Данные о движениях товаров  ";
                    titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;
                    titleParagraph.Range.InsertParagraphAfter();
                }
            
                else
                {
                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Данные о перемещениях товаров ";
                    titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;
                    titleParagraph.Range.InsertParagraphAfter();
                }
          

                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count);

                    // Добавляем заголовки столбцов
                    for (int i = 0; i < dataGridView.Columns.Count; i++)
                    {
                        table.Cell(1, i + 1).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, i + 1).Range.Font.Bold = 1; // Заголовок жирный
                    
                    table.Cell(1, i+1).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    table.Cell(1, i+1).Range.Font.Size = 8;
                }

                    // Заполняем таблицу данными
                 
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            table.Cell( 2, j + 1).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                     
                        table.Cell( 2, j + 1).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell( 2, j + 1).Range.Font.Size = 8;
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

                if (comboBox1.Text == "Склад не выбран")
                {
                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Данные о движениях товаров за промежуток от " + dateTimePicker1.Value.Date.ToString() + " по " + dateTimePicker2.Value.Date.ToString();
                    titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;
                    titleParagraph.Range.InsertParagraphAfter();
                }

                else
                {
                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Данные о перемещениях за промежуток от " + dateTimePicker1.Value.ToString() + " по " + dateTimePicker2.Value.ToString() + ". Склад: " + comboBox1.Text;
                    titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;
                    titleParagraph.Range.InsertParagraphAfter();
                }

                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count );

                // Добавляем заголовки столбцов
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    table.Cell(1, i+1).Range.Text = dataGridView.Columns[i].HeaderText;
                    table.Cell(1, i + 1).Range.Font.Bold = 1; // Заголовок жирный
                    table.Cell(1, i + 1).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    table.Cell(1, i + 1).Range.Font.Size = 8;
                }

                // Заполняем таблицу данными
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        table.Cell(i + 2, j + 1).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        table.Cell(i + 2, j + 1).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(i + 2, j + 1).Range.Font.Size = 8;
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
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
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
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
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
       





        
       

        private void вWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "Склад не выбран")
                //ExportToExcel(dataGridView1, filePath);
                {

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Word Files|*.docx";
                        saveFileDialog.Title = "Сохранить файл Word";
                        saveFileDialog.FileName = "invoices_" + comboBox1.Text + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWord(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
                else
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Word Files|*.docx";
                        saveFileDialog.Title = "Сохранить файл Word";
                        saveFileDialog.FileName = "invoices_"  + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWord(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
            }
            catch { }
            //ExportToWord(dataGridView1);
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "Склад не выбран")
                //ExportToExcel(dataGridView1, filePath);
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;

                        saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
                else
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;

                        saveFileDialog.FileName = "invoices_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
            }
            catch { }
        }

        private void вExcelИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "Склад не выбран")
                //ExportToExcel(dataGridView1, filePath);
                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                            string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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
                else
                {
                    if (dataGridView1.CurrentRow != null)
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                            string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            saveFileDialog.FileName = "invoices_" + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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


        private void вJSONИнформациюВсехТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "Склад не выбран")
                //ExportToExcel(dataGridView1, filePath);
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                        saveFileDialog.Title = "Сохраните файл JSON как";
                        saveFileDialog.FileName = $"invoices_{comboBox1.Text.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportJSON_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }


                else
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                        saveFileDialog.Title = "Сохраните файл JSON как";
                        saveFileDialog.FileName = $"invoices_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportJSON_all(dataGridView1, saveFileDialog.FileName);
                        }
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
                    if (comboBox1.Text != "Склад не выбран")
                    //ExportToExcel(dataGridView1, filePath);
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                            saveFileDialog.Title = "Сохраните файл JSON как";
                            string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            saveFileDialog.FileName = $"invoices__{comboBox1.Text.Replace(" ", "_")}_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Вызываем метод экспорта с выбранным путем
                                ExportToJSON(dataGridView1, saveFileDialog.FileName);
                            }
                        }
                    }
                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                            saveFileDialog.Title = "Сохраните файл JSON как";
                            string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            saveFileDialog.FileName = $"invoices_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Вызываем метод экспорта с выбранным путем
                                ExportToJSON(dataGridView1, saveFileDialog.FileName);
                            }
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
        private void вWordИнформациюВсехТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "Товар не выбран")
                {
                    if (comboBox1.Text != "Склад не выбран")
                    //ExportToExcel(dataGridView1, filePath);
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Word Files|*.docx";
                            saveFileDialog.Title = "Сохранить файл Word";
                            saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToWord_all(dataGridView1, saveFileDialog.FileName);
                            }
                        }
                    }

                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Word Files|*.docx";
                            saveFileDialog.Title = "Сохранить файл Word";
                            saveFileDialog.FileName = "invoices_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToWord_all(dataGridView1, saveFileDialog.FileName);
                            }
                        }
                    }
                }

                else
                {
                    if (comboBox1.Text != "Склад не выбран")
                    //ExportToExcel(dataGridView1, filePath);
                    {

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            saveFileDialog.Filter = "Word Files|*.docx";
                            saveFileDialog.Title = "Сохранить файл Word";
                            saveFileDialog.FileName = "invoices_"+ code.Replace(" ", "_")+"_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToWordProduct(dataGridView1, saveFileDialog.FileName);
                            }
                        }
                    }

                    else
                    {
                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            saveFileDialog.Filter = "Word Files|*.docx";
                            saveFileDialog.Title = "Сохранить файл Word";
                            saveFileDialog.FileName = "invoices_"+code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToWordProduct(dataGridView1, saveFileDialog.FileName);
                            }
                        }
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
                   
                        if (comboBox1.Text != "Склад не выбран")
                        //ExportToExcel(dataGridView1, filePath);
                        {
                            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                            {
                                saveFileDialog.Filter = "Word Files|*.docx";
                                saveFileDialog.Title = "Сохранить файл Word";
                                string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                                saveFileDialog.FileName = "invoices_" + comboBox1.Text.Replace(" ", "_") + "_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    ExportToWord(dataGridView1, saveFileDialog.FileName);
                                }
                            }
                        }


                        else
                        {
                            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                            {
                                saveFileDialog.Filter = "Word Files|*.docx";
                                saveFileDialog.Title = "Сохранить файл Word";
                                string code = (string)dataGridView1.CurrentRow.Cells[2].Value;
                                saveFileDialog.FileName = "invoices_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    ExportToWord(dataGridView1, saveFileDialog.FileName);
                                }
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
        private void InitializeProgressBar()
        {
            progressBar = new ProgressBar();
            progressBar.Location = new Point(200, 15); // Установите нужные координаты
            progressBar.Size = new Size(200, 30); // Установите нужный размер
            progressBar.Visible = false; // Скрываем его изначально
            this.Controls.Add(progressBar); // Добавляем ProgressBar на форму
        }

        private void вWordДанныеЖурналаУчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (comboBox1.Text == "Склад не выбран")
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
                                string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                                saveFileDialog.Filter = "Word Files|*.docx";
                                saveFileDialog.Title = "Сохранить файл Word";
                                saveFileDialog.FileName = "accounting_" + code + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    // Создаем и настраиваем BackgroundWorker
                                    BackgroundWorker worker = new BackgroundWorker();
                                    worker.WorkerReportsProgress = true;

                                    worker.DoWork += (s, args) =>
                                    {
                                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                                        // Создание экземпляра Word
                                        Word.Application wordApp = new Word.Application();
                                        // Создание экземпляра Word
                                        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "МХ-5.docx");

                                        // Указываем путь для копии документа
                                        string copyPath = Path.Combine(saveFileDialog.FileName);

                                        // Копируем файл
                                        File.Copy(templatePath, copyPath, true); // true - перезаписывает файл, если он существует

                                        // Открываем копию документа
                                        Word.Document wordDoc = wordApp.Documents.Open(copyPath);
                                        DateTime startDate = dateTimePicker1.Value.Date;
                                        DateTime endDate = dateTimePicker2.Value.Date;
                                        // Убедитесь, что endDate увеличивается на один день, чтобы включить всю дату
                                        endDate = endDate.AddDays(1).AddTicks(-1); // Устанавливаем время на конец дня
                                        // Делаем приложение видимым (по желанию)
                                        wordApp.Visible = true;

                                        String sql1 = "SELECT " +
                 " CONCAT('  ',organization.name_f, ' , ИНН: ',organization.INN , ' , КПП: ',organization.KPP, ' , ОГРН: ',organization.OGRN  ) AS recipient," +

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
"    storehouse s ON i.id_storehouse = s.id  where  i.flag = 0  and s.id_div = " + this.div.ToString() + " and i.shipment >='" + startDate + "' and i.shipment <='" + endDate + "'  GROUP BY i.id, i.num_invoices, i.shipment, s.name, i.flag " +


"UNION ALL " +



"SELECT" +
" m.id AS id_moving_number, " +

"   m.num_invoices AS invoice_number,           " +
"   m.shipment AS shipment_date,           " +
"   SUM(mi.quantity) AS quantity,                  " +
"   SUM(mi.count) AS count,                  " +
"  s2.name AS storehouse_name,             " +
"   'Перемещение на склад' AS invoice_type, " +
"    m.status AS status,    " +
"    m.shipment_to AS shipment    " +
"   " +
"FROM " +
"   moving_info mi " +
"JOIN " +
"  moving m ON mi.invoices_in = m.id " +

"JOIN " +
"   storehouse s2 ON m.id_storehouse_2 = s2.id   where  s2.id_div = " + this.div.ToString() + "   and m.shipment_to <= '" + startDate +  "   and m.shipment_to <= '" + endDate + "'  GROUP BY m.id, m.num_invoices, m.shipment, s2.name  ORDER BY shipment_date DESC";

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
                                            String sql8 = "Select row_number() over (partition by prod_storehouse_info.invoices_in order by prod_storehouse_info.id) as row_n,prod_storehouse_info.date_add, CONCAT('  ',Product_card.code,' , ',Product_card.name,' , номер партии:', batch_number.number, ' , ' ,Product_card.name_firm,' , ', Product_card.code) as num_pro, unit_of_measurement.litter as litter,Firm.name_f, moving_info.quantity as col_pro, batch_number.price as price,batch_number.price*moving_info.quantity as sum  from Product_card,batch_number,unit_of_measurement,moving_info,moving where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and moving.id =moving_info.invoices_in and batch_number.id=moving_info.id_batch_number  and moving.id=:id ORDER BY moving_info.id ASC;";
                                            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                                            da8.SelectCommand.Parameters.AddWithValue("id", id);
                                            ds8.Reset();
                                            da8.Fill(ds8);
                                            dt8 = ds8.Tables[0];
                                            // Вставка данных из DataGridView
                                            if (dt8.Rows.Count > 0)

                                            {// Проверяем, существует ли закладка
                                                string bookmarkName = "table"; // Имя закладки соответствует имени столбца
                                                if (wordDoc.Bookmarks.Exists(bookmarkName))
                                                {
                                                    // Получаем закладку
                                                    Word.Bookmark bookmark = wordDoc.Bookmarks[bookmarkName];

                                                    // Вставляем таблицу в место закладки
                                                    Word.Range range = bookmark.Range; // Создаем новый параграф для установки позиции таблицы
                                                                                       //Word.Paragraph para = wordDoc.Content.Paragraphs.Add();
                                                                                       //para.Range.InsertParagraphAfter(); // Добавляем пустой параграф

                                                    //Word.Table table = wordDoc.Tables.Add(range, 1, 3); // 1 строка, 3 столбца
                                                    //// Устанавливаем отступы для параграфа
                                                    //para.LeftIndent = 28.35f; // 1 см от левого края
                                                    //para.SpaceBefore = 28.5f; // 10 см от верхнего края (10 см = 283.5 пунктов)
                                                    Word.Table table = wordDoc.Tables.Add(range, 4, 11);
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
                                                    foreach (Word.Cell cell in table.Rows[4].Cells)
                                                    {
                                                        cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
                                                        cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                    }

                                                    //// Высота строк шапки (опционально)
                                                    //for (int i = 1; i <= 3; i++)
                                                    //{
                                                    //    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                    //     cell.Range.Font.Size = 8; // Устанавливаем размер шрифта

                                                    //    //table.Rows[i].HeightRule = Word.WdRowHeightRule.wdRowHeightExactly;
                                                    //    //table.Rows[i].Height = wordApp.CentimetersToPoints(0.7f);
                                                    //}

                                                    // 1-я строка шапки
                                                    // "По учетным ценам" занимает 10-11 столбцы
                                                    table.Cell(1, 10).Merge(table.Cell(1, 11));
                                                    table.Cell(1, 10).Range.Text = "По учетным ценам";
                                                    // "Товар, тара" занимает 1-3 столбец
                                                    // "Отпущено" занимает 5-9 столбцы
                                                    table.Cell(1, 6).Merge(table.Cell(1, 9));
                                                    table.Cell(1, 6).Range.Text = "Отпущено";
                                                    // "Ед. изм." занимает 4-й столбец
                                                    table.Cell(1, 4).Merge(table.Cell(1, 5));
                                                    table.Cell(1, 4).Range.Text = "Ед. изм.";

                                                    table.Cell(1, 3).Range.Text = "Сорт";
                                                    table.Cell(1, 1).Merge(table.Cell(1, 2));
                                                    table.Cell(1, 1).Range.Text = "Товар, тара";



                                                    // "сумма, руб. коп." - 11 столбец
                                                    table.Cell(2, 11).Range.Text = "сумма, руб. коп.";
                                                    // В "По учетным ценам" (10-11) разбиваем на:
                                                    // "цена, руб. коп." - 10 столбец
                                                    table.Cell(2, 10).Range.Text = "цена, руб. коп.";


                                                    // 2-я строка шапки
                                                    // Внутри "Товар, тара" разбиваем на две части:
                                                    // "наименование, характеристика" занимает 1-2 столбец
                                                    table.Cell(2, 8).Merge(table.Cell(2, 9));
                                                    table.Cell(2, 8).Range.Text = "масса";
                                                    table.Cell(2, 6).Merge(table.Cell(2, 7));
                                                    table.Cell(2, 6).Range.Text = "количество";
                                                    table.Cell(2, 5).Range.Text = "код по ОКЕИ";
                                                    // В "Отпущено" (5-9) разбиваем на три части:
                                                    // "наименование" - 5 столбец
                                                    table.Cell(2, 4).Range.Text = "наименование";
                                                    // "Сорт" - 4 столбец
                                                    //table.Cell(2, 3).Range.Text = "Сорт";
                                                    //table.Cell(2, 1).Merge(table.Cell(2, 2));
                                                    // "код" - 3 столбец
                                                    table.Cell(2, 2).Range.Text = "код";
                                                    table.Cell(2, 1).Range.Text = "наименование, характеристика";




                                                    // "код по ОКЕИ" - 6 столбец

                                                    // "количество" и "масса" объединены в 7-9 столбцы


                                                    // 3-я строка шапки
                                                    // Нумерация колонок

                                                    // Дополнительно, если нужно разделить "количество" и "масса" во 2-й строке более детально, можно сделать так:

                                                    // Разбиваем 7-9 столбцы 3-й строки на подзаголовки (если нужны)
                                                    table.Cell(3, 6).Range.Text = "в одном месте";
                                                    table.Cell(3, 7).Range.Text = "мест,в штуках";
                                                    table.Cell(3, 8).Range.Text = "брутто";
                                                    table.Cell(3, 9).Range.Text = "нетто";
                                                    // Можно сместить нумерацию в 4-й строке, если нужно (в примере 3 строки шапки + 4-я с данными)

                                                    // Настройка выравнивания текста в заголовках
                                                    string[] colNumbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" };
                                                    for (int i = 0; i < colNumbers.Length; i++)
                                                    {
                                                        table.Cell(4, i + 1).Range.Text = colNumbers[i];
                                                    }
                                                    //table.Cell(2, 1).Merge(table.Cell(3, 1));
                                                    //table.Cell(2, 2).Merge(table.Cell(3, 2));
                                                    //table.Cell(1, 3).Merge(table.Cell(3, 3));
                                                    //table.Cell(2, 5).Merge(table.Cell(3, 5));
                                                    //table.Cell(2, 4).Merge(table.Cell(3, 4));
                                                    //table.Cell(2, 10).Merge(table.Cell(3, 10));
                                                    //table.Cell(2, 11).Merge(table.Cell(3, 11));

                                                    foreach (Word.Row row in table.Rows)
                                                    {
                                                        foreach (Word.Cell cell in row.Cells)
                                                        {
                                                            cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                                            cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                                            //cell.Range.Font.Bold = 1;
                                                            //cell.Range.Font.Size = 9;
                                                        }
                                                    }

                                                    // Пример добавления строки с данными (4-я строка)
                                                    // Можно добавлять данные начиная с 4-й строки
                                                    //table.Rows.Add();
                                                    //Word.Row dataRow = table.Rows[table.Rows.Count];
                                                    //for (int i = 1; i <= 11; i++)
                                                    //{
                                                    //    dataRow.Cells[i].Range.Text = $"Данные {i}";
                                                    //}

                                                    // Сохранение файла (при необходимости)
                                                    // string filePath = @"C:\temp\Таблица.docx";
                                                    // doc.SaveAs2(filePath);

                                                    // wordApp.Quit(); // Если нужно закрыть Word автоматически

                                                    //    table.Cell(1, 1).Range.Text = "Номер по порядку";
                                                    //    table.Cell(1, 2).Range.Text = "Товар";
                                                    //    table.Cell(1, 3).Range.Text = "Товар";
                                                    //    table.Cell(1, 4).Range.Text = "Единица измерения";
                                                    //    table.Cell(1, 5).Range.Text = "Единица измерения";
                                                    //    table.Cell(1, 6).Range.Text = "Количество";

                                                    //    table.Cell(2, 1).Range.Text = "";
                                                    //    table.Cell(2, 2).Range.Text = "наименование, характеристика, сорт, артикул товара";
                                                    //    table.Cell(2, 3).Range.Text = "код";
                                                    //    table.Cell(2, 4).Range.Text = "наименование";
                                                    //    table.Cell(2, 5).Range.Text = "код по ОКЕИ";
                                                    //    table.Cell(2, 6).Range.Text = "мест,штук";

                                                    //// Объединяем ячейки в первой строке
                                                    //Word.Range cellRange = table.Cell(1, 1).Range;
                                                    //    cellRange.End = table.Cell(1, 2).Range.End; // Устанавливаем конец диапазона на конец третьей ячейки


                                                    //// Настройка внешнего вида таблицы (например, шрифт, размеры и т.д.)
                                                    //table.Borders.Enable = 1; // Включаем рамки
                                                    //                          // Дополнительные настройки можно добавить здесь
                                                    //                          //Word.Table table = wordDoc.Tables[3]; // Получаем первую таблицу (индексация с 1)
                                                    //                          //foreach (Word.Cell cell in table.Rows[4].Cells)
                                                    //                          //{
                                                    //                          //    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                    //                          //    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                    //                          //}
                                                    //                          //table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
                                                    int k = 0;
                                                    for (int i = 0; i < dt8.Rows.Count; i++)
                                                    {
                                                        int h = 0;
                                                        Word.Row newRow = table.Rows.Add();
                                                        for (int j = 0; j < dt8.Columns.Count + 4; j++)
                                                        {
                                                            if (j != 2 && j != 5 && j != 7 && j != 8)
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
                                                    String sql200 = "Select SUM(moving_info.quantity) as total_col,SUM(batch_number.price) as total_sum_nds from  moving,moving_info, batch_number,Product_card where batch_number.id = moving_info.id_batch_number and moving.id = moving_info.invoices_in  and moving.id = " + id + " GROUP BY moving.id";


                                                    NpgsqlDataAdapter da200 = new NpgsqlDataAdapter(sql200, con);
                                                    da200.SelectCommand.Parameters.AddWithValue("id", id);
                                                    ds200.Reset();
                                                    da200.Fill(ds200);
                                                    dt200 = ds200.Tables[0];
                                                    // Вставка данных из DataGridView
                                                    if (dt200.Rows.Count > 0)
                                                    {

                                                        int t = 7;
                                                        Word.Row newRow = table.Rows.Add();
                                                        Word.Row newRow2 = table.Rows.Add();

                                                        // Получаем значение ячейки
                                                        var cellValue = dt200.Rows[0][0]?.ToString();
                                                        newRow.Cells[t].Range.Text = cellValue;
                                                        newRow.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                        newRow.Cells[t].Range.Font.Size = 8;
                                                        newRow2.Cells[t].Range.Text = cellValue;
                                                        newRow2.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                        newRow2.Cells[t].Range.Font.Size = 8;
                                                        t = 11;
                                                        var cellValue2 = dt200.Rows[0][1]?.ToString();
                                                        newRow.Cells[t].Range.Text = cellValue2;
                                                        newRow.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                        newRow.Cells[t].Range.Font.Size = 8;
                                                        newRow2.Cells[t].Range.Text = cellValue2;
                                                        newRow2.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                        newRow2.Cells[t].Range.Font.Size = 8;
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


                                                        newRow.Cells[1].Merge(newRow.Cells[6]);
                                                        newRow2.Cells[1].Merge(newRow2.Cells[6]);
                                                        newRow.Cells[1].Range.Text = "Итого";
                                                        newRow2.Cells[1].Range.Text = "Всего по накладной";
                                                        newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                                                        newRow2.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                                                        string bookmarkName1 = dt200.Columns[1].ColumnName; // Пример имени закладки

                                                        if (wordDoc.Bookmarks.Exists(bookmarkName1))
                                                        {
                                                            wordDoc.Bookmarks[bookmarkName1].Range.Text = cellValue2;
                                                        }
                                                        wordDoc.Bookmarks["vid_d_1"].Range.Text = "хранение";
                                                        wordDoc.Bookmarks["vid_d_2"].Range.Text = "хранение";
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

                                            MessageBox.Show("Приходная накладная не найдена.");
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
    }
    }

    

