using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using Npgsql;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace sclade
{
    public partial class prod_in_sclad : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public int id;

        public string name;

        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана

        public prod_in_sclad(NpgsqlConnection con, int id, string name)
        {
            this.id = id;

            this.name = name;

            this.con = con;
            InitializeComponent();
        }
        public void Update()
        {


            try
            {

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);



                //if (this.name != "")
                //{
                //    textBox1.Text = this.name;
                //}
                if (this.id != -1)
                {
                    String sql = "Select DISTINCT prod_store.id,storehouse.name, Product_card.name,Product_card.code,prod_store.count from storehouse,Product_card,prod_store where prod_store.count>0 and prod_store.id_store=storehouse.id and prod_store.id_product_card=Product_card.id and Product_card.id = " + this.id + " ORDER BY  prod_store.count ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название склада";
                    dataGridView1.Columns[2].Visible = false;
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].HeaderText = "Количество на складе";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                if (this.name != "")
                {
                    String sql = "Select DISTINCT prod_store.id,storehouse.name, Product_card.name,Product_card.code,prod_store.count from storehouse,Product_card,prod_store where prod_store.count>0 and prod_store.id_store=storehouse.id and prod_store.id_product_card=Product_card.id and Product_card.code = '" + this.name + "' ORDER BY  prod_store.count ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название склада";
                    dataGridView1.Columns[2].Visible = false;
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].HeaderText = "Количество на складе";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                if (dt.Rows.Count > 0)
                {
                    label1.Font = new Font("Arial", 11);
                    label1.Text = "Название товара: " + dt.Rows[0][2].ToString() + "\nКод товара: " + dt.Rows[0][3].ToString();

                }
            }
            catch { }
        }
        private void prod_in_sclad_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "";
                Update();

                dataGridView1.ReadOnly = true;
                
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
