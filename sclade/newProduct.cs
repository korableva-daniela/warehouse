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
    public partial class newProduct : Form
    {
        public int id;
        public string name;
        public string description;
        public string type;
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        public newProduct(NpgsqlConnection con, int id, string name, string description, string type)
        {

            this.id = id;
            this.con = con;
            this.description = description;
            this.name = name;
            this.type = type;
            InitializeComponent();
        }

        private void newProduct_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Enabled = false;
              
                label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            label3.Font = new Font("Arial", 11);
            comboBox1.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
    
            richTextBox1.Font = new Font("Arial", 11);
            updateType_toinfo();
            if (this.id != -1)
            {
                updateType_toinfo();
                textBox1.BackColor = Color.LightGray;
                textBox1.Text = this.name;
                comboBox1.BackColor = Color.LightGray;
                comboBox1.Text = this.type;
                richTextBox1.Text = this.description;



                }
            }
            catch { }
        }
        public void updateType_toinfo()
            {
                try
                {
                    String sqli = "Select * from Type_to ORDER BY name ASC";
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



        private void button1_Click(object sender, EventArgs e)
        {
            if (this.id == -1)
            {
                try
                {
                    string sql = "Insert into Product (name, description,id_type ) values (:name,:description,:id_type)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("description", richTextBox1.Text);
                    command.Parameters.AddWithValue("id_type", comboBox1.SelectedValue);
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Close();
                    }


                }
                catch { }
            }
            else
            {
                try
                {
                    string sql = "update Product set name=:name, description=:description, id_type=:id_type where id=:id";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("description", richTextBox1.Text);
                    command.Parameters.AddWithValue("id_type", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("id", this.id);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Close();
                    }



                }
                catch { }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Type_to_in fp = new Type_to_in(con);
            fp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
                {
                    try
                    {
                      
                    }
                    catch { }
                }
    }
}