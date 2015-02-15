namespace TimetableExtractionApp.Interactive
{
    partial class JrEastTimetableExtractionControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.baseURLLabel = new System.Windows.Forms.Label();
            this.testTargetTextBox = new System.Windows.Forms.TextBox();
            this.extractTimetableButton = new System.Windows.Forms.Button();
            this.consecutiveCheckBox = new System.Windows.Forms.CheckBox();
            this.consecutiveTolabel = new System.Windows.Forms.Label();
            this.consecutiveToNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.consecutiveFromNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.consecutiveFromLabel = new System.Windows.Forms.Label();
            this.traceTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.consecutiveToNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consecutiveFromNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // baseURLLabel
            // 
            this.baseURLLabel.AutoSize = true;
            this.baseURLLabel.Location = new System.Drawing.Point(3, 6);
            this.baseURLLabel.Name = "baseURLLabel";
            this.baseURLLabel.Size = new System.Drawing.Size(114, 24);
            this.baseURLLabel.TabIndex = 0;
            this.baseURLLabel.Text = "Base URL:";
            // 
            // testTargetTextBox
            // 
            this.testTargetTextBox.Location = new System.Drawing.Point(123, 3);
            this.testTargetTextBox.Name = "testTargetTextBox";
            this.testTargetTextBox.ReadOnly = true;
            this.testTargetTextBox.Size = new System.Drawing.Size(704, 31);
            this.testTargetTextBox.TabIndex = 1;
            this.testTargetTextBox.Text = "866 (新宿) : 1039 (東京) 253 : (海芝浦)";
            // 
            // extractTimetableButton
            // 
            this.extractTimetableButton.Location = new System.Drawing.Point(848, 1);
            this.extractTimetableButton.Name = "extractTimetableButton";
            this.extractTimetableButton.Size = new System.Drawing.Size(132, 34);
            this.extractTimetableButton.TabIndex = 2;
            this.extractTimetableButton.Text = "Extract(&E)";
            this.extractTimetableButton.UseVisualStyleBackColor = true;
            this.extractTimetableButton.Click += new System.EventHandler(this.extractTimetableButton_Click);
            // 
            // consecutiveCheckBox
            // 
            this.consecutiveCheckBox.AutoSize = true;
            this.consecutiveCheckBox.Location = new System.Drawing.Point(370, 57);
            this.consecutiveCheckBox.Name = "consecutiveCheckBox";
            this.consecutiveCheckBox.Size = new System.Drawing.Size(165, 28);
            this.consecutiveCheckBox.TabIndex = 3;
            this.consecutiveCheckBox.Text = "Consecutive";
            this.consecutiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // consecutiveTolabel
            // 
            this.consecutiveTolabel.AutoSize = true;
            this.consecutiveTolabel.Location = new System.Drawing.Point(553, 58);
            this.consecutiveTolabel.Name = "consecutiveTolabel";
            this.consecutiveTolabel.Size = new System.Drawing.Size(41, 24);
            this.consecutiveTolabel.TabIndex = 4;
            this.consecutiveTolabel.Text = "To:";
            // 
            // consecutiveToNumericUpDown
            // 
            this.consecutiveToNumericUpDown.Location = new System.Drawing.Point(600, 56);
            this.consecutiveToNumericUpDown.Maximum = new decimal(new int[] {
            1732,
            0,
            0,
            0});
            this.consecutiveToNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.consecutiveToNumericUpDown.Name = "consecutiveToNumericUpDown";
            this.consecutiveToNumericUpDown.Size = new System.Drawing.Size(120, 31);
            this.consecutiveToNumericUpDown.TabIndex = 5;
            this.consecutiveToNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.consecutiveToNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // consecutiveFromNumericUpDown
            // 
            this.consecutiveFromNumericUpDown.Location = new System.Drawing.Point(211, 56);
            this.consecutiveFromNumericUpDown.Maximum = new decimal(new int[] {
            1732,
            0,
            0,
            0});
            this.consecutiveFromNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.consecutiveFromNumericUpDown.Name = "consecutiveFromNumericUpDown";
            this.consecutiveFromNumericUpDown.Size = new System.Drawing.Size(120, 31);
            this.consecutiveFromNumericUpDown.TabIndex = 7;
            this.consecutiveFromNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.consecutiveFromNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // consecutiveFromLabel
            // 
            this.consecutiveFromLabel.AutoSize = true;
            this.consecutiveFromLabel.Location = new System.Drawing.Point(139, 58);
            this.consecutiveFromLabel.Name = "consecutiveFromLabel";
            this.consecutiveFromLabel.Size = new System.Drawing.Size(66, 24);
            this.consecutiveFromLabel.TabIndex = 6;
            this.consecutiveFromLabel.Text = "From:";
            // 
            // traceTextBox
            // 
            this.traceTextBox.Location = new System.Drawing.Point(7, 107);
            this.traceTextBox.Multiline = true;
            this.traceTextBox.Name = "traceTextBox";
            this.traceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.traceTextBox.Size = new System.Drawing.Size(973, 942);
            this.traceTextBox.TabIndex = 8;
            // 
            // JrEastTimetableExtractionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.traceTextBox);
            this.Controls.Add(this.consecutiveFromNumericUpDown);
            this.Controls.Add(this.consecutiveFromLabel);
            this.Controls.Add(this.consecutiveToNumericUpDown);
            this.Controls.Add(this.consecutiveTolabel);
            this.Controls.Add(this.consecutiveCheckBox);
            this.Controls.Add(this.extractTimetableButton);
            this.Controls.Add(this.testTargetTextBox);
            this.Controls.Add(this.baseURLLabel);
            this.Name = "JrEastTimetableExtractionControl";
            this.Size = new System.Drawing.Size(1047, 1088);
            ((System.ComponentModel.ISupportInitialize)(this.consecutiveToNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consecutiveFromNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label baseURLLabel;
        private System.Windows.Forms.TextBox testTargetTextBox;
        private System.Windows.Forms.Button extractTimetableButton;
        private System.Windows.Forms.CheckBox consecutiveCheckBox;
        private System.Windows.Forms.Label consecutiveTolabel;
        private System.Windows.Forms.NumericUpDown consecutiveToNumericUpDown;
        private System.Windows.Forms.NumericUpDown consecutiveFromNumericUpDown;
        private System.Windows.Forms.Label consecutiveFromLabel;
        private System.Windows.Forms.TextBox traceTextBox;
    }
}
