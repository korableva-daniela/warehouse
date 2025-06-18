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
using System.Text.RegularExpressions;
namespace sclade
{
    public partial class new_product_shipment : Form
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        DataTable dt4 = new DataTable();
        DataSet ds4 = new DataSet();
        DataTable dt5 = new DataTable();
        DataSet ds5 = new DataSet();
        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt11 = new DataTable();
        DataSet ds11 = new DataSet();
        DataTable dt14 = new DataTable();
        DataSet ds14 = new DataSet();
        DataTable dt12 = new DataTable();
        DataSet ds12 = new DataSet();
        Regex regex1 = new Regex(@"\d$");
        public NpgsqlConnection con;
        string str_place;
        public int id_invoices;
        int count_info;
        public int quantity;
        public int all_quantity;
        public string id_storehouse;
        public int ind;
        public string stor;
        int st;
        string num;
        int id_invoices_info;
        public int id_em;
        int id_prod_storehouse;
        int id_prod_storehouse_info;
        public int div;
        public new_product_shipment(NpgsqlConnection con, int id_invoices, string stor, int ind, int id_em, int quantity, int all_quantity,int div)
        {
            this.div = div;
            this.all_quantity = all_quantity;
            this.id_invoices = id_invoices;
            this.ind = ind;
            this.quantity = quantity;
            InitializeComponent();
            this.con = con;
            this.stor = stor;
            this.id_em = id_em;
        }
        public void updateProduct_cardinfo(int id_pro)
        {
            try
            {

                String sql4 = @"SELECT id, code FROM Product_card 
                                                  WHERE id = ";
                sql4 += id_pro.ToString();


                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
                ds4.Reset();
                da4.Fill(ds4);
                dt4 = ds4.Tables[0];
                comboBox1.DataSource = dt4;
                comboBox1.DisplayMember = "code";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;

            }
            catch { }
        }
        public void updatebatch_numberinfo(int id_b)
        {



            try
            {




                string sql5 = "SELECT id, number,id_Firm FROM batch_number WHERE   id =";
                sql5 += id_b.ToString();
                //sql2 += "and id_Firm = (select id from Firm where id=";
                //sql2 += this.id_Firm.ToString();
                //sql2 += ")";






                using (NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con))
                {
                    //da2.SelectCommand.Parameters.AddWithValue("@id_c", id_c);

                    ds5.Reset();
                    da5.Fill(ds5);

                    dt5 = ds5.Tables[0];


                    comboBox2.DataSource = dt5;
                    comboBox2.DisplayMember = "number";
                    comboBox2.ValueMember = "id";
                }
                //updatepricerinfo();
            }
            catch (Exception ex)
            {

            }
        }

        public void updateProduct_cardinfoupdate(string code)
        {
            try
            {



                try
                {

                    String sql4 = @"SELECT id, code FROM Product_card 
                                                  WHERE code = '";
                    sql4 += code;
                    sql4 += "'";


                    NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
                    ds4.Reset();
                    da4.Fill(ds4);
                    dt4 = ds4.Tables[0];
                    comboBox1.DataSource = dt4;
                    comboBox1.DisplayMember = "code";
                    comboBox1.ValueMember = "id";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
                //}
            }
            catch { }



        }
        public void updatebreachinfoupdate(string number)
        {
            try
            {



                try
                {

                    String sql5 = @"SELECT id, number FROM batch_number 
                                                  WHERE number = '";
                    sql5 += number;
                    sql5 += "'";


                    NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con);
                    ds5.Reset();
                    da5.Fill(ds5);
                    dt5 = ds5.Tables[0];
                    comboBox2.DataSource = dt5;
                    comboBox2.DisplayMember = "number";
                    comboBox2.ValueMember = "id";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
                //}
            }
            catch { }



        }
        //public void updatestorehouseinfoupdate(string name)
        //{
        //    try
        //    {
        //        String sql8 = "Select * from storehouse where name='";
        //        sql8 += name;
        //        sql8 += "'";
        //        NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
        //        ds8.Reset();
        //        da8.Fill(ds8);
        //        dt8 = ds8.Tables[0];
        //        if (dt8.Rows.Count > 0)
        //        {
        //            st = Convert.ToInt32(dt8.Rows[0]["id"]);

        //        }
        //        this.StartPosition = FormStartPosition.CenterScreen;

        //    }
        //    catch { }
        //}


        public void Update()
        {
            try
            {
                if (this.ind == 0)
                {
                    String sql1 = "Select storehouse.name,invoices_in.num_invoices,storehouse.id from invoices_in,storehouse where invoices_in.id_storehouse=storehouse.id and invoices_in.id = " + this.id_invoices;
                    NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                    ds1.Reset();
                    da1.Fill(ds1);
                    dt1 = ds1.Tables[0];

                    if (dt1.Rows.Count > 0)
                    {
                        label11.Font = new Font("Arial", 11);
                        label11.Text = "Название склада: " + dt1.Rows[0][0].ToString();
                        label6.Font = new Font("Arial", 11);
                        label6.Text = "Номер накладной: " + dt1.Rows[0][1].ToString();
                        st = Convert.ToInt32(dt1.Rows[0][2]);
                        num = dt1.Rows[0][1].ToString();

                    }
                    String sql2 = "Select invoices_in_info.id, invoices_in.id,invoices_in.num_invoices,Product_card.id,batch_number.id,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter, invoices_in_info.quantity,invoices_in_info.count from Product_card,batch_number,unit_of_measurement,invoices_in_info,invoices_in where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and invoices_in.id =invoices_in_info.invoices_in and batch_number.id=invoices_in_info.id_batch_number and invoices_in_info.count!= invoices_in_info.quantity and invoices_in.id=:id ORDER BY invoices_in_info.id ASC;";

                    NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sql2, con);
                    da2.SelectCommand.Parameters.AddWithValue("id", this.id_invoices);
                    ds2.Reset();
                    da2.Fill(ds2);
                    dt2 = ds2.Tables[0];
                    dataGridView3.DataSource = dt2;
                    dataGridView3.Columns[0].Visible = false;
                    dataGridView3.Columns[1].Visible = false;
                    dataGridView3.Columns[2].Visible = false;
                    dataGridView3.Columns[3].Visible = false;
                    dataGridView3.Columns[4].Visible = false;
                    dataGridView3.Columns[5].HeaderText = "Номер партии";
                    dataGridView3.Columns[6].HeaderText = "Код товара";
                    dataGridView3.Columns[7].HeaderText = "Название товара";
                    dataGridView3.Columns[8].HeaderText = "Производитель";
                    dataGridView3.Columns[9].HeaderText = "Единица измерения";
                    dataGridView3.Columns[10].HeaderText = "Количество";
                    dataGridView3.Columns[11].HeaderText = "Количество собранного товара";
                }
                else
                {
                    if (this.ind == 1)
                    {
                        String sql1 = "Select storehouse.name,moving.num_invoices,storehouse.id from moving,storehouse where moving.id_storehouse_2=storehouse.id and moving.id = " + this.id_invoices;
                        NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                        ds1.Reset();
                        da1.Fill(ds1);
                        dt1 = ds1.Tables[0];

                        if (dt1.Rows.Count > 0)
                        {
                            label11.Font = new Font("Arial", 11);
                            label11.Text = "Название склада: " + dt1.Rows[0][0].ToString();
                            label6.Font = new Font("Arial", 11);
                            label6.Text = "Номер накладной: " + dt1.Rows[0][1].ToString();
                            num = dt1.Rows[0][1].ToString();
                            st = Convert.ToInt32(dt1.Rows[0][2]);

                        }
                        String sql2 = "Select moving_info.id, moving.id,moving.num_invoices,Product_card.id,batch_number.id,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter, moving_info.quantity,moving_info.count from Product_card,batch_number,unit_of_measurement,moving_info,moving where batch_number.id_ed=unit_of_measurement.id and batch_number.id_pro_card=Product_card.id and moving.id =moving_info.invoices_in and batch_number.id=moving_info.id_batch_number and moving_info.count!= moving_info.quantity and moving.id=:id ORDER BY moving_info.id ASC;";

                        NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sql2, con);
                        da2.SelectCommand.Parameters.AddWithValue("id", this.id_invoices);
                        ds2.Reset();
                        da2.Fill(ds2);
                        dt2 = ds2.Tables[0];
                        dataGridView3.DataSource = dt2;
                        dataGridView3.Columns[0].Visible = false;
                        dataGridView3.Columns[1].Visible = false;
                        dataGridView3.Columns[2].Visible = false;
                        dataGridView3.Columns[3].Visible = false;
                        dataGridView3.Columns[4].Visible = false;
                        dataGridView3.Columns[5].HeaderText = "Номер партии";
                        dataGridView3.Columns[6].HeaderText = "Код товара";
                        dataGridView3.Columns[7].HeaderText = "Название товара";
                        dataGridView3.Columns[8].HeaderText = "Производитель";
                        dataGridView3.Columns[9].HeaderText = "Единица измерения";
                        dataGridView3.Columns[10].HeaderText = "Количество";
                        dataGridView3.Columns[11].HeaderText = "Количество собранного товара";
                    }
                }
            }

            catch { }
        }
        public void Update_shelf(int id_pro, int id_br)
        {
            try
            {

                String sql11 = "Select prod_storehouse.id, prod_storehouse.id_store, prod_storehouse_info.id, prod_storehouse.num_place, SUM(prod_storehouse_info.count) from  prod_storehouse,prod_storehouse_info where prod_storehouse.id_store= " + st.ToString() + " and prod_storehouse_info.id_prod_storehouse=prod_storehouse.id and prod_storehouse_info.id_batch_number= " + id_br.ToString() + "  and prod_storehouse_info.id_product_card= " + id_pro.ToString() + " and prod_storehouse_info.count> 0 Group by prod_storehouse.id,prod_storehouse.num_place,  prod_storehouse_info.id";
                NpgsqlDataAdapter da11 = new NpgsqlDataAdapter(sql11, con);
                ds11.Reset();
                da11.Fill(ds11);
                dt11 = ds11.Tables[0];

                if (dt11.Rows.Count > 0)
                {

                    dataGridView1.DataSource = dt11;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].HeaderText = "Номер полки";
                    dataGridView1.Columns[4].HeaderText = "Количество выбранного товара на полке";
                   


                }
                else { MessageBox.Show($"Товар ещё не размещен на полках.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            }

            catch { }
        }

        private void new_product_shipment_Load(object sender, EventArgs e)
        {
            try
            {

                dataGridView3.ContextMenuStrip = contextMenuStrip2;

                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView3.ContextMenuStrip = contextMenuStrip2;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                Update();
                //if (this.stor != "")
                //{
                //    updatestorehouseinfoupdate(this.stor);
                //}
                //if (this.id_invoices != -1)
                //{
                //    updatenumberinfo();
                //}

                comboBox1.Font = new Font("Arial", 11);
                comboBox2.Font = new Font("Arial", 11);
                comboBox5.Font = new Font("Arial", 11);
                comboBox3.Font = new Font("Arial", 11);

                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox5.Enabled = false;
                comboBox3.Enabled = false;

                textBox2.Enabled = false;


                dataGridView3.ReadOnly = true;
                //updatepricerinfo();

                Update();



                textBox1.Font = new Font("Arial", 11);
                textBox2.Font = new Font("Arial", 11);

                comboBox1.Text = "Товар не выбран";
                comboBox2.Text = "Партия товара не выбрана";

                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label10.Font = new Font("Arial", 11);


                label10.Font = new Font("Arial", 11);

            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView3.CurrentRow != null && dataGridView3.CurrentRow.Cells[0].Value != null)
                {
                    int id_c = 0;
                    string number = "";


                    int id_pro_card = (int)dataGridView3.CurrentRow.Cells[3].Value;


                    int id_batch = (int)dataGridView3.CurrentRow.Cells[4].Value;
                    if (id_batch != null && id_pro_card != null)
                    {



                        updatebatch_numberinfo(id_batch);
                        updateProduct_cardinfo(id_pro_card);
                        comboBox5.Text = (Convert.ToInt32(dataGridView3.CurrentRow.Cells[10].Value) - Convert.ToInt32(dataGridView3.CurrentRow.Cells[11].Value)).ToString();
                        id_invoices_info = (int)dataGridView3.CurrentRow.Cells[0].Value;
                        count_info = Convert.ToInt32(dataGridView3.CurrentRow.Cells[11].Value);
                        if (comboBox1.SelectedValue!=null && comboBox2.SelectedValue!=null)
                        {
                            id_pro_card = (int)comboBox1.SelectedValue;
                            id_batch = (int)comboBox2.SelectedValue;
                        Update_shelf(id_pro_card, id_batch);
                        }
                    }
                    else
                    {
                        comboBox2.Text = "Партия не выбрана";

                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(comboBox5.Text))
                {
                    DialogResult result1 = MessageBox.Show("Столько товара нет в поступлении", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.BackColor = Color.DarkSalmon;
                }
                else
                {
                    if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(comboBox3.Text))
                    {
                        DialogResult result1 = MessageBox.Show("Столько товара нет на полке", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.BackColor = Color.DarkSalmon;
                    }
                    else {

                        DialogResult result2 = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result2 == DialogResult.Yes)
                        {

                            //command2.ExecuteNonQuery();

                            quantity = quantity + Convert.ToInt32(textBox1.Text);
                            string sql3 = "update prod_storehouse_info set  count=:count,id_Employee=:id_Employee where id=:id";
                            NpgsqlCommand command3 = new NpgsqlCommand(sql3, con);


                            command3.Parameters.AddWithValue("id", id_prod_storehouse_info);
                            command3.Parameters.AddWithValue("count", Convert.ToDouble(comboBox3.Text) - Convert.ToDouble(textBox1.Text));
                 
                            command3.Parameters.AddWithValue("id_Employee", this.id_em);
                            command3.ExecuteNonQuery();
                            if (ind == 0)
                            {
                                string sql4 = "update invoices_in_info set count=:count where id=:id";
                                NpgsqlCommand command4 = new NpgsqlCommand(sql4, con);
                                command4.Parameters.AddWithValue("count", Convert.ToDouble(textBox1.Text) + count_info);
                                command4.Parameters.AddWithValue("id", id_invoices_info);
                                command4.ExecuteNonQuery();
                                Update();
                            }
                            if (ind == 1)
                            {
                                string sql4 = "update moving_info set count=:count where id=:id";
                                NpgsqlCommand command4 = new NpgsqlCommand(sql4, con);
                                command4.Parameters.AddWithValue("count", Convert.ToDouble(textBox1.Text) + count_info);
                                command4.Parameters.AddWithValue("id", id_invoices_info);
                                command4.ExecuteNonQuery();
                                Update();

                            }
                        }
                        if (dataGridView3.CurrentRow != null && dataGridView3.CurrentRow.Cells[0].Value != null)
                        {
                            id_invoices_info = (int)dataGridView3.CurrentRow.Cells[0].Value;
                            count_info = Convert.ToInt32(dataGridView3.CurrentRow.Cells[11].Value);
                            comboBox5.Text = (Convert.ToInt32(dataGridView3.CurrentRow.Cells[10].Value) - Convert.ToInt32(dataGridView3.CurrentRow.Cells[11].Value)).ToString();
                        }
                        if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                        {
                            int id_pro_card = (int)comboBox1.SelectedValue;


                            int id_batch = (int)comboBox2.SelectedValue;
                            Update_shelf(id_pro_card, id_batch);
                            comboBox3.Text = (Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value)).ToString();
                            str_place = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                            textBox2.Text = str_place;
                            id_prod_storehouse_info = (int)dataGridView1.CurrentRow.Cells[2].Value;

                        }

                       

                    }
                }
              
            }






            catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView3.CurrentRow.Cells[3].Value != null)
                {

                    int id_pro = (int)dataGridView3.CurrentRow.Cells[3].Value;



                    prod_info fp = new prod_info(con, "", id_pro);
                    fp.ShowDialog();
                }
            }
            catch { }
        }

        private void информацияОПартииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView3.CurrentRow.Cells[4].Value != null)
                {


                    int id_batch_number = (int)dataGridView3.CurrentRow.Cells[4].Value;

                    batch_info fp = new batch_info(con, "", id_batch_number);
                    fp.ShowDialog();
                }

            }
            catch { }
        }

        private void информацияОНакладнойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.ind == 0)
                {




                    invoices_ fp = new invoices_(con, st, this.id_em, num, 0, this.div);
                    fp.ShowDialog();
                }
                if (this.ind == 1)
                {




                    moving fp = new moving(con, -1, this.id_em, st, num, 0, this.div);
                    fp.ShowDialog();
                }



            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                
                if(dataGridView1.CurrentRow!=null&& dataGridView1.CurrentRow.Cells[0].Value!=null)
                { 

                //int id_pro_card = (int)dataGridView3.CurrentRow.Cells[0].Value;


                    comboBox3.Text = (Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value)).ToString();
                    str_place = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox2.Text = str_place;
                    id_prod_storehouse_info= (int)dataGridView1.CurrentRow.Cells[2].Value;
                }
                
            }
            catch { }
        }
    }
}
