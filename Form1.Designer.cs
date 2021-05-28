namespace KG6
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
			this._renderedImage = new System.Windows.Forms.PictureBox();
			this._startButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._renderedImage)).BeginInit();
			this.SuspendLayout();
			// 
			// _renderedImage
			// 
			this._renderedImage.BackColor = System.Drawing.SystemColors.Window;
			this._renderedImage.Location = new System.Drawing.Point(12, 9);
			this._renderedImage.Name = "_renderedImage";
			this._renderedImage.Size = new System.Drawing.Size(660, 609);
			this._renderedImage.TabIndex = 0;
			this._renderedImage.TabStop = false;
			// 
			// _startButton
			// 
			this._startButton.Location = new System.Drawing.Point(12, 624);
			this._startButton.Name = "_startButton";
			this._startButton.Size = new System.Drawing.Size(660, 40);
			this._startButton.TabIndex = 1;
			this._startButton.Text = "Start";
			this._startButton.UseVisualStyleBackColor = true;
			this._startButton.Click += new System.EventHandler(this.OnStartButtonClick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Desktop;
			this.ClientSize = new System.Drawing.Size(685, 675);
			this.Controls.Add(this._startButton);
			this.Controls.Add(this._renderedImage);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this._renderedImage)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox _renderedImage;
		private System.Windows.Forms.Button _startButton;
	}
}

