using System.Windows.Forms;

namespace AlgReshZO
{
    public partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            textBox1 = new RichTextBox();
            textBox2 = new RichTextBox();
            textBox3 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(4, 170);
            button1.Name = "button1";
            button1.Size = new Size(116, 57);
            button1.TabIndex = 0;
            button1.Text = "Проверить формулу";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(136, 170);
            button2.Name = "button2";
            button2.Size = new Size(116, 57);
            button2.TabIndex = 0;
            button2.Text = "Начать расчет";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top;
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(14, 31);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(700, 35);
            textBox1.TabIndex = 1;
            textBox1.Text = exp;
            textBox1.TextChanged += TextBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top;
            textBox2.Enabled = false;
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox2.Location = new Point(14, 129);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(847, 35);
            textBox2.TabIndex = 1;
            textBox2.Text = "";
            textBox2.TextChanged += TextBox2_TextChanged;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top;
            textBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.Location = new Point(746, 31);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 35);
            textBox3.TabIndex = 1;
            textBox3.Text = "0.001";
            textBox3.TextChanged += TextBox3_TextChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBox1.Location = new Point(4, 233);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(866, 246);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "Результат выполнения расчета";
            richTextBox1.TextChanged += RichTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(14, 9);
            label1.Name = "label1";
            label1.Size = new Size(104, 15);
            label1.TabIndex = 3;
            label1.Text = "Введите формулу";
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(14, 82);
            label2.Name = "label2";
            label2.Size = new Size(0, 15);
            label2.TabIndex = 4;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(14, 111);
            label3.Name = "label3";
            label3.Size = new Size(0, 15);
            label3.TabIndex = 5;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(136, 9);
            label4.Name = "label4";
            label4.Size = new Size(0, 15);
            label4.TabIndex = 6;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(270, 170);
            button3.Name = "button3";
            button3.Size = new Size(112, 57);
            button3.TabIndex = 7;
            button3.Text = "Сбросить значения";
            button3.Enabled = false;
            button3.UseVisualStyleBackColor = true;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Click += Button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 491);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(richTextBox1);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(button3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private RichTextBox textBox1;
        private RichTextBox textBox2;
        private RichTextBox textBox3;
        private RichTextBox richTextBox1;
        private Font font = new Font(SystemFonts.MessageBoxFont.FontFamily, 12);
        private bool isButton2Enabled = false;
        private Label label1;
        private Label label2;
        private Label label3;
        public string labelText = "Введите значения переменных через ; в следующем порядке: ";
        private Label label4;
        private Button button3;
    }
}
