
namespace OFS.UnitifySAC.App.Forms
{
    partial class StormSewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StormSewerForm));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.richTextBoxLogControlLog = new Serilog.Sinks.WinForms.RichTextBoxLogControl();
            this.labelLog = new System.Windows.Forms.Label();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.toolStripStandard = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUnitifySAC = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenInMainApp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShowLog = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStripStandard.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.richTextBoxLogControlLog);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.labelLog);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.richTextBox);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(712, 437);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(712, 462);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripStandard);
            // 
            // richTextBoxLogControlLog
            // 
            this.richTextBoxLogControlLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLogControlLog.BackColor = System.Drawing.SystemColors.ControlDark;
            this.richTextBoxLogControlLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLogControlLog.ForContext = "";
            this.richTextBoxLogControlLog.Location = new System.Drawing.Point(0, 190);
            this.richTextBoxLogControlLog.Name = "richTextBoxLogControlLog";
            this.richTextBoxLogControlLog.ReadOnly = true;
            this.richTextBoxLogControlLog.Size = new System.Drawing.Size(712, 247);
            this.richTextBoxLogControlLog.TabIndex = 2;
            this.richTextBoxLogControlLog.TabStop = false;
            this.richTextBoxLogControlLog.Text = "";
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(3, 174);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(30, 13);
            this.labelLog.TabIndex = 1;
            this.labelLog.Text = "Logs";
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(712, 163);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.TabStop = false;
            this.richTextBox.Text = "";
            // 
            // toolStripStandard
            // 
            this.toolStripStandard.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripStandard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripButtonSaveAs,
            this.toolStripSeparator1,
            this.toolStripButtonUnitifySAC,
            this.toolStripSeparator2,
            this.toolStripButtonOpenInMainApp,
            this.toolStripButtonShowLog});
            this.toolStripStandard.Location = new System.Drawing.Point(3, 0);
            this.toolStripStandard.Name = "toolStripStandard";
            this.toolStripStandard.Size = new System.Drawing.Size(162, 25);
            this.toolStripStandard.TabIndex = 0;
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "Open";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Save";
            // 
            // toolStripButtonSaveAs
            // 
            this.toolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveAs.Image")));
            this.toolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.toolStripButtonSaveAs.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSaveAs.Text = "Save As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUnitifySAC
            // 
            this.toolStripButtonUnitifySAC.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUnitifySAC.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUnitifySAC.Image")));
            this.toolStripButtonUnitifySAC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUnitifySAC.Name = "toolStripButtonUnitifySAC";
            this.toolStripButtonUnitifySAC.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUnitifySAC.Text = "Unitify SAC";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonOpenInMainApp
            // 
            this.toolStripButtonOpenInMainApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenInMainApp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenInMainApp.Image")));
            this.toolStripButtonOpenInMainApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenInMainApp.Name = "toolStripButtonOpenInMainApp";
            this.toolStripButtonOpenInMainApp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpenInMainApp.Text = "Open In Main App";
            // 
            // toolStripButtonShowLog
            // 
            this.toolStripButtonShowLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonShowLog.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonShowLog.Image")));
            this.toolStripButtonShowLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShowLog.Name = "toolStripButtonShowLog";
            this.toolStripButtonShowLog.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonShowLog.Text = "Show Log";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusProgressLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 440);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(712, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripProgressBar.Visible = false;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusProgressLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusProgressLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // StormSewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 462);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "StormSewerForm";
            this.helpProviderHaestadForm.SetShowHelp(this, false);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStripStandard.ResumeLayout(false);
            this.toolStripStandard.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ToolStrip toolStripStandard;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUnitifySAC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenInMainApp;
        private System.Windows.Forms.ToolStripButton toolStripButtonShowLog;
        private System.Windows.Forms.Label labelLog;
        private Serilog.Sinks.WinForms.RichTextBoxLogControl richTextBoxLogControlLog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProgressLabel;
    }
}

