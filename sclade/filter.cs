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
    public partial class filter : Form
    {
        public NpgsqlConnection con;
        public int id;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public int div;
        public filter(NpgsqlConnection con,int div)
        {
            this.div = div;
            this.con = con;
            InitializeComponent();
        }

        private void filter_Load(object sender, EventArgs e)
        {
           
           
           
          

        }

        private void button3_Click(object sender, EventArgs e)
        {
          

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Type_to_in fp = new Type_to_in(con);
            fp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            unit_of_measurement_in fp = new unit_of_measurement_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            country_of_origin_in fp = new country_of_origin_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Firm_in fp = new Firm_in(con);
            fp.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            batch_number fp = new batch_number(con, -1, "", -1, -1,-1,div);
            fp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            storehouse fp = new storehouse(con, -2, "" ,div, "");
            fp.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Product_card fp = new Product_card(con, -2, "", "", "",-1, this.div);
            fp.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            NDS_in fp = new NDS_in(con);
            fp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client_in fp = new client_in(con);
            fp.ShowDialog();
        }
    }
}
