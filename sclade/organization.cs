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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


using Word = Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
namespace sclade
{
    public partial class organization : Form
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
        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
 
        public organization(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(MainForm_MouseDown);
            this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
        }
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Начинаем перетаскивание, если нажали левую кнопку мыши
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position; // Получаем текущую позицию курсора
                dragFormPoint = this.Location; // Получаем текущее местоположение формы
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Если перетаскиваем форму, обновляем её позицию
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Завершаем перетаскивание
            dragging = false;
        }
        public void updatecountry_of_origininfo(int id_t)
        {
            //try
            //{
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
            //}
            //catch { }
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

        public void Update()
        {
            try
            {
                
            String sql1 = "Select  organization.id as id,organization.name_f,organization.phone_f,organization.view_,country_of_origin.id as country_of_registration ,organization.INN,organization.KPP,organization.OGRN,organization.pc,organization.bank,organization.bik  from organization,country_of_origin where organization.country_of_registration=country_of_origin.id ORDER BY organization.id DESC LIMIT 1 ;";
            NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql1, con);
            ds5.Reset();
            da5.Fill(ds5);
            dt5 = ds5.Tables[0];
            if (dt5.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt5.Rows[0]["id"]);
                    String sql = "Select organization.id,organization.name_f,organization.phone_f,organization.view_,country_of_origin.litter,organization.INN,organization.KPP,organization.OGRN,organization.pc,organization.bank,organization.bik  from organization,country_of_origin where organization.country_of_registration=country_of_origin.id and organization.id = " + id.ToString() +"  ORDER BY organization.name_f ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название";
                    dataGridView1.Columns[2].HeaderText = "Контактный телефон";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].HeaderText = "Статус поставщика";
                    dataGridView1.Columns[4].HeaderText = "Страна регистрации";
                    dataGridView1.Columns[5].HeaderText = "ИНН";
                    dataGridView1.Columns[6].HeaderText = "КПП";
                    dataGridView1.Columns[7].HeaderText = "ОРГН";
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    this.StartPosition = FormStartPosition.CenterScreen;

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


                    updatecountry_of_origininfo(Convert.ToInt32(dt5.Rows[0]["country_of_registration"]));

                    textBox1.BackColor = Color.LightGray;
                    textBox2.BackColor = Color.LightGray;
                    //textBox3.BackColor = Color.LightGray;
                    textBox4.BackColor = Color.LightGray;
                    textBox9.BackColor = Color.LightGray;
                    textBox6.BackColor = Color.LightGray;
                    textBox7.BackColor = Color.LightGray;
                    textBox8.BackColor = Color.LightGray;
                    textBox10.BackColor = Color.LightGray;
                    comboBox1.BackColor = Color.LightGray;
                    comboBox2.BackColor = Color.LightGray;

                    textBox1.Text = dt5.Rows[0]["name_f"].ToString();
                textBox2.Text = dt5.Rows[0]["phone_f"].ToString();
                //textBox3.Text = this.fio_f;
                textBox4.Text = dt5.Rows[0]["INN"].ToString();
                textBox9.Text = dt5.Rows[0]["KPP"].ToString();
                textBox6.Text = dt5.Rows[0]["bik"].ToString();
                textBox7.Text = dt5.Rows[0]["bank"].ToString();
                textBox8.Text = dt5.Rows[0]["OGRN"].ToString();
                textBox10.Text = dt5.Rows[0]["pc"].ToString();
                comboBox1.Text = dt5.Rows[0]["view_"].ToString();
          
                    /* textBox4.Text = this.country;
                     textBox5.Text = this.city;
                     textBox6.Text = this.street;
                     textBox7.Text = this.house;
                     textBox8.Text = this.post_in;*/
                    updateaddressinfo(Convert.ToInt32(dt5.Rows[0]["id"]));


            }
            else
            {

                MessageBox.Show("Данные организации не заполнены. Заполните данные.");
                comboBox2.Enabled = false;
                    button1.Visible = true;

                    comboBox1.Text = "Не указано";
            comboBox2.Text = "Код страны не выбран";
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
                    String sqli = "Select Address_organization.id, organization.id, Address_organization.country_f,Address_organization.city_f,Address_organization.street_f,Address_organization.house_f,Address_organization.post_in_f  from organization , Address_organization  where organization.id = Address_organization.id_f and organization.id=:id ORDER BY Address_organization.id ASC;";

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
                    String sqli = "Select Address_organization.id, organization.id,  Address_organization.country_f,Address_organization.city_f,Address_organization.street_f,Address_organization.house_f,Address_f.post_in_f  from organization, Address_organization  where organization.id =  Address_organization.id_f ORDER BY Address_organization.id ASC;";

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



        private void organization_Load(object sender, EventArgs e)
        {
            try
                    {
                dataGridView1.Visible = false;
                button1.Visible = false;
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

                dataGridView2.Font = new Font("Arial", 9);

                        dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Update();
            }
            catch { }
        }

        private void личныеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String sql1 = "Select  organization.id,organization.name_f,organization.phone_f,organization.view_,country_of_origin.litter,organization.INN,organization.KPP,organization.OGRN,organization.pc,organization.bank,organization.bik  from organization,country_of_origin where organization.country_of_registration=country_of_origin.id ORDER BY organization.id DESC LIMIT 1 ;";
            NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql1, con);
            ds9.Reset();
            da9.Fill(ds9);
            dt9 = ds9.Tables[0];
            if (dt9.Rows.Count == 0)
            {

                try
                {
  
                        string sql = "Insert into organization (name_f,phone_f, view_,country_of_registration,INN,KPP,OGRN,pc,bank,bik) values (:name_f,:phone_f,:view,:country_of_registration,:INN,:KPP,:OGRN,:pc,:bank,:bik);";
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
                        DialogResult result2 = MessageBox.Show("Данные организации успешно заполнены!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Update();
                    }
                    else
                        Update();

                }
                catch { }

            }

            else
            {
                if (dt9.Rows.Count == 1)
                {
                    try
                    {

                        string sql = "update organization set name_f=:name_f, phone_f=:phone_f, view_=:view,country_of_registration=:country_of_registration,INN=:INN,KPP=:KPP,OGRN=:OGRN,pc=:pc,bank=:bank,bik=:bik where id=:id; ";
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
                            DialogResult result2 = MessageBox.Show("Данные организации успешно изменены!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Update();

                        }
                        else
                            Update();



                    }
                    catch { }
                }
            }
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

        private void адресToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

              
                Address_organization f = new Address_organization(con, -1, this.id, "", "", "", "", "");
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
                int id_f = (int)dataGridView2.CurrentRow.Cells[1].Value;
                string country = (string)dataGridView2.CurrentRow.Cells[2].Value;
                string city = (string)dataGridView2.CurrentRow.Cells[3].Value;
                string street = (string)dataGridView2.CurrentRow.Cells[4].Value;
                string house = (string)dataGridView2.CurrentRow.Cells[5].Value;
                string post_in = (string)dataGridView2.CurrentRow.Cells[6].Value;
                Address_organization f = new Address_organization(con, id, id_f, country, city, street, house, post_in);
                f.ShowDialog();
                Update();
                updateaddressinfo(id);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String sql1 = "Select  organization.id,organization.name_f,organization.phone_f,organization.view_,country_of_origin.litter,organization.INN,organization.KPP,organization.OGRN,organization.pc,organization.bank,organization.bik  from organization,country_of_origin where organization.country_of_registration=country_of_origin.id  ;";
                NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql1, con);
                ds8.Reset();
                da8.Fill(ds8);
                dt8 = ds8.Tables[0];
                if (dt8.Rows.Count == 0)
                {
                    string sql = "Insert into organization (name_f,phone_f, view_,country_of_registration,INN,KPP,OGRN,pc,bank,bik) values (:name_f,:phone_f,:view,:country_of_registration,:INN,:KPP,:OGRN,:pc,:bank,:bik);";
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
                        Update();
                        DialogResult result2 = MessageBox.Show("Данные организации успешно заполнены!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch { DialogResult result = MessageBox.Show("Данные заполнены некорректно", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
       
        private void ExportToExcel_all(DataGridView dataGridView, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                // Создаем новую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;

                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    if (i == 11)
                    {


                    }


                    else
                    {


                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11)
                        {

                        }


                        else
                        {
                            worksheet.Cells[i + 2, m] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            m++;
                        }

                    }
                }


                workbook.SaveAs(filePath);
                // Освобождаем ресурсы
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { }
        }
        private void ExportToExcel_address(DataGridView dataGridView, DataGridView dataGridView2, string filePath)
        {
            try
            {

                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                // Создаем новую книгу
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;
                // Записываем заголовки столбцов
                //if (comboBox1.SelectedValue == null)
                //{
                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    if (i == 11)
                    {


                    }

                    else
                    {

                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }
                //}




                    // Записываем данные
                    //for (int i = 0; i < dataGridView.Rows.Count; i++)
                    //{
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11)
                        {

                        }


                        else
                        {


                            worksheet.Cells[2, m] = dataGridView.Rows[0].Cells[j].Value?.ToString();
                            m++;
                        }


                    }

                
                int h_1 = 1;
                // Записываем заголовки столбцов
                //if (comboBox1.SelectedValue == null)
                //{
                for (int i = 2; i < dataGridView2.Columns.Count; i++)

                {
                    if (i == 11)
                    {


                    }

                    else
                    {

                        worksheet.Cells[4, h_1] = dataGridView2.Columns[i].HeaderText;
                        h_1++;
                    }
                }
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                     m = 1;
                    for (int j = 2; j < dataGridView2.Columns.Count; j++)
                    {



                        worksheet.Cells[i + 5, m] = dataGridView2.Rows[i].Cells[j].Value?.ToString();
                        m++;


                    }
                }

                workbook.SaveAs(filePath);
                // Освобождаем ресурсы
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(excelApp);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            catch (Exception ex) { MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
}
        private void ExportJSON_all(DataGridView dataGridView, DataGridView dataGridView3, string filePath)
        {
            try
            {
                var dataList = new List<Dictionary<string, object>>();

                // Сбор данных из DataGridView
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow) // Игнорируем пустую строку
                    {
                        var data = new Dictionary<string, object>();
                        for (int j = 1; j < dataGridView.Columns.Count; j++)
                        {


                            data[dataGridView.Columns[j].HeaderText] = row.Cells[j].Value ?? ""; // Добавляем данные в словарь

                        }
                        dataList.Add(data);
                    }
                }

                // Сериализация списка в JSON
                string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                // Сохранение JSON в файл
                File.WriteAllText(filePath, json);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      
        private void ExportToJSON_address(DataGridView dataGridView, string filePath)
        {
            try
            {
                
                    var dataList = new List<Dictionary<string, object>>();

                    // Сбор данных из DataGridView
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        if (!row.IsNewRow) // Игнорируем пустую строку
                        {
                            var data = new Dictionary<string, object>();
                            for (int j = 2; j < dataGridView.Columns.Count; j++)
                            {


                                data[dataGridView.Columns[j].HeaderText] = row.Cells[j].Value ?? ""; // Добавляем данные в словарь

                            }
                            dataList.Add(data);
                        }
                    }

                    // Сериализация списка в JSON
                    string json = JsonConvert.SerializeObject(dataList, Formatting.Indented);

                    // Сохранение JSON в файл
                    File.WriteAllText(filePath, json);
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportToWord_all(DataGridView dataGridView, DataGridView dataGridView3, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;

            try
            {
                // Создаем новый экземпляр Word
                wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();

                // Добавляем заголовок
                Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                titleParagraph.Range.Text = "Контрагенты";
                titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph.Range.Font.Size = 12;

                titleParagraph.Range.InsertParagraphAfter();

                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count - 1);

                int h = 1;
                int tmp = 0;
                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {

                    //if (dataGridView.Columns[i].Visible == true)
                    //{
                    table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                    table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                    table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    table.Cell(1, h).Range.Font.Size = 8;
                    h++;
                    //}
                    //tmp = h;
                }



                // Добавляем заголовки столбцов
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {


                        table.Cell(i + 2, m).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(i + 2, m).Range.Font.Size = 8;
                        m++;



                    }





                }



                foreach (Word.Row row in table.Rows)
                {
                    foreach (Word.Cell cell in row.Cells)
                    {
                        cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                    }
                }
                // Сохраняем документ
                wordDoc.SaveAs(filePath);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                // Освобождаем ресурсы
                if (table != null) Marshal.ReleaseComObject(table);
                if (wordDoc != null)
                {
                    wordDoc.Close();
                    Marshal.ReleaseComObject(wordDoc);
                }
                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }
            }





        }
      


        private void ExportToWord_address(DataGridView dataGridView, DataGridView dataGridView2, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;
            Word.Table table2 = null;
            try
            {
                

                    // Создаем новый экземпляр Word
                    wordApp = new Word.Application();
                    wordDoc = wordApp.Documents.Add();
                  
                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Организация";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();


                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count - 1);

                    int h = 1;
                    int tmp = 0;
                    for (int i = 1; i < dataGridView.Columns.Count; i++)

                    {

                        //if (dataGridView.Columns[i].Visible == true)
                        //{
                        table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, h).Range.Font.Size = 8;
                        h++;
                        //}
                        //tmp = h;
                    }



                    // Заполняем таблицу данными
                    // Добавляем заголовки столбцов


                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        //if (dataGridView.Columns[j].Visible == true)
                        //{
                        table.Cell(2, m).Range.Text = dataGridView.Rows[0].Cells[j].Value?.ToString();
                        table.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(2, m).Range.Font.Size = 8;
                        m++;
                        //}


                    }






                    table.Borders.Enable = 1; // Включаем рамки для всей таблицы
                    foreach (Word.Row row in table.Rows)
                    {
                        foreach (Word.Cell cell in row.Cells)
                        {
                            cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                        }
                    }





                    //string code = (string)dataGridView1.CurrentRow.Cells[1].Value;

                    Word.Paragraph titleParagraph2 = wordDoc.Content.Paragraphs.Add();
                    titleParagraph2.Range.Text = "Адреса организации ";
                    titleParagraph2.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph2.Range.Font.Size = 12;

                    titleParagraph2.Range.InsertParagraphAfter();
                    if (dataGridView2.Rows.Count == 0)
                    {
                        MessageBox.Show("Ошибка: Нет данных ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создаем таблицу
                    table2 = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView2.Rows.Count + 1, dataGridView2.Columns.Count - 2);




                    h = 1;

                    for (int i = 2; i < dataGridView2.Columns.Count; i++)

                    {

                        //if (dataGridView.Columns[i].Visible == true)
                        //{
                        table2.Cell(1, h).Range.Text = dataGridView2.Columns[i].HeaderText;
                        table2.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table2.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table2.Cell(1, h).Range.Font.Size = 8;
                        h++;
                        //}
                        //tmp = h;
                    }



                    //// Добавляем заголовки столбцов
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        m = 1;
                        for (int j = 2; j < dataGridView2.Columns.Count; j++)
                        {


                            table2.Cell(i + 2, m).Range.Text = dataGridView2.Rows[i].Cells[j].Value?.ToString();
                            table2.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(i + 2, m).Range.Font.Size = 8;
                            m++;



                        }





                    }





                    foreach (Word.Row row in table2.Rows)
                    {
                        foreach (Word.Cell cell in row.Cells)
                        {
                            cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                        }
                    }
                    // Сохраняем документ
                    wordDoc.SaveAs(filePath);
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                // Освобождаем ресурсы
                if (table != null) Marshal.ReleaseComObject(table);
                if (table2 != null) Marshal.ReleaseComObject(table2);
                if (wordDoc != null)
                {
                    wordDoc.Close();
                    Marshal.ReleaseComObject(wordDoc);
                }
                if (wordApp != null)
                {
                    wordApp.Quit();
                    Marshal.ReleaseComObject(wordApp);
                }
            }





        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;

                        saveFileDialog.FileName = "organization_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
            }
            catch { }
        }

        private void вExcelДанныеАдресовФирмыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                {
                    

                        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                            saveFileDialog.Title = "Сохранить файл Excel";
                            DateTime time = DateTime.Today.Date;
                          
                            saveFileDialog.FileName = "organization_address_" + "_"  + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ExportToExcel_address(dataGridView1, dataGridView2, saveFileDialog.FileName);
                            }
                        }

                  

                }
            }
            catch { }
        }

        private void вWordВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {



                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Files|*.docx";
                    saveFileDialog.Title = "Сохранить файл Word";
                    saveFileDialog.FileName = "organization_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToWord_all(dataGridView1, dataGridView2, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вWordВсеДанныеToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void вWordАдресаКонтрагентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Word Files|*.docx";
                        saveFileDialog.Title = "Сохранить файл Word";
                        
                        saveFileDialog.FileName = $"organization_address_"   + DateTime.Today.ToString("dd_MM_yyyy") + ".docx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportToWord_address(dataGridView1, dataGridView2, saveFileDialog.FileName);
                        }
                    }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вJSONВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    saveFileDialog.FileName = $"organization_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Вызываем метод экспорта с выбранным путем
                        ExportJSON_all(dataGridView1, dataGridView2, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вJSONАдресКонтагентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                        saveFileDialog.Title = "Сохраните файл JSON как";
                       
                        saveFileDialog.FileName = $"organization_address_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportToJSON_address(dataGridView2, saveFileDialog.FileName);
                        }
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
