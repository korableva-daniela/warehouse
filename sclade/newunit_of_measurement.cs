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
    public partial class newunit_of_measurement : Form
    {
        public int id;
        public string code;
        public string name;
        public string litter;
        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        public NpgsqlConnection con;
        public newunit_of_measurement(NpgsqlConnection con, int id, string code, string name, string litter)
        {
            this.con = con;
            this.id = id;
            this.name = name;
            this.code = code;
            this.litter = litter;
            InitializeComponent();
        }

        private void newunit_of_measurement_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.ReadOnly = true;
                label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
     
            label6.Font = new Font("Arial", 11);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
  
            textBox1.Font = new Font("Arial", 11);
                textBox4.Font = new Font("Arial", 11);
                textBox5.Font = new Font("Arial", 11);
                Update();
            if (this.id != -1)

            {

                textBox4.BackColor = Color.LightGray;
                textBox5.BackColor = Color.LightGray;
                textBox1.BackColor = Color.LightGray;
                textBox1.Text = this.code;
                textBox4.Text = this.name;
                textBox5.Text = this.litter;

                }
            }
            catch { }
        }
        public void Update()
        {
                try
                {
                    if (id == -1)
            {
                String sql = "Select *  from unit_of_measurement  ORDER BY code ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Код единицы измерения";
                dataGridView1.Columns[2].HeaderText = "Название единицы измерения";
                dataGridView1.Columns[3].HeaderText = "Буквенная идентификация";

            }
            else
            {
                String sql = "Select *  from unit_of_measurement where id=:id ORDER BY code ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("id", id);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Код единицы измерения";
                dataGridView1.Columns[2].HeaderText = "Название единицы измерения";
                dataGridView1.Columns[3].HeaderText = "Буквенная идентификация";
            }

            this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }

            }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.id == -1)
            {
                try
                {

                    string sql = "Insert into unit_of_measurement (code,name,litter) values(:code,:name,:litter);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("code", textBox1.Text);
                    command.Parameters.AddWithValue("litter", textBox5.Text);

                    command.Parameters.AddWithValue("name", textBox4.Text);






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
                    string sql = "update unit_of_measurement  set code=:code,name=:name, litter=:litter  where id=:id;";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("code", textBox1.Text);
                    command.Parameters.AddWithValue("litter", textBox5.Text);

                    command.Parameters.AddWithValue("name", textBox4.Text);

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
