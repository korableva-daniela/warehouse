using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class employee : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public int id;
        DataTable dt1 = new DataTable();
        DataSet ds1 = new DataSet();
        public string name;
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        byte[] binaryData ;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        List<String> messages = new List<String>();
        public employee(NpgsqlConnection con, int id, string name )
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
            try
            {
                dataGridView3.Visible = false;
                dataGridView2.Visible = false;
                if (id == 0)
                {
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = false;
                    textBox5.Visible = false;
                    textBox6.Visible = false;
                    textBox7.Visible = false;
                    label1.Visible = false;
                    label3.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;
                }
                if (id != 0)
                {
                    this.WindowState = FormWindowState.Maximized;
                    button1.Visible = false;
                }
                label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            label3.Font = new Font("Arial", 11);
            label4.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
            label8.Font = new Font("Arial", 11);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (textBox1.Text == "")
            {
                String sql = "Select *  from Employee   ORDER BY name ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Телефон";
                dataGridView1.Columns[3].HeaderText = "Почта";
                dataGridView1.Columns[4].HeaderText = "Дата рождения";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Дата принятия на работу";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Статус";

                    this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                String sql = "Select *  from Employee where  name ILIKE '";
                sql += textBox1.Text;
                sql += "%' ORDER BY name ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible= false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Телефон";
                dataGridView1.Columns[3].HeaderText = "Почта";
                dataGridView1.Columns[4].HeaderText = "Дата рождения";
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].HeaderText = "Дата принятия на работу";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Статус";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                String sql1 = "Select Job_em.id, Employee.id, Division.name," +
                 " Department.name,  Job.name,  access_level.name, Job_em.date_of_appointment,Job_em.sal  " +
                 "from Division, access_level, Employee, Department,Job,Job_em where Job_em.id_em =Employee.id and " +
                 "Job_em.id_j = Job.id and Job_em.id_dep = Department.id and Job_em.id_d = Division.id and" +
                 " Job_em.id_a = access_level.id ;";

                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);

                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                dataGridView3.DataSource = dt1;
                dataGridView3.Columns[0].Visible = false;
                dataGridView3.Columns[1].Visible = false;
                dataGridView3.Columns[2].HeaderText = "Подразделение";
                dataGridView3.Columns[3].HeaderText = "Департамент";
                dataGridView3.Columns[4].HeaderText = "Должность";
                dataGridView3.Columns[5].HeaderText = "Уровень доступа";
                dataGridView3.Columns[6].HeaderText = "Дата назначения";
                dataGridView3.Columns[7].HeaderText = "Зарплата";


                this.StartPosition = FormStartPosition.CenterScreen;
            }

            catch { }
        }
        public void updatejobinfo(int id)
        {
            try
            {
                if (id != null)
                {
                    dataGridView3.Visible = false;
                    dataGridView2.Visible = false;
                    dataGridView2.Visible = false;
                    String sqli = "Select Job_em.id, Employee.id, Division.name," +
                        " Department.name,  Job.name,  access_level.name, Job_em.date_of_appointment,Job_em.sal  " +
                        "from Division, access_level, Employee, Department,Job,Job_em where Job_em.id_em =Employee.id and " +
                        "Job_em.id_j = Job.id and Job_em.id_dep = Department.id and Job_em.id_d = Division.id and" +
                        " Job_em.id_a = access_level.id and Employee.id = :id;";

                    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                    dai.SelectCommand.Parameters.AddWithValue("id", id);
                    dsi.Reset();
                    dai.Fill(dsi);
                    dti = dsi.Tables[0];
                    dataGridView2.DataSource = dti;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].HeaderText = "Подразделение";
                    dataGridView2.Columns[3].HeaderText = "Департамент";
                    dataGridView2.Columns[4].HeaderText = "Должность";
                    dataGridView2.Columns[5].HeaderText = "Уровень доступа";
                    dataGridView2.Columns[6].HeaderText = "Дата назначения";
                    dataGridView2.Columns[7].HeaderText = "Зарплата";


                    this.StartPosition = FormStartPosition.CenterScreen;
                    if (dataGridView1.CurrentRow != null)
                    {
                        //if (dataGridView1.CurrentRow != null)
                        //{

                        string d = (string)dataGridView2.Rows[0].Cells[2].Value;
                        string dep = (string)dataGridView2.Rows[0].Cells[3].Value;
                        string j = (string)dataGridView2.Rows[0].Cells[4].Value;
                        string a = (string)dataGridView2.Rows[0].Cells[5].Value;
                        DateTime date_of_accept = (DateTime)dataGridView2.Rows[0].Cells[6].Value;
                        int sal = (int)dataGridView2.Rows[0].Cells[7].Value;




                        textBox2.Text = d;
                        textBox3.Text = dep;
                        textBox6.Text = date_of_accept.Date.ToString("dd.MM.yyyy");

                        textBox5.Text = a;
                        textBox4.Text = j;
                        textBox7.Text = sal.ToString();

                        /* }*/
                    }
                    else
                    {
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
                textBox2.Font = new Font("Arial", 13);
                textBox3.Font = new Font("Arial", 13);
                textBox6.Font = new Font("Arial", 13);
                textBox5.Font = new Font("Arial", 13);
                textBox4.Font = new Font("Arial", 13);
                textBox7.Font = new Font("Arial", 13);
            }
                else
                {
                    String sqli = "Select Job_em.id, Employee.id, Division.name," +
                        " Department.name,  Job.name,  access_level.name, Job_em.date_of_appointment,Job_em.sal  " +
                        "from Division, access_level, Employee, Department,Job,Job_em where Job_em.id_em =Employee.id and " +
                        "Job_em.id_j = Job.id and Job_em.id_dep = Department.id and Job_em.id_d = Division.id and" +
                        " Job_em.id_a = access_level.id ;";

                    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);

                    dsi.Reset();
                    dai.Fill(dsi);
                    dti = dsi.Tables[0];
                    dataGridView2.DataSource = dti;
                    dataGridView2.Columns[0].Visible=false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].HeaderText = "Подразделение";
                    dataGridView2.Columns[3].HeaderText = "Департамент";
                    dataGridView2.Columns[4].HeaderText = "Должность";
                    dataGridView2.Columns[5].HeaderText = "Уровень доступа";
                    dataGridView2.Columns[6].HeaderText = "Дата назначения";
                    dataGridView2.Columns[7].HeaderText = "Зарплата";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch { }
        }
        public void Update_filt(List<string> messages)
        {
            dataGridView3.Visible = false;
            dataGridView2.Visible = false;
            if (messages.Count == 0)
            {


                Update();
                var filterRows = dt.AsEnumerable()
                     //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                     .Where(row => ((row.Field<string>("status") != "Уволен")));


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
                    MessageBox.Show("Сотрудников с таким статусом нет.");
                    var originalTable = (DataTable)dataGridView1.DataSource;
                    dataGridView1.DataSource = null; // Очищаем DataSource
                    dataGridView1.DataSource = originalTable.Clone();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "ФИО";
                    dataGridView1.Columns[2].HeaderText = "Телефон";
                    dataGridView1.Columns[3].HeaderText = "Почта";
                    dataGridView1.Columns[4].HeaderText = "Дата рождения";
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].HeaderText = "Дата принятия на работу";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Статус";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
            }
            if (messages.Count == 1)
            {
               
                

                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ( (row.Field<string>("status") == messages[0])));


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
                        MessageBox.Show("Сотрудников с таким статусом нет.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "ФИО";
                    dataGridView1.Columns[2].HeaderText = "Телефон";
                    dataGridView1.Columns[3].HeaderText = "Почта";
                    dataGridView1.Columns[4].HeaderText = "Дата рождения";
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].HeaderText = "Дата принятия на работу";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Статус";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
                
            }
            if (messages.Count == 2)
            {
   

                
                    var filterRows = dt.AsEnumerable()
                    //.Where(row => row.Field<string>("product_code") == comboBox1.Text &&  row.Field<DateTime>("shipment_date") > startDate && row.Field<DateTime>("shipment_date") < endDate);
                    .Where(row => ((row.Field<string>("status") == messages[0]) || (row.Field<string>("status") == messages[1])));


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
                        MessageBox.Show("Сотрудников с таким статусом нет.");
                        var originalTable = (DataTable)dataGridView1.DataSource;
                        dataGridView1.DataSource = null; // Очищаем DataSource
                        dataGridView1.DataSource = originalTable.Clone();
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "ФИО";
                    dataGridView1.Columns[2].HeaderText = "Телефон";
                    dataGridView1.Columns[3].HeaderText = "Почта";
                    dataGridView1.Columns[4].HeaderText = "Дата рождения";
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].HeaderText = "Дата принятия на работу";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].HeaderText = "Статус";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
                
            }
        }

        private void employee_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView3.Visible = false;
                dataGridView2.Visible = false;
                textBox7.Visible = false;
                label8.Visible = false;
                comboBox4.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                comboBox4.Enabled = true; // Сделать ComboBox доступным для выбора
                label4.Font = new Font("Arial", 11);
                comboBox4.Font = new Font("Arial", 11);

                comboBox4.Text = "Все действующие сотрудники";
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                dataGridView3.Visible = false;
                dataGridView2.Visible = false;
                dataGridView1.ReadOnly = true;
                Update();
                Update_filt(messages);

            }
            catch { }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void личныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
                try
                {
                    newemp f = new newemp(con, -1, "", "", "", DateTime.Today,"", binaryData, binaryData, DateTime.Today,-1,  "", "", "", "", DateTime.Today, 0,"");
            f.ShowDialog();
            Update();
                }

                catch { }
            }

        private void личныеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            string name = (string)dataGridView1.CurrentRow.Cells[1].Value;
            string phone = (string)dataGridView1.CurrentRow.Cells[2].Value;
            string mail = (string)dataGridView1.CurrentRow.Cells[3].Value;
            DateTime birthday = (DateTime)dataGridView1.CurrentRow.Cells[4].Value;
            
            string login = (string)dataGridView1.CurrentRow.Cells[5].Value;
            byte[] passw = (byte[])dataGridView1.CurrentRow.Cells[6].Value;
            DateTime date_of_accept = (DateTime)dataGridView1.CurrentRow.Cells[7].Value;
            int id_j_em = (int)dataGridView2.CurrentRow.Cells[0].Value;
            string div = (string)dataGridView2.CurrentRow.Cells[2].Value;
            string dep = (string)dataGridView2.CurrentRow.Cells[3].Value;
            string job = (string)dataGridView2.CurrentRow.Cells[4].Value;
            string acc = (string)dataGridView2.CurrentRow.Cells[5].Value;
            DateTime date_of_appointment = (DateTime)dataGridView2.CurrentRow.Cells[6].Value;
            string status = (string)dataGridView1.CurrentRow.Cells[9].Value;
            int sal=(int)dataGridView2.CurrentRow.Cells[7].Value;
            byte[] salt = (byte[])dataGridView1.CurrentRow.Cells[8].Value;


                newemp f = new newemp(con, id, name, phone, mail, birthday, login, passw, salt, date_of_accept,id_j_em, job, dep, div, acc, DateTime.Today, sal,status);
            f.ShowDialog();
            Update();
            updatejobinfo(id);
        //}

        //            catch { }
                }

        private void личныеДанныеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
            try
            {
                //int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                //NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Employee WHERE id=:id", con);
                //NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM  Job_em   WHERE id_em=:id", con);
                //command.Parameters.AddWithValue("id", id);
                //command1.Parameters.AddWithValue("id", id);
                //DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
                //    command1.ExecuteNonQuery();
                //    command.ExecuteNonQuery();
                //    Update();
                //}
                //else
                //    Update();
                //updatejobinfo(id);
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                string sql9 = "update Employee set status=:status  where id=:id;";
                NpgsqlCommand command = new NpgsqlCommand(sql9, con);
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("status", "Уволен");
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                   
                    command.ExecuteNonQuery();
                    Update();
                }
                else
                    Update();
                updatejobinfo(id);

                //int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                //NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Employee WHERE id=:id", con);
                //NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM  Job_em   WHERE id_em=:id", con);
                //command.Parameters.AddWithValue("id", id);
                //command1.Parameters.AddWithValue("id", id);
                //DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
                //    command1.ExecuteNonQuery();
                //    command.ExecuteNonQuery();
                //    Update();
                //}
                //else
                //    Update();
                //updatejobinfo(id);
                Update();
            }

            catch { }
        }

        private void адресToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {

                //        int id;
                //        if (dataGridView1.CurrentRow != null)
                //            if (dataGridView1.CurrentRow.Index != 0)
                //            {
                //                id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                //            }
                //            else id = 1;
                //        else id = dataGridView1.RowCount;
                //        updatejobinfo(id);
                //    }
                //    catch { }
                //}





                int id;
                if (dataGridView1.CurrentRow != null)
                {
                    //if (dataGridView1.CurrentRow.Index != 0)
                    //{
                    id = (int)dataGridView1.CurrentRow.Cells[0].Value;

                    //    }
                    //    else
                    //    //id = 1;
                    //    {


                    //        String sql1 = "Select * from Employee  ORDER BY name ASC LIMIT 1 ;";
                    //        NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                    //        ds6.Reset();
                    //        da6.Fill(ds6);
                    //        dt6 = ds6.Tables[0];
                    //        if (dt6.Rows.Count > 0)
                    //        {
                    //            id = Convert.ToInt32(dt6.Rows[0]["id"]);

                    //        }
                    //        else { id = 1; }

                    //    }
                }
                else
                {
                    String sql1 = "Select * from Employee  ORDER BY name ASC LIMIT 1 ;";
                    NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                    ds6.Reset();
                    da6.Fill(ds6);
                    dt6 = ds6.Tables[0];
                    if (dt6.Rows.Count > 0)
                    {
                        id = Convert.ToInt32(dt6.Rows[0]["id"]);

                    }
                    id = dataGridView1.RowCount;
                }
                updatejobinfo(id);
            }

            catch { }
        }

        private void информациюОДолжэностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
                        
                 
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

        private void button7_Click(object sender, EventArgs e)
        {
            filter_emp fp = new filter_emp(con);
            fp.Show();
        }
        private void ExportToExcel(DataGridView dataGridView1, DataGridView dataGridView2, DataGridView dataGridView3, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;

                int tmp = 0;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)

                {
                    if (i == 5 || i == 6 || i == 8)
                    {

                    }

                    else
                    {
                        worksheet.Cells[1, h] = dataGridView1.Columns[i].HeaderText;
                        h++;
                    }
                    tmp = h;
                }


                for (int i = 2; i < dataGridView2.Columns.Count; i++)

                {
                    worksheet.Cells[1, h] = dataGridView2.Columns[i].HeaderText;
                    h++;
                }



                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {

                    int m = 1;
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {
                        if (j == 5 || j == 6 || j == 8)
                        {

                        }

                        else
                        {

                            worksheet.Cells[2, m] = dataGridView1.Rows[1].Cells[j].Value?.ToString();
                            m++;
                        }


                    }

                    for (int j = 2; j < dataGridView2.Columns.Count; j++)
                    {
                        {

                            worksheet.Cells[2, m] = dataGridView2.Rows[0].Cells[j].Value?.ToString();
                            m++;
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        int m = 1;
                        for (int j = 1; j < dataGridView1.Columns.Count; j++)
                        {
                            if (j == 5 || j == 6 || j == 8)
                            {

                            }

                            else
                            {

                                worksheet.Cells[i + 2, m] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                                m++;
                            }


                        }

                        for (int j = 2; j < dataGridView3.Columns.Count; j++)
                        {
                            {

                                worksheet.Cells[i + 2, m] = dataGridView3.Rows[i].Cells[j].Value?.ToString();
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
        private void ExportToExcel_all(DataGridView dataGridView1, DataGridView dataGridView3, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;
                int tmp = 0;
                for (int i = 1; i < dataGridView1.Columns.Count; i++)

                {
                    if (i == 5 || i == 6 || i == 8)
                    {

                    }

                    else
                    {
                        worksheet.Cells[1, h] = dataGridView1.Columns[i].HeaderText;
                        h++;
                    }
                    tmp = h;
                }


                for (int i = 2; i < dataGridView3.Columns.Count; i++)

                {
                    worksheet.Cells[1, h] = dataGridView3.Columns[i].HeaderText;
                    h++;
                }


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {
                        if (j == 5 || j == 6||j==8)
                        {

                        }

                        else
                        {

                            worksheet.Cells[i + 2, m] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                            m++;
                        }


                    }

                    for (int j = 2; j < dataGridView3.Columns.Count; j++)
                    {
                        {

                            worksheet.Cells[i + 2, m] = dataGridView3.Rows[i].Cells[j].Value?.ToString();
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
                        int m = 1;
                        var data = new Dictionary<string, object>();
                        for (int j = 1; j < dataGridView.Columns.Count; j++)
                        {

                            if (dataGridView.Columns[j].Visible == true)
                            {
                                data[dataGridView.Columns[j].HeaderText] = row.Cells[j].Value ?? ""; // Добавляем данные в словарь
                            }

                        }
                        for (int j = 2; j < dataGridView3.Columns.Count; j++)
                        {

                            if (dataGridView3.Columns[j].Visible == true)
                            {
                                data[dataGridView3.Columns[j].HeaderText] = dataGridView3.Rows[row.Index].Cells[j].Value ?? ""; // Добавляем данные в словарь
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
                        data[dataGridView.Columns[j].HeaderText] = dataGridView1.CurrentRow.Cells[j].Value ?? ""; // Добавляем данные в словарь
                    }

                }
                for (int j = 2; j < dataGridView3.Columns.Count; j++)
                {

                    if (dataGridView3.Columns[j].Visible == true)
                    {
                        data[dataGridView3.Columns[j].HeaderText] = dataGridView3.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value ?? ""; // Добавляем данные в словарь
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
                titleParagraph.Range.Text = "Сотрудники";
                titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph.Range.Font.Size = 12;

                titleParagraph.Range.InsertParagraphAfter();

                int visibleColumnCount = 0;
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    if (dataGridView.Columns[i].Visible)
                        visibleColumnCount++;
                }
                for (int i = 0; i < dataGridView3.Columns.Count; i++)
                {
                    if (dataGridView3.Columns[i].Visible)
                        visibleColumnCount++;
                }
                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count+ dataGridView3.Columns.Count-6);
               
                int h = 1;
                int tmp = 0;
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
                    tmp = h;
                }


                for (int i = 2; i < dataGridView3.Columns.Count; i++)

                {
                    table.Cell(1, h).Range.Text = dataGridView3.Columns[i].HeaderText;
                    table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                    table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    table.Cell(1, h).Range.Font.Size = 8;
                    h++;
                }
                // Добавляем заголовки столбцов
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {
                        if (j == 5 || j == 6 || j == 8)
                        {

                        }

                        else
                        {

                            table.Cell(i + 2, m).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(i + 2, m).Range.Font.Size = 8;
                            m++;
                        }


                    }

                    for (int j = 2; j < dataGridView3.Columns.Count; j++)
                    {
                        {
                            table.Cell(i + 2, m).Range.Text = dataGridView3.Rows[i].Cells[j].Value?.ToString();
                            table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(i + 2, m).Range.Font.Size = 8;
                          
                            m++;
                        }

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
                    titleParagraph.Range.Text = "Сотрудник";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();


                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count+ dataGridView3.Columns.Count - 6);
                    //int tmp = 0;
                    //for (int i = 1; i < dataGridView1.Columns.Count; i++)

                    //{
                    //    if (i == 5 || i == 6 || i == 8)
                    //    {

                    //    }

                    //    else
                    //    {
                    //        worksheet.Cells[1, h] = dataGridView1.Columns[i].HeaderText;
                    //        h++;
                    //    }
                    //    tmp = h;
                    //}


                    //for (int i = 2; i < dataGridView2.Columns.Count; i++)

                    //{
                    //    worksheet.Cells[1, h] = dataGridView2.Columns[i].HeaderText;
                    //    h++;
                    //}



                    //if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    //{

                    //    int m = 1;
                    //    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    //    {
                    //        if (j == 5 || j == 6 || j == 8)
                    //        {

                    //        }

                    //        else
                    //        {

                    //            worksheet.Cells[2, m] = dataGridView1.Rows[1].Cells[j].Value?.ToString();
                    //            m++;
                    //        }


                    //    }

                    //    for (int j = 2; j < dataGridView2.Columns.Count; j++)
                    //    {
                    //        {

                    //            worksheet.Cells[2, m] = dataGridView2.Rows[0].Cells[j].Value?.ToString();
                    //            m++;
                    //        }

                    //    }
                    //}
                    // Добавляем заголовки столбцов
                    int h = 1;
                    int tmp = 0;
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
                        tmp = h;
                    }


                    for (int i = 2; i < dataGridView2.Columns.Count; i++)

                    {
                        table.Cell(1, h).Range.Text = dataGridView2.Columns[i].HeaderText;
                        table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, h).Range.Font.Size = 8;
                        h++;
                    }
                    // Заполняем таблицу данными
                    // Добавляем заголовки столбцов


                    int m = 1;
                        for (int j = 1; j < dataGridView.Columns.Count; j++)
                        {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            table.Cell(2, m).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                            table.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(2, m).Range.Font.Size = 8;
                            m++;
                            }


                        }

                    for (int j = 2; j < dataGridView2.Columns.Count; j++)
                    {
                        {
                            
                            table.Cell(2, m).Range.Text = dataGridView2.Rows[0].Cells[j].Value?.ToString();
                            table.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(2, m).Range.Font.Size = 8;

                            m++;
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
        private void button4_Click(object sender, EventArgs e)
        {

          
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void вExcelДанныеВыбранногоПодразделенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void выгрузитьВExcelВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void просмотретьДанныеУволеныхСотрудниковToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
           
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = 0;

            messages.Clear();

            string name_in = "";
            if (comboBox4.Text == "Все действующие сотрудники")
            {
                Update_filt(messages);
            }
            if (comboBox4.Text == "Активный")
            {
                messages.Add("Активный");
                Update_filt(messages);
            }
            if (comboBox4.Text == "На испытательном сроке")
            {
                messages.Add("На испытательном сроке");
                Update_filt(messages);
            }
            if (comboBox4.Text == "В отпуске")
            {
                messages.Add("В отпуске");
                Update_filt(messages);
            }
            if (comboBox4.Text == "Уволен")
            {
                messages.Add("Уволен");
                Update_filt(messages);
            }
            if (comboBox4.Text == "В декретном отпусе")
            {
                messages.Add("В декретном отпусе");
                Update_filt(messages);
            }
            if (comboBox4.Text == "На больничном")
            {
                messages.Add("На больничном");
                Update_filt(messages);
            }
            if (comboBox4.Text == "В командировке")
            {
                messages.Add("В командировке");
                Update_filt(messages);
            }
            if (comboBox4.Text == "На обучении")
            {
                messages.Add("На обучении");
                Update_filt(messages);
            }
            if (comboBox4.Text == "Неактивный")
            {
                messages.Add("Неактивный");
                Update_filt(messages);
            }
            //if (comboBox4.Text == "Перемещение")
            //{
            //    messages.Add("Перемещение со склада");
            //    messages.Add("Перемещение на склад");
            //    Update_filt(messages);
            //}
            //if (comboBox4.Text == "Перемещение со склада")
            //{

            //    messages.Add("Перемещение со склада");
            //    Update_filt(messages);
            //}
            //if (comboBox4.Text == "Перемещение на склад")
            //{
            //    messages.Add("Перемещение на склад");
            //    Update_filt(messages);
            //}
            //if (comboBox4.Text == "Приходные и Перемещение на склад")
            //{
            //    messages.Add("Приходная");
            //    messages.Add("Перемещение на склад");
            //    Update_filt(messages);
            //}
            //if (comboBox4.Text == "Расходные и Перемещение со склада")
            //{
            //    messages.Add("Расходная");
            //    messages.Add("Перемещение со склада");
            //    Update_filt(messages);
            //}
            //if (comboBox4.Text == "Приходные и Расходные")
            //{
            //    messages.Add("Приходная");
            //    messages.Add("Расходная");
            //    Update_filt(messages);
            //}
        }

        private void вWordДанныеВыбранногоТипаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void вWordВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
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

                    saveFileDialog.FileName = "Staff_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel_all(dataGridView1, dataGridView3, saveFileDialog.FileName);
                    }
                }
            }
            catch { }
        }

        private void вExcelИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow != null)
                {

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;
                        string name = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Employee_" + name.Replace(" ", "_") + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel(dataGridView1, dataGridView2, dataGridView3, saveFileDialog.FileName);
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
                    saveFileDialog.FileName = "Staff_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToWord_all(dataGridView1, dataGridView3, saveFileDialog.FileName);
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
                        saveFileDialog.FileName = "Employee_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWord(dataGridView1, dataGridView3, saveFileDialog.FileName);
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
                    saveFileDialog.FileName = $"Staff_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Вызываем метод экспорта с выбранным путем
                        ExportJSON_all(dataGridView1, dataGridView3, saveFileDialog.FileName);
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
                        saveFileDialog.FileName = $"Employee_{code.Replace(" ", "_")}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
    }

