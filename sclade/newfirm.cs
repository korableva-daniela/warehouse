using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Npgsql;
using OfficeOpenXml;

namespace sclade
{
    public partial class newfirm : Form
    {
      
        public NpgsqlConnection con;
        public int id;
        public string name;
        public string phone;
        //public string fio_f;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public string view;
        public string country_of_registration;
        public string INN;
        public string KPP;
        public string OGRN;
        public string pc;
        public string bank;
        public string bik;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        DataTable dt1 = new DataTable();
        DataSet ds1 = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        DataTable dt5 = new DataTable();
        DataSet ds5 = new DataSet();
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        DataTable dt7 = new DataTable();
        DataSet ds7 = new DataSet();
        DataTable dt4 = new DataTable();
        DataSet ds4 = new DataSet();
      
        public newfirm(NpgsqlConnection con, int id, string name, string phone,  string view, string country_of_registration, string INN, string KPP, string OGRN, string pc, string bank, string bik)
        {
            //try
            //{
            //    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                this.con = con;
                this.id = id;
                this.name = name;
                this.phone = phone;
                //this.fio_f = fio_f;
                this.view = view;
                this.country_of_registration = country_of_registration;
                this.INN = INN;
                this.KPP = KPP;
                this.OGRN = OGRN;
                this.pc = pc;
                this.bank = bank;
                this.bik = bik;
                InitializeComponent();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }
        //public void updatecountry_of_origin()
        //{
        //    try
        //    {
        //        String sqli = "Select * from country_of_origin ORDER BY litter ASC";
        //    NpgsqlDataAdapter dai = new NpgsqlDataAdapter(sqli, con);
        //    dsi.Reset();
        //    dai.Fill(dsi);
        //    dti = dsi.Tables[0];
        //    comboBox2.DataSource = dti;
        //    comboBox2.DisplayMember = "litter";
        //    comboBox2.ValueMember = "id";
        //    this.StartPosition = FormStartPosition.CenterScreen;
        //    }
        //    catch { }
        //}
        public void Update()
            {
                try
                {
             
                comboBox2.Enabled = false;
                comboBox1.Text = "Не указано";
                comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
                //comboBox1.Enabled = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            dataGridView2.Font = new Font("Arial", 9);

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (id == -1)
            {
               
            }
            else
            {

                    String sql = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id and Firm.id=:id ORDER BY Firm.id ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.AddWithValue("id", id);
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
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
            if (id == -1)
            {
                
            }
            else
            {

                    String sql1 = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id and Firm.id=:id ORDER BY Firm.id ASC;";
                    NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sql1, con);
                    da1.SelectCommand.Parameters.AddWithValue("id", id);
                    ds1.Reset();
                    da1.Fill(ds1);
                    dt1 = ds1.Tables[0];
                    dataGridView2.DataSource = dt1;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Visible = false;
                    dataGridView2.Columns[2].Visible = false;
                    //dataGridView2.Columns[3].Visible = false;
                    dataGridView2.Columns[3].Visible = false;
                    dataGridView2.Columns[4].Visible = false;
                    dataGridView2.Columns[5].HeaderText = "ИНН";
                    dataGridView2.Columns[6].HeaderText = "КПП";
                    dataGridView2.Columns[7].HeaderText = "ОРГН";
                    dataGridView2.Columns[8].HeaderText = "p/c";
                    dataGridView2.Columns[9].HeaderText = "Банк";
                    dataGridView2.Columns[10].HeaderText = "Бик";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                }
                catch { }
            }
        private void newfirm_Load(object sender, EventArgs e)
                {
            try
            {
                comboBox1.Text = "Код страны не выбран";
                comboBox2.Text = "Код страны не выбран";
                dataGridView1.ReadOnly = true;
                dataGridView2.ReadOnly = true;
                //comboBox3.Visible=false;

            comboBox2.Font = new Font("Arial", 11);
                comboBox1.Font = new Font("Arial", 11);
                label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            //label3.Font = new Font("Arial", 11);
            label4.Font = new Font("Arial", 11);
            label5.Font = new Font("Arial", 11);
            label6.Font = new Font("Arial", 11);
            label7.Font = new Font("Arial", 11);
            label8.Font = new Font("Arial", 11);
            label9.Font = new Font("Arial", 11);
            label10.Font = new Font("Arial", 11);
            label11.Font = new Font("Arial", 11);
            label12.Font = new Font("Arial", 11);
            textBox1.Font = new Font("Arial", 11);
            textBox2.Font = new Font("Arial", 11);
            //textBox3.Font = new Font("Arial", 11);
            textBox4.Font = new Font("Arial", 11);
            textBox9.Font = new Font("Arial", 11);
            textBox6.Font = new Font("Arial", 11);
            textBox7.Font = new Font("Arial", 11);
            textBox8.Font = new Font("Arial", 11);
            textBox10.Font = new Font("Arial", 11);
            Update();
            if (this.id != -1)
            {
                    updatecountry_of_origininfo(this.country_of_registration);
                    textBox1.BackColor = Color.LightGray;
                textBox2.BackColor = Color.LightGray;
                //textBox3.BackColor = Color.LightGray;
                textBox4.BackColor = Color.LightGray;
                textBox9.BackColor = Color.LightGray;
                textBox6.BackColor = Color.LightGray;
                textBox7.BackColor = Color.LightGray;
                textBox8.BackColor = Color.LightGray;
                textBox10.BackColor = Color.LightGray;


                textBox1.Text = this.name;
                textBox2.Text = this.phone;
                //textBox3.Text = this.fio_f;
                textBox4.Text = this.INN;
                textBox9.Text = this.KPP;
                textBox6.Text = this.bik;
                textBox7.Text = this.bank;
                textBox8.Text = this.OGRN;
                textBox10.Text = this.pc;
                comboBox1.Text = this.view;
                comboBox2.Text = this.country_of_registration;
                            /* textBox4.Text = this.country;
                             textBox5.Text = this.city;
                             textBox6.Text = this.street;
                             textBox7.Text = this.house;
                             textBox8.Text = this.post_in;*/


                        }
        }
                    catch { }
                }

        private void button1_Click(object sender, EventArgs e)
        {
            int employeeId = -1;
            if (this.id == -1)
            {

                try
                {

                    string sql = "Insert into Firm (name_f,phone_f, view_,country_of_registration,INN,KPP,OGRN,pc,bank,bik) values (:name_f,:phone_f,:view,:country_of_registration,:INN,:KPP,:OGRN,:pc,:bank,:bik);";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.Parameters.AddWithValue("name_f", textBox1.Text);
                    command.Parameters.AddWithValue("phone_f", textBox2.Text);
                    //command.Parameters.AddWithValue("fio_f", textBox3.Text);
                    command.Parameters.AddWithValue("view", comboBox1.Text);
                    command.Parameters.AddWithValue("country_of_registration", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("INN", textBox4.Text);
                    command.Parameters.AddWithValue("KPP", textBox9.Text);
                    command.Parameters.AddWithValue("OGRN", textBox8.Text);
                    command.Parameters.AddWithValue("pc", textBox10.Text);
                    command.Parameters.AddWithValue("bank", textBox7.Text);
                    command.Parameters.AddWithValue("bik", textBox6.Text);



                    DialogResult result = MessageBox.Show("Вы уверены, что добавить  запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        String sql1 = "Select * from Firm ORDER BY id DESC LIMIT 1 ;";
                        NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql1, con);
                        ds5.Reset();
                        da5.Fill(ds5);
                        dt5 = ds5.Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            employeeId = Convert.ToInt32(dt5.Rows[0]["id"]);

                        }
                        else
                        {

                            MessageBox.Show("Пользователь не найден.");
                        }
                        this.StartPosition = FormStartPosition.CenterScreen;
                        String sql2 = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id and Firm.id=";
                        sql2 += employeeId;
                        NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql2, con);
                        ds6.Reset();
                        da6.Fill(ds6);
                        dt6 = ds6.Tables[0];
                        dataGridView1.DataSource = dt6;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].HeaderText = "Название";
                        dataGridView1.Columns[2].HeaderText = "Контактный телефон";
                        //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                        dataGridView1.Columns[3].HeaderText = "Статус клиента";
                        dataGridView1.Columns[4].HeaderText = "Страна регистрации";
                        dataGridView1.Columns[5].Visible = false;
                        dataGridView1.Columns[6].Visible = false;
                        dataGridView1.Columns[7].Visible = false;
                        dataGridView1.Columns[8].Visible = false;
                        dataGridView1.Columns[9].Visible = false;
                        dataGridView1.Columns[10].Visible = false;
                        this.StartPosition = FormStartPosition.CenterScreen;
                        String sql4 = "Select Firm.id,Firm.name_f,Firm.phone_f,Firm.view_,country_of_origin.litter,Firm.INN,Firm.KPP,Firm.OGRN,Firm.pc,Firm.bank,Firm.bik  from Firm,country_of_origin where Firm.country_of_registration=country_of_origin.id and Firm.id=";
                        sql4 += employeeId;
                        NpgsqlDataAdapter da7 = new NpgsqlDataAdapter(sql4, con);
                        ds7.Reset();
                        da7.Fill(ds7);
                        dt7 = ds7.Tables[0];
                        dataGridView2.DataSource = dt7;

                        dataGridView2.Columns[0].Visible = false;
                        dataGridView2.Columns[1].Visible = false;
                        dataGridView2.Columns[2].Visible = false;
                        //dataGridView2.Columns[3].Visible = false;
                        dataGridView2.Columns[3].Visible = false;
                        dataGridView2.Columns[4].Visible = false;
                        dataGridView2.Columns[5].HeaderText = "ИНН";
                        dataGridView2.Columns[6].HeaderText = "КПП";
                        dataGridView2.Columns[7].HeaderText = "ОРГН";
                        dataGridView2.Columns[8].HeaderText = "p/c";
                        dataGridView2.Columns[9].HeaderText = "Банк";
                        dataGridView2.Columns[10].HeaderText = "Бик";
                        this.StartPosition = FormStartPosition.CenterScreen;
                   
                    DialogResult result1 = MessageBox.Show("Добавить данные об адресах поставщика?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result1 == DialogResult.Yes)
                    {

                            newaddressinfo_f f = new newaddressinfo_f(con, -1, employeeId, "", "", "", "", "");
                            f.ShowDialog();
                            Update();
                        }
                        Update();
                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
    
            else
            {

                try
                {

                    string sql = "update Firm set name_f=:name_f, phone_f=:phone_f, view_=:view,country_of_registration=:country_of_registration,INN=:INN,KPP=:KPP,OGRN=:OGRN,pc=:pc,bank=:bank,bik=:bik where id=:id; ";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
              
                    command.Parameters.AddWithValue("id", this.id);
                    command.Parameters.AddWithValue("name_f", textBox1.Text);
                    command.Parameters.AddWithValue("phone_f", textBox2.Text);
                    //command.Parameters.AddWithValue("fio_f", textBox3.Text);
                    command.Parameters.AddWithValue("view", comboBox1.Text);
                    command.Parameters.AddWithValue("country_of_registration", comboBox2.SelectedValue);
                    command.Parameters.AddWithValue("INN", textBox4.Text);
                    command.Parameters.AddWithValue("KPP", textBox9.Text);
                    command.Parameters.AddWithValue("OGRN", textBox8.Text);
                    command.Parameters.AddWithValue("pc", textBox10.Text);
                    command.Parameters.AddWithValue("bank", textBox7.Text);
                    command.Parameters.AddWithValue("bik", textBox6.Text);


                    DialogResult result = MessageBox.Show("Вы уверены, что хотите изменить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {

                        command.ExecuteNonQuery();
                        Update();
        DialogResult result1 = MessageBox.Show("Добавить данные об адресах поставщика?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result1 == DialogResult.Yes)
                        {
                            
                            newaddressinfo_f f = new newaddressinfo_f(con, -1, this.id, "", "", "", "", "");
                            f.ShowDialog();
                            Update();
                        }
                        Update();
                    }
                }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
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
                comboBox2.DataSource = dt4;
                comboBox2.DisplayMember = "litter";
                comboBox2.ValueMember = "id";
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
                comboBox2.DataSource = dt4;
                comboBox2.DisplayMember = "litter";
                comboBox2.ValueMember = "id";
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


                country_of_origin_in fp = new country_of_origin_in(con, id, name);
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatecountry_of_origininfo(fp.id);


                    ;

                }
                else
                {
                    comboBox2.Text = "Код страны не выбран";

                }
            }
            catch { };
        }
     
        private void LoadDataFromExcel(string filePath)
        {
            // Проверка на существование файла
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден. Пожалуйста, проверьте путь к файлу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileInfo fileInfo = new FileInfo(filePath);

            try
            {
                using (ExcelPackage package = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Получаем первый лист

                    // Проверка на наличие данных
                    if (worksheet.Dimension == null)
                    {
                        MessageBox.Show("Лист пуст. Пожалуйста, проверьте файл Excel.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DataTable dataTable = new DataTable();

                    // Добавляем заголовки столбцов
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        dataTable.Columns.Add(worksheet.Cells[1, col].Text);
                    }

                    // Добавляем строки данных
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            dataRow[col - 1] = worksheet.Cells[row, col].Text;
                        }
                        dataTable.Rows.Add(dataRow);
                    }

                    // Заполняем DataGridView данными
                    dataGridView1.DataSource = dataTable;
                }
            
            }
                catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

}



private void выгрузитьВExcelВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel Files|*.xlsx;*.xls",
                Title = "Выберите файл Excel"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadDataFromExcel(openFileDialog.FileName);
            }
        }
    }
}
