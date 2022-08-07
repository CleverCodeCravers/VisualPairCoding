namespace VisualPairCoding.WinForms
{
    partial class EnterNamesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterNamesForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.participant1Textbox = new System.Windows.Forms.TextBox();
            this.participant2Textbox = new System.Windows.Forms.TextBox();
            this.participant3Textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.participant4Textbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.participant5Textbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.minutesPerRoundNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.minutesPerRoundNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Participant 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Participant 2";
            // 
            // participant1Textbox
            // 
            this.participant1Textbox.Location = new System.Drawing.Point(90, 17);
            this.participant1Textbox.Name = "participant1Textbox";
            this.participant1Textbox.Size = new System.Drawing.Size(176, 23);
            this.participant1Textbox.TabIndex = 2;
            this.participant1Textbox.Text = "Eik";
            // 
            // participant2Textbox
            // 
            this.participant2Textbox.Location = new System.Drawing.Point(90, 46);
            this.participant2Textbox.Name = "participant2Textbox";
            this.participant2Textbox.Size = new System.Drawing.Size(176, 23);
            this.participant2Textbox.TabIndex = 3;
            this.participant2Textbox.Text = "Bob";
            // 
            // participant3Textbox
            // 
            this.participant3Textbox.Location = new System.Drawing.Point(90, 75);
            this.participant3Textbox.Name = "participant3Textbox";
            this.participant3Textbox.Size = new System.Drawing.Size(176, 23);
            this.participant3Textbox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Participant 3";
            // 
            // participant4Textbox
            // 
            this.participant4Textbox.Location = new System.Drawing.Point(90, 104);
            this.participant4Textbox.Name = "participant4Textbox";
            this.participant4Textbox.Size = new System.Drawing.Size(176, 23);
            this.participant4Textbox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Participant 4";
            // 
            // participant5Textbox
            // 
            this.participant5Textbox.Location = new System.Drawing.Point(90, 133);
            this.participant5Textbox.Name = "participant5Textbox";
            this.participant5Textbox.Size = new System.Drawing.Size(176, 23);
            this.participant5Textbox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Participant 5";
            // 
            // minutesPerRoundNumericUpDown
            // 
            this.minutesPerRoundNumericUpDown.Location = new System.Drawing.Point(184, 162);
            this.minutesPerRoundNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minutesPerRoundNumericUpDown.Name = "minutesPerRoundNumericUpDown";
            this.minutesPerRoundNumericUpDown.Size = new System.Drawing.Size(82, 23);
            this.minutesPerRoundNumericUpDown.TabIndex = 10;
            this.minutesPerRoundNumericUpDown.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Number of minutes per turn";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(110, 191);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(76, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(191, 191);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(76, 23);
            this.closeButton.TabIndex = 13;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // EnterNamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 222);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.minutesPerRoundNumericUpDown);
            this.Controls.Add(this.participant5Textbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.participant4Textbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.participant3Textbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.participant2Textbox);
            this.Controls.Add(this.participant1Textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnterNamesForm";
            this.Text = "Setup Pair/Mob Coding";
            ((System.ComponentModel.ISupportInitialize)(this.minutesPerRoundNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox participant1Textbox;
        private TextBox participant2Textbox;
        private TextBox participant3Textbox;
        private Label label3;
        private TextBox participant4Textbox;
        private Label label4;
        private TextBox participant5Textbox;
        private Label label5;
        private NumericUpDown minutesPerRoundNumericUpDown;
        private Label label6;
        private Button startButton;
        private Button closeButton;
    }
}