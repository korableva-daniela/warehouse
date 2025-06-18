namespace sclade
{
    partial class Product_card
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выгрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelИнформациюВсехПартийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вExcelИнформациюВыбраннойПартииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordИнформациюВсехТоваровToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.посмотретьИнформациюОПартияхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияОДвиженияхТовараToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияОКоличествеТовараНаСкладахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.npgsqlCommandBuilder1 = new Npgsql.NpgsqlCommandBuilder();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(697, 585);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Location = new System.Drawing.Point(1288, 711);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 43);
            this.button2.TabIndex = 58;
            this.button2.Text = "Назад";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.menuStrip1.Size = new System.Drawing.Size(1412, 24);
            this.menuStrip1.TabIndex = 59;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.добавитьToolStripMenuItem_Click);
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.изменитьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // выгрузитьToolStripMenuItem
            // 
            this.выгрузитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вExcelИнформациюВсехПартийToolStripMenuItem,
            this.вExcelИнформациюВыбраннойПартииToolStripMenuItem,
            this.вWordИнформациюВсехТоваровToolStripMenuItem,
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem,
            this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem,
            this.вToolStripMenuItem,
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem});
            this.выгрузитьToolStripMenuItem.Name = "выгрузитьToolStripMenuItem";
            this.выгрузитьToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.выгрузитьToolStripMenuItem.Text = "Выгрузить";
            // 
            // вExcelИнформациюВсехПартийToolStripMenuItem
            // 
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Name = "вExcelИнформациюВсехПартийToolStripMenuItem";
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Text = "в Excel информацию всех товаров";
            this.вExcelИнформациюВсехПартийToolStripMenuItem.Click += new System.EventHandler(this.вExcelИнформациюВсехПартийToolStripMenuItem_Click);
            // 
            // вExcelИнформациюВыбраннойПартииToolStripMenuItem
            // 
            this.вExcelИнформациюВыбраннойПартииToolStripMenuItem.Name = "вExcelИнформациюВыбраннойПартииToolStripMenuItem";
            this.вExcelИнформациюВыбраннойПартииToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вExcelИнформациюВыбраннойПартииToolStripMenuItem.Text = "в Excel информацию выбранного товара";
            this.вExcelИнформациюВыбраннойПартииToolStripMenuItem.Click += new System.EventHandler(this.вExcelИнформациюВыбраннойПартииToolStripMenuItem_Click);
            // 
            // вWordИнформациюВсехТоваровToolStripMenuItem
            // 
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Name = "вWordИнформациюВсехТоваровToolStripMenuItem";
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Text = "в Word информацию всех товаров";
            this.вWordИнформациюВсехТоваровToolStripMenuItem.Click += new System.EventHandler(this.вWordИнформациюВсехТоваровToolStripMenuItem_Click);
            // 
            // вWordИнформациюВыбранногоТовараToolStripMenuItem
            // 
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Name = "вWordИнформациюВыбранногоТовараToolStripMenuItem";
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Text = "в Word информацию выбранного товара ";
            this.вWordИнформациюВыбранногоТовараToolStripMenuItem.Click += new System.EventHandler(this.вWordИнформациюВыбранногоТовараToolStripMenuItem_Click);
            // 
            // вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem
            // 
            this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem.Name = "вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem";
            this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem.Text = "в Word информацию о передвижениях выбранного товара";
            this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem.Click += new System.EventHandler(this.вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem_Click);
            // 
            // вToolStripMenuItem
            // 
            this.вToolStripMenuItem.Name = "вToolStripMenuItem";
            this.вToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вToolStripMenuItem.Text = "в JSON информацию всех товаров";
            this.вToolStripMenuItem.Click += new System.EventHandler(this.вToolStripMenuItem_Click);
            // 
            // вJSONИнформациюВыбранногоТовараToolStripMenuItem
            // 
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Name = "вJSONИнформациюВыбранногоТовараToolStripMenuItem";
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Size = new System.Drawing.Size(401, 22);
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Text = "в JSON информацию выбранного товара ";
            this.вJSONИнформациюВыбранногоТовараToolStripMenuItem.Click += new System.EventHandler(this.вJSONИнформациюВыбранногоТовараToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(715, 93);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(352, 292);
            this.richTextBox1.TabIndex = 60;
            this.richTextBox1.Text = "                                   Карточка товара\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Location = new System.Drawing.Point(1165, 391);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 26);
            this.button1.TabIndex = 61;
            this.button1.Text = "найти";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(828, 412);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Описание товара";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(715, 428);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(352, 250);
            this.richTextBox2.TabIndex = 74;
            this.richTextBox2.Text = "";
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button6.Location = new System.Drawing.Point(12, 694);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(123, 43);
            this.button6.TabIndex = 76;
            this.button6.Text = "Выбрать товар";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button7.Location = new System.Drawing.Point(1165, 434);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(150, 32);
            this.button7.TabIndex = 77;
            this.button7.Text = "Справочник";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(1091, 115);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(299, 30);
            this.textBox1.TabIndex = 78;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(1091, 185);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(299, 29);
            this.textBox2.TabIndex = 79;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(1091, 263);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(299, 29);
            this.textBox3.TabIndex = 80;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1198, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Код товара";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1180, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 82;
            this.label3.Text = "Название товара";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1193, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 83;
            this.label4.Text = "Фирма товара";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1198, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 85;
            this.label5.Text = " Тип товара";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(1091, 332);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(299, 29);
            this.textBox4.TabIndex = 84;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.посмотретьИнформациюОПартияхToolStripMenuItem,
            this.информацияОДвиженияхТовараToolStripMenuItem,
            this.информацияОКоличествеТовараНаСкладахToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(327, 70);
            // 
            // посмотретьИнформациюОПартияхToolStripMenuItem
            // 
            this.посмотретьИнформациюОПартияхToolStripMenuItem.Name = "посмотретьИнформациюОПартияхToolStripMenuItem";
            this.посмотретьИнформациюОПартияхToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.посмотретьИнформациюОПартияхToolStripMenuItem.Text = "Посмотреть информацию о партиях";
            this.посмотретьИнформациюОПартияхToolStripMenuItem.Click += new System.EventHandler(this.посмотретьИнформациюОПартияхToolStripMenuItem_Click);
            // 
            // информацияОДвиженияхТовараToolStripMenuItem
            // 
            this.информацияОДвиженияхТовараToolStripMenuItem.Name = "информацияОДвиженияхТовараToolStripMenuItem";
            this.информацияОДвиженияхТовараToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.информацияОДвиженияхТовараToolStripMenuItem.Text = "Информация о движениях товара";
            this.информацияОДвиженияхТовараToolStripMenuItem.Click += new System.EventHandler(this.информацияОДвиженияхТовараToolStripMenuItem_Click);
            // 
            // информацияОКоличествеТовараНаСкладахToolStripMenuItem
            // 
            this.информацияОКоличествеТовараНаСкладахToolStripMenuItem.Name = "информацияОКоличествеТовараНаСкладахToolStripMenuItem";
            this.информацияОКоличествеТовараНаСкладахToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
            this.информацияОКоличествеТовараНаСкладахToolStripMenuItem.Text = "Информация о количестве товара на складах";
            this.информацияОКоличествеТовараНаСкладахToolStripMenuItem.Click += new System.EventHandler(this.информацияОКоличествеТовараНаСкладахToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button4.Location = new System.Drawing.Point(465, 36);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 36);
            this.button4.TabIndex = 87;
            this.button4.Text = "Выбрать склад";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(425, 21);
            this.comboBox1.TabIndex = 86;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button3.Location = new System.Drawing.Point(614, 36);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(153, 36);
            this.button3.TabIndex = 88;
            this.button3.Text = "По всем складам";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // npgsqlCommandBuilder1
            // 
            this.npgsqlCommandBuilder1.QuotePrefix = "\"";
            this.npgsqlCommandBuilder1.QuoteSuffix = "\"";
            // 
            // Product_card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1412, 766);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Product_card";
            this.Text = "Карточка товара";
            this.Load += new System.EventHandler(this.Product_card_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem посмотретьИнформациюОПартияхToolStripMenuItem;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
        private Npgsql.NpgsqlCommandBuilder npgsqlCommandBuilder1;
        private System.Windows.Forms.ToolStripMenuItem информацияОДвиженияхТовараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выгрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelИнформациюВсехПартийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вExcelИнформациюВыбраннойПартииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информацияОКоличествеТовараНаСкладахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordИнформациюВсехТоваровToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordИнформациюВыбранногоТовараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вJSONИнформациюВыбранногоТовараToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вWordИнформациюОПередвиженияхВыбранногоТовараToolStripMenuItem;
    }
}