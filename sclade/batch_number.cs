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
    public partial class batch_number : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public int id_c;
        public int id_pr_card;
        public string number;
        public int id_Firm;
        public int stor;
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        DataTable dt6 = new DataTable();
        DataSet ds6 = new DataSet();
        DataTable dt8 = new DataTable();
        DataSet ds8 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public int div;
        public batch_number(NpgsqlConnection con,int id_c,string number, int id_pr_card,int id_Firm, int stor,int div)
        {
            this.div = div;
            this.number = number;
            this.id_pr_card = id_pr_card;
            this.id_c = id_c;
            this.con = con;
            this.stor = stor;
            this.id_Firm = id_Firm;
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
            try
            {
                comboBox1.Enabled = false;
                comboBox1.Font = new Font("Arial", 11);
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
                if (id_c != 0)
                {
                    button3.Visible = false;


                }
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
                if (id_c == -2)
                {
                   
                    menuStrip1.Visible = false;
                   

                }
                if (this.number != "")
                {
                    textBox3.Text = this.number;
                    //textBox3.Enabled = false;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Font = new Font("Arial", 9);
                textBox1.Font = new Font("Arial", 11);
                textBox2.Font = new Font("Arial", 11);
                textBox3.Font = new Font("Arial", 11);
                textBox4.Font = new Font("Arial", 11);
                dataGridView1.ReadOnly = true;
                if (comboBox1.Text == "Склад не выбран")
                {
                    if ((id_c == -1) && (id_pr_card == -1) && (id_Firm == -1))
                    {
                        this.WindowState = FormWindowState.Maximized;
                        if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {

                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed ORDER BY  Product_card.code ASC; ";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.name ILIKE" +
                                " '";
                            sql += textBox2.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.number ILIKE '";

                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Firm.name_f ILIKE '";

                            sql += textBox4.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.code ILIKE '";

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
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.code ILIKE'";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and  Product_card.name ILIKE '";

                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";

                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                    }
                    if ((id_c > 0))
                    {



                        String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id= ";
                        sql += id_c.ToString();

                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);



                    }

                    if ((id_c < 1) && (id_pr_card != -1) && (id_Firm == -1))
                    {

                        if ((textBox3.Text == "") & (textBox4.Text == ""))
                        {

                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);
                        }

                        else if ((textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }





                    }

                    if ((id_c < 1) && (id_pr_card != -1) && (id_Firm != -1))
                    {
                        if ((textBox3.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and  batch_number.id_pro_card=";

                            sql += id_pr_card.ToString();
                            sql += " and batch_number.id_Firm = ";
                            sql += id_Firm.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);



                        }


                        else if ((textBox3.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and  batch_number.id_pro_card=";

                            sql += id_pr_card.ToString();
                            sql += " and batch_number.id_Firm = ";
                            sql += id_Firm.ToString();
                            sql += " and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }



                    }
                    if ((id_c < 1) && (id_pr_card == -1) && (id_Firm != -1))
                    {

                        if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == ""))
                        {

                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);
                        }

                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();

                            sql += " and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);


                        }

                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != ""))
                        {
                            String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from unit_of_measurement, batch_number, Product_card,Firm where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and batch_number.id_Firm =";

                            sql += id_Firm.ToString();

                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;

                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }





                    }

                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Номер партии";
                    dataGridView1.Columns[2].HeaderText = "Код товара";
                    dataGridView1.Columns[3].HeaderText = "Название товара";
                    dataGridView1.Columns[4].HeaderText = "Поставщик";
                    dataGridView1.Columns[5].HeaderText = "Дата и время выпуска";
                    dataGridView1.Columns[6].HeaderText = "Дата и время конца срока годности";

                    dataGridView1.Columns[7].HeaderText = "Гарантийный срок";

                    dataGridView1.Columns[8].HeaderText = "Количество товара";
                    dataGridView1.Columns[9].HeaderText = "Единица измерения";
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[12].HeaderText = "Цена за единицу товара";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                   
                    if ((id_c == -1) && (id_pr_card == -1) && (id_Firm == -1))
                    {
                        this.WindowState = FormWindowState.Maximized;
                        if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {

                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id ";

                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;"; 
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed   and prod_store.id_batch_number= batch_number.id and Product_card.name ILIKE" +
                                " '";
                            sql += textBox2.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and batch_number.number ILIKE '";

                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Firm.name_f ILIKE '";

                            sql += textBox4.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id  and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id  and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id  and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.code ILIKE'";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id  and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and  Product_card.name ILIKE '";

                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and Product_card.code ILIKE '";

                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";

                            sql += textBox2.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                    }
                    if ((id_c > 0))
                    {



                        String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and batch_number.id= ";
                        sql += id_c.ToString();
                      
                        sql += " and  prod_store.id_store = ";
                        sql += comboBox1.SelectedValue.ToString();
                        sql += " ORDER BY  Product_card.code ASC;";
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                        ds.Reset();
                        da.Fill(ds);



                    }

                    if ((id_c < 1) && (id_pr_card != -1) && (id_Firm == -1))
                    {

                        if ((textBox3.Text == "") & (textBox4.Text == ""))
                        {

                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);
                        }

                        else if ((textBox3.Text != "") & (textBox4.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox3.Text == "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id  and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox3.Text != "") & (textBox4.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and Product_card.id=";

                            sql += id_pr_card.ToString();
                            sql += " and Firm.name_f ILIKE '";
                            sql += textBox4.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }





                    }

                    if ((id_c < 1) && (id_pr_card != -1) && (id_Firm != -1))
                    {
                        if ((textBox3.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and  batch_number.id_pro_card=";

                            sql += id_pr_card.ToString();
                            sql += " and batch_number.id_Firm = ";
                            sql += id_Firm.ToString();
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);



                        }


                        else if ((textBox3.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and  batch_number.id_pro_card=";

                            sql += id_pr_card.ToString();
                            sql += " and batch_number.id_Firm = ";
                            sql += id_Firm.ToString();
                            sql += " and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }



                    }
                    if ((id_c < 1) && (id_pr_card == -1) && (id_Firm != -1))
                    {

                        if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text == ""))
                        {

                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed and prod_store.id_batch_number= batch_number.id  and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);
                        }

                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }
                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id  and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text == "") & (textBox2.Text == "") & (textBox3.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and batch_number.id_Firm =";

                            sql += id_Firm.ToString();

                            sql += " and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text == ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);


                        }

                        else if ((textBox1.Text != "") & (textBox2.Text == "") & (textBox3.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm and unit_of_measurement.id= batch_number.id_ed   and prod_store.id_batch_number= batch_number.id and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";
                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text == "") & (textBox2.Text != "") & (textBox3.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and batch_number.id_Firm =";

                            sql += id_Firm.ToString();
                            sql += " and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;
                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }

                        else if ((textBox1.Text != "") & (textBox2.Text != "") & (textBox3.Text != ""))
                        {
                            String sql = "Select DISTINCT batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price, prod_store.count_id_batch, storehouse.name from unit_of_measurement, prod_store,   batch_number, Product_card,Firm,storehouse where storehouse.id = prod_store.id_store and Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and unit_of_measurement.id= batch_number.id_ed  and prod_store.id_batch_number= batch_number.id and batch_number.id_Firm =";

                            sql += id_Firm.ToString();

                            sql += " and Product_card.code ILIKE '";
                            sql += textBox1.Text;
                            sql += "%' and Product_card.name ILIKE '";
                            sql += textBox2.Text; ;

                            sql += "%' and batch_number.number ILIKE '";
                            sql += textBox3.Text;
                            sql += "%' ";
                            sql += " and  prod_store.id_store = ";
                            sql += comboBox1.SelectedValue.ToString();
                            sql += " ORDER BY  Product_card.code ASC;";

                            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                            ds.Reset();
                            da.Fill(ds);

                        }





                    }

                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Номер партии";
                    dataGridView1.Columns[2].HeaderText = "Код товара";
                    dataGridView1.Columns[3].HeaderText = "Название товара";
                    dataGridView1.Columns[4].HeaderText = "Поставщик";
                    dataGridView1.Columns[5].HeaderText = "Дата и время выпуска";
                    dataGridView1.Columns[6].HeaderText = "Дата и время конца срока годности";

                    dataGridView1.Columns[7].HeaderText = "Гарантийный срок";

                    dataGridView1.Columns[8].HeaderText = "Общее количество товара";
                    dataGridView1.Columns[9].HeaderText = "Единица измерения";
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[11].Visible = false;
                    dataGridView1.Columns[12].HeaderText = "Цена за единицу товара";
                    dataGridView1.Columns[13].HeaderText = "Количество товара данной партии на складе";
                    dataGridView1.Columns[14].HeaderText = "Склад";
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                    if ((id_c > 0))
                {
                    try
                    {
                        if (dataGridView1.RowCount > 1)
                        {
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            textBox3.Enabled = false;
                            textBox4.Enabled = false;

                            textBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                            textBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                            textBox3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                        }
                    }
                    catch { }

                }

                if ((id_c < 1) && (id_pr_card != -1) && (id_Firm == -1))
                {
                    try
                    {
                        if (dataGridView1.RowCount > 1)
                        {
                            textBox3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                            textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                            textBox3.Enabled = false;
                            textBox4.Enabled = false;
                        }
                    }
                    catch { }
                }

                if ((id_c < 1) && (id_pr_card != -1) && (id_Firm != -1))
                {
                    try
                    {
                        if (dataGridView1.RowCount > 1)
                        {
                            textBox1.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                            textBox2.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();

                            textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            textBox4.Enabled = false;
                        }
                    }
                    catch { }

                }
                if ((id_c < 1) && (id_pr_card == -1) && (id_Firm != -1))
                {
                    try
                    {
                        if (dataGridView1.RowCount > 1)
                        {
                            textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                            textBox4.Enabled = false;
                        }
                    }

                    catch { }
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

                        int id_n = (int)dataGridView1.CurrentRow.Cells[0].Value;

                        string id_pro_card = (string)dataGridView1.CurrentRow.Cells[2].Value;
                            string name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                            string number = (string)dataGridView1.CurrentRow.Cells[1].Value;
                            string id_Firm = (string)dataGridView1.CurrentRow.Cells[4].Value;
                            DateTime release = (DateTime)dataGridView1.CurrentRow.Cells[5].Value;
                        DateTime last_expiration = (DateTime)dataGridView1.CurrentRow.Cells[6].Value;
                        string warranty = (string)dataGridView1.CurrentRow.Cells[7].Value;
                  
                        int col_pro = (int)dataGridView1.CurrentRow.Cells[8].Value;
                            string description = (string)dataGridView1.CurrentRow.Cells[10].Value;
                            double price = (double)dataGridView1.CurrentRow.Cells[11].Value;
                            richTextBox2.Clear();
                            richTextBox2.Text= description;
                            richTextBox1.Clear();
                        richTextBox1.AppendText("             Партия\n");
                        richTextBox1.AppendText("\n");
                     
                        richTextBox1.AppendText("Код товара: " + id_pro_card + "\n");
                            richTextBox1.AppendText("Название товара: " + name + "\n");
                            richTextBox1.AppendText("Номер партии: " + number + "\n");
                            richTextBox1.AppendText("Поставщик: " + id_Firm + "\n");
                            richTextBox1.AppendText("Дата и время выпуска: " + release + "\n");
                        richTextBox1.AppendText("Дата и время конца срока годности: " + last_expiration + "\n");
                        richTextBox1.AppendText("Гарантийный срок: " + warranty + "\n");
                        richTextBox1.AppendText("Количество товара: " + col_pro + "\n");
                           
                            richTextBox1.AppendText("Цена за единицу товара: " + price + "\n");
                        }
                        try
                        {
                            if (dataGridView1.CurrentRow.Index == 0)
                            {
                                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                                {
                                    //String sql1 = "Select * from Employee  ORDER BY id DESC LIMIT 1 ;";
                                    //NpgsqlDataAdapter da6 = new NpgsqlDataAdapter(sql1, con);
                                    //ds6.Reset();
                                    //da6.Fill(ds6);
                                    //dt6 = ds6.Tables[0];
                                    //if (dt6.Rows.Count > 0)
                                    //{
                                    //    id = Convert.ToInt32(dt6.Rows[0]["id"]);

                                    //}
                                    //else { id = -1; }
                                    int id_n = (int)dataGridView1.CurrentRow.Cells[0].Value;

                                    string id_pro_card = (string)dataGridView1.CurrentRow.Cells[2].Value;
                                    string name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                                    string number = (string)dataGridView1.CurrentRow.Cells[1].Value;
                                    string id_Firm = (string)dataGridView1.CurrentRow.Cells[4].Value;
                                    DateTime release = (DateTime)dataGridView1.CurrentRow.Cells[5].Value;
                                    DateTime last_expiration = (DateTime)dataGridView1.CurrentRow.Cells[6].Value;
                                    string warranty = (string)dataGridView1.CurrentRow.Cells[7].Value;

                                    int col_pro = (int)dataGridView1.CurrentRow.Cells[8].Value;
                                    string litter = (string)dataGridView1.CurrentRow.Cells[9].Value;
                                    string description = (string)dataGridView1.CurrentRow.Cells[11].Value;
                                    double price = (double)dataGridView1.CurrentRow.Cells[12].Value;
                                    richTextBox2.Clear();
                                    richTextBox2.Text = description;
                                    richTextBox1.Clear();
                                    richTextBox1.AppendText("             Партия\n");
                                    richTextBox1.AppendText("\n");

                                    richTextBox1.AppendText("Код товара: " + id_pro_card + "\n");
                                    richTextBox1.AppendText("Название товара: " + name + "\n");
                                    richTextBox1.AppendText("Номер партии: " + number + "\n");
                                    richTextBox1.AppendText("Поставщик: " + id_Firm + "\n");
                                    richTextBox1.AppendText("Дата и время выпуска: " + release + "\n");
                                    richTextBox1.AppendText("Дата и время конца срока годности: " + last_expiration + "\n");
                                    richTextBox1.AppendText("Гарантийный срок: " + warranty + "\n");
                                    richTextBox1.AppendText("Количество товара: " + col_pro + "\n");
                                    richTextBox1.AppendText("Единица измерения: " + litter + "\n");
                                    richTextBox1.AppendText("Цена за единицу товара: " + price + "\n");
                                }
                            }
                        }
                        catch { }
                }
                else richTextBox1.Text = " ";
                // else richTextBox1.Text =" ";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else richTextBox1.Text = " ";
            }
            catch { }

        }
        private void batch_number_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;
            Update();
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                if (this.stor != -1)
                {
                    newbatch_number f = new newbatch_number(con, -1, "", "", DateTime.Today, DateTime.Today, "", 0, "", -1, 0, "", this.stor, this.div);
                    f.ShowDialog();
                }else {
                    newbatch_number f = new newbatch_number(con, -1, "", "", DateTime.Today, DateTime.Today, "", 0, "", -1, 0, "", -1, this.div);
                    f.ShowDialog();
                }
            Update();
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                //if (id != -1)
                //{
                //    description(id);
                //}
            }
                Update();
            }

            catch { }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    try
                    {
                        int id_n = (int)dataGridView1.CurrentRow.Cells[0].Value;

     
                string name = (string)dataGridView1.CurrentRow.Cells[3].Value;
                string number = (string)dataGridView1.CurrentRow.Cells[1].Value;
                string id_Firm = (string)dataGridView1.CurrentRow.Cells[4].Value;
                DateTime release = (DateTime)dataGridView1.CurrentRow.Cells[5].Value;
            DateTime last_expiration = (DateTime)dataGridView1.CurrentRow.Cells[6].Value;
            string warranty = (string)dataGridView1.CurrentRow.Cells[7].Value;

            int col_pro = (int)dataGridView1.CurrentRow.Cells[8].Value;

            int id_pro_card = (int)dataGridView1.CurrentRow.Cells[10].Value;
                string litter = (string)dataGridView1.CurrentRow.Cells[9].Value;
                string description = (string)dataGridView1.CurrentRow.Cells[11].Value;
                double price = (double)dataGridView1.CurrentRow.Cells[12].Value;
                newbatch_number f = new newbatch_number(con, id_n, name, number, release, last_expiration, warranty, col_pro, litter, id_pro_card, price, id_Firm,this.stor, this.div);
            f.ShowDialog();
            Update();
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    //if (id != -1)
                    //{
                    //    description(id);

                }
                Update();
            }
            catch { }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
                    {
                        try
                        {
                            int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM batch_number WHERE id=:id", con);

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
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
                            {
                                try
                                {
                                    Firm_in fp = new Firm_in(con);
            fp.ShowDialog();
                }
                catch { }
            }

        private void button1_Click(object sender, EventArgs e)
                                {

        }

        private void button4_Click(object sender, EventArgs e)
                                    {

        }

        private void button5_Click(object sender, EventArgs e)
                                        {
                                            try
                                            {
                                                country_of_origin_in fp = new country_of_origin_in(con, -1, "");
            fp.ShowDialog();
                }
                catch { }
            }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
                                            {
                                               
                //if (dataGridView1.CurrentRow != null)
                //{
                //    int id_c = (int)dataGridView1.CurrentRow.Cells[9].Value;
                //    product_card_in fp = new product_card_in(con, id_c);
                //    fp.ShowDialog();
                //}
                //}
                //catch { }
          
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                int id_ = (int)dataGridView1.CurrentRow.Cells[0].Value;
                string number_ = (string)dataGridView1.CurrentRow.Cells[1].Value;

                this.number = number_;
                this.id_c = id_;
                Close();
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.number = textBox3.Text;
            Update();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            filter fp = new filter(con,div);
            fp.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void посмотретьИнформациюОПоставщикеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[4].Value != null)
                {
                    string id_Firm = (string)dataGridView1.CurrentRow.Cells[4].Value;


                    firm fp = new firm(con,-1, id_Firm);
                    fp.ShowDialog();
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
            //}
        }
            catch { }
        }
        public void updatestorehouseinfo(int id_s)
        {
            try
            {
                String sql3 = "Select * from storehouse where  id=";
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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {


                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name,div,"");
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            updatestorehouseinfo(-1);
            comboBox1.Text = "Склад не выбран";
            this.stor = -1;
            Update();
        }

        private void информацияОТовареToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[2].Value != null)
                {

                    string id_pro = (string)dataGridView1.CurrentRow.Cells[2].Value;



                    prod_info fp = new prod_info(con, id_pro,-1);
                    fp.ShowDialog();
                }
            }
            catch { }
        }

        private void информацияОДвиженияхТовараToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView1.CurrentRow.Cells[2].Value != null)
                {

                    string id_pro = (string)dataGridView1.CurrentRow.Cells[2].Value;


                    if (this.stor != -1)
                    {
                        mov_pro fp = new mov_pro(con, this.stor, id_pro, -1, -1,div);
                        fp.ShowDialog();
                    }
                    else
                    {
                        mov_pro fp = new mov_pro(con, -1, id_pro, -1, -1,div);
                        fp.ShowDialog();
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

            // Создаем новую книгу
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
            int h = 1;
            // Записываем заголовки столбцов
            //if (comboBox1.SelectedValue == null)
            //{
            for (int i = 1; i < dataGridView.Columns.Count; i++)

            {
                if (i == 11 || i == 10)
                {
                    if (i == 11)
                    {
                        //worksheet.Cells[1, i] = dataGridView.Columns[i + 2].HeaderText;
                        //i += 1;

                    }
                    

                }


                else
                {


                    worksheet.Cells[1, h] = dataGridView.Columns[i].HeaderText;
                    h++;
                }
            }
            //}




            if (dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                // Записываем данные
                //for (int i = 0; i < dataGridView.Rows.Count; i++)
                //{
                int m = 1;
                for (int j = 1; j < dataGridView.Columns.Count; j++)
                {
                    if (j == 11 || j == 10)
                    {
                        //if (j == 11)
                        //{
                        //    //worksheet.Cells[1, i] = dataGridView.Columns[i + 2].HeaderText;
                        //    //i += 1;

                        //}

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
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    int m = 1;
                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (j == 11 || j == 10)
                        {
                            //if (j == 11)
                            //{
                            //    //worksheet.Cells[1, i] = dataGridView.Columns[i + 2].HeaderText;
                            //    //i += 1;

                            //}

                        }


                        else
                        {


                            worksheet.Cells[i + 2, m] = dataGridView.Rows[i].Cells[j].Value?.ToString();
                            m++;
                        }

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
                    if (i == 11  || i == 10)
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
                    if (j == 11 || j == 10)
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
                titleParagraph.Range.Text = "Партии";
                titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                titleParagraph.Range.Font.Size = 8;

                titleParagraph.Range.InsertParagraphAfter();


                // Создаем таблицу
                table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, dataGridView.Rows.Count + 1, dataGridView.Columns.Count - 3);

                int h = 1;
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
            Word.Table table2 = null;
            try
            {
                if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    // Создаем новый экземпляр Word
                    wordApp = new Word.Application();
                    wordDoc = wordApp.Documents.Add();
                 
                    Word.Paragraph titleParagraph2 = wordDoc.Content.Paragraphs.Add();
                    titleParagraph2.Range.Text = "Данные о товаре";
                    titleParagraph2.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph2.Range.Font.Size = 12;

                    titleParagraph2.Range.InsertParagraphAfter();
                    if (dataGridView.Rows.Count == 0)
                    {
                        MessageBox.Show("Ошибка: Нет данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создаем таблицу




                    String sql8 = "Select Product_card.code as cod_pro,Product_card.name as name_pro,Type_to.name ,Product_card.name_firm as name_firm,Product_card.col_pro as col_pro, unit_of_measurement.litter as u_litter,unit_of_measurement.code as u_code,country_of_origin.litter as litter, country_of_origin.code as code,Product_card.numgtd as numgtd,Product_card.numrnpt as numrnpt,NDS.percent as percent,Product_card.numexcise as numexcise,Product_card.numegis as numegis" +
                              " from Type_to, Product_card, unit_of_measurement, country_of_origin, NDS" +
                              " where Type_to.id = Product_card.id_type  and Product_card.id_ed = unit_of_measurement.id and Product_card.id_coun = country_of_origin.id and" +
                               " Product_card.id_nds = NDS.id and Product_card.code = :code";
                    NpgsqlDataAdapter da8 = new NpgsqlDataAdapter(sql8, con);
                    da8.SelectCommand.Parameters.AddWithValue("code", (string)dataGridView1.CurrentRow.Cells[2].Value);
                    ds8.Reset();
                    da8.Fill(ds8);
                    dt8 = ds8.Tables[0];
                    // Вставка данных из DataGridView
                    if (dt8.Rows.Count > 0)

                    {// Проверяем, существует ли закладка
                     // Имя закладки соответствует имени столбца



                        table2 = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 1, dt8.Columns.Count);
                        foreach (Word.Cell cell in table2.Rows[1].Cells)
                        {
                            cell.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            cell.Range.Font.Size = 8; // Устанавливаем размер шрифта
                        }

                        table2.Cell(1, 1).Range.Text = "Код товара";
                        table2.Cell(1, 2).Range.Text = "Название товара";
                        table2.Cell(1, 3).Range.Text = "Тип";
                        table2.Cell(1, 4).Range.Text = "Производитель";
                        table2.Cell(1, 5).Range.Text = "Количество";
                        table2.Cell(1, 6).Range.Text = "Базовая единица измерения";
                        table2.Cell(1, 7).Range.Text = "код по ОКЕИ";
                        table2.Cell(1, 8).Range.Text = "Страна производитель";
                        table2.Cell(1, 9).Range.Text = "Код страны производителя";
                        table2.Cell(1, 10).Range.Text = "Номер ГТД";
                        table2.Cell(1, 11).Range.Text = "Номер РНПТ";
                        table2.Cell(1, 12).Range.Text = "НДС";
                        table2.Cell(1, 13).Range.Text = "Номер ставка акциза";
                        table2.Cell(1, 14).Range.Text = "Номер ЕГАИС.";







                        Word.Row newRow = table2.Rows.Add();
                        for (int j = 0; j < dt8.Columns.Count; j++)
                        {
                            // Получаем значение ячейки
                            var cellValue = dt8.Rows[0][j]?.ToString();
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


                        foreach (Word.Row row in table2.Rows)
                        {
                            foreach (Word.Cell cell in row.Cells)
                            {
                                cell.Borders.Enable = 1; // Включаем рамки для каждой ячейки
                            }
                        }
                    }
                        // Добавляем заголовок
                        Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                        titleParagraph.Range.Text = "Партия";
                        titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        titleParagraph.Range.Font.Size = 12;

                        titleParagraph.Range.InsertParagraphAfter();

                        int m = 1;
                        int h = 1;
                        // Создаем таблицу
                        table = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count - 3);

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
                    wordApp.Visible = true;
                    if (comboBox1.Text == "Склад не выбран")
                    {
                        // Добавляем заголовок
                        Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                        titleParagraph.Range.Text = "Данные о партии ";
                        titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        titleParagraph.Range.Font.Size = 8;
                        titleParagraph.Range.InsertParagraphAfter();
                    }

                    else
                    {
                        // Добавляем заголовок
                        Word.Paragraph titleParagraph = wordDoc.Content.Paragraphs.Add();
                        titleParagraph.Range.Text = "Данные о партии";
                        titleParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        titleParagraph.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                        titleParagraph.Range.Font.Size = 8;
                        titleParagraph.Range.InsertParagraphAfter();
                    }

                    // Создаем таблицу
                    table2 = wordDoc.Tables.Add(wordDoc.Bookmarks["\\endofdoc"].Range, 2, dataGridView.Columns.Count - 3);
                    int m = 1;
                    int h = 1;
                    // Добавляем заголовки столбцов
                    for (int i = 1; i < dataGridView.Columns.Count; i++)
                    {
                        if (dataGridView.Columns[i].Visible == true)
                        {
                            table2.Cell(1, h).Range.Text = dataGridView.Columns[i].HeaderText;
                            table2.Cell(1, h).Range.Font.Bold = 1; // Заголовок жирный
                            table2.Cell(1, h).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(1, h).Range.Font.Size = 8;
                            h++;
                        }
                    }

                    // Заполняем таблицу данными

                    for (int j = 1; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Columns[j].Visible == true)
                        {
                            table2.Cell(2, m).Range.Text = dataGridView.Rows[dataGridView1.CurrentRow.Index].Cells[j].Value?.ToString();
                            table2.Cell(2, m).Range.Font.Name = "Arial"; // Устанавливаем шрифт
                            table2.Cell(2, m).Range.Font.Size = 8;
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
                    titleParagraph2.Range.Text = "Данные о движениях партии";
                    titleParagraph2.Range.Font.Name = "Arial"; // Устанавливаем шрифт
                    titleParagraph2.Range.Font.Size = 12;

                    titleParagraph2.Range.InsertParagraphAfter();
                    if (dataGridView.Rows.Count == 0)
                    {
                        MessageBox.Show("Ошибка: Нет данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                    String sql8 = "SELECT " +
"     i.id AS id, i.num_invoices AS invoice_number,  " +
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
"    Product_card pc ON ii.id_Product_card = pc.id where i.id=:code " +


"UNION ALL " +

"SELECT " +
"    m.id AS id,  m.num_invoices AS invoice_number,       " +
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
"   Product_card pc ON mi.id_Product_card = pc.id where m.id=:code " +


"UNION ALL " +

"SELECT " +
"   m.id AS id, m.num_invoices AS invoice_number,           " +
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
"    Product_card pc ON mi.id_Product_card = pc.id where m.id=:code ORDER BY shipment_date DESC";
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
                        table.Cell(1,6).Range.Text = "Склад";
                        table.Cell(1,7).Range.Text = "Тип накладной";



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
                            for (int j = 1; j < dt8.Columns.Count; j++)
                            {
                                // Получаем значение ячейки
                                var cellValue = dt8.Rows[i][j]?.ToString();
                                newRow.Cells[j +1].Range.Text = cellValue;
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
        private void button8_Click(object sender, EventArgs e)
                {
                    
        }

        private void button1_Click_2(object sender, EventArgs e)
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

                    saveFileDialog.FileName = "Batch_numbers_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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
                        saveFileDialog.FileName = "Batch_number_" + code + "_" + DateTime.Today.Date.Day.ToString() + "_" + DateTime.Today.Date.Month.ToString() + "_" + DateTime.Today.Date.Year.ToString() + ".xlsx"; // Имя файла по умолчанию

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

        private void информацияОбОстаткахПартийПоСкладамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridView1.CurrentRow.Cells[0].Value != null)
                {
                    int id_batch = (int)dataGridView1.CurrentRow.Cells[0].Value;


                    batch_in_prod fp = new batch_in_prod(con, id_batch, "");
                    fp.ShowDialog();
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

        private void информацияОбОстатковТовараПоСкладамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {


                if (dataGridView1.CurrentRow.Cells[10].Value != null)
                {
                    int id_pro = (int)dataGridView1.CurrentRow.Cells[10].Value;


                    prod_in_sclad fp = new prod_in_sclad(con, id_pro, "");
                    fp.ShowDialog();
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
                    saveFileDialog.FileName = "Batch_numbers_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

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

        private void вJSONИнформациюВсехТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                    saveFileDialog.Title = "Сохраните файл JSON как";
                    saveFileDialog.FileName = $"Batch_numbers_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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
                        saveFileDialog.FileName = $"Batch_numbers_{code}_{DateTime.Today.Day}_{DateTime.Today.Month}_{DateTime.Today.Year}.json";

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
                        saveFileDialog.FileName = "Batch_number_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

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

        private void вWordИнформациюОПередвиженияхВыбраннойПартииToolStripMenuItem_Click(object sender, EventArgs e)
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
                        saveFileDialog.FileName = "Batch_number_" + code.Replace(" ", "_") + "_" + DateTime.Today.ToString("dd_MM_yyyy") + ".docx"; // Имя файла по умолчанию

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
