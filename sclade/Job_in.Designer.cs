namespace sclade
{
    partial class Job_in
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.выгрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelИнформациюВсехПартийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelИнформациюВыбранногоТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordИнформациюВсехТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJSONИнформациюВсехТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button2.Location = new System.Drawing.Point(484, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 32);
            this.button2.TabIndex = 33;
            this.button2.Text = "Найти по названию";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(17, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(446, 32);
            this.textBox1.TabIndex = 32;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(443, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Описание должности";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Должности";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Location = new System.Drawing.Point(568, 647);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 46);
            this.button1.TabIndex = 29;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(349, 88);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(347, 553);
            this.richTextBox1.TabIndex = 28;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(328, 553);
            this.dataGridView1.TabIndex = 27;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button3.Location = new System.Drawing.Point(12, 647);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(128, 46);
            this.button3.TabIndex = 34;
            this.button3.Text = "Выбрать должность";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выгрузитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(705, 24);
            this.menuStrip1.TabIndex = 35;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // выгрузитьToolStripMenuItem
            // 
            this.выгрузитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вExcelИнформациюВсехПартийToolStripMenuItem,
            this.вExcelИнформациюВыбранногоТовараToolStripMenuItem,
            this.вWordИнформациюВсехТоваровToolStripMenuItem,
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem,
            this.вJSONИнформациюВсехТоваровToolStripMenuItem,
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem});
            this.выгрузитьToolStripMenuItem.Name = "выгрузитьToolStripMenuItem";
            this.выгрузитьToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.выгрузитьToolStripMenuItem.Text = "Выгрузить";
            // 
            // вExcelИнформациюВсехПартийToolStripMenuItem
            // 
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Name = "вExcelИнформациюВсехПартийToolStripMenuItem";
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Text = "в Excel все данные";
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Click += new System.EventHandler(this.вExcelИнформациюВсехПартийToolStripMenuItem_Click);
            // 
            // вExcelИнформациюВыбранногоТовараToolStripMenuItem
            // 
            this.вExcelИнформациюВыбранногоТовараToolStripMenuItem.Name = "вExcelИнформациюВыбранногоТовараToolStripMenuItem";
            this.вExcelИнформациюВыбранногоТовараToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.вExcelИнформациюВыбранногоТовараToolStripMenuItem.Text = "в Excel информацию выбранной записи";
            this.вExcelИнформациюВыбранногоТовараToolStripMenuItem.Click += new System.EventHandler(this.вExcelИнформациюВыбранногоТовараToolStripMenuItem_Click);
            // 
            // вWordИнформациюВсехТоваровToolStripMenuItem
            // 
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Name = "вWordИнформациюВсехТоваровToolStripMenuItem";
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Text = "в Word все данные";
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Click += new System.EventHandler(this.вWordИнформациюВсехТоваровToolStripMenuItem_Click);
            // 
            // вWordИнформациюВыбранногоТовараToolStripMenuItem
            // 
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Name = "вWordИнформациюВыбранногоТовараToolStripMenuItem";
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Text = "в Word информацию выбранной записи";
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Click += new System.EventHandler(this.вWordИнформациюВыбранногоТовараToolStripMenuItem_Click);
            // 
            // вJSONИнформациюВсехТоваровToolStripMenuItem
            // 
            this.вJSONИнформациюВсехТоваровToolStripMenuItem.Name = "вJSONИнформациюВсехТоваровToolStripMenuItem";
            this.вJSONИнформациюВсехТоваровToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.вJSONИнформациюВсехТоваровToolStripMenuItem.Text = "в JSON все данные";
            this.вJSONИнформациюВсехТоваровToolStripMenuItem.Click += new System.EventHandler(this.вJSONИнформациюВсехТоваровToolStripMenuItem_Click);
            // 
            // вJSONИнформациюВыбранногоТовараToolStripMenuItem
            // 
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Name = "вJSONИнформациюВыбранногоТовараToolStripMenuItem";
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Size = new System.Drawing.Size(298, 22);
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Text = "в JSON информацию выбранной записи";
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Click += new System.EventHandler(this.вJSONИнформациюВыбранногоТовараToolStripMenuItem_Click);
            // 
            // Job_in
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(705, 693);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Job_in";
            this.Text = "Данные о должностях";
            this.Load += new System.EventHandler(this.Job_in_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem выгрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelИнформациюВсехПартийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelИнформациюВыбранногоТовараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordИнформациюВсехТоваровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordИнформациюВыбранногоТовараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вJSONИнформациюВсехТоваровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вJSONИнформациюВыбранногоТовараToolStripMenuItem;
    }
}