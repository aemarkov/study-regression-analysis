namespace RegressionAnalysis
{
	partial class Form1
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
			this.btnOpen = new System.Windows.Forms.Button();
			this.gridA = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtDispertion = new System.Windows.Forms.TextBox();
			this.txtAvErr = new System.Windows.Forms.TextBox();
			this.txtFCrit = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.gridA)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(227, 126);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(371, 36);
			this.btnOpen.TabIndex = 0;
			this.btnOpen.Text = "Открыть данные и расчитать";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// gridA
			// 
			this.gridA.AllowUserToAddRows = false;
			this.gridA.AllowUserToDeleteRows = false;
			this.gridA.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.gridA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridA.Location = new System.Drawing.Point(12, 33);
			this.gridA.Name = "gridA";
			this.gridA.ReadOnly = true;
			this.gridA.Size = new System.Drawing.Size(209, 129);
			this.gridA.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(208, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Расчитанные значение коэффициентов";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtFCrit);
			this.groupBox1.Controls.Add(this.txtAvErr);
			this.groupBox1.Controls.Add(this.txtDispertion);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(227, 14);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(371, 106);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Оценка качества модели";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Дисперсия среднего";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(170, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Средняя относительная ошибка";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 77);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(111, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Статистика Фишера";
			// 
			// txtDispertion
			// 
			this.txtDispertion.Location = new System.Drawing.Point(199, 20);
			this.txtDispertion.Name = "txtDispertion";
			this.txtDispertion.ReadOnly = true;
			this.txtDispertion.Size = new System.Drawing.Size(166, 20);
			this.txtDispertion.TabIndex = 3;
			// 
			// txtAvErr
			// 
			this.txtAvErr.Location = new System.Drawing.Point(199, 46);
			this.txtAvErr.Name = "txtAvErr";
			this.txtAvErr.ReadOnly = true;
			this.txtAvErr.Size = new System.Drawing.Size(166, 20);
			this.txtAvErr.TabIndex = 4;
			// 
			// txtFCrit
			// 
			this.txtFCrit.Location = new System.Drawing.Point(199, 74);
			this.txtFCrit.Name = "txtFCrit";
			this.txtFCrit.ReadOnly = true;
			this.txtFCrit.Size = new System.Drawing.Size(166, 20);
			this.txtFCrit.TabIndex = 5;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(615, 181);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridA);
			this.Controls.Add(this.btnOpen);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.gridA)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.DataGridView gridA;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtFCrit;
		private System.Windows.Forms.TextBox txtAvErr;
		private System.Windows.Forms.TextBox txtDispertion;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
	}
}

