using FastColoredTextBoxNS;
namespace GameEditor.Core.Controls
{
    partial class ScriptForm
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
            this.coloredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ResultCompile = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // coloredTextBox
            // 
            this.coloredTextBox.AutoScroll = true;
            this.coloredTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.coloredTextBox.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.coloredTextBox.IsChanged = true;
            this.coloredTextBox.Location = new System.Drawing.Point(12, 12);
            this.coloredTextBox.Name = "coloredTextBox";
            this.coloredTextBox.SelectedText = "";
            this.coloredTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.coloredTextBox.Size = new System.Drawing.Size(535, 472);
            this.coloredTextBox.TabIndex = 0;
            this.coloredTextBox.TabLength = 0;
            this.coloredTextBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ColoredTextBoxTextChange);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(553, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Проверить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ResultCompile
            // 
            this.ResultCompile.AutoSize = true;
            this.ResultCompile.Location = new System.Drawing.Point(550, 384);
            this.ResultCompile.Name = "ResultCompile";
            this.ResultCompile.Size = new System.Drawing.Size(113, 13);
            this.ResultCompile.TabIndex = 2;
            this.ResultCompile.Text = "Результат проверки:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(696, 447);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 37);
            this.button2.TabIndex = 3;
            this.button2.Text = "Сохранить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ScriptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 496);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ResultCompile);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.coloredTextBox);
            this.Name = "ScriptForm";
            this.ShowIcon = false;
            this.Text = "Редактор скриптов";
            this.Load += new System.EventHandler(this.ScriptForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBox coloredTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ResultCompile;
        private System.Windows.Forms.Button button2;


    }
}