namespace CefGlueWinForms.EmbeddedBrowser
{
    partial class EmbeddedBrowserForm
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
            this.embeddedCefBrowserControl1 = new EmbeddedCefBrowserControl();
            this.SuspendLayout();
            // 
            // embeddedCefBrowserControl1
            // 
            this.embeddedCefBrowserControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.embeddedCefBrowserControl1.BrowserSettings = null;
            this.embeddedCefBrowserControl1.Location = new System.Drawing.Point(0, 0);
            this.embeddedCefBrowserControl1.Name = "embeddedCefBrowserControl1";
            this.embeddedCefBrowserControl1.Size = new System.Drawing.Size(284, 261);
            this.embeddedCefBrowserControl1.TabIndex = 0;
            this.embeddedCefBrowserControl1.Text = "embeddedCefBrowserControl1";
            // 
            // EmbeddedBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.embeddedCefBrowserControl1);
            this.Name = "EmbeddedBrowserForm";
            this.Text = "EmbeddedBrowserForm";
            this.ResumeLayout(false);

        }

        #endregion

        private EmbeddedCefBrowserControl embeddedCefBrowserControl1;
    }
}