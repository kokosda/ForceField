namespace GameEditor.Core.Controls
{
    partial class MapForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MapWidth = new System.Windows.Forms.TextBox();
            this.MapName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TileWidth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TileHeight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.MapHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 430);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Создать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ширина карты";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // MapWidth
            // 
            this.MapWidth.Location = new System.Drawing.Point(11, 25);
            this.MapWidth.Name = "MapWidth";
            this.MapWidth.Size = new System.Drawing.Size(100, 20);
            this.MapWidth.TabIndex = 3;
            this.MapWidth.Text = "64";
            // 
            // MapName
            // 
            this.MapName.Location = new System.Drawing.Point(11, 97);
            this.MapName.Name = "MapName";
            this.MapName.Size = new System.Drawing.Size(94, 20);
            this.MapName.TabIndex = 4;
            this.MapName.Text = "Default";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Название";
            // 
            // TileWidth
            // 
            this.TileWidth.Location = new System.Drawing.Point(11, 141);
            this.TileWidth.Name = "TileWidth";
            this.TileWidth.Size = new System.Drawing.Size(100, 20);
            this.TileWidth.TabIndex = 7;
            this.TileWidth.Text = "64";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ширина клетки";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // TileHeight
            // 
            this.TileHeight.Location = new System.Drawing.Point(12, 184);
            this.TileHeight.Name = "TileHeight";
            this.TileHeight.Size = new System.Drawing.Size(100, 20);
            this.TileHeight.TabIndex = 9;
            this.TileHeight.Text = "32";
            this.TileHeight.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Высота клетки";
            // 
            // MapHeight
            // 
            this.MapHeight.Location = new System.Drawing.Point(11, 58);
            this.MapHeight.Name = "MapHeight";
            this.MapHeight.Size = new System.Drawing.Size(100, 20);
            this.MapHeight.TabIndex = 11;
            this.MapHeight.Text = "64";
            this.MapHeight.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Высота карты";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 468);
            this.Controls.Add(this.MapHeight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TileHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TileWidth);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MapName);
            this.Controls.Add(this.MapWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapForm";
            this.ShowIcon = false;
            this.Text = "Создание карты";
            this.Load += new System.EventHandler(this.MapForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox MapWidth;
        private System.Windows.Forms.TextBox MapName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TileWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TileHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox MapHeight;
        private System.Windows.Forms.Label label5;
    }
}