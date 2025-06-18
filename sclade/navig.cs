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
    public partial class navig : Form
    {
        public int stor;
        public NpgsqlConnection con;
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public int div;
        public navig(NpgsqlConnection con,  int stor,int div)
        {
            this.div = div;
            this.stor = stor;
       
        
            this.con = con;
            InitializeComponent();
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
        private void navig_Load(object sender, EventArgs e)
        {
            comboBox1.Font = new Font("Arial", 11);
            updatestorehouseinfo(this.stor);
            comboBox1.Enabled = false;
            label1.Font = new Font("Arial", 11);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.stor != -1)
            {
                firm_client fp = new firm_client(con, -1, "", this.stor,div);
                fp.ShowDialog();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.stor != -1)
            {
                firm_firm fp = new firm_firm(con, -1, "", this.stor,div);
                fp.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
