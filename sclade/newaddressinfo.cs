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
    public partial class newaddressinfo : Form
    {
        public int id;
        public int id_client;
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
        public NpgsqlConnection con;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public newaddressinfo(NpgsqlConnection con, int id, int id_client, string country, string city, string street, string house, string post_in)
        {

            InitializeComponent();
            this.id = id;
            this.country = country;
            this.city = city;
            this.street = street;
            this.con = con;
            this.id_client = id_client;
            this.house = house;
            this.post_in = post_in;



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
                String sql = "Select Address_cl.id,Client.id, Address_cl.country_cl,Address_cl.city_cl,Address_cl.street_cl,Address_cl.house_cl,Address_cl.post_in_cl  from Client, Address_cl  where Client.id =  Address_cl.id_client and Address_cl.id_client =";
                sql += this.id_client.ToString();

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

                String sql1 = "Select Client.id,Client.name,Client.phone,Client.mail,Client.view_,country_of_origin.litter,Client.INN,Client.KPP,Client.OGRN  from Client,country_of_origin where Client.country_of_registration=country_of_origin.id and Client.id =  ";
                sql1 += this.id_client.ToString();
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                dataGridView2.DataSource = dt1;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "ФИО";
                dataGridView2.Columns[2].HeaderText = "Телефон";
                dataGridView2.Columns[3].HeaderText = "Почта";
                dataGridView2.Columns[4].HeaderText = "Статус клиента";
                dataGridView2.Columns[5].HeaderText = "Страна рЕГАИСтрации";
                dataGridView2.Columns[6].HeaderText = "ИНН";
                dataGridView2.Columns[7].HeaderText = "КПП";
                dataGridView2.Columns[8].HeaderText = "ОРГН";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }

        private void newaddressinfo_Load(object sender, EventArgs e)
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

                    string sql = "Insert into Address_cl (id_client,country_cl,city_cl,street_cl,house_cl,post_in_cl) values(:id_client ,:country_cl, :city_cl, :street_cl, :house_cl, :post_in_cl);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("country_cl", textBox4.Text);
                    command.Parameters.AddWithValue("city_cl", textBox5.Text);
                    command.Parameters.AddWithValue("street_cl", textBox6.Text);
                    command.Parameters.AddWithValue("house_cl", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_cl", textBox8.Text);
                    command.Parameters.AddWithValue("id_client", this.id_client);





                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
            else
            {
                try
                {
                    string sql = "update Address_cl  set id_client=:id_client, country_cl=:country_cl, city_cl=:city_cl,street_cl=:street_cl, house_cl=:house_cl,post_in_cl=:post_in_cl  where id=:id and id_client=:id_client;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("country_cl", textBox4.Text);
                    command.Parameters.AddWithValue("city_cl", textBox5.Text);
                    command.Parameters.AddWithValue("street_cl", textBox6.Text);
                    command.Parameters.AddWithValue("house_cl", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_cl", textBox8.Text);
                    command.Parameters.AddWithValue("id_client", this.id_client);
                    command.Parameters.AddWithValue("id", this.id);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        update();
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
