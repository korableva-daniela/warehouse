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
    public partial class start : Form
    {
            public NpgsqlConnection con;
        public start()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client fp = new client(con,-1,"");
            fp.ShowDialog();
        }

        private void start_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            con = new NpgsqlConnection("Server = localhost; Port = 5432; UserID =postgres; Password=postpass; Database=warehouse");
            con.Open();
         
            //this.WindowState = FormWindowState.Maximized;
            button2.Visible = false;
            button4.Visible = false;
            button20.Visible = false;
            button22.Visible = false;
            button24.Visible = false;
            button17.Visible = false;
            button16.Visible = false;
            button13.Visible = false;
            button8.Visible = false;
            button26.Visible = false;
            button29.Visible = false;
            button28.Visible = false;
            button18.Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            job1 fp = new job1(con);
            fp.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            department fp = new department(con);
            fp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            division fp = new division(con);
            fp.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            firm fp = new firm(con,-1,"");
            fp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client_in fp = new client_in(con);
            fp.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            department_in fp = new department_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            division_in fp = new division_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Job_in fp = new Job_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Firm_in fp = new Firm_in(con);
            fp.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            access_level fp = new access_level(con);
            fp.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            access_level_in fp = new access_level_in(con,-1,"");
            fp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            employee fp = new employee(con,-1,"");
            fp.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
         

        }

        private void button20_Click(object sender, EventArgs e)
        {
            Type_to_in fp = new Type_to_in(con);
            fp.ShowDialog();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            NDS fp = new NDS(con);
            fp.ShowDialog();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            NDS_in fp = new NDS_in(con);
            fp.ShowDialog();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            country_of_origin fp = new country_of_origin(con);
            fp.ShowDialog();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            country_of_origin_in fp = new country_of_origin_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            unit_of_measurement fp = new unit_of_measurement(con);
            fp.ShowDialog();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            unit_of_measurement_in fp = new unit_of_measurement_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button29_Click(object sender, EventArgs e)
        {
          
        }

        private void button28_Click(object sender, EventArgs e)
        {
           
        }

        private void button30_Click(object sender, EventArgs e)
        {
           
        }

        private void button32_Click(object sender, EventArgs e)
        {
           
        }

        private void button34_Click(object sender, EventArgs e)
        {
            storehouse fp = new storehouse(con,-1,"", -1, "");
            fp.ShowDialog();
        }

        private void button33_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
           
        }

        private void button36_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            getting_started fp = new getting_started(con);
            fp.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }
    }
}
