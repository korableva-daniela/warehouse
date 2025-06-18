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
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
namespace sclade
{
    public partial class Product_card : Form
    {
        public int id;

     
        public string name;
        
        public string code;
        public string id_f;
        public int stor;
        
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public int div;
        public Product_card(NpgsqlConnection con, int id, string name, string code, string id_f,int stor,int div )
        {
            this.id = id;
            this.div = div;
            this.name = name;
            this.stor = stor;
            this.code = code;
            this.id_f = id_f;
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
        public void Update()
        {
            comboBox1.Enabled = false;
            comboBox1.Font = new Font("Arial", 11);
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            label1.Font = new Font("Arial", 11);
            richTextBox2.Font = new Font("Arial", 11);
            if (this.stor != -1)
            {
                try
                {
                    String sql3 = "Select * from storehouse where id=";
                    sql3 += this.stor.ToString();
                    NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
                    ds3.Reset();
                    da3.Fill(ds3);
                    dt3 = ds3.Tables[0];
                    comboBox1.DataSource = dt3;
                    comboBox1.DisplayMember = "name";
                    comboBox1.ValueMember = "id";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                catch { }
            }
            else
            {
                comboBox1.Text = "Склад не выбран";
            }
            try
            {
                
                if (id != 0)
                {
                    button6.Visible = false;
                    this.WindowState = FormWindowState.Maximized;
                    
                }
                if (id == -2)
                {
                    button6.Visible = false;
                    //this.WindowState = FormWindowState.Minimized;
                    button7.Visible = false;
                    menuStrip1.Visible = false;
                    this.Size = new Size(600, 500);

                }
                if (id == 0)
                {
                    button4.Visible = false;
                    button3.Visible = false;

                }
                if (this.code != "")
                {
                    textBox1.Text = this.code;
                    //textBox1.Enabled = false;
                }
                if (comboBox1.Text == "Склад не выбран")
                {
                    
                    if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          " Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS " +
                          " where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id   and " +
                          " Product_card.id_nds = NDS.id ORDER BY id ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          " Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS " +
                          " where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and  " +
                          " Product_card.id_nds = NDS.id and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store  " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and " +
                          " Product_card.id_nds = NDS.id and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and " +
                          " Product_card.id_nds = NDS.id and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and " +
                          " Product_card.id_nds = NDS.id and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }

                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and  " +
                          " Product_card.id_nds = NDS.id and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select   Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and  " +
                          " Product_card.id_nds = NDS.id and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and " +
                          " Product_card.id_nds = NDS.id and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }

                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and  " +
                          " Product_card.id_nds = NDS.id and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and " +
                          " Product_card.id_nds = NDS.id and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id  and " +
                          " Product_card.id_nds = NDS.id and  Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT  Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and  " +
                          " Product_card.id_nds = NDS.id and  Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT  Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and " +
                          " Product_card.id_nds = NDS.id  and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and  " +
                          " Product_card.id_nds = NDS.id aand Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;

                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }

                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and " +
                          " Product_card.id_nds = NDS.id and Product_card.code  ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select  DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                          "  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS   " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and " +
                          " Product_card.id_nds = NDS.id and Product_card.code  ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ORDER BY Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }



                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Код товара";
                    dataGridView1.Columns[2].HeaderText = "Название товара";
                    dataGridView1.Columns[3].HeaderText = "Тип товара";
                    dataGridView1.Columns[4].HeaderText = "Название фирмы продукта";
                    //dataGridView1.Columns[5].HeaderText = "Поставщик";
                    dataGridView1.Columns[5].HeaderText = "Количество";
                    dataGridView1.Columns[6].HeaderText = "Единица измерения";

                    dataGridView1.Columns[7].HeaderText = "Страна производитель";
                    dataGridView1.Columns[8].HeaderText = "Номер ГТД";
                    dataGridView1.Columns[9].HeaderText = "Номер РНПТ";
                    dataGridView1.Columns[10].HeaderText = "НДС";
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].HeaderText = "Номер ставка акциза";
                    dataGridView1.Columns[14].HeaderText = "Номер ЕГАИС";
                    dataGridView1.Columns[15].Visible = false;



                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                 
                   
                    if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and storehouse.id = ";
                        sql += this.stor.ToString();
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }

                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and  prod_store.count>0 and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }

                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id  and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and  Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.code ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;

                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }

                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and Product_card.code  ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }
                    else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                    {
                        String sql = "Select DISTINCT Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro,unit_of_measurement.litter,country_of_origin.litter," +
                          "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description," +
                          "storehouse.name,prod_store.count  from Type_to, Product_card  ,unit_of_measurement ,country_of_origin ,NDS, storehouse,prod_store " +
                          "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                          " Product_card.id_nds = NDS.id and prod_store.id_store=storehouse.id and  prod_store.id_product_card=Product_card.id and prod_store.count>0 and  Product_card.code  ILIKE '";
                        sql += textBox1.Text;
                        sql += "%' and Product_card.name ILIKE '";
                        sql += textBox2.Text;
                        sql += "%' and Product_card.name_firm ILIKE '";
                        sql += textBox3.Text;
                        sql += "%' and Type_to.name ILIKE '";
                        sql += textBox4.Text;
                        sql += "%' ";
                        sql += " and storehouse.id = ";
                        sql += this.stor.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);

                    }



                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Код товара";
                    dataGridView1.Columns[2].HeaderText = "Название товара";
                    dataGridView1.Columns[3].HeaderText = "Тип товара";
                    dataGridView1.Columns[4].HeaderText = "Название фирмы продукта";
                    //dataGridView1.Columns[5].HeaderText = "Поставщик";

                    dataGridView1.Columns[5].HeaderText = "Количество";
                    dataGridView1.Columns[6].HeaderText = "Единица измерения";

                    dataGridView1.Columns[7].HeaderText = "Страна производитель";
                    dataGridView1.Columns[8].HeaderText = "Номер ГТД";
                    dataGridView1.Columns[9].HeaderText = "Номер РНПТ";
                    dataGridView1.Columns[10].HeaderText = "НДС";
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].HeaderText = "Номер ставка акциза";
                    dataGridView1.Columns[14].HeaderText = "Номер ЕГАИС";
                    dataGridView1.Columns[15].Visible = false;
                    dataGridView1.Columns[16].HeaderText = "Название склада";
                    dataGridView1.Columns[17].HeaderText = "Количество товара на складе";


                    this.StartPosition = FormStartPosition.CenterScreen;
                }
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
                    if (dataGridView1.CurrentRow.Index == 0)
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
        private void Product_card_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                Update();
                dataGridView1.ReadOnly = true;
                label1.Font = new Font("Arial", 11);
                label2.Font = new Font("Arial", 11);
                label3.Font = new Font("Arial", 11);
                label4.Font = new Font("Arial", 11);
                label5.Font = new Font("Arial", 11);
                textBox1.Font = new Font("Arial", 11);
                textBox2.Font = new Font("Arial", 11);
                textBox3.Font = new Font("Arial", 11);
                textBox4.Font = new Font("Arial", 11);
            }
            catch
            {

            }

        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newProduct_card f = new newProduct_card(con, -1, "",  "", "", "",0, "", "", "", "", 0, "", 0, "", "", "");
            f.ShowDialog();
            Update();
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                    try
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
                string description = (string)dataGridView1.CurrentRow.Cells[15].Value;
                newProduct_card f = new newProduct_card(con, id_k, name, name_type, name_firm, code,col,  ed, coun, gtd, rnpt, nds, code_post, pr_post, ak, egis, description);
            f.ShowDialog();
            Update();
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    //if (id != -1)
                    //{
                    //    description(id);

                }
            }
            catch { }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                        try
                        {
                            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM Product_card WHERE id=:id", con);

            command.Parameters.AddWithValue("id", id);

            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить запись?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {

                command.ExecuteNonQuery();
                Update();
            }
            else
                Update();
                //description(id);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void description_d(int id)
        {
                            try
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
            description(id);
            description_d(id);
            }
            catch { }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Firm_in fp = new Firm_in(con);
            fp.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.code = textBox1.Text;
            Update();
        }

        private void button5_Click(object sender, EventArgs e)
        {
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                int id_ = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string code_ = (string)dataGridView1.CurrentRow.Cells[1].Value;

                this.code = code_;
                this.id = id_;
                Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            filter fp = new filter(con, this.div);
            fp.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void посмотретьИнформациюОПартияхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                  
                    if (dataGridView1.CurrentRow.Cells[0].Value != null)
                    {
                        int id_pro = (int)dataGridView1.CurrentRow.Cells[0].Value;


                        batch_number fp = new batch_number(con, -1, "", id_pro, -1,-1, this.div);
                    fp.Show();
                }
                    //        else
                    //        {
                    //            DialogResult result = MessageBox.Show("У выбранного товара нет партий. Хотите создать новую партию товара?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    //            if (result == DialogResult.Yes)
                    //            {


                    //                //textBox1.Visible = false;
                    //                newbatch_number f = new newbatch_number(con, -1, comboBox1.Text, "", DateTime.Today, DateTime.Today, "", 0, id_pro_card, 0);
                    //                f.ShowDialog();


                    //                //checkBox1.Checked = true;

                    //            }
                    //            else { }

                    //        }
                
            }
            catch { }
        }
        public void updatestorehouseinfo(int id_s)
        {
            try
            {
                String sql3 = "Select * from storehouse where id=";
                sql3 += id_s.ToString();
                NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
                ds3.Reset();
                da3.Fill(ds3);
                dt3 = ds3.Tables[0];
                comboBox1.DataSource = dt3;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name, div, "");
                fp.ShowDialog();
                if (fp.name != "")
                {
                    updatestorehouseinfo(fp.id_c);
                    this.stor = fp.id_c;
                    Update();
                }
                else
                {
                    comboBox1.Text = "Склад не выбран";

                }
        }
            catch { }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            updatestorehouseinfo(-1);
            comboBox1.Text = "Склад не выбран";
            this.stor = -1;
            Update();
            

        }

        private void информацияОДвиженияхТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {

                    int id_pro = (int)dataGridView1.CurrentRow.Cells[0].Value;


                    if (this.stor != -1)
                    {
                        mov_pro fp = new mov_pro(con, this.stor, "", -1, id_pro, this.div);
                        fp.Show();
                    }
                    else
                    {
                        mov_pro fp = new mov_pro(con, -1, "", -1, id_pro, this.div);
                        fp.Show();
                    }
                }
            }
            catch { }
        }
        private void ExportToExcel(DataGridView dataGridView, string filePath)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true; // Установите в false, если не хотите показывать Excel

                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                int h = 1;

                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    if (i == 11 || i == 15 || i == 12)
                    {
                        if (i == 11)
                        {

                        }


                    }

                    else
                    {

                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }

                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {

                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11 || j == 12 || j == 15)
                        {

                        }

                        else
                        {

                            worksheet.Cells[2, m] = dataGridView.Rows[dataGridView1.CurrentCell.RowIndex].Cells[j].Value?.ToString();
                            m++;
                        }

                        //}
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
          

            workbook.SaveAs(filePath);
            // Освобождаем ресурсы
            Marshal.ReleaseComObject(worksheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(excelApp);
                MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                // Записываем заголовки столбцов
                //if (comboBox1.SelectedValue == null)
                //{
                for (int i = 1; i < dataGridView.Columns.Count; i++)

                {
                    if (i == 11 || i == 15 || i == 12)
                    {
                        if (i == 11)
                        {
                            //worksheet.Cells[1, i] = dataGridView.Columns[i + 2].HeaderText;
                            //i += 1;

                        }
                        //if (i == 15)
                        //{
                        //    worksheet.Cells[1, h] = "Описание";
                        //    h++;
                        //}

                    }


                    else
                    {


                        worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                        h++;
                    }
                }
                //}





                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11 || j == 12 || j == 15)
                        {
                            if (j == 11)
                            {
                                //worksheet.Cells[1, i] = dataGridView.Columns[i + 2].HeaderText;
                                //i += 1;

                            }

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
        private void ExportJSON_all(DataGridView dataGridView, string filePath)
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

                            if (dataGridView.Columns[j].Visible == true)
                            {
                                data[dataGridView.Columns[j].HeaderText] = row.Cells[j].Value ?? ""; // Добавляем данные в словарь
                            }
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
        private void ExportToJSON(DataGridView dataGridView, string filePath)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    var data = new Dictionary<string, object>();

                    // Сбор данных только из выбранной строки
                    for (int j = 1; j < dataGridView1.Columns.Count; j++)
                    {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            data[dataGridView1.Columns[j].HeaderText] = dataGridView1.CurrentRow.Cells[j].Value ?? ""; // Добавляем данные в словарь
                        }
                    }

                    // Сериализация в JSON
                    string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                    // Сохранение JSON в файл
                    File.WriteAllText(filePath, json);
                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ExportToWord_all(DataGridView dataGridView, string filePath)
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
                titleParagraph.Range.Text = "Товары";
                titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph.Range.Font.Size = 12;

                titleParagraph.Range.InsertParagraphAfter();


                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count - 4);
                
                int h = 1;
                // Добавляем заголовки столбцов
                for (int i = 1; i < dataGridView.Columns.Count; i++)
                {
                    if (dataGridView.Columns[i].Visible == true)
                    {
                        table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                        table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                        table.Cell(1,h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        table.Cell(1, h).Range.Font.Size = 8;
                        h++;
                    }
                }

                // Заполняем таблицу данными
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            table.Cell(i + 2, m).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(i + 2, m).Range.Font.Size = 8;
                            m++;
                        }
                    }
                }
                table.Borders.Enable = 1; // Включаем рамки для всей таблицы
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
        private void ExportToWord(DataGridView dataGridView, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;

            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    // Создаем новый экземпляр Word
                    wordApp = new Word.Application();
                    wordDoc = wordApp.Documents.Add();

                    // Добавляем заголовок
                    Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                    titleParagraph.Range.Text = "Товар";
                    titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph.Range.Font.Size = 12;

                    titleParagraph.Range.InsertParagraphAfter();

                    int m = 1;
                    int h = 1;
                    // Создаем таблицу
                    table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count-4);

                    // Добавляем заголовки столбцов
                    for (int i = 1; i < dataGridView.Columns.Count; i++)
                    {
                        if (dataGridView.Columns[i].Visible == true)
                        {
                            table.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                            table.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                            table.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(1, h).Range.Font.Size = 8;
                            h++;
                        }
                    }

                    // Заполняем таблицу данными

                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            table.Cell(2, m).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                            table.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table.Cell(2, m).Range.Font.Size = 8;
                            m++;
                        }
                    }

                    table.Borders.Enable = 1; // Включаем рамки для всей таблицы
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

        private void ExportToWordProduct(DataGridView dataGridView, string filePath)
        {
            Word.Application wordApp = null;
            Word.Document wordDoc = null;
            Word.Table table = null;
            Word.Table table2 = null;
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    wordApp = new Word.Application();
                wordDoc = wordApp.Documents.Add();
                    if (comboBox1.Text == "Склад не выбран")
                    {
                        // Добавляем заголовок
                        Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                        titleParagraph.Range.Text = "Данные о товаре ";
                        titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        titleParagraph.Range.Font.Size = 12;
                        titleParagraph.Range.InsertParagraphAfter();
                    }

                    else
                    {
                        // Добавляем заголовок
                        Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                        titleParagraph.Range.Text = "Данные о товаре";
                        titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        titleParagraph.Range.Font.Size = 12;
                        titleParagraph.Range.InsertParagraphAfter();
                    }

                    // Создаем таблицу
                    table2 = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count-4);
                    int m = 1;
                    int h = 1;
                    // Добавляем заголовки столбцов
                    for (int i = 1; i < dataGridView.Columns.Count; i++)
                    {
                        if (dataGridView.Columns[i].Visible == true)
                        {
                            table2.Cell(1, h ).Range.Text = dataGridView.Columns[i].HeaderText;
                            table2.Cell(1, h ).Range.Font.Bold = 1; // Заголовок жирный
                            table2.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(1, h ).Range.Font.Size = 8;
                            h++;
                        }
                    }

                    // Заполняем таблицу данными

                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            table2.Cell(2, m ).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                            table2.Cell(2, m ).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(2, m ).Range.Font.Size = 8;
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
                    Word.Paragraph titleParagraph2 = wordDoc.Content.Paragraphs.Add();
                titleParagraph2.Range.Text = "Данные о движениях товара";
                titleParagraph2.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph2.Range.Font.Size = 12;

                titleParagraph2.Range.InsertParagraphAfter();
                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("Ошибка: Нет данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
              


                    String sql8 = "SELECT " +
"   i.num_invoices AS invoice_number,  " +
"    i.shipment AS shipment_date,    " +
"   pc.code AS product_code,       " +
"    bn.number AS batch_number,        " +
"   ii.quantity AS quantity,                  " +
"   s.name AS storehouse_name,                " +
"  CASE " +
"      WHEN i.flag = 0 THEN 'Приходная' " +
"       WHEN i.flag = 1 THEN 'Расходная' " +
"      WHEN i.flag = 2 THEN 'Перемещение' " +
"   END AS invoice_type " +
"FROM " +
"    invoices_in_info ii " +
"JOIN " +
"    invoices_in i ON ii.invoices_in = i.id " +
"JOIN " +
"    batch_number bn ON ii.id_batch_number = bn.id " +
"JOIN " +
"    storehouse s ON i.id_storehouse = s.id " +
"JOIN " +
"    Product_card pc ON ii.id_Product_card = pc.id where pc.id=:code " +


"UNION ALL " +

"SELECT " +
"   m.num_invoices AS invoice_number,       " +
"   m.shipment AS shipment_date,          " +
"  pc.code AS product_code,     " +
"   bn.number AS batch_number,        " +
"  mi.quantity AS quantity,                 " +
"  s1.name AS storehouse_name,           " +
"  'Перемещение со склада' AS invoice_type  " +
"FROM " +
"   moving_info mi " +
"JOIN " +
"   moving m ON mi.invoices_in = m.id " +
"JOIN " +
"   batch_number bn ON mi.id_batch_number = bn.id " +
"JOIN " +
"  storehouse s1 ON m.id_storehouse_1 = s1.id " +
"JOIN " +
"   Product_card pc ON mi.id_Product_card = pc.id where pc.id=:code " +


"UNION ALL " +

"SELECT " +
"   m.num_invoices AS invoice_number,           " +
"   m.shipment AS shipment_date,           " +
"   pc.code AS product_code,       " +
"   bn.number AS batch_number,      " +
"   mi.quantity AS quantity,            " +
"  s2.name AS storehouse_name,             " +
"   'Перемещение на склад' AS invoice_type " +
"   " +
"FROM " +
"   moving_info mi " +
"JOIN " +
"  moving m ON mi.invoices_in = m.id " +
"JOIN " +
"   batch_number bn ON mi.id_batch_number = bn.id " +
"JOIN " +
"   storehouse s2 ON m.id_storehouse_2 = s2.id " +
"JOIN " +
"    Product_card pc ON mi.id_Product_card = pc.id where pc.id=:code ORDER BY shipment_date DESC";
                NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                    da8.SelectCommand.Parameters.AddWithValue("code", dataGridView1.CurrentRow.Cells[0].Value);
                    ds8.Reset();
                    da8.Fill(ds8);
                    dt8 = ds8.Tables[0];
                    // Вставка данных из DataGridView
                    if (dt8.Rows.Count > 0)

                    {// Проверяем, существует ли закладка
                     // Имя закладки соответствует имени столбца



                        table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 1, dt8.Columns.Count);
                        foreach (Word.Cell cell in table.Rows[1].Cells)
                        {
                            cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                        }
                 
                        table.Cell(1, 1).Range.Text = "Номер накладной";
                        table.Cell(1, 2).Range.Text = "Дата";
                        table.Cell(1, 3).Range.Text = "Код товара";
                        table.Cell(1, 4).Range.Text = "Номер партии";
                        table.Cell(1, 5).Range.Text = "Количество товара";
                        table.Cell(1, 6).Range.Text = "Склад";
                        table.Cell(1, 7).Range.Text = "Тип накладной";



                        //for (int i = 0; i < dataGridView.Rows.Count; i++)
                        //{
                        //    int m = 1;
                        //    for (int j = 1; j < dataGridView.Columns.Count; j++)
                        //    {
                        //        if (dataGridView.Columns[j].Visible == true)
                        //        {
                        //            table.Cell(i + 2, m).Range.Text = dataGridView.Rows[i].Cells[j].Value?.ToString();
                        //            table.Cell(i + 2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        //            table.Cell(i + 2, m).Range.Font.Size = 8;
                        //            m++;
                        //        }
                        //    }
                        //}




                       
                        for (int i = 0; i < dt8.Rows.Count; i++)
                        {
                            Word.Row newRow = table.Rows.Add();
                            for (int j = 0; j < dt8.Columns.Count; j++)
                            {
                                // Получаем значение ячейки
                                var cellValue = dt8.Rows[i][j]?.ToString();
                                newRow.Cells[j + 1].Range.Text = cellValue;
                                newRow.Cells[j + 1].Range.Font.Name = "Arial"; // Устанавливаем шрифт
                                newRow.Cells[j + 1].Range.Font.Size = 8;
                                //if (wordDoc.Bookmarks.Exists(bookmarkName))
                                //{
                                //    wordDoc.Bookmarks[bookmarkName].Range.Text = cellValue; // Вставляем значение в закладку
                                //}

                                ////Заменяем закладки в документе
                                //string bookmarkName_pro = dt8.Columns[j].ColumnName; // Пример имени закладки
                                //if (wordDoc.Bookmarks.Exists(bookmarkName_pro))
                                //{
                                //    wordDoc.Bookmarks[bookmarkName_pro].Range.Text = cellValue;
                                //    //newRow.Cells[j + 1].Range.Text = cellValue;
                                //}
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
                }
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
          
            
        }

        private void вExcelИнформациюВсехПартийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                    saveFileDialog.Title = "Сохранить файл Excel";
                    DateTime time = DateTime.Today.Date;

                    saveFileDialog.FileName = "Рroduct_cards_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToExcel_all(dataGridView1, saveFileDialog.FileName);
                    }
                }
            }
            catch { }
        }

        private void вExcelИнформациюВыбраннойПартииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ExportToExcel(dataGridView1, filePath);
                if (dataGridView1.CurrentRow != null)
                {

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";
                        saveFileDialog.Title = "Сохранить файл Excel";
                        DateTime time = DateTime.Today.Date;
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Рroduct_card_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToExcel(dataGridView1, saveFileDialog.FileName);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }

        private void информацияОКоличествеТовараНаСкладахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    int id_pro = (int)dataGridView1.CurrentRow.Cells[0].Value;


                   prod_in_sclad fp = new prod_in_sclad(con, id_pro, "");
                    fp.Show();
                }
                //        else
                //        {
                //            DialogResult result = MessageBox.Show("У выбранного товара нет партий. Хотите создать новую партию товара?", "Выполнение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //            if (result == DialogResult.Yes)
                //            {


                //                //textBox1.Visible = false;
                //                newbatch_number f = new newbatch_number(con, -1, comboBox1.Text, "", DateTime.Today, DateTime.Today, "", 0, id_pro_card, 0);
                //                f.ShowDialog();


                //                //checkBox1.Checked = true;

                //            }
                //            else { }

                //        }

            }
            catch { }
        }

        private void вWordИнформациюВсехТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {



                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Word Files|*.docx";
                    saveFileDialog.Title = "Сохранить файл Word";
                    saveFileDialog.FileName = "Рroduct_card_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToWord_all(dataGridView1, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    saveFileDialog.FileName = $"Product_cards_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Вызываем метод экспорта с выбранным путем
                        ExportJSON_all(dataGridView1, saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вJSONИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                        saveFileDialog.Title = "Сохраните файл JSON как";
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = $"Product_cards_{code}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Вызываем метод экспорта с выбранным путем
                            ExportToJSON(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вWordИнформациюВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Word Files|*.docx";
                        saveFileDialog.Title = "Сохранить файл Word";
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Рroduct_card_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWord(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Word Files|*.docx";
                        saveFileDialog.Title = "Сохранить файл Word";
                        string code = (string)dataGridView1.CurrentRow.Cells[1].Value;
                        saveFileDialog.FileName = "Рroduct_card_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            ExportToWordProduct(dataGridView1, saveFileDialog.FileName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для экспорта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
