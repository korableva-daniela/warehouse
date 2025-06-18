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
    public partial class newstorehouse1 : Form
    {
        public int id;
        public string country;
        public string city;
        public string street;
        public string house;
        public string post_in;
        public string name;
        public string name_div;

        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        DataTable dt1 = new DataTable();
        DataSet ds1 = new DataSet();
        public NpgsqlConnection con;
        public newstorehouse1(NpgsqlConnection con, int id, string name_div, string name, string country, string city, string street, string house, string post_in)
        {
            this.con = con;
            this.id = id;
            this.name = name;
            this.country = country;
            this.city = city;
            this.street = street;
            this.con = con;
            this.name_div = name_div;
            this.house = house;
            this.post_in = post_in;
            InitializeComponent();
        }
        public void Update()
        {
            try
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                if (id == -1)
                {
                    String sql = "Select storehouse.id,storehouse.name,Division.name,storehouse.country_d ,storehouse.city_d,storehouse.street_d,storehouse.house_d,storehouse.post_in_d from storehouse,Division where Division.id=storehouse.id_div  ORDER BY id ASC";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Подразделение";
        
                    dataGridView1.Columns[3].HeaderText = "Стран";
                    dataGridView1.Columns[4].HeaderText = "Город";
                    dataGridView1.Columns[5].HeaderText = "Улица";
                    dataGridView1.Columns[6].HeaderText = "Дом";
                    dataGridView1.Columns[7].HeaderText = "Индекс";
                }
                else
                {
                    String sql = "Select storehouse.id,storehouse.name,Division.name,storehouse.country_d ,storehouse.city_d,storehouse.street_d,storehouse.house_d,storehouse.post_in_d from storehouse,Division where Division.id=storehouse.id_div and storehouse.id=:id ORDER BY id ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.AddWithValue("id", id);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Подразделение";
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
        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void updatedivision(int id_d)
        {
            try
            {
                String sqli = "Select * from Division  where id=";
                sqli += id_d.ToString();
                NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                dsi.Reset();
                dai.Fill(dsi);
                dti = dsi.Tables[0];
                comboBox1.DataSource = dti;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatedivisionupdate(string name)
        {
            try
            {
                String sql4 = "Select * from Division  where name='";
                sql4 += name;
                sql4 += "'";
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql4, con);
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
        private void newstorehouse1_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Enabled = false;
           
                if (this.id==-1)
                {

                    comboBox1.Text = "Подразделение не выбрано";
                }
           
                dataGridView1.ReadOnly = true;
                comboBox1.Font = new Font("Arial", 11);
                label1.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label7.Font = new Font("Arial", 11);
                label8.Font = new Font("Arial", 11);

                label10.Font = new Font("Arial", 11);


                textBox1.Font = new Font("Arial", 11);

                textBox4.Font = new Font("Arial", 11);
                textBox5.Font = new Font("Arial", 11);

                textBox6.Font = new Font("Arial", 11);
                textBox7.Font = new Font("Arial", 11);
                textBox8.Font = new Font("Arial", 11);


                Update();
                if (this.id != -1)

                {

                    textBox4.BackColor = Color.LightGray;
                    textBox5.BackColor = Color.LightGray;
                    textBox6.BackColor = Color.LightGray;
                    textBox7.BackColor = Color.LightGray;
                    textBox8.BackColor = Color.LightGray;
                    textBox1.BackColor = Color.LightGray;
                    comboBox1.BackColor = Color.LightGray;

                    //comboBox1.Text = this.name_div;
                    textBox1.Text = this.name;
                    textBox4.Text = this.country;
                    textBox5.Text = this.city;
                    textBox6.Text = this.street;
                    textBox7.Text = this.house;
                    textBox8.Text = this.post_in;
                    updatedivisionupdate(this.name_div);
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.id == -1)
            {
                try
                {

                    string sql = "Insert into storehouse (id_div, name ,country_d ,city_d,street_d,house_d,post_in_d) values(:id_div,:name ,:country_d , :city_d, :street_d, :house_d, :post_in_d);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("id_div", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("country_d", textBox4.Text);
                    command.Parameters.AddWithValue("city_d", textBox5.Text);
                    command.Parameters.AddWithValue("street_d", textBox6.Text);
                    command.Parameters.AddWithValue("house_d", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_d", textBox8.Text);





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
                    string sql = "update storehouse  set id_div=:id_div, name=:name, country_d=:country_d, city_d=:city_d,street_d=:street_d, house_d=:house_d,post_in_d=:post_in_d  where id=:id;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("id_div", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("country_d", textBox4.Text);
                    command.Parameters.AddWithValue("city_d", textBox5.Text);
                    command.Parameters.AddWithValue("street_d", textBox6.Text);
                    command.Parameters.AddWithValue("house_d", textBox7.Text);
                    command.Parameters.AddWithValue("post_in_d", textBox8.Text);

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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                int id_d = 0;
                string name = "";
                division_in fp = new division_in(con, id_d, name);

                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatedivision(fp.id_d);

                }
                else
                {
                    comboBox1.Text = "Подразделение не выбрано";

                }
            }
            catch { }
        }
    }
}
