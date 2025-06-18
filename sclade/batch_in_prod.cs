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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace sclade
{
    public partial class batch_in_prod : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        DataTable dti = new DataTable();
        DataSet dsi = new DataSet();
        public int id;
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        string st;
        public string name;

        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана

        public batch_in_prod(NpgsqlConnection con, int id, string name)
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
                    //try
                    //{
                    //    String sql3 = "Select * from batch_number where id=";
                    //    sql3 += id.ToString();

                    //    NpgsqlDataAdapter da3 = new NpgsqlDataAdapter(sql3, con);
                    //    ds3.Reset();
                    //    da3.Fill(ds3);
                    //    dt3 = ds3.Tables[0];

                       
                    //    if (dt3.Rows.Count > 0)
                    //    {
                    //        st = dt3.Rows[0]["number"].ToString();

                    //    }
                    //    this.StartPosition = FormStartPosition.CenterScreen;
                    //    label1.Text += st;
                    //}
                    //catch (Exception ex) { MessageBox.Show(ex.Message); }
                    String sql = "Select DISTINCT prod_store.id,storehouse.name, Product_card.code,batch_number.number AS number,prod_store.count_id_batch from storehouse,Product_card,prod_store,batch_number where prod_store.count>0 and prod_store.id_store=storehouse.id and prod_store.id_product_card=Product_card.id and batch_number.id = prod_store.id_batch_number  and batch_number.id = " + this.id + "  ORDER BY  prod_store.count_id_batch ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название склада";
                    dataGridView1.Columns[2].HeaderText = "Код товара";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].HeaderText = "Количество на складе";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                if (this.name != "")
                {
                    String sql = "Select DISTINCT prod_store.id,storehouse.name, Product_card.code,batch_number.number AS number ,prod_store.count_id_batch from storehouse,Product_card,prod_store,batch_number where prod_store.count>0 and prod_store.id_store=storehouse.id and prod_store.id_product_card=Product_card.id and batch_number.id = prod_store.id_batch_number and batch_number.number = '" + this.name + "' ORDER BY prod_store.count_id_batch ASC;";
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                    ds.Reset();
                    da.Fill(ds);
                    dt = ds.Tables[0];
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].HeaderText = "Название склада";
                    dataGridView1.Columns[2].HeaderText = "Код товара";
                    //dataGridView1.Columns[3].HeaderText = "ФИО представителя";
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].HeaderText = "Количество на складе";

                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                if (dt.Rows.Count > 0)
                {
                    label1.Font = new Font("Arial", 11);
                    label1.Text = "Номер партии: " + dt.Rows[0]["number"].ToString();
                }
            }
            catch { }
        }
        private void batch_in_prod_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text = "Номер партии = ";
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
