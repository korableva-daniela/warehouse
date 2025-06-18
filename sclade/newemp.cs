using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Npgsql;
using System.Threading;

using System.Text.RegularExpressions;
namespace sclade
{
    public partial class newemp : Form
    {
        public NpgsqlConnection con;
        public int id;
        public string name;
        public string phone;
        public string mail;
        public DateTime birthday;
        public string login;
        Regex regex1 = new Regex(@"^\d+$");

        public byte[] passw;
        public DateTime date_of_accept;
        
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        public string job;
        public string dep;
        public string div;
        public string acc;
        public DateTime date_of_appointment;
        public int sal;
        public int id_j_em;
        public byte[] salt;
        DataTable dt1 = new DataTable();
        DataSet ds1 = new DataSet();
        DataTable dt2 = new DataTable();
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
        DataSet ds8= new DataSet();
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt11 = new DataTable();
        DataSet ds11 = new DataSet();
        DataTable dt12 = new DataTable();
        DataSet ds12 = new DataSet();
        DataTable dt13 = new DataTable();
        DataSet ds13 = new DataSet();
        DataTable dt14 = new DataTable();
        DataSet ds14 = new DataSet();
        DataTable dt15 = new DataTable();
        DataSet ds15 = new DataSet();
        public string status;
        public newemp(NpgsqlConnection con, int id, string name, string phone, string mail, DateTime birthday, string login, byte[] passw, byte[] salt, DateTime date_of_accept, int id_j_em, string job, string dep, string div, string acc, DateTime date_of_appointment, int sal,string status)
        {
            this.id_j_em = id_j_em;
            this.con = con;
            this.id = id;
            this.name = name;
            this.phone = phone;
            this.birthday = birthday;
            this.mail = mail;
            this.login = login;
            this.passw = passw;
            this.date_of_accept = date_of_accept;
            this.job = job;
            this.dep = dep;
            this.div = div;
            this.acc = acc;
            this.date_of_appointment = date_of_appointment;
            this.sal = sal;
            InitializeComponent();
            this.salt = salt;
            this.status = status;
        }
        static byte[] Create_for_pass()
        {
            const int SaltLength = 64;
            byte[] salt = new byte[SaltLength];
            var rngRand = new RNGCryptoServiceProvider();
            rngRand.GetBytes(salt);

            return salt;
        }

        static byte[] GenerateSHA256Hash(string password, byte[] salt)
        {
            // Преобразуем пароль в байты
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            // Создаем массив для соли и пароля
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

            // Копируем соль в начало массива
            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            // Копируем пароль после соли
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

            // Создаем хеш-объект для SHA-256
            using (var sha256 = SHA256.Create())
            {
                // Вычисляем хеш и возвращаем его
                return sha256.ComputeHash(saltedPassword);
            }
        }

        public void updatejob(int id_j)
        {
            try
            {
                String sql1 = "Select * from Job where id=";
                sql1 += id_j.ToString();
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
            ds1.Reset();
            da1.Fill(ds1);
            dt1 = ds1.Tables[0];
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatejobupdate(string name)
        {
            try
            {
                String sql12 = "Select * from Job where name='";
                sql12 += name;
                sql12 += "'";
                NpgsqlDataAdapter da12 = new NpgsqlDataAdapter(sql12, con);
                ds12.Reset();
                da12.Fill(ds12);
                dt12 = ds12.Tables[0];
                comboBox1.DataSource = dt12;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateDepartment(int id_d)
            {
                try
                {
                    String sql2 = "Select * from Department where id=";
                sql2 += id_d.ToString();
                NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sql2, con);
            ds2.Reset();
            da2.Fill(ds2);
            dt2 = ds2.Tables[0];

            comboBox3.DataSource = dt2;
            comboBox3.DisplayMember = "name";
            comboBox3.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
            }
        public void updateDepartmentupdate(string name)
        {
            try
            {
                String sql14 = "Select * from Department  where name='";
                sql14 += name;
                sql14 += "'";
                NpgsqlDataAdapter da14 = new NpgsqlDataAdapter(sql14, con);
                ds14.Reset();
                da14.Fill(ds14);
                dt14 = ds14.Tables[0];
                comboBox3.DataSource = dt14;
                comboBox3.DisplayMember = "name";
                comboBox3.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateDivision(int id_d)
                {
                    try
                    {
                        String sql3 = "Select * from Division where id=";
                sql3 += id_d.ToString();
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
        public void updatedivisionupdate(string name)
        {
            try
            {
                String sql13 = "Select * from Division  where name='";
                sql13 += name;
                sql13 += "'";
                NpgsqlDataAdapter da13 = new NpgsqlDataAdapter(sql13, con);
                ds13.Reset();
                da13.Fill(ds13);
                dt13 = ds13.Tables[0];
                comboBox2.DataSource = dt13;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
        }
            catch { }
        }
        public void updateaccess_level(int id_a)
                    {
                        try
                        {
                            String sql4 = "Select * from access_level where id=";
                sql4 += id_a.ToString();
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
            ds4.Reset();
            da4.Fill(ds4);
            dt4 = ds4.Tables[0];
            comboBox5.DataSource = dt4;
            comboBox5.DisplayMember = "name";
            comboBox5.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
                        }
                        catch { }

                    }
        public void updateaccess_levelupdate(string name)
        {
            try
            {
                String sql15 = "Select * from access_level where name='";
                sql15 += name;
                sql15 += "'";
                NpgsqlDataAdapter da15 = new NpgsqlDataAdapter(sql15, con);
                ds15.Reset();
                da15.Fill(ds15);
                dt15 = ds15.Tables[0];
                comboBox5.DataSource = dt15;
                comboBox5.DisplayMember = "name";
                comboBox5.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }

        }

        private void newemp_Load(object sender, EventArgs e)
                        {
                            try
            {
                comboBox4.Font = new Font("Arial", 11);
                comboBox4.Text = "Активный";
                comboBox4.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                comboBox4.Enabled = true; // Сделать ComboBox доступным для выбора
                label14.Font = new Font("Arial", 11);
                textBox4.Text = "";
                textBox5.Text = "";
                button8.Visible = false;
                textBox5.PasswordChar = '*';
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                //comboBox4.Enabled = false;
                comboBox5.Enabled = false;
            
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                //comboBox4.Visible = false;
            label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            label3.Font = new Font("Arial", 11);
            label4.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
            label8.Font = new Font("Arial", 11);
            label9.Font = new Font("Arial", 11);
            label10.Font = new Font("Arial", 11);
            label11.Font = new Font("Arial", 11);
            label12.Font = new Font("Arial", 11);
            label13.Font = new Font("Arial", 11);

                label15.Font = new Font("Arial", 11);
            label16.Font = new Font("Arial", 11);
            label17.Font = new Font("Arial", 11);
            label18.Font = new Font("Arial", 11);
         
            textBox1.Font = new Font("Arial", 13);
            textBox2.Font = new Font("Arial", 13);
            textBox3.Font = new Font("Arial", 13);
            textBox6.Font = new Font("Arial", 13);
            textBox5.Font = new Font("Arial", 13);
            textBox4.Font = new Font("Arial", 13);
            textBox6.Font = new Font("Arial", 13);
           comboBox1.Font = new Font("Arial", 11);
            comboBox2.Font = new Font("Arial", 11);
            comboBox3.Font = new Font("Arial", 11);
           comboBox5.Font = new Font("Arial", 11);
    
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Update();
          
         
           

            if (this.id == -1)
                {
                    comboBox2.Text = "Подразделение не выбрано";
                    comboBox1.Text = "Должность не выбрана";
                    comboBox3.Text = "Департамент не выбран";
                    comboBox5.Text = "Уровень доступа не выбран";
                    button7.Visible = false;
                    button8.Visible = true;
                }
                if (this.id != -1)
                {
                    updateaccess_levelupdate(this.acc);
                    updateDepartmentupdate(this.dep);
                    updatejobupdate(this.job);
                    updatedivisionupdate(this.div);
                textBox1.BackColor = Color.LightGray;
                textBox2.BackColor = Color.LightGray;
                textBox3.BackColor = Color.LightGray;
                textBox4.Visible=false;
                textBox5.Visible =false;
                dateTimePicker1.BackColor = Color.LightGray;
                dateTimePicker2.BackColor = Color.LightGray;
                textBox1.Text = this.name;
                textBox2.Text = this.phone;
                textBox3.Text = this.mail;
                //textBox4.Text = this.login;
                //textBox5.Text = this.passw;
                dateTimePicker1.Value= this.birthday;
                dateTimePicker2.Value = this.date_of_accept;
                    comboBox4.Text = this.status;

              
        
                comboBox1.BackColor = Color.LightGray;
                comboBox2.BackColor = Color.LightGray;
                comboBox3.BackColor = Color.LightGray;
                comboBox5.BackColor = Color.LightGray;
                textBox6.BackColor = Color.LightGray;
                dateTimePicker3.BackColor = Color.LightGray;

                //comboBox3.Text = this.dep;
                dateTimePicker3.Value = this.date_of_appointment;

                textBox6.Text = this.sal.ToString();
                //comboBox5.Text = this.acc;
                String sql_ = "Select * from Employee where id=";
                sql_ += id.ToString();
                NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql_, con);
                ds10.Reset();
                da10.Fill(ds10);
                dt10 = ds10.Tables[0];

                dataGridView1.DataSource = dt10;
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
                String sql11 = "Select Job_em.id, Employee.id, Division.name," +
                 " Department.name,  Job.name,  access_level.name, Job_em.date_of_appointment,Job_em.sal  " +
                 "from Division, access_level, Employee, Department,Job,Job_em where Job_em.id_em =Employee.id and " +
                 "Job_em.id_j = Job.id and Job_em.id_dep = Department.id and Job_em.id_d = Division.id and" +
                 " Job_em.id_a = access_level.id and Employee.id = :id;";

                NpgsqlDataAdapter da11 = new NpgsqlDataAdapter(sql11, con);
                da11.SelectCommand.Parameters.AddWithValue("id", this.id);
                ds11.Reset();
                da11.Fill(ds11);
                dt11 = ds11.Tables[0];
                dataGridView2.DataSource = dt11;
                dataGridView2.Columns[0].Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            int employeeId = -1;
            if (this.id == -1)
            {

                try
                {


                    string sql = "Insert into Employee (name,phone, mail,birthday,login,passw,date_of_accept,salt,status) values (:name,:phone,:mail,:birthday,:login,:passw,:date_of_accept,:salt,:status);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("phone", textBox2.Text);
                    command.Parameters.AddWithValue("mail", textBox3.Text);
                    string login = textBox4.Text;
                    byte[] for_hash = Create_for_pass();

                    command.Parameters.AddWithValue("login", login);
                    string password = textBox5.Text;

                    byte[] hashed_pass = GenerateSHA256Hash(password, for_hash);
                    //string hashed_pass_str = Convert.ToBase64String(hashed_pass);
                    command.Parameters.AddWithValue("passw", hashed_pass);
                    command.Parameters.AddWithValue("birthday", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("date_of_accept", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("salt", for_hash);
                    command.Parameters.AddWithValue("status", comboBox4.Text);
                    /* sql = "Insert into Address_cl (id_client,country_cl,city_cl,street_cl,house_cl,post_in_cl) values (:id_client ,:country_cl, :city_cl, :street_cl, :house_cl, :post_in_cl);";
                     command.Parameters.AddWithValue("country", textBox4.Text);
                     command.Parameters.AddWithValue("city", textBox5.Text);
                     command.Parameters.AddWithValue("street", textBox6.Text);
                     command.Parameters.AddWithValue("house_cl", textBox7.Text);
                     command.Parameters.AddWithValue("post_in_cl", textBox8.Text);
                     command.Parameters.AddWithValue("id_client", this.id);
                     command.ExecuteNonQuery();*/


                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();


                        String sql1 = "Select * from Employee ORDER BY id DESC LIMIT 1 ;";
                        NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql1, con);
                        ds5.Reset();
                        da5.Fill(ds5);
                        dt5 = ds5.Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            employeeId = Convert.ToInt32(dt5.Rows[0]["id"]);

                        }
                        else
                        {

                            MessageBox.Show("Пользователь не найден.");
                        }
                        this.StartPosition = FormStartPosition.CenterScreen;
                        dataGridView1.DataSource = dt5;
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

                        if (employeeId != -1)
                        {
                            string sql2 = "Insert into Job_em (id_em,id_j,id_dep,id_d,id_a,date_of_appointment,sal) values (:id_em ,:id_j, :id_dep, :id_d,:id_a,:date_of_appointment,:sal)";

                            NpgsqlCommand command2 = new NpgsqlCommand(sql2, con);
                            command2.Parameters.AddWithValue("id_j", comboBox1.SelectedValue);
                            command2.Parameters.AddWithValue("id_dep", comboBox3.SelectedValue);
                            command2.Parameters.AddWithValue("id_d", comboBox2.SelectedValue);
                            command2.Parameters.AddWithValue("date_of_appointment", dateTimePicker3.Value);
                            command2.Parameters.AddWithValue("id_a", comboBox5.SelectedValue);
                            if (regex1.IsMatch(textBox6.Text) == false)
                            {



                                command2.Parameters.AddWithValue("sal", 0);
                            }
                            else
                            {
                                command2.Parameters.AddWithValue("sal", Convert.ToDouble(textBox6.Text));
                            }
                            command2.Parameters.AddWithValue("id_em", employeeId);


                            command2.ExecuteNonQuery();
                            String sql3 = "Select * from Employee ORDER BY id DESC LIMIT 1 ;";
                            NpgsqlDataAdapter da7 = new NpgsqlDataAdapter(sql3, con);
                            ds7.Reset();
                            da7.Fill(ds7);
                            dt7 = ds7.Tables[0];
                            if (dt7.Rows.Count > 0)
                            {
                                employeeId = Convert.ToInt32(dt7.Rows[0]["id"]);

                            }
                            else
                            {

                                MessageBox.Show("Пользователь не найден.");
                            }


                            dataGridView1.DataSource = dt7;
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
                            if (employeeId != -1)
                            {
                                String sql6 = "Select Job_em.id, Employee.id, Division.name," +
                                 " Department.name,  Job.name,  access_level.name, Job_em.date_of_appointment,Job_em.sal  " +
                                 "from Division, access_level, Employee, Department,Job,Job_em where Job_em.id_em =Employee.id and " +
                                 "Job_em.id_j = Job.id and Job_em.id_dep = Department.id and Job_em.id_d = Division.id and" +
                                 " Job_em.id_a = access_level.id and Employee.id = :id;";

                                NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                                da6.SelectCommand.Parameters.AddWithValue("id", employeeId);
                                ds6.Reset();
                                da6.Fill(ds6);
                                dt6 = ds6.Tables[0];
                                dataGridView2.DataSource = dt6;
                                dataGridView2.Columns[0].Visible = false;
                                dataGridView2.Columns[1].Visible = false
                                    ;
                                dataGridView2.Columns[2].HeaderText = "Подразделение";
                                dataGridView2.Columns[3].HeaderText = "Департамент";
                                dataGridView2.Columns[4].HeaderText = "Должность";
                                dataGridView2.Columns[5].HeaderText = "Уровень доступа";
                                dataGridView2.Columns[6].HeaderText = "Дата назначения";
                                dataGridView2.Columns[7].HeaderText = "Зарплата";

                            }
                        }
                        else
                            Update();


                    }
                }
                catch
                {
                    DialogResult result5 = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataTable dt200 = new DataTable();
                    DataSet ds200 = new DataSet();

                    String sql3 = "Select * from Employee ORDER BY id DESC LIMIT 1 ;";
                    NpgsqlDataAdapter da200 = new NpgsqlDataAdapter(sql3, con);
                    ds200.Reset();
                    da200.Fill(ds200);
                    dt200 = ds200.Tables[0];
                    if (dt200.Rows.Count > 0)
                    {
                        int id = Convert.ToInt32(dt200.Rows[0]["id"]);
                        NpgsqlCommand command5 = new NpgsqlCommand("DELETE FROM Employee WHERE id=:id", con);

                        command5.Parameters.AddWithValue("id", id);

                        command5.ExecuteNonQuery();

                    }
                }
            }
            else
            {

                try
                {


                    string sql9 = "update Employee set name=:name, phone=:phone, mail=:mail,birthday=:birthday, login=:login, passw=:passw,  date_of_accept=:date_of_accept,salt=:salt,status=:status  where id=:id;";
                    NpgsqlCommand command9 = new NpgsqlCommand(sql9, con);
                    command9.Parameters.AddWithValue("name", textBox1.Text);
                    command9.Parameters.AddWithValue("phone", textBox2.Text);
                    command9.Parameters.AddWithValue("mail", textBox3.Text);

                    command9.Parameters.AddWithValue("birthday", dateTimePicker1.Value);
                    command9.Parameters.AddWithValue("date_of_accept", dateTimePicker2.Value);


                    command9.Parameters.AddWithValue("id", this.id);
                    command9.Parameters.AddWithValue("status", comboBox4.Text);

                    if ((textBox5.Text == ""))
                    {
                        command9.Parameters.AddWithValue("login", this.login);

                        command9.Parameters.AddWithValue("passw", this.passw);
                        command9.Parameters.AddWithValue("salt", this.salt);
                    }
                    else
                    {
                        string login = textBox4.Text;
                        byte[] for_hash = Create_for_pass();

                        command9.Parameters.AddWithValue("login", login);
                        string password = textBox5.Text;

                        byte[] hashed_pass = GenerateSHA256Hash(password, for_hash);


                        command9.Parameters.AddWithValue("passw", hashed_pass);
                        command9.Parameters.AddWithValue("salt", for_hash);
                    }

                    /*sql = "update Address_cl  set id_client=:id_client, country_cl=:country_cl, city_cl=: city_cl,street_cl=:street_cl, house_cl=:house_cl,post_in_cl=:post_in_cl  where id=:id_a and id_client=:id";
                    command.Parameters.AddWithValue("country", textBox4.Text);
                    command.Parameters.AddWithValue("city", textBox5.Text);
                    command.Parameters.AddWithValue("street", textBox6.Text);
                    command.Parameters.AddWithValue("house_cl", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_cl", textBox8.Text);
                    command.Parameters.AddWithValue("id_client", this.id);
                    command.Parameters.AddWithValue("id_a", this.id_a);*/
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command9.ExecuteNonQuery();
                        string sql8 = "update Job_em  set id_em=:id_em,id_j=:id_j,id_dep=:id_dep,id_d=:id_d,id_a=:id_a,date_of_appointment=:date_of_appointment,sal=:sal where id=:id and id_em=:id_em";
                        NpgsqlCommand command8 = new NpgsqlCommand(sql8, con);

                        command8.Parameters.AddWithValue("id_j", comboBox1.SelectedValue);
                        command8.Parameters.AddWithValue("id_dep", comboBox3.SelectedValue);
                        command8.Parameters.AddWithValue("id_d", comboBox2.SelectedValue);
                        command8.Parameters.AddWithValue("date_of_appointment", dateTimePicker3.Value);
                        command8.Parameters.AddWithValue("id_a", comboBox5.SelectedValue);
                        if (regex1.IsMatch(textBox6.Text) == false)
                        {



                            command8.Parameters.AddWithValue("sal", 0);
                        }
                        else
                        {
                            command8.Parameters.AddWithValue("sal", Convert.ToDouble(textBox6.Text));
                        }
                        command8.Parameters.AddWithValue("id_em", this.id);
                        command8.Parameters.AddWithValue("id", this.id_j_em);

                        command8.ExecuteNonQuery();
                        String sql88 = "Select * from Employee where id=";
                        sql88 += id.ToString();
                        NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql88, con);
                        ds8.Reset();
                        da8.Fill(ds8);
                        dt8 = ds8.Tables[0];
                        if (dt8.Rows.Count > 0)
                        {
                            employeeId = Convert.ToInt32(dt8.Rows[0]["id"]);

                        }
                        else
                        {

                            MessageBox.Show("Пользователь не найден.");
                        }
                        dataGridView1.DataSource = dt8;
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
                        //if (employeeId != -1)
                        //{
                        String sql99 = "Select Job_em.id, Employee.id, Division.name," +
                     " Department.name,  Job.name,  access_level.name, Job_em.date_of_appointment, Job_em.sal  " +
                     "from Division, access_level, Employee, Department, Job, Job_em where Job_em.id_em =Employee.id and " +
                     "Job_em.id_j = Job.id and Job_em.id_dep = Department.id and Job_em.id_d = Division.id and" +
                     " Job_em.id_a = access_level.id and Employee.id = :id ";

                        NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql99, con);
                        da9.SelectCommand.Parameters.AddWithValue("id", this.id);
                        da9.SelectCommand.Parameters.AddWithValue("id_j_em", this.id_j_em);
                        ds9.Reset();
                        da9.Fill(ds9);
                        dt9 = ds9.Tables[0];
                        dataGridView2.DataSource = dt9;
                        dataGridView2.Columns[0].Visible = false;
                        dataGridView2.Columns[1].Visible = false;
                        dataGridView2.Columns[2].HeaderText = "Подразделение";
                        dataGridView2.Columns[3].HeaderText = "Департамент";
                        dataGridView2.Columns[4].HeaderText = "Должность";
                        dataGridView2.Columns[5].HeaderText = "Уровень доступа";
                        dataGridView2.Columns[6].HeaderText = "Дата назначения";
                        dataGridView2.Columns[7].HeaderText = "Зарплата";
                        this.StartPosition = FormStartPosition.CenterScreen;
                        //}
                    }
                    else
                        Update();

                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Job_in fp = new Job_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            division_in fp = new division_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            department_in fp = new department_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            access_level_in fp = new access_level_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {


                int id_j = 0;
                string name = "";
                Job_in fp = new Job_in(con, id_j, name);

                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatejob(fp.id);

                }
                else
                {
                    comboBox1.Text = "Должность не выбрана";

                }
            }
            catch { }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {


                int id_d = 0;
                string name = "";
                division_in fp = new division_in(con, id_d, name);

                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateDivision(fp.id_d);

                }
                else
                {
                    comboBox2.Text = "Подразделение не выбрано";

                }
            }
            catch { }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {


                int id_d = 0;
                string name = "";
                department_in fp = new department_in(con, id_d, name);

                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateDepartment(fp.id);

                }
                else
                {
                    comboBox3.Text = "Департамент не выбран";

                }
            }
            catch { }
           
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {


                int id_d = 0;
                string name = "";
                access_level_in fp = new access_level_in(con, id_d, name);

                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateaccess_level(fp.id);

                }
                else
                {
                    comboBox5.Text = "Уровень доступа не выбран";

                }
            }
            catch { }
        
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox5.Text = "";
            textBox4.Visible = true;
            textBox5.Visible = true;
            button8.Visible = true;
            

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox5.PasswordChar != '\0')
            {
                textBox5.PasswordChar = '\0';
            }
            else
                textBox5.PasswordChar = '*';
        }
    }
}
