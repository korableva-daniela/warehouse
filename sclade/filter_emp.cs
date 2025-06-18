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
    public partial class filter_emp : Form
    {
        public NpgsqlConnection con;
        public int id;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public filter_emp(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Job_in fp = new Job_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            department_in fp = new department_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            division_in fp = new division_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            access_level_in fp = new access_level_in(con, -1, "");
            fp.ShowDialog();
        }

        private void filter_emp_Load(object sender, EventArgs e)
        {

        }
    }
}
