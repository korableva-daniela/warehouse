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
    public partial class per_acc_ass_3 : Form
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
        DataTable dt5 = new DataTable();
        DataSet ds5 = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана

        public per_acc_ass_3(NpgsqlConnection con, int id_em)
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
                if (stor == -1)
                {
                    comboBox2.Text = "Склад не выбран";
                }
                else
                {
                    updatestorehouseinfo(stor);
                }
                label1.Font = new Font("Arial", 11);
              
                label6.Font = new Font("Arial", 11);
                comboBox2.Font = new Font("Arial", 11);
                comboBox1.Font = new Font("Arial", 11);



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
                comboBox2.DataSource = dt3;
                comboBox2.DisplayMember = "name";
                comboBox2.ValueMember = "id";
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch { }
        }
        private void per_acc_ass_3_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                //button8.Visible = false;



                Update();
                if (this.id_em != -1)
                {
                    updateEmpoupdate(id_em);
                    try
                    {
                        String sql5 = "Select id, name from Division where id= (Select id_d from Job_em where id=(Select id from Employee where id=";
                        sql5 += id_em.ToString();
                        sql5 += "))";
                        NpgsqlDataAdapter da5 = new NpgsqlDataAdapter(sql5, con);
                        ds5.Reset();
                        da5.Fill(ds5);
                        dt5 = ds5.Tables[0];
                        if (dt5.Rows.Count > 0)
                        {
                            div = Convert.ToInt32(dt5.Rows[0]["id"]);
                            div_name = dt5.Rows[0]["name"].ToString();
                        }
                        else
                        {

                            MessageBox.Show("Подразделение не найдено.");
                        }
                    }
                    catch { }



                }

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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //try
                //{
                //    String sql4 = "Select id, name from Division where id= (Select id_d from Job_em where id=(Select id from Employee where id=";
                //    sql4 += id_em.ToString();
                //    sql4 += "))";
                //    NpgsqlDataAdapter da4 = new NpgsqlDataAdapter(sql4, con);
                //    ds4.Reset();
                //    da4.Fill(ds4);
                //    dt4 = ds4.Tables[0];
                //    if (dt4.Rows.Count > 0)
                //    {
                //        div = Convert.ToInt32(dt4.Rows[0]["id"]);
                //        div_name = dt4.Rows[0]["name"].ToString();
                //    }
                //    else
                //    {

                //        MessageBox.Show("Приходная накладная не найдена.");
                //    }
                //}
                //catch { }

                int id_s = 0;
                string name = "";

                storehouse fp = new storehouse(con, id_s, name, div, div_name);
                fp.ShowDialog();
                if (fp.name != "")
                {
                    stor = fp.id_c;
                    updatestorehouseinfo(stor);

                }
                else
                {
                    comboBox2.Text = "Склад не выбран";

                }
            }
            catch { }
        }

            private void button8_Click(object sender, EventArgs e)
        {
            try
            {



                updatestorehouseinfo(-1);
                comboBox2.Text = "Склад не выбран";
            }


            catch { }
        }

        private void per_acc_ass_3_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void per_acc_ass_3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                product_place fp = new product_place(con, this.stor, "", this.id_em, -1, this.div);
                fp.Show();
            }
            else
            {
                product_place fp = new product_place(con, -1, "", this.id_em, -1, this.div);
                fp.Show();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                product_shipment fp = new product_shipment(con, this.stor, "", this.id_em, -1, this.div);
                fp.Show();
            }
            else
            {
                product_shipment fp = new product_shipment(con, -1, "", this.id_em, -1, this.div);
                fp.Show();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                product_accounting fp = new product_accounting(con, this.stor, "", this.id_em, -1, this.div);
                fp.Show();
            }
            else
            {
                product_accounting fp = new product_accounting(con, -1, "", this.id_em, -1, this.div);
                fp.Show();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue != null)
            {
                mov_pro fp = new mov_pro(con, (int)comboBox2.SelectedValue, "", this.id_em, -1, this.div);
                fp.Show();
            }
            else
            {
                mov_pro fp = new mov_pro(con, -1, "", this.id_em, -1, this.div);
                fp.Show();
            }
        }
    }
    
   
}
