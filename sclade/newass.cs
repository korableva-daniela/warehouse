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
    public partial class newass : Form
    {
        public int id;
        public string name;
        public string description;
        public NpgsqlConnection con;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public newass(NpgsqlConnection con, int id, string name, string description)
        {
            this.id = id;
            this.con = con;
            this.description = description;
            this.name = name;
            InitializeComponent();
        }

        private void newass_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);

            richTextBox1.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
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
                    string sql = "Insert into access_level (name, description ) values (:name,:description)";
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
                catch { }
            }
            else
            {
                try
                {
                    string sql = "update access_level set name=:name, description=:description where id=:id";
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
                catch { }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
