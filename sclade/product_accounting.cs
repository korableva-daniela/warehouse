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
    public partial class product_accounting : Form
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
        private ProgressBar progressBar;
        DateTime shipment;
        DateTime shipment_to;
        public int div;
        DataTable dt30 = new DataTable();
        DataSet ds30 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt200 = new DataTable();
        DataSet ds200 = new DataSet();
        public product_accounting(NpgsqlConnection con, int stor, string code, int id_em, int pro,int div)
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
        public void Update_filt()
        {


            if ((comboBox1.Text != "Склад не выбран" && comboBox2.Text == "Товар не выбран"))
            {


                String sql1 = "Select prod_storehouse.id, prod_storehouse.id_store, prod_storehouse.num_place, SUM(prod_storehouse_info.count),storehouse.name as storehouse_name from prod_storehouse, prod_storehouse_info,storehouse where prod_storehouse.id_store = storehouse.id and prod_storehouse_info.id_prod_storehouse = prod_storehouse.id and prod_storehouse.id_store= " + this.stor + " Group by prod_storehouse.id,storehouse.name Order by prod_storehouse.num_place";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                ds.Reset();
                da.Fill(ds);

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].HeaderText = "Номер полки";
                    dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[4].HeaderText = "Склад";
                }

                else
                {
                    MessageBox.Show("Склад пустой.");

                    var originalTable = (DataTable)dataGridView1.DataSource;
                    dataGridView1.DataSource = null; // Очищаем DataSource
                    dataGridView1.DataSource = originalTable.Clone();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].HeaderText = "Номер полки";
                    dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[4].HeaderText = "Склад";

                    var originalTable1 = (DataTable)dataGridView2.DataSource;
                    dataGridView2.DataSource = null; // Очищаем DataSource
                    dataGridView2.DataSource = originalTable1.Clone();
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].Visible = false;
                    dataGridView2.Columns[3].HeaderText = "Номер партии";
                    dataGridView2.Columns[4].HeaderText = "Код товара";
                    dataGridView2.Columns[5].HeaderText = "Название товара";
                    dataGridView2.Columns[6].HeaderText = "Производитель";
                    dataGridView2.Columns[7].HeaderText = "Единица измерения";
                    dataGridView2.Columns[8].HeaderText = "Количество";
                    dataGridView2.Columns[9].HeaderText = "Дата размещения";
                    dataGridView2.Columns[10].HeaderText = "Сотрудник, который положил товар на полку";
                }
            }
            else if ((comboBox1.Text == "Склад не выбран" && comboBox2.Text == "Товар не выбран"))
            {

                Update();

            }
            else if ((comboBox1.Text == "Склад не выбран" && comboBox2.Text != "Товар не выбран"))
            {

                String sql1 = "Select prod_storehouse.id, prod_storehouse.id_store, prod_storehouse.num_place, SUM(prod_storehouse_info.count),storehouse.name as storehouse_name from prod_storehouse, prod_storehouse_info,storehouse where prod_storehouse.id_store = storehouse.id and prod_storehouse_info.id_prod_storehouse = prod_storehouse.id and prod_storehouse_info.id_product_card= " + this.pro + "  and storehouse.id_div = " + this.div.ToString() + "  Group by prod_storehouse.id,storehouse.name Order by prod_storehouse.num_place";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                ds.Reset();
                da.Fill(ds);

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].HeaderText = "Номер полки";
                    dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[4].HeaderText = "Склад";
                }
                else
                {
                    MessageBox.Show("Товар отсутствует на складах");

                    var originalTable = (DataTable)dataGridView1.DataSource;
                    dataGridView1.DataSource = null; // Очищаем DataSource
                    dataGridView1.DataSource = originalTable.Clone();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].HeaderText = "Номер полки";
                    dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[4].HeaderText = "Склад";
                    var originalTable1 = (DataTable)dataGridView2.DataSource;
                    dataGridView2.DataSource = null; // Очищаем DataSource
                    dataGridView2.DataSource = originalTable1.Clone();
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].Visible = false;
                    dataGridView2.Columns[3].HeaderText = "Номер партии";
                    dataGridView2.Columns[4].HeaderText = "Код товара";
                    dataGridView2.Columns[5].HeaderText = "Название товара";
                    dataGridView2.Columns[6].HeaderText = "Производитель";
                    dataGridView2.Columns[7].HeaderText = "Единица измерения";
                    dataGridView2.Columns[8].HeaderText = "Количество";
                    dataGridView2.Columns[9].HeaderText = "Дата размещения";
                    dataGridView2.Columns[10].HeaderText = "Сотрудник, который положил товар на полку";
                }
            }
            else if ((comboBox1.Text != "Склад не выбран" && comboBox2.Text != "Товар не выбран"))
            {

                String sql1 = "Select prod_storehouse.id, prod_storehouse.id_store, prod_storehouse.num_place, SUM(prod_storehouse_info.count),storehouse.name as storehouse_name from prod_storehouse, prod_storehouse_info,storehouse where prod_storehouse.id_store=storehouse.id and prod_storehouse.id_store = " + this.stor + " and prod_storehouse_info.id_prod_storehouse = prod_storehouse.id and prod_storehouse_info.id_product_card= " + this.pro + " Group by prod_storehouse.id,storehouse.name Order by prod_storehouse.num_place";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                ds.Reset();
                da.Fill(ds);

                dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].HeaderText = "Номер полки";
                    dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[4].HeaderText = "Склад";
                }
                else
                {
                    MessageBox.Show("Товар отсутствует на складе");

                    var originalTable = (DataTable)dataGridView1.DataSource;
                    dataGridView1.DataSource = null; // Очищаем DataSource
                    dataGridView1.DataSource = originalTable.Clone();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;

                    dataGridView1.Columns[2].HeaderText = "Номер полки";
                    dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[4].HeaderText = "Склад";

                    var originalTable1 = (DataTable)dataGridView2.DataSource;
                    dataGridView2.DataSource = null; // Очищаем DataSource
                    dataGridView2.DataSource = originalTable1.Clone();
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].Visible = false;
                    dataGridView2.Columns[3].HeaderText = "Номер партии";
                    dataGridView2.Columns[4].HeaderText = "Код товара";
                    dataGridView2.Columns[5].HeaderText = "Название товара";
                    dataGridView2.Columns[6].HeaderText = "Производитель";
                    dataGridView2.Columns[7].HeaderText = "Единица измерения";
                    dataGridView2.Columns[8].HeaderText = "Количество";
                    dataGridView2.Columns[9].HeaderText = "Дата размещения";
                    dataGridView2.Columns[10].HeaderText = "Сотрудник, который положил товар на полку";
                }
            }

        
           
        }
        private void product_accounting_Load(object sender, EventArgs e)
        {
            try
            {
                
  
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);

                comboBox1.Font = new Font("Arial", 11);
                comboBox2.Font = new Font("Arial", 11);

                Update();
                dataGridView1.ReadOnly = true;
                Update_filt();

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
                    Update_filt();
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
                    fp.ShowDialog();
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
                    fp.ShowDialog();
                }
            }
            catch { }
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
                        
                            
                                String sql1 = "Select * from prod_storehouse where id = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
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
                }

                else id = dataGridView1.RowCount;
                updateinvoices_in_info(id);
            }
            catch { }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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
                        Update_filt();

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
                        Update_filt();

                    }

                }
              
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
        private void button6_Click(object sender, EventArgs e)
        {
            updateProduct_cardinfo(-1);
            comboBox2.Text = "Товар не выбран";
            this.pro = -1;
            Update_filt();
        }

        public void updateinvoices_in_info(int id)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                   
                    
                       
                            String sqli = "Select prod_storehouse_info.id, prod_storehouse.id,prod_storehouse.num_place,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter,prod_storehouse_info.count,prod_storehouse_info.date_add, Employee.name from Employee, Product_card,batch_number,unit_of_measurement,prod_storehouse_info,prod_storehouse where Employee.id = prod_storehouse_info.id_Employee and batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and prod_storehouse.id =prod_storehouse_info.id_prod_storehouse and batch_number.id=prod_storehouse_info.id_batch_number and prod_storehouse_info.count >0 and prod_storehouse.id=:id ORDER BY prod_storehouse_info.id ASC;";

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
                            dataGridView2.Columns[9].HeaderText = "Дата размещения";
                    dataGridView2.Columns[10].HeaderText = "Сотрудник, который положил товар на полку";



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
                if (this.pro != -1)
                {
                    updateProduct_cardinfo(this.pro);

                }
                else
                {
                    comboBox2.Text = "Товар не выбран";
                }


                label1.Font = new Font("Arial", 11);
           ;
                //label2.Font = new Font("Arial", 11);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.Font = new Font("Arial", 9);

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                //label5.Visible = false;
                comboBox1.Font = new Font("Arial", 11);
                comboBox1.Enabled = false;
                comboBox2.Font = new Font("Arial", 11);
                comboBox2.Enabled = false;
                //comboBox3.Font = new Font("Arial", 11);


                //comboBox3.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                //comboBox3.Enabled = true; // Сделать ComboBox доступным для выбора
                //comboBox3.Font = new Font("Arial", 11);
                //comboBox3.Text = "Типы накладных";

                dataGridView2.ContextMenuStrip = contextMenuStrip2;
                try
                {
                    //if ((this.stor != -1) & (this.pro != -1) & (shipment != dateTimePicker1.MinDate) & (shipment_to != dateTimePicker2.MaxDate))
                    //{

                    //}
                    //        if ((this.stor != -1) & (this.pro != -1) & (shipment != dateTimePicker1.MinDate) & (shipment_to != dateTimePicker2.MaxDate))
                    //{
                    String sql1 = "Select prod_storehouse.id, prod_storehouse.id_store, prod_storehouse.num_place, SUM(prod_storehouse_info.count),storehouse.name as storehouse_name from prod_storehouse, prod_storehouse_info,storehouse where prod_storehouse.id_store = storehouse.id and prod_storehouse_info.id_prod_storehouse = prod_storehouse.id   and storehouse.id_div = " + this.div.ToString() + "   Group by prod_storehouse.id,storehouse.name Order by prod_storehouse.num_place";

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                    ds.Reset();
                    da.Fill(ds);

                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Visible = false;
                      
                        dataGridView1.Columns[2].HeaderText = "Номер полки";
                        dataGridView1.Columns[3].HeaderText = "Общее количество товара";
                        dataGridView1.Columns[4].HeaderText = "Склад";
                    }

                }
                catch { }


            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updatestorehouseinfo(-1);
            comboBox1.Text = "Склад не выбран";
            this.stor = -1;
            Update_filt();
        }
        private void InitializeProgressBar()
        {
            progressBar = new ProgressBar();
            progressBar.Location = new Point(200, 15); // Установите нужные координаты
            progressBar.Size = new Size(200, 30); // Установите нужный размер
            progressBar.Visible = false; // Скрываем его изначально
            this.Controls.Add(progressBar); // Добавляем ProgressBar на форму
        }

        private void выгрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void выгрузитьДанныеВЖурналУчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////try
            ////{
            //    if (dataGridView1.CurrentRow != null)
            //    {
            //        if (comboBox1.Text == "Склад не выбран")
            //        {
            //            MessageBox.Show("Пожалуйста, выберите склад.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        else
            //        {
            //            String sql30 = "Select * from organization ";
            //            NpgsqlDataAdapter da30 = new NpgsqlDataAdapter(sql30, con);
            //            ds30.Reset();
            //            da30.Fill(ds30);
            //            dt30 = ds30.Tables[0];
            //            if (dt30.Rows.Count > 0)
            //            {
            //                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            //                {

            //                    saveFileDialog.Filter = "Word Files|*.docx";
            //                    saveFileDialog.Title = "Сохранить файл Word";
            //                    saveFileDialog.FileName = "accounting_" +  "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

            //                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //                    {
            //                        // Создаем и настраиваем BackgroundWorker
            //                        BackgroundWorker worker = new BackgroundWorker();
            //                        worker.WorkerReportsProgress = true;

            //                        worker.DoWork += (s, args) =>
            //                        {
            //                            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            //                            // Создание экземпляра Word
            //                            Word.Application wordApp = new Word.Application();
            //                            // Создание экземпляра Word
            //                            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "MX-5.docx");

            //                            // Указываем путь для копии документа
            //                            string copyPath = Path.Combine(saveFileDialog.FileName);

            //                            // Копируем файл
            //                            File.Copy(templatePath, copyPath, true); // true - перезаписывает файл, если он существует

            //                            // Открываем копию документа
            //                            Word.Document wordDoc = wordApp.Documents.Open(copyPath);

            //                            // Делаем приложение видимым (по желанию)
            //                            wordApp.Visible = true;

            //                            String sql1 = "SELECT " +
            //     " CONCAT('  ',organization.name_f, ' , ИНН: ',organization.INN , ' , КПП: ',organization.KPP, ' , ОГРН: ',organization.OGRN  ) AS recipient," +
            //     "  CONCAT('Склад:  ', storehouse1.name, ' , Подразделение: ', Division1.name, ' , адрес: ', storehouse1.country_d, ' , ', storehouse1.city_d, ' ,  ', storehouse1.street_d, ' ,  ', storehouse1.house_d, ' , ', storehouse1.post_in_d) AS sclade_1, " +

            //      "  prod_storehouse.num_place AS worh" +


            //    " FROM prod_storehouse JOIN  storehouse AS storehouse1 ON storehouse1.id = prod_storehouse.id_store" +
            //                            " JOIN Division AS Division1 ON storehouse1.id_div = Division1.id" +
            //    " JOIN organization ON organization.id=1" +
            //    " WHERE  prod_storehouse.id =  " + id ;
            //                            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter(sql1, con);
            //                            ds7.Reset();
            //                            da7.Fill(ds7);
            //                            dt7 = ds7.Tables[0];
            //                            if (dt7.Rows.Count > 0)
            //                            {
            //                                for (int j = 0; j < dt7.Columns.Count; j++)
            //                                {
            //                                    // Получаем значение ячейки
            //                                    var cellValue = dt7.Rows[0][j]?.ToString();

            //                                    // Заменяем закладки в документе
            //                                    string bookmarkName = dt7.Columns[j].ColumnName; // Пример имени закладки

            //                                    if (wordDoc.Bookmarks.Exists(bookmarkName))
            //                                    {
            //                                        wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue;
            //                                    }

            //                                    // Отправляем информацию о прогрессе
            //                                    int progressPercentage = (int)((j + 1) / (double)dt7.Columns.Count * 100);
            //                                    worker.ReportProgress(progressPercentage);
            //                                }

            //                            String sql8 = "Select row_number() over (partition by prod_storehouse_info.id_prod_storehouse  order by prod_storehouse_info.id) as row_n,prod_storehouse_info.date_add, CONCAT('  ',Product_card.code,' , ',Product_card.name,' , номер партии:', batch_number.number, ' , ' ,Product_card.name_firm,' , ', Product_card.code) as num_pro, unit_of_measurement.litter as litter,Firm.name_f, prod_storehouse_info.count as col_pro, batch_number.price as price,batch_number.price*prod_storehouse_info.count as sum  from Firm,Product_card,batch_number,unit_of_measurement,prod_storehouse_info,prod_storehouse,storehouse where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and prod_storehouse.id =prod_storehouse_info.id_prod_storehouse  and batch_number.id=prod_storehouse_info.id_batch_number and batch_number.id_Firm = Firm.id  and prod_storehouse.id= " + id + " and storehouse.id=prod_storehouse.id_store and prod_storehouse_info.count>0 ORDER BY prod_storehouse_info.id ASC;";
            //                            NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
            //                            da8.SelectCommand.Parameters.AddWithValue("id", id);
            //                            ds8.Reset();
            //                            da8.Fill(ds8);
            //                            dt8 = ds8.Tables[0];
            //                            // Вставка данных из DataGridView
            //                            if (dt8.Rows.Count > 0)

            //                            {// Проверяем, существует ли закладка
            //                                string bookmarkName = "table"; // Имя закладки соответствует имени столбца
            //                                if (wordDoc.Bookmarks.Exists(bookmarkName))
            //                                {
            //                                    // Получаем закладку
            //                                    Word.Bookmark bookmark = wordDoc.Bookmarks[bookmarkName];

            //                                    Word.Range range = bookmark.Range; 
            //                                    Word.Table table = wordDoc.Tables.Add(range, 3, 10);
            //                                        foreach (Word.Cell cell in table.Rows[1].Cells)
            //                                        {
            //                                            cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
            //                                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
            //                                        }
            //                                        foreach (Word.Cell cell in table.Rows[2].Cells)
            //                                        {
            //                                            cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
            //                                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
            //                                        }
            //                                        foreach (Word.Cell cell in table.Rows[3].Cells)
            //                                        {
            //                                            cell.Range.Font.Name = "Verdana"; // Устанавливаем шрифт
            //                                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
            //                                        }


            //                                        table.Cell(1, 6).Merge(table.Cell(1, 7));
            //                                    table.Cell(1, 6).Range.Text = "Товарный  документ";



            //                                    // "сумма, руб. коп." - 11 столбец
            //                                    table.Cell(2, 1).Range.Text = "Номер по порядку";

            //                                    table.Cell(2, 2).Range.Text = "Дата";



            //                                    table.Cell(2, 3).Range.Text = "Продукция, товарно - материальные ценности";

            //                                    table.Cell(2, 4).Range.Text = "Единица измерения";
            //                                    table.Cell(2, 5).Range.Text = "Поставщик (грузоотправитель)";

            //                                    table.Cell(2, 6).Range.Text = "Номер";
            //                                    table.Cell(2, 7).Range.Text = "Дата";
            //                                    table.Cell(2, 8).Range.Text = "Количество";
            //                                    table.Cell(2, 9).Range.Text = "Цена, руб.коп.";
            //                                    table.Cell(2, 10).Range.Text = "Сумма,руб.коп.";

            //                                    // Настройка выравнивания текста в заголовках
            //                                    string[] colNumbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            //                                    for (int i = 0; i < colNumbers.Length; i++)
            //                                    {
            //                                        table.Cell(3, i + 1).Range.Text = colNumbers[i];
            //                                    }


            //                                    foreach (Word.Row row in table.Rows)
            //                                    {
            //                                        foreach (Word.Cell cell in row.Cells)
            //                                        {
            //                                            cell.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //                                            cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            //                                        }
            //                                    }


            //                                    int k = 0;
            //                                    for (int i = 0; i < dt8.Rows.Count; i++)
            //                                    {
            //                                        int h = 0;
            //                                        Word.Row newRow = table.Rows.Add();
            //                                        for (int j = 0; j < dt8.Columns.Count + 2; j++)
            //                                        {
            //                                            if ( j != 5 && j != 6 )
            //                                            {
            //                                                // Получаем значение ячейки
            //                                                var cellValue = dt8.Rows[i][h]?.ToString();
            //                                                newRow.Cells[j + 1].Range.Text = cellValue;
            //                                                newRow.Cells[j + 1].Range.Font.Name = "Arial"; // Устанавливаем шрифт
            //                                                newRow.Cells[j + 1].Range.Font.Size = 8;
            //                                                //if (wordDoc.Bookmarks.Exists(bookmarkName))
            //                                                //{
            //                                                //    wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue; // Вставляем значение в закладку
            //                                                //}

            //                                                ////Заменяем закладки в документе
            //                                                //string bookmarkName_pro = dt8.Columns[j].ColumnName; // Пример имени закладки
            //                                                //if (wordDoc.Bookmarks.Exists(bookmarkName_pro))
            //                                                //{
            //                                                //    wordDoc.Bookmarks[bookmarkName_pro].Range.Text = cellValue;
            //                                                //    //newRow.Cells[j + 1].Range.Text = cellValue;
            //                                                //}
            //                                                h++;
            //                                            }


            //                                        }
            //                                        k = i;
            //                                    }
            //                                    String sql200 = "Select SUM(prod_storehouse_info.count) as total_col,SUM(prod_storehouse_info.count*batch_number.price) as total_sum_nds from prod_storehouse,prod_storehouse_info , batch_number where batch_number.id = prod_storehouse_info.id_batch_number and prod_storehouse.id = prod_storehouse_info.id_prod_storehouse and prod_storehouse.id = "+id+" GROUP BY prod_storehouse.id";


            //                                    NpgsqlDataAdapter da200 = new NpgsqlDataAdapter(sql200, con);
            //                                    da200.SelectCommand.Parameters.AddWithValue("id", id);
            //                                    ds200.Reset();
            //                                    da200.Fill(ds200);
            //                                    dt200 = ds200.Tables[0];
            //                                    // Вставка данных из DataGridView
            //                                    if (dt200.Rows.Count > 0)
            //                                    {

            //                                        int t = 8;
            //                                        Word.Row newRow = table.Rows.Add();


            //                                        // Получаем значение ячейки
            //                                        var cellValue = dt200.Rows[0][0]?.ToString();
            //                                        newRow.Cells[t].Range.Text = cellValue;
            //                                        newRow.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
            //                                        newRow.Cells[t].Range.Font.Size = 8;

            //                                        t = 10;
            //                                        var cellValue2 = dt200.Rows[0][1]?.ToString();
            //                                        newRow.Cells[t].Range.Text = cellValue2;
            //                                        newRow.Cells[t].Range.Font.Name = "Arial"; // Устанавливаем шрифт
            //                                        newRow.Cells[t].Range.Font.Size = 8;

            //                                        //if (wordDoc.Bookmarks.Exists(bookmarkName))
            //                                        //{
            //                                        //    wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue; // Вставляем значение в закладку
            //                                        //}

            //                                        ////Заменяем закладки в документе
            //                                        //string bookmarkName_pro = dt8.Columns[j].ColumnName; // Пример имени закладки
            //                                        //if (wordDoc.Bookmarks.Exists(bookmarkName_pro))
            //                                        //{
            //                                        //    wordDoc.Bookmarks[bookmarkName_pro].Range.Text = cellValue;
            //                                        //    //newRow.Cells[j + 1].Range.Text = cellValue;


            //                                        newRow.Cells[1].Merge(newRow.Cells[7]);

            //                                        newRow.Cells[1].Range.Text = "Итого";

            //                                        newRow.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;

            //                                        string bookmarkName1 = dt200.Columns[1].ColumnName; // Пример имени закладки


            //                                    }
            //                                    table.Borders.Enable = 1; // Включаем рамки для всей таблицы
            //                                    foreach (Word.Row row in table.Rows)
            //                                    {
            //                                        foreach (Word.Cell cell in row.Cells)
            //                                        {
            //                                            cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }





            //                            else
            //                            {

            //                                MessageBox.Show("Приходная накладная не найдена.");
            //                            }
            //                            // Вставка данных из DataGridView
            //                            //for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //                            //{
            //                            //    // Получаем значение ячейки
            //                            //    var cellValue = dataGridView1.CurrentRow.Cells[j].Value?.ToString();

            //                            //    // Заменяем закладки в документе
            //                            //    string bookmarkName = dataGridView1.Columns[j].Name; // Пример имени закладки
            //                            //    if (wordDoc.Bookmarks.Exists(bookmarkName))
            //                            //    {
            //                            //        wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue;
            //                            //    }

            //                            //    // Отправляем информацию о прогрессе
            //                            //    int progressPercentage = (int)((j + 1) / (double)dataGridView1.Columns.Count * 100);
            //                            //    worker.ReportProgress(progressPercentage);
            //                            //}

            //                            // Показываем Word
            //                            wordApp.Visible = true;

            //                            // Освобождаем ресурсы
            //                            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc);
            //                            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            //                        };

            //                        worker.ProgressChanged += (s, args) =>
            //                        {
            //                            // Обновляем ProgressBar
            //                            progressBar.Value = args.ProgressPercentage;
            //                        };

            //                        worker.RunWorkerCompleted += (s, args) =>
            //                        {
            //                            // Скрываем ProgressBar после завершения
            //                            progressBar.Visible = false;
            //                        };

            //                        // Настраиваем и запускаем ProgressBar
            //                        progressBar.Visible = true;
            //                        progressBar.Value = 0;

            //                        // Запускаем фоновую работу
            //                        worker.RunWorkerAsync();
            //                    }
            //                }

            //            }
            //            else
            //            {
            //                MessageBox.Show("Пожалуйста, заполните данные Вашей организации!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }


            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////}
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
                        if (dt30.Rows.Count == 0)
                        {
                            MessageBox.Show("Пожалуйста, заполните данные Вашей организации!.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {


                            if (this.stor != -1 && dataGridView1.CurrentRow.Cells[0].Value!=null)
                            {
                                dates fp = new dates(con, (int)dataGridView1.CurrentRow.Cells[0].Value, (int)comboBox1.SelectedValue);
                                fp.ShowDialog();
                               

                            }
                            else
                            {
                                MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

                    private void переместитьТоварНаДругуюПолкуToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    {
                        if (dataGridView2.CurrentRow != null)
                        {
                            if (dataGridView2.CurrentRow.Cells[0].Value != null)
                            {


                                int id_prod_storehouse_info = (int)dataGridView2.CurrentRow.Cells[0].Value;
                                string stor = (string)dataGridView1.CurrentRow.Cells[4].Value;
                                int id_stor = (int)dataGridView1.CurrentRow.Cells[1].Value;
                                int quantity = Convert.ToInt32(dataGridView2.CurrentRow.Cells[8].Value);
                                string pro = (string)dataGridView2.CurrentRow.Cells[4].Value;
                                string br = (string)dataGridView2.CurrentRow.Cells[3].Value;
                                string name_place = (string)dataGridView1.CurrentRow.Cells[2].Value;
                                string name_pro = (string)dataGridView2.CurrentRow.Cells[5].Value;
                                int id_prod_storehouse = (int)dataGridView2.CurrentRow.Cells[1].Value;
                                new_product_accounting f = new new_product_accounting(con, id_prod_storehouse_info, id_prod_storehouse, stor, this.id_em, quantity, pro, br, name_pro, name_place, id_stor);
                                f.ShowDialog();

                                Update();
                                updateinvoices_in_info(id_prod_storehouse_info);
                                Update_filt();

                            }
                        }
                        else
                        {
                            MessageBox.Show("Пожалуйста, выберите товар из второй таблицы, который необходимо переместить.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    Update();
                    Update_filt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
  