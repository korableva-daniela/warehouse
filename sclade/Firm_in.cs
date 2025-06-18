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
    public partial class Firm_in : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public Firm_in(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }
        public void Update()
        {
            try
            {
                label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
              
                if (textBox1.Text == "")
            {
                    String sql = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id ORDER BY Firm.name_f ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Контактный телефон";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].HeaderText = "Статус клиента";
                    dataGridView1.Columns[4].HeaderText = "Страна регистрации";
                    dataGridView1.Columns[5].HeaderText = "ИНН";
                    dataGridView1.Columns[6].HeaderText = "КПП";
                    dataGridView1.Columns[7].HeaderText = "ОРГН";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            else
            {
                    String sql = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.fio_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id and Firm.name_f ILIKE '";
                    sql += textBox1.Text;
                    sql += "%' ORDER BY Firm.name_f ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Контактный телефон";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].HeaderText = "Статус клиента";
                    dataGridView1.Columns[4].HeaderText = "Страна регистрации";
                    dataGridView1.Columns[5].HeaderText = "ИНН";
                    dataGridView1.Columns[6].HeaderText = "КПП";
                    dataGridView1.Columns[7].HeaderText = "ОРГН";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch { }
        }
        public void updateaddressinfo(int id)
        {
                try
                {
                    if (id != null)
            {
                String sqli = "Select Address_f.id, Firm.id, Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm , Address_f  where Firm.id =  Address_f.id_f and Firm.id=:id ORDER BY Address_f.id ASC;";

                NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                dai.SelectCommand.Parameters.AddWithValue("id", id);
                dsi.Reset();
                dai.Fill(dsi);
                dti = dsi.Tables[0];
                dataGridView2.DataSource = dti;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].HeaderText = "Стран";
                dataGridView2.Columns[3].HeaderText = "Город";
                dataGridView2.Columns[4].HeaderText = "Улица";
                dataGridView2.Columns[5].HeaderText = "Дом";
                dataGridView2.Columns[6].HeaderText = "Индекс";

                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                String sqli = "Select Address_f.id, Firm.id,  Address_f.country_f,Address_f.city_f,Address_f.street_f,Address_f.house_f,Address_f.post_in_f  from Firm, Address_f  where Firm.id =  Address_f.id_f ORDER BY Address_f.id ASC;";

                NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);

                dsi.Reset();
                dai.Fill(dsi);
                dti = dsi.Tables[0];
                dataGridView2.DataSource = dti;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].HeaderText = "Стран";
                dataGridView2.Columns[3].HeaderText = "Город";
                dataGridView2.Columns[4].HeaderText = "Улица";
                dataGridView2.Columns[5].HeaderText = "Дом";
                dataGridView2.Columns[6].HeaderText = "Индекс";

                this.StartPosition = FormStartPosition.CenterScreen;
                }
            }
            catch { }
        }

        private void Firm_in_Load(object sender, EventArgs e)
        {
            try
            {
                Update();
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
            }
            catch { }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
                    try
                    {
                        int id;
                if (dataGridView1.CurrentRow != null)
                    if (dataGridView1.CurrentRow.Index != 0)
                    {
                        id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    }
                    else {
                        if (dataGridView1.Rows[0].Cells[0].Value != null)
                        {
                            String sql1 = "Select * from firm  where id = " + dataGridView1.Rows[0].Cells[0].Value.ToString();
                            //String sql1 = "Select * from invoices_in,storehouse where flag = 0  and storehouse.id_div = " + this.div.ToString() + " and storehouse.id=invoices_in.id_storehouse  ORDER BY invoices_in.num_invoices  ASC LIMIT 1 ;";
                            NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                            ds6.Reset();
                            da6.Fill(ds6);
                            dt6 = ds6.Tables[0];
                            if (dt6.Rows.Count > 0)
                            {
                                id = Convert.ToInt32(dt6.Rows[0]["id"]);

                            }
                            else { id = -1; }
                        }
                        else { id = -1; }
                    }
             
            else id = dataGridView1.RowCount;
            updateaddressinfo(id);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
