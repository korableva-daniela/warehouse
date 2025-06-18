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
    public partial class newdiv : Form
    {
        public int id;
        public string country;
        public string city;
        public string street;
        public string house;
        public string post_in;
        public string name;
        public DateTime date_open;
        
        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        public NpgsqlConnection con;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана


        public newdiv(NpgsqlConnection con, int id, string name, DateTime date_open, string country, string city, string street, string house, string post_in)
        {
            this.con = con;
            this.id = id;
            this.name = name;
            this.country = country;
            this.city = city;
            this.street = street;
            this.con = con;
            this.date_open = date_open;
            this.house = house;
            this.post_in = post_in;
            InitializeComponent();
        }
        public void Update()
        {
            try
            {
                label1.Font = new Font("Arial", 11);

            label3.Font = new Font("Arial", 11);
            label4.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
            label8.Font = new Font("Arial", 11);
    
            label10.Font = new Font("Arial", 11);

                textBox1.Font = new Font("Arial", 13);
            textBox4.Font = new Font("Arial", 13);
            textBox5.Font = new Font("Arial", 13);
            textBox6.Font = new Font("Arial", 13);
                textBox7.Font = new Font("Arial", 13);
                textBox8.Font = new Font("Arial", 13);
          

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
     
            if (id == -1)
            {
                String sql = "Select *  from Division  ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название";
                dataGridView1.Columns[2].HeaderText = "Дата открытия";
                dataGridView1.Columns[3].HeaderText = "Стран";
                dataGridView1.Columns[4].HeaderText = "Город";
                dataGridView1.Columns[5].HeaderText = "Улица";
                dataGridView1.Columns[6].HeaderText = "Дом";
                dataGridView1.Columns[7].HeaderText = "Индекс";
            }
            else
            {
                String sql = "Select *  from Division where id=:id ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("id", id);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название";
                dataGridView1.Columns[2].HeaderText = "Дата открытия";
                dataGridView1.Columns[3].HeaderText = "Стран";
                dataGridView1.Columns[4].HeaderText = "Город";
                dataGridView1.Columns[5].HeaderText = "Улица";
                dataGridView1.Columns[6].HeaderText = "Дом";
                dataGridView1.Columns[7].HeaderText = "Индекс";
            }

            this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }

        }
        private void newdiv_Load(object sender, EventArgs e)
            {
                try
                {
                    Update();
            if (this.id != -1)

            {
                    dataGridView1.ReadOnly = true;
                    textBox4.BackColor = Color.LightGray;
                textBox5.BackColor = Color.LightGray;
                textBox6.BackColor = Color.LightGray;
                textBox7.BackColor = Color.LightGray;
                textBox8.BackColor = Color.LightGray;
                textBox1.BackColor = Color.LightGray;
                dateTimePicker1.BackColor = Color.LightGray;

                dateTimePicker1.Value = this.date_open;
                textBox1.Text = this.name;
                textBox4.Text = this.country;
                textBox5.Text = this.city;
                textBox6.Text = this.street;
                textBox7.Text = this.house;
                textBox8.Text = this.post_in;

                    }
                }
                catch { }
            }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.id == -1)
            {
                try
                {

                    string sql = "Insert into Division (name,date_open,country_d,city_d,street_d,house_d,post_in_d) values(:name,:date_open,:country_d, :city_d, :street_d, :house_d, :post_in_d);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("country_d", textBox4.Text);
                    command.Parameters.AddWithValue("city_d", textBox5.Text);
                    command.Parameters.AddWithValue("street_d", textBox6.Text);
                    command.Parameters.AddWithValue("house_d", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_d", textBox8.Text);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("date_open", dateTimePicker1.Value);





                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Update();
                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else
            {
                try
                {
                    string sql = "update Division  set name=:name,date_open=:date_open, country_d=:country_d, city_d=:city_d,street_d=:street_d, house_d=:house_d,post_in_d=:post_in_d  where id=:id;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("country_d", textBox4.Text);
                    command.Parameters.AddWithValue("city_d", textBox5.Text);
                    command.Parameters.AddWithValue("street_d", textBox6.Text);
                    command.Parameters.AddWithValue("house_d", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_d", textBox8.Text);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("date_open", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("id", this.id);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Update();
                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
