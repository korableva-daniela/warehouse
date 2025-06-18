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
    public partial class NDS : Form
    {
        public NpgsqlConnection con;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        private bool dragging = false; // Флаг для отслеживания состояния перетаскивания
        private Point dragCursorPoint; // Точка курсора мыши относительно формы
        private Point dragFormPoint; // Точка формы относительно экрана
        public NDS(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NDS_Load(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.ReadOnly = true;
              
                dataGridView1.ReadOnly = true;
                Update();
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                if (id != 0)

                    description(id);
            }
            }

            catch { }
        }
        public void Update()
        {
                try
                {
                    label1.Font = new Font("Arial", 11);
            label2.Font = new Font("Arial", 11);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Font = new Font("Arial", 9);
            richTextBox1.Font = new Font("Arial", 11);
  
            String sql = "Select * from NDS ORDER BY percent ASC";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                ds.Reset();
                da.Fill(ds);
                dt = ds.Tables[0];
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "%";
                dataGridView1.Columns[2].Visible = false;

                this.StartPosition = FormStartPosition.CenterScreen;
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
                        string desc = (string)dataGridView1.CurrentRow.Cells[2].Value;
                        richTextBox1.Text = desc;
                    }
                    if (dataGridView1.CurrentRow.Index == 0)
                    {
                        if (dataGridView1.Rows[0].Cells[0].Value != null)
                        {
                            string desc = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            richTextBox1.Text = desc;
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

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                        try
                        {
                            newNDS f = new newNDS(con, -1, 0, "");
            f.ShowDialog();
            Update();
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                //if (id != -1)
                //{
                //    description(id);
                //}
            }
            }

            catch { }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                            try
                            {
                                int id = (int)dataGridView1.CurrentRow.Cells[0].Value;

            int percent = (int)dataGridView1.CurrentRow.Cells[1].Value;
            string descr = (string)dataGridView1.CurrentRow.Cells[2].Value;
            newNDS f = new newNDS(con, id, percent, descr);
            f.ShowDialog();
            Update();
                //description(id);
            }

            catch { }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
                                try
                                {
                                    int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
            NpgsqlCommand command = new NpgsqlCommand("DELETE FROM NDS WHERE id=:id", con);

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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
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
    }
}
