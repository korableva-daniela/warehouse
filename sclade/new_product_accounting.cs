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
    public partial class new_product_accounting : Form
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
        DataTable dt14 = new DataTable();
        DataSet ds14 = new DataSet();
        DataTable dt12 = new DataTable();
        DataSet ds12 = new DataSet();
        DataTable dt15 = new DataTable();
        DataSet ds15 = new DataSet();
        Regex regex1 = new Regex(@"\d$");
        Regex regex2 = new Regex(@"\$");
        public NpgsqlConnection con;

        public int id_prod_storehouse_info;
        public int id_prod_storehouse;
        public int quantity;

        public string id_storehouse;
        public string pro;
        public string br;
        public string stor;
        int id_pro;
        int id_br;

        public string name_pro;
        public string name_place;
        int st;
        string num;
        public int id_stor;
        public int id_em;
        public new_product_accounting(NpgsqlConnection con, int id_prod_storehouse_info, int id_prod_storehouse, string stor, int id_em, int quantity, string pro, string br, string name_pro, string name_place, int id_stor)
        {
            this.id_stor = id_stor;
            this.id_prod_storehouse_info = id_prod_storehouse_info;
            this.id_prod_storehouse = id_prod_storehouse;
            this.quantity = quantity;
            InitializeComponent();
            this.con = con;
            this.stor = stor;
            this.pro = pro;
            this.br = br;
            this.id_em = id_em;
            this.name_pro = name_pro;
            this.name_place = name_place;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void new_product_accounting_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Arial", 11);
            label8.Font = new Font("Arial", 11);
            label9.Font = new Font("Arial", 11);
            label10.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
            textBox2.Font = new Font("Arial", 18);
            textBox3.Font = new Font("Arial", 11);
            textBox6.Font = new Font("Arial", 11);
            textBox7.Font = new Font("Arial", 11);
            textBox8.Font = new Font("Arial", 11);
            textBox9.Font = new Font("Arial", 11);

            if (this.pro != "" && this.br != "")
            {
                label11.Font = new Font("Arial", 11);
                label11.Text = "Название товара: " + this.name_pro + "\n Код товара: " + this.pro + "\n Партия товара: " + this.br;
                label12.Font = new Font("Arial", 11);
                label12.Text = "Склад: " + this.stor;
                updateProduct_cardinfoupdate(this.pro);
                updatebreachinfoupdate(this.br);

            }
            if (this.name_place != "")
            {
                textBox2.Text = this.name_place;
            }
            textBox3.Text = this.quantity.ToString();
            textBox2.Enabled = false;
            textBox3.Enabled = false;





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
                if (dt4.Rows.Count > 0)
                {
                    id_pro = Convert.ToInt32(dt4.Rows[0]["id"]);


                }
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


                    if (dt5.Rows.Count > 0)
                    {
                        id_br = Convert.ToInt32(dt5.Rows[0]["id"]);


                    }
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
                    if (dt4.Rows.Count > 0)
                    {
                        id_pro = Convert.ToInt32(dt4.Rows[0]["id"]);


                    }
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
                    if (id_pro != null)
                    {

                        String sql5 = @"SELECT id, number FROM batch_number 
                                                  WHERE id_pro_card = " + id_pro + " and number = '";
                        sql5 += number;
                        sql5 += "'";


                        NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con);
                        ds5.Reset();
                        da5.Fill(ds5);
                        dt5 = ds5.Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            id_br = Convert.ToInt32(dt5.Rows[0]["id"]);


                        }
                        this.StartPosition = FormStartPosition.CenterScreen;
                    }
                }
                catch { }
                //}
            }
            catch { }



        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToDouble(textBox1.Text) > Convert.ToDouble(textBox3.Text))
                {
                    DialogResult result1 = MessageBox.Show("Столько товара нет на полке", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.BackColor = Color.DarkSalmon;
                }
                else
                {

                    int id_prod_stor;
                    
                    String sql9 = "Select * from prod_storehouse where id_store =";
                    sql9 += this.id_stor.ToString();
                    sql9 += " and num_place = '" + textBox9.Text + "/" + textBox8.Text + "/" + textBox7.Text + "/" + textBox6.Text + "'";

                    NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                    ds9.Reset();
                    da9.Fill(ds9);
                    dt9 = ds9.Tables[0];
                    if (dt9.Rows.Count > 0)
                    {
                        id_prod_stor = Convert.ToInt32(dt9.Rows[0]["id"]);

                        //string sql10 = "update prod_storehouse set count=:count where id=:id;";
                        //NpgsqlCommand command2 = new NpgsqlCommand(sql10, con);
                        //command2.Parameters.AddWithValue("count", Convert.ToDouble(count) + Convert.ToDouble(textBox1.Text));

                        //command2.Parameters.AddWithValue("id", id_prod_storehouse);

                        DialogResult result2 = MessageBox.Show("Вы уверены, что хотите переместить товар", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result2 == DialogResult.Yes)
                        {

                            //command2.ExecuteNonQuery();
                            string sql5 = "update prod_storehouse_info set count=:count where id=:id";
                            NpgsqlCommand command5 = new NpgsqlCommand(sql5, con);
                            command5.Parameters.AddWithValue("count", Convert.ToDouble(textBox3.Text) - Convert.ToDouble(textBox1.Text));
                            command5.Parameters.AddWithValue("id", this.id_prod_storehouse_info);
                            command5.ExecuteNonQuery();


                            quantity = quantity + Convert.ToInt32(textBox1.Text);
                            string sql3 = "Insert into prod_storehouse_info (id_prod_storehouse, id_batch_number,id_product_card,date_add,count,id_Employee) values (:id_prod_storehouse,:id_batch_number,:id_product_card,:date_add,:count,:id_Employee)";
                            NpgsqlCommand command3 = new NpgsqlCommand(sql3, con);


                            command3.Parameters.AddWithValue("id_prod_storehouse", id_prod_stor);
                            command3.Parameters.AddWithValue("id_Product_card", id_pro);
                            command3.Parameters.AddWithValue("id_batch_number", id_br);
                            command3.Parameters.AddWithValue("count", Convert.ToDouble(textBox1.Text));
                            command3.Parameters.AddWithValue("date_add", DateTime.Today);

                            command3.Parameters.AddWithValue("id_Employee", this.id_em);
                            command3.ExecuteNonQuery();

                        }

                    }
                    else
                    {
                        DialogResult result10 = MessageBox.Show("Такой полки нет. Добавить полку?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result10 == DialogResult.Yes)
                        {
                            string sql10 = "Insert into prod_storehouse ( id_store,num_place) values  ( :id_store,:num_place)";
                            NpgsqlCommand command2 = new NpgsqlCommand(sql10, con);
                            //command2.Parameters.AddWithValue("count", Convert.ToDouble(textBox1.Text));
                            string str_place = textBox9.Text + "/" + textBox8.Text + "/" + textBox7.Text + "/" + textBox6.Text;
                            command2.Parameters.AddWithValue("num_place", str_place);
                            command2.Parameters.AddWithValue("id_store", this.id_stor);

                            DialogResult result2 = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result2 == DialogResult.Yes)
                            {

                                command2.ExecuteNonQuery();
                                String sql11 = "Select * from prod_storehouse where id_store = ";
                                sql11 += this.id_stor.ToString();
                                sql11 += " and num_place = '" + textBox9.Text + "/" + textBox8.Text + "/" + textBox7.Text + "/" + textBox6.Text + "'";
                                NpgsqlDataAdapter da10 = new NpgsqlDataAdapter(sql11, con);
                                ds10.Reset();
                                da10.Fill(ds10);
                                dt10 = ds10.Tables[0];
                                if (dt10.Rows.Count > 0)
                                {
                                    id_prod_stor = Convert.ToInt32(dt10.Rows[0]["id"]);
                                    string sql5 = "update prod_storehouse_info set count=:count where id=:id";
                                    NpgsqlCommand command5 = new NpgsqlCommand(sql5, con);
                                    command5.Parameters.AddWithValue("count", Convert.ToDouble(textBox3.Text) - Convert.ToDouble(textBox1.Text));
                                    command5.Parameters.AddWithValue("id", id_prod_storehouse_info);
                                    command5.ExecuteNonQuery();
                                    string sql3 = "Insert into prod_storehouse_info ( id_prod_storehouse,id_batch_number,id_product_card,date_add,count,id_Employee) values (:id_prod_storehouse,:id_batch_number,:id_product_card,:date_add,:count,:id_Employee)";
                                    NpgsqlCommand command3 = new NpgsqlCommand(sql3, con);


                                    command3.Parameters.AddWithValue("id_prod_storehouse", id_prod_stor);
                                    command3.Parameters.AddWithValue("id_Product_card", id_pro);
                                    command3.Parameters.AddWithValue("id_batch_number", id_br);
                                    command3.Parameters.AddWithValue("count", Convert.ToDouble(textBox1.Text));
                                    command3.Parameters.AddWithValue("date_add", DateTime.Today);

                                    command3.Parameters.AddWithValue("id_Employee", this.id_em);
                                    command3.ExecuteNonQuery();

                                }

                            }
                           
                        }

                    }
                }

                String sql15 = "Select * from prod_storehouse_info where id = " + this.id_prod_storehouse_info;
                NpgsqlDataAdapter da15 = new NpgsqlDataAdapter(sql15, con);
                ds15.Reset();
                da15.Fill(ds15);
                dt15 = ds15.Tables[0];
                if (dt15.Rows.Count > 0)
                {

                    textBox3.Text = Convert.ToInt32(dt15.Rows[0]["count"]).ToString();

                }
            }
            catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }



        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
