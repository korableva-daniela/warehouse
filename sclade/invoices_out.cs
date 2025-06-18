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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
namespace sclade
{
    public partial class invoices_ : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
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
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        DataTable dt30 = new DataTable();
        DataSet ds30 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        public int div;
        private ProgressBar progressBar;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public int stor;
        public int id_em;
        public string num;
        public int ind;
        public invoices_(NpgsqlConnection con, int stor, int id_em, string num, int ind,int div)
        {
            this.div = div;
            this.ind = ind;
            InitializeProgressBar();
            this.num = num;
            this.id_em = id_em;
            this.stor = stor;
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
                if (this.ind == 0)
                {
                    menuStrip1.Visible = false;
                }
                if (this.stor != -1)
                {
                    updatestorehouseinfo(this.stor);

                }
                else
                {
                    comboBox1.Text = "Склад не выбран";
                }
                if (this.num != "")
                {
                    textBox1.Text = this.num;
                }
                label1.Font = new Font("Arial", 11);
                //label2.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                dataGridView2.Font = new Font("Arial", 9);
                comboBox1.Font = new Font("Arial", 11);
                comboBox1.Enabled = false;
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
                dataGridView2.ContextMenuStrip = contextMenuStrip2;
                try
                {
                    //if (comboBox1.SelectedValue != null)
                    //{
                    //int stor = (int)comboBox1.SelectedValue;
                    if (comboBox1.Text != "Склад не выбран")
                    {
                        if ((textBox1.Text == "") & (textBox2.Text == ""))
                        {
                            String sql1 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where invoices_in.Address_f=Address_f.id and Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id_storehouse=";
                            sql1 += this.stor.ToString();
                            sql1 += " and  invoices_in.flag=1 ORDER BY invoices_in.num_invoices";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == ""))
                        {
                            String sql1 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where invoices_in.Address_f=Address_f.id and Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id_storehouse=";
                            sql1 += this.stor.ToString();
                            sql1 += " and invoices_in.num_invoices ILIKE '";
                            sql1 += textBox1.Text;
                            sql1 += "%' and  invoices_in.flag=1 ORDER BY  invoices_in.num_invoices ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != ""))
                        {
                            String sql1 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where invoices_in.Address_f=Address_f.id and Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id_storehouse=";
                            sql1 += this.stor.ToString();
                            sql1 += " and Firm.name_f ILIKE '";
                            sql1 += textBox2.Text;
                            sql1 += "%' and  invoices_in.flag=1 ORDER BY  invoices_in.num_invoices ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                            ds.Reset();
                            da.Fill(ds);

                        }


                        else if ((textBox1.Text != "") & (textBox2.Text != ""))
                        {
                            String sql1 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where invoices_in.Address_f=Address_f.id and  Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id_storehouse=";
                            sql1 += this.stor.ToString();
                            sql1 += " and invoices_in.num_invoices ILIKE '";
                            sql1 += textBox1.Text;

                            sql1 += "%' and Firm.name_f ILIKE '";
                            sql1 += textBox2.Text;
                            sql1 += "%'  and  invoices_in.flag=1 ORDER BY  invoices_in.num_invoices ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql1, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                    }
                    //}

                    else
                    {
                        if (comboBox1.Text == "Склад не выбран")
                        {
                            if ((textBox1.Text == "") & (textBox2.Text == ""))
                            {
                                String sql2 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where invoices_in.Address_f=Address_f.id and  Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and  invoices_in.flag=1 and storehouse.id_div = " + this.div.ToString() + "  ORDER BY invoices_in.num_invoices";
                                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sql2, con);
                                ds.Reset();
                                da2.Fill(ds);

                            }
                            else if ((textBox1.Text != "") & (textBox2.Text == ""))
                            {
                                String sql2 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where invoices_in.Address_f=Address_f.id and  Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee ";
                                sql2 += " and invoices_in.num_invoices ILIKE '";
                                sql2 += textBox1.Text;
                                sql2 += "%'  and  invoices_in.flag=1 and storehouse.id_div = " + this.div.ToString() + " ORDER BY  invoices_in.num_invoices ASC;";
                                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql2, con);
                                ds.Reset();
                                da.Fill(ds);

                            }
                            else if ((textBox1.Text == "") & (textBox2.Text != ""))
                            {
                                String sql2 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where  invoices_in.Address_f=Address_f.id and Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee ";
                                sql2 += " and Firm.name_f ILIKE '";
                                sql2 += textBox2.Text;
                                sql2 += "%'  and  invoices_in.flag=1 and storehouse.id_div = " + this.div.ToString() + " ORDER BY  invoices_in.num_invoices ASC;";
                                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql2, con);
                                ds.Reset();
                                da.Fill(ds);

                            }


                            else if ((textBox1.Text != "") & (textBox2.Text != ""))
                            {
                                String sql2 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds, invoices_in.shipment,invoices_in.status, Employee.name,Firm.id ,invoices_in.Address_f from Firm,Address_f, storehouse,invoices_in,Employee where  invoices_in.Address_f=Address_f.id and Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee ";
                                sql2 += " and invoices_in.num_invoices ILIKE '";
                                sql2 += textBox1.Text;

                                sql2 += "%' and Firm.name_f ILIKE '";
                                sql2 += textBox2.Text;
                                sql2 += "%'  and  invoices_in.flag=1 and storehouse.id_div = " + this.div.ToString() + "  ORDER BY  invoices_in.num_invoices ASC;";
                                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql2, con);
                                ds.Reset();
                                da.Fill(ds);

                            }
                        }
                    }


                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Номер накладной";
                    dataGridView1.Columns[2].HeaderText = "Поставщик";
                    dataGridView1.Columns[3].HeaderText = "Склад";
                    dataGridView1.Columns[4].HeaderText = "Дата оформления";
                    dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                    dataGridView1.Columns[6].HeaderText = "Общая сумма";
                    dataGridView1.Columns[7].HeaderText = "Общая сумма c НДС";
                    dataGridView1.Columns[8].HeaderText = "Дата отгрузки";
                    dataGridView1.Columns[9].HeaderText = "Статус";
                    dataGridView1.Columns[10].HeaderText = "Обработчик";
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }

                //String sql = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.shipment,invoices_in.status, Employee.name,Firm.id from Firm, storehouse,invoices_in,Employee where Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee ORDER BY invoices_in.id";
                //    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                //    ds.Reset();
                //    da.Fill(ds);
                //    dt = ds.Tables[0];
                //    dataGridView1.DataSource = dt;
                //    dataGridView1.Columns[0].Visible = false;
                //    dataGridView1.Columns[1].HeaderText = "Номер накладной";
                //    dataGridView1.Columns[2].HeaderText = "Поставщик";
                //    dataGridView1.Columns[3].HeaderText = "Склад";
                //    dataGridView1.Columns[4].HeaderText = "Дата оформления";
                //    dataGridView1.Columns[5].HeaderText = "Номер договора";
                //    dataGridView1.Columns[6].HeaderText = "Общая сумма";
                //    dataGridView1.Columns[7].HeaderText = "Дата отгрузки";
                //    dataGridView1.Columns[8].Visible = false;
                //    dataGridView1.Columns[9].HeaderText = "Обработчик";
                //    dataGridView1.Columns[10].Visible = false;
                //    this.StartPosition = FormStartPosition.CenterScreen;

            }
            catch { }
        }
        public void updateinvoices_in_info(int id)
        {
            try
            {
                try
                {
                    if (id != null)
                    {
                        String sqli = "Select invoices_in_info.id, invoices_in.id,invoices_in.num_invoices,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter, invoices_in_info.quantity,invoices_in_info.price,  NDS.percent, invoices_in_info.price_nds, invoices_in_info.quantity*invoices_in_info.price,invoices_in_info.quantity*invoices_in_info.price_nds  from Product_card,batch_number,unit_of_measurement,NDS,invoices_in_info,invoices_in where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and invoices_in.id =invoices_in_info.invoices_in and batch_number.id=invoices_in_info.id_batch_number and NDS.id=Product_card.id_nds and invoices_in.id=:id ORDER BY invoices_in_info.id ASC;";

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
                        dataGridView2.Columns[9].HeaderText = "Цена за одну единицу товара";
                        dataGridView2.Columns[10].HeaderText = "НДС";
                        dataGridView2.Columns[11].HeaderText = "Цена за одну единицу товара с НДС";
                        dataGridView2.Columns[12].HeaderText = "Общая цена";
                        dataGridView2.Columns[13].HeaderText = "Общая цена с НДС";
                        this.StartPosition = FormStartPosition.CenterScreen;
                    }


                    else
                    {


                    }
                }
                catch { }
            }
            catch { }
        }
        private void invoices__Load(object sender, EventArgs e)
        {
            try
            {
                label1.Font = new Font("Arial", 11);
                //label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                textBox1.Font = new Font("Arial", 11);
                textBox2.Font = new Font("Arial", 11);
                comboBox1.Font = new Font("Arial", 11);

                comboBox1.Text = "Склад не выбран";
                Update();
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                this.WindowState = FormWindowState.Maximized;
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
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                        if (dataGridView1.CurrentRow.Index != 0)
                        {
                            id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                            updateinvoices_in_info(id);

                        }


                        else
                        {
                            //String sql1 = "Select * from invoices_in,storehouse where flag = 1  and storehouse.id_div = " + this.div.ToString() + " and storehouse.id=invoices_in.id_storehouse ORDER BY invoices_in.num_invoices  ASC LIMIT 1 ;";
                            if (dataGridView1.Rows[0].Cells[0].Value != null)
                            {
                                String sql1 = "Select * from invoices_in  where id = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
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
                            updateinvoices_in_info(id);


                        }
                }
               


            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void личныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.id_em != -1)
                {
                    if (comboBox1.Text != "Склад не выбран")
                    {
                        newinvoices_out fp = new newinvoices_out(con, -1, "", "", comboBox1.Text, "", 0,0, "", this.id_em, DateTime.Today, DateTime.Today,-1,div);
                        fp.ShowDialog();
                    }
                    else
                    {
                        newinvoices_out fp = new newinvoices_out(con, -1, "", "", "", "", 0,0, "", this.id_em, DateTime.Today, DateTime.Today,-1,div);
                        fp.ShowDialog();
                    }
                    Update();
                }

            }
            catch { }
        }

        private void адресToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            //    if (this.stor !=-1)
            //{
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    String sql1 = "Select * from invoices_in where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql1, con);
                    ds10.Reset();
                    da10.Fill(ds10);
                    dt10 = ds10.Tables[0];
                    if (dt10.Rows.Count > 0)
                    {
                        if (this.id_em == Convert.ToInt32(dt10.Rows[0]["id_Employee"]))
                        {
                            if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Не указано")
                            {
                                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                                string storh = (string)dataGridView1.CurrentRow.Cells[3].Value;
                                if (id > 0)
                                {
                                    newinvoices_out_info fp = new newinvoices_out_info(con, -1, id, "", "", 0, 0, storh, this.div);
                                    fp.ShowDialog();
                                    Update();
                                    updateinvoices_in_info(id);
                                }
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Накладная уже находится в обработке и изменить её нельзя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("У Вас нет прав на редактирование выбранной накладной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Необходимо выборать накладную, в которую хотите добавить информацию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Update();
                    }
                }
            //}
            //    else
            //    {
            //        DialogResult result = MessageBox.Show("Необходимо выборать склад", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
            //    }
        }

            catch
            {

            }

            Update();
        }

        private void личныеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    String sql1 = "Select * from invoices_in where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql1, con);
                ds10.Reset();
                da10.Fill(ds10);
                dt10 = ds10.Tables[0];
                    if (dt10.Rows.Count > 0)
                    {
                        if (this.id_em == Convert.ToInt32(dt10.Rows[0]["id_Employee"]))
                        {
                            if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Не указано")
                            {
                                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                            string num_invoices = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            string id_Firm = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            string id_storehouse = (string)dataGridView1.CurrentRow.Cells[3].Value;
                            DateTime data = (DateTime)dataGridView1.CurrentRow.Cells[4].Value;
                            string num_Contract = (string)dataGridView1.CurrentRow.Cells[5].Value;
                            double total_sum = (double)dataGridView1.CurrentRow.Cells[6].Value;
                            double total_sum_nds = (double)dataGridView1.CurrentRow.Cells[7].Value;
                            DateTime shipment = (DateTime)dataGridView1.CurrentRow.Cells[8].Value;
                            string status = (string)dataGridView1.CurrentRow.Cells[9].Value;
                            //string id_Employee = (string)dataGridView1.CurrentRow.Cells[9].Value;
                            int address = (int)dataGridView1.CurrentRow.Cells[12].Value;
                            newinvoices_out fp = new newinvoices_out(con, id, num_invoices, id_Firm, id_storehouse, num_Contract, total_sum, total_sum_nds, status, this.id_em, data, shipment, address,div);
                            fp.ShowDialog();
                            Update();
                            updateinvoices_in_info(id);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Накладная уже находится в обработке и изменить её нельзя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("У Вас нет прав на редактирование выбранной накладной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Необходимо выборать накладную, в которую хотите добавить информацию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Update();
                }
            }
            catch { }
        }

        private void адресToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                        if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                String sql1 = "Select * from invoices_in where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql1, con);
                ds10.Reset();
                da10.Fill(ds10);
                dt10 = ds10.Tables[0];
                if (dt10.Rows.Count > 0)
                {
                    if (this.id_em == Convert.ToInt32(dt10.Rows[0]["id_Employee"]))
                        {
                            if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Не указано")
                            {
                                int id = (int)dataGridView2.CurrentRow.Cells[0].Value;
                    int id_invoices = (int)dataGridView2.CurrentRow.Cells[1].Value;
                    string id_Product_card = (string)dataGridView2.CurrentRow.Cells[4].Value;
                    string id_batch_number = (string)dataGridView2.CurrentRow.Cells[3].Value;
                    int quantity = (int)dataGridView2.CurrentRow.Cells[8].Value;
                    string storh = (string)dataGridView1.CurrentRow.Cells[3].Value;
                    double price = (double)dataGridView2.CurrentRow.Cells[9].Value;
                   
                   
                    newinvoices_out_info fp = new newinvoices_out_info(con, id, id_invoices, id_Product_card, id_batch_number, quantity, price, storh, this.div);
                    fp.ShowDialog();
                    //Update();
                    updateinvoices_in_info(id);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Накладная уже находится в обработке и изменить её нельзя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("У Вас нет прав на редактирование выбранной накладной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    String sql1 = "Select * from invoices_in where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql1, con);
                    ds10.Reset();
                    da10.Fill(ds10);
                    dt10 = ds10.Tables[0];
                    if (dt10.Rows.Count > 0)
                    {
                        if (this.id_em == Convert.ToInt32(dt10.Rows[0]["id_Employee"]))
                        {
                            if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Не указано")
                            {
                                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                    NpgsqlCommand command = new NpgsqlCommand("DELETE FROM invoices_in WHERE id=:id", con);
                    NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM   invoices_in_info  WHERE invoices_in=:id", con);
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
                    updateinvoices_in_info(id);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Накладная уже находится в обработке и изменить её нельзя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("У Вас нет прав на редактирование выбранной накладной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
            }
            catch { }
        }

        private void адресToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    String sql1 = "Select * from invoices_in where id = " + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql1, con);
                    ds10.Reset();
                    da10.Fill(ds10);
                    dt10 = ds10.Tables[0];
                    if (dt10.Rows.Count > 0)
                    {
                        if (this.id_em == Convert.ToInt32(dt10.Rows[0]["id_Employee"]))
                        {
                            if (dataGridView1.CurrentRow.Cells[9].Value.ToString() == "Не указано")
                            {
                                int id = (int)dataGridView2.CurrentRow.Cells["id"].Value;
                    NpgsqlCommand command = new NpgsqlCommand("DELETE FROM   invoices_in_info  WHERE id=:id", con);
                    command.Parameters.AddWithValue("id", id);
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Update();
                    }
                    else
                        //Update();
                        updateinvoices_in_info(id);
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show("Накладная уже находится в обработке и изменить её нельзя!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show("У Вас нет прав на редактирование выбранной накладной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{


            //    int id_s = 0;
            //    string name = "";

            //    storehouse fp = new storehouse(con, id_s, name);
            //    fp.ShowDialog();
            //    if (fp.name != "")
            //    {
            //        updatestorehouseinfo(fp.id_c);
            //        Update();
            //    }
            //    else
            //    {
            //        comboBox1.Text = "Склад не выбран";

            //    }
            //}
            //catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filter fp = new filter(con,div);
            fp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            batch_number fp = new batch_number(con, -1, "", -1, -1,-1,div);
            fp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                this.num = textBox1.Text;
                Update();
                if (dataGridView1.CurrentRow!=null)
                {
                    int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    updateinvoices_in_info(id);
                }
                else
                {
                    int id = -1;
                    updateinvoices_in_info(id);
                }
            }
            catch { }
        }

        private void button4_Click_1(object sender, EventArgs e)
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
                    Update();
                }
                else
                {
                    comboBox1.Text = "Склад не выбран";

                }
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            updatestorehouseinfo(-1);
            comboBox1.Text = "Склад не выбран";
            this.stor = -1;
            Update();
        }

        private void информацияОКлиентеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells[2].Value != null)
                {
                    string id_Firm = (string)dataGridView1.CurrentRow.Cells[2].Value;



                    firm fp = new firm(con, -2, id_Firm);
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

        private void информацияОДвиженияхТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView2.CurrentRow.Cells[4].Value != null)
                {

                    string id_pro = (string)dataGridView2.CurrentRow.Cells[4].Value;


                    if (this.stor != -1)
                    {
                        mov_pro fp = new mov_pro(con, this.stor, id_pro, this.id_em, -1,div);
                        fp.Show();
                    }
                    else
                    {
                        mov_pro fp = new mov_pro(con, -1, id_pro, this.id_em, -1,div);
                        fp.Show();
                    }
                }
            }
            catch { }
        
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
                    if (i == 11||i==9 || i == 12)
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
                        if (j== 11 || j == 9 || j == 12)
                        {

                        }


                        else
                        {


                            worksheet.Cells[2, m] = dataGridView.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value?.ToString();
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
                            if (j == 11 || j == 9 || j == 12)
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
                    if (i == 11 || i == 9 || i == 12)
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
                        if (j == 11 || j == 9 || j == 12)
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
                    if (i == 11 || i == 9 || i == 12)
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
                        if (j == 11 || j == 9 || j == 12)
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
                for (int i = 3; i < dataGridView2.Columns.Count; i++)

                {
                   

                    
                        worksheet.Cells[4, h_1] = dataGridView2.Columns[i].HeaderText;
                        h_1++;
                  
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 3; j < dataGridView2.Columns.Count; j++)
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
        private void InitializeProgressBar()
        {
            progressBar = new ProgressBar();
            progressBar.Location = new Point(200, 15); // Установите нужные координаты
            progressBar.Size = new Size(200, 30); // Установите нужный размер
            progressBar.Visible = false; // Скрываем его изначально
            this.Controls.Add(progressBar); // Добавляем ProgressBar на форму
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
                if (comboBox1.Text != "Склад не выбран")
                {
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Приходные накладные. Склад: " + comboBox1.Text;
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();
                }
                else
                {
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Приходные накладные. ";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();
                }


                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count -3);

                int h = 1;
                // Добавляем заголовки столбцов
                for (int i = 1; i < dataGridView.Columns.Count; i++)
                {
                    if (dataGridView.Columns[i].Visible == true)
                    {
                        table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, h).Range.Font.Size = 8;
                        h++;
                    }
                }

                // Заполняем таблицу данными
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            table.Cell(i + 2, m).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(i + 2, m).Range.Font.Size = 8;
                            m++;
                        }
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
        private void вWordВыбраннуюНакладнуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    // Создаем и настраиваем BackgroundWorker
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;

                    worker.DoWork += (s, args) =>
                    {
                        int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                        // Создание экземпляра Word
                        Word.Application wordApp = new Word.Application();
                        string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "TORG-12.docx");
                        Word.Document wordDoc = wordApp.Documents.Open(templatePath);
                        String sql1 = "Select CONCAT('  ',Firm.name_f, ' , ИНН: ',Firm.INN , ' , КПП: ',Firm.KPP, ' , ОГРН: ',Firm.OGRN, ' , р/с: ',Firm.pc, ' , название банка: ',Firm.bank, ' , бик:',Firm.bik, ' , адрес: ',Address_f.country_f, ' , ',Address_f.city_f, ' , ',Address_f.street_f, ' , ',Address_f.house_f, ' , ',Address_f.post_in_f) AS payer,CONCAT('  ',Firm.name_f, ' , ИНН: ',Firm.INN , ' , КПП: ',Firm.KPP, ' , ОГРН: ',Firm.OGRN, ' , р/с: ',Firm.pc, ' , название банка: ',Firm.bank, ' , бик:',Firm.bik, ' , адрес: ',Address_f.country_f, ' , ',Address_f.city_f, ' , ',Address_f.street_f, ' , ',Address_f.house_f, ' , ',Address_f.post_in_f) AS sender, CONCAT('  ',Firm.name_f, ' , ИНН: ',Firm.INN , ' , КПП: ',Firm.KPP, ' , ОГРН: ',Firm.OGRN, ' , р/с: ',Firm.pc, ' , название банка: ',Firm.bank, ' , бик:',Firm.bik, ' , адрес: ',Address_f.country_f, ' , ',Address_f.city_f, ' , ',Address_f.street_f, ' , ',Address_f.house_f, ' , ',Address_f.post_in_f) AS provider, invoices_in.num_invoices as num_invoices,  invoices_in.data as data,invoices_in.num_Contract as num_Contract,invoices_in.total_sum as total_sum,invoices_in.total_sum_nds as sum, invoices_in.total_sum_nds as total_sum_nds   from Firm,Address_f,invoices_in where Firm.id=Address_f.id_f and invoices_in.id_Firm=Firm.id and invoices_in.id= " + id + "  and invoices_in.Address_f=Address_f.id";
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
                            String sql8 = "Select row_number() over (partition by invoices_in_info.invoices_in order by invoices_in_info.id) as row_n, CONCAT('  ',Product_card.code,' , ',Product_card.name,' , номер партии:', batch_number.number, ' , ' ,Product_card.name_firm) as num_pro,Product_card.code as code_pro, unit_of_measurement.litter as litter,unit_of_measurement.code as ed_code, invoices_in_info.quantity as col_pro,batch_number.price as price, invoices_in_info.price_nds,  NDS.percent as nds,batch_number.price*NDS.percent/100 as pr_nds, invoices_in_info.quantity*batch_number.price as price_no_nds,invoices_in_info.quantity*invoices_in_info.price_nds as price_with_nds from Product_card,batch_number,unit_of_measurement,NDS,invoices_in_info,invoices_in where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and invoices_in.id =invoices_in_info.invoices_in and batch_number.id=invoices_in_info.id_batch_number and NDS.id=Product_card.id_nds and invoices_in.id=:id ORDER BY invoices_in_info.id ASC;";
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
                                    Word.Table table = wordDoc.Tables.Add(range, 2, dt8.Columns.Count);
                                    foreach (Word.Cell cell in table.Rows[1].Cells)
                                    {
                                        cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                        cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                    }
                                    foreach (Word.Cell cell in table.Rows[2].Cells)
                                    {
                                        cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                        cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                    }
                                    table.Cell(1, 1).Range.Text = "Номер по порядку";
                                    table.Cell(1, 2).Range.Text = "Товар";
                                    table.Cell(1, 3).Range.Text = "Товар";
                                    table.Cell(1, 4).Range.Text = "Единица измерения";
                                    table.Cell(1, 5).Range.Text = "Единица измерения";
                                    table.Cell(1, 6).Range.Text = "Количество";
                                    table.Cell(1, 7).Range.Text = "Цена,без учета НДС,руб. коп.";
                                    table.Cell(1, 8).Range.Text = "Цена, с НДС,руб. коп.";
                                    table.Cell(1, 9).Range.Text = "НДС";
                                    table.Cell(1, 10).Range.Text = "НДС";
                                    table.Cell(1, 11).Range.Text = "Сумма без учета НДС, руб.коп.";
                                    table.Cell(1, 12).Range.Text = "Сумма с НДС, руб.коп.";
                                    table.Cell(2, 1).Range.Text = "";
                                    table.Cell(2, 2).Range.Text = "наименование, характеристика, сорт, артикул товара";
                                    table.Cell(2, 3).Range.Text = "код";
                                    table.Cell(2, 4).Range.Text = "наименование";
                                    table.Cell(2, 5).Range.Text = "код по ОКЕИ";
                                    table.Cell(2, 6).Range.Text = "мест,штук";
                                    table.Cell(1, 7).Range.Text = "Цена,без учета НДС,руб. коп.";
                                    table.Cell(1, 8).Range.Text = "Цена, с НДС,руб. коп.";
                                    table.Cell(2, 9).Range.Text = "ставка, %";
                                    table.Cell(2, 10).Range.Text = "сумма,руб.коп.";
                                    table.Cell(1, 11).Range.Text = "Сумма без учета НДС, руб.коп.";
                                    table.Cell(1, 12).Range.Text = "Сумма с НДС, руб.коп.";
                                    // Объединяем ячейки в первой строке
                                    Word.Range cellRange = table.Cell(1, 1).Range;
                                    cellRange.End = table.Cell(1, 2).Range.End; // Устанавливаем конец диапазона на конец третьей ячейки


                                    // Настройка внешнего вида таблицы (например, шрифт, размеры и т.д.)
                                    table.Borders.Enable = 1; // Включаем рамки
                                                              // Дополнительные настройки можно добавить здесь
                                                              //Word.Table table = wordDoc.Tables[3]; // Получаем первую таблицу (индексация с 1)
                                                              //foreach (Word.Cell cell in table.Rows[4].Cells)
                                                              //{
                                                              //    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                              //    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                              //}
                                                              //table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
                                    for (int i = 0; i < dt8.Rows.Count; i++)
                                    {
                                        Word.Row newRow = table.Rows.Add();
                                        for (int j = 0; j < dt8.Columns.Count; j++)
                                        {
                                            // Получаем значение ячейки
                                            var cellValue = dt8.Rows[i][j]?.ToString();
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


                        String sql9 = "Select SUM(invoices_in_info.quantity), invoices_in.total_sum,invoices_in.total_sum_nds from  invoices_in,invoices_in_info where  invoices_in.id = invoices_in_info.invoices_in  and invoices_in.id = " + id + " GROUP BY invoices_in.id";


                        NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                        da9.SelectCommand.Parameters.AddWithValue("id", id);
                        ds9.Reset();
                        da9.Fill(ds9);
                        dt9 = ds9.Tables[0];
                        // Вставка данных из DataGridView
                        if (dt9.Rows.Count > 0)

                        {// Проверяем, существует ли закладка
                            string bookmarkName = "table_itog"; // Имя закладки соответствует имени столбца
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
                                Word.Table table = wordDoc.Tables.Add(range, 1, dt9.Columns.Count + 1);
                                foreach (Word.Cell cell in table.Rows[1].Cells)
                                {
                                    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                }

                                table.Cell(1, 1).Range.Text = "Итог :";

                                table.Cell(1, 2).Range.Text = "Общее количество товаров";

                                table.Cell(1, 3).Range.Text = "Общая Сумма без учета НДС,руб. коп.";
                                table.Cell(1, 4).Range.Text = "Общая Сумма с учетом НДС, руб.коп.";




                                // Настройка внешнего вида таблицы (например, шрифт, размеры и т.д.)
                                table.Borders.Enable = 1; // Включаем рамки
                                                          // Дополнительные настройки можно добавить здесь
                                                          //Word.Table table = wordDoc.Tables[3]; // Получаем первую таблицу (индексация с 1)
                                                          //foreach (Word.Cell cell in table.Rows[4].Cells)
                                                          //{
                                                          //    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                          //    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                          //}
                                                          //table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
                                for (int i = 0; i < dt9.Rows.Count; i++)
                                {
                                    Word.Row newRow = table.Rows.Add();
                                    for (int j = 0; j < dt9.Columns.Count; j++)
                                    {
                                        // Получаем значение ячейки
                                        var cellValue = dt9.Rows[i][j]?.ToString();
                                        newRow.Cells[j + 2].Range.Text = cellValue;
                                        newRow.Cells[j + 2].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                        newRow.Cells[j + 2].Range.Font.Size = 8;
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

                            if (dataGridView.Columns[j].Visible == true)
                            {
                                data[dataGridView.Columns[j].HeaderText] = row.Cells[j].Value ?? ""; // Добавляем данные в словарь
                            }
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

                        if (dataGridView.Columns[j].Visible == true)
                        {
                            //if (dataGridView.Columns[j].Visible == true)
                            //{
                            data[dataGridView.Columns[j].HeaderText] = dataGridView1.CurrentRow.Cells[j].Value ?? ""; // Добавляем данные в словарь
                                                                                                                      //}
                        }

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
                            for (int j = 3; j < dataGridView.Columns.Count; j++)
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



        private void button10_Click(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void вExcelДанныеВыбранногоПодразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void вExcelДанныеАдресовФирмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void информацияОбОстаткамПоПартиямToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridView2.CurrentRow.Cells[3].Value != null)
                {
                    string number_batch = (string)dataGridView2.CurrentRow.Cells[3].Value;


                    batch_in_prod fp = new batch_in_prod(con, -1, number_batch);
                    fp.Show();
                }
                //        else
                //        {
                //            DialogResult result = MessageBox.Show("У выбранного товара нет партий. Хотите создать новую партию товара?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //            if (result == DialogResult.Yes)
                //            {


                //                //textBox1.Visible = false;
                //                newbatch_number f = new newbatch_number(con, -1, comboBox1.Text, "", DateTime.Today, DateTime.Today, "", 0, id_pro_card, 0);
                //                fp.ShowDialog();


                //                //checkBox1.Checked = true;

                //            }
                //            else { }

                //        }

            }
            catch { }
        }

        private void информацияОбОстаткахПоСкладамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridView2.CurrentRow.Cells[4].Value != null)
                {
                    string prod = (string)dataGridView2.CurrentRow.Cells[4].Value;


                    prod_in_sclad fp = new prod_in_sclad(con, -1, prod);
                    fp.Show();
                }
                //        else
                //        {
                //            DialogResult result = MessageBox.Show("У выбранного товара нет партий. Хотите создать новую партию товара?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //            if (result == DialogResult.Yes)
                //            {


                //                //textBox1.Visible = false;
                //                newbatch_number f = new newbatch_number(con, -1, comboBox1.Text, "", DateTime.Today, DateTime.Today, "", 0, id_pro_card, 0);
                //                fp.ShowDialog();


                //                //checkBox1.Checked = true;

                //            }
                //            else { }

                //        }

            }
            catch { }
        }

        private void информацияОбАдресеОтгрузкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridView1.CurrentRow.Cells[12].Value != null)
                {
                    int address_id = (int)dataGridView1.CurrentRow.Cells[12].Value;

                    string name = (string)dataGridView1.CurrentRow.Cells[2].Value;
                    address fp = new address(con, address_id, name, -1);
                    fp.Show();
                }
                //        else
                //        {
                //            DialogResult result = MessageBox.Show("У выбранного товара нет партий. Хотите создать новую партию товара?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //            if (result == DialogResult.Yes)
                //            {


                //                //textBox1.Visible = false;
                //                newbatch_number f = new newbatch_number(con, -1, comboBox1.Text, "", DateTime.Today, DateTime.Today, "", 0, id_pro_card, 0);
                //                fp.ShowDialog();


                //                //checkBox1.Checked = true;

                //            }
                //            else { }

                //        }

            }
            catch { }
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click_1(object sender, EventArgs e)
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

                        saveFileDialog.FileName = "invoices_out_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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

                        saveFileDialog.FileName = "invoices_out_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
            }
            catch { }
        }

        private void вExcelДанныеВыбранногоПодразделенияToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            saveFileDialog.FileName = "invoices_out_" + comboBox1.Text.Replace(" ", "_") + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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

                            saveFileDialog.FileName = "invoices_out_" + comboBox1.Text.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            saveFileDialog.FileName = "invoices_out_" + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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

                            saveFileDialog.FileName = "invoices_out_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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

        private void вExcelДанныеАдресовФирмыToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            saveFileDialog.FileName = "invoices_out_info_" + comboBox1.Text.Replace(" ", "_") + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel_address(dataGridView1, dataGridView2, saveFileDialog.FileName);
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
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            saveFileDialog.FileName = "invoices_out_info_" + "_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel_address(dataGridView1, dataGridView2, saveFileDialog.FileName);
                            }
                        }

                    }

                }
            }
            catch { }
        }

        private void вExcelВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    saveFileDialog.FileName = $"invoices_out_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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

        private void вExcelВыбраннойНакладнойToolStripMenuItem_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = $"invoices_out_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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

        private void вExcelДанныеВНакладнойToolStripMenuItem1_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = $"invoices_out_address_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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

        private void вWordВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {



                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Files|*.docx";
                    saveFileDialog.Title = "Сохранить файл Word";
                    saveFileDialog.FileName = "invoices_out_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

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

        private void вWordВыбраннуюНакладнуюToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                { String sql30 = "Select * from organization ";
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
                            saveFileDialog.FileName = "invoices_out_" + code + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

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
                                string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "TORG-12.docx");

                                // Указываем путь для копии документа
                                string copyPath = Path.Combine(saveFileDialog.FileName);

                                // Копируем файл
                                File.Copy(templatePath, copyPath, true); // true - перезаписывает файл, если он существует

                                // Открываем копию документа
                                Word.Document wordDoc = wordApp.Documents.Open(copyPath);

                                // Делаем приложение видимым (по желанию)
                                wordApp.Visible = true;

                                    String sql1 = "Select CONCAT('  ',Firm.name_f, ' , ИНН: ',Firm.INN , ' , КПП: ',Firm.KPP, ' , ОГРН: ',Firm.OGRN, ' , р/с: ',Firm.pc, ' , название банка: ',Firm.bank, ' , бик:',Firm.bik, ' , адрес: ',Address_f.country_f, ' , ',Address_f.city_f, ' , ',Address_f.street_f, ' , ',Address_f.house_f, ' , ',Address_f.post_in_f) AS recipient," +
                                    " CONCAT('  ',organization.name_f, ' , ИНН: ',organization.INN , ' , КПП: ',organization.KPP, ' , ОГРН: ',organization.OGRN, ' , р/с: ',organization.pc, ' , название банка: ',organization.bank, ' , бик: ',organization.bik,' Склад:  ', storehouse.name, ' , Подразделение: ', Division.name, ' , адрес: ', storehouse.country_d, ' , ', storehouse.city_d, ' ,  ', storehouse.street_d, ' ,  ', storehouse.house_d, ' , ', storehouse.post_in_d) AS sender, " +
                                    " CONCAT('  ',organization.name_f, ' , ИНН: ',organization.INN , ' , КПП: ',organization.KPP, ' , ОГРН: ',organization.OGRN, ' , р/с: ',organization.pc, ' , название банка: ',organization.bank, ' , бик:',organization.bik) AS provider, " +
                                    " CONCAT('  ',Firm.name_f, ' , ИНН: ',Firm.INN , ' , КПП: ',Firm.KPP, ' , ОГРН: ',Firm.OGRN, ' , р/с: ',Firm.pc, ' , название банка: ',Firm.bank, ' , бик:',Firm.bik) AS payer , " +
                                    " invoices_in.num_invoices as num_invoices,  invoices_in.data as data,invoices_in.num_Contract as num_Contract,invoices_in.total_sum as total_sum,invoices_in.total_sum_nds as sum, invoices_in.total_sum_nds as total_sum_nds  " +
                                    " from Firm,Address_f,invoices_in,organization,Division,storehouse where Firm.id=Address_f.id_f and invoices_in.id_Firm=Firm.id and  storehouse.id=invoices_in.id_storehouse and Division.id=storehouse.id_div  and invoices_in.id= " + id + "  and invoices_in.Address_f=Address_f.id";
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
                                        String sql8 = "Select row_number() over (partition by invoices_in_info.invoices_in order by invoices_in_info.id) as row_n, CONCAT('  ',Product_card.code,' , ',Product_card.name,' , номер партии:', batch_number.number, ' , ' ,Product_card.name_firm) as num_pro,Product_card.code as code_pro, unit_of_measurement.litter as litter,unit_of_measurement.code as ed_code, invoices_in_info.quantity as col_pro,batch_number.price as price, invoices_in_info.price_nds,  NDS.percent as nds,batch_number.price*NDS.percent/100 as pr_nds, invoices_in_info.quantity*batch_number.price as price_no_nds,invoices_in_info.quantity*invoices_in_info.price_nds as price_with_nds from Product_card,batch_number,unit_of_measurement,NDS,invoices_in_info,invoices_in where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and invoices_in.id =invoices_in_info.invoices_in and batch_number.id=invoices_in_info.id_batch_number and NDS.id=Product_card.id_nds and invoices_in.id=:id ORDER BY invoices_in_info.id ASC;";
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
                                            Word.Table table = wordDoc.Tables.Add(range, 2, dt8.Columns.Count);
                                                foreach (Word.Cell cell in table.Rows[1].Cells)
                                                {
                                                    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                            }
                                                foreach (Word.Cell cell in table.Rows[2].Cells)
                                                {
                                                    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                            }
                                                table.Cell(1, 1).Range.Text = "Номер по порядку";
                                                table.Cell(1, 2).Range.Text = "Товар";
                                                table.Cell(1, 3).Range.Text = "Товар";
                                                table.Cell(1, 4).Range.Text = "Единица измерения";
                                                table.Cell(1, 5).Range.Text = "Единица измерения";
                                                table.Cell(1, 6).Range.Text = "Количество";
                                                table.Cell(1, 7).Range.Text = "Цена,без учета НДС,руб. коп.";
                                                table.Cell(1, 8).Range.Text = "Цена, с НДС,руб. коп.";
                                                table.Cell(1, 9).Range.Text = "НДС";
                                                table.Cell(1, 10).Range.Text = "НДС";
                                                table.Cell(1, 11).Range.Text = "Сумма без учета НДС, руб.коп.";
                                                table.Cell(1, 12).Range.Text = "Сумма с НДС, руб.коп.";
                                                table.Cell(2, 1).Range.Text = "";
                                                table.Cell(2, 2).Range.Text = "наименование, характеристика, сорт, артикул товара";
                                                table.Cell(2, 3).Range.Text = "код";
                                                table.Cell(2, 4).Range.Text = "наименование";
                                                table.Cell(2, 5).Range.Text = "код по ОКЕИ";
                                                table.Cell(2, 6).Range.Text = "мест,штук";
                                                table.Cell(1, 7).Range.Text = "Цена,без учета НДС,руб. коп.";
                                                table.Cell(1, 8).Range.Text = "Цена, с НДС,руб. коп.";
                                                table.Cell(2, 9).Range.Text = "ставка, %";
                                                table.Cell(2, 10).Range.Text = "сумма,руб.коп.";
                                                table.Cell(1, 11).Range.Text = "Сумма без учета НДС, руб.коп.";
                                                table.Cell(1, 12).Range.Text = "Сумма с НДС, руб.коп.";
                                            // Объединяем ячейки в первой строке
                                            Word.Range cellRange = table.Cell(1, 1).Range;
                                                cellRange.End = table.Cell(1, 2).Range.End; // Устанавливаем конец диапазона на конец третьей ячейки


                                            // Настройка внешнего вида таблицы (например, шрифт, размеры и т.д.)
                                            table.Borders.Enable = 1; // Включаем рамки
                                                                      // Дополнительные настройки можно добавить здесь
                                                                      //Word.Table table = wordDoc.Tables[3]; // Получаем первую таблицу (индексация с 1)
                                                                      //foreach (Word.Cell cell in table.Rows[4].Cells)
                                                                      //{
                                                                      //    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                                      //    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                                      //}
                                                                      //table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
                                            for (int i = 0; i < dt8.Rows.Count; i++)
                                                {
                                                    Word.Row newRow = table.Rows.Add();
                                                    for (int j = 0; j < dt8.Columns.Count; j++)
                                                    {
                                                    // Получаем значение ячейки
                                                    var cellValue = dt8.Rows[i][j]?.ToString();
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


                                    String sql9 = "Select SUM(invoices_in_info.quantity), invoices_in.total_sum,invoices_in.total_sum_nds from  invoices_in,invoices_in_info where  invoices_in.id = invoices_in_info.invoices_in  and invoices_in.id = " + id + " GROUP BY invoices_in.id";


                                    NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                                    da9.SelectCommand.Parameters.AddWithValue("id", id);
                                    ds9.Reset();
                                    da9.Fill(ds9);
                                    dt9 = ds9.Tables[0];
                                // Вставка данных из DataGridView
                                if (dt9.Rows.Count > 0)

                                    {// Проверяем, существует ли закладка
                                    string bookmarkName = "table_itog"; // Имя закладки соответствует имени столбца
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
                                        Word.Table table = wordDoc.Tables.Add(range, 1, dt9.Columns.Count + 1);
                                            foreach (Word.Cell cell in table.Rows[1].Cells)
                                            {
                                                cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                        }

                                            table.Cell(1, 1).Range.Text = "Итог :";

                                            table.Cell(1, 2).Range.Text = "Общее количество товаров";

                                            table.Cell(1, 3).Range.Text = "Общая Сумма без учета НДС,руб. коп.";
                                            table.Cell(1, 4).Range.Text = "Общая Сумма с учетом НДС, руб.коп.";




                                        // Настройка внешнего вида таблицы (например, шрифт, размеры и т.д.)
                                        table.Borders.Enable = 1; // Включаем рамки
                                                                  // Дополнительные настройки можно добавить здесь
                                                                  //Word.Table table = wordDoc.Tables[3]; // Получаем первую таблицу (индексация с 1)
                                                                  //foreach (Word.Cell cell in table.Rows[4].Cells)
                                                                  //{
                                                                  //    cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                                  //    cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                                                                  //}
                                                                  //table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitContent);
                                        for (int i = 0; i < dt9.Rows.Count; i++)
                                            {
                                                Word.Row newRow = table.Rows.Add();
                                                for (int j = 0; j < dt9.Columns.Count; j++)
                                                {
                                                // Получаем значение ячейки
                                                var cellValue = dt9.Rows[i][j]?.ToString();
                                                    newRow.Cells[j + 2].Range.Text = cellValue;
                                                    newRow.Cells[j + 2].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                                newRow.Cells[j + 2].Range.Font.Size = 8;
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
    


