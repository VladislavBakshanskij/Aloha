namespace Aloha {
    partial class ReportForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportForm));
            this.button1 = new System.Windows.Forms.Button();
            this.async = new System.Windows.Forms.ListBox();
            this.sync = new System.Windows.Forms.ListBox();
            this.report = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(560, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // async
            // 
            this.async.FormattingEnabled = true;
            this.async.Location = new System.Drawing.Point(12, 12);
            this.async.Name = "async";
            this.async.Size = new System.Drawing.Size(560, 147);
            this.async.TabIndex = 1;
            // 
            // sync
            // 
            this.sync.FormattingEnabled = true;
            this.sync.Location = new System.Drawing.Point(12, 168);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(560, 147);
            this.sync.TabIndex = 2;
            // 
            // report
            // 
            this.report.FormattingEnabled = true;
            this.report.Location = new System.Drawing.Point(12, 321);
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(560, 147);
            this.report.TabIndex = 3;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 526);
            this.Controls.Add(this.report);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.async);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            this.Shown += new System.EventHandler(this.ReportForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox async;
        private System.Windows.Forms.ListBox sync;
        private System.Windows.Forms.ListBox report;
    }
}