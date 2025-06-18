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
    public partial class per_acc_ass_4 : Form
    {
        public NpgsqlConnection con;
        public int id_em;
        DataTable dt9 = new DataTable();
        DataSet ds9 = new DataSet();
        DataTable dt3 = new DataTable();
        DataSet ds3 = new DataSet();
        DataTable dt4 = new DataTable();
        DataSet ds4 = new DataSet();
        int stor = -1;
        int div = -1;
        string div_name = "";
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        
        public per_acc_ass_4(NpgsqlConnection con, int id_em)
        {
            this.con = con;
            InitializeComponent();
            this.id_em = id_em;
            this.MouseDown += new MouseEventHandler(MainForm_MouseDown);
            this.MouseMove += new MouseEventHandler(MainForm_MouseMove);
            this.MouseUp += new MouseEventHandler(MainForm_MouseUp);
        }
        public void updateEmpoupdate(int id_em)
        {
            try
            {
                String sql9 = "Select * from Employee where id=";
                sql9 += id_em.ToString();

                NpgsqlDataAdapter da9 = new NpgsqlDataAdapter(sql9, con);
                ds9.Reset();
                da9.Fill(ds9);
                dt9 = ds9.Tables[0];
                comboBox1.DataSource = dt9;
                comboBox1.DisplayMember = "name";
                comboBox1.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
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
                comboBox1.Text = "Сотрудник не выбран";
               
                label1.Font = new Font("Arial", 11);
               
           
                comboBox1.Font = new Font("Arial", 11);



            }
            catch { }


        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.id_em != -1)
                {
                    log_and_pass fp = new log_and_pass(con, id_em);
                    fp.Show();


                }
            }
            catch { }
        }

        private void per_acc_ass_4_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Enabled = false;
                




                Update();
                if (this.id_em != -1)
                {
                    updateEmpoupdate(id_em);
                   
                 



                }

            }
            catch { }
        }

        private void per_acc_ass_4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            division fp = new division(con);
            fp.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            department fp = new department(con);
            fp.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            employee fp = new employee(con, -1, "");
            fp.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            job1 fp = new job1(con);
            fp.Show();
        }
    }
}
