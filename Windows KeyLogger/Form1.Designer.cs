namespace Windows_KeyLogger
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.Listen = new System.Windows.Forms.Button();
            this.richLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Listen
            // 
            this.Listen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Listen.Location = new System.Drawing.Point(22, 678);
            this.Listen.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Listen.Name = "Listen";
            this.Listen.Size = new System.Drawing.Size(139, 46);
            this.Listen.TabIndex = 0;
            this.Listen.Text = "Hook";
            this.Listen.UseVisualStyleBackColor = true;
            this.Listen.Click += new System.EventHandler(this.button1_Click);
            // 
            // richLog
            // 
            this.richLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richLog.Location = new System.Drawing.Point(22, 24);
            this.richLog.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.richLog.Name = "richLog";
            this.richLog.ReadOnly = true;
            this.richLog.Size = new System.Drawing.Size(1406, 637);
            this.richLog.TabIndex = 1;
            this.richLog.Text = "";
            this.richLog.TextChanged += new System.EventHandler(this.richLog_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 747);
            this.Controls.Add(this.richLog);
            this.Controls.Add(this.Listen);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Key Logger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Listen;
        public System.Windows.Forms.RichTextBox richLog;
    }
}

