﻿namespace sclade
{
    partial class invoices
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Накладные";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 484);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Информация о накладных";
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView2.Location = new System.Drawing.Point(12, 511);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1579, 234);
            this.dataGridView2.TabIndex = 25;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.Location = new System.Drawing.Point(12, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1320, 402);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button3.Location = new System.Drawing.Point(1432, 751);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 51);
            this.button3.TabIndex = 29;
            this.button3.Text = "Назад";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1580, 24);
            this.menuStrip1.TabIndex = 30;
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
            this.личныеДанныеToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.личныеДанныеToolStripMenuItem.Text = "Накладную";
            this.личныеДанныеToolStripMenuItem.Click += new System.EventHandler(this.личныеДанныеToolStripMenuItem_Click);
            // 
            // адресToolStripMenuItem
            // 
            this.адресToolStripMenuItem.Name = "адресToolStripMenuItem";
            this.адресToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.адресToolStripMenuItem.Text = "Информацию о накладной";
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
            this.личныеДанныеToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.личныеДанныеToolStripMenuItem1.Text = "Накладную";
            this.личныеДанныеToolStripMenuItem1.Click += new System.EventHandler(this.личныеДанныеToolStripMenuItem1_Click);
            // 
            // адресToolStripMenuItem1
            // 
            this.адресToolStripMenuItem1.Name = "адресToolStripMenuItem1";
            this.адресToolStripMenuItem1.Size = new System.Drawing.Size(224, 22);
            this.адресToolStripMenuItem1.Text = "Информацию о накладной";
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
            this.личныеДанныеToolStripMenuItem2.Size = new System.Drawing.Size(224, 22);
            this.личныеДанныеToolStripMenuItem2.Text = "Накладную";
            this.личныеДанныеToolStripMenuItem2.Click += new System.EventHandler(this.личныеДанныеToolStripMenuItem2_Click);
            // 
            // адресToolStripMenuItem2
            // 
            this.адресToolStripMenuItem2.Name = "адресToolStripMenuItem2";
            this.адресToolStripMenuItem2.Size = new System.Drawing.Size(224, 22);
            this.адресToolStripMenuItem2.Text = "Информацию о накладной";
            this.адресToolStripMenuItem2.Click += new System.EventHandler(this.адресToolStripMenuItem2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Location = new System.Drawing.Point(1338, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(218, 31);
            this.button1.TabIndex = 31;
            this.button1.Text = "Информация о единицах измерения";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button2.Location = new System.Drawing.Point(1338, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(218, 31);
            this.button2.TabIndex = 32;
            this.button2.Text = "Информация о партиях";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button5.Location = new System.Drawing.Point(1338, 152);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(218, 31);
            this.button5.TabIndex = 34;
            this.button5.Text = "Информация о клиентах";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1412, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Справочник";
            // 
            // invoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1580, 814);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "invoices";
            this.Text = "Накладные продаж";
            this.Load += new System.EventHandler(this.invoices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
    }
}