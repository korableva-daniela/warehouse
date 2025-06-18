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
namespace sclade
{
    public partial class newinvoices_in : Form
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
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
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt11 = new DataTable();
        DataSet ds11 = new DataSet();
        public NpgsqlConnection con;
        public int id;
        public string num_invoices;
        public string id_Firm;
        public string id_storehouse;
        public string num_Contract;
        public double total_sum;
        public double total_sum_nds;
        public string status;
        public int id_Employee;
        public int address;
        public DateTime data;
        public DateTime shipment;
        int id_i;
        public int div;
        public newinvoices_in(NpgsqlConnection con, int id, string num_invoices, string id_Firm, string id_storehouse, string num_Contract, double total_sum, double total_sum_nds, string status, int id_Employee, DateTime data, DateTime shipment, int address,int div)
        {
            this.address = address;
            this.con = con;
            InitializeComponent();
            this.id = id;
            this.num_invoices = num_invoices;
            this.id_Firm = id_Firm;
            this.id_storehouse = id_storehouse;
            this.num_Contract = num_Contract;
            this.total_sum = total_sum;
            this.total_sum_nds= total_sum_nds;
            this.div = div;
            this.status = status;
            this.id_Employee = id_Employee;
            this.data = data;
            this.shipment = shipment;
        }


        public void updateFirminfo(int id_f)
        {
            try
            {
                String sql1 = "Select * from Firm where id=";
                sql1 += id_f.ToString();
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "name_f";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateFirminfoupdate(string name)
        {
            try
            {
                String sql9 = "Select * from Firm where name_f='";
                sql9 += name;
                sql9 += "'";
                NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                ds9.Reset();
                da9.Fill(ds9);
                dt9 = ds9.Tables[0];
                comboBox1.DataSource = dt9;
                comboBox1.DisplayMember = "name_f";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
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
                comboBox2.DataSource = dt3;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatestorehouseinfoupdate(string name)
        {
            try
            {
                String sql8 = "Select * from storehouse where name='";
                sql8 += name;
                sql8 += "'";
                NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                ds8.Reset();
                da8.Fill(ds8);
                dt8 = ds8.Tables[0];
                comboBox2.DataSource = dt8;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateaddressinfo(int id_s)
        {
            try
            {
                String sql11 = "Select * from Address_f where id=";
                sql11 += id_s.ToString();
                NpgsqlDataAdapter da11 = new NpgsqlDataAdapter(sql11, con);
                ds11.Reset();
                da11.Fill(ds11);
                dt11 = ds11.Tables[0];
                comboBox5.DataSource = dt11;
                comboBox5.DisplayMember = "post_in_f";
                comboBox5.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateaddressupdate(string name)
        {
            try
            {
                String sql11 = "Select * from Address_f where name='";
                sql11 += name;
                sql11 += "'";
                NpgsqlDataAdapter da11 = new NpgsqlDataAdapter(sql11, con);
                ds11.Reset();
                da11.Fill(ds11);
                dt11 = ds11.Tables[0];
                comboBox5.DataSource = dt11;
                comboBox5.DisplayMember = "post_in_f";
                comboBox5.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateEmployeeinfo(int id_e)
        {
            try
            {
                String sql4 = "Select * from Employee where id=";
                sql4 += id_e.ToString();
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
                ds4.Reset();
                da4.Fill(ds4);
                dt4 = ds4.Tables[0];
                comboBox4.DataSource = dt4;
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateEmployeeupdate(string name)
        {
            try
            {
              
                String sql10 = "Select * from Employee where name='";
                sql10 += name;
                sql10 += "'";
                NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql10, con);
                ds10.Reset();
                da10.Fill(ds10);
                dt10 = ds10.Tables[0];
                comboBox4.DataSource = dt10;
                comboBox4.DisplayMember = "name";
                comboBox4.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
             
            }
            catch { }
            } 
            

        public void Update()
        {
            try
            {
                if (id != -1)

                {

                    String sql6 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds ,invoices_in.shipment,invoices_in.status, Employee.name from Firm, storehouse,invoices_in,Employee where Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id=";
                    sql6 += id.ToString();
                    
                    NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                    ds6.Reset();
                    da6.Fill(ds6);
                    dt6 = ds6.Tables[0];
                    dataGridView1.DataSource = dt6;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Номер накладной";
                    dataGridView1.Columns[2].HeaderText = "Поставщик";
                    dataGridView1.Columns[3].HeaderText = "Склад";
                    dataGridView1.Columns[4].HeaderText = "Дата оформления";
                    dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                    dataGridView1.Columns[6].HeaderText = "Общая сумма";
                    dataGridView1.Columns[7].HeaderText = "Общая сумма c НДС";
                    dataGridView1.Columns[8].HeaderText = "Дата поставки";
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].HeaderText = "Обработчик";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    //String sql6 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.shipment,invoices_in.status, Employee.name from Firm, storehouse,invoices_in,Employee where Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id=";
                    //sql6 += -1;
                    //NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                    //ds6.Reset();
                    //da6.Fill(ds6);
                    //dt6 = ds6.Tables[0];
                    //dataGridView1.DataSource = dt6;

                    //dataGridView1.Columns[0].Visible = false;
                    //dataGridView1.Columns[1].HeaderText = "Номер накладной";
                    //dataGridView1.Columns[2].HeaderText = "Поставщик";
                    //dataGridView1.Columns[3].HeaderText = "Склад";
                    //dataGridView1.Columns[4].HeaderText = "Дата оформления";
                    //dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                    //dataGridView1.Columns[6].HeaderText = "Общая сумма";
                    //dataGridView1.Columns[7].HeaderText = "Дата отгрузки";
                    //dataGridView1.Columns[8].Visible = false;
                    //dataGridView1.Columns[9].HeaderText = "Обработчик";
                    //this.StartPosition = FormStartPosition.CenterScreen;

                }
                label9.Font = new Font("Arial", 11);
                comboBox1.Enabled = false;
                comboBox1.Text = "Поставщик не выбран";
                comboBox1.Font = new Font("Arial", 11);
                if (this.address != -1)
                {
                    comboBox5.Font = new Font("Arial", 11);
                    comboBox5.Text = "Адрес не выбран";
                    updateaddressinfo(this.address);
                    comboBox5.Enabled = false;


                }
                else
                {
                    comboBox5.Font = new Font("Arial", 11);
                    comboBox5.Text = "Адрес не выбран";
                    comboBox5.Enabled = false;

                }
                if (this.id_storehouse != "")
                {
                    comboBox2.Font = new Font("Arial", 11);
                    comboBox2.Text = "Склад не выбран";
                    updatestorehouseinfoupdate(this.id_storehouse);
                    comboBox2.Enabled = false;


                }
                else
                {
                    comboBox2.Font = new Font("Arial", 11);
                    comboBox2.Text = "Склад не выбран";
                    comboBox2.Enabled = false;

                }
                if (this.id_Employee != -1)
                {
               
                    comboBox4.Font = new Font("Arial", 11);
                  
                    comboBox4.Text = "Сотрудник не выбран";
                    comboBox4.Enabled = false;
                    updateEmployeeinfo(this.id_Employee);
                

                }
                else
                {
                    comboBox4.Font = new Font("Arial", 11);
                    comboBox4.Text = "Сотрудник не выбран";
                    comboBox4.Enabled = false;
                }
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label7.Font = new Font("Arial", 11);
                label8.Font = new Font("Arial", 11);

                comboBox3.Font = new Font("Arial", 11);
           
                comboBox3.Text = "Не указано";
           


                textBox1.Font = new Font("Arial", 11);
                //textBox1.Enabled = false;

                textBox2.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);

                
                        }
            
                        catch { }


                    }
        private void button5_Click(object sender, EventArgs e)
        {
            Firm_in fp = new Firm_in(con);
            fp.ShowDialog();
        }

        private void newinvoices_in_Load(object sender, EventArgs e)
        {
            try
            {
                Update();


                label1.Visible = false;

                textBox1.Visible = false;
               
                dateTimePicker1.Visible = false;
                label4.Visible = false;
                label7.Visible = false;
                comboBox3.Visible = false;
                dataGridView1.ReadOnly = true;

               



                if (id != -1)
                {
                    updateFirminfoupdate(this.id_Firm);
                    updatestorehouseinfoupdate(this.id_storehouse);

                    updateaddressinfo(this.address);
                    textBox1.Text = this.num_invoices;

                    comboBox3.Text = status;


                   
                    dateTimePicker2.Value = this.shipment;
                    textBox2.Text = this.num_Contract;


                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int code_;
            DataTable dt31 = new DataTable();
            DataSet ds31 = new DataSet();
            String sql31 = "Select * from invoices_in ORDER BY id DESC LIMIT 1 ;";
            NpgsqlDataAdapter da31 = new NpgsqlDataAdapter(sql31, con);
            ds31.Reset();
            da31.Fill(ds31);
            dt31 = ds31.Tables[0];
            if (dt31.Rows.Count > 0)
            {
                code_ = Convert.ToInt32(dt31.Rows[0]["num_invoices"].ToString());

            }
            else
            {
                code_ = 100;
            }
            if (this.id == -1)
            {

                try
                {

                    string sql = "Insert into invoices_in (num_invoices,id_Firm, id_storehouse,data,num_Contract,total_sum,total_sum_nds,shipment,status,id_Employee,flag,Address_f) values (:num_invoices, :id_Firm, :id_storehouse, :data, :num_Contract, :total_sum,:total_sum_nds, :shipment, :status, :id_Employee, 0,:Address_f)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("num_invoices", (code_ + 1).ToString());
                    command.Parameters.AddWithValue("id_Firm", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("id_storehouse", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("data", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("num_Contract", textBox2.Text);
                    command.Parameters.AddWithValue("total_sum", this.total_sum);
                    command.Parameters.AddWithValue("total_sum_nds", this.total_sum_nds);
                    command.Parameters.AddWithValue("shipment", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("status", comboBox3.Text);
                    command.Parameters.AddWithValue("id_Employee", comboBox4.SelectedValue);
                    command.Parameters.AddWithValue("Address_f", comboBox5.SelectedValue);

                    DialogResult result = MessageBox.Show("Вы уверены, что добавить  запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();

                        String sql6 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds, invoices_in.shipment,invoices_in.status, Employee.name from Firm, storehouse,invoices_in,Employee where Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee ORDER BY invoices_in.id   DESC LIMIT 1   ";

                        NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                        ds6.Reset();
                        da6.Fill(ds6);
                        dt6 = ds6.Tables[0];
                        dataGridView1.DataSource = dt6;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "Номер накладной";
                        dataGridView1.Columns[2].HeaderText = "Поставщик";
                        dataGridView1.Columns[3].HeaderText = "Склад";
                        dataGridView1.Columns[4].HeaderText = "Дата оформления";
                        dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                        dataGridView1.Columns[6].HeaderText = "Общая сумма";
                        dataGridView1.Columns[7].HeaderText = "Общая сумма c НДС";
                        dataGridView1.Columns[8].HeaderText = "Дата поставки";
                        dataGridView1.Columns[9].Visible = false;
                        dataGridView1.Columns[10].HeaderText = "Обработчик";
                        this.StartPosition = FormStartPosition.CenterScreen;



                        DialogResult result1 = MessageBox.Show("Добавить данные о товарах в накладную?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result1 == DialogResult.Yes)
                        {
                            String sql1 = "Select * from invoices_in ORDER BY id DESC LIMIT 1 ;";
                            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql1, con);
                            ds5.Reset();
                            da5.Fill(ds5);
                            dt5 = ds5.Tables[0];
                            if (dt5.Rows.Count > 0)
                            {
                                id_i = Convert.ToInt32(dt5.Rows[0]["id"]);

                            }
                            else
                            {

                                MessageBox.Show("Приходная накладная не найдена.");
                            }
                            this.StartPosition = FormStartPosition.CenterScreen;
                            newinvoices_in_info f = new newinvoices_in_info(con, -1, id_i, "", "", 0, 0, (int)comboBox1.SelectedValue, this.div);
                            f.ShowDialog();
                            Close();
                          
                        }
                        Update();
                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }


            }
            else
            {

                try
                {

                    string sql = "update invoices_in set num_invoices=:num_invoices, id_Firm=:id_Firm, id_storehouse=:id_storehouse,data=:data,num_Contract=:num_Contract,total_sum=:total_sum,total_sum_nds=:total_sum_nds,shipment=:shipment,status=:status,id_Employee=:id_Employee,Address_f=:Address_f where id=:id;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("num_invoices", textBox1.Text);
                    command.Parameters.AddWithValue("id_Firm", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("id_storehouse", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("data", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("num_Contract", textBox2.Text);
                    command.Parameters.AddWithValue("total_sum", this.total_sum);
                    command.Parameters.AddWithValue("total_sum_nds", this.total_sum_nds);
                    command.Parameters.AddWithValue("shipment", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("status", comboBox3.Text);
                    command.Parameters.AddWithValue("id_Employee", comboBox4.SelectedValue);
                    command.Parameters.AddWithValue("Address_f", comboBox5.SelectedValue);
                    command.Parameters.AddWithValue("id", this.id);
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        {
                            String sql6 = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name from Firm, storehouse,invoices_in,Employee where Firm.id=invoices_in.id_Firm and invoices_in.id_storehouse=storehouse.id and Employee.id=invoices_in.id_Employee and invoices_in.id=";
                            sql6 += id.ToString();
                            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                            ds6.Reset();
                            da6.Fill(ds6);
                            dt6 = ds6.Tables[0];
                            dataGridView1.DataSource = dt6;
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.Columns[1].HeaderText = "Номер накладной";
                            dataGridView1.Columns[2].HeaderText = "Поставщик";
                            dataGridView1.Columns[3].HeaderText = "Склад";
                            dataGridView1.Columns[4].HeaderText = "Дата оформления";
                            dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                            dataGridView1.Columns[6].HeaderText = "Общая сумма";
                            dataGridView1.Columns[7].HeaderText = "Общая сумма c НДС";
                            dataGridView1.Columns[8].HeaderText = "Дата поставки";
                            dataGridView1.Columns[9].Visible = false;
                            dataGridView1.Columns[10].HeaderText = "Обработчик";
                            this.StartPosition = FormStartPosition.CenterScreen;

                        }

                        DialogResult result1 = MessageBox.Show("Добавить данные о товарах в накладную?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result1 == DialogResult.Yes)
                        {

                            newinvoices_in_info f = new newinvoices_in_info(con, -1, this.id, "", "", 0, 0, (int)comboBox1.SelectedValue, this.div);
                            f.ShowDialog();
                            Update();
                            Close();
                        }

                    }
                }
                
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }


            

        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                int id_f = 0;
                string name = "";
                if (comboBox2.SelectedValue != null)
                {
                    firm_firm fp = new firm_firm(con, id_f, name,(int)comboBox2.SelectedValue, this.div);
                    fp.ShowDialog();
                    if (fp.name != "")
                    {
                        updateFirminfo(fp.id);

                    }
                    else
                    {
                        comboBox1.Text = "Поставщик не выбран";

                    }
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                            try
                            {
                                newfirm f = new newfirm(con, -1,  "", "", "", "", "", "", "", "", "", "");
            f.ShowDialog();
         
                            }
                            catch { }
                        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name, this.div, "");
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatestorehouseinfo(fp.id_c);

                }
                else
                {
                    comboBox2.Text = "Склад не выбран";

                }
            }
            catch { }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {


                int id_e = 0;
                string name = "";

                employee fp = new employee(con, id_e, name);
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateEmployeeinfo(fp.id);

                }
                else
                {
                    comboBox4.Text = "Сотрудник не выбран";

                }
            }
            catch { }
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            try
            {

                if (comboBox1.SelectedValue != null)
                {
                    int id_e = 0;
                    string name = "";

                    address fp = new address(con, -1, "", (int)comboBox1.SelectedValue);
                    fp.ShowDialog();
                    if (fp.name != "")
                    {
                        updateaddressinfo(fp.id);

                    }
                    else
                    {
                        comboBox5.Text = "Адрес не выбран";

                    }
                }
                
            }
            catch { }
        }
    }
}
