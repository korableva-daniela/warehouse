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
    public partial class log_and_pass : Form
    {
        public NpgsqlConnection con;
        public int id_em;
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        int ind = 0;
        int i=0;
        public log_and_pass(NpgsqlConnection con, int id_em)
        {
            this.con = con;
            InitializeComponent();
            this.id_em = id_em;
        }
        
        private void log_and_pass_Load(object sender, EventArgs e)
        {
            button1.Text = "Ввести";
            label1.Text = "Введите предыдущий логин и пароль";
            textBox5.Font = new Font("Arial", 11);
            textBox4.Font = new Font("Arial", 11);
            textBox5.PasswordChar = '*';
            Update();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (id_em != -1)
            {
                if(ind ==1)
                {
                    try
                    {


                        string sql9 = "update Employee set  login=:login, passw=:passw, salt=:salt where id=:id;";
                        NpgsqlCommand command9 = new NpgsqlCommand(sql9, con);
                        string login = textBox4.Text;
                        byte[] for_hash = Create_for_pass();
                        command9.Parameters.AddWithValue("id", this.id_em);
                        command9.Parameters.AddWithValue("login", login);
                        string password = textBox5.Text;

                        byte[] hashed_pass = GenerateSHA256Hash(password, for_hash);
                        //string hashed_pass_str = Convert.ToBase64String(hashed_pass);

                        command9.Parameters.AddWithValue("passw", hashed_pass);
                        command9.Parameters.AddWithValue("salt", for_hash);


                        DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            command9.ExecuteNonQuery();
                            Update();
                            Close();
                        }
                        else
                            Update();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
                else
                {
                    if (i < 3)
                    {
                        string login = textBox4.Text;
                        byte[] for_hash = Create_for_pass();

                        int employeeId;
                        int id_a;

                        string pass = textBox5.Text;

                        //byte[] hashed_pass = GenerateSHA256Hash(pass, for_hash);
                        //string hashed_pass_str = Convert.ToBase64String(hashed_pass);
                        AuthenticateUser(login, pass);
                        i++;
                    }
                    else
                    {
                        MessageBox.Show("Лимит попыток превышен. Автоматический выход из аккунта.");
                        // Получаем текущую форму

                        // Открываем новую форму GettingStarted
                       
                        // Открываем новую форму GettingStarted
                            getting_started fp = new getting_started(con);
                        fp.ShowDialog(); // Или fp.ShowDialog() для модального открытия

                        // Закрываем все остальные формы, кроме новой
                        foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
                            {
                                if (form != fp)
                                {
                                    form.Hide();
                                }
                            }

                            // Закрываем текущую форму, если нужно
                            this.Close(); // Или this.Hide(); если хотите скрыть текущую форму
                        
                    }
                }


                //try
                //{


                //    string sql9 = "update Employee set  login=:login, passw=:passw, salt=:salt where id=:id;";
                //    NpgsqlCommand command9 = new NpgsqlCommand(sql9, con);
                //    string login = textBox4.Text;
                //    byte[] for_hash = Create_for_pass();
                //    command9.Parameters.AddWithValue("id", this.id_em);
                //    command9.Parameters.AddWithValue("login", login);
                //    string password = textBox5.Text;

                //    byte[] hashed_pass = GenerateSHA256Hash(password, for_hash);
                //    //string hashed_pass_str = Convert.ToBase64String(hashed_pass);

                //    command9.Parameters.AddWithValue("passw", hashed_pass);
                //    command9.Parameters.AddWithValue("salt", for_hash);


                //    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //    if (result == DialogResult.Yes)
                //    {

                //        command9.ExecuteNonQuery();
                //        Update();
                //        Close();
                //    }
                //    else
                //        Update();

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Ошибка: " + ex.Message);
                //}
            }
        }

        private void AuthenticateUser(string login, string password)
        {
            try
            {
                string sql = "SELECT id, passw, salt,status FROM Employee WHERE status!='Уволен' and login = @login and id = " + this.id_em;
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

                                    //if(employeeId==this.id_em)
                                    //{
                                        ind = 1;
                                        label1.Text = "Введите новый логин и пароль";
                                        button1.Text = "Сохранить";
                                        MessageBox.Show("Аутентификация успешна! Можете сменить логин и пароль.");
                                        textBox4.Text = "";
                                        textBox5.Text = "";

                                    //}
                                }
                                else
                                {
                                    MessageBox.Show("Неверный логин или пароль.");
                                }
                            }
                            else
                            {
                               
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

        private void button3_Click(object sender, EventArgs e)
            {
                Close();
            }

        private void button2_Click(object sender, EventArgs e)
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

