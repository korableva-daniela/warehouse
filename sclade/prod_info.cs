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
    public partial class prod_info : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
 
        public string code;
        public int id;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public prod_info(NpgsqlConnection con, string code,int id)
        {
            this.id = id;
            this.code = code;
            InitializeComponent();
            this.con = con;
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
        private void prod_info_Load(object sender, EventArgs e)
        {
            if (this.code != "" || this.id != -1)
            {
                if (this.code != "")
                {
                    richTextBox1.Font = new Font("Arial", 11);
                    richTextBox2.Font = new Font("Arial", 11);
                    richTextBox1.ReadOnly = true;
                    richTextBox2.ReadOnly = true;
                    String sql = "Select Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro, unit_of_measurement.litter,country_of_origin.litter," +
                    "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                    "  from Type_to,Product_card ,unit_of_measurement ,country_of_origin ,NDS  " +
                    "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                    " Product_card.id_nds = NDS.id and Product_card.code = '";
                    sql += this.code;
                    sql += "' ORDER BY Product_card.code ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {



                        string code = dt.Rows[0][1].ToString();
                        string pro = dt.Rows[0][2].ToString();
                        string name_type = dt.Rows[0][3].ToString();
                        string name_firm = dt.Rows[0][4].ToString();
                        //string firm = (string)dataGridView1.CurrentRow.Cells[5].Value;
                        int col = Convert.ToInt32(dt.Rows[0][5]);
                        string ed = dt.Rows[0][6].ToString();
                        string coun = dt.Rows[0][7].ToString();
                        string gtd = dt.Rows[0][8].ToString();
                        string rnpt = dt.Rows[0][9].ToString();
                        int nds = Convert.ToInt32(dt.Rows[0][10]);
                        string code_post = dt.Rows[0][11].ToString();
                        double pr_post = Convert.ToInt32(dt.Rows[0][12]);
                        string ak = dt.Rows[0][13].ToString();
                        string egis = dt.Rows[0][14].ToString();
                        string description = dt.Rows[0][15].ToString();
                        richTextBox2.Clear();
                        richTextBox2.AppendText("             Описание\n");
                        richTextBox2.AppendText("\n");
                        richTextBox2.AppendText(" " + description + "\n");
                        richTextBox1.Clear();
                        richTextBox1.AppendText("             Карточка товара\n");
                        richTextBox1.AppendText("\n");

                        richTextBox1.AppendText("Код товара: " + code + "\n");
                        richTextBox1.AppendText("Название товара: " + pro + "\n");
                        richTextBox1.AppendText("Тип товара: " + name_type + "\n");
                        richTextBox1.AppendText("Название фирмы товара: " + name_firm + "\n");
                        richTextBox1.AppendText("Количество: " + col + "\n");
                        richTextBox1.AppendText("Единица измерения: " + ed + "\n");
                        richTextBox1.AppendText("Страна производитель: " + coun + "\n");
                        //richTextBox1.AppendText("Поставщик: " + firm + "\n");
                        //richTextBox1.AppendText("Код товара от поставщика: " + code_post + "\n");
                        //richTextBox1.AppendText("Цена товара от поставщика: " + pr_post.ToString() + "\n");
                        richTextBox1.AppendText("НДС: " + nds.ToString() + "\n");
                        richTextBox1.AppendText("ГТД: " + gtd + "\n");
                        richTextBox1.AppendText("РНПТ: " + rnpt + "\n");
                        richTextBox1.AppendText("Ставка акциза: " + ak + "\n");
                        richTextBox1.AppendText("ЕГАИС: " + egis + "\n");
                    }
                }
                if (this.id != -1)
                {
                    richTextBox1.Font = new Font("Arial", 11);
                    richTextBox2.Font = new Font("Arial", 11);
                    richTextBox1.ReadOnly = true;
                    richTextBox2.ReadOnly = true;
                    String sql = "Select Product_card.id,Product_card.code,Product_card.name,Type_to.name,Product_card.name_firm,Product_card.col_pro, unit_of_measurement.litter,country_of_origin.litter," +
                    "Product_card.numgtd,Product_card.numrnpt,NDS.percent,Product_card.code_firm_pro,Product_card.price_firm_pro,Product_card.numexcise,Product_card.numegis,Product_card.description" +
                    "  from Type_to,Product_card ,unit_of_measurement ,country_of_origin ,NDS  " +
                    "where Type_to.id = Product_card.id_type  and Product_card.id_ed =unit_of_measurement.id and Product_card.id_coun =country_of_origin.id and" +
                    " Product_card.id_nds = NDS.id and Product_card.id = ";
                    sql += this.id;
                    sql += " ORDER BY Product_card.code ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {



                        string code = dt.Rows[0][1].ToString();
                        string pro = dt.Rows[0][2].ToString();
                        string name_type = dt.Rows[0][3].ToString();
                        string name_firm = dt.Rows[0][4].ToString();
                        //string firm = (string)dataGridView1.CurrentRow.Cells[5].Value;
                        int col = Convert.ToInt32(dt.Rows[0][5]);
                        string ed = dt.Rows[0][6].ToString();
                        string coun = dt.Rows[0][7].ToString();
                        string gtd = dt.Rows[0][8].ToString();
                        string rnpt = dt.Rows[0][9].ToString();
                        int nds = Convert.ToInt32(dt.Rows[0][10]);
                        string code_post = dt.Rows[0][11].ToString();
                        double pr_post = Convert.ToInt32(dt.Rows[0][12]);
                        string ak = dt.Rows[0][13].ToString();
                        string egis = dt.Rows[0][14].ToString();
                        string description = dt.Rows[0][15].ToString();
                        richTextBox2.Clear();
                        richTextBox2.AppendText("             Описание\n");
                        richTextBox2.AppendText("\n");
                        richTextBox2.AppendText(" " + description + "\n");
                        richTextBox1.Clear();
                        richTextBox1.AppendText("             Карточка товара\n");
                        richTextBox1.AppendText("\n");

                        richTextBox1.AppendText("Код товара: " + code + "\n");
                        richTextBox1.AppendText("Название товара: " + pro + "\n");
                        richTextBox1.AppendText("Тип товара: " + name_type + "\n");
                        richTextBox1.AppendText("Название фирмы товара: " + name_firm + "\n");
                        richTextBox1.AppendText("Количество: " + col + "\n");
                        richTextBox1.AppendText("Единица измерения: " + ed + "\n");
                        richTextBox1.AppendText("Страна производитель: " + coun + "\n");
                        //richTextBox1.AppendText("Поставщик: " + firm + "\n");
                        //richTextBox1.AppendText("Код товара от поставщика: " + code_post + "\n");
                        //richTextBox1.AppendText("Цена товара от поставщика: " + pr_post.ToString() + "\n");
                        richTextBox1.AppendText("НДС: " + nds.ToString() + "\n");
                        richTextBox1.AppendText("ГТД: " + gtd + "\n");
                        richTextBox1.AppendText("РНПТ: " + rnpt + "\n");
                        richTextBox1.AppendText("Ставка акциза: " + ak + "\n");
                        richTextBox1.AppendText("ЕГАИС: " + egis + "\n");
                    }
                }
            }
            else
            {
                MessageBox.Show("Товар не найден.");
                Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
