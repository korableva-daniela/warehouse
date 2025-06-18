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
    public partial class product_card_in : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public int id_c;
        public int div;
        public product_card_in(NpgsqlConnection con, int id_c,int div)
        {
            this.div = div;
            this.con = con;
            this.id_c = id_c;
            InitializeComponent();
        }
        public void Update()
        {
            try
            {
            
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                richTextBox1.Font = new Font("Arial", 11);
                richTextBox2.Font = new Font("Arial", 11);

                String sql = "Select Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro, unit_of_measurement.litter,country_of_origin.litter," +
                "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                "  from Type_to,Product_card ,unit_of_measurement ,country_of_origin ,NDS  " +
                "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                " Product_card.id_nds = NDS.id and Product_card.id = :id_c ORDER BY id ASC;";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                da.SelectCommand.Parameters.AddWithValue("id_c", this.id_c);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Код товара";
                dataGridView1.Columns[2].HeaderText = "Название товара";
                dataGridView1.Columns[3].HeaderText = "Тип товара";
                dataGridView1.Columns[4].HeaderText = "Название фирмы продукта";
       
                dataGridView1.Columns[5].HeaderText = "Единица измерения";

                dataGridView1.Columns[6].HeaderText = "Страна производитель";
                dataGridView1.Columns[7].HeaderText = "Номер ГТД";
                dataGridView1.Columns[8].HeaderText = "Номер РНПТ";
                dataGridView1.Columns[9].HeaderText = "НДС";
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].HeaderText = "Номер ставка акциза";
                dataGridView1.Columns[13].HeaderText = "Номер ЕГАИС";
                dataGridView1.Columns[14].Visible=false;



                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void description(int id)
        {
            try
            {

                if (id.ToString() != null)
                {


                    if (dataGridView1.CurrentRow != null)
                    {
                        if (dataGridView1.CurrentRow.Index > 0)
                        {

                            int id_k = (int)dataGridView1.CurrentRow.Cells[0].Value;
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            string name = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            string name_type = (string)dataGridView1.CurrentRow.Cells[3].Value;
                            string name_firm = (string)dataGridView1.CurrentRow.Cells[4].Value;
                            //string firm = (string)dataGridView1.CurrentRow.Cells[5].Value;
                            int col = (int)dataGridView1.CurrentRow.Cells[5].Value;
                            string ed = (string)dataGridView1.CurrentRow.Cells[6].Value;
                            string coun = (string)dataGridView1.CurrentRow.Cells[7].Value;
                            string gtd = (string)dataGridView1.CurrentRow.Cells[8].Value;
                            string rnpt = (string)dataGridView1.CurrentRow.Cells[9].Value;
                            int nds = (int)dataGridView1.CurrentRow.Cells[10].Value;
                            string code_post = (string)dataGridView1.CurrentRow.Cells[11].Value;
                            double pr_post = (double)dataGridView1.CurrentRow.Cells[12].Value;
                            string ak = (string)dataGridView1.CurrentRow.Cells[13].Value;
                            string egis = (string)dataGridView1.CurrentRow.Cells[14].Value;
                            richTextBox1.Clear();
                            richTextBox1.AppendText("             Карточка товара\n");
                            richTextBox1.AppendText("\n");

                            richTextBox1.AppendText("Код товара: " + code + "\n");
                            richTextBox1.AppendText("Название товара: " + name + "\n");
                            richTextBox1.AppendText("Тип товара: " + name_type + "\n");
                            richTextBox1.AppendText("Название фирмы товара: " + name_firm + "\n");
                            //richTextBox1.AppendText("Поставщик: " + firm + "\n");
                            richTextBox1.AppendText("Количество: " + col + "\n");
                            richTextBox1.AppendText("Единица измерения: " + ed + "\n");
                            richTextBox1.AppendText("Страна производитель: " + coun + "\n");

                            //richTextBox1.AppendText("Код товара от поставщика: " + code_post + "\n");
                            //richTextBox1.AppendText("Цена товара от поставщика: " + pr_post.ToString() + "\n");
                            richTextBox1.AppendText("НДС: " + nds.ToString() + "\n");
                            richTextBox1.AppendText("ГТД: " + gtd + "\n");
                            richTextBox1.AppendText("РНПТ: " + rnpt + "\n");
                            richTextBox1.AppendText("Ставка акциза: " + ak + "\n");
                            richTextBox1.AppendText("ЕГАИС: " + egis + "\n");

                        }
                        if (dataGridView1.CurrentRow.Index == 0 && id>0)
                        {
                            int id_k = (int)dataGridView1.CurrentRow.Cells[0].Value;
                            string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            string name = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            string name_type = (string)dataGridView1.CurrentRow.Cells[3].Value;
                            string name_firm = (string)dataGridView1.CurrentRow.Cells[4].Value;
                            //string firm = (string)dataGridView1.CurrentRow.Cells[5].Value;
                            int col = (int)dataGridView1.CurrentRow.Cells[5].Value;
                            string ed = (string)dataGridView1.CurrentRow.Cells[6].Value;
                            string coun = (string)dataGridView1.CurrentRow.Cells[7].Value;
                            string gtd = (string)dataGridView1.CurrentRow.Cells[8].Value;
                            string rnpt = (string)dataGridView1.CurrentRow.Cells[9].Value;
                            int nds = (int)dataGridView1.CurrentRow.Cells[10].Value;
                            string code_post = (string)dataGridView1.CurrentRow.Cells[11].Value;
                            double pr_post = (double)dataGridView1.CurrentRow.Cells[12].Value;
                            string ak = (string)dataGridView1.CurrentRow.Cells[13].Value;
                            string egis = (string)dataGridView1.CurrentRow.Cells[14].Value;
                            richTextBox1.Clear();
                            richTextBox1.AppendText("             Карточка товара\n");
                            richTextBox1.AppendText("\n");

                            richTextBox1.AppendText("Код товара: " + code + "\n");
                            richTextBox1.AppendText("Название товара: " + name + "\n");
                            richTextBox1.AppendText("Тип товара: " + name_type + "\n");
                            richTextBox1.AppendText("Название фирмы товара: " + name_firm + "\n");
                            //richTextBox1.AppendText("Поставщик: " + firm + "\n");
                            richTextBox1.AppendText("Количество: " + col + "\n");
                            richTextBox1.AppendText("Единица измерения: " + ed + "\n");
                            richTextBox1.AppendText("Страна производитель: " + coun + "\n");

                            //richTextBox1.AppendText("Код товара от поставщика: " + code_post + "\n");
                            //richTextBox1.AppendText("Цена товара от поставщика: " + pr_post.ToString() + "\n");
                            richTextBox1.AppendText("НДС: " + nds.ToString() + "\n");
                            richTextBox1.AppendText("ГТД: " + gtd + "\n");
                            richTextBox1.AppendText("РНПТ: " + rnpt + "\n");
                            richTextBox1.AppendText("Ставка акциза: " + ak + "\n");
                            richTextBox1.AppendText("ЕГАИС: " + egis + "\n");
                        }
                    }
                    else richTextBox1.Text = " ";
                    // else richTextBox1.Text =" ";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else richTextBox1.Text = " ";
            }
            catch { }
        }
        public void description_d(int id)
        {
            if (id.ToString() != null)
            {


                if (dataGridView1.CurrentRow != null)
                {
                    if (dataGridView1.CurrentRow.Index > 0)
                    {
                        richTextBox2.Clear();
                        string desc = (string)dataGridView1.CurrentRow.Cells[15].Value;
                        richTextBox2.Text = desc;
                    }
                    if (dataGridView1.CurrentRow.Index == 0)
                    {
                        if (dataGridView1.Rows[0].Cells[0].Value != null)
                        {
                            richTextBox2.Clear();
                            string desc = dataGridView1.CurrentRow.Cells[15].Value.ToString();
                            richTextBox2.Text = desc;
                        }
                    }
                }
                else richTextBox1.Text = " ";
                // else richTextBox1.Text =" ";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else richTextBox1.Text = " ";

        }
        private void button4_Click(object sender, EventArgs e)
        {
            unit_of_measurement_in fp = new unit_of_measurement_in(con, - 1, "");
            fp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Firm_in fp = new Firm_in(con);
            fp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            country_of_origin_in fp = new country_of_origin_in(con, -1, "");
            fp.ShowDialog();
        }

        private void product_card_in_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            Update();
            dataGridView1.ReadOnly = true;
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
            description(id);
            description_d(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            filter fp = new filter(con, this.div);
            fp.ShowDialog();
        }
    }
}
