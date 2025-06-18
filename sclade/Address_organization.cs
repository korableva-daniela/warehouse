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
    public partial class Address_organization : Form
    {
        public int id;
        public int id_f;
        public string country;
        public string city;
        public string street;
        public string house;
        public string post_in;

        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        DataTable dt1 = new DataTable();
        DataSet ds1 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public NpgsqlConnection con;
        public Address_organization(NpgsqlConnection con, int id, int id_f, string country, string city, string street, string house, string post_in)
        {
            this.id = id;
            this.country = country;
            this.city = city;
            this.street = street;
            this.con = con;
            this.id_f = id_f;
            this.house = house;
            this.post_in = post_in;
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
        public void update()
        {
            try
            {
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label7.Font = new Font("Arial", 11);
                label8.Font = new Font("Arial", 11);

                label10.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                dataGridView2.Font = new Font("Arial", 9);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                textBox4.Font = new Font("Arial", 11);
                textBox5.Font = new Font("Arial", 11);
                textBox6.Font = new Font("Arial", 11);
                textBox7.Font = new Font("Arial", 11);
                textBox8.Font = new Font("Arial", 11);
                String sql = "Select Address_organization.id, organization.id,  Address_organization.country_f,Address_organization.city_f,Address_organization.street_f,Address_organization.house_f,Address_organization.post_in_f  from organization, Address_organization  where organization.id =  Address_organization.id_f and Address_organization.id_f =  ";
                sql += this.id_f.ToString();
                sql += " ORDER BY Address_organization.id ASC;";


                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].HeaderText = "Стран";
                dataGridView1.Columns[3].HeaderText = "Город";
                dataGridView1.Columns[4].HeaderText = "Улица";
                dataGridView1.Columns[5].HeaderText = "Дом";
                dataGridView1.Columns[6].HeaderText = "Индекс";



                this.StartPosition = FormStartPosition.CenterScreen;

                String sql1 = "Select organization.id,organization.name_f,organization.phone_f,organization.view_,country_of_origin.litter,organization.INN,organization.KPP,organization.OGRN,organization.pc,organization.bank,organization.bik  from organization,country_of_origin where organization.country_of_registration=country_of_origin.id and organization.id=";
                sql1 += this.id_f.ToString();
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                dataGridView2.DataSource = dt1;

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Название";
                dataGridView2.Columns[2].HeaderText = "Контактный телефон";
                //dataGridView2.Columns[3].HeaderText = "ФИО представителя";
                dataGridView2.Columns[3].HeaderText = "Статус поставщика";
                dataGridView2.Columns[4].HeaderText = "Страна регистрации";
                dataGridView2.Columns[5].HeaderText = "ИНН";
                dataGridView2.Columns[6].HeaderText = "КПП";
                dataGridView2.Columns[7].HeaderText = "ОРГН";
                dataGridView2.Columns[8].Visible = false;
                dataGridView2.Columns[9].Visible = false;
                dataGridView2.Columns[10].Visible = false;
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            catch { }
        }
        
        private void Address_organization_Load(object sender, EventArgs e)
        {
            update();
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            if (this.id != -1)

            {

                textBox4.BackColor = Color.LightGray;
                textBox5.BackColor = Color.LightGray;
                textBox6.BackColor = Color.LightGray;
                textBox7.BackColor = Color.LightGray;
                textBox8.BackColor = Color.LightGray;


                textBox4.Text = this.country;
                textBox5.Text = this.city;
                textBox6.Text = this.street;
                textBox7.Text = this.house;
                textBox8.Text = this.post_in;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.id == -1)
            {
                try
                {

                    string sql = "Insert into Address_organization (id_f,country_f,city_f,street_f,house_f,post_in_f) values(:id_f ,:country_f, :city_f, :street_f, :house_f, :post_in_f);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("country_f", textBox4.Text);
                    command.Parameters.AddWithValue("city_f", textBox5.Text);
                    command.Parameters.AddWithValue("street_f", textBox6.Text);
                    command.Parameters.AddWithValue("house_f", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_f", textBox8.Text);
                    command.Parameters.AddWithValue("id_f", this.id_f);





                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {

                            command.ExecuteNonQuery();
                        }
                        catch
                        {
                            MessageBox.Show("Данные заполнены некорректно или заполнена не вся информация");
                        }
                        update();
                    }
                    else
                        update();


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    string sql = "update Address_organization  set id_f=:id_f, country_f=:country_f, city_f=:city_f,street_f=:street_f, house_f=:house_f,post_in_f=:post_in_f  where id=:id and id_f=:id_f;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("country_f", textBox4.Text);
                    command.Parameters.AddWithValue("city_f", textBox5.Text);
                    command.Parameters.AddWithValue("street_f", textBox6.Text);
                    command.Parameters.AddWithValue("house_f", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_f", textBox8.Text);
                    command.Parameters.AddWithValue("id_f", this.id_f);
                    command.Parameters.AddWithValue("id", this.id);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        update();
                    }
                    else
                        update();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
