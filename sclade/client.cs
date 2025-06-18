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
    public partial class client : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public int id;

        public string name;
        public client(NpgsqlConnection con, int id, string name)
        {
            this.id = id;

            this.name = name;
            this.con = con;
            InitializeComponent();
        }
        public void Update()
        {
            if (id != 0)
            {
                button1.Visible = false;
                this.WindowState = FormWindowState.Maximized;
                
            }
            if(id==0)
            {
                dataGridView2.Visible = false;
                label1.Visible = false;
            }
            try
            {
                label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
                textBox1.Font = new Font("Arial", 11);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (textBox1.Text =="")
            {
                String sql = "Select Client.id,Client.name,Client.phone,Client.mail,Client.view_,country_of_origin.litter,Client.INN,Client.KPP,Client.OGRN,Client.pc,Client.bank,Client.bik  from Client,country_of_origin where Client.country_of_registration=country_of_origin.id ORDER BY Client.id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Название";
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
                dataGridView1.Columns[1].HeaderText = "Название";
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
                dataGridView2.Columns[0].Visible=false;
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

        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void client_Load(object sender, EventArgs e)
        {
            try
            {
                
                Update();
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                newclient f = new newclient(con, -1, "", "", "","","","","","","","","");
            f.ShowDialog();
            Update();
            }
            catch { }
        }

        private void адресToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                newaddressinfo f = new newaddressinfo(con, -1, id, "", "", "", "", "");
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
                    {
                        try
                        {

                            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string name = (string)dataGridView1.CurrentRow.Cells[1].Value;
                string phone = (string)dataGridView1.CurrentRow.Cells[2].Value;
                string mail = (string)dataGridView1.CurrentRow.Cells[3].Value;
                string view = (string)dataGridView1.CurrentRow.Cells[4].Value;
                string country_of_registration = (string)dataGridView1.CurrentRow.Cells[5].Value;
                string INN = (string)dataGridView1.CurrentRow.Cells[6].Value;
                string KPP = (string)dataGridView1.CurrentRow.Cells[7].Value;
                string OGRN = (string)dataGridView1.CurrentRow.Cells[8].Value;
                string pc = (string)dataGridView1.CurrentRow.Cells[9].Value;
                string bank = (string)dataGridView1.CurrentRow.Cells[10].Value;
                string bik = (string)dataGridView1.CurrentRow.Cells[11].Value;
                newclient f = new newclient(con, id, name, phone, mail,  view,  country_of_registration,  INN, KPP,  OGRN, pc,  bank,  bik);
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
            }
            catch { }

        }

        private void адресToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView2.CurrentRow.Cells[0].Value;
                int id_client = (int)dataGridView2.CurrentRow.Cells[1].Value;
                string country = (string)dataGridView2.CurrentRow.Cells[2].Value;
                string city = (string)dataGridView2.CurrentRow.Cells[3].Value;
                string street = (string)dataGridView2.CurrentRow.Cells[4].Value;
                string house = (string)dataGridView2.CurrentRow.Cells[5].Value;
                string post_in = (string)dataGridView2.CurrentRow.Cells[6].Value;
                newaddressinfo f = new newaddressinfo(con, id, id_client, country, city, street, house, post_in);
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
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
                else id = 1;
            else id = dataGridView1.RowCount;
            updateaddressinfo(id);
            }
            catch { }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void личныеДанныеToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Client WHERE id=:id", con);
                NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM  Address_cl   WHERE id_client=:id", con);
                command.Parameters.AddWithValue("id", id);
                command1.Parameters.AddWithValue("id", id);
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    command1.ExecuteNonQuery();
                    command.ExecuteNonQuery();
                    Update();
                }
                else
                    Update();
                updateaddressinfo(id);

            }

            catch { }
        }

        private void адресToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)dataGridView2.CurrentRow.Cells["id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM  Address_cl   WHERE id=:id", con);
            command.Parameters.AddWithValue("id", id);
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
               
                command.ExecuteNonQuery();
                Update();
            }
            else
                Update();
            updateaddressinfo(id);
            }

            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font("Arial", 11);
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int id_ = (int)dataGridView1.CurrentRow.Cells[0].Value;
            string name_ = (string)dataGridView1.CurrentRow.Cells[1].Value;

            this.name = name_;
            this.id = id_;
            Close();
        }
    }
}
