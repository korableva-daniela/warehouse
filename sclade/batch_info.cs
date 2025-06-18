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
    public partial class batch_info : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public int id;
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public string number;
        public batch_info(NpgsqlConnection con, string number, int id)
        {
            this.id = id;
            this.number = number;
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
        private void batch_info_Load(object sender, EventArgs e)
        {
            //try {
            if (this.number != "" || this.id != -1)
            {
                richTextBox1.Font = new Font("Arial", 11);
                richTextBox2.Font = new Font("Arial", 11);
                richTextBox1.ReadOnly = true;
                richTextBox2.ReadOnly = true;
                if (this.number != "")
                {
                    String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from batch_number, Product_card,Firm,unit_of_measurement where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and batch_number.number= '";
                    sql += this.number;
                    sql += "'";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string id_pro_card = dt.Rows[0][2].ToString();
                        string name = dt.Rows[0][3].ToString();
                        string number = dt.Rows[0][1].ToString();
                        string id_Firm = dt.Rows[0][4].ToString();
                        DateTime release = (DateTime)dt.Rows[0][5];
                        DateTime last_expiration = (DateTime)dt.Rows[0][6];
                        string warranty = dt.Rows[0][7].ToString();

                        int col_pro = Convert.ToInt32(dt.Rows[0][8]);
                        string litter = dt.Rows[0][9].ToString();
                        string description = dt.Rows[0][11].ToString();
                        double price = Convert.ToInt32(dt.Rows[0][12]);
                        richTextBox2.Clear();
                        richTextBox2.AppendText("             Описание\n");
                        richTextBox2.AppendText("\n");
                        richTextBox2.AppendText(" " + description + "\n");
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
                if (this.id != -1)
                {
                    String sql = "Select  batch_number.id,batch_number.number,Product_card.code,Product_card.name, Firm.name_f, batch_number.release, batch_number.last_expiration,batch_number.warranty,   batch_number.col_pro,unit_of_measurement.litter,Product_card.id,Product_card.description,batch_number.price from batch_number, Product_card,Firm,unit_of_measurement where Product_card.id = batch_number.id_pro_card and Firm.id=batch_number.id_Firm  and batch_number.id= ";
                    sql += this.id;

                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        string id_pro_card = dt.Rows[0][2].ToString();
                        string name = dt.Rows[0][3].ToString();
                        string number = dt.Rows[0][1].ToString();
                        string id_Firm = dt.Rows[0][4].ToString();
                        DateTime release = (DateTime)dt.Rows[0][5];
                        DateTime last_expiration = (DateTime)dt.Rows[0][6];
                        string warranty = dt.Rows[0][7].ToString();

                        int col_pro = Convert.ToInt32(dt.Rows[0][8]);
                        string litter = dt.Rows[0][9].ToString();
                        string description = dt.Rows[0][11].ToString();
                        double price = Convert.ToInt32(dt.Rows[0][12]);
                        richTextBox2.Clear();
                        richTextBox2.AppendText("             Описание\n");
                        richTextBox2.AppendText("\n");
                        richTextBox2.AppendText(" " + description + "\n");
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
            else
            {
                MessageBox.Show("Партия не найдена.");
                Close();
            }
                
            //}catch { }
        
     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
