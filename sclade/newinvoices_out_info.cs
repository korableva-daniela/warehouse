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
    public partial class newinvoices_out_info : Form
    {
        Regex regex1 = new Regex(@"\d$");
        public NpgsqlConnection con;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataTable dt8= new DataTable();
        DataSet ds8 = new DataSet();
        DataTable dt10 = new DataTable();
        DataSet ds10 = new DataSet();
        DataTable dt11 = new DataTable();
        DataSet ds11 = new DataSet();
        DataTable dt12 = new DataTable();
        DataSet ds12 = new DataSet();
        DataTable dt13 = new DataTable();
        DataSet ds13 = new DataSet();
        DataTable dt14 = new DataTable();
        DataSet ds14 = new DataSet();
        DataTable dt15 = new DataTable();
        DataSet ds15 = new DataSet();
        DataTable dt16 = new DataTable();
        DataSet ds16 = new DataSet();
        DataTable dt17 = new DataTable();
        DataSet ds17 = new DataSet();
        DataTable dt18 = new DataTable();
        DataSet ds18 = new DataSet();
        DataTable dt19 = new DataTable();
        DataSet ds19 = new DataSet();
        DataTable dt20 = new DataTable();
        DataSet ds20 = new DataSet();
        public int id;
        public int id_invoices;
        public string id_Product_card;
        public string id_batch_number;
        public int quantity;
        public double price;
        public string id_storehouse;
        public int tmp;
        public string stor;
        int st;
        public int div;
        public newinvoices_out_info(NpgsqlConnection con, int id, int id_invoices, string id_Product_card, string id_batch_number, int quantity, double price, string stor,int div)
        {
            this.div = div;
            this.id = id;
            this.id_invoices = id_invoices;
            this.id_Product_card = id_Product_card;
            this.id_batch_number = id_batch_number;
            this.price = price;
            this.quantity = quantity;
            InitializeComponent();
            this.con = con;
            this.stor = stor;
           
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
                if (dt8.Rows.Count > 0)
                {
                    st = Convert.ToInt32(dt8.Rows[0]["id"]);

                }
                this.StartPosition = FormStartPosition.CenterScreen;

            }
            catch { }
        }
        public void updateProduct_cardinfo(int id_pro)
        {
            try
            {

                String sql1 = @"SELECT id, code FROM Product_card 
                                                  WHERE id = ";
                sql1 += id_pro.ToString();


                NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                ds1.Reset();
                da1.Fill(ds1);
                dt1 = ds1.Tables[0];
                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "code";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;

            }
            catch { }
        }
        public void updatebatch_numberinfo(int id_b)
        {


            if (comboBox1.SelectedValue == null)
            {
                comboBox2.DataSource = null;
                return;
            }
            try
            {

                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out int id_c))
                {

                    return;
                }


                string sql2 = "SELECT id, number,id_Firm FROM batch_number WHERE   id =";
                sql2 += id_b.ToString();
                //sql2 += "and id_Firm = (select id from Firm where id=";
                //sql2 += this.id_Firm.ToString();
                //sql2 += ")";






                using (NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sql2, con))
                {
                    //da2.SelectCommand.Parameters.AddWithValue("@id_c", id_c);

                    ds2.Reset();
                    da2.Fill(ds2);

                    dt2 = ds2.Tables[0];


                    comboBox2.DataSource = dt2;
                    comboBox2.DisplayMember = "number";
                    comboBox2.ValueMember = "id";
                }
                //updatepricerinfo();
            }
            catch (Exception ex)
            {

            }
        }
        public void updatenumberinfo()
        {


            try
            {
                String sql12 = "Select * from invoices_in where id =";
                sql12 += this.id_invoices.ToString();

                NpgsqlDataAdapter da12 = new NpgsqlDataAdapter(sql12, con);
                ds12.Reset();
                da12.Fill(ds12);
                dt12 = ds12.Tables[0];
                comboBox3.DataSource = dt12;
                comboBox3.DisplayMember = "num_invoices";
                comboBox3.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }







        }
        public void updatepricerinfo(int id_b)
        {



            if (comboBox2.SelectedValue == null)
            {
                comboBox4.DataSource = null;
                return;
            }
            try
            {

                if (!int.TryParse(comboBox2.SelectedValue.ToString(), out int id_c))
                {

                    return;
                }


                string sql2 = @"SELECT * FROM batch_number WHERE id =";
                sql2 += id_b.ToString();

                using (NpgsqlDataAdapter da13 = new NpgsqlDataAdapter(sql2, con))
                {
                    //da13.SelectCommand.Parameters.AddWithValue("@id_c", id_c);

                    ds13.Reset();
                    da13.Fill(ds13);

                    dt13 = ds13.Tables[0];


                    comboBox4.DataSource = dt13;
                    comboBox4.DisplayMember = "price";
                    comboBox4.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {

            }





        }
        public void updatepricerinfoupdate(string number)
        {



            if (comboBox2.SelectedValue == null)
            {
                comboBox4.DataSource = null;
                return;
            }
            try
            {

                if (!int.TryParse(comboBox2.SelectedValue.ToString(), out int id_c))
                {

                    return;
                }


                String sql16 = @"SELECT id, number FROM batch_number 
                                                  WHERE number = '";
                sql16 += number;
                sql16 += "'";

                using (NpgsqlDataAdapter da13 = new NpgsqlDataAdapter(sql16, con))
                {
                    //da13.SelectCommand.Parameters.AddWithValue("@id_c", id_c);

                    ds13.Reset();
                    da13.Fill(ds13);

                    dt13 = ds13.Tables[0];


                    comboBox4.DataSource = dt13;
                    comboBox4.DisplayMember = "price";
                    comboBox4.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {

            }





        }
        public void updateProduct_cardinfoupdate(string code)
        {
            try
            {

                //if (id_pro != -1)
                //{
                //    comboBox1.Text = "Товар не выбран";
                //comboBox2.Text = "Товар не выбран";
                //String sql1 = "Select * from Product_card ORDER BY code ASC";

                //NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                //ds1.Reset();
                //da1.Fill(ds1);
                //dt1 = ds1.Tables[0];
                //comboBox1.DataSource = dt1;
                //comboBox1.DisplayMember = "code";
                //comboBox1.ValueMember = "id";
                //this.StartPosition = FormStartPosition.CenterScreen;
                ////}
                //else
                //{

                try
                {

                    String sql15 = @"SELECT id, code FROM Product_card 
                                                  WHERE code = '";
                    sql15 += code;
                    sql15 += "'";


                    NpgsqlDataAdapter da15 = new NpgsqlDataAdapter(sql15, con);
                    ds15.Reset();
                    da15.Fill(ds15);
                    dt15 = ds15.Tables[0];
                    comboBox1.DataSource = dt15;
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

                //if (id_pro != -1)
                //{
                //    comboBox1.Text = "Товар не выбран";
                //comboBox2.Text = "Товар не выбран";
                //String sql1 = "Select * from Product_card ORDER BY code ASC";

                //NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                //ds1.Reset();
                //da1.Fill(ds1);
                //dt1 = ds1.Tables[0];
                //comboBox1.DataSource = dt1;
                //comboBox1.DisplayMember = "code";
                //comboBox1.ValueMember = "id";
                //this.StartPosition = FormStartPosition.CenterScreen;
                ////}
                //else
                //{

                try
                {

                    String sql16 = @"SELECT id, number FROM batch_number 
                                                  WHERE number = '";
                    sql16 += number;
                    sql16 += "'";


                    NpgsqlDataAdapter da16 = new NpgsqlDataAdapter(sql16, con);
                    ds16.Reset();
                    da16.Fill(ds16);
                    dt16 = ds16.Tables[0];
                    comboBox2.DataSource = dt16;
                    comboBox2.DisplayMember = "number";
                    comboBox2.ValueMember = "id";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
                //}
            }
            catch { }



        }
        public void updatequantityinfo()
        {


            if (comboBox2.SelectedValue == null)
            {
                comboBox5.DataSource = null;
                return;
            }
            try
            {

                if (!int.TryParse(comboBox2.SelectedValue.ToString(), out int id_c))
                {

                    return;
                }


                string sql2 = @"SELECT * FROM prod_store WHERE id_store = ";
                sql2 += st.ToString();
                sql2 += " and id_product_card =  ";
                sql2 += comboBox1.SelectedValue.ToString();
                sql2 += " and id_batch_number =  ";
                sql2 += comboBox2.SelectedValue.ToString();


                using (NpgsqlDataAdapter da14 = new NpgsqlDataAdapter(sql2, con))
                {


                    ds14.Reset();
                    da14.Fill(ds14);

                    dt14 = ds14.Tables[0];


                    comboBox5.DataSource = dt14;
                    comboBox5.DisplayMember = "count_id_batch";
                    comboBox5.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {

            }
        }
      
   
        public void Update()
        {
            try
            {
                String sql_ = "Select invoices_in.id,invoices_in.num_invoices, Firm.name_f,storehouse.name, invoices_in.data,invoices_in.num_Contract,invoices_in.total_sum,invoices_in.total_sum_nds,invoices_in.shipment,invoices_in.status, Employee.name from Firm, storehouse,invoices_in,Employee where Firm.id = invoices_in.id_Firm and invoices_in.id_storehouse = storehouse.id and Employee.id = invoices_in.id_Employee and invoices_in.id = ";
                sql_ += id_invoices.ToString();
                NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql_, con);
                ds10.Reset();
                da10.Fill(ds10);
                dt10 = ds10.Tables[0];

                dataGridView2.DataSource = dt10;

                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Номер накладной";
                dataGridView2.Columns[2].HeaderText = "Поставщик";
                dataGridView2.Columns[3].HeaderText = "Склад";
                dataGridView2.Columns[4].HeaderText = "Дата оформления";
                dataGridView2.Columns[5].HeaderText = "Номер распоряжения";
                dataGridView2.Columns[6].HeaderText = "Общая сумма";
                dataGridView2.Columns[7].HeaderText = "Общая сумма c НДС";
                dataGridView2.Columns[8].HeaderText = "Дата отгрузки";
                dataGridView2.Columns[9].Visible = false;
                dataGridView2.Columns[10].HeaderText = "Обработчик";
                

                this.StartPosition = FormStartPosition.CenterScreen;




                String sql11 = "Select invoices_in_info.id, invoices_in.id,invoices_in.num_invoices,batch_number.number, Product_card.code,Product_card.name,Product_card.name_firm,unit_of_measurement.litter, invoices_in_info.quantity,invoices_in_info.price,  NDS.percent, invoices_in_info.price_nds, invoices_in_info.quantity*invoices_in_info.price,invoices_in_info.quantity*invoices_in_info.price_nds from Product_card,batch_number,unit_of_measurement,NDS,invoices_in_info,invoices_in where batch_number.id_ed=unit_of_measurement.id and invoices_in.id =invoices_in_info.invoices_in and batch_number.id=invoices_in_info.id_batch_number and NDS.id=Product_card.id_nds and batch_number.id_pro_card=Product_card.id and invoices_in.id=:id ORDER BY invoices_in_info.id ASC;";

                NpgsqlDataAdapter da11 = new NpgsqlDataAdapter(sql11, con);
                da11.SelectCommand.Parameters.AddWithValue("id", this.id_invoices);
                ds11.Reset();
                da11.Fill(ds11);
                dt11 = ds11.Tables[0];
                dataGridView1.DataSource = dt11;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].HeaderText = "Номер партии";
                dataGridView1.Columns[4].HeaderText = "Код товара";
                dataGridView1.Columns[5].HeaderText = "Название товара";
                dataGridView1.Columns[6].HeaderText = "Производитель";
                dataGridView1.Columns[7].HeaderText = "Единица измерения";
                dataGridView1.Columns[8].HeaderText = "Количество";
                dataGridView1.Columns[9].HeaderText = "Цена";
                dataGridView1.Columns[10].HeaderText = "НДС";
                dataGridView1.Columns[11].HeaderText = "Цена за одну единицу товара с НДС";
                dataGridView1.Columns[12].HeaderText = "Общая цена";
                dataGridView1.Columns[13].HeaderText = "Общая цена с НДС";
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            catch { }
        }



        private void newinvoices_out_info_Load(object sender, EventArgs e)
        {

            try
            {
                updatestorehouseinfoupdate(this.stor);
                dataGridView3.ContextMenuStrip = null;
                String sql19 = "Select DISTINCT  Product_card.code,Product_card.name, prod_store.count,storehouse.id,storehouse.name, Product_card.id from prod_store,storehouse,Product_card where prod_store.id_store=storehouse.id and Product_card.id=prod_store.id_product_card and storehouse.id =   ";
                sql19 += st.ToString();
                    sql19 += " ORDER BY prod_store.count ASC;";
                NpgsqlDataAdapter da19 = new NpgsqlDataAdapter(sql19, con);
                da19.SelectCommand.Parameters.AddWithValue("id", id);
                ds19.Reset();
                da19.Fill(ds19);
                dt19 = ds19.Tables[0];
                dataGridView3.DataSource = dt19;
                //dataGridView3.Columns[0].Visible = false;
            
                dataGridView3.Columns[0].HeaderText = "Код товара";
                dataGridView3.Columns[1].HeaderText = "Название товара";
                dataGridView3.Columns[2].HeaderText = "Количество товара на складе";
                dataGridView3.Columns[3].Visible = false;
                dataGridView3.Columns[4].Visible = false;
                dataGridView3.Columns[5].Visible = false;
                if (dt19.Rows.Count > 0)
                {
                    label11.Font = new Font("Arial", 11);
                    label11.Text = "Название склада: "+ dt19.Rows[0][4].ToString();

                }
                this.StartPosition = FormStartPosition.CenterScreen;
              
                comboBox3.Font = new Font("Arial", 11);
                comboBox1.Font = new Font("Arial", 11);
                comboBox2.Font = new Font("Arial", 11);
                comboBox5.Font = new Font("Arial", 11);

                comboBox4.Font = new Font("Arial", 11);
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox5.Enabled = false;
                comboBox4.Enabled = false;
            
                comboBox5.Enabled = false;

                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                dataGridView3.ReadOnly = true;
                //updatepricerinfo();

                Update();
                updatenumberinfo();
               

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                dataGridView2.Font = new Font("Arial", 9);
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView3.Font = new Font("Arial", 9);
                textBox1.Font = new Font("Arial", 11);
                textBox2.Font = new Font("Arial", 11);
                comboBox4.Font = new Font("Arial", 11);
                comboBox1.Text = "Товар не выбран";
                comboBox2.Text = "Партия товара не выбрана";
              
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label7.Font = new Font("Arial", 11);
                
                label9.Font = new Font("Arial", 11);
                label10.Font = new Font("Arial", 11);
                if (id != -1)
                {

                
                    updateProduct_cardinfoupdate(this.id_Product_card);
                    updatebreachinfoupdate(this.id_batch_number);
                    textBox1.Text = this.quantity.ToString();
                    comboBox4.Text = this.price.ToString();


                }
            }
            catch { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            { if (comboBox1.SelectedValue != null)
                {
                    int id_c = (int)comboBox1.SelectedValue;
                    product_card_in fp = new product_card_in(con, id_c, this.div);
                    fp.Show();
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                int id = 0;
                string name = "";
                string code = "";
                int aa;
       


            
                    Product_card fp = new Product_card(con, id, name, code, "", st, this.div);
                fp.ShowDialog();
                if (fp.code != "")
                    {
                        //dataGridView3.ContextMenuStrip = null;
                        updateProduct_cardinfo(fp.id);


                  
                    
                    if (st != null)
                    {
                        //updatestorehouseinfo(fp.id_c);

                        String sql19 = "Select DISTINCT  batch_number.number, prod_store.count_id_batch,batch_number.id from prod_store, batch_number where prod_store.id_batch_number= batch_number.id and  prod_store.id_product_card= ";
                        sql19 += comboBox1.SelectedValue.ToString();
                        sql19 += " and prod_store.count>0 and  prod_store.id_store = ";
                        sql19 += st.ToString();
                        sql19 += " ORDER BY prod_store.count_id_batch ASC;";
                        NpgsqlDataAdapter da19 = new NpgsqlDataAdapter(sql19, con);
                        da19.SelectCommand.Parameters.AddWithValue("id", id);
                        ds19.Reset();
                        da19.Fill(ds19);
                        dt19 = ds19.Tables[0];
                        dataGridView3.DataSource = dt19;
                        //dataGridView3.Columns[0].Visible = false;
                        dataGridView3.Columns[0].HeaderText = "Партия";
                        dataGridView3.Columns[1].HeaderText = "Количество товара на складе данной партии";
                        dataGridView3.Columns[2].Visible = false;
                    }
                    else
                    {


                    }
          


            comboBox2.Text = "Партия не выбрана";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    //textBox2.Text = comboBox1.SelectedValue.ToString();
                    //textBox3.Text =this.id_Firm.ToString();

                    
                    dataGridView3.ContextMenuStrip = contextMenuStrip1;
                }
                else
                {
                    comboBox1.Text = "Товар не выбран";

                }
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {


                int id_c = 0;
                string number = "";


                int id_pro_card = (int)comboBox1.SelectedValue;

                
                int id_batch = (int)dataGridView3.CurrentRow.Cells[2].Value;
                if (id_batch != null)
                {


                   
                    updatebatch_numberinfo(id_batch);
                  
                    updatepricerinfo(id_batch);
                    updatequantityinfo();
                }
                else
                {
                    comboBox2.Text = "Партия не выбрана";

                }
            }
            catch { }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue != null)
                {
                    int id_pro_card = (int)comboBox1.SelectedValue;
                    if (comboBox2.SelectedValue != null)
                    {
                        int id_c = (int)comboBox2.SelectedValue;

                        batch_number fp = new batch_number(con, id_c, "", id_pro_card, -1, -1, this.div);
                        fp.ShowDialog();
                    }

                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                //storehouse fp = new storehouse(con, id_s, name);
                //fp.ShowDialog();
                //if (fp.name != "")
                int id_store = (int)dataGridView3.CurrentRow.Cells[2].Value;
                if (id_store != null)
                {
                    //updatestorehouseinfo(fp.id_c);
                 
                    String sql19 = "Select DISTINCT  batch_number.number, prod_store.count_id_batch,batch_number.id from prod_store, batch_number where prod_store.id_batch_number= batch_number.id and  prod_store.id_product_card= ";
                    sql19 += comboBox1.SelectedValue.ToString();
                    sql19 += " and  prod_store.id_store = ";
                    sql19 += st.ToString();
                    sql19 += " ORDER BY prod_store.count_id_batch ASC;";
                    NpgsqlDataAdapter da19 = new NpgsqlDataAdapter(sql19, con);
                    da19.SelectCommand.Parameters.AddWithValue("id", id);
                    ds19.Reset();
                    da19.Fill(ds19);
                    dt19 = ds19.Tables[0];
                    dataGridView3.DataSource = dt19;
                    //dataGridView3.Columns[0].Visible = false;
                    dataGridView3.Columns[0].HeaderText = "Партия";
                    dataGridView3.Columns[1].HeaderText = "Количество товара на складе данной партии";
                    dataGridView3.Columns[2].Visible = false;
                }
                else
                {
                    

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
                    int nds = 0;
                    if (comboBox1.SelectedValue != null)
                    {



                        String sql20 = @"SELECT id, percent FROM NDS 
                                                  WHERE id = (SELECT id_nds FROM Product_card where id = ";
                        sql20 += comboBox1.SelectedValue;
                        sql20 += ")";

                        NpgsqlDataAdapter da20 = new NpgsqlDataAdapter(sql20, con);
                        ds20.Reset();
                        da20.Fill(ds20);
                        dt20 = ds20.Tables[0];
                        if (dt20.Rows.Count > 0)
                        {
                            nds = Convert.ToInt32(dt20.Rows[0]["percent"]);

                        }

                    }
                    else
                    {
                        nds = 0;
                    }
                    if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(comboBox5.Text))
                    {
                        DialogResult result1 = MessageBox.Show("Столько товара нет в партии", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.BackColor = Color.DarkSalmon;
                    }
                    else
                    {
                        string sql = "Insert into invoices_in_info ( invoices_in,id_Product_card,id_batch_number,quantity,price,price_nds,count) values ( :invoices_in,:id_Product_card,:id_batch_number,:quantity,:price,:price_nds,0)";
                        NpgsqlCommand command = new NpgsqlCommand(sql, con);


                        command.Parameters.AddWithValue("invoices_in", this.id_invoices);
                        command.Parameters.AddWithValue("id_Product_card", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("id_batch_number", comboBox2.SelectedValue);
                        command.Parameters.AddWithValue("quantity", Convert.ToDouble(textBox1.Text));
                        command.Parameters.AddWithValue("price", Convert.ToDouble(textBox2.Text));
                        command.Parameters.AddWithValue("price_nds", Convert.ToDouble(textBox2.Text) + (((Convert.ToDouble(textBox2.Text)) * nds) / 100));
                        DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            command.ExecuteNonQuery();

                            Update();
                            updatequantityinfo();
                            //updatepricerinfo();
                            if (st != null)
                            {
                                //updatestorehouseinfo(fp.id_c);

                                String sql19 = "Select DISTINCT  batch_number.number, prod_store.count_id_batch,batch_number.id from prod_store, batch_number where prod_store.id_batch_number= batch_number.id and  prod_store.id_product_card= ";
                                sql19 += comboBox1.SelectedValue.ToString();
                                sql19 += " and  prod_store.id_store = ";
                                sql19 += st.ToString();
                                sql19 += " ORDER BY prod_store.count_id_batch ASC;";
                                NpgsqlDataAdapter da19 = new NpgsqlDataAdapter(sql19, con);
                                da19.SelectCommand.Parameters.AddWithValue("id", id);
                                ds19.Reset();
                                da19.Fill(ds19);
                                dt19 = ds19.Tables[0];
                                dataGridView3.DataSource = dt19;
                                //dataGridView3.Columns[0].Visible = false;
                                dataGridView3.Columns[0].HeaderText = "Партия";
                                dataGridView3.Columns[1].HeaderText = "Количество товара на складе данной партии";
                                dataGridView3.Columns[2].Visible = false;
                            }
                            else
                            {


                            }

                        }

                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }

            else
            {
                try
                {

                    int nds = 0;
                    if (comboBox1.SelectedValue != null)
                    {



                        String sql20 = @"SELECT id, percent FROM NDS 
                                                  WHERE id = (SELECT id_nds FROM Product_card where id = ";
                        sql20 += comboBox1.SelectedValue;
                        sql20 += ")";

                        NpgsqlDataAdapter da20 = new NpgsqlDataAdapter(sql20, con);
                        ds20.Reset();
                        da20.Fill(ds20);
                        dt20 = ds20.Tables[0];
                        if (dt20.Rows.Count > 0)
                        {
                            nds = Convert.ToInt32(dt20.Rows[0]["percent"]);

                        }

                    }
                    else
                    {
                        nds = 0;
                    }
                    int count = 0;



                    String sql18 = @"SELECT count FROM invoices_in_info 
                                                  WHERE id = " + this.id;


                    NpgsqlDataAdapter da18 = new NpgsqlDataAdapter(sql18, con);
                    ds18.Reset();
                    da18.Fill(ds18);
                    dt18 = ds18.Tables[0];
                    if (dt18.Rows.Count > 0)
                    {
                        count = Convert.ToInt32(dt18.Rows[0]["count"]);
                    }

                    if (count != 0)
                    {

                        DialogResult result2 = MessageBox.Show("Данные о товаре не могут быть изменены, так как товар уже находится на стадии размещения на складе?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    }
                    else
                    {

                        string sql = "update invoices_in_info  set invoices_in=:invoices_in, id_Product_card=:id_Product_card, id_batch_number=:id_batch_number," +
                       "quantity=:quantity, price=:price, price_nds=:price_nds " +
                       " where id=:id and invoices_in=:invoices_in";
                        NpgsqlCommand command = new NpgsqlCommand(sql, con);
                        command.Parameters.AddWithValue("invoices_in", this.id_invoices);
                        command.Parameters.AddWithValue("id_Product_card", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("id_batch_number", comboBox2.SelectedValue);
                        command.Parameters.AddWithValue("quantity", Convert.ToDouble(textBox1.Text));
                        command.Parameters.AddWithValue("price", Convert.ToDouble(textBox2.Text));
                        command.Parameters.AddWithValue("id", this.id);
                        command.Parameters.AddWithValue("price_nds", Convert.ToDouble(textBox2.Text) + (((Convert.ToDouble(textBox2.Text)) * nds) / 100));
                        DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            command.ExecuteNonQuery();

                            Update();
                            updatequantityinfo();
                            //updatepricerinfo();
                            if (st != null)
                            {
                                //updatestorehouseinfo(fp.id_c);

                                String sql19 = "Select DISTINCT  batch_number.number, prod_store.count_id_batch,batch_number.id from prod_store, batch_number where prod_store.id_batch_number= batch_number.id and  prod_store.id_product_card= ";
                                sql19 += comboBox1.SelectedValue.ToString();
                                sql19 += " and  prod_store.id_store = ";
                                sql19 += st.ToString();
                                sql19 += " ORDER BY prod_store.count_id_batch ASC;";
                                NpgsqlDataAdapter da19 = new NpgsqlDataAdapter(sql19, con);
                                da19.SelectCommand.Parameters.AddWithValue("id", id);
                                ds19.Reset();
                                da19.Fill(ds19);
                                dt19 = ds19.Tables[0];
                                dataGridView3.DataSource = dt19;
                                //dataGridView3.Columns[0].Visible = false;
                                dataGridView3.Columns[0].HeaderText = "Партия";
                                dataGridView3.Columns[1].HeaderText = "Количество товара на складе данной партии";
                                dataGridView3.Columns[2].Visible = false;
                            }
                            else
                            {


                            }
                        }
                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
        }  
                
        

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int id;
                if (dataGridView1.CurrentRow != null)
                    if (dataGridView1.CurrentRow.Index != 0)
                    {
                        id = (int)dataGridView1.CurrentRow.Cells[2].Value;
                    }
                    else id = 1;
                else id = dataGridView1.RowCount;
                
            }
            catch { }
        }

        private void посмотретьИнформациюОПартииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue != null)
                {
                    int id_pro_card = (int)comboBox1.SelectedValue;
                    if (dataGridView3.CurrentRow.Cells[2].Value != null)
                    {
                        int id_batch = (int)dataGridView3.CurrentRow.Cells[2].Value;
                      

                        batch_number fp = new batch_number(con, id_batch, "", -1, -1,-1, this.div);
                        fp.ShowDialog();
                    }
                    
                }
            }
            catch { }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView3.CurrentRow.Cells[5].Value != null)
                {
                    int id_pro = (int)dataGridView3.CurrentRow.Cells[5].Value;


                    product_card_in fp = new product_card_in(con, id_pro, this.div);
                    fp.ShowDialog();
                }


            }
            catch { }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
