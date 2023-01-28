namespace PolUnion
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
            this.doneButton = new System.Windows.Forms.Button();
            this.clearSceneButton = new System.Windows.Forms.Button();
            this.scenePictureBox = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scenePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(13, 13);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(89, 60);
            this.doneButton.TabIndex = 0;
            this.doneButton.Text = "Построить";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // clearSceneButton
            // 
            this.clearSceneButton.Location = new System.Drawing.Point(108, 12);
            this.clearSceneButton.Name = "clearSceneButton";
            this.clearSceneButton.Size = new System.Drawing.Size(84, 61);
            this.clearSceneButton.TabIndex = 1;
            this.clearSceneButton.Text = "Очистить";
            this.clearSceneButton.UseVisualStyleBackColor = true;
            this.clearSceneButton.Click += new System.EventHandler(this.clearSceneButton_Click);
            // 
            // scenePictureBox
            // 
            this.scenePictureBox.Location = new System.Drawing.Point(13, 80);
            this.scenePictureBox.Name = "scenePictureBox";
            this.scenePictureBox.Size = new System.Drawing.Size(784, 443);
            this.scenePictureBox.TabIndex = 2;
            this.scenePictureBox.TabStop = false;
            this.scenePictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.scenePictureBox_MouseClick);
            this.scenePictureBox.MouseEnter += new System.EventHandler(this.scenePictureBox_MouseEnter);
            this.scenePictureBox.MouseLeave += new System.EventHandler(this.scenePictureBox_MouseLeave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(646, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 59);
            this.button1.TabIndex = 3;
            this.button1.Text = "Объединить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 535);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scenePictureBox);
            this.Controls.Add(this.clearSceneButton);
            this.Controls.Add(this.doneButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.scenePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button clearSceneButton;
        private System.Windows.Forms.PictureBox scenePictureBox;
        private System.Windows.Forms.Button button1;
    }
}

