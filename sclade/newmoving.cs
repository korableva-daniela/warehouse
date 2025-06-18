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
    public partial class newmoving : Form
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
        public NpgsqlConnection con;
        public int id;
        public string num_invoices;
        public string id_storehouse_to;
        public string id_storehouse;
        public string num_Contract;

        public string status;
        public int id_Employee;
        public DateTime shipment_to;
        public DateTime data;
        public DateTime shipment;
        int id_i;
        public int div;
        public newmoving(NpgsqlConnection con, int id, string num_invoices, string id_storehouse, string id_storehouse_to, string num_Contract,  string status, int id_Employee, DateTime data, DateTime shipment, DateTime shipment_to,int div)
        {
            this.div = div;
            this.con = con;
            InitializeComponent();
            this.id = id;
            this.num_invoices = num_invoices;
            this.id_storehouse_to = id_storehouse_to;
            this.id_storehouse = id_storehouse;
            this.num_Contract = num_Contract;
    

            this.status = status;
            this.id_Employee = id_Employee;
            this.data = data;
            this.shipment = shipment;
            this.shipment_to = shipment_to;
        }
        public void updatestorehouseinfo(int id_s)
        {
            try
            {
                String sql9 = "Select * from storehouse where id=";
                sql9 += id_s.ToString();
                NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                ds9.Reset();
                da9.Fill(ds9);
                dt9 = ds9.Tables[0];
                comboBox2.DataSource = dt9;
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
                String sql1 = "Select * from storehouse where name='";
                sql1 += name;
                sql1 += "'";
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                comboBox2.DataSource = dt1;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatestorehouseinfo_to(int id_s)
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
            catch { }
        }
        public void updatestorehouseinfoupdate_to(string name)
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
                comboBox1.DataSource = dt8;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
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

                    String sql6 = "Select moving.id,moving.num_invoices, storehouse.name,(select storehouse.name from storehouse where storehouse.id = moving.id_storehouse_2)  AS storehouse_to, moving.data,moving.num_Contract,moving.shipment,moving.shipment_to, moving.status, Employee.name from  storehouse,moving,Employee where  moving.id_storehouse_1=storehouse.id  and  Employee.id=moving.id_Employee and moving.id=";
                    sql6 += this.id.ToString();
                 

                    NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                    ds6.Reset();
                    da6.Fill(ds6);
                    dt6 = ds6.Tables[0];
                    dataGridView1.DataSource = dt6;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Номер накладной";
                    dataGridView1.Columns[2].HeaderText = "Склад отгрузки";
                    dataGridView1.Columns[3].HeaderText = "Склад постаки";
                    dataGridView1.Columns[4].HeaderText = "Дата оформления";
                    dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                    dataGridView1.Columns[6].HeaderText = "Дата отгрузки";
                    dataGridView1.Columns[7].HeaderText = "Дата поставки";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Обработчик";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                   

                }
               
                if (this.id_storehouse != "")
                {
                    comboBox2.Font = new Font("Arial", 11);
                    comboBox2.Text = "Склад отгрузки не выбран";
                    updatestorehouseinfoupdate(this.id_storehouse);
                   
                    comboBox2.Enabled = false;


                }
                else
                {
                    comboBox2.Font = new Font("Arial", 11);
                    comboBox2.Text = "Склад отгрузки не выбран";
                    comboBox2.Enabled = false;
                }
                if (this.id_storehouse_to != "")
                {
                    comboBox1.Font = new Font("Arial", 11);
                    comboBox1.Text = "Склад поставки не выбран";
                    updatestorehouseinfoupdate_to(this.id_storehouse_to);

                    comboBox1.Enabled = false;


                }
                else
                {
                    comboBox1.Font = new Font("Arial", 11);
                    comboBox1.Text = "Склад поставки не выбран";
                    comboBox1.Enabled = false;
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
                    comboBox4.Text = "Сотрудник не выбран";
                }
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label7.Font = new Font("Arial", 11);
                label8.Font = new Font("Arial", 11);
                label9.Font = new Font("Arial", 11);
                comboBox3.Font = new Font("Arial", 11);

                comboBox3.Text = "Не указано";



               
                textBox1.Font = new Font("Arial", 11);


                textBox2.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);


            }

            catch { }



        }
        private void newmoving_Load(object sender, EventArgs e)
        {
            try
            {
                Update();






                label1.Visible = false;

                textBox1.Visible = false;

                label7.Visible = false;
                comboBox3.Visible = false;
                dataGridView1.ReadOnly = true;
                dateTimePicker1.Visible = false;
                label4.Visible = false;




                if (id != -1)
                {
                    
                    updatestorehouseinfoupdate(this.id_storehouse);
                    updatestorehouseinfoupdate_to(this.id_storehouse_to);

                    textBox1.Text = this.num_invoices;

                    comboBox3.Text = status;


                    dateTimePicker2.Value = this.shipment;
                    dateTimePicker3.Value = this.shipment;
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

                    string sql = "Insert into moving (num_invoices, id_storehouse_1,id_storehouse_2,data,num_Contract,shipment,shipment_to,status,id_Employee) values (:num_invoices, :id_storehouse_1,:id_storehouse_2, :data, :num_Contract, :shipment,:shipment_to, :status, :id_Employee)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("num_invoices", (code_ + 1).ToString());
           
                    command.Parameters.AddWithValue("id_storehouse_1", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("data", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("num_Contract", textBox2.Text);
                    command.Parameters.AddWithValue("id_storehouse_2", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("shipment", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("shipment_to", dateTimePicker3.Value);
                    command.Parameters.AddWithValue("status", comboBox3.Text);
                    command.Parameters.AddWithValue("id_Employee", comboBox4.SelectedValue);


                    DialogResult result = MessageBox.Show("Вы уверены, что добавить  запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();

                        String sql6 = "Select moving.id,moving.num_invoices, storehouse.name,(select storehouse.name from storehouse where storehouse.id = moving.id_storehouse_2)  AS storehouse_to, moving.data,moving.num_Contract,moving.shipment,moving.shipment_to, moving.status, Employee.name from  storehouse,moving,Employee where  moving.id_storehouse_1=storehouse.id  and  Employee.id=moving.id_Employee  ORDER BY moving.id   DESC LIMIT 1   ";

                        NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                        ds6.Reset();
                        da6.Fill(ds6);
                        dt6 = ds6.Tables[0];
                        dataGridView1.DataSource = dt6;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "Номер накладной";
                        dataGridView1.Columns[2].HeaderText = "Склад отгрузки";
                        dataGridView1.Columns[3].HeaderText = "Склад постаки";
                        dataGridView1.Columns[4].HeaderText = "Дата оформления";
                        dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                        dataGridView1.Columns[6].HeaderText = "Дата отгрузки";
                        dataGridView1.Columns[7].HeaderText = "Дата поставки";
                        dataGridView1.Columns[8].Visible = false;
                        dataGridView1.Columns[9].HeaderText = "Обработчик";
                        this.StartPosition = FormStartPosition.CenterScreen;


                       
                        DialogResult result1 = MessageBox.Show("Добавить данные о товарах в накладную?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result1 == DialogResult.Yes)
                        {
                            String sql1 = "Select * from moving ORDER BY id DESC LIMIT 1 ;";
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

                                MessageBox.Show("Накладная перемещения не найдена.");
                            }
                            this.StartPosition = FormStartPosition.CenterScreen;
                            newmoving_info f = new newmoving_info(con, -1, id_i, "", "", 0,  comboBox2.Text, this.div);
                            f.ShowDialog();
                            Update();
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

                    string sql = "update moving set num_invoices=:num_invoices, id_storehouse_1=:id_storehouse_1,id_storehouse_2=:id_storehouse_2, data=:data,num_Contract=:num_Contract,shipment=:shipment,shipment_to=:shipment_to,status=:status,id_Employee=:id_Employee where id=:id;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("num_invoices", textBox1.Text);
        
                    command.Parameters.AddWithValue("id_storehouse_1", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("id_storehouse_2", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("data", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("num_Contract", textBox2.Text);

                    command.Parameters.AddWithValue("shipment", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("shipment_to", dateTimePicker3.Value);
                    command.Parameters.AddWithValue("status", comboBox3.Text);
                    command.Parameters.AddWithValue("id_Employee", comboBox4.SelectedValue);
                    command.Parameters.AddWithValue("id", this.id);
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        {
                            String sql6 = "Select moving.id,moving.num_invoices, storehouse.name,(select storehouse.name from storehouse where storehouse.id = moving.id_storehouse_2)  AS storehouse_to, moving.data,moving.num_Contract,moving.shipment,moving.shipment_to, moving.status, Employee.name from  storehouse,moving,Employee where  moving.id_storehouse_1=storehouse.id  and  Employee.id=moving.id_Employee and moving.id=";
                            sql6 += this.id.ToString();
                            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                            ds6.Reset();
                            da6.Fill(ds6);
                            dt6 = ds6.Tables[0];
                            dataGridView1.DataSource = dt6;
                            dataGridView1.Columns[0].Visible = false;
                  
                            dataGridView1.Columns[1].HeaderText = "Номер накладной";
                            dataGridView1.Columns[2].HeaderText = "Склад отгрузки";
                            dataGridView1.Columns[3].HeaderText = "Склад постаки";
                            dataGridView1.Columns[4].HeaderText = "Дата оформления";
                            dataGridView1.Columns[5].HeaderText = "Номер распоряжения";
                            dataGridView1.Columns[6].HeaderText = "Дата отгрузки";
                            dataGridView1.Columns[7].HeaderText = "Дата поставки";
                            dataGridView1.Columns[8].Visible = false;
                            dataGridView1.Columns[9].HeaderText = "Обработчик";
                            this.StartPosition = FormStartPosition.CenterScreen;

                        }
                        
                        DialogResult result1 = MessageBox.Show("Добавить данные о товарах в накладную?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result1 == DialogResult.Yes)
                        {

                            newmoving_info f = new newmoving_info(con, -1, this.id, "", "", 0,  comboBox2.Text, this.div);
                            f.ShowDialog();
                            Update();
                            Close();
                        }
                        
                            Update();

                    }
                    

                }
                catch
                {



                }
            }
    }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
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
                    comboBox2.Text = "Склад отгрузки не выбран";

                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name, this.div, "");
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatestorehouseinfo_to(fp.id_c);

                }
                else
                {
                    comboBox1.Text = "Склад поставки не выбран";

                }
            }
            catch { }
        }
    }
    }
    
