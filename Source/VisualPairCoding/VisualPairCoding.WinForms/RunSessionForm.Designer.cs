namespace VisualPairCoding.WinForms
{
    partial class RunSessionForm
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
            this.PauseButton = new System.Windows.Forms.Button();
            this.activeParticipantLabel = new System.Windows.Forms.Label();
            this.remainingTimeLabel = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.flashTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // PauseButton
            // 
            this.PauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PauseButton.Location = new System.Drawing.Point(258, 5);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 0;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // activeParticipantLabel
            // 
            this.activeParticipantLabel.AutoSize = true;
            this.activeParticipantLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.activeParticipantLabel.Location = new System.Drawing.Point(12, 9);
            this.activeParticipantLabel.Name = "activeParticipantLabel";
            this.activeParticipantLabel.Size = new System.Drawing.Size(146, 21);
            this.activeParticipantLabel.TabIndex = 1;
            this.activeParticipantLabel.Text = "Active Participant";
            // 
            // remainingTimeLabel
            // 
            this.remainingTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remainingTimeLabel.Location = new System.Drawing.Point(159, 42);
            this.remainingTimeLabel.Name = "remainingTimeLabel";
            this.remainingTimeLabel.Size = new System.Drawing.Size(93, 15);
            this.remainingTimeLabel.TabIndex = 2;
            this.remainingTimeLabel.Text = "Remaining Time";
            this.remainingTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.Location = new System.Drawing.Point(258, 34);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // flashTimer
            // 
            this.flashTimer.Tick += new System.EventHandler(this.flashTimer_Tick);
            // 
            // RunSessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 64);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.remainingTimeLabel);
            this.Controls.Add(this.activeParticipantLabel);
            this.Controls.Add(this.PauseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "RunSessionForm";
            this.Text = "Pair Coding Session";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button PauseButton;
        private Label activeParticipantLabel;
        private Label remainingTimeLabel;
        private Button StopButton;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer flashTimer;
    }
}