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
    public partial class invoices : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public invoices(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }
        public void Update()
        {
            try { 
            label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           
                String sql = "Select invoices.id,invoices.num_invoices, Client.name,storehouse.name, invoices.data,invoices.num_Contract,invoices.total_sum,invoices.shipment,invoices.status from Client, storehouse,invoices where Client.id=invoices.id_client and invoices.id_storehouse=storehouse.id ORDER BY invoices.id";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Номер накладной";
                dataGridView1.Columns[2].HeaderText = "Клиент";
                dataGridView1.Columns[3].HeaderText = "Склад";
                dataGridView1.Columns[4].HeaderText = "Дата оформления";
                dataGridView1.Columns[5].HeaderText = "Номер договора";
                dataGridView1.Columns[6].HeaderText = "Общая сумма";
                dataGridView1.Columns[7].HeaderText = "Дата отгрузки";
                dataGridView1.Columns[8].HeaderText = "Статус";
     
                this.StartPosition = FormStartPosition.CenterScreen;
            
            }
            catch { }
        }
        public void updateaddressinfo(int id)
            {
            try { 
                if (id != null)
                {
                    String sqli = "Select invoices_info.id, invoices.id,batch_number.number, Product_card.code,Product.name,Product_card.name_firm,unit_of_measurement.litter, invoices_info.quantity,invoices_info.price,  NDS.percent from Product_card,Product,batch_number,unit_of_measurement,NDS,invoices_info,invoices where Product_card.id_ed=unit_of_measurement.id and invoices.id =invoices_info.id_invoices and batch_number.id=invoices_info.id_batch_number and NDS.id=Product_card.id_nds and invoices.id=:id ORDER BY invoices_info.id ASC;";

                    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
                    dai.SelectCommand.Parameters.AddWithValue("id", id);
                    dsi.Reset();
                    dai.Fill(dsi);
                    dti = dsi.Tables[0];
                    dataGridView2.DataSource = dti;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].HeaderText = "Номер партии";
                    dataGridView2.Columns[3].HeaderText = "Код товара";
                    dataGridView2.Columns[4].HeaderText = "Название товара";
                    dataGridView2.Columns[5].HeaderText = "Производитель";
                    dataGridView2.Columns[6].HeaderText = "Единица измерения";
                    dataGridView2.Columns[7].HeaderText = "Количество";
                    dataGridView2.Columns[8].HeaderText = "Цена";
                    dataGridView2.Columns[9].HeaderText = "НДС";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                
            }
            catch { }
        }
        private void invoices_Load(object sender, EventArgs e)
        {
            Update();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void личныеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newclient f = new newclient(con, -1, "", "", "", "", "", "", "", "", "", "", "");
            f.ShowDialog();
            Update();
        }

        private void адресToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            newaddressinfo f = new newaddressinfo(con, -1, id, "", "", "", "", "");
            f.ShowDialog();
            Update();
            updateaddressinfo(id);
        }

        private void личныеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            string name = (string)dataGridView1.CurrentRow.Cells[1].Value;
            string phone = (string)dataGridView1.CurrentRow.Cells[2].Value;
            string mail = (string)dataGridView1.CurrentRow.Cells[3].Value;
            string view = (string)dataGridView2.CurrentRow.Cells[4].Value;
            string country_of_registration = (string)dataGridView2.CurrentRow.Cells[5].Value;
            string INN = (string)dataGridView1.CurrentRow.Cells[6].Value;
            string KPP = (string)dataGridView1.CurrentRow.Cells[7].Value;
            string OGRN = (string)dataGridView1.CurrentRow.Cells[8].Value;
            string pc = (string)dataGridView1.CurrentRow.Cells[9].Value;
            string bank = (string)dataGridView1.CurrentRow.Cells[10].Value;
            string bik = (string)dataGridView1.CurrentRow.Cells[11].Value;
            newclient f = new newclient(con, id, name, phone, mail, view, country_of_registration, INN, KPP, OGRN, pc, bank, bik);
            f.ShowDialog();
            Update();
            updateaddressinfo(id);
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

        private void button5_Click(object sender, EventArgs e)
        {
            client_in fp = new client_in(con);
            fp.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            unit_of_measurement_in fp = new unit_of_measurement_in(con, -1, "");
            fp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Product_card fp = new Product_card(con,-1,"","", "",-1, -1);
            fp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            batch_number fp = new batch_number(con, -1,"",-1,-1,-1,-1);
            fp.ShowDialog();
        }
    }
}
