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
namespace sclade
{
    public partial class getting_started : Form
    {
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
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
        public NpgsqlConnection con;
        public getting_started(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
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


        private void getting_started_Load(object sender, EventArgs e)
        {
            button2.Visible = false;

            textBox2.PasswordChar = '*';
            //textBox2.UseSystemPasswordChar = true;
            this.ControlBox = false;
            label3.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
            textBox2.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
        }
        private void AuthenticateUser(string login, string password)
        {
            try
            {
                string sql = "SELECT id, passw, salt,status FROM Employee WHERE status!='Уволен' and login = @login";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@login", login);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["passw"] != DBNull.Value && reader["salt"] != DBNull.Value)
                            {
                                byte[] storedHashedPassword = (byte[])reader["passw"];
                                byte[] storedSalt = (byte[])reader["salt"];

                                byte[] hashedInputPassword = GenerateSHA256Hash(password, storedSalt);

                                if (storedHashedPassword.SequenceEqual(hashedInputPassword))
                                {
                                    //MessageBox.Show("Аутентификация успешна!");

                                    // Сохранение идентификатора сотрудника из текущего результата
                                    int employeeId = Convert.ToInt32(reader["id"]);

                                    // Закрываем reader перед выполнением нового запроса
                                    reader.Close();

                                    string sql2 = "SELECT * FROM Job_em WHERE id_em = @employeeId";
                                    using (NpgsqlCommand cmd1 = new NpgsqlCommand(sql2, con))
                                    {
                                        cmd1.Parameters.AddWithValue("@employeeId", employeeId);

                                        using (NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(cmd1))
                                        {
                                            DataSet ds2 = new DataSet();
                                            da2.Fill(ds2);
                                            DataTable dt2 = ds2.Tables[0];

                                            if (dt2.Rows.Count > 0)
                                            {
                                                int id_a = Convert.ToInt32(dt2.Rows[0]["id_a"]);
                                                if (id_a == 1)
                                                {
                                                    this.Hide();
                                                    new per_acc_ass_1(con, employeeId).ShowDialog();
                                                }
                                                if (id_a == 2)
                                                {
                                                    this.Hide();
                                                    new per_acc_ass_2(con, employeeId).ShowDialog();
                                                }
                                                if (id_a == 3)
                                                {
                                                    this.Hide();
                                                    new per_acc_ass_3(con, employeeId).ShowDialog();
                                                }
                                                if (id_a == 4)
                                                {
                                                    this.Hide();
                                                    new per_acc_ass_4(con, employeeId).ShowDialog();
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Пользователь не найден.");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Неверный логин или пароль.");
                                }
                            }
                            else
                            {
                                //MessageBox.Show("Пароль или соль отсутствуют в базе данных.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            byte[] for_hash = Create_for_pass();
    
            int employeeId;
            int id_a;

            string pass = textBox2.Text;
            String sql1 = "Select * from Employee ;";
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql1, con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            if (dt5.Rows.Count == 0)
            {
                this.Hide();
                per_acc_ass_4 fp = new per_acc_ass_4(con, -1);
                fp.ShowDialog();

            }
            else { AuthenticateUser(login, pass); }

            //    String sql3 = "Select * from Employee ";
            //    using (NpgsqlCommand cmd_1 = new NpgsqlCommand(sql3, con))
            //    {
            //        using (NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(cmd_1))
            //        {

            //            ds3.Reset();
            //            da3.Fill(ds3);
            //            dt3 = ds3.Tables[0];


            //            if (dt3.Rows.Count == 0)
            //            {

            //                this.Hide();
            //                per_acc_ass_1 fp = new per_acc_ass_1(con, -1);
            //                fp.ShowDialog();

            //            }
            //            else
            //            {

            //                String sql1 = "Select * from Employee where login = @login and  passw = @passw";
            //                using (NpgsqlCommand cmd = new NpgsqlCommand(sql1, con))
            //                {

            //                    cmd.Parameters.AddWithValue("@login", hashed_log_str);
            //                    cmd.Parameters.AddWithValue("@passw", hashed_pass_str);


            //                    using (NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(cmd))
            //                    {
            //                        ds1.Reset();
            //                        da1.Fill(ds1);
            //                        dt1 = ds1.Tables[0];


            //                        if (dt1.Rows.Count > 0)
            //                        {
            //                            employeeId = Convert.ToInt32(dt1.Rows[0]["id"]);
            //                            String sql2 = "Select * from Job_em where id_em = @employeeId";
            //                            using (NpgsqlCommand cmd1 = new NpgsqlCommand(sql2, con))
            //                            {

            //                                cmd1.Parameters.AddWithValue("@employeeId", employeeId);



            //                                using (NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(cmd1))
            //                                {
            //                                    ds2.Reset();
            //                                    da2.Fill(ds2);
            //                                    dt2 = ds2.Tables[0];


            //                                    if (dt2.Rows.Count > 0)
            //                                    {
            //                                        id_a = Convert.ToInt32(dt2.Rows[0]["id_a"]);
            //                                        if (id_a == 1)
            //                                        {
            //                                            this.Hide();
            //                                            per_acc_ass_1 fp = new per_acc_ass_1(con, employeeId);
            //                                            fp.ShowDialog();

            //                                        }




            //                                    }
            //                                    else
            //                                    {

            //                                        MessageBox.Show("Пользователь не найден.");
            //                                    }
            //                                }

            //                            }
            //                        }
            //                        else
            //                        {

            //                            MessageBox.Show("Пользователь не найден.");
            //                        }
            //                    }
            //                }


            //                this.StartPosition = FormStartPosition.CenterScreen;
            //                Update();
            //            }
            //        }
            //    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            int id;

                String sql1 = "Select * from Employee  ORDER BY id ASC LIMIT 1 ;";
                NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                ds6.Reset();
                da6.Fill(ds6);
                dt6 = ds6.Tables[0];
                if (dt6.Rows.Count > 0)
                {
                    id = Convert.ToInt32(dt6.Rows[0]["id"]);

                }
                else { id = -1; }
                per_acc_ass_1 fp = new per_acc_ass_1(con, id);
            fp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar != '\0')
            {
                textBox2.PasswordChar = '\0';
            }
            else
                textBox2.PasswordChar = '*';
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
      
            if (e.KeyCode == Keys.Enter)
            {
                string login = textBox1.Text;
                byte[] for_hash = Create_for_pass();

                int employeeId;
                int id_a;

                string pass = textBox2.Text;

                //byte[] hashed_pass = GenerateSHA256Hash(pass, for_hash);
                //string hashed_pass_str = Convert.ToBase64String(hashed_pass);
                AuthenticateUser(login, pass);

                e.SuppressKeyPress = true; // Отменить звуковой сигнал
            }
        
    }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
 

                e.SuppressKeyPress = true; // Отменить звуковой сигнал
            }
        }
    }
}
