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
    public partial class client_in : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public client_in(NpgsqlConnection con)
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
 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                textBox1.Font = new Font("Arial", 11);
                if (textBox1.Text == "")
            {
                String sql = "Select Client.id,Client.name,Client.phone,Client.mail,Client.view_,country_of_origin.litter,Client.INN,Client.KPP,Client.OGRN,Client.pc,Client.bank,Client.bik  from Client,country_of_origin where Client.country_of_registration=country_of_origin.id ORDER BY Client.id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Телефон";
                dataGridView1.Columns[3].HeaderText = "Почта";
                dataGridView1.Columns[4].HeaderText = "Статус клиента";
                dataGridView1.Columns[5].HeaderText = "Страна рЕГАИСтрации";
                dataGridView1.Columns[6].HeaderText = "ИНН";
                dataGridView1.Columns[7].HeaderText = "КПП";
                dataGridView1.Columns[8].HeaderText = "ОРГН";
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                String sql = "Select Client.id,Client.name,Client.phone,Client.mail,Client.view_,country_of_origin.litter,Client.INN,Client.KPP,Client.OGRN,Client.pc,Client.bank,Client.bik  from Client,country_of_origin where Client.country_of_registration=country_of_origin.id Client.name ILIKE '";
                sql += textBox1.Text;
                sql += "%' ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "ФИО";
                dataGridView1.Columns[2].HeaderText = "Телефон";
                dataGridView1.Columns[3].HeaderText = "Почта";
                dataGridView1.Columns[4].HeaderText = "Статус клиента";
                dataGridView1.Columns[5].HeaderText = "Страна рЕГАИСтрации";
                dataGridView1.Columns[6].HeaderText = "ИНН";
                dataGridView1.Columns[7].HeaderText = "КПП";
                dataGridView1.Columns[8].HeaderText = "ОРГН";
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
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
                String sqli = "Select Address_cl.id, Client.id, Address_cl.country_cl,Address_cl.city_cl,Address_cl.street_cl,Address_cl.house_cl,Address_cl.post_in_cl  from Client, Address_cl  where Client.id =  Address_cl.id_client and Client.id=:id ORDER BY Address_cl.id ASC;";

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
                String sqli = "Select Address_cl.id, Client.id,  Address_cl.country_cl,Address_cl.city_cl,Address_cl.street_cl,Address_cl.house_cl,Address_cl.post_in_cl  from Client, Address_cl  where Client.id =  Address_cl.id_client ORDER BY Address_cl.id ASC;";

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
        private void client_in_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;
            Update();
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
                else id = 1;
            else id = dataGridView1.RowCount;
            updateaddressinfo(id);
            }

            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Arial", 11);
        }
    }
}
