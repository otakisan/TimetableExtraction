namespace TimetableExtractionApp.Interactive
{
    partial class TimetableExtractionForm
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
            this.jrEastTimetableExtractionControl1 = new TimetableExtractionApp.Interactive.JrEastTimetableExtractionControl();
            this.SuspendLayout();
            // 
            // jrEastTimetableExtractionControl1
            // 
            this.jrEastTimetableExtractionControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jrEastTimetableExtractionControl1.Location = new System.Drawing.Point(12, 12);
            this.jrEastTimetableExtractionControl1.Name = "jrEastTimetableExtractionControl1";
            this.jrEastTimetableExtractionControl1.Size = new System.Drawing.Size(1315, 1003);
            this.jrEastTimetableExtractionControl1.TabIndex = 0;
            // 
            // TimetableExtractionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1575, 1045);
            this.Controls.Add(this.jrEastTimetableExtractionControl1);
            this.Name = "TimetableExtractionForm";
            this.Text = "TimetableExtractionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private JrEastTimetableExtractionControl jrEastTimetableExtractionControl1;
    }
}