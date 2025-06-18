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
    public partial class firm_storehouse : Form
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();

        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public NpgsqlConnection con;
        public int id;
        public int id_storehouse;
        public int id_Firm;
        public int div;
        public firm_storehouse(NpgsqlConnection con, int id, int id_Firm, int id_storehouse,int div)
        {
            this.div = div;
            this.con = con;
            InitializeComponent();
            this.id = id;

            this.id_Firm = id_Firm;
            this.id_storehouse = id_storehouse;
        }
        public void updateFirminfo(int id_f)
        {
            try
            {
                String sql1 = "Select * from Firm where id=";
                sql1 += id_f.ToString();
                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "name_f";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateFirminfoupdate(string name)
        {
            try
            {
                String sql9 = "Select * from Firm where name_f='";
                sql9 += name;
                sql9 += "'";
                NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                ds9.Reset();
                da9.Fill(ds9);
                dt9 = ds9.Tables[0];
                comboBox1.DataSource = dt9;
                comboBox1.DisplayMember = "name_f";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatestorehouseinfo(int id_s)
        {
            try
            {
                String sql3 = "Select * from storehouse where id=";
                sql3 += id_s.ToString();
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
                ds3.Reset();
                da3.Fill(ds3);
                dt3 = ds3.Tables[0];
                comboBox2.DataSource = dt3;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatestorehouseinfoupdate(string name)
        {
            try
            {
                String sql8 = "Select * from storehouse where name='";
                sql8 += name;
                sql8 += "'";
                NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                ds8.Reset();
                da8.Fill(ds8);
                dt8 = ds8.Tables[0];
                comboBox2.DataSource = dt8;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void Update()
        {
            try
            {
                comboBox1.Text = "Поставщик не выбран";
                comboBox2.Text = "Склад не выбран";

                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);

                comboBox2.Font = new Font("Arial", 11);
                comboBox1.Font = new Font("Arial", 11);



            }
            catch { }


        }
        private void firm_storehouse_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;




                Update();
                if (this.id_Firm != -1)
                {
                    updateFirminfo(this.id_Firm);




                }
                if (this.id_storehouse != -1)
                {

                    updatestorehouseinfo(this.id_storehouse);



                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.id == -1)
            {

                try
                {

                    string sql = "Insert into firm_storehouse (id_Firm, id_storehouse) values ( :id_Firm, :id_storehouse)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);

                    command.Parameters.AddWithValue("id_Firm", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("id_storehouse", comboBox2.SelectedValue);


                    DialogResult result = MessageBox.Show("Вы уверены, что добавить  запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();

                        Update();

                    }
                    else
                        Update();



                }

                catch { }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                int id_f = 0;
                string name = "";

                firm fp = new firm(con, id_f, name);
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateFirminfo(fp.id);

                }
                else
                {
                    comboBox1.Text = "Поставщик не выбран";

                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name, div, "");
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatestorehouseinfo(fp.id_c);

                }
                else
                {
                    comboBox2.Text = "Склад не выбран";

                }
            }
            catch { }
        }
    }
}
