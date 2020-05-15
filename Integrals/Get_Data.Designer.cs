namespace Integrals
{
    partial class Get_Data
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Get_Data));
            this.A = new System.Windows.Forms.Label();
            this.B = new System.Windows.Forms.Label();
            this.Step = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.numA = new System.Windows.Forms.NumericUpDown();
            this.numB = new System.Windows.Forms.NumericUpDown();
            this.numQ = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQ)).BeginInit();
            this.SuspendLayout();
            // 
            // A
            // 
            this.A.AutoSize = true;
            this.A.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.A.Location = new System.Drawing.Point(368, 60);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(46, 29);
            this.A.TabIndex = 0;
            this.A.Text = "a =";
            // 
            // B
            // 
            this.B.AutoSize = true;
            this.B.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B.Location = new System.Drawing.Point(368, 108);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(47, 29);
            this.B.TabIndex = 1;
            this.B.Text = "b =";
            // 
            // Step
            // 
            this.Step.AutoSize = true;
            this.Step.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Step.Location = new System.Drawing.Point(8, 161);
            this.Step.Name = "Step";
            this.Step.Size = new System.Drawing.Size(383, 31);
            this.Step.TabIndex = 2;
            this.Step.Text = "Количество точек/отрезков =";
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 30;
            this.listBox1.Items.AddRange(new object[] {
            "Метод средних прямоугольников ",
            "Метод Симсона",
            "Метод Монте-Карло"});
            this.listBox1.Location = new System.Drawing.Point(15, 203);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(513, 124);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // numA
            // 
            this.numA.Location = new System.Drawing.Point(421, 60);
            this.numA.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numA.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.numA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numA.Name = "numA";
            this.numA.Size = new System.Drawing.Size(103, 22);
            this.numA.TabIndex = 4;
            this.numA.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numB
            // 
            this.numB.Location = new System.Drawing.Point(421, 108);
            this.numB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numB.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numB.Name = "numB";
            this.numB.Size = new System.Drawing.Size(103, 22);
            this.numB.TabIndex = 5;
            this.numB.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numQ
            // 
            this.numQ.Location = new System.Drawing.Point(421, 167);
            this.numQ.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numQ.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numQ.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQ.Name = "numQ";
            this.numQ.Size = new System.Drawing.Size(103, 22);
            this.numQ.TabIndex = 6;
            this.numQ.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(15, 37);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 116);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(16, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "Вычислим:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(393, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 9;
            // 
            // Get_Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 328);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.numQ);
            this.Controls.Add(this.numB);
            this.Controls.Add(this.numA);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Step);
            this.Controls.Add(this.B);
            this.Controls.Add(this.A);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Get_Data";
            this.Text = "Get_Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Get_Data_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label A;
        private System.Windows.Forms.Label B;
        private System.Windows.Forms.Label Step;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown numA;
        private System.Windows.Forms.NumericUpDown numB;
        private System.Windows.Forms.NumericUpDown numQ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

