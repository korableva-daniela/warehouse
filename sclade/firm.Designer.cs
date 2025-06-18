namespace sclade
{
    partial class firm
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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.личныеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.адресToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.личныеДанныеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.адресToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.личныеДанныеToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.адресToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.выгрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelИнформациюВсехПартийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelДанныеАдресовФирмыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordВсеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordВсеДанныеToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordАдресаКонтрагентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJSONВсеДанныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJSONВыбранногоКонтрагентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJSONАдресКонтагентаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button3.Location = new System.Drawing.Point(1012, 641);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 48);
            this.button3.TabIndex = 26;
            this.button3.Text = "Назад";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button2.Location = new System.Drawing.Point(572, 34);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 32);
            this.button2.TabIndex = 25;
            this.button2.Text = "Найти по названию";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 34);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(554, 32);
            this.textBox1.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Данные контрагентов";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 477);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Адреса контрагентов";
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView2.Location = new System.Drawing.Point(12, 505);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1124, 108);
            this.dataGridView2.TabIndex = 21;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.Location = new System.Drawing.Point(12, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1124, 346);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.выгрузитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1146, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.личныеДанныеToolStripMenuItem,
            this.адресToolStripMenuItem});
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // личныеДанныеToolStripMenuItem
            // 
            this.личныеДанныеToolStripMenuItem.Name = "личныеДанныеToolStripMenuItem";
            this.личныеДанныеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.личныеДанныеToolStripMenuItem.Text = "Данные фирмы";
            this.личныеДанныеToolStripMenuItem.Click += new System.EventHandler(this.личныеДанныеToolStripMenuItem_Click);
            // 
            // адресToolStripMenuItem
            // 
            this.адресToolStripMenuItem.Name = "адресToolStripMenuItem";
            this.адресToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.адресToolStripMenuItem.Text = "Адрес";
            this.адресToolStripMenuItem.Click += new System.EventHandler(this.адресToolStripMenuItem_Click);
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.личныеДанныеToolStripMenuItem1,
            this.адресToolStripMenuItem1});
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            // 
            // личныеДанныеToolStripMenuItem1
            // 
            this.личныеДанныеToolStripMenuItem1.Name = "личныеДанныеToolStripMenuItem1";
            this.личныеДанныеToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.личныеДанныеToolStripMenuItem1.Text = "Данные фирмы";
            this.личныеДанныеToolStripMenuItem1.Click += new System.EventHandler(this.личныеДанныеToolStripMenuItem1_Click);
            // 
            // адресToolStripMenuItem1
            // 
            this.адресToolStripMenuItem1.Name = "адресToolStripMenuItem1";
            this.адресToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.адресToolStripMenuItem1.Text = "Адрес";
            this.адресToolStripMenuItem1.Click += new System.EventHandler(this.адресToolStripMenuItem1_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.личныеДанныеToolStripMenuItem2,
            this.адресToolStripMenuItem2});
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // личныеДанныеToolStripMenuItem2
            // 
            this.личныеДанныеToolStripMenuItem2.Name = "личныеДанныеToolStripMenuItem2";
            this.личныеДанныеToolStripMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.личныеДанныеToolStripMenuItem2.Text = "Данные фирмы";
            this.личныеДанныеToolStripMenuItem2.Click += new System.EventHandler(this.личныеДанныеToolStripMenuItem2_Click);
            // 
            // адресToolStripMenuItem2
            // 
            this.адресToolStripMenuItem2.Name = "адресToolStripMenuItem2";
            this.адресToolStripMenuItem2.Size = new System.Drawing.Size(161, 22);
            this.адресToolStripMenuItem2.Text = "Адрес";
            this.адресToolStripMenuItem2.Click += new System.EventHandler(this.адресToolStripMenuItem2_Click);
            // 
            // выгрузитьToolStripMenuItem
            // 
            this.выгрузитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вExcelИнформациюВсехПартийToolStripMenuItem,
            this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem,
            this.вExcelДанныеАдресовФирмыToolStripMenuItem,
            this.вWordВсеДанныеToolStripMenuItem,
            this.вWordВсеДанныеToolStripMenuItem1,
            this.вWordАдресаКонтрагентаToolStripMenuItem,
            this.вJSONВсеДанныеToolStripMenuItem,
            this.вJSONВыбранногоКонтрагентаToolStripMenuItem,
            this.вJSONАдресКонтагентаToolStripMenuItem});
            this.выгрузитьToolStripMenuItem.Name = "выгрузитьToolStripMenuItem";
            this.выгрузитьToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.выгрузитьToolStripMenuItem.Text = "Выгрузить";
            // 
            // вExcelИнформациюВсехПартийToolStripMenuItem
            // 
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Name = "вExcelИнформациюВсехПартийToolStripMenuItem";
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Text = "в Excel все данные";
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Click += new System.EventHandler(this.вExcelИнформациюВсехПартийToolStripMenuItem_Click);
            // 
            // вExcelДанныеВыбранногоПодразделенияToolStripMenuItem
            // 
            this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem.Name = "вExcelДанныеВыбранногоПодразделенияToolStripMenuItem";
            this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem.Text = "в Excel выбранного контрагента";
            this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem.Click += new System.EventHandler(this.вExcelДанныеВыбранногоПодразделенияToolStripMenuItem_Click);
            // 
            // вExcelДанныеАдресовФирмыToolStripMenuItem
            // 
            this.вExcelДанныеАдресовФирмыToolStripMenuItem.Name = "вExcelДанныеАдресовФирмыToolStripMenuItem";
            this.вExcelДанныеАдресовФирмыToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вExcelДанныеАдресовФирмыToolStripMenuItem.Text = "в Excel адреса контагента";
            this.вExcelДанныеАдресовФирмыToolStripMenuItem.Click += new System.EventHandler(this.вExcelДанныеАдресовФирмыToolStripMenuItem_Click);
            // 
            // вWordВсеДанныеToolStripMenuItem
            // 
            this.вWordВсеДанныеToolStripMenuItem.Name = "вWordВсеДанныеToolStripMenuItem";
            this.вWordВсеДанныеToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вWordВсеДанныеToolStripMenuItem.Text = "в Word все данные";
            this.вWordВсеДанныеToolStripMenuItem.Click += new System.EventHandler(this.вWordВсеДанныеToolStripMenuItem_Click);
            // 
            // вWordВсеДанныеToolStripMenuItem1
            // 
            this.вWordВсеДанныеToolStripMenuItem1.Name = "вWordВсеДанныеToolStripMenuItem1";
            this.вWordВсеДанныеToolStripMenuItem1.Size = new System.Drawing.Size(253, 22);
            this.вWordВсеДанныеToolStripMenuItem1.Text = "в Word выбранного контрагента";
            this.вWordВсеДанныеToolStripMenuItem1.Click += new System.EventHandler(this.вWordВсеДанныеToolStripMenuItem1_Click);
            // 
            // вWordАдресаКонтрагентаToolStripMenuItem
            // 
            this.вWordАдресаКонтрагентаToolStripMenuItem.Name = "вWordАдресаКонтрагентаToolStripMenuItem";
            this.вWordАдресаКонтрагентаToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вWordАдресаКонтрагентаToolStripMenuItem.Text = "в Word адреса контрагента";
            this.вWordАдресаКонтрагентаToolStripMenuItem.Click += new System.EventHandler(this.вWordАдресаКонтрагентаToolStripMenuItem_Click);
            // 
            // вJSONВсеДанныеToolStripMenuItem
            // 
            this.вJSONВсеДанныеToolStripMenuItem.Name = "вJSONВсеДанныеToolStripMenuItem";
            this.вJSONВсеДанныеToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вJSONВсеДанныеToolStripMenuItem.Text = "в JSON все данные ";
            this.вJSONВсеДанныеToolStripMenuItem.Click += new System.EventHandler(this.вJSONВсеДанныеToolStripMenuItem_Click);
            // 
            // вJSONВыбранногоКонтрагентаToolStripMenuItem
            // 
            this.вJSONВыбранногоКонтрагентаToolStripMenuItem.Name = "вJSONВыбранногоКонтрагентаToolStripMenuItem";
            this.вJSONВыбранногоКонтрагентаToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вJSONВыбранногоКонтрагентаToolStripMenuItem.Text = "в JSON выбранного контрагента";
            this.вJSONВыбранногоКонтрагентаToolStripMenuItem.Click += new System.EventHandler(this.вJSONВыбранногоКонтрагентаToolStripMenuItem_Click);
            // 
            // вJSONАдресКонтагентаToolStripMenuItem
            // 
            this.вJSONАдресКонтагентаToolStripMenuItem.Name = "вJSONАдресКонтагентаToolStripMenuItem";
            this.вJSONАдресКонтагентаToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.вJSONАдресКонтагентаToolStripMenuItem.Text = "в JSON адреса контагента";
            this.вJSONАдресКонтагентаToolStripMenuItem.Click += new System.EventHandler(this.вJSONАдресКонтагентаToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Location = new System.Drawing.Point(12, 641);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 48);
            this.button1.TabIndex = 27;
            this.button1.Text = "Выбрать контрагента";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // firm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1146, 701);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "firm";
            this.Text = "Контрагенты";
            this.Load += new System.EventHandler(this.firm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem личныеДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem адресToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem личныеДанныеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem адресToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem личныеДанныеToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem адресToolStripMenuItem2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem выгрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelИнформациюВсехПартийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelДанныеВыбранногоПодразделенияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelДанныеАдресовФирмыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordВсеДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordВсеДанныеToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem вWordАдресаКонтрагентаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вJSONВсеДанныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вJSONВыбранногоКонтрагентаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вJSONАдресКонтагентаToolStripMenuItem;
    }
}