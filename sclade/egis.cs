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
    public partial class egis : Form
    {
        public string numegis;
        Regex regex1 = new Regex(@"^\d{1,19}$");
        DataTable dt = new DataTable();
        DataTable dti = new DataTable();
        DataSet ds = new DataSet();
        DataSet dsi = new DataSet();
        public NpgsqlConnection con;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public egis(NpgsqlConnection con, string numegis)
        {
            this.con = con;
            this.numegis = numegis;

            InitializeComponent();
        }

        private void egis_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (regex1.IsMatch(textBox1.Text) == false)
            {
                DialogResult result = MessageBox.Show("Некорректно введены значения первого блока", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.BackColor = Color.DarkSalmon;
            }
            else
            {
                textBox1.BackColor = Color.Honeydew;
                string txt = textBox1.Text;
                if (textBox1.Text.Length < 19)
                {
                    string text1 = "";
                    int nul = 19 - textBox1.Text.Length;
                    for (int i = 0; i < nul; i++)
                        text1 = text1 + "0";

                    this.numegis = text1 + txt;
                    textBox1.BackColor = Color.Honeydew;

                    Close();
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.Name, 13);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
