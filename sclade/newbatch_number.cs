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
    public partial class newbatch_number : Form
    {
        public NpgsqlConnection con;
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
        public int id_n;
        public string id_Firm;
        Regex regex1 = new Regex(@"\d$");
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        Regex regex2 = new Regex(@"\w$");
        public string id_pro;
        public string number;
        public DateTime release;
        public DateTime last_expiration;
        public string warranty;
        public int col_pro;
        public string num_time;
        public int id_pro_card;
        public double price;
        public string litter;
        public int stor;
        public int div;
        public newbatch_number(NpgsqlConnection con, int id_n, string id_pro, string number, DateTime release, DateTime last_expiration, string warranty, int col_pro, string litter, int id_pro_card, double price,string id_Firm, int stor,int div)
        {
            this.div = div;
            this.litter = litter;
            InitializeComponent();
            this.id_n = id_n;
            this.id_pro_card = id_pro_card;
            this.number = number;
            this.release = release;
            this.last_expiration = last_expiration;
            this.warranty = warranty;
            this.col_pro = col_pro;
            this.id_pro = id_pro;
            this.con = con;
            this.price = price;
            this.id_Firm = id_Firm;
            this.stor = stor;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void updateProduct_cardinfo(int id_pro)
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
                //}
            }
            catch { }



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

                    String sql1 = @"SELECT id, code FROM Product_card 
                                                  WHERE code = '";
                    sql1 += code;
                    sql1 += "'";


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
                //}
            }
            catch { }



        }
        public void updateFirminfo(int id_f)
        {
            try
            {
                String sql1 = "Select * from Firm where id=";
                sql1 += id_f.ToString();
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql1, con);
                ds4.Reset();
                da4.Fill(ds4);
                dt4 = ds4.Tables[0];
                comboBox3.DataSource = dt4;
                comboBox3.DisplayMember = "name_f";
                comboBox3.ValueMember = "id";
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
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql9, con);
                ds4.Reset();
                da4.Fill(ds4);
                dt4 = ds4.Tables[0];
                comboBox3.DataSource = dt4;
                comboBox3.DisplayMember = "name_f";
                comboBox3.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        private void newbatch_number_Load(object sender, EventArgs e)
        {
            try
            {
                label2.Visible = false;
                textBox1.Visible = false;
                richTextBox1.ReadOnly = true;
                comboBox4.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
          
                comboBox4.Font = new Font("Arial", 11);
                label14.Font = new Font("Arial", 11);
                comboBox1.Text = "Товар не выбран";
                comboBox2.Text = "Товар не выбран";
                comboBox3.Text = "Поставщик не выбран";
                comboBox4.Text = "Единица измерения не выбрана";
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                label6.Font = new Font("Arial", 11);
                label7.Font = new Font("Arial", 11);
                label8.Font = new Font("Arial", 11);
                label9.Font = new Font("Arial", 11);
                label10.Font = new Font("Arial", 11);
                label12.Font = new Font("Arial", 11);
                label11.Font = new Font("Arial", 11);
                textBox1.Font = new Font("Arial", 11);
                textBox2.Font = new Font("Arial", 11);
                label6.Visible = false;
                textBox3.Visible = false;
                comboBox1.Font = new Font("Arial", 11);
                comboBox2.Font = new Font("Arial", 11);
                comboBox3.Font = new Font("Arial", 11);
                textBox4.Font = new Font("Arial", 11);
                textBox5.Font = new Font("Arial", 11);
                radioButton4.Font = new Font("Arial", 11);
                radioButton3.Font = new Font("Arial", 11);
                radioButton2.Font = new Font("Arial", 11);
                radioButton1.Font = new Font("Arial", 11);
                radioButton5.Font = new Font("Arial", 11);

                this.num_time = radioButton1.Text;


                dateTimePicker1.Format = DateTimePickerFormat.Time;
                dateTimePicker2.Format = DateTimePickerFormat.Time;
                textBox3.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.Text = this.col_pro.ToString();

                updatenameinfo();

                if (id_n != -1)
                {
                    updateProduct_cardinfo(this.id_pro_card);
                    updateFirminfoupdate(this.id_Firm);
                    updateunit_of_measurementinfoupdate(this.litter);
                    textBox1.Text = this.number;
                    textBox2.Text = this.warranty;

                    textBox5.Text = this.price.ToString();
                    updatenameinfo();
                    textBox3.Text = this.col_pro.ToString();
                    dateTimePicker1.Value = this.release;
                    dateTimePicker2.Value = this.last_expiration;
                    int T = 0;
                    string day = "";
                    string ed = "";
                    string[] words = warranty.Split(new char[] { ' ' });
                   int K = 0;
                    foreach (string i in words)
                    {
                        if (regex1.IsMatch(i) == true) day += i;
                        else if ((regex2.IsMatch(i) == true) && (K != 2))
                        {
                            ed += i;
                            K++;
                        }
                        else 
                        {
                            ed += " ";
                            ed += i;
                            K++;
                        }
                       


                    }
                    textBox2.Text = ed;
                    textBox4.Text = day;
                }

                if (id_pro_card != -1)
                {

                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id_c = (int)comboBox1.SelectedValue;
                product_card_in fp = new product_card_in(con, id_c, this.div);
                fp.Show();

            }
            catch { }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox2.Text != " без гарантии") && (regex1.IsMatch(textBox4.Text) == false))
                {
                    DialogResult result = MessageBox.Show("Некорректно введено количество дней гарантии. Можно использовать только цифры", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox4.BackColor = Color.DarkSalmon;
                }
                else
                { textBox1.BackColor = Color.Honeydew; }
                if (textBox4.Text == "0")
                {
                    textBox4.Text = "";
                    textBox2.Text = radioButton5.Text;
                }
                string id_pro = comboBox1.Text;
                string number = textBox1.Text;
                DateTime release = dateTimePicker1.Value;
                DateTime last_expiration = dateTimePicker2.Value;
                string warranty = textBox4.Text + " " + textBox2.Text;
                string col_pro = textBox3.Text;
                string name = comboBox2.Text;
                string price = textBox5.Text;
                string firm = comboBox3.Text;
                string litter = comboBox4.Text;
                richTextBox1.Clear();
                richTextBox1.AppendText("            Партия \n");
                richTextBox1.AppendText("\n");
                richTextBox1.AppendText("Код товара: " + id_pro + "\n\n");
                richTextBox1.AppendText("Название товара: " + name + "\n\n");
                //richTextBox1.AppendText("Номер партии: " + number + "\n\n");
                richTextBox1.AppendText("Поставщик: " + firm + "\n\n");
                richTextBox1.AppendText("Дата и время выпуска: " + release + "\n\n");
                richTextBox1.AppendText("Дата и время конца срока годности: " + last_expiration + "\n\n");
                richTextBox1.AppendText("Количество товара в партии: " + col_pro + "\n\n");
                richTextBox1.AppendText("Единица измерения: " + litter + "\n\n");
                richTextBox1.AppendText("Цена за единицу товара: " + price + "\n\n");

                richTextBox1.AppendText("           Гаранития \n");
                richTextBox1.AppendText("Период: " + warranty + "\n");
            }
            catch { }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            label9.Text = String.Format("Вы выбрали: {0}", dateTimePicker1.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label10.Text = String.Format("Вы выбрали: {0}", dateTimePicker2.Value);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = radioButton2.Text;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = radioButton3.Text;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = radioButton4.Text;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Text = radioButton5.Text;
            textBox4.Text = "";
        }
        public void updatenameinfo()
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


                string sql2 = @"SELECT name FROM Product_card WHERE id = @id_c";


                using (NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql2, con))
                {
                    da3.SelectCommand.Parameters.AddWithValue("@id_c", id_c);

                    ds3.Reset();
                    da3.Fill(ds3);

                    dt3 = ds3.Tables[0];


                    comboBox2.DataSource = dt3;
                    comboBox2.DisplayMember = "name";
                    comboBox2.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void updatenameinfoupdate(string code)
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


                string sql2 = @"SELECT name FROM Product_card WHERE id = @id_c";


                using (NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql2, con))
                {
                    da3.SelectCommand.Parameters.AddWithValue("@id_c", id_c);

                    ds3.Reset();
                    da3.Fill(ds3);

                    dt3 = ds3.Tables[0];


                    comboBox2.DataSource = dt3;
                    comboBox2.DisplayMember = "name";
                    comboBox2.ValueMember = "id";
                }
            }
            catch (Exception ex)
            {

            }
        
    





            }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int code_;
                DataTable dt31 = new DataTable();
                DataSet ds31 = new DataSet();
                String sql31 = "Select * from batch_number ORDER BY id DESC LIMIT 1 ;";
                NpgsqlDataAdapter da31 = new NpgsqlDataAdapter(sql31, con);
                ds31.Reset();
                da31.Fill(ds31);
                dt31 = ds31.Tables[0];
                if (dt31.Rows.Count > 0)
                {
                    code_ = Convert.ToInt32(dt31.Rows[0]["number"].ToString());

                }
                else
                {
                    code_ = 100;
                }
                if ((textBox2.Text == " без гарантии") || regex1.IsMatch(textBox4.Text) != false)
            {
                
                    if (textBox4.Text == "0")
                    {
                        textBox4.Text = "";
                        textBox2.Text = radioButton5.Text;
                    }

                        if (this.id_n == -1)
                {
                        try
                        {
                            string time = textBox4.Text + " " + textBox2.Text;
                        string sql = "Insert into batch_number (id_pro_card,id_ed, number, release,last_expiration, warranty,col_pro,price,id_Firm ) values (:id_pro_card,:id_ed, :number, :release,:last_expiration, :warranty,:col_pro,:price, :id_Firm)";
                        NpgsqlCommand command = new NpgsqlCommand(sql, con);
                        command.Parameters.AddWithValue("id_pro_card", comboBox1.SelectedValue);
                            command.Parameters.AddWithValue("id_ed", comboBox4.SelectedValue);
                            command.Parameters.AddWithValue("number", (code_ + 1).ToString());
                        command.Parameters.AddWithValue("release", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("last_expiration", dateTimePicker2.Value);
                        command.Parameters.AddWithValue("warranty", time);
                        command.Parameters.AddWithValue("col_pro", Convert.ToDouble(textBox3.Text));
                            command.Parameters.AddWithValue("price", Convert.ToDouble(textBox5.Text));
                            command.Parameters.AddWithValue("id_Firm", comboBox3.SelectedValue);
                            DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            command.ExecuteNonQuery();
                            Close();
                        }
                        }
                        catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                    }
                else
                {
                    try
                    {
                        string sql = "update batch_number set id_pro_card=:id_pro_card, id_ed=:id_ed, number=:number, release=:release, last_expiration=:last_expiration, warranty=:warranty, col_pro=:col_pro, price=:price,id_Firm=:id_Firm where id=:id";
                        string time = textBox4.Text + " " + textBox2.Text;
                        
                        NpgsqlCommand command = new NpgsqlCommand(sql, con);
                        command.Parameters.AddWithValue("id_pro_card", comboBox1.SelectedValue);
                            command.Parameters.AddWithValue("id_ed", comboBox4.SelectedValue);
                            command.Parameters.AddWithValue("number", textBox1.Text);
                        command.Parameters.AddWithValue("release", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("last_expiration", dateTimePicker2.Value);
                        command.Parameters.AddWithValue("warranty", time);
                        command.Parameters.AddWithValue("col_pro", Convert.ToDouble(textBox3.Text));
                            command.Parameters.AddWithValue("price", Convert.ToDouble(textBox5.Text));
                            command.Parameters.AddWithValue("id_Firm", comboBox3.SelectedValue);
                            command.Parameters.AddWithValue("id", this.id_n);
                        DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {

                            command.ExecuteNonQuery();
                            Close();
                        }



                    }
                        catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
            }
            else
            {
                DialogResult result = MessageBox.Show("Некорректно введено количество дней гарантии. Можно использовать только цифры", "Выполнение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox4.BackColor = Color.DarkSalmon;
                }
            }
            catch { }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatenameinfo();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
              
            
                int id = 0;
                string name = "";
                string code = "";
                int aa;
                Product_card fp = new Product_card(con, id,name,code,"",-1, this.div);
                fp.ShowDialog();
                if (fp.code != "")
                {
                    updateProduct_cardinfo(fp.id);
                  
                   
                    aa = fp.id;
                    updatenameinfo();
                }
                else
                {
                    comboBox1.Text = "Товар не выбран";
                    comboBox2.Text = "Товар не выбран";
                }
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {


                int id_f = 0;
                string name = "";
                if (this.stor != -1)
                {
                    firm_firm fp = new firm_firm(con, id_f, name, this.stor, div);
                    fp.ShowDialog();
                    if (fp.name != "")
                    {
                        updateFirminfo(fp.id);

                    }
                    else
                    {
                        comboBox3.Text = "Поставщик не выбран";

                    }
                }
                else
                {
                   firm fp = new firm(con, id_f, name);
                    fp.ShowDialog();
                    if (fp.name != "")
                    {
                        updateFirminfo(fp.id);

                    }
                    else
                    {
                        comboBox3.Text = "Поставщик не выбран";

                    }
                }
           
            }
            catch { }
        }
        public void updateunit_of_measurementinfo(int id_t)
        {
            try
            {
                String sql5 = "Select * from unit_of_measurement  where id=";
                sql5 += id_t.ToString();
                NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con);
                ds5.Reset();
                da5.Fill(ds5);
                dt5 = ds5.Tables[0];
                comboBox4.DataSource = dt5;
                comboBox4.DisplayMember = "litter";
                comboBox4.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateunit_of_measurementinfoupdate(string litter)
        {
            try
            {
                String sql5= "Select * from unit_of_measurement  where litter='";
                sql5 += litter;
                sql5 += "'";
                NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con);
                ds5.Reset();
                da5.Fill(ds5);
                dt5 = ds5.Tables[0];
                comboBox4.DataSource = dt5;
                comboBox4.DisplayMember = "litter";
                comboBox4.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {


                int id = 0;
                string name = "";


                unit_of_measurement_in fp = new unit_of_measurement_in(con, id, name);
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateunit_of_measurementinfo(fp.id);


                    ;

                }
                else
                {
                    comboBox4.Text = "Единица измерения не выбрана";

                }
            }
            catch { }
        }
    }
}
