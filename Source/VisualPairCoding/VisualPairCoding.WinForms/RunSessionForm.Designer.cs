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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunSessionForm));
            this.PauseButton = new System.Windows.Forms.Button();
            this.activeParticipantLabel = new System.Windows.Forms.Label();
            this.remainingTimeLabel = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.flashTimer = new System.Windows.Forms.Timer(this.components);
            this.recommendedNavigator = new System.Windows.Forms.Label();
            this.DriverLabel = new System.Windows.Forms.Label();
            this.recNavigatorLabel = new System.Windows.Forms.Label();
            this.skipCurrentDriverButton = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnAnimationTransparencyMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PauseButton
            // 
            this.PauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PauseButton.Location = new System.Drawing.Point(219, 12);
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
            this.activeParticipantLabel.Location = new System.Drawing.Point(12, 29);
            this.activeParticipantLabel.Name = "activeParticipantLabel";
            this.activeParticipantLabel.Size = new System.Drawing.Size(146, 21);
            this.activeParticipantLabel.TabIndex = 1;
            this.activeParticipantLabel.Text = "Active Participant";
            // 
            // remainingTimeLabel
            // 
            this.remainingTimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remainingTimeLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.remainingTimeLabel.Location = new System.Drawing.Point(127, 30);
            this.remainingTimeLabel.Name = "remainingTimeLabel";
            this.remainingTimeLabel.Size = new System.Drawing.Size(86, 21);
            this.remainingTimeLabel.TabIndex = 2;
            this.remainingTimeLabel.Text = "Remaining Time";
            this.remainingTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.Location = new System.Drawing.Point(219, 41);
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
            // recommendedNavigator
            // 
            this.recommendedNavigator.AutoSize = true;
            this.recommendedNavigator.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.recommendedNavigator.Location = new System.Drawing.Point(12, 70);
            this.recommendedNavigator.Name = "recommendedNavigator";
            this.recommendedNavigator.Size = new System.Drawing.Size(150, 17);
            this.recommendedNavigator.TabIndex = 4;
            this.recommendedNavigator.Text = "Recommended Navigator";
            // 
            // DriverLabel
            // 
            this.DriverLabel.AutoSize = true;
            this.DriverLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DriverLabel.Location = new System.Drawing.Point(12, 16);
            this.DriverLabel.Name = "DriverLabel";
            this.DriverLabel.Size = new System.Drawing.Size(37, 13);
            this.DriverLabel.TabIndex = 5;
            this.DriverLabel.Text = "Driver";
            // 
            // recNavigatorLabel
            // 
            this.recNavigatorLabel.AutoSize = true;
            this.recNavigatorLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.recNavigatorLabel.Location = new System.Drawing.Point(12, 57);
            this.recNavigatorLabel.Name = "recNavigatorLabel";
            this.recNavigatorLabel.Size = new System.Drawing.Size(81, 13);
            this.recNavigatorLabel.TabIndex = 6;
            this.recNavigatorLabel.Text = "Rec. Navigator";
            // 
            // skipCurrentDriverButton
            // 
            this.skipCurrentDriverButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.skipCurrentDriverButton.Location = new System.Drawing.Point(219, 70);
            this.skipCurrentDriverButton.Name = "skipCurrentDriverButton";
            this.skipCurrentDriverButton.Size = new System.Drawing.Size(75, 23);
            this.skipCurrentDriverButton.TabIndex = 7;
            this.skipCurrentDriverButton.Text = "Skip";
            this.skipCurrentDriverButton.UseVisualStyleBackColor = true;
            this.skipCurrentDriverButton.Click += new System.EventHandler(this.skipCurrentDriverButton_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleFrameToolStripMenuItem,
            this.turnAnimationTransparencyMenu});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(270, 70);
            // 
            // toggleFrameToolStripMenuItem
            // 
            this.toggleFrameToolStripMenuItem.Name = "toggleFrameToolStripMenuItem";
            this.toggleFrameToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.toggleFrameToolStripMenuItem.Text = "Toggle Window Frame";
            this.toggleFrameToolStripMenuItem.Click += new System.EventHandler(this.toggleWindowFrameToolStripMenuItem_Click);
            // 
            // turnAnimationTransparencyMenu
            // 
            this.turnAnimationTransparencyMenu.Name = "turnAnimationTransparencyMenu";
            this.turnAnimationTransparencyMenu.Size = new System.Drawing.Size(269, 22);
            this.turnAnimationTransparencyMenu.Text = "Make turn animation not transparent";
            this.turnAnimationTransparencyMenu.Click += new System.EventHandler(this.turnAnimationTransparencyMenu_Click);
            // 
            // RunSessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(306, 105);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.activeParticipantLabel);
            this.Controls.Add(this.skipCurrentDriverButton);
            this.Controls.Add(this.recNavigatorLabel);
            this.Controls.Add(this.DriverLabel);
            this.Controls.Add(this.recommendedNavigator);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.remainingTimeLabel);
            this.Controls.Add(this.PauseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MdiChildrenMinimizedAnchorBottom = false;
            this.MinimizeBox = false;
            this.Name = "RunSessionForm";
            this.Text = "Pair Coding Session";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RunSessionForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RunSessionForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RunSessionForm_MouseUp);
            this.contextMenuStrip.ResumeLayout(false);
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
        private Label recommendedNavigator;
        private Label DriverLabel;
        private Label recNavigatorLabel;
        private Button skipCurrentDriverButton;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem toggleFrameToolStripMenuItem;
        private ToolStripMenuItem turnAnimationTransparencyMenu;
    }
}