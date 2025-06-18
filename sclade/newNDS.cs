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
    public partial class newNDS : Form
    {
        public int id;
        public int percent;
        public string description;
        public NpgsqlConnection con;
        public newNDS(NpgsqlConnection con, int id, int percent, string description)
        {
            this.id = id;
            this.con = con;
            this.description = description;
            this.percent = percent;
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

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
                    string sql = "Insert into NDS (percent, description ) values (:percent,:description)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("percent", Convert.ToDouble(textBox1.Text));
                    command.Parameters.AddWithValue("description", richTextBox1.Text);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Close();
                    }


                }
                catch
                {
                    DialogResult result = MessageBox.Show("Некорректно введено значение НДС, можно вводить в поле только число без пробелов и иных символов", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            else
            {
                try
                {
                    string sql = "update NDS set percent=:percent, description=:description where id=:id";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("percent", Convert.ToDouble(textBox1.Text));
                    command.Parameters.AddWithValue("description", richTextBox1.Text);
                    command.Parameters.AddWithValue("id", this.id);

                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Close();
                    }



                }
                catch
                {
                    DialogResult result = MessageBox.Show("Некорректно введено значение НДС, можно вводить в поле только число без пробелов и иных символов", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void newNDS_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);

            richTextBox1.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
            label3.Font = new Font(label3.Font.Name, 16);
            if (this.id != -1)
            {
                textBox1.BackColor = Color.LightGray;
                textBox1.Text = this.percent.ToString();
                richTextBox1.Text = this.description;



            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
    }
}
