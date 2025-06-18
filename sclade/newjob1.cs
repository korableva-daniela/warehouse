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
    public partial class newjob1 : Form
    {
        public int id;
        public string name;
        public string description;
        public NpgsqlConnection con;
        public newjob1(NpgsqlConnection con, int id, string name, string description)
        {
            this.id = id;
            this.con = con;
            this.description = description;
            this.name = name;
            InitializeComponent();
        }

        private void newjob11_Load(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Arial", 11);
            richTextBox1.Font = new Font("Arial", 11);
            if (this.id != -1)
            {
                textBox1.BackColor = Color.LightGray;
                textBox1.Text = this.name;
                richTextBox1.Text = this.description;



            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.id == -1)
            {
                try
                {
                    string sql = "Insert into Job (name, description ) values (:name,:description)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("description", richTextBox1.Text);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Close();
                    }


                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
            else
            {
                try
                {
                    string sql = "update Job set name=:name, description=:description where id=:id";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name", textBox1.Text);
                    command.Parameters.AddWithValue("description", richTextBox1.Text);
                    command.Parameters.AddWithValue("id", this.id);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Close();
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
