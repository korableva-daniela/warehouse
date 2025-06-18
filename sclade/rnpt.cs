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
using System.Text.RegularExpressions;
namespace sclade
{
    public partial class rnpt : Form
    {
        public string numrnpt;
        Regex regex1 = new Regex(@"^\d{8}$");
        Regex regex2 = new Regex(@"^\d{6}$");
        Regex regex3 = new Regex(@"^\d{7}$");
        Regex regex4 = new Regex(@"^\d{1,9}$");
        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        public NpgsqlConnection con;
        public rnpt(NpgsqlConnection con, string numrnpt)
        {
            this.con = con;
            this.numrnpt = numrnpt;

            InitializeComponent();
        }

        private void rnpt_Load(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.Name, 16);
            label2.Font = new Font(label2.Font.Name, 16);
            label6.Font = new Font(label6.Font.Name, 16);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (regex1.IsMatch(textBox1.Text) == false || regex2.IsMatch(textBox2.Text) == false || regex3.IsMatch(textBox3.Text) == false|| regex4.IsMatch(textBox4.Text) == false)
            {
                if (regex1.IsMatch(textBox1.Text) == false)
                {
                    DialogResult result = MessageBox.Show("Некорректно введены значения первого блока", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.BackColor = Color.DarkSalmon;
                }
                else
                { textBox1.BackColor = Color.Honeydew; }
                if (regex2.IsMatch(textBox2.Text) == false)
                {
                    DialogResult result = MessageBox.Show("Некорректно введены значения второго блока", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.BackColor = Color.DarkSalmon;
                }
                else
                { textBox2.BackColor = Color.Honeydew; }
                if (regex3.IsMatch(textBox3.Text) == false)
                {
                    DialogResult result = MessageBox.Show("Некорректно введены значения третьего блока", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.BackColor = Color.DarkSalmon;

                }
                else
                { textBox3.BackColor = Color.Honeydew; }
                if (regex4.IsMatch(textBox4.Text) == false)
                {
                    DialogResult result = MessageBox.Show("Некорректно введены значения третьего блока", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox4.BackColor = Color.DarkSalmon;
                }
                else
                { textBox4.BackColor = Color.Honeydew; }
            }
            else
            {
                string txt = textBox1.Text + "/" + textBox2.Text + "/" + textBox3.Text + "/" + textBox4.Text;
                this.numrnpt = txt;
                textBox1.BackColor = Color.Honeydew;
                textBox2.BackColor = Color.Honeydew;
                textBox3.BackColor = Color.Honeydew;
                textBox4.BackColor = Color.Honeydew;
                Close();
               
            }
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, 13);
            textBox2.Font = new Font(textBox2.Font.Name, 13);
            textBox3.Font = new Font(textBox3.Font.Name, 13);
            textBox4.Font = new Font(textBox4.Font.Name, 13);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
