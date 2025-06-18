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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace sclade
{
    public partial class address : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public int id;

        public string name;

        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        int id_f;
        public address(NpgsqlConnection con, int id, string name,int id_f)
        {
            this.id = id;
            this.id_f = id_f;
            this.name = name;

            this.con = con;
            InitializeComponent();
        }
        public void Update()
        {


            try
            {

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);

                    label1.Font = new Font("Arial", 11);
                if (this.name != "")
                {
                    label1.Text = "Название фирмы контрагента: " + name;
                }


                if (this.id ==-1)
                {
                    button3.Visible = false;
                }
                if (this.id != -1)
                {
                    String sqli = "Select Address_f.id, Firm.name_f,Firm.id, Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm , Address_f  where Firm.id =  Address_f.id_f and Address_f.id=:id ORDER BY Address_f.id ASC;";

                    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                    dai.SelectCommand.Parameters.AddWithValue("id", id);
                    dsi.Reset();
                    dai.Fill(dsi);
                    dti = dsi.Tables[0];
                    dataGridView1.DataSource = dti;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].HeaderText = "Стран";
                    dataGridView1.Columns[4].HeaderText = "Город";
                    dataGridView1.Columns[5].HeaderText = "Улица";
                    dataGridView1.Columns[6].HeaderText = "Дом";
                    dataGridView1.Columns[7].HeaderText = "Индекс";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {

                    if (this.name != "")
                    {
                      
                        String sqli = "Select Address_f.id, Firm.name_f,Firm.id, Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm , Address_f  where Firm.id =  Address_f.id_f and Firm.name_f=:name ORDER BY Address_f.id ASC;";

                        NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                        dai.SelectCommand.Parameters.AddWithValue("name", name);
                        dsi.Reset();
                        dai.Fill(dsi);
                        dti = dsi.Tables[0];
                        dataGridView1.DataSource = dti;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[2].Visible = false;
                        dataGridView1.Columns[3].HeaderText = "Стран";
                        dataGridView1.Columns[4].HeaderText = "Город";
                        dataGridView1.Columns[5].HeaderText = "Улица";
                        dataGridView1.Columns[6].HeaderText = "Дом";
                        dataGridView1.Columns[7].HeaderText = "Индекс";

                        this.StartPosition = FormStartPosition.CenterScreen;
                    }

                    else
                    {
                        if (this.id_f != -1)
                        {
                            String sqli = "Select Address_f.id, Firm.name_f, Firm.id,Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm , Address_f  where Firm.id =  Address_f.id_f and Firm.id=:id ORDER BY Address_f.id ASC;";

                            NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                            dai.SelectCommand.Parameters.AddWithValue("id", id_f);
                            dsi.Reset();
                            dai.Fill(dsi);
                            dti = dsi.Tables[0];
                            dataGridView1.DataSource = dti;
                            dataGridView1.Columns[0].Visible = false;
                            dataGridView1.Columns[1].Visible = false;
                            dataGridView1.Columns[2].Visible = false;
                            dataGridView1.Columns[3].HeaderText = "Стран";
                            dataGridView1.Columns[4].HeaderText = "Город";
                            dataGridView1.Columns[5].HeaderText = "Улица";
                            dataGridView1.Columns[6].HeaderText = "Дом";
                            dataGridView1.Columns[7].HeaderText = "Индекс";

                            this.StartPosition = FormStartPosition.CenterScreen;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    label1.Font = new Font("Arial", 11);
                    label1.Text = "Название фирмы контрагента: " + dt.Rows[0][1].ToString();

                }
            }
            catch { }
        }
        private void address_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "";
                Update();

                dataGridView1.ReadOnly = true;

            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                int id_ = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string name_ = (string)dataGridView1.CurrentRow.Cells[1].Value;

                this.name = name_;
                this.id = id_;
                Close();
            }
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                int id = (int)dataGridView1.Rows[0].Cells[2].Value;
                newaddressinfo_f f = new newaddressinfo_f(con, -1, id, "", "", "", "", "");
                f.ShowDialog();
                Update();
              
            }
            catch { }
        }

        private void выгрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                int id = (int)dataGridView1.Rows[0].Cells[2].Value;
                newaddressinfo_f f = new newaddressinfo_f(con, -1, id, "", "", "", "", "");
                f.ShowDialog();
                Update();

            }
            catch { }

        }
    }
}
