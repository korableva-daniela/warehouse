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
    public partial class newProduct_card : Form
    {
        public int id ;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана

        public string id_type ;
        public string name ;
        string description;
        public string name_firm;
        public string code ;
        //public string id_f;
        public string id_ed;
        public string id_coun;
        public string numgtd;
        public string numrnpt;
        public int id_nds;
        public string code_firm_pro;
        public double price_firm_pro;
        public string numexcise;
        public string numegis;
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
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        public int col;
        public newProduct_card(NpgsqlConnection con, int id,string name, string id_type, string name_firm,string code, int col,  string id_ed, string id_coun, string numgtd, string numrnpt, int id_nds,
            string code_firm_pro, double price_firm_pro,string numexcise, string numegis, string description )
        {
            this.id = id;
            this.id_type= id_type;
            this.name = name;
            this.description = description;
            this.name_firm= name_firm;
            this.code= code;
            //this.id_f= id_f;
            this.id_ed= id_ed;
            this.id_coun= id_coun;
            this.numgtd= numgtd;
            this.numrnpt= numrnpt;
            this.id_nds= id_nds;
            this.code_firm_pro = code_firm_pro;
            this.price_firm_pro= price_firm_pro;
            this.numexcise= numexcise;
            this.numegis= numegis;
            this.col = col;
        this.con=con;
        InitializeComponent();
            this.MouseDown += new MouseEventHandler(MainForm_MouseDown);
            this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
        }
        public void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Начинаем перетаскивание, если нажали левую кнопку мыши
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; // Получаем текущую позицию курсора
                dragFormPoint = this.Location; // Получаем текущее местоположение формы
            }
        }

        public void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Если перетаскиваем форму, обновляем её позицию
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        public void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Завершаем перетаскивание
            dragging = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void newProduct_card_Load(object sender, EventArgs e)

        {
            try
            {
                comboBox5.DropDownStyle = ComboBoxStyle.DropDownList; // Запретить ввод текста
                comboBox5.Enabled = true; // Сделать ComboBox доступным для выбора
                richTextBox1.ReadOnly = true;
                textBox1.Visible = false;
                label1.Visible = false;
                comboBox1.Enabled = false;
           
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
            
                comboBox3.Text = "Единица измерения не выбрана";
                comboBox4.Text = "Код товара не выбран";
                //comboBox2.Text = "Поставщик не выбран";
                comboBox1.Text = "Тип товара не выбран";
                //textBox5.ReadOnly = true;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
                textBox5.Enabled = false;
                label9.Font = new Font(label9.Font.Name, 16);
    
            comboBox1.Font = new Font("Arial", 11);
            label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            label3.Font = new Font("Arial", 11);
            //label4.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
            label8.Font = new Font("Arial", 11);
                label17.Font = new Font("Arial", 11);
                label10.Font = new Font("Arial", 11);
                label11.Font = new Font("Arial", 11);
                label12.Font = new Font("Arial", 11);
                label13.Font = new Font("Arial", 11);
                label14.Font = new Font("Arial", 11);
                label15.Font = new Font("Arial", 11);
                label16.Font = new Font("Arial", 11);
        
                //comboBox2.Font = new Font("Arial", 11);
            comboBox3.Font = new Font("Arial", 11);
            comboBox4.Font = new Font("Arial", 11);
            comboBox5.Font = new Font("Arial", 11);
                textBox9.Font = new Font("Arial", 11);
                textBox1.Font = new Font("Arial", 11);
                textBox10.Font = new Font("Arial", 11);
                textBox3.Font = new Font("Arial", 11);
            textBox4.Font = new Font("Arial", 11);
            textBox5.Font = new Font("Arial", 11);
            textBox2.Font = new Font("Arial", 11);
            textBox6.Font = new Font("Arial", 11);
            textBox7.Font = new Font("Arial", 11);
            textBox8.Font = new Font("Arial", 11);
                richTextBox2.Font = new Font("Arial", 11);
                textBox3.Visible=false;
                textBox4.Visible = false;
                label7.Visible = false;
               

                label10.Visible = false;


                updateNDSinfo();
                richTextBox2.Clear();
                if (this.id != -1)
                {
                    updateunit_of_measurementinfoupdate(this.id_ed);
                    updatecountry_of_origininfo(this.id_coun);
                    updateType_toinfoupdate(this.id_type);
                    richTextBox2.Clear();
                   
             
             
                
                updateNDSinfo();
                //textBox1.BackColor = Color.LightGray;
        
                
                    textBox9.Text = this.name;
                    textBox1.Text = this.code;
                textBox2.Text = this.name_firm;
                    //comboBox2.Text = this.id_f;
                    comboBox3.Text = this.id_ed;
                comboBox4.Text = this.id_coun;
                textBox5.Text = this.numgtd;
                textBox6.Text = this.numrnpt;
                comboBox5.Text = this.id_nds.ToString();
                textBox4.Text = this.code_firm_pro;
                textBox3.Text = this.price_firm_pro.ToString();
                textBox7.Text = this.numexcise;
                textBox8.Text = this.numegis;
                    textBox10.Text = this.col.ToString();
                    richTextBox2.Text = this.description;

                richTextBox1.Clear();
                richTextBox1.AppendText("             Карточка товара\n");
                richTextBox1.AppendText("\n");
                    //richTextBox1.AppendText("Код товара: " + code + "\n");
                richTextBox1.AppendText("Название товара: " + name + "\n");
                    richTextBox1.AppendText("Тип товара: " + id_type + "\n");
                richTextBox1.AppendText("Название фирмы товара: " + name_firm + "\n");
                    richTextBox1.AppendText("Количество: " + col + "\n");
                    richTextBox1.AppendText("Единица измерения: " + id_ed + "\n");
                richTextBox1.AppendText("Страна производитель: " + id_coun + "\n");
                //richTextBox1.AppendText("Поставщик: " + id_f + "\n");
                //richTextBox1.AppendText("Код товара от поставщика: " + code_firm_pro + "\n");
                //richTextBox1.AppendText("Цена товара от поставщика: " + price_firm_pro + "\n");
                richTextBox1.AppendText("НДС: " + id_nds + "\n");
                richTextBox1.AppendText("ГТД: " + numgtd + "\n");
                richTextBox1.AppendText("РНПТ: " + numrnpt + "\n");
                richTextBox1.AppendText("Ставка акциза: " + numexcise + "\n");
                richTextBox1.AppendText("ЕГАИС: " + numegis + "\n");



                }
            }
            catch { }
        }

        public void updateType_toinfo(int id_t)
            {
                try
                {
                String sql1 = "Select * from Type_to where id=";
                  sql1 += id_t.ToString();
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
            ds1.Reset();
            da1.Fill(ds1);
            dt1 = ds1.Tables[0];
            comboBox1.DataSource = dt1;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
            }
        public void updateType_toinfoupdate(string name)
        {
            try
            {
                String sql6 = "Select * from Type_to where name='";
                sql6 += name;
                sql6 += "'";
                NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql6, con);
                ds6.Reset();
                da6.Fill(ds6);
                dt6 = ds6.Tables[0];
                comboBox1.DataSource = dt6;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        //public void updateFirminfo(int id_f)
        //        {
        //            try
        //            {
        //                String sql2 = "Select * from Firm where id=";
        //        sql2 += id_f.ToString();
        //        NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(sql2, con);
        //    ds2.Reset();
        //    da2.Fill(ds2);
        //    dt2 = ds2.Tables[0];
        //    comboBox2.DataSource = dt2;
        //    comboBox2.DisplayMember = "name_f";
        //    comboBox2.ValueMember = "id";
        //    this.StartPosition = FormStartPosition.CenterScreen;
        //            }
        //            catch { }
        //        }
        public void updateunit_of_measurementinfo(int id_t)
                    {
                        try
                        {
                            String sql3 = "Select * from unit_of_measurement  where id=";
                sql3 += id_t.ToString();
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
            ds3.Reset();
            da3.Fill(ds3);
            dt3 = ds3.Tables[0];
            comboBox3.DataSource = dt3;
            comboBox3.DisplayMember = "litter";
            comboBox3.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
                        }
                        catch { }
                    }
        public void updateunit_of_measurementinfoupdate(string litter)
        {
            try
            {
                String sql3 = "Select * from unit_of_measurement  where litter='";
                sql3 += litter;
                sql3 += "'";
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
                ds3.Reset();
                da3.Fill(ds3);
                dt3 = ds3.Tables[0];
                comboBox3.DataSource = dt3;
                comboBox3.DisplayMember = "litter";
                comboBox3.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updatecountry_of_origininfo(int id_t)
                        {
                            try
                            {
                                String sql4 = "Select * from country_of_origin where id=";
                sql4 += id_t.ToString();
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
            ds4.Reset();
            da4.Fill(ds4);
            dt4 = ds4.Tables[0];
            comboBox4.DataSource = dt4;
            comboBox4.DisplayMember = "litter";
            comboBox4.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
                            }
                            catch { }
                        }
        public void updatecountry_of_origininfo(string litter)
        {
            try
            {
                String sql4 = "Select * from country_of_origin  where litter='";
                sql4 += litter;
                sql4 += "'";
                NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
                ds4.Reset();
                da4.Fill(ds4);
                dt4 = ds4.Tables[0];
                comboBox4.DataSource = dt4;
                comboBox4.DisplayMember = "litter";
                comboBox4.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        public void updateNDSinfo()
                            {
                                try
                                { String sql5 = "Select * from NDS ORDER BY percent ASC";
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            comboBox5.DataSource = dt5;
            comboBox5.DisplayMember = "percent";
            comboBox5.ValueMember = "id";
            this.StartPosition = FormStartPosition.CenterScreen;
                                }
                                catch { }
                            }
       
        private void button3_Click(object sender, EventArgs e)
        {
           
            try
            {


                int id = 0;
                string name = "";
              
                int aa;
                Type_to fp = new Type_to(con, id,name);

                fp.ShowDialog();
                if (fp.name != "")
                {
                    updateType_toinfo(fp.id);


                    aa = fp.id;
                  
                }
                else
                {
                    comboBox1.Text = "Тип товара не выбран";
                  
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    comboBox3.Text = "Единица измерения не выбрана";

                }
            }
            catch { }
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                int id = 0;
                string name = "";


                country_of_origin_in fp = new country_of_origin_in(con, id, name);
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatecountry_of_origininfo(fp.id);


                    ;

                }
                else
                {
                    comboBox4.Text = "Код страны не выбран";

                }
            }
            catch { }
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //try
            //{


            //    int id_f = 0;
            //    string name = "";
             
            //    firm fp = new firm(con, id_f, name);
            //    fp.ShowDialog();
            //    if (fp.name != "")
            //    {
            //        updateFirminfo(fp.id);

            //    }
            //    else
            //    {
            //        comboBox2.Text = "Поставщик не выбран";

            //    }
            //}
            //catch { }
        }
     
        

        private void button5_Click(object sender, EventArgs e)
        {
            NDS_in fp = new NDS_in(con);
            fp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox5.Text = "Нет";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox6.Text = "Нет";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox7.Text = "Нет";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox8.Text = "Нет";
        }

        private void button6_Click(object sender, EventArgs e)
                                {
                                    try
                                    {
                                        gtd fp = new gtd(con,"");
                fp.ShowDialog();
                if (fp.numgtd != "")
            {
                textBox5.Text = fp.numgtd;
            }
            else
            { textBox5.Text = "Нет"; }
                                    }
                                    catch { }
                                }

        private void button9_Click(object sender, EventArgs e)
                                    {
                                        try
                                        {
                                            rnpt fp = new rnpt(con, "");
                fp.ShowDialog();
                if (fp.numrnpt != "")
            {
                textBox6.Text = fp.numrnpt;
            }
            else
            { textBox6.Text = "Нет"; }
                                        }
                                        catch { }
                                    }

        private void button11_Click(object sender, EventArgs e)
                                        {
                                            try
                                            {
                                                excise fp = new excise(con, "");
                fp.ShowDialog();
                if (fp.numexcise != "")
            {
                textBox7.Text = fp.numexcise;
            }
            else
            { textBox7.Text = "Нет"; }
                                            }
                                            catch { }
                                        }

        private void button13_Click(object sender, EventArgs e)
                                            {
                                                try
                                                {
                                                    egis fp = new egis(con, "");
                fp.ShowDialog();
                if (fp.numegis != "")
            {
                textBox8.Text = fp.numegis;
            }
            else
            { textBox8.Text = "Нет"; }
                                                }
                                                catch { }

                                            }

        private void button16_Click(object sender, EventArgs e)
                                                {
                                                    try
                                                    {
                                                    
            string code = textBox1.Text;
                string id_type= comboBox1.Text;
                string name = textBox9.Text;
                string name_firm = textBox2.Text;
            //string firm = comboBox2.Text;
            string ed = comboBox3.Text;
            string coun = comboBox4.Text;
            string gtd = textBox5.Text;
            string rnpt = textBox6.Text;
            string nds = comboBox5.Text;
            string code_post = textBox4.Text;
            string pr_post = textBox3.Text;
            string ak = textBox7.Text;
            string egis = textBox8.Text;
                string col = textBox10.Text;
                richTextBox1.Clear();

            richTextBox1.AppendText("             Карточка товара\n");
            richTextBox1.AppendText("\n");
                richTextBox1.AppendText("Код товара: " + code + "\n");
                richTextBox1.AppendText("Название товара: " + name + "\n");
                richTextBox1.AppendText("Тип товара: " + id_type + "\n");
           
            richTextBox1.AppendText("Название фирмы товара: " + name_firm + "\n");
                richTextBox1.AppendText("Количество: " + col + "\n");
                richTextBox1.AppendText("Единица измерения: " + ed + "\n");
            richTextBox1.AppendText("Страна производитель: " + coun + "\n");
            //richTextBox1.AppendText("Поставщик: " + firm + "\n");
            //richTextBox1.AppendText("Код товара от поставщика: " + code_post + "\n");
            //richTextBox1.AppendText("Цена товара от поставщика: " + pr_post + "\n");
            richTextBox1.AppendText("НДС: " + nds + "\n");
            richTextBox1.AppendText("ГТД: " + gtd + "\n");
            richTextBox1.AppendText("РНПТ: " + rnpt + "\n");
            richTextBox1.AppendText("Ставка акциза: " + ak + "\n");
            richTextBox1.AppendText("ЕГАИС: " + egis + "\n");

                                                    }
                                                    catch { }
                                                }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int code_;
            DataTable dt31 = new DataTable();
            DataSet ds31 = new DataSet();
            String sql31 = "Select * from Product_card ORDER BY id DESC LIMIT 1 ;";
            NpgsqlDataAdapter da31 = new NpgsqlDataAdapter(sql31, con);
            ds31.Reset();
            da31.Fill(ds31);
            dt31 = ds31.Tables[0];
            if (dt31.Rows.Count > 0)
            {
                code_= Convert.ToInt32(dt31.Rows[0]["code"].ToString());

            }
            else
            {
                code_ = 100;
            }
            if (this.id == -1)
            {
                try
                {

                    string sql = "Insert into Product_card ( code,id_ed,id_coun,numgtd,numrnpt,id_nds,col_pro,code_firm_pro,price_firm_pro,numexcise,numegis,name_firm,name,description,id_type ) values ( :code,:id_ed,:id_coun,:numgtd,:numrnpt,:id_nds,:col_pro,:code_firm_pro,:price_firm_pro,:numexcise,:numegis,:name_firm,:name,:description,:id_type)";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("id_type", comboBox1.SelectedValue);
                    command.Parameters.AddWithValue("code", (code_+1).ToString());
                    command.Parameters.AddWithValue("name_firm", textBox2.Text);
                    command.Parameters.AddWithValue("col_pro", Convert.ToDouble(textBox10.Text));
                    //command.Parameters.AddWithValue("id_f", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("id_ed", comboBox3.SelectedValue);
                    command.Parameters.AddWithValue("id_coun", comboBox4.SelectedValue);
                    command.Parameters.AddWithValue("numgtd", textBox5.Text);
                    command.Parameters.AddWithValue("numrnpt", textBox6.Text);
                    command.Parameters.AddWithValue("id_nds", comboBox5.SelectedValue);
                    command.Parameters.AddWithValue("code_firm_pro", textBox4.Text);
                    command.Parameters.AddWithValue("price_firm_pro", 0);
                    command.Parameters.AddWithValue("numexcise", textBox7.Text);
                    command.Parameters.AddWithValue("numegis", textBox8.Text);
                    command.Parameters.AddWithValue("name", textBox9.Text);
                    command.Parameters.AddWithValue("description", richTextBox2.Text);
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
                    string sql = "update  product_card set code=:code, id_ed=:id_ed, id_coun=:id_coun," +
                        "numgtd=:numgtd, numrnpt=:numrnpt, id_nds=:id_nds,col_pro=:col_pro, code_firm_pro=:code_firm_pro, price_firm_pro=:price_firm_pro, numexcise=:numexcise," +
                        "numegis=:numegis, name_firm=:name_firm, name=:name,description=:description,id_type=:id_type " +
                        " where id=:id";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.Parameters.AddWithValue("id_type", comboBox1.SelectedValue);
                command.Parameters.AddWithValue("code", textBox1.Text);
                command.Parameters.AddWithValue("name_firm", textBox2.Text);
                    //command.Parameters.AddWithValue("id_f", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("col_pro", Convert.ToDouble(textBox10.Text));
                    command.Parameters.AddWithValue("id_ed", comboBox3.SelectedValue);
                command.Parameters.AddWithValue("id_coun", comboBox4.SelectedValue);
                command.Parameters.AddWithValue("numgtd", textBox5.Text);
                command.Parameters.AddWithValue("numrnpt", textBox6.Text);
                command.Parameters.AddWithValue("id_nds", comboBox5.SelectedValue);
                command.Parameters.AddWithValue("code_firm_pro", textBox4.Text);
                command.Parameters.AddWithValue("price_firm_pro", 0);
                command.Parameters.AddWithValue("numexcise", textBox7.Text);
                command.Parameters.AddWithValue("numegis", textBox8.Text);
                command.Parameters.AddWithValue("name", textBox9.Text);
                command.Parameters.AddWithValue("description", richTextBox2.Text);
                command.Parameters.AddWithValue("id", this.id);

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

        private void button14_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button17_Click(object sender, EventArgs e)
                                                    {
                                                      
    }

        private void button17_Click_1(object sender, EventArgs e)
        {
            try
            {
                newType_to f = new newType_to(con, -1, "", "");
                f.ShowDialog();

                
            }
            catch { }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }
    }
    }
